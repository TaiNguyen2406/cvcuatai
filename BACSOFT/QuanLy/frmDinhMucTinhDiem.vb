Imports BACSOFT.Db.SqlHelper

Public Class frmDinhMucTinhDiem
    Private _exit As Boolean = False

    Private Sub frmDinhMucThuongPhong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tbThang.EditValue = Today.ToString("MM")
        tbNam.EditValue = Today.Year
        LoadDSPhong()
        LoadDS()
    End Sub

    Private Sub LoadDSPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,Ten FROM NhanSu_BoPhan ORDER BY STT")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDS()

        AddParameterWhere("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM DiemSo_DinhMuc WHERE Thang=@Thang ORDER BY IDBoPhan,Level DESC, Class DESC")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If _exit Then Exit Sub
        Try

            AddParameter("@CapDo", gdvCT.GetFocusedRowCellValue("CapDo"))
            AddParameter("@Level", gdvCT.GetFocusedRowCellValue("Level"))
            AddParameter("@Class", gdvCT.GetFocusedRowCellValue("Class"))
            AddParameter("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
            AddParameter("@LoiNhuanGop", gdvCT.GetFocusedRowCellValue("LoiNhuanGop"))
            AddParameter("@LuongThamChieu", gdvCT.GetFocusedRowCellValue("LuongThamChieu"))
            AddParameter("@DiemThamChieu", gdvCT.GetFocusedRowCellValue("DiemThamChieu"))
            AddParameter("@HeSoThuong", gdvCT.GetFocusedRowCellValue("HeSoThuong"))
            AddParameter("@DiemPTMax", gdvCT.GetFocusedRowCellValue("DiemPTMax"))
            AddParameter("@DiemThuong", gdvCT.GetFocusedRowCellValue("DiemThuong"))
            AddParameter("@IDBoPhan", gdvCT.GetFocusedRowCellValue("IDBoPhan"))
            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
                Dim _id As Object = doInsert("DiemSo_DinhMuc")
                If _id Is Nothing Then Throw New Exception(LoiNgoaiLe)
                _exit = True
                gdvCT.SetFocusedRowCellValue("ID", _id)
                gdvCT.CloseEditor()
                gdvCT.UpdateCurrentRow()
                _exit = False
            Else
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("DiemSo_DinhMuc", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
            ShowAlert("Đã cập nhật")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            LoadDS()
        End Try
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.FieldName = "IDBoPhan" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub

    Private Sub mLayThangTruoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLayThangTruoc.ItemClick
        If ShowCauHoi("Lấy dữ liệu tháng " & DateAdd(DateInterval.Day, -1, New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1)).ToString("MM/yyyy") & " sang tháng " & New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1).ToString("MM/yyyy")) Then
            Dim sql As String = ""
            sql &= " INSERT INTO DiemSo_DinhMuc(CapDo,IDBoPhan,LoiNhuanGop,LuongThamChieu,DiemThamChieu,HeSoThuong,DiemPTMax,DiemThuong,Level,Class,Thang)"
            sql &= " SELECT CapDo,IDBoPhan,LoiNhuanGop,LuongThamChieu,DiemThamChieu,HeSoThuong,DiemPTMax,DiemThuong,Level,Class,@Thang"
            sql &= " FROM DiemSo_DinhMuc"
            sql &= " WHERE Thang=@ThangTruoc"
            AddParameterWhere("@ThangTruoc", DateAdd(DateInterval.Day, -1, New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1)).ToString("MM/yyyy"))
            AddParameterWhere("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật !")
                btXem.PerformClick()
            End If

        End If
    End Sub
End Class