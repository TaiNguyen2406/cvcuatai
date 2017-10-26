Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmNhaCCTheoHangSX

    Private Sub frmNhaCCTheoHangSX_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.Enabled = False
        tbDenNgay.Enabled = False
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        LoadDS()
    End Sub

    Public Sub LoadDS()
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SELECT tblNhaCCTheoHangSX.ID,tblNhaCCTheoHangSX.FileDinhKem,tblNhaCCTheoHangSX.MoTa,TENHANGSANXUAT.Ten AS HangSX,(ISNULL(KHACHHANG.Ten,N'') + ' (' + ISNULL(KHACHHANG.ttcMa,N'') +')') TenNhaCC,NgayNhap,NHANSU.Ten AS PhuTrach"
        sql &= " FROM tblNhaCCTheoHangSX "
        sql &= " INNER JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=tblNhaCCTheoHangSX.IDHangSX"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=tblNhaCCTheoHangSX.IDNCC"
        If cbXem.EditValue = "Theo ngày" Then
            sql &= " AND (Convert(datetime,Convert(nvarchar,tblNhaCCTheoHangSX.NgayNhap,103),103) BETWEEN @TuNgay AND @DenNgay) "
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=tblNhaCCTheoHangSX.IDPhuTrach"
        sql &= " ORDER BY HangSX,NgayNhap"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

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
        If gdvCT.FocusedRowHandle < 0 Then
            mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mXoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mXoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            OpenFileOnLocal(UrlNhaCC & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub rcbFile_Popup(sender As System.Object, e As System.EventArgs) Handles rcbFile.Popup
        gListFileCT.Text = "Danh sách file đính kèm"
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
    End Sub

    Private Sub mThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThem.ItemClick, btThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNNhaCCTheoHangSX
        f.ShowDialog()
    End Sub

    Private Sub mSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSua.ItemClick, btSua.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        TrangThai.isUpdate = True
        objID = gdvCT.GetFocusedRowCellValue("ID")
        Dim f As New frmCNNhaCCTheoHangSX
        f.ShowDialog()

    End Sub

    Private Sub mXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoa.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If ShowCauHoi("Xóa mục được chọn ?") Then
            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If doDelete("tblNhaCCTheoHangSX", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã xóa !")
                LoadDS()
            End If
        End If
        
    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadDS()
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.FieldName = "FileDinhKem" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub cbXem_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbXem.EditValueChanged
        If cbXem.EditValue = "Tất cả" Then
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
        Else
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = tbDenNgay.EditValue
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub
End Class
