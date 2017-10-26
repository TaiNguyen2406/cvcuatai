Imports BACSOFT.Db.SqlHelper

Public Class frmDinhMucThuongPhong
    Private _exit As Boolean = False

    Private Sub frmDinhMucThuongPhong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSPhong()
        LoadDS()
    End Sub

    Private Sub LoadDSPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDS()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblDMThuongPhong ORDER BY ID DESC")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If _exit Then Exit Sub
        Try
            AddParameter("@Thang", gdvCT.GetFocusedRowCellValue("Thang"))
            AddParameter("@LoiNhuanGop", gdvCT.GetFocusedRowCellValue("LoiNhuanGop"))
            AddParameter("@LuongThamChieu", gdvCT.GetFocusedRowCellValue("LuongThamChieu"))
            AddParameter("@DiemThamChieu", gdvCT.GetFocusedRowCellValue("DiemThamChieu"))
            AddParameter("@HeSoThuong", gdvCT.GetFocusedRowCellValue("HeSoThuong"))
            AddParameter("@DiemPTMax", gdvCT.GetFocusedRowCellValue("DiemPTMax"))
            AddParameter("@DiemThuong", gdvCT.GetFocusedRowCellValue("DiemThuong"))
            AddParameter("@IDPhong", gdvCT.GetFocusedRowCellValue("IDPhong"))
            AddParameter("@MaThuong", gdvCT.GetFocusedRowCellValue("MaThuong"))
            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
                Dim _id As Object = doInsert("tblDMThuongPhong")
                If _id Is Nothing Then Throw New Exception(LoiNgoaiLe)
                _exit = True
                gdvCT.SetFocusedRowCellValue("ID", _id)
                gdvCT.CloseEditor()
                gdvCT.UpdateCurrentRow()
                _exit = False
            Else
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("tblDMThuongPhong", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
            ShowAlert("Đã cập nhật")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.FieldName = "IDPhong" Then
            SendKeys.Send("{F4}")
        End If
    End Sub
End Class