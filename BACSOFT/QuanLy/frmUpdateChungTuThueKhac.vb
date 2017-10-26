Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Imports BACSOFT.HoaDonGTGT


Public Class frmUpdateChungTuThueKhac


    Public Sub New()


        ' This call is required by the designer.
        InitializeComponent()

        loadKhachHang()
        txtNgayHoaDon.EditValue = GetServerTime()

       

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            LoadChiTietHangTien(-1)
        Else

        End If


        LoadDsTaiKhoan()

    End Sub



    Public isDangXuatKho As Boolean = False
    Public idHoaDon As Object

    Public LoaiCT2 As ChungTu.LoaiCT2

    Private Sub frmUpdateHoaDon_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


      

        If TrangThai.isUpdate Or TrangThai.isCopy Then

            Dim sql As String = "SELECT * FROM CHUNGTU WHERE Id = @Id"
            AddParameter("@Id", idHoaDon)
            Dim r As DataRow = ExecuteSQLDataTable(sql).Rows(0)

            cmbDoiTuong.EditValue = r("IdKH")
            txtTenDoiTuong.EditValue = r("TenKH")
            txtDiaChi.EditValue = r("DiaChi")
            txtDienGiaiChung.EditValue = r("DienGiai")
            txtNgayHoaDon.EditValue = r("NgayCT")
            LoadChiTietHangTien(idHoaDon)

            If TrangThai.isUpdate Then
                If Convert.ToBoolean(r("GhiSo")) Then
                    chkGhiSo.Checked = True
                Else
                    chkGhiSo.Checked = False
                End If
            End If

            LoadChiTietHangTien(idHoaDon)

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


    Public Sub loadKhachHang()
        Dim sql As String = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cmbDoiTuong.Properties.DataSource = tb
        End If
    End Sub



    Public Sub LoadChiTietHangTien(IdCT As Long)
        Dim sql As String = "SELECT convert(int,0)STT, Id,IdChiTiet,DienGiai,ThanhTien,GhiChu,TaiKhoanNo,TaiKhoanCo "
        sql &= "FROM CHUNGTUCHITIET "
        sql &= "WHERE Id_CT = @Id_CT And ButToan = @ButToan "
        AddParameter("@Id_CT", IdCT)
        AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("STT") = i + 1
            Next
            If TrangThai.isCopy Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("Id") = DBNull.Value
                    dt.Rows(i)("ref") = DBNull.Value
                    dt.Rows(i)("IdVatTu") = DBNull.Value
                    dt.Rows(i)("IdChiTiet") = DBNull.Value
                Next
            End If
        End If
        gdvHangTien.DataSource = dt
    End Sub


   


    Private Sub cmbDoiTuong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbDoiTuong.EditValueChanged

        On Error Resume Next
        If cmbDoiTuong.IsPopupOpen Then Exit Sub

        AddParameter("@ID", cmbDoiTuong.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachiTs FROM KHACHHANG WHERE ID = @ID")

        If tb Is Nothing OrElse tb.Rows.Count = 0 Then
            txtTenDoiTuong.EditValue = ""
            txtDiaChi.EditValue = ""
        Else
            txtTenDoiTuong.EditValue = tb.Rows(0)("Ten").ToString
            txtDiaChi.EditValue = tb.Rows(0)("ttcDiachiTs").ToString
            'txtNguoiLienHe.Focus()
        End If


    End Sub
   

    Private calHitTestHoaDon As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
    Private Sub gdvHangTienCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvHangTienCT.MouseDown
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calHitTestHoaDon = gdvHangTienCT.CalcHitInfo(e.Location)
        If calHitTestHoaDon.InRowCell Then
            mnuXoaDongHoaDon.Enabled = True
        Else
            mnuXoaDongHoaDon.Enabled = False
        End If
        pMenuHoaDon.ShowPopup(gdvHangTien.PointToScreen(e.Location))
    End Sub

    Private Sub mnuXoaDongHoaDon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaDongHoaDon.ItemClick

        gdvHangTienCT.CloseEditor()
        gdvHangTienCT.UpdateCurrentRow()


        If gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id") Is Nothing Then 'Truong hop hang moi chua them gi
            gdvHangTienCT.DeleteRow(calHitTestHoaDon.RowHandle)
        Else
            If Not ShowCauHoi("Bạn có chắc muốn xóa dòng này không?") Then Exit Sub
            Try
                BeginTransaction()
                'gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id")

                AddParameter("@Id", gdvHangTienCT.GetRowCellValue(calHitTestHoaDon.RowHandle, "Id"))
                If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                gdvHangTienCT.DeleteRow(calHitTestHoaDon.RowHandle)

                ComitTransaction()
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(ex.Message)
            End Try
        End If


    End Sub


    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub


    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click


        Try

            btGhiLai.Enabled = False

            gdvHangTienCT.CloseEditor()
            gdvHangTienCT.UpdateCurrentRow()

            BeginTransaction()

            AddParameter("@NgayCT", txtNgayHoaDon.EditValue)
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.TongHop)
            AddParameter("@IdKH", cmbDoiTuong.EditValue)
            AddParameter("@TenKH", txtTenDoiTuong.EditValue)
            AddParameter("@DiaChi", txtDiaChi.EditValue)
            AddParameter("@DienGiai", txtDienGiaiChung.EditValue)
            Dim tongthanhtien As Double = 0
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                tongthanhtien += gdvHangTienCT.GetRowCellValue(i, "ThanhTien")
            Next
            AddParameter("@ThanhTien", tongthanhtien)
            AddParameter("@GhiSo", chkGhiSo.Checked)
            AddParameter("@NguoiLap", TaiKhoan)

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                idHoaDon = doInsert("CHUNGTU")
                If idHoaDon Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@dk_Id", idHoaDon)
                If doUpdate("CHUNGTU", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            'Hàng tiền
            For i As Integer = 0 To gdvHangTienCT.RowCount - 1
                AddParameter("@Id_CT", idHoaDon)
                AddParameter("@DienGiai", gdvHangTienCT.GetRowCellValue(i, "DienGiai"))
                AddParameter("@ThanhTien", gdvHangTienCT.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@TaiKhoanNo", gdvHangTienCT.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvHangTienCT.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)
                If gdvHangTienCT.GetRowCellValue(i, "Id") Is DBNull.Value Then
                    Dim idHdCT As Object = doInsert("CHUNGTUCHITIET")
                    If idHdCT Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvHangTienCT.SetRowCellValue(i, "Id", idHdCT)
                Else
                    AddParameterWhere("@dk_Id", gdvHangTienCT.GetRowCellValue(i, "Id"))
                    If doUpdate("CHUNGTUCHITIET", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            ComitTransaction()

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                TrangThai.isUpdate = True
                ShowAlert("Nhập chứng từ thành công !")
            Else
                ShowAlert("Cập nhật chứng từ thành công !")
            End If

            btnThemMoi.Enabled = True
            'btInHoaDon.Enabled = True
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            RollBackTransaction()
        Finally
            btGhiLai.Enabled = True
        End Try


    End Sub


    Private Sub mnuThemDongHD_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuThemDongDV.ItemClick

        gdvHangTienCT.AddNewRow()
        gdvHangTienCT.SetFocusedRowCellValue("SoLuong", 0)
        gdvHangTienCT.SetFocusedRowCellValue("DonGia", 0)
        gdvHangTienCT.SetFocusedRowCellValue("ThanhTien", 0)
        gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanNo", "")
        gdvHangTienCT.SetFocusedRowCellValue("TaiKhoanCo", "")

        gdvHangTienCT.Focus()
        gdvHangTienCT.FocusedColumn = gdvHangTienCT.Columns("DienGiai")
        gdvHangTienCT.ShowEditor()
        SendKeys.Send("{F4}")

    End Sub

    Private Sub txtTenDoiTuong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtTenDoiTuong.EditValueChanged

    End Sub


    Private Sub gdvHangTienCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvHangTienCT.InitNewRow
        gdvHangTienCT.SetRowCellValue(e.RowHandle, "STT", gdvHangTienCT.RowCount)
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean

        Select Case keyData
            Case Keys.F2
                If mnuThemDongDV.Enabled And mnuThemDongDV.Visibility = DevExpress.XtraBars.BarItemVisibility.Always Then
                    mnuThemDongHD_ItemClick(mnuThemDongHD, New DevExpress.XtraBars.ItemClickEventArgs(mnuThemDongDV, mnuThemDongDV.Links(0)))
                End If
            Case Keys.Control Or Keys.S
                btGhiLai_Click(btGhiLai, New System.EventArgs())
            Case Keys.Control Or Keys.N
                If btnThemMoi.Enabled Then
                    btnThemMoi_Click(btnThemMoi, New System.EventArgs())
                End If
        End Select

    End Function

    Private Sub cmbDoiTuong_Enter(sender As System.Object, e As System.EventArgs) Handles cmbDoiTuong.Enter
        If Not isDangXuatKho And TrangThai.isAddNew Then
            SendKeys.Send("{F4}")
        End If
    End Sub

   
    Private Sub btnThemMoi_Click(sender As System.Object, e As System.EventArgs) Handles btnThemMoi.Click
        TrangThai.isAddNew = True
        cmbDoiTuong.EditValue = DBNull.Value
        txtDienGiaiChung.Text = ""
        chkGhiSo.Checked = False
        If TrangThai.isAddNew Or TrangThai.isCopy Then
            LoadChiTietHangTien(-1)
        End If
        btnThemMoi.Enabled = False
        cmbDoiTuong.Focus()
        SendKeys.Send("{F4}")
    End Sub

    Private Sub chkGhiSo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGhiSo.CheckedChanged
        If chkGhiSo.Checked Then
            chkGhiSo.ForeColor = Color.Green
        Else
            chkGhiSo.ForeColor = Color.Black
        End If
        chkGhiSo.Refresh()
    End Sub



   


End Class