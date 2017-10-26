Imports BACSOFT.Db.SqlHelper

Public Class frmThongBao

    Private Sub frmThongBao_ClientSizeChanged(sender As Object, e As System.EventArgs) Handles Me.ClientSizeChanged
        'LoadDS()
    End Sub

    Public Sub LoadDS()
        ShowWaiting("Đang tải thông báo ...")
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT row_number() over (order by DaXem,ThoiGian DESC ) AS STT, ID,ThoiGian,NoiDung,DaXem FROM tblThongBao WHERE IDNhanVien=" & TaiKhoan & " order By DaXem, ThoiGian DESC")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    'Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
    '    AddParameter("@DaXem", True)
    '    AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
    '    If doUpdate("tblThongBao", "ID=@ID") Is Nothing Then
    '        ShowBaoLoi(LoiNgoaiLe)
    '    Else
    '        gdvCT.SetFocusedRowCellValue("DaXem", True)
    '        gdvCT.CloseEditor()
    '        gdvCT.UpdateCurrentRow()
    '    End If
    'End Sub


    Private Sub gdvCT_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvCT.RowStyle
        On Error Resume Next
        If gdvCT.GetRowCellValue(e.RowHandle, "DaXem") = False Then
            e.Appearance.Font = New Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold)
        Else
            e.Appearance.Font = New Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Regular)
        End If
    End Sub


    Private Sub mChuaXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuaXem.ItemClick
        For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
            AddParameter("@DaXem", False)
            AddParameterWhere("@ID", gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "ID"))
            If doUpdate("tblThongBao", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "DaXem", False)

            End If
        Next
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        
    End Sub

    Private Sub mDaXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDaXem.ItemClick
        For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
            AddParameter("@DaXem", True)
            AddParameterWhere("@ID", gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "ID"))
            If doUpdate("tblThongBao", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "DaXem", True)

            End If
        Next
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
    End Sub

    Private Sub gdv_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdv.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
    End Sub
End Class
