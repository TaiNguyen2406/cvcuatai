Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Public Class frmDinhMucKhauHao
    Private Sub loadData()
        riLueTSorCCDC.DataSource = tableTSorCCDC()
        Dim query As String = "select * from Taisan_DinhMuc where TSorCCDC=1 order by ngayapdung desc"
        gcDinhMucKhauHao.DataSource() = ExecuteSQLDataTable(query)
    End Sub

    Private Sub frmDinhMucTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub gvDinhMucKhauHao_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvDinhMucKhauHao.RowUpdated
        gvDinhMucKhauHao.UpdateCurrentRow()
        AddParameter("@TSorCCDC", 1)
        AddParameter("@mucdau", gvDinhMucKhauHao.GetFocusedRowCellValue("mucdau"))
        AddParameter("@muccuoi", gvDinhMucKhauHao.GetFocusedRowCellValue("muccuoi"))
        AddParameter("@thoigiankh", gvDinhMucKhauHao.GetFocusedRowCellValue("thoigiankh"))
        AddParameter("@ngayapdung", gvDinhMucKhauHao.GetFocusedRowCellValue("ngayapdung"))
        '  AddParameter("@ghichu", gvDinhMucKhauHao.GetFocusedRowCellValue("ghichu"))

        If Not IsDBNull(gvDinhMucKhauHao.GetFocusedRowCellValue("id")) Then
            AddParameterWhere("@id", gvDinhMucKhauHao.GetFocusedRowCellValue("id"))
            If doUpdate("Taisan_DinhMuc", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)

            Else
                ' ShowAlert("Đã cập nhật định mức !")
                gvDinhMucKhauHao.SetFocusedRowCellValue("id", gvDinhMucKhauHao.GetFocusedRowCellValue("id"))
            End If
        Else
            If doInsert("Taisan_DinhMuc") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gvDinhMucKhauHao.DeleteSelectedRows()
            Else
                ' ShowAlert("Đã thêm định mức !")
                gvDinhMucKhauHao.SetFocusedRowCellValue("id", gvDinhMucKhauHao.GetFocusedRowCellValue("id"))
            End If
        End If
        loadData()
     
    End Sub

    Private Sub mXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoa.ItemClick
        If ShowCauHoi("Xóa định mức  ?") Then
            AddParameterWhere("@id", gvDinhMucKhauHao.GetFocusedRowCellValue("id"))
            If doDelete("Taisan_DinhMuc", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gvDinhMucKhauHao.DeleteSelectedRows()
                ShowAlert("Đã xoá!")
            End If
        End If
    End Sub

    Private Sub gvDinhMucKhauHao_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvDinhMucKhauHao.KeyDown
        If e.KeyCode = Keys.Delete Then
            mXoa.PerformClick()
        End If
        
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        loadData()
    End Sub

    Private Sub gvDinhMucKhauHao_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvDinhMucKhauHao.DoubleClick
        '   gvDinhMucKhauHao.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")

    End Sub
    Private Sub gvDinhMucKhauHao_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvDinhMucKhauHao.HiddenEditor
        '   gvDinhMucKhauHao.OptionsBehavior.Editable = False
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)

    End Sub
End Class