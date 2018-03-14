Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmDanhsachBaotri

    Public _idVattuPhanmem As String
    Dim view As GridView

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
    Public Sub SelectDulieubaotri(idVattu As Integer)
        ' Truyền tham số mã vật tư phần mềm idMavattu
        Dim sql As String
        sql = "exec sp_BaotriNangCapPhanmem_IT @activity = 'xem', @idvattu = " & idVattu.ToString()

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDulieubaotri.DataSource = dt
    End Sub

    ' Select các bản ghi vật tư phần mềm theo chọn combobox 500
    Public Sub SelectDanhsachphanmem()
        Dim sql As String
        If cbo500.EditValue = "Top 500" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 500, @trangthai = 1"
        ElseIf cbo500.EditValue = "Tất cả" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 1000000, @trangthai = 1"
        Else
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 1000000, @makh = '" & cboKH.EditValue & "', @trangthai = 1"
        End If

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDSPhanmem.DataSource = dt
    End Sub

    Private Sub frmDanhsachBaotri_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowWaiting("Đang tải dữ liệu ...")
        SelectDSKH()
        SelectDanhsachphanmem()
        CloseWaiting()
    End Sub

    Private Sub submnuThem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles submnuThem.ItemClick
        If grdViewDSPhanmem.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn một vật tư trước khi thêm.")
            Exit Sub
        End If

        If grdViewDSPhanmem.GetFocusedRowCellValue("Id") Is Nothing Then
            ShowCanhBao("Bạn phải chọn một vật tư trước khi thêm.")
            Exit Sub
        End If

        Dim formThembaotri As frmThemBaotri
        formThembaotri = New frmThemBaotri
        formThembaotri._flagForm = False ' Báo form làm nhiệm vụ thêm mới
        formThembaotri._idVattuPhanmem = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
        formThembaotri.ShowDialog()
    End Sub

    Private Sub submnuSua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles submnuSua.ItemClick
        If grdViewDulieubaotri.GetFocusedRowCellValue("Id") Is Nothing Then
            Exit Sub
        End If

        If grdViewDulieubaotri.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn một bản ghi trước khi sửa yêu cầu.")
            Exit Sub
        End If

        Dim _dabaotri = grdViewDulieubaotri.GetFocusedRowCellValue("_Dabaotri")
        If _dabaotri = "1" Then
            ShowCanhBao("Bạn không được sửa yêu cầu đã được xử lý.")
            Exit Sub
        End If

        ' Nhận dữ liệu để fill vào form
        If grdViewDulieubaotri.GetFocusedRowCellValue("IdVatTu") IsNot Nothing Then
            Dim recordId = grdViewDulieubaotri.GetFocusedRowCellValue("Id")
            Dim mavattu = grdViewDulieubaotri.GetFocusedRowCellValue("IdVatTu")
            Dim ngaythongbao = grdViewDulieubaotri.GetFocusedRowCellValue("Ngaythongbao")
            Dim nguoithongbao = grdViewDulieubaotri.GetFocusedRowCellValue("Nguoithongbao")
            Dim noidungthongbao = grdViewDulieubaotri.GetFocusedRowCellValue("Noidungthongbao")
            Dim trangthaixuly = grdViewDulieubaotri.GetFocusedRowCellValue("Trangthaixuly")
            Dim files = grdViewDulieubaotri.GetFocusedRowCellValue("Filedinhkem").ToString()

            Dim formThembaotri As frmThemBaotri
            formThembaotri = New frmThemBaotri
            formThembaotri._ngaythongbao = ngaythongbao
            formThembaotri._nguoithongbao = nguoithongbao
            formThembaotri._noidungthongbao = noidungthongbao
            formThembaotri._trangthaixuly = trangthaixuly
            formThembaotri._idVattuPhanmem = mavattu
            formThembaotri._recordId = recordId
            formThembaotri._flagForm = True ' Báo form làm nhiệm vụ cập nhật
            formThembaotri._Files = files
            formThembaotri.ShowDialog()
        End If
    End Sub

    Private Sub submnuNhanxuly_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles submnuNhanxuly.ItemClick
        If grdViewDulieubaotri.GetFocusedRowCellValue("Id") Is Nothing Then
            Exit Sub
        End If

        If grdViewDulieubaotri.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn một bản ghi trước khi nhận xử lý.")
            Exit Sub
        End If

        Dim _dabaotri = grdViewDulieubaotri.GetFocusedRowCellValue("_Dabaotri")
        If _dabaotri = "1" Then
            ShowCanhBao("Bạn không thực hiện được khi yêu cầu đã được xử lý.")
            Exit Sub
        End If

        Dim _danhanxl = grdViewDulieubaotri.GetFocusedRowCellValue("Trangthaixuly").ToString()
        If _danhanxl.Contains("Đã nhận xử lý") Then
            ShowCanhBao("Bạn không thực hiện được khi yêu cầu đã được nhận xử lý.")
            Exit Sub
        End If

        If ShowCauHoi("Bạn chắc chắn nhận xử lý yêu cầu đã chọn?") Then
            Dim recordId = grdViewDulieubaotri.GetFocusedRowCellValue("Id")
            Dim formNhanxulyBaotri As frmNhanxulyBaotri
            formNhanxulyBaotri = New frmNhanxulyBaotri
            formNhanxulyBaotri._idVattuPhanmem = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
            formNhanxulyBaotri._recordId = recordId
            formNhanxulyBaotri.ShowDialog()
        End If
    End Sub

    Private Sub grdViewDSPhanmem_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSPhanmem.FocusedRowChanged
        ' Khi người dùng chọn dòng phần mềm, sẽ hiển thị dữ liệu bảo trì liên quan.
        Dim idVatTu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
        SelectDulieubaotri(idVatTu)
    End Sub

    Private Sub cbo500_EditValueChanged(sender As Object, e As EventArgs) Handles cbo500.EditValueChanged
        KiemtraTuychinh()
    End Sub

    Private Sub btnXem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXem1.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        SelectDanhsachphanmem()
        CloseWaiting()

        Dim idVatTu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
        SelectDulieubaotri(idVatTu)
    End Sub

    Private Sub RepositoryItemLookUpEdit3_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit3.ButtonClick
        If e.Button.Index = 1 Then
            cboKH.EditValue = Nothing
        End If
    End Sub

    Private Sub RepositoryItemComboBox3_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemComboBox3.ButtonClick
        If e.Button.Index = 1 Then
            cboTrangthai.EditValue = Nothing
        End If
    End Sub

    Private Sub grdViewDulieubaotri_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdViewDulieubaotri.RowStyle
        view = CType(sender, GridView)

        ' Tô màu dòng bảo trì đã xử lý xong
        If (e.RowHandle >= 0) Then
            Dim _dabaotri = view.GetRowCellDisplayText(e.RowHandle, view.Columns("_Dabaotri"))

            If _dabaotri = "1" Then
                e.Appearance.BackColor = Color.LightPink
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

    Private Sub RepositoryItemPopupContainerEdit1_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles RepositoryItemPopupContainerEdit1.Closed
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

    Private Sub gdvListFileCT_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            'OpenFileOnLocal(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & e.CellValue, e.CellValue)
            OpenFileOnLocal(UrlITPhanmem & e.CellValue, e.CellValue)
        End If
    End Sub
End Class
