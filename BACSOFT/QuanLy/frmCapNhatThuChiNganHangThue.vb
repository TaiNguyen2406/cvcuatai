Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Imports BACSOFT.HoaDonGTGT
Imports DevExpress.XtraEditors


Public Class frmCapNhatThuChiNganHangThue

    Public idHoaDon As Object = Nothing
    Public LoaiCT As ChungTu.LoaiChungTu
    Public LoaiCT2 As ChungTu.LoaiCT2
    Public idChungTu As Object = DBNull.Value

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim tg As DateTime = GetServerTime()
        txtNgayCT.EditValue = tg

        Dim sql As String = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cmbDoiTuong.Properties.DataSource = tb
        End If
        cmbDoiTuong.EditValue = DBNull.Value

        loadTienTe()
        'Mặc định tiền tệ VNĐ
        cmbTienTe.SelectedItem = cmbTienTe.Properties.Items.Cast(Of LoaiTienTe).Where(Function(x) x.Ten = "VNĐ").FirstOrDefault


        sql = "select ID, MaSo, Ten from taikhoan order by ten"
        Dim dt1 As DataTable = ExecuteSQLDataTable(sql)
        Dim dt2 As DataTable = dt1.Copy

        cmbSoTK.Properties.DataSource = dt1
        cmbSoTaiKhoanDoiUng.Properties.DataSource = dt2

        LoadDsTaiKhoan()

        LoadChiTiet(-1)

    End Sub

    Private Sub frmCapNhatThuChiNganHangThue_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


     
        Dim sql As String = ""

        If TrangThai.isUpdate Or TrangThai.isCopy Then

            sql = "SELECT * FROM CHUNGTU WHERE Id = @Id"
            AddParameter("@Id", idHoaDon)
            Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)

            tbSoPhieu.EditValue = r("SoCT")
            txtNgayCT.EditValue = r("NgayCT")
            cmbTienTe.SelectedItem = cmbTienTe.Properties.Items.Cast(Of LoaiTienTe).Where(Function(x) x.ID = r("TienTe")).FirstOrDefault
            txtTyGia.EditValue = r("TyGia")

            cmbDoiTuong.EditValue = r("IdKH")
            txtTenDoiTuong.EditValue = r("TenKH")
            txtDiaChi.EditValue = r("DiaChi")
            txtMST.EditValue = r("MaSoThue")
            txtNoiDung.EditValue = r("DienGiai")
            txtDaiDien.EditValue = r("NguoiLienHe")


            Dim dt1 As DataTable = CType(cmbSoTK.Properties.DataSource, DataTable)
            Dim dr1 As DataRow = dt1.NewRow
            dr1("MaSo") = r("SoTkNganHang").ToString.Trim
            dr1("Ten") = r("TenTkNganHang").ToString.Trim
            dr1("ID") = DBNull.Value
            dt1.Rows.InsertAt(dr1, dt1.Rows.Count)

            Dim dt2 As DataTable = CType(cmbSoTaiKhoanDoiUng.Properties.DataSource, DataTable)
            Dim dr2 As DataRow = dt2.NewRow
            dr2("MaSo") = r("SoTkNganHangDoiUng").ToString.Trim
            dr2("Ten") = r("TenTkNganHangDoiUng").ToString.Trim
            dr2("ID") = DBNull.Value
            dt2.Rows.InsertAt(dr2, dt2.Rows.Count)

            cmbSoTK.EditValue = r("SoTkNganHang")
            txtTenTaiKhoan.EditValue = r("TenTkNganHang")
            cmbSoTaiKhoanDoiUng.EditValue = r("SoTkNganHangDoiUng")
            txtTenTaiKhoanDoiUng.EditValue = r("TenTkNganHangDoiUng")

            chkGhiSo.Checked = Convert.ToBoolean(r("GhiSo"))

            idChungTu = r("refId")

            LoadChiTiet(idHoaDon)


        End If


    End Sub

    Private Sub LoadDsTaiKhoan()
        Dim sql As String = "SELECT TaiKhoan,TaiKhoanCha,TenGoi FROM TAIKHOANTHUE ORDER BY TaiKhoan "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        Dim tb2 As DataTable = tb.Copy
        tb2.Rows.Clear()
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = tb.Rows(i)("TenGoi")
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), 1, tb2)
            End If
        Next
        rcmbTaiKhoan.DataSource = tb2
    End Sub

    Private Sub deQuy(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then Continue For
            If tb.Rows(i)("TaiKhoanCha") = idCha Then
                Dim strTen As String = ""
                For j As Integer = 0 To level - 1
                    strTen &= "-- "
                Next
                strTen = " " & strTen & tb.Rows(i)("TenGoi")
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = strTen
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), level + 1, tb2)
            End If
        Next
    End Sub

    Private Sub LoadChiTiet(id As Object)
        Dim sql As String = ""
        sql = "SELECT Id,DienGiai,ThanhTien,TaiKhoanNo,TaiKhoanCo FROM CHUNGTUCHITIET WHERE ID_CT = @IdCT AND ButToan = @ButToan "
        AddParameter("@IdCT", id)
        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        gdv.DataSource = dt
    End Sub


    Private Sub mnuThemDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemDong.ItemClick

        gdvData.AddNewRow()
        gdvData.SetFocusedRowCellValue("ThanhTien", 0)

        Select Case LoaiCT
            Case ChungTu.LoaiChungTu.NopTienNganHang
                gdvData.SetFocusedRowCellValue("TaiKhoanNo", "1121")
                gdvData.SetFocusedRowCellValue("TaiKhoanCo", "131")
            Case ChungTu.LoaiChungTu.UyNhiemChi
                gdvData.SetFocusedRowCellValue("TaiKhoanNo", "331")
                gdvData.SetFocusedRowCellValue("TaiKhoanCo", "1121")
        End Select

        gdvData.Focus()
        gdvData.FocusedColumn = gdvData.Columns("DienGiai")
        gdvData.ShowEditor()
        SendKeys.Send("{F4}")

    End Sub

    Private Sub mnuXoaDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaDong.ItemClick

        gdvData.CloseEditor()
        gdvData.UpdateCurrentRow()


        If gdvData.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id") Is DBNull.Value Or _
            gdvData.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id") Is Nothing Then 'Truong hop hang moi chua them gi
            gdvData.DeleteRow(calHitTestHoaDon.RowHandle)
        Else
            If Not ShowCauHoi("Bạn có chắc muốn xóa dòng này không?") Then Exit Sub
            Try

                AddParameter("@Id", gdvData.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id"))
                If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                gdvData.DeleteRow(calHitTestHoaDon.RowHandle)

            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try
        End If

    End Sub

    Private Sub cmbDoiTuong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbDoiTuong.EditValueChanged
        On Error Resume Next
        If cmbDoiTuong.IsPopupOpen Then Exit Sub

        txtTenDoiTuong.EditValue = ""
        txtMST.EditValue = ""
        txtDiaChi.EditValue = ""

        AddParameter("@ID", cmbDoiTuong.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachiTs,IDTakecare,ttcTaikhoan,ttcNoimo FROM KHACHHANG WHERE ID = @ID")


        If tb Is Nothing OrElse tb.Rows.Count = 0 Then
            txtTenDoiTuong.EditValue = ""
            txtDiaChi.EditValue = ""
            txtMST.EditValue = ""
        Else
            txtTenDoiTuong.EditValue = tb.Rows(0)("Ten").ToString
            txtDiaChi.EditValue = tb.Rows(0)("ttcDiachiTs").ToString
            txtMST.EditValue = tb.Rows(0)("ttcMasothue").ToString
            txtNoiDung.Focus()

        End If

    End Sub

    Public Sub loadTienTe()
        Dim sql As String = "SELECT ID,Ten,TyGia FROM tblTienTe ORDER BY ID"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For Each r As DataRow In tb.Rows
                cmbTienTe.Properties.Items.Add(New LoaiTienTe(r("ID"), r("Ten"), r("TyGia")))
            Next
        End If
    End Sub

    Private Sub cmbTienTe_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTienTe.SelectedIndexChanged
        txtTyGia.EditValue = CType(cmbTienTe.SelectedItem, LoaiTienTe).TyGia
    End Sub

    Private Sub cmbSoTK_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbSoTK.EditValueChanged
        Try
            If cmbSoTK.EditValue Is Nothing Then Exit Sub
            Dim edit As LookUpEdit = CType(sender, LookUpEdit)
            Dim dr As DataRowView = edit.GetSelectedDataRow
            txtTenTaiKhoan.EditValue = dr("Ten")
        Catch ex As Exception
            txtTenTaiKhoan.EditValue = DBNull.Value
        End Try
    End Sub

    Private Sub cmbSoTK_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmbSoTK.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbSoTK.EditValue = DBNull.Value
            txtTenTaiKhoan.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub cmbSoTaiKhoanDoiUng_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbSoTaiKhoanDoiUng.EditValueChanged
        Try
            If cmbSoTaiKhoanDoiUng.EditValue Is Nothing Then Exit Sub
            Dim edit As LookUpEdit = CType(sender, LookUpEdit)
            Dim dr As DataRowView = edit.GetSelectedDataRow
            txtTenTaiKhoanDoiUng.EditValue = dr("Ten")
        Catch ex As Exception
            txtTenTaiKhoanDoiUng.EditValue = DBNull.Value
        End Try
    End Sub

    Private Sub cmbSoTaiKhoanDoiUng_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmbSoTaiKhoanDoiUng.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbSoTaiKhoanDoiUng.EditValue = DBNull.Value
            txtTenTaiKhoanDoiUng.EditValue = DBNull.Value
        End If
    End Sub


    Private calHitTestHoaDon As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calHitTestHoaDon = gdvData.CalcHitInfo(e.Location)
        If calHitTestHoaDon.InRowCell Then
            mnuXoaDong.Enabled = True
            'If gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id") Is DBNull.Value Then
            '    mnuXoaDongHoaDon.Enabled = True
            'Else
            '    mnuXoaDongHoaDon.Enabled = False
            'End If
        Else
            mnuXoaDong.Enabled = False
        End If
        PopupMenu1.ShowPopup(gdv.PointToScreen(e.Location))
    End Sub

    Private Sub btnThemMoi_Click(sender As System.Object, e As System.EventArgs) Handles btnThemMoi.Click
        TrangThai.isAddNew = True

        cmbDoiTuong.EditValue = DBNull.Value
      
        cmbSoTK.EditValue = DBNull.Value
        cmbSoTaiKhoanDoiUng.EditValue = DBNull.Value

        txtNoiDung.EditValue = ""

        LoadChiTiet(-1)

        btnThemMoi.Enabled = False

        If LoaiCT = ChungTu.LoaiChungTu.NopTienNganHang Then
            Me.Text = "Thêm mới chứng từ nộp tiền ngân hàng"
        ElseIf LoaiCT = ChungTu.LoaiChungTu.UyNhiemChi Then
            Me.Text = "Thêm mới chứng từ ủy nhiệm chi"
        End If

        cmbDoiTuong.Focus()
        SendKeys.Send("{F4}")
    End Sub



    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click

        Try
            gdvData.CloseEditor()
            gdvData.UpdateCurrentRow()

            If gdvData.RowCount = 0 Then
                ShowCanhBao("Chưa có nội dung bút toán!")
                Exit Sub
            End If


            Dim ThanhTien As Double = 0
            For i As Integer = 0 To gdvData.RowCount - 1
                ThanhTien += gdvData.GetRowCellValue(i, "ThanhTien")
            Next


            AddParameter("@LoaiCT", LoaiCT)
            AddParameter("@NgayCT", txtNgayCT.EditValue)
            AddParameter("@TienTe", CType(cmbTienTe.SelectedItem, LoaiTienTe).ID)
            AddParameter("@TyGia", txtTyGia.EditValue)
            AddParameter("@GhiSo", Convert.ToByte(chkGhiSo.Checked))
            AddParameter("@IdKH", cmbDoiTuong.EditValue)
            AddParameter("@TenKH", txtTenDoiTuong.EditValue)
            AddParameter("@DiaChi", txtDiaChi.EditValue)
            AddParameter("@MaSoThue", txtMST.EditValue)
            AddParameter("@NguoiLienHe", txtDaiDien.EditValue)
            AddParameter("@DienGiai", txtNoiDung.EditValue)
            AddParameter("@SoTkNganHang", cmbSoTK.EditValue)
            AddParameter("@TenTkNganHang", txtTenTaiKhoan.EditValue)
            AddParameter("@SoTkNganHangDoiUng", cmbSoTaiKhoanDoiUng.EditValue)
            AddParameter("@TenTkNganHangDoiUng", txtTenTaiKhoanDoiUng.EditValue)
            AddParameter("@ThanhTien", ThanhTien)

            AddParameter("@refId", idChungTu)

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                idHoaDon = doInsert("CHUNGTU")
                If idHoaDon Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@dk_Id", idHoaDon)
                If doUpdate("CHUNGTU", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If


            'Hàng tiền
            For i As Integer = 0 To gdvData.RowCount - 1
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@DienGiai", gdvData.GetRowCellValue(i, "DienGiai"))
                AddParameter("@ThanhTien", gdvData.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@ThanhTienQD", gdvData.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@TaiKhoanNo", gdvData.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvData.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
                'AddParameter("@IdChiTiet", gdvData.GetRowCellValue(i, "IdChiTiet"))
                If gdvData.GetRowCellValue(i, "Id") Is DBNull.Value Then
                    Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                    If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvData.SetRowCellValue(i, "Id", idHdCT)
                Else
                    AddParameterWhere("@dk_Id", gdvData.GetRowCellValue(i, "Id"))
                    If doUpdate("CHUNGTUCHITIET", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                TrangThai.isUpdate = True
                ShowAlert("Lập mới chứng từ thành công !")
            Else
                ShowAlert("Cập nhật chứng từ thành công !")
            End If

            btnThemMoi.Enabled = True

        Catch ex As Exception

        End Try
    End Sub



    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub

    Private Sub cmbSoTK_Properties_ProcessNewValue(sender As System.Object, e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs) Handles cmbSoTK.Properties.ProcessNewValue
        If e.DisplayValue.ToString.Trim <> "" Then
            e.Handled = True
            Try
                Dim dt As DataTable = CType(cmbSoTK.Properties.DataSource, DataTable)
                Dim r As DataRow = dt.NewRow
                r("Id") = DBNull.Value
                r("MaSo") = e.DisplayValue
                r("Ten") = ""
                dt.Rows.InsertAt(r, 0)
                cmbSoTK.EditValue = e.DisplayValue
                cmbSoTK.Focus()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub cmbSoTaiKhoanDoiUng_ProcessNewValue(sender As System.Object, e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs) Handles cmbSoTaiKhoanDoiUng.ProcessNewValue
        If e.DisplayValue.ToString.Trim <> "" Then
            e.Handled = True
            Try
                Dim dt As DataTable = CType(cmbSoTaiKhoanDoiUng.Properties.DataSource, DataTable)
                Dim r As DataRow = dt.NewRow
                r("Id") = DBNull.Value
                r("MaSo") = e.DisplayValue
                r("Ten") = ""
                dt.Rows.InsertAt(r, 0)
                cmbSoTaiKhoanDoiUng.EditValue = e.DisplayValue
                cmbSoTaiKhoanDoiUng.Focus()
            Catch ex As Exception
            End Try
        End If
    End Sub



End Class