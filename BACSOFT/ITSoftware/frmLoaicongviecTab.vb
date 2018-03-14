Imports BACSOFT.Db.SqlHelper

Public Class frmLoaicongviecTab

    ' Method này select tất cả bản ghi của bảng tblLoaicongviec_IT
    ' Và fill các bản ghi đó vào GridControl
    Sub SelectLoaicongviec()
        Dim sql = "SELECT * FROM tblLoaicongviec_IT"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        ' Fill dữ liệu vào GridControl
        If Not tb Is Nothing Then
            grdDSCV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub frmLoaicongviecTab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelectLoaicongviec()
        NewDefaultRow()
    End Sub

    ' Method này Khởi tạo mặc định dòng Empty chờ nhập công việc mới
    Sub NewDefaultRow()
        grdViewDSCV.AddNewRow()
        grdViewDSCV.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        grdViewDSCV.OptionsBehavior.Editable = True
    End Sub

    Private Sub btnLuu_ItemClick_1(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLuu.ItemClick

        ' Bắt đầu phiên
        BeginTransaction()
        Dim _iD, _iDUpdate As Object

        For i = 0 To grdViewDSCV.DataRowCount - 1

            ' Insert
            If grdViewDSCV.GetRowCellValue(i, "Id").ToString() = "" And grdViewDSCV.GetRowCellValue(i, "Tencongviec").ToString() <> "" Then
                AddParameter("@Tencongviec", grdViewDSCV.GetRowCellValue(i, "Tencongviec").ToString())
                AddParameter("@Ghichu", grdViewDSCV.GetRowCellValue(i, "Ghichu").ToString())
                AddParameter("@Active", grdViewDSCV.GetRowCellValue(i, "Active").ToString())
                _iD = doInsert("tblLoaicongviec_IT")
                If _iD Is Nothing Then
                    ' Có lỗi thì Huỷ phiên
                    RollBackTransaction()
                    ' Báo lỗi
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If
            Else
                ' Update
                AddParameter("@Tencongviec", grdViewDSCV.GetRowCellValue(i, "Tencongviec").ToString())
                AddParameter("@Ghichu", grdViewDSCV.GetRowCellValue(i, "Ghichu").ToString())
                AddParameter("@Active", grdViewDSCV.GetRowCellValue(i, "Active").ToString())
                AddParameterWhere("@Id", grdViewDSCV.GetRowCellValue(i, "Id").ToString())
                _iDUpdate = doUpdate("tblLoaicongviec_IT", "Id = @Id")

                If _iDUpdate Is Nothing Then
                    ' Có lỗi thì Huỷ phiên
                    RollBackTransaction()
                    ' Báo lỗi
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If
            End If
        Next

        ' Xác nhận phiên
        ComitTransaction()

        ' Refresh loại công việc
        SelectLoaicongviec()

        ShowAlert("Đã cập nhật thành công!")
    End Sub

    Private Sub btnXem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXem.ItemClick
        SelectLoaicongviec()
    End Sub

    Private Sub RepositoryItemCheckEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEdit1.EditValueChanged
        ' Đảm bảo ngay sau khi người dùng edit thông tin, thì GridControl sẽ nhận dữ liệu ngay khi vẫn còn Focus trên row đang edit.
        grdViewDSCV.PostEditor()
    End Sub
End Class
