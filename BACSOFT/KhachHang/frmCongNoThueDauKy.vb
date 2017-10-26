Imports BACSOFT.Db.SqlHelper
Public Class frmCongNoThueDauKy


    Private Sub frmCongNoThueDauKy_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtNam.EditValue = GetServerTime().Year
        'LoadDuLieu()
    End Sub


    Private Sub LoadDuLieu()

        Dim sql As String = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi, "
        sql &= "ISNULL((SELECT SoDuNo FROM CONGNOTHUEDAUKY WHERE IdKH = KHACHHANG.ID AND Nam = " & txtNam.EditValue & "),0)SoDuNo, "
        sql &= "ISNULL((SELECT SoDuCo FROM CONGNOTHUEDAUKY WHERE IdKH = KHACHHANG.ID AND Nam = " & txtNam.EditValue & "),0)SoDuCo "
        sql &= "FROM KHACHHANG "
        If cmbDoiTuong.EditValue <> -1 Then
            sql &= " WHERE ttcKhachhang = " & cmbDoiTuong.EditValue
        End If

        sql &= " ORDER BY Ten "

        gdv.DataSource = ExecuteSQLDataTable(sql)
    End Sub


    Private Sub btnTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDS.ItemClick
        LoadDuLieu()
    End Sub


    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        If e.Column.FieldName = "SoDuNo" Or e.Column.FieldName = "SoDuCo" Then
            If IsNumeric(e.CellValue) AndAlso e.CellValue = 0 Then e.DisplayText = ""
        End If
    End Sub




    Private Sub gdvData_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvData.CellValueChanged

        If e.RowHandle < 0 Then Exit Sub


        If e.Column.FieldName = "SoDuNo" Then

            If gdvData.GetRowCellValue(e.RowHandle, "SoDuNo") Is DBNull.Value Then
                gdvData.SetRowCellValue(e.RowHandle, "SoDuNo", 0)
            End If

            If gdvData.GetRowCellValue(e.RowHandle, "SoDuNo") = 0 And gdvData.GetRowCellValue(e.RowHandle, "SoDuCo") = 0 Then
                AddParameter("@IdKH", gdvData.GetRowCellValue(e.RowHandle, "ID"))
                AddParameter("@Nam", txtNam.EditValue)
                ExecuteSQLNonQuery("DELETE FROM CONGNOTHUEDAUKY WHERE IdKH = @IdKH AND Nam = @Nam")
                ShowAlert("Đã xóa dư nợ đầu kỳ " & gdvData.GetRowCellValue(e.RowHandle, "Ten"))
            Else
                AddParameter("@IdKH", gdvData.GetRowCellValue(e.RowHandle, "ID"))
                AddParameter("@Nam", txtNam.EditValue)
                Dim sql As String = "SELECT ID FROM CONGNOTHUEDAUKY WHERE IdKH = @IdKH AND Nam = @Nam"
                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                If dt.Rows.Count = 0 Then
                    AddParameter("@IdKH", gdvData.GetRowCellValue(e.RowHandle, "ID"))
                    AddParameter("@Nam", txtNam.EditValue)
                    AddParameter("@SoDuNo", gdvData.GetRowCellValue(e.RowHandle, "SoDuNo"))
                    doInsert("CONGNOTHUEDAUKY")
                    ShowAlert("Đã cập nhật dư nợ " & gdvData.GetRowCellValue(e.RowHandle, "Ten"))
                Else
                    AddParameter("@SoDuNo", gdvData.GetRowCellValue(e.RowHandle, "SoDuNo"))
                    AddParameterWhere("@IdKH", gdvData.GetRowCellValue(e.RowHandle, "ID"))
                    AddParameterWhere("@Nam", txtNam.EditValue)
                    doUpdate("CONGNOTHUEDAUKY", "IdKH = @IdKH AND Nam = @Nam")
                    ShowAlert("Đã cập nhật dư nợ " & gdvData.GetRowCellValue(e.RowHandle, "Ten"))
                End If
            End If
        ElseIf e.Column.FieldName = "SoDuCo" Then

            If gdvData.GetRowCellValue(e.RowHandle, "SoDuCo") Is DBNull.Value Then
                gdvData.SetRowCellValue(e.RowHandle, "SoDuCo", 0)
            End If

            If gdvData.GetRowCellValue(e.RowHandle, "SoDuCo") = 0 And gdvData.GetRowCellValue(e.RowHandle, "SoDuNo") = 0 Then
                AddParameter("@IdKH", gdvData.GetRowCellValue(e.RowHandle, "ID"))
                AddParameter("@Nam", txtNam.EditValue)
                ExecuteSQLNonQuery("DELETE FROM CONGNOTHUEDAUKY WHERE IdKH = @IdKH AND Nam = @Nam")
                ShowAlert("Đã xóa dư nợ đầu kỳ " & gdvData.GetRowCellValue(e.RowHandle, "Ten"))
            Else
                AddParameter("@IdKH", gdvData.GetRowCellValue(e.RowHandle, "ID"))
                AddParameter("@Nam", txtNam.EditValue)
                Dim sql As String = "SELECT ID FROM CONGNOTHUEDAUKY WHERE IdKH = @IdKH AND Nam = @Nam"
                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                If dt.Rows.Count = 0 Then
                    AddParameter("@IdKH", gdvData.GetRowCellValue(e.RowHandle, "ID"))
                    AddParameter("@Nam", txtNam.EditValue)
                    AddParameter("@SoDuCo", gdvData.GetRowCellValue(e.RowHandle, "SoDuCo"))
                    doInsert("CONGNOTHUEDAUKY")
                    ShowAlert("Đã cập nhật dư nợ " & gdvData.GetRowCellValue(e.RowHandle, "Ten"))
                Else
                    AddParameter("@SoDuCo", gdvData.GetRowCellValue(e.RowHandle, "SoDuCo"))
                    AddParameterWhere("@IdKH", gdvData.GetRowCellValue(e.RowHandle, "ID"))
                    AddParameterWhere("@Nam", txtNam.EditValue)
                    doUpdate("CONGNOTHUEDAUKY", "IdKH = @IdKH AND Nam = @Nam")
                    ShowAlert("Đã cập nhật dư nợ " & gdvData.GetRowCellValue(e.RowHandle, "Ten"))
                End If
            End If
        End If

    End Sub






End Class
