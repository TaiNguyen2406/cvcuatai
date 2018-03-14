Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmDanhsachVatTu

    ' Method này Kiểm tra tuỳ chỉnh chọn combobox 500
    Sub KiemtraTuychinh()
        If cbo500.EditValue = "Tuỳ chỉnh" Then
            txtSoYC.Enabled = True
            cboKH.Enabled = True
            cboTrangthai.Enabled = True
            txtHoanthanhTu.Enabled = True
            txtHoanthanhDen.Enabled = True
        Else
            txtSoYC.Enabled = False
            cboKH.Enabled = False
            cboTrangthai.Enabled = False
            txtHoanthanhTu.Enabled = False
            txtHoanthanhDen.Enabled = False
        End If
    End Sub

    ' Khởi tạo dữ liệu cho form cập nhật vật tư được gọi
    Sub OpenUpdateForm()
        If grdViewDSVattu.GetFocusedRowCellValue("Id") IsNot Nothing Then

            Dim recordId = grdViewDSVattu.GetFocusedRowCellValue("Id")
            Dim Tennhom = grdViewDSVattu.GetFocusedRowCellValue("Tennhom")
            Dim Tenvattu = grdViewDSVattu.GetFocusedRowCellValue("Tenvattu")
            Dim TenhangSX = grdViewDSVattu.GetFocusedRowCellValue("TenhangSX")
            Dim Model = grdViewDSVattu.GetFocusedRowCellValue("Model")
            Dim Code = grdViewDSVattu.GetFocusedRowCellValue("Code")
            Dim Tendonvitinh = grdViewDSVattu.GetFocusedRowCellValue("Tendonvitinh")
            Dim Dongia = grdViewDSVattu.GetFocusedRowCellValue("Dongia")
            Dim Trangthai = grdViewDSVattu.GetFocusedRowCellValue("Trangthai").ToString()
            Dim GhiChu = grdViewDSVattu.GetFocusedRowCellValue("Ghichu")
            Dim SoYC = grdViewDSVattu.GetFocusedRowCellValue("Sophieu").ToString()

            If Trangthai.Contains("Đã xác nhận") Then
                ShowCanhBao("Bạn không được phép sửa vật tư đã được xác nhận.")
                Exit Sub
            End If

            Dim formThemvattu = New frmThemVatTu
            formThemvattu._Tennhom = Tennhom
            formThemvattu._Tenvattu = Tenvattu
            formThemvattu._TenhangSX = TenhangSX
            formThemvattu._Model = Model
            formThemvattu._Code = Code
            formThemvattu._Tendonvitinh = Tendonvitinh
            formThemvattu._Dongia = Dongia
            formThemvattu._Trangthai = Trangthai

            If Not SoYC Is Nothing Then
                formThemvattu._SoYC = SoYC
            End If

            If Not GhiChu Is Nothing Then
                formThemvattu._GhiChu = GhiChu
            End If

            formThemvattu._recordId = recordId
            formThemvattu._flagForm = True ' Báo form làm nhiệm vụ cập nhật
            formThemvattu.ShowDialog()
        End If
    End Sub

    ' Method này select tất cả bản ghi bảng KhachHang
    Sub SelectDSKH()
        Dim tb As DataTable = ExecuteSQLDataTable("Select ttcMa, Ten from KhachHang	")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit5.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Fill dữ liệu từ bảng Thanhvienduan vào GridControl grdDSVaitro
    Public Sub FillDSVaitro(idVattu As Integer)
        Dim sql As String
        sql = "exec [sp_Thanhvienduan_IT] @activity = 'xem', @idvattu = '" & idVattu & "'"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDSVaitro.DataSource = dt
    End Sub

    Function KiemtraRole(idVattu As Object) As Boolean
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID from tblThanhvienduan_IT where IdVatTu = '" & idVattu & "' and IdNhansu = " & TaiKhoan & " and IdRole = 1 ")
        If tb.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Select các bản ghi vật tư phần mềm theo chọn combobox 500
    Public Sub SelectDanhsachphanmem()
        Dim sql = ""
        If cbo500.EditValue = "Top 500" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 500"
        ElseIf cbo500.EditValue = "Tất cả" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 100000"
        Else
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xemhet', @top = 1000000, @makh = '" & cboKH.EditValue & "', @trangthai = " &
            RepositoryItemComboBox2.Items.IndexOf(cboTrangthai.EditValue) & ", @soyc = '" & txtSoYC.EditValue & "', @hoanthanhtu = '" &
            txtHoanthanhTu.EditValue & "', @hoanthanhden = '" & txtHoanthanhDen.EditValue & "'"
        End If

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDSVattu.DataSource = dt

        grdViewDSVattu_FocusedRowChanged(Nothing, Nothing)
    End Sub

    Private Sub btnThem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThem.ItemClick
        Dim formThemvattu As frmThemVatTu
        formThemvattu = New frmThemVatTu
        formThemvattu.ShowDialog()
    End Sub

    Private Sub frmDanhsachVatTu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbo500.EditValue = "Top 500"
        KiemtraTuychinh()
        ShowWaiting("Đang tải dữ liệu ...")
        SelectDSKH()
        SelectDanhsachphanmem()
        CloseWaiting()
    End Sub

    Private Sub btnXem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXem.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        SelectDanhsachphanmem()
        CloseWaiting()
    End Sub

    Private Sub btnSua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        OpenUpdateForm()
    End Sub

    Private Sub btnCapnhatgia_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCapnhatgia.ItemClick
        If grdViewDSVattu.RowCount = 0 Then
            Exit Sub
        End If

        If grdViewDSVattu.GetFocusedRowCellValue("Id") Is Nothing Then
            Exit Sub
        End If

        Dim Trangthai = grdViewDSVattu.GetFocusedRowCellValue("Trangthai").ToString()
        If Trangthai.Contains("Đã xác nhận") Then
            ShowCanhBao("Bạn không được phép cập nhật giá của vật tư đã được xác nhận.")
            Exit Sub
        End If

        Dim idVatTu = grdViewDSVattu.GetFocusedRowCellValue("Id")
        Dim formBaogiavattu As frmBaogiaVatTu
        formBaogiavattu = New frmBaogiaVatTu
        formBaogiavattu._idVattuPhanmem = idVatTu
        formBaogiavattu._baogia = grdViewDSVattu.GetFocusedRowCellValue("Dongia")
        formBaogiavattu.ShowDialog()
    End Sub

    Private Sub btnMoithamgiaduan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnMoithamgiaduan.ItemClick
        If grdViewDSVattu.RowCount = 0 Then
            Exit Sub
        End If

        If grdViewDSVattu.GetFocusedRowCellValue("Id") Is Nothing Then
            Exit Sub
        End If

        If KiemtraRole(grdViewDSVattu.GetFocusedRowCellValue("Id")) = False Then
            ShowCanhBao("Bạn không có quyền cập nhật NVDA. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        Dim idVatTu = grdViewDSVattu.GetFocusedRowCellValue("Id")
        Dim formThamgiaduan As frmThamgiaduan
        formThamgiaduan = New frmThamgiaduan
        formThamgiaduan._idVattuPhanmem = idVatTu
        formThamgiaduan._soYC = grdViewDSVattu.GetFocusedRowCellValue("Sophieu")
        formThamgiaduan._tenPM = grdViewDSVattu.GetFocusedRowCellValue("TenPM")
        formThamgiaduan._model = grdViewDSVattu.GetFocusedRowCellValue("ttcMa")
        formThamgiaduan._code = grdViewDSVattu.GetFocusedRowCellValue("Code")
        formThamgiaduan.ShowDialog()
    End Sub

    Private Sub btnKiemtratrangthai_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKiemtratrangthai.ItemClick
        Dim id = ""
        Dim _iDUpdate As Object

        For i = 0 To grdViewDSVattu.DataRowCount - 1
            id = grdViewDSVattu.GetRowCellValue(i, "Id").ToString()

            ' Về sau để giảm số Update. kiểm tra tồn tại trước.
            BeginTransaction()
            _iDUpdate = ExecuteSQLNonQuery("update VATTU set TrangthaiPM = (case when (select TrangThai from CHAOGIA where IDvattu = VATTU.Id) = 2 then 1 else 0 end)   where Id = " & id)

            If _iDUpdate Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                Exit For
            Else
                'count = count + 1
                ' Xác nhận phiên
                ComitTransaction()
            End If
        Next i

        ' Tải lại dữ liệu của gridview sau khi thêm mới
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhsachVatTu).SelectDanhsachphanmem()
    End Sub

    Private Sub grdViewDSVattu_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSVattu.FocusedRowChanged
        If grdViewDSVattu.RowCount > 0 Then
            Dim idVatTu = grdViewDSVattu.GetFocusedRowCellValue("Id")
            If idVatTu Is Nothing Then
                Exit Sub
            End If
            FillDSVaitro(idVatTu)
        Else
            FillDSVaitro(-1)
        End If
    End Sub

    Private Sub RepositoryItemLookUpEdit5_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit5.ButtonClick
        If e.Button.Index = 1 Then
            cboKH.EditValue = Nothing
        End If
    End Sub

    Private Sub RepositoryItemComboBox2_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemComboBox2.ButtonClick
        If e.Button.Index = 1 Then
            cboTrangthai.EditValue = Nothing
        End If
    End Sub

    Private Sub cbo500_EditValueChanged(sender As Object, e As EventArgs) Handles cbo500.EditValueChanged
        KiemtraTuychinh()
    End Sub

    Private Sub RepositoryItemSpinEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemSpinEdit1.ButtonClick
        If e.Button.Index = 1 Then
            txtHoanthanhTu.EditValue = Nothing
        End If
    End Sub

    Private Sub RepositoryItemSpinEdit2_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemSpinEdit2.ButtonClick
        If e.Button.Index = 1 Then
            txtHoanthanhDen.EditValue = Nothing
        End If
    End Sub

    Public Function GetgrdViewDSVattu() As GridView
        Return grdViewDSVattu
    End Function

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
        LoadDSFileDinhKem1(CType(sender, PopupContainerEdit).EditValue)
    End Sub
End Class
