Imports BACSOFT.Db.SqlHelper

Public Class frmHinhThucTT


    Private Sub frmHinhThucTT_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDS()
    End Sub

    Public Sub LoadDS()
        Dim sql As String = ""
        sql &= " SELECT tblHinhThucTTKH.ID,tblHinhThucTTKH.HinhThucTT_VIE,tblHinhThucTTKHCT.ThanhToanPT,tblHinhThucTTKHCT.Ngay,(SELECT NoiDung FROM tblTuDien WHERE Loai=5 AND Ma=tblHinhThucTTKHCT.IDThoiDiemTT)ThoiDiem"
        sql &= " FROM tblHinhThucTTKH "
        sql &= " LEFT OUTER JOIN tblHinhThucTTKHCT ON tblHinhThucTTKH.ID=tblHinhThucTTKHCT.IDHinhThucTTKH "
        sql &= " ORDER BY HinhThucTT_VIE "

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btThem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, pmThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNHinhThucTT
        f.Tag = "frmHinhThucTT"
        f.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        TrangThai.isUpdate = True
        Dim _index As Integer = gdvCT.FocusedRowHandle
        MaTuDien = gdvCT.GetFocusedRowCellValue("ID")
        Dim f As New frmCNHinhThucTT
        f.Tag = "frmHinhThucTT"
        f.ShowDialog()
        gdvCT.FocusedRowHandle = _index
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        If e.KeyCode = Keys.Delete Then
            btXoa_ItemClick(New Object, New EventArgs)
        ElseIf e.Control = Keys.N Then
            btThem.PerformClick()
        ElseIf e.Control = Keys.E Then
            btSua.PerformClick()
        End If
    End Sub

    Private Sub btXoa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoa.ItemClick, pmXoa.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If ShowCauHoi("Xóa hình thức thanh toán được chọn ?") Then
            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If Not doDelete("tblHinhThucTTKH", "ID=@ID") Is Nothing Then
                gdvCT.DeleteSelectedRows()
                ShowAlert("Đã xóa !")
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

End Class
