Imports BACSOFT.Db.SqlHelper
Public Class frmSoDuNganHangDauKy


    Private Sub frmSoDuNganHangDauKy_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtNam.EditValue = GetServerTime().Year
        'LoadDuLieu()
    End Sub


    Private Sub LoadDuLieu()

        Dim sql As String = "select MaSo,Ten,LoaiTK, "
        sql &= "isnull((select DuNo from sodunganhangdauky where nam = " & txtNam.EditValue & " and IdTaiKhoan = taikhoan.id),0)DuNo, "
        sql &= "isnull((select DuCo from sodunganhangdauky where nam = " & txtNam.EditValue & " and IdTaiKhoan = taikhoan.id),0)DuCo "
        sql &= "from taikhoan order by ten "

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
        gdv.DataSource = dt

    End Sub


    Private Sub btnTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDS.ItemClick
        LoadDuLieu()
    End Sub


    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        If e.Column.FieldName = "DuNo" Or e.Column.FieldName = "DuCo" Then
            If IsNumeric(e.CellValue) AndAlso e.CellValue = 0 Then e.DisplayText = ""
        End If
    End Sub



    Private Sub gdvData_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvData.CellValueChanged

        If e.RowHandle < 0 Then Exit Sub


        If e.Column.FieldName = "DuNo" Then

            If gdvData.GetRowCellValue(e.RowHandle, "DuNo") Is DBNull.Value Then
                gdvData.SetRowCellValue(e.RowHandle, "DuNo", 0)
            End If

            If gdvData.GetRowCellValue(e.RowHandle, "DuNo") = 0 And gdvData.GetRowCellValue(e.RowHandle, "DuCo") = 0 Then
                AddParameter("@MaSo", gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                AddParameter("@Nam", txtNam.EditValue)
                ExecuteSQLNonQuery("DELETE FROM SODUNGANHANGDAUKY WHERE MaSo = @MaSo AND Nam = @Nam")
                ShowAlert("Đã xóa dư nợ đầu kỳ tài khoản " & gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
            Else
                AddParameter("@MaSo", gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                AddParameter("@Nam", txtNam.EditValue)
                Dim sql As String = "SELECT ID FROM SODUNGANHANGDAUKY WHERE MaSo = @MaSo AND Nam = @Nam"
                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                If dt.Rows.Count = 0 Then
                    AddParameter("@MaSo", gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                    AddParameter("@Nam", txtNam.EditValue)
                    AddParameter("@DuNo", gdvData.GetRowCellValue(e.RowHandle, "DuNo"))
                    doInsert("SODUNGANHANGDAUKY")
                    ShowAlert("Đã cập nhật dư nợ " & gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                Else
                    AddParameter("@DuNo", gdvData.GetRowCellValue(e.RowHandle, "DuNo"))
                    AddParameterWhere("@MaSo", gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                    AddParameterWhere("@Nam", txtNam.EditValue)
                    doUpdate("SODUNGANHANGDAUKY", "MaSo = @MaSo AND Nam = @Nam")
                    ShowAlert("Đã cập nhật dư nợ " & gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                End If
            End If
        ElseIf e.Column.FieldName = "DuCo" Then

            If gdvData.GetRowCellValue(e.RowHandle, "DuCo") Is DBNull.Value Then
                gdvData.SetRowCellValue(e.RowHandle, "DuCo", 0)
            End If

            If gdvData.GetRowCellValue(e.RowHandle, "DuCo") = 0 And gdvData.GetRowCellValue(e.RowHandle, "DuNo") = 0 Then
                AddParameter("@MaSo", gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                AddParameter("@Nam", txtNam.EditValue)
                ExecuteSQLNonQuery("DELETE FROM SODUNGANHANGDAUKY WHERE MaSo = @MaSo AND Nam = @Nam")
                ShowAlert("Đã xóa dư nợ đầu kỳ " & gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
            Else
                AddParameter("@MaSo", gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                AddParameter("@Nam", txtNam.EditValue)
                Dim sql As String = "SELECT ID FROM SODUNGANHANGDAUKY WHERE MaSo = @MaSo AND Nam = @Nam"
                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                If dt.Rows.Count = 0 Then
                    AddParameter("@MaSo", gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                    AddParameter("@Nam", txtNam.EditValue)
                    AddParameter("@DuCo", gdvData.GetRowCellValue(e.RowHandle, "DuCo"))
                    doInsert("SODUNGANHANGDAUKY")
                    ShowAlert("Đã cập nhật dư nợ " & gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                Else
                    AddParameter("@DuCo", gdvData.GetRowCellValue(e.RowHandle, "DuCo"))
                    AddParameterWhere("@MaSo", gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                    AddParameterWhere("@Nam", txtNam.EditValue)
                    doUpdate("SODUNGANHANGDAUKY", "MaSo = @MaSo AND Nam = @Nam")
                    ShowAlert("Đã cập nhật dư nợ " & gdvData.GetRowCellValue(e.RowHandle, "MaSo"))
                End If
            End If
        End If

    End Sub






End Class
