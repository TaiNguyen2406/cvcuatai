Imports BACSOFT.Db.SqlHelper
Imports System.Linq

Public Class frmVatTuPhu

    Public idVatTu As Object

    Private Sub frmVatTuPhu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        LoadDsNhomVT()
        LoadDsTenVT()
        LoadDsHangSX()

        Dim sql As String = "SELECT dbo.tblGhepVatTu.ID, dbo.tblGhepVatTu.SoLuong, dbo.TENVATTU.ten AS TenVatTu, dbo.TENHANGSANXUAT.TEN AS HangSX, dbo.TENNHOM.Ten AS NhomVT, "
        sql &= "dbo.TENDONVITINH.TEN AS DVT, dbo.VATTU.Model, dbo.VATTU.Code, dbo.VATTU.Thongso,  dbo.tblGhepVatTu.IDVatTuPhu "
        sql &= "FROM dbo.VATTU INNER JOIN "
        sql &= "dbo.TENVATTU ON dbo.VATTU.IDTenvattu = dbo.TENVATTU.ID INNER JOIN "
        sql &= "dbo.TENHANGSANXUAT ON dbo.VATTU.IDHangSanxuat = dbo.TENHANGSANXUAT.ID INNER JOIN "
        sql &= "dbo.TENNHOM ON dbo.VATTU.IDTennhom = dbo.TENNHOM.ID INNER JOIN "
        sql &= "dbo.TENDONVITINH ON dbo.VATTU.IDDonvitinh = dbo.TENDONVITINH.ID RIGHT OUTER JOIN "
        sql &= "dbo.tblGhepVatTu ON dbo.VATTU.ID = dbo.tblGhepVatTu.IDVatTu "
        sql &= "WHERE dbo.tblGhepVatTu.IDVatTu =  " & idVatTu

        gdv.DataSource = ExecuteSQLDataTable(sql)

    End Sub


    Private Sub btnLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLuuLai.ItemClick
        gdvDataVT.CloseEditor()
        gdvData.UpdateCurrentRow()
        Try
            BeginTransaction()
            For i As Integer = 0 To gdvData.RowCount - 1
                AddParameter("@IDVatTu", idVatTu)
                AddParameter("@IDVatTuPhu", gdvData.GetRowCellValue(i, "IDVatTuPhu"))
                AddParameter("@SoLuong", gdvData.GetRowCellValue(i, "SoLuong"))
                If gdvData.GetRowCellValue(i, "ID") Is DBNull.Value Then
                    Dim idVTP As Object = doInsert("tblGhepVatTu")
                    If idVTP Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvData.SetRowCellValue(i, "ID", idVTP)
                Else
                    AddParameterWhere("@dk_ID", gdvData.GetRowCellValue(i, "ID"))
                    If doUpdate("tblGhepVatTu", "ID = @dk_ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next
            ShowAlert("Cập nhật vật tư phụ thành công!")
            ComitTransaction()
        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try
       
    End Sub


    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        gdvDataVT.CloseEditor()
        gdvData.UpdateCurrentRow()
        If gdvData.GetFocusedRowCellValue("ID") Is DBNull.Value Then
            gdvData.DeleteRow(gdvData.FocusedRowHandle)
            Exit Sub
        End If
        If ShowCauHoi("Chắc chắn xóa vật tư phụ này?") Then
            If ExecuteSQLNonQuery("DELETE FROM tblGhepVatTu WHERE ID = " & gdvData.GetFocusedRowCellValue("ID")) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            gdvData.DeleteRow(gdvData.FocusedRowHandle)
        End If

    End Sub

    Private Sub btnDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDong.ItemClick
        Me.Close()
    End Sub


    Private Sub LoadDsNhomVT()
        Dim sql As String = "SELECT ID,Ten FROM tennhom ORDER BY Ten ASC"
        rCmbNhomVT.DataSource = ExecuteSQLDataTable(sql)
        cmbNhomVT.EditValue = DBNull.Value
    End Sub

    Private Sub LoadDsTenVT()
        Dim sql As String = "SELECT ID,ten from tenvattu ORDER BY ten ASC"
        rCmbTenVT.DataSource = ExecuteSQLDataTable(sql)
        cmbTenVT.EditValue = DBNull.Value
    End Sub

    Private Sub LoadDsHangSX()
        Dim sql As String = "SELECT ID,TEN from tenhangsanxuat ORDER BY ten ASC"
        rCmbHangSX.DataSource = ExecuteSQLDataTable(sql)
        cmbHangSX.EditValue = DBNull.Value
    End Sub

    Private Sub rCmbNhomVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCmbNhomVT.ButtonClick
        cmbNhomVT.EditValue = DBNull.Value
    End Sub

    Private Sub rCmbTenVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCmbTenVT.ButtonClick
        cmbTenVT.EditValue = DBNull.Value
    End Sub

    Private Sub rCmbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCmbHangSX.ButtonClick
        cmbHangSX.EditValue = DBNull.Value
    End Sub

    Private Sub btnLocVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLocVT.ItemClick
        Dim sql As String = "SELECT ID, Model,Code, "
        sql &= "(SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu)TenVatTu, "
        sql &= "(SELECT Ten FROM TENNHOM WHERE ID = VATTU.IDTennhom)NhomVT, "
        sql &= "(SELECT Ten FROM TENHANGSANXUAT WHERE ID = VATTU.IDHangSanxuat)HangSX, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT "
        sql &= "FROM VATTU WHERE Model Like N'%" & txtMaVT.EditValue.ToString & "%' "
        If Not cmbNhomVT.EditValue Is DBNull.Value Then
            sql &= " AND IDTennhom = " & cmbNhomVT.EditValue & " "
        End If
        If Not cmbTenVT.EditValue Is DBNull.Value Then
            sql &= " AND IDTenvattu = " & cmbTenVT.EditValue & " "
        End If
        If Not cmbHangSX.EditValue Is DBNull.Value Then
            sql &= " AND IDHangSanxuat = " & cmbHangSX.EditValue & " "
        End If
        sql &= "ORDER BY TenVatTu, Model "
        gdvVT.DataSource = ExecuteSQLDataTable(sql)

    End Sub

    Private Sub gdvDataVT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDataVT.MouseDown
        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvDataVT.CalcHitInfo(e.Location)
        If calTest.InRowCell Then
            PopupMenu1.ShowPopup(gdvVT.PointToScreen(e.Location))
        End If
    End Sub



    Private Sub mnuDuaVaoLaVtPhu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoLaVtPhu.ItemClick
        If gdvDataVT.FocusedRowHandle < 0 Then Exit Sub
        gdvDataVT.CloseEditor()
        gdvData.UpdateCurrentRow()
        Dim idVT As Object = gdvDataVT.GetFocusedRowCellValue("ID")
        For i As Integer = 0 To gdvData.RowCount - 1
            If gdvData.GetRowCellValue(i, "IDVatTu") = idVT Then
                ShowCanhBao("Vật tư này đã tồn tại, vui lòng cập nhật lại số lượng!")
                Exit Sub
            End If
        Next
        gdvData.AddNewRow()
        gdvData.SetFocusedRowCellValue("IDVatTuPhu", idVT)
        gdvData.SetFocusedRowCellValue("TenVatTu", gdvDataVT.GetFocusedRowCellValue("TenVatTu"))
        gdvData.SetFocusedRowCellValue("Model", gdvDataVT.GetFocusedRowCellValue("Model"))
        gdvData.SetFocusedRowCellValue("Code", gdvDataVT.GetFocusedRowCellValue("Code"))
        gdvData.SetFocusedRowCellValue("SoLuong", 1)
        gdvData.SetFocusedRowCellValue("DVT", gdvDataVT.GetFocusedRowCellValue("DVT"))
        gdvData.SetFocusedRowCellValue("HangSX", gdvDataVT.GetFocusedRowCellValue("HangSX"))
        gdvData.SetFocusedRowCellValue("NhomVT", gdvDataVT.GetFocusedRowCellValue("NhomVT"))

    End Sub


End Class