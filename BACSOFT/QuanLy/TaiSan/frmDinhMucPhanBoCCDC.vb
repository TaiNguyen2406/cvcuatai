Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Public Class frmDinhMucPhanBoCCDC
    Private Sub loadData()
        riLueTSorCCDC.DataSource = tableTSorCCDC()
        Dim query As String = "select * from Taisan_DinhMuc where TSorCCDC=2 order by ngayapdung desc"
        gcDinhMucPhanBo.DataSource() = ExecuteSQLDataTable(query)
    End Sub

    Private Sub frmDinhMucTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub gvDinhMucPhanBo_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvDinhMucPhanBo.RowUpdated
        gvDinhMucPhanBo.UpdateCurrentRow()
        AddParameter("@TSorCCDC", 2)
        AddParameter("@tennhomccdc", gvDinhMucPhanBo.GetFocusedRowCellValue("tennhomccdc"))
        AddParameter("@thoigiankh", gvDinhMucPhanBo.GetFocusedRowCellValue("thoigiankh"))
        AddParameter("@ngayapdung", gvDinhMucPhanBo.GetFocusedRowCellValue("ngayapdung"))
        AddParameter("@ghichu", gvDinhMucPhanBo.GetFocusedRowCellValue("ghichu"))

        If Not IsDBNull(gvDinhMucPhanBo.GetFocusedRowCellValue("id")) Then
            AddParameterWhere("@id", gvDinhMucPhanBo.GetFocusedRowCellValue("id"))
            If doUpdate("Taisan_DinhMuc", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)

            Else
                ' ShowAlert("Đã cập nhật định mức !")
                gvDinhMucPhanBo.SetFocusedRowCellValue("id", gvDinhMucPhanBo.GetFocusedRowCellValue("id"))
            End If
        Else
            If doInsert("Taisan_DinhMuc") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gvDinhMucPhanBo.DeleteSelectedRows()
            Else
                ' ShowAlert("Đã thêm định mức !")
                gvDinhMucPhanBo.SetFocusedRowCellValue("id", gvDinhMucPhanBo.GetFocusedRowCellValue("id"))
            End If
        End If
        loadData()

    End Sub

    Private Sub mXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoa.ItemClick
        If ShowCauHoi("Xóa định mức  ?") Then
            AddParameterWhere("@id", gvDinhMucPhanBo.GetFocusedRowCellValue("id"))
            If doDelete("Taisan_DinhMuc", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gvDinhMucPhanBo.DeleteSelectedRows()
                ShowAlert("Đã xoá!")
            End If
        End If
    End Sub

    Private Sub gvDinhMucPhanBo_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gvDinhMucPhanBo.KeyDown
        If e.KeyCode = Keys.Delete Then
            mXoa.PerformClick()
        End If

    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        loadData()
    End Sub

    Private Sub gvDinhMucPhanBo_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gvDinhMucPhanBo.DoubleClick
        '   gvDinhMucPhanBo.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")

    End Sub
    Private Sub gvDinhMucPhanBo_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvDinhMucPhanBo.HiddenEditor
        '   gvDinhMucPhanBo.OptionsBehavior.Editable = False
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)

    End Sub
End Class