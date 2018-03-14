Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmPhantichChucnang

    ' Biến này lưu các yêu cầu bảo trì
    Dim _dataBaotri As New ArrayList

    ' Biến này lưu tham chiếu đến GridControl grdViewDSPhanmem
    Dim view As GridView
    Dim idVattuPhanmem As String

    ' Method này kiểm tra xem có phải role quản trị không
    Function KiemtraRole(idVattu As Object) As Boolean
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID from tblThanhvienduan_IT where IdVatTu = '" & idVattu & "' and IdNhansu = " & TaiKhoan & " and IdRole = 1 ")
        If tb.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Kiểm tra người dùng chọn giá trị tuỳ chỉnh trong combobox 500
    Sub KiemtraTuychinh()
        If cbo500.EditValue = "Tuỳ chỉnh" Then
            cboKH.Enabled = True
            cboTrangthai.Enabled = True
        Else
            cboKH.Enabled = False
            cboTrangthai.Enabled = False
        End If
    End Sub

    ' Fill danh sách khách hàng vào combobox chọn KH
    Sub SelectDSKH()
        Dim tb As DataTable = ExecuteSQLDataTable("Select ttcMa, Ten from KhachHang	")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit3.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Kiểm tra xem có bản ghi bảo trì hay không để tô màu dòng vật tư.
    Sub KiemtraBaotri()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT distinct IdVatTu FROM tblDulieubaotri_IT where _Dabaotri = 0")
        _dataBaotri.Clear()
        If tb.Rows.Count > 0 Then
            BarEditItem2.Visibility = BarItemVisibility.Always
            Bar3.Visible = True
            RepositoryItemTextEdit2.NullText = "Có " & tb.Rows.Count & " phần mềm đang có yêu cầu bảo trì."
            For Each item As DataRow In tb.Rows
                _dataBaotri.Add(Integer.Parse(item(0).ToString()))
            Next
        Else
            BarEditItem2.Visibility = BarItemVisibility.Never
            Bar3.Visible = False
            RepositoryItemTextEdit2.NullText = ""
        End If
    End Sub

    ' Select danh sách vật tư phần mềm theo chọn combobox 500
    Public Sub SelectDanhsachphanmem()
        Dim sql As String
        'sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 500, @trangthai = 1"
        sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 500 "

        If cbo500.EditValue = "Top 500" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 500 "
        ElseIf cbo500.EditValue = "Tất cả" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 1000000 "
        ElseIf cbo500.EditValue = "Tuỳ chỉnh" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 1000000, @makh = '" & cboKH.EditValue & "' "
        End If

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDSPhanmem.DataSource = dt
    End Sub

    Public Sub SelectDanhmucphanhe(mavattu As Integer)
        Dim sql As String
        sql = "exec [sp_Phanhe_IT] @activity = 'xem', @idvattu = " & mavattu.ToString()
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDanhmucphanhe.DataSource = dt
    End Sub

    ' Select danh mục chức năng
    Public Sub SelectDanhmucchucnang(maphanhe As Integer)
        Dim sql As String
        sql = "exec [sp_DSChucnang_IT] @activity = 'xem', @IdPhanhe = " & maphanhe.ToString()
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDanhmucchucnang.DataSource = dt
    End Sub

    Private Sub mnuPopupThemPhanhe_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPopupThemPhanhe.ItemClick
        If grdViewDSPhanmem.GetFocusedRowCellValue("Id") Is Nothing Then
            Exit Sub
        End If

        If KiemtraRole(grdViewDSPhanmem.GetFocusedRowCellValue("Id")) = False Then
            ShowCanhBao("Bạn không có quyền thêm phân hệ. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        If grdViewDSPhanmem.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn vật tư trước khi thêm.")
            Exit Sub
        End If

        Dim idVatTu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
        Dim formThemphanhe As frmThemPhanhe
        formThemphanhe = New frmThemPhanhe
        formThemphanhe._idVattuPhanmem = idVatTu
        formThemphanhe.ShowDialog()
    End Sub

    Private Sub frmPhantichChucnang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowWaiting("Đang tải dữ liệu ...")
        KiemtraBaotri()
        SelectDanhsachphanmem()
        SelectDSKH()
        CloseWaiting()
    End Sub

    Private Sub grdViewDanhmucphanhe_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDanhmucphanhe.FocusedRowChanged
        Dim idPhanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Id")
        SelectDanhmucchucnang(idPhanhe)
    End Sub

    Private Sub mnuPopupThemchucnang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPopupThemchucnang.ItemClick
        Dim idPhanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Id")
        If grdViewDanhmucphanhe.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn phân hệ trước khi thêm chức năng.")
            Exit Sub
        End If

        If KiemtraRole(grdViewDSPhanmem.GetFocusedRowCellValue("Id")) = False Then
            ShowCanhBao("Bạn không có quyền thêm chức năng. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        Dim idVattu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
        Dim formThemChucnang As frmThemChucnang
        formThemChucnang = New frmThemChucnang
        formThemChucnang._idPhanhe = idPhanhe
        formThemChucnang._idVattuPhanmem = idVattu
        formThemChucnang.ShowDialog()
    End Sub

    Private Sub grdViewDSPhanmem_FocusedRowChanged_1(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSPhanmem.FocusedRowChanged
        Dim idVatTu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
        SelectDanhmucphanhe(idVatTu)

        Dim idPhanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Id")
        SelectDanhmucchucnang(idPhanhe)
    End Sub

    Private Sub mnuPopupSuaPhanhe_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPopupSuaPhanhe.ItemClick
        If grdViewDanhmucphanhe.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn phân hệ trước khi sửa.")
            Exit Sub
        End If

        If KiemtraRole(grdViewDSPhanmem.GetFocusedRowCellValue("Id")) = False Then
            ShowCanhBao("Bạn không có quyền sửa phân hệ. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        Dim idVatTu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
        Dim formThemphanhe As frmThemPhanhe
        formThemphanhe = New frmThemPhanhe
        formThemphanhe._idVattuPhanmem = idVatTu
        formThemphanhe.Text = "Sửa phân hệ"
        formThemphanhe._flagForm = True

        On Error Resume Next
        formThemphanhe._recordId = grdViewDanhmucphanhe.GetFocusedRowCellValue("Id")
        formThemphanhe._Ngaytao = grdViewDanhmucphanhe.GetFocusedRowCellValue("Ngaylap")
        formThemphanhe._Nguoitao = grdViewDanhmucphanhe.GetFocusedRowCellValue("Tennguoilap")
        formThemphanhe._Tenphanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Tenphanhe")
        formThemphanhe._Motaphanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Motaphanhe")
        formThemphanhe._Files = grdViewDanhmucphanhe.GetFocusedRowCellValue("Filedinhkem")
        formThemphanhe._Ghichu = grdViewDanhmucphanhe.GetFocusedRowCellValue("Ghichu")
        formThemphanhe._isBaotri = grdViewDanhmucphanhe.GetFocusedRowCellValue("_Nangcapbaotri")
        formThemphanhe._isThemoiphanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("_Themphanhe")
        formThemphanhe._isSuaphanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("_Suaphanhe")

        formThemphanhe.ShowDialog()
    End Sub

    Private Sub mnuPopupSuachucnang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPopupSuachucnang.ItemClick
        Dim idPhanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Id")
        Dim idVattu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")

        If grdViewDanhmuchucnang.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn phân hệ/chức năng trước khi sửa chức năng.")
            Exit Sub
        End If

        If KiemtraRole(grdViewDSPhanmem.GetFocusedRowCellValue("Id")) = False Then
            ShowCanhBao("Bạn không có quyền sửa chức năng. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        If grdViewDanhmuchucnang.GetFocusedRowCellValue("Trangthaichucnang") <> "Chờ xử lý" Then
            ShowCanhBao("Bạn chỉ sửa được chức năng Chờ xử lý.")
            Exit Sub
        End If

        Dim idChucnang = grdViewDanhmuchucnang.GetFocusedRowCellValue("Id")
        Dim formThemchucnang As frmThemChucnang
        formThemchucnang = New frmThemChucnang
        formThemchucnang.Text = "Sửa chức năng"
        formThemchucnang._idPhanhe = idPhanhe
        formThemchucnang._idVattuPhanmem = idVattu
        formThemchucnang._flagForm = True

        On Error Resume Next
        formThemchucnang._recordId = idChucnang
        formThemchucnang._Ngaytao = grdViewDanhmuchucnang.GetFocusedRowCellValue("Ngaylap")
        formThemchucnang._Nguoitao = grdViewDanhmuchucnang.GetFocusedRowCellValue("Nguoilap")
        formThemchucnang._Files = grdViewDanhmuchucnang.GetFocusedRowCellValue("Filedinhkem")
        formThemchucnang._Ghichu = grdViewDanhmuchucnang.GetFocusedRowCellValue("Ghichu")
        formThemchucnang._Tenchucnang = grdViewDanhmuchucnang.GetFocusedRowCellValue("Tenchucnang")
        formThemchucnang._Motachucnang = grdViewDanhmuchucnang.GetFocusedRowCellValue("Motachucnang")
        formThemchucnang._Trangthai = grdViewDanhmuchucnang.GetFocusedRowCellValue("Trangthaichucnang")
        formThemchucnang._idPhanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Id")
        formThemchucnang._isBaotri = grdViewDanhmuchucnang.GetFocusedRowCellValue("_Nangcapbaotri")
        formThemchucnang._isThemoichucnang = grdViewDanhmuchucnang.GetFocusedRowCellValue("_Themchucnang")
        formThemchucnang._isSuachucnang = grdViewDanhmuchucnang.GetFocusedRowCellValue("_Suachucnag")

        formThemchucnang.ShowDialog()
    End Sub

    Private Sub grdViewDSPhanmem_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdViewDSPhanmem.RowStyle
        view = CType(sender, GridView)

        If (e.RowHandle >= 0) Then
            idVattuPhanmem = view.GetRowCellDisplayText(e.RowHandle, view.Columns("Id"))
            If _dataBaotri.Contains(Integer.Parse(idVattuPhanmem.ToString())) Then
                e.Appearance.BackColor = Color.Gold
            End If
        End If
    End Sub

    Private Sub cbo500_EditValueChanged(sender As Object, e As EventArgs) Handles cbo500.EditValueChanged
        KiemtraTuychinh()
    End Sub

    Private Sub btnXem_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnXem.ItemClick
        KiemtraBaotri()
        SelectDanhsachphanmem()
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

    Private Sub btnXembaotri_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnXembaotri.ItemClick
        If grdViewDanhmuchucnang.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn phân hệ/chức năng trước khi xem thông tin.")
            Exit Sub
        End If

        If Not grdViewDanhmuchucnang.GetFocusedRowCellValue("Tinhtrang") Is Nothing Then
            Dim trangthaichucnang = grdViewDanhmuchucnang.GetFocusedRowCellValue("Tinhtrang").ToString()
            Dim idPhanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Id")
            If grdViewDanhmuchucnang.GetFocusedRowCellValue("IdChucnangcu") IsNot Nothing And trangthaichucnang <> "" Then
                Dim formSuachucnangdaco As frmSuachucnangdaco
                formSuachucnangdaco = New frmSuachucnangdaco
                formSuachucnangdaco.Text = "Xem thông tin bảo trì"
                formSuachucnangdaco._idChucnangSua = grdViewDanhmuchucnang.GetFocusedRowCellValue("Id")
                formSuachucnangdaco._maPhanhe = idPhanhe
                formSuachucnangdaco._idVattuPhanmem = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
                formSuachucnangdaco.ShowDialog(Me)
            End If
        End If
    End Sub

    Private Sub btnXemYCBaotri_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnXemYCBaotri.ItemClick
        Dim idvattu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")

        Dim formChonyeucaubaotri As frmChonyeucaubaotri
        formChonyeucaubaotri = New frmChonyeucaubaotri
        formChonyeucaubaotri._idVattuPhanmem = idvattu
        formChonyeucaubaotri._xemthongtin = True
        formChonyeucaubaotri.ShowDialog(Me)
    End Sub

    Private Sub btnXoaphanhe_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnXoaphanhe.ItemClick
        If grdViewDanhmucphanhe.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn một bản ghi trước khi xoá.")
            Exit Sub
        End If

        If KiemtraRole(grdViewDSPhanmem.GetFocusedRowCellValue("Id")) = False Then
            ShowCanhBao("Bạn không có quyền xoá phân hệ. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        If ShowCauHoi("Bạn chắc chắn xoá bản ghi này?") Then
            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD As Object
            _iD = ExecuteSQLNonQuery("Delete from tblPhanhe_IT where Id = " & grdViewDanhmucphanhe.GetFocusedRowCellValue("Id").ToString())

            If _iD Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã xoá thành công!")
                ' Xác nhận phiên
                ComitTransaction()

                Dim idVatTu = grdViewDSPhanmem.GetFocusedRowCellValue("Id")
                SelectDanhmucphanhe(idVatTu)
            End If
        End If
    End Sub

    Private Sub btnXoacn_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btnXoacn.ItemClick
        If grdViewDanhmuchucnang.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn một bản ghi trước khi xoá.")
            Exit Sub
        End If

        If KiemtraRole(grdViewDSPhanmem.GetFocusedRowCellValue("Id")) = False Then
            ShowCanhBao("Bạn không có quyền xoá chức năng. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        If ShowCauHoi("Bạn chắc chắn xoá bản ghi này?") Then
            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD As Object
            _iD = ExecuteSQLNonQuery("Delete from tblDSChucnang_IT where Id = " & grdViewDanhmuchucnang.GetFocusedRowCellValue("Id").ToString())

            If _iD Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã xoá thành công!")
                ' Xác nhận phiên
                ComitTransaction()

                Dim idPhanhe = grdViewDanhmucphanhe.GetFocusedRowCellValue("Id")
                SelectDanhmucchucnang(idPhanhe)
            End If
        End If
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

    Private Sub gdvListFileCT_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            'OpenFileOnLocal(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & e.CellValue, e.CellValue)
            OpenFileOnLocal(UrlITPhanmem & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub LoadDSFileDinhKem1(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile1.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvListFileCT1.AddNewRow()
                gdvListFileCT1.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvListFileCT1.CloseEditor()
        gdvListFileCT1.UpdateCurrentRow()
    End Sub

    Private Sub RepositoryItemPopupContainerEdit2_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles RepositoryItemPopupContainerEdit2.Closed
        Dim _File = ""
        For i = 0 To gdvListFileCT1.RowCount - 1
            _File &= gdvListFileCT1.GetRowCellValue(i, "File")
            If i < gdvListFileCT1.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        If _File = "" Then
            _File = Nothing
        End If
        CType(sender, PopupContainerEdit).EditValue = _File
    End Sub

    Private Sub RepositoryItemPopupContainerEdit2_Popup(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit2.Popup
        LoadDSFileDinhKem1(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub gdvListFileCT1_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles gdvListFileCT1.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            'OpenFileOnLocal(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & e.CellValue, e.CellValue)
            OpenFileOnLocal(UrlITPhanmem & e.CellValue, e.CellValue)
        End If
    End Sub
End Class
