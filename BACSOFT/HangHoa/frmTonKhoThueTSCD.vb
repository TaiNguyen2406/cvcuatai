Imports BACSOFT.Db.SqlHelper
Public Class frmTonKhoThueTSCD


    Private Sub frmTonKhoThueTSCD_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim tg As DateTime = GetServerTime()
        txtNam.EditValue = tg.Year

        LoadDsPhongBan()

        cmbTenNhom.EditValue = DBNull.Value

        'LoadDuLieu()

    End Sub

    Public Sub LoadDsPhongBan()
        Dim sql As String = "SELECT Id, Ten FROM TenNhom order by Ten"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For Each r As DataRow In tb.Rows
                rCmbPhongBan.Items.Add(New ObjectItemCmb(r("Id"), r("Ten")))
            Next
        End If
    End Sub


    Private Sub LoadDuLieu()

        Dim sql As String = "SELECT * FROM ("
        sql &= "SELECT ID, IdVatTu,IDkhachhang, SoLuong,NgayMua,NgayGhiTang,NgaySuDung,NgayTinhKhauHao,NguyenGia,SoNamSuDung,TyLeKhauHaoThang, "
        sql &= "GiaTriKhauHaoThang,GiaTriConLai,KhauHaoLuyKe,GhiChu, "
        sql &= "(SELECT ttcMa FROM KHACHHANG WHERE ID=tonkhothuetscd.IDkhachhang )DoiTuong, "
        sql &= "(SELECT TenHoaDon FROM VATTU WHERE ID=tonkhothuetscd.IdVatTu )TenHoaDon, "
        sql &= "(SELECT Ten FROM TENNHOM WHERE ID = (SELECT IDTenNhom FROM VATTU WHERE ID=tonkhothuetscd.IdVatTu))TenNhom, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = (SELECT IDDonvitinh FROM VATTU WHERE ID=tonkhothuetscd.IdVatTu))DVT "
        sql &= "FROM tonkhothuetscd "
        If Not cmbTenNhom.EditValue Is DBNull.Value Then
            sql &= " WHERE IdVatTu IN (SELECT ID FROM VATTU WHERE IDTennhom = " & CType(cmbTenNhom.EditValue, ObjectItemCmb).GiaTri & " ) "
        End If

        sql &= ")tbl order by TenHoaDon "

        Dim dt As DataTable = Nothing

        dt = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If


        gdvVT.DataSource = dt

    End Sub

    Private Sub btnLocVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLocVT.ItemClick

        LoadDuLieu()

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

        If e.RowHandle < 0 Then Exit Sub


        If e.Column.FieldName = "Ton" Or e.Column.FieldName = "GiaTri" Then
            If gdvDataVT.GetRowCellValue(e.RowHandle, "Ton") = 0 And gdvDataVT.GetRowCellValue(e.RowHandle, "GiaTri") = 0 Then
                AddParameter("@IdVatTu", gdvDataVT.GetRowCellValue(e.RowHandle, "ID"))
                AddParameter("@Nam", txtNam.EditValue)
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
                    AddParameter("@GiaTri", gdvDataVT.GetRowCellValue(e.RowHandle, "GiaTri"))
                    AddParameter("@CuoiKy", 0)
                    doInsert("TONKHOVATTUTHUE")
                    ShowAlert("Đã thêm tồn đầu kỳ vật tư " & gdvDataVT.GetRowCellValue(e.RowHandle, "Model"))
                Else
                    AddParameter("@DauKy", gdvDataVT.GetRowCellValue(e.RowHandle, "Ton"))
                    AddParameter("@GiaTri", gdvDataVT.GetRowCellValue(e.RowHandle, "GiaTri"))
                    AddParameter("@CuoiKy", 0)
                    AddParameterWhere("@IdVatTu", gdvDataVT.GetRowCellValue(e.RowHandle, "ID"))
                    AddParameterWhere("@Nam", txtNam.EditValue)
                    doUpdate("TONKHOVATTUTHUE", "IdVatTu = @IdVatTu AND Nam = @Nam")
                    ShowAlert("Đã cập nhật tồn đầu kỳ vật tư " & gdvDataVT.GetRowCellValue(e.RowHandle, "Model"))
                End If
            End If
        End If

    End Sub


    Private Sub gdvDataVT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvDataVT.RowCellStyle


        If e.Column.FieldName = "Ton" And gdvDataVT.GetRowCellValue(e.RowHandle, "Ton") <> 0 Then
            e.Appearance.ForeColor = Color.DeepPink
        End If
        If e.Column.FieldName = "GiaTri" And gdvDataVT.GetRowCellValue(e.RowHandle, "GiaTri") <> 0 Then
            e.Appearance.ForeColor = Color.DarkRed
        End If



    End Sub

    Private Sub btnLoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLoc.ItemClick
        If gdvDataVT.FocusedColumn Is Nothing Then
            ShowCanhBao("Chọn cột cần lọc trước!")
            Exit Sub
        End If
        gdvDataVT.ShowFilterEditor(gdvDataVT.FocusedColumn)
    End Sub

    Private Sub gdvDataVT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvDataVT.CustomDrawCell
        If IsNumeric(e.CellValue) AndAlso e.CellValue = 0 Then e.DisplayText = ""
    End Sub

    Private Sub RepositoryItemCalcEdit1_MouseWheel(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles RepositoryItemCalcEdit1.MouseWheel
        'If e.Delta <> 0 Then
        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = True
        'End If

    End Sub


    Private Sub btnXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "TonKhoDauKy" & txtNam.EditValue & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvDataVT, False)

                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub



    Private Sub mnuThemMoiCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemMoiCT.ItemClick

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        TrangThai.isAddNew = True
        Dim f As New frmUpdateTonKhoThueTSCD
        f.Text = "Thêm mới tồn kho thuế TSCĐ"
        f.Nam = txtNam.EditValue
        f.ShowDialog()
        LoadDuLieu()

    End Sub


    Private Sub mnuCapNhatCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuCapNhatCT.ItemClick


        If gdvDataVT.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        TrangThai.isUpdate = True
        Dim f As New frmUpdateTonKhoThueTSCD
        f.Nam = txtNam.EditValue
        f.idTonKho = gdvDataVT.GetFocusedRowCellValue("ID")
        f.Text = "Cập nhật tồn đầu kỳ kho thuế TSCĐ"
        f.ShowDialog()
        LoadDuLieu()


    End Sub


    Private Sub mnuXoaBoCT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaBoCT.ItemClick
        If gdvDataVT.FocusedRowHandle < 0 Then Exit Sub



        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If
        If Not ShowCauHoi("Xóa dữ liệu bạn vừa chọn hay không?") Then Exit Sub


        Try
            BeginTransaction()
            'Xóa chứng từ
            Dim sql As String = "DELETE FROM TONKHOTHUETSCD WHERE ID = " & gdvDataVT.GetFocusedRowCellValue("ID")
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ComitTransaction()
            LoadDuLieu()
            ShowAlert("Đã xóa dữ liệu thành công !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        End Try


    End Sub

    Private Sub gdvDataVT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDataVT.MouseDown
        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvDataVT.CalcHitInfo(e.Location)
        mnuCapNhatCT.Enabled = calTest.InRowCell
        mnuXoaBoCT.Enabled = calTest.InRowCell
        PopupMenu1.ShowPopup(gdvVT.PointToScreen(e.Location))
    End Sub

    Private Sub rCmbPhongBan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCmbPhongBan.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbTenNhom.EditValue = DBNull.Value
        End If
    End Sub

End Class
