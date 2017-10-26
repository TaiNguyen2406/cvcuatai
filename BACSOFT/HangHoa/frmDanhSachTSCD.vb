
Imports BACSOFT.Db.SqlHelper

Public Class frmDanhSachTSCD

    Private Sub frmDanhSachTSCD_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        LoadDsNhomVT()
        LoadDsTenVT()
        LoadDsHangSX()

        rcmbMaVT.DataSource = ExecuteSQLDataTable("SELECT N'' as Model")

        LocDsCongCuDungCu()

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


    Private Sub btnLocVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLocVT.ItemClick
        LocDsCongCuDungCu()
    End Sub

    Private Sub LocDsCongCuDungCu()

        Dim sql As String = "SELECT ID,TenHoaDon,Model, "
        sql &= "(SELECT Ten FROM TENNHOM WHERE ID = VATTU.IDTennhom)TenNhom, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT "
        sql &= "FROM VATTU WHERE isTaiSanCoDinh = 1 "


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
        sql &= "ORDER BY TenHoaDon "

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        gdv.DataSource = dt

    End Sub


    Private Sub btnLoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLoc.ItemClick
        If gdvData.FocusedColumn Is Nothing Then
            ShowCanhBao("Chọn cột cần lọc trước!")
            Exit Sub
        End If
        gdvData.ShowFilterEditor(gdvData.FocusedColumn)
    End Sub


    Private Sub mnuThemMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemMoi.ItemClick

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        TrangThai.isAddNew = True
        Dim f As New frmUpdateTSCD
        If f.ShowDialog() = DialogResult.OK Then
            LocDsCongCuDungCu()
        End If

    End Sub


    Private Sub mnuSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSua.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        MaTuDien = gdvData.GetFocusedRowCellValue("ID")
        TrangThai.isUpdate = True
        Dim f As New frmUpdateTSCD
        If f.ShowDialog() = DialogResult.OK Then
            LocDsCongCuDungCu()
        End If
        For i As Integer = 0 To gdvData.RowCount - 1
            If gdvData.GetRowCellValue(i, "ID") = MaTuDien Then
                gdvData.FocusedRowHandle = i
            End If
        Next
    End Sub

    Private Sub mnuXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoa.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then
            ShowCanhBao("Bạn không có quyền thực hiện thao tác này!")
            Exit Sub
        End If

        If Not ShowCauHoi("Xóa công cụ dụng cụ bạn đang chọn?") Then Exit Sub
        Dim sql As String = "select count(id) from chungtuchitiet where IdVatTu = " & gdvData.GetFocusedRowCellValue("ID")
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt.Rows(0)(0) > 0 Then
            ShowBaoLoi("Công cụ dụng cụ này đã có chứng từ nên không thể xóa!")
            Exit Sub
        End If
        AddParameterWhere("@ID", gdvData.GetFocusedRowCellValue("ID"))
        If doDelete("VATTU", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        LocDsCongCuDungCu()
    End Sub

    Private Sub gdv_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdv.MouseDown
        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvData.CalcHitInfo(e.Location)
        If calTest.InRowCell Then
            mnuSua.Enabled = True
            mnuXoa.Enabled = True
        Else
            mnuSua.Enabled = False
            mnuXoa.Enabled = False
        End If
        pMnu.ShowPopup(gdv.PointToScreen(e.Location))
    End Sub




End Class
