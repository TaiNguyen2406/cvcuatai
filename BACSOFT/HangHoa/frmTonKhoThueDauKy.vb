Imports BACSOFT.Db.SqlHelper
Public Class frmTonKhoThueDauKy


    Private Sub frmTonKhoThueDauKy_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim tg As DateTime = GetServerTime()
        txtNam.EditValue = tg.Year

        LoadDsNhomVT()
        LoadDsTenVT()
        LoadDsHangSX()

        rcmbMaVT.DataSource = ExecuteSQLDataTable("SELECT N'' as Model")

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            gdvDataVT.Columns("Ton").OptionsColumn.ReadOnly = True
        End If

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

    Private Sub rcmbMaVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcmbMaVT.ButtonClick
        cmbMa.EditValue = DBNull.Value
    End Sub

    Private Sub LoadModelVT(sender As System.Object, e As System.EventArgs) Handles cmbNhomVT.EditValueChanged, cmbTenVT.EditValueChanged, cmbHangSX.EditValueChanged
        If cmbNhomVT.EditValue Is DBNull.Value And cmbTenVT.EditValue Is DBNull.Value And cmbHangSX.EditValue Is DBNull.Value Then
            rcmbMaVT.DataSource = Nothing
            Exit Sub
        End If
        Dim sql As String = "SELECT NULL as Model UNION ALL SELECT Model FROM VATTU WHERE 1=1 "
        If Not cmbNhomVT.EditValue Is DBNull.Value Then sql &= "AND IDTennhom = " & cmbNhomVT.EditValue & " "
        If Not cmbTenVT.EditValue Is DBNull.Value Then sql &= "AND IDTenvattu = " & cmbTenVT.EditValue & " "
        If Not cmbHangSX.EditValue Is DBNull.Value Then sql &= "AND IDHangSanxuat = " & cmbHangSX.EditValue & " "
        sql &= "ORDER BY Model "
        rcmbMaVT.DataSource = ExecuteSQLDataTable(sql)
    End Sub


    Private Sub btnLocVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLocVT.ItemClick

        Dim sql As String = "SELECT ID, Model,Code, "
        sql &= "(SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu)TenVatTu, "
        sql &= "(SELECT Ten FROM TENNHOM WHERE ID = VATTU.IDTennhom)NhomVT, "
        sql &= "(SELECT Ten FROM TENHANGSANXUAT WHERE ID = VATTU.IDHangSanxuat)HangSX, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT, "
        sql &= "ISNULL((SELECT DauKy FROM TONKHOVATTUTHUE WHERE IdVatTu = VATTU.ID AND Nam = " & txtNam.EditValue & "),0)Ton "
        sql &= "FROM VATTU WHERE 1=1 "
        If Not cmbMa.EditValue Is Nothing Then
            sql &= "AND Model Like N'%" & cmbMa.EditValue.ToString & "%' "
        End If


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


    Private Sub rcmbMaVT_ProcessNewValue(sender As System.Object, e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs) Handles rcmbMaVT.ProcessNewValue
        If e.DisplayValue.ToString.Trim <> "" Then
            e.Handled = True
            Dim dt As DataTable = CType(rcmbMaVT.DataSource, DataTable)
            Dim r As DataRow = dt.NewRow
            r("Model") = e.DisplayValue
            dt.Rows.InsertAt(r, 0)
            cmbMa.EditValue = e.DisplayValue
        End If
    End Sub


    Private Sub gdvDataVT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvDataVT.CellValueChanged
        If e.RowHandle >= 0 And e.Column.FieldName = "Ton" Then
            If e.Value = 0 Then
                AddParameter("@IdVatTu", gdvDataVT.GetRowCellValue(e.RowHandle, "ID"))
                AddParameter("@Na", txtNam.EditValue)
                ExecuteSQLNonQuery("DELETE FROM TONKHOVATTUTHUE WHERE IdVatTu = @IdVatTu AND Nam = @Nam")
                ShowAlert("Đã xóa tồn đầu kỳ vật tư " & gdvDataVT.GetRowCellValue(e.RowHandle, "Model"))
            Else
                AddParameter("@IdVatTu", gdvDataVT.GetRowCellValue(e.RowHandle, "ID"))
                AddParameter("@Nam", txtNam.EditValue)
                Dim sql As String = "SELECT ID FROM TONKHOVATTUTHUE WHERE IdVatTu = @IdVatTu AND Nam = @Nam"
                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                If dt.Rows.Count = 0 Then
                    AddParameter("@IdVatTu", gdvDataVT.GetRowCellValue(e.RowHandle, "ID"))
                    AddParameter("@Nam", txtNam.EditValue)
                    AddParameter("@DauKy", gdvDataVT.GetRowCellValue(e.RowHandle, "Ton"))
                    AddParameter("@CuoiKy", 0)
                    doInsert("TONKHOVATTUTHUE")
                    ShowAlert("Đã thêm tồn đầu kỳ vật tư " & gdvDataVT.GetRowCellValue(e.RowHandle, "Model"))
                Else
                    AddParameter("@DauKy", gdvDataVT.GetRowCellValue(e.RowHandle, "Ton"))
                    AddParameter("@CuoiKy", 0)
                    AddParameterWhere("@IdVatTu", gdvDataVT.GetRowCellValue(e.RowHandle, "ID"))
                    AddParameterWhere("@Nam", txtNam.EditValue)
                    doUpdate("TONKHOVATTUTHUE", "IdVatTu = @IdVatTu AND Nam = @Nam")
                    ShowAlert("Đã cập nhật tồn đầu kỳ vật tư " & gdvDataVT.GetRowCellValue(e.RowHandle, "Model"))
                End If
            End If
        End If
    End Sub




End Class
