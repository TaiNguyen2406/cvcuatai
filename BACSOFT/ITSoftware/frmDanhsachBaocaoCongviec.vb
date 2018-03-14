Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmDanhsachBaocaoCongviec

    ' Method này Kiểm tra tuỳ chỉnh chọn combobox 500
    Sub KiemtraTuychinh()
        If cbo500.EditValue = "Tuỳ chỉnh" Then
            cboKH.Enabled = True
            cboTrangthai.Enabled = True
        Else
            cboKH.Enabled = False
            cboTrangthai.Enabled = False
        End If
    End Sub

    ' Method này select tất cả bản ghi bảng KhachHang
    Sub SelectDSKH()
        Dim tb As DataTable = ExecuteSQLDataTable("Select ttcMa, Ten from KhachHang	")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit3.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Select các bản ghi khảo sát theo mã vật tư
    Public Sub SelectKhaosat(idVatTu As Integer)
        ' Truyền tham số mã vật tư phần mềm idMavattu
        Dim sql As String
        sql = "exec sp_Baocaocongviec_IT @activity = 'xem', @idVattu = " & idVatTu.ToString()
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdKhaosatcongviec.DataSource = dt
    End Sub

    ' Select các bản ghi vật tư phần mềm theo chọn combobox 500
    Public Sub SelectDanhsachphanmem()
        Dim sql As String
        If cbo500.EditValue = "Top 500" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemks', @top = 500"
        ElseIf cbo500.EditValue = "Tất cả" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemks', @top = 1000000"
        Else
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemks', @top = 1000000, @makh = '" & cboKH.EditValue & "', @trangthai = " &
            RepositoryItemComboBox2.Items.IndexOf(cboTrangthai.EditValue)
        End If

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDSphanmem.DataSource = dt
    End Sub

    Private Sub btnThemkhaosat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemkhaosat.ItemClick
        Dim formThemBaocaoCongviec As frmThemBaocaoCongviec
        formThemBaocaoCongviec = New frmThemBaocaoCongviec
        Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
        If idVatTu Is Nothing Then
            MessageBox.Show("Bạn phải chọn vật tư trước khi thực hiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        formThemBaocaoCongviec._idVattuPhanmem = idVatTu
        formThemBaocaoCongviec.ShowDialog()
    End Sub

    Private Sub frmDanhsachBaocaoCongviec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowWaiting("Đang tải dữ liệu ...")
        SelectDanhsachphanmem()
        Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
        SelectKhaosat(idVatTu)
        SelectDSKH()
        CloseWaiting()
    End Sub

    Private Sub btnXem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXem.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        SelectDanhsachphanmem()
        Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
        SelectKhaosat(idVatTu)
        CloseWaiting()
    End Sub

    Private Sub cbo500_EditValueChanged(sender As Object, e As EventArgs) Handles cbo500.EditValueChanged
        KiemtraTuychinh()
    End Sub

    Private Sub RepositoryItemLookUpEdit3_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit3.ButtonClick
        If e.Button.Index = 1 Then
            cboKH.EditValue = Nothing
        End If
    End Sub

    Private Sub RepositoryItemComboBox2_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemComboBox2.ButtonClick
        If e.Button.Index = 1 Then
            cboTrangthai.EditValue = Nothing
        End If
    End Sub

    Private Sub grdViewDSphanmem_FocusedRowChanged_1(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSphanmem.FocusedRowChanged
        ' Khi người dùng chọn dòng phần mềm, sẽ hiển thị dữ liệu khảo sát liên quan.
        Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
        SelectKhaosat(idVatTu)
    End Sub

    Private Sub btnSuakhaosat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSuakhaosat.ItemClick
        If grdViewKhaosatcongviec.RowCount = 0 Then
            MessageBox.Show(Me, "Bạn phải chọn một bản ghi trước khi sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
        If idVatTu Is Nothing Then
            MessageBox.Show("Bạn phải chọn vật tư trước khi thực hiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        On Error Resume Next
        Dim formThemBaocaoCongviec As frmThemBaocaoCongviec
        formThemBaocaoCongviec = New frmThemBaocaoCongviec
        formThemBaocaoCongviec._idVattuPhanmem = idVatTu
        formThemBaocaoCongviec._flagForm = True
        formThemBaocaoCongviec._Ngaybaocao = grdViewKhaosatcongviec.GetFocusedRowCellValue("Ngaybaocao")
        formThemBaocaoCongviec._Nguoibaocao = grdViewKhaosatcongviec.GetFocusedRowCellValue("Nguoibaocao")
        formThemBaocaoCongviec._Ngaybatdau = grdViewKhaosatcongviec.GetFocusedRowCellValue("Ngaybatdau")
        formThemBaocaoCongviec._Ngayketthuc = grdViewKhaosatcongviec.GetFocusedRowCellValue("Ngayketthuc")
        formThemBaocaoCongviec._Diadiemthuchien = grdViewKhaosatcongviec.GetFocusedRowCellValue("Diadiemthuchien")
        formThemBaocaoCongviec._Thuchien = grdViewKhaosatcongviec.GetFocusedRowCellValue("Nhanvienthuchien")
        formThemBaocaoCongviec._Loaicongviec = grdViewKhaosatcongviec.GetFocusedRowCellValue("Tencongviec")
        formThemBaocaoCongviec._Noidung = grdViewKhaosatcongviec.GetFocusedRowCellValue("Noidungcongviec")
        formThemBaocaoCongviec._Files = grdViewKhaosatcongviec.GetFocusedRowCellValue("Filedinhkem")
        formThemBaocaoCongviec._Ghichu = grdViewKhaosatcongviec.GetFocusedRowCellValue("Ghichu")
        formThemBaocaoCongviec._recordId = grdViewKhaosatcongviec.GetFocusedRowCellValue("Id")
        formThemBaocaoCongviec.ShowDialog()
    End Sub

    Private Sub btnDel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDel.ItemClick
        If grdViewKhaosatcongviec.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn một bản ghi trước khi xoá.")
            Exit Sub
        End If

        If ShowCauHoi("Bạn chắc chắn xoá bản ghi này?") Then
                ' Bắt đầu phiên
                BeginTransaction()
                Dim _iD As Object
                _iD = ExecuteSQLNonQuery("Delete from tblBaocaocongviec_IT where Id = " & grdViewKhaosatcongviec.GetFocusedRowCellValue("Id").ToString())

                If _iD Is Nothing Then
                    ' Có lỗi thì Huỷ phiên
                    RollBackTransaction()
                    ' Báo lỗi
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã xoá thành công!")
                    ' Xác nhận phiên
                    ComitTransaction()

                    Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
                    SelectKhaosat(idVatTu)
                End If
            End If
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

    Private Sub RepositoryItemPopupContainerEdit1_Popup(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit1.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub RepositoryItemPopupContainerEdit1_Closed(sender As Object, e As Controls.ClosedEventArgs) Handles RepositoryItemPopupContainerEdit1.Closed
        Dim _File = ""
        For i = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        If _File = "" Then
            _File = Nothing
        End If
        CType(sender, PopupContainerEdit).EditValue = _File
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            'OpenFileOnLocal(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & e.CellValue, e.CellValue)
            OpenFileOnLocal(UrlITPhanmem & e.CellValue, e.CellValue)
        End If
    End Sub
    
    Function KiemtraRole(idVattu As Object) As Boolean
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID from tblThanhvienduan_IT where IdVatTu = '" & idVattu & "' and IdNhansu = " & TaiKhoan & " and IdRole = 1 ")
        If tb.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub mnuGiaoviec_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGiaoviec.ItemClick
        If KiemtraRole(grdViewDSPhanmem.GetFocusedRowCellValue("IdVatTu")) = False Then
            ShowCanhBao("Bạn không có quyền giao việc khảo sát phần mềm. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        If (grdViewDSphanmem.RowCount > 0)
            Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
            If (idVatTu IsNot nothing)
                Dim frmGiaoviecKhaosat As frmGiaoviecKhaosat
                frmGiaoviecKhaosat = New frmGiaoviecKhaosat
                frmGiaoviecKhaosat._idVattu = idVatTu 
                frmGiaoviecKhaosat._tenPhanmem = grdViewDSphanmem.GetFocusedRowCellValue("TenPM").ToString()
                frmGiaoviecKhaosat.ShowDialog()
            End If
        End If
    End Sub

    Private Sub btnGV_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGV.ItemClick
        If KiemtraRole(grdViewDSPhanmem.GetFocusedRowCellValue("IdVatTu")) = False Then
            ShowCanhBao("Bạn không có quyền giao việc khảo sát phần mềm. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        If (grdViewDSphanmem.RowCount > 0)
            Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
            If (idVatTu IsNot nothing)
                Dim frmGiaoviecKhaosat As frmGiaoviecKhaosat
                frmGiaoviecKhaosat = New frmGiaoviecKhaosat
                frmGiaoviecKhaosat._idVattu = idVatTu 
                frmGiaoviecKhaosat._tenPhanmem = grdViewDSphanmem.GetFocusedRowCellValue("TenPM").ToString()
                frmGiaoviecKhaosat.ShowDialog()
            End If
        End If
    End Sub
End Class
