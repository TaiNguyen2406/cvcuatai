Imports BACSOFT.Db.SqlHelper
Imports System.Xml
Imports System.Globalization

Public Class frmTienTe

    Private Sub frmTienTe_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDS()
    End Sub

    Private Sub LoadDS()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT *,ID AS ID2 FROM tblTienTe")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            If ShowCauHoi("Xóa loại tiền tệ được chọn ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If Not doDelete("tblTienTe", "ID=@ID") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
            If e.KeyCode = Keys.F5 Then
                LoadDS()
            End If
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Else
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        End If
        AddParameter("@ID", gdvCT.GetFocusedRowCellValue("ID"))
        AddParameter("@Ten", gdvCT.GetFocusedRowCellValue("Ten"))
        AddParameter("@TyGia", gdvCT.GetFocusedRowCellValue("TyGia"))
        AddParameter("@Ngay", gdvCT.GetFocusedRowCellValue("Ngay"))
        AddParameter("@TyGiaBAC", gdvCT.GetFocusedRowCellValue("TyGiaBAC"))
        AddParameter("@TyGiaHQ", gdvCT.GetFocusedRowCellValue("TyGiaHQ"))
        If IsDBNull(gdvCT.GetFocusedRowCellValue("ID2")) Then
            If doInsert("tblTienTe") Is Nothing Then
                gdvCT.DeleteSelectedRows()
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvCT.SetFocusedRowCellValue("ID2", gdvCT.GetFocusedRowCellValue("ID"))
            End If
        Else
            AddParameterWhere("@ID2", gdvCT.GetFocusedRowCellValue("ID2"))
            If doUpdate("tblTienTe", "ID=@ID2") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    Private Sub btCapNhatTyGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCapNhatTyGia.ItemClick
        LayTyGia()
        LoadDS()
    End Sub


End Class
