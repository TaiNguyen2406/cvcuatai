Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmXulyCongviec

    ' Lưu Id của Role của người đăng nhập
   ' Dim _idRole As String
    Dim _idRoles As new ArrayList

    ' Biến này lưu tạm các file đính kèm
    Dim dt As New DataTable

    Dim _flagUpdatePhanhoi = False

    ' Method này kiểm tra khi người dùng thay đổi chọn combobox 500
    Sub KiemtraTuychinh()
        If cbo500.EditValue = "Tuỳ chỉnh" Then
            cboKH.Enabled = True
            cboTrangthai.Enabled = True
        Else
            cboKH.Enabled = False
            cboTrangthai.Enabled = False
        End If
    End Sub

    ' Fill danh sách khách hàng vào combobox khách hàng - KH
    Sub SelectDSKH()
        Dim tb As DataTable = ExecuteSQLDataTable("Select ttcMa, Ten from KhachHang	")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit8.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Lưu ID Role của người đăng nhập
    Sub SelectUserRole(idvattu As String, idnhansu As String)
        _idRoles.Clear()
        If idvattu Is Nothing Then
            idvattu = "0"
        End If
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT IdRole FROM tblThanhvienduan_IT " &
            " where IdVatTu = '" & idvattu & "' And IdNhansu = '" & idnhansu & "'")
        If Not tb Is Nothing Then
            For Each item As DataRow In tb.Rows
                _idRoles.Add(Integer.Parse(item(0).ToString()))
            Next
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Enum Trangthaichucnang
        Choxuly = 1
        DoiQuantrixacnhan = 2
        DoiCoderxacnhan = 3
        Dangxuly = 4
        Chuyensangtest = 5
        Testcoloi = 6
        Hoanthanh = 7
    End Enum

    ' Select danh sách phần mềm theo thay đổi giá trị của cbo500
    Public Sub SelectDanhsachphanmem()
        Dim sql As String
        If cbo500.EditValue = "Top 500" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xem', @top = 500, @trangthai = 1"
        ElseIf cbo500.EditValue = "Tất cả" Then
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xem', @top = 1000000, @trangthai = 1"
        Else
            sql = "exec [sp_QuanlyVattu_Phanmem_IT] @activity = 'xem', @top = 1000000, @makh = '" & cboKH.EditValue & "', @trangthai = 1"
        End If

        Dim dt1 As DataTable = ExecuteSQLDataTable(sql)
        grdDSVattu.DataSource = dt1
    End Sub

    Public Sub SelectDanhmucphanhe(mavattu As Integer)
        Dim sql As String
        sql = "exec [sp_Phanhe_IT] @activity = 'xem', @idvattu = " & mavattu.ToString()
        Dim dt1 As DataTable = ExecuteSQLDataTable(sql)
        grdDSPhanhe.DataSource = dt1
    End Sub

    Public Sub SelectDanhmucchucnang(maphanhe As Integer)
        Dim sql As String
        sql = "exec [sp_DSChucnang_IT] @activity = 'xemgv', @IdPhanhe = " & maphanhe.ToString()
        Dim dt1 As DataTable = ExecuteSQLDataTable(sql)
        grdDSChucnang.DataSource = dt1
    End Sub

    Public Sub SelectNoidungphanhoi(idchucnang As Integer)
        Dim sql As String
        sql = "exec [sp_Xulycongviec_IT] @activity = 'xem', @IdChucnang = " & idchucnang.ToString()
        Dim dt1 As DataTable = ExecuteSQLDataTable(sql)
        grdNoidungphanhoi.DataSource = dt1
    End Sub

    ' Đổ dữ liệu vào các combobox lọc danh mục chức năng
    Sub SelectDSNhanvien()

        'Dim tb As DataTable = ExecuteSQLDataTable("Select ID, Ten from NHANSU")
        Dim idVatTu = grdViewDSVattu.GetFocusedRowCellValue("IdVatTu")
        If grdViewDSVattu.RowCount > 0 Then
            Dim tb As DataTable = ExecuteSQLDataTable("exec [sp_Thanhvienduan_IT] @activity = 'xem' , @IdRole = 2, @idvattu = '" & idVatTu & "'")

            If Not tb Is Nothing Then
                RepositoryItemLookUpEdit5.DataSource = tb
                RepositoryItemLookUpEdit5.DisplayMember = "Ten"
                RepositoryItemLookUpEdit5.ValueMember = "Id"
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

            tb = ExecuteSQLDataTable("exec [sp_Thanhvienduan_IT] @activity = 'xem' , @IdRole = 3,  @idvattu = '" & idVatTu & "'")

            If Not tb Is Nothing Then
                RepositoryItemLookUpEdit6.DataSource = tb
                RepositoryItemLookUpEdit6.DisplayMember = "Ten"
                RepositoryItemLookUpEdit6.ValueMember = "Id"
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    ' Fill các bản ghi trạng thái vào combobox trạng thái
    Sub SelectFilterTrangthai()
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID, Ten from tblTrangthaichucnang_IT")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit7.DataSource = tb
            RepositoryItemLookUpEdit7.DisplayMember = "Ten"
            RepositoryItemLookUpEdit7.ValueMember = "ID"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        tb = ExecuteSQLDataTable("Select ID, Ten from tblTrangthaichucnang_IT where Id <> 1 and Id <> 2 and Id <> 3")
        cboTrangthaichucnang.Properties.DataSource = tb
    End Sub

    ' Method này kiểm tra xem có phải role quản trị không
    Function KiemtraRole() As Boolean
        Dim idVattu = grdViewDSVattu.GetFocusedRowCellValue("IdVatTu").ToString()
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID from tblThanhvienduan_IT where IdVatTu = '" & idVattu & "' and IdNhansu = " & TaiKhoan & " and IdRole = 1 ")
        If tb.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Method này kiểm tra xem chức năng đã rao việc chưa
    Function KiemtraDaRaoviec(idChucnang As String) As Boolean
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID from tblGiaoNhanViec_IT where IdChucnang = " & idChucnang & " and _Active = 1")
        If tb.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Method này kiểm tra xem chức năng đã nhận việc chưa
    Function KiemtraDaNhanviec(idChucnang As String) As Boolean
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID from tblGiaoNhanViec_IT where IdChucnang = " & idChucnang & " and _Active = 1")
        If tb.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnGiaoviec_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGiaoviec.ItemClick
        Dim idChucnang = grdViewDSChucnang.GetFocusedRowCellValue("Id")

        If grdViewDSPhanhe.RowCount = 0 Or grdViewDSChucnang.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn phân hệ/chức năng trước khi giao việc.")
            Exit Sub
        End If

        Dim idGiaoviec As Object
        If KiemtraRole() = False Then
            ShowCanhBao("Bạn không có quyền giao việc. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        If KiemtraDaRaoviec(idChucnang) = True Then
            Dim trangthaiChucnang = grdViewDSChucnang.GetFocusedRowCellValue("Trangthaichucnang")
            If trangthaiChucnang.ToString().Contains("Hoàn thành") Or
                trangthaiChucnang.ToString().Contains("Test có lỗi") Or
                trangthaiChucnang.ToString().Contains("Đang xử lý") Or
                trangthaiChucnang.ToString().Contains("Chuyển sang test") Then
                ShowCanhBao("Chức năng này đang được xử lý.")
                Exit Sub
            End If

            ShowCanhBao("Chức năng này đã được giao việc. Bạn sẽ giao việc lại.")
            idGiaoviec = grdViewDSChucnang.GetFocusedRowCellValue("IdGiaoviec")
        End If

        Dim idPhanhe = grdViewDSPhanhe.GetFocusedRowCellValue("Id")
        Dim idVatTu = grdViewDSVattu.GetFocusedRowCellValue("IdVatTu")
        Dim formGiaonhanviec As frmGiaoNhanViec
        formGiaonhanviec = New frmGiaoNhanViec
        formGiaonhanviec._idPhanhe = idPhanhe
        formGiaonhanviec._idChucnang = idChucnang
        formGiaonhanviec._idVattuPhanmem = idVatTu
        formGiaonhanviec._IdGiaoviec = idGiaoviec

        formGiaonhanviec._NgayNhanviec = grdViewDSChucnang.GetFocusedRowCellValue("Ngaynhanviec").ToString()
        formGiaonhanviec._Thoihan = grdViewDSChucnang.GetFocusedRowCellValue("Thoihanhoanthanh").ToString()
        formGiaonhanviec._NgayHoanthanh = grdViewDSChucnang.GetFocusedRowCellValue("Ngayketthuc").ToString()
        formGiaonhanviec._Coder = grdViewDSChucnang.GetFocusedRowCellValue("Nguoinhanviec").ToString()
        If Not grdViewDSChucnang.GetFocusedRowCellValue("IdDanhsachNguoiTest") Is Nothing Then
            formGiaonhanviec._IdTesters = grdViewDSChucnang.GetFocusedRowCellValue("IdDanhsachNguoiTest").ToString()
            formGiaonhanviec._Testers = grdViewDSChucnang.GetFocusedRowCellValue("DanhsachNguoiTest").ToString()
        End If

        If Not grdViewDSChucnang.GetFocusedRowCellValue("IdDanhsachNguoiPhoihop") Is Nothing Then
            formGiaonhanviec._NguoiHotro = grdViewDSChucnang.GetFocusedRowCellValue("DanhsachNguoiPhoihop").ToString()
            formGiaonhanviec._IdNguoiHotro = grdViewDSChucnang.GetFocusedRowCellValue("IdDanhsachNguoiPhoihop").ToString()
        End If

        formGiaonhanviec._Ghichu = grdViewDSChucnang.GetFocusedRowCellValue("GhichuGV").ToString()
        formGiaonhanviec._Files = grdViewDSChucnang.GetFocusedRowCellValue("Filedinhkem").ToString()

        ' Báo form làm nhiệm vụ giao việc
        formGiaonhanviec.Text = "Giao việc"
        formGiaonhanviec._giaoViec = True
        formGiaonhanviec.ShowDialog()
    End Sub

    Private Sub frmXulyCongviec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Thêm cột vào table chứa file tạm
        dt.Columns.Add("colFile")
        dt.Columns(0).ColumnName = "File"

        ShowWaiting("Đang tải dữ liệu ...")
        SelectDanhsachphanmem()
        SelectDSNhanvien()
        SelectFilterTrangthai()
        SelectDSKH()
        CloseWaiting()
        txtNoidungphanhoi.Visible = true
    End Sub

    Private Sub grdViewDSVattu_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSVattu.FocusedRowChanged
        Dim idVatTu = grdViewDSVattu.GetFocusedRowCellValue("IdVatTu")
        SelectDanhmucphanhe(idVatTu)

        SelectUserRole(idVatTu, TaiKhoan.ToString())

        Dim idPhanhe = grdViewDSPhanhe.GetFocusedRowCellValue("Id")
        SelectDanhmucchucnang(idPhanhe)

        ' Fill lại dữ liệu combobox Coder, Tester
        SelectDSNhanvien()

        cboTrangthaichucnang.Properties.ReadOnly = False
        _flagUpdatePhanhoi = False

        If grdViewDSChucnang.RowCount = 0 Then
            btLuuLai.Enabled = False
        End If
    End Sub

    Private Sub grdViewDSPhanhe_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSPhanhe.FocusedRowChanged
        Dim idPhanhe = grdViewDSPhanhe.GetFocusedRowCellValue("Id")
        SelectDanhmucchucnang(idPhanhe)

        grdViewDSChucnang_FocusedRowChanged(Nothing, Nothing)

        cboTrangthaichucnang.Properties.ReadOnly = False
        _flagUpdatePhanhoi = False

        If grdViewDSChucnang.RowCount = 0 Then
            btLuuLai.Enabled = False
        End If
    End Sub

    Private Sub btnNhanviec_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNhanviec.ItemClick
        Dim idChucnang = grdViewDSChucnang.GetFocusedRowCellValue("Id")

        If grdViewDSPhanhe.RowCount = 0 Or grdViewDSChucnang.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn phân hệ/chức năng trước khi nhận việc.")
            Exit Sub
        End If

        If KiemtraDaNhanviec(idChucnang) Then
            ShowCanhBao("Chức năng này đã được nhận việc. Bạn không thể nhận việc lại.")
            Exit Sub
        End If

        Dim idPhanhe = grdViewDSPhanhe.GetFocusedRowCellValue("Id")
        Dim idVatTu = grdViewDSVattu.GetFocusedRowCellValue("IdVatTu")
        Dim formGiaonhanviec As frmGiaoNhanViec
        formGiaonhanviec = New frmGiaoNhanViec
        formGiaonhanviec._idPhanhe = idPhanhe
        formGiaonhanviec._idChucnang = idChucnang
        formGiaonhanviec._idVattuPhanmem = idVatTu
        ' Báo form làm nhiệm vụ nhận việc
        formGiaonhanviec.Text = "Nhận việc"
        formGiaonhanviec._giaoViec = False
        formGiaonhanviec.ShowDialog()
    End Sub

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        Dim idChucnang = grdViewDSChucnang.GetFocusedRowCellValue("Id")

        ' Nếu chọn chức năng rồi thì mới cho phép chat.
        If Not idChucnang Is Nothing Then

            Dim files = ""
            For i = 0 To gdvFileCT.DataRowCount - 1
                files = files & gdvFileCT.GetRowCellValue(i, "File").ToString() & ";"
            Next i

            ' Truyền tham số và giá trị để insert/update dữ liệu
            AddParameter("@IdChucnang", idChucnang)
            AddParameter("@TrangthaiChucnang", cboTrangthaichucnang.EditValue)
            AddParameter("@TenPhanhoi", NguoiDung)
            AddParameter("@NoidungPhanhoi", txtNoidungphanhoi.Text)
            AddParameter("@NgayPhanhoi", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
            ' Mặc định lấy item0 trong _idRoles
            AddParameter("@IdRole", _idRoles.Item(0))
            AddParameter("@Filedinhkem", files)

            ' Bắt đầu phiên
            BeginTransaction()
            Dim _iD, _iDUpdate, _iDUpdate2 As Object

            'If _flagUpdatePhanhoi = False Then
            ' Insert bản ghi vào bảng CSDL tblXulycongviec_IT
            _iD = doInsert("tblXulycongviec_IT")
            _iDUpdate = ExecuteSQLNonQuery("Update tblDSChucnang_IT set TrangthaiChucnang = " & cboTrangthaichucnang.EditValue & " where Id = " & idChucnang)

            If cboTrangthaichucnang.Text.Contains("Hoàn thành") Then
                _iDUpdate2 = ExecuteSQLNonQuery("Update tblGiaoNhanViec_IT set NgayketthucThucte = convert(datetime,'" & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & "',103) where IdChucnang = " & idChucnang)
            End If


            'Else
            'AddParameterWhere("@Id", grdViewNoidungphanhoi.GetFocusedRowCellValue("Id"))

            ' Update bản ghi vào bảng CSDL tblXulycongviec_IT
            '_iD = doUpdate("tblXulycongviec_IT", "Id = @Id")
            '_iDUpdate = 1
            '_iDUpdate = ExecuteSQLNonQuery("Update tblDSChucnang_IT set TrangthaiChucnang = " & cboTrangthaichucnang.EditValue & " where Id = " & idChucnang)

            'If cboTrangthaichucnang.Text.Contains("Hoàn thành") Then
            '_iDUpdate2 = ExecuteSQLNonQuery("Update tblGiaoNhanViec_IT set NgayketthucThucte = convert(datetime,'" & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & "',103) where IdChucnang = " & idChucnang)
            'End If
            'End If


            If _iD Is Nothing Or _iDUpdate Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công!")
                ' Xác nhận phiên
                ComitTransaction()

                ' Tải lại dữ liệu của gridview sau khi thêm mới
                grdViewDSChucnang.GetFocusedDataRow()("Trangthaichucnang") = cboTrangthaichucnang.Text
                If cboTrangthaichucnang.Text.Contains("Hoàn thành") Then
                    grdViewDSChucnang.GetFocusedDataRow()("NgayketthucThucte") = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                End If
                ' Hiển thị danh sách nội dung phản hồi
                SelectNoidungphanhoi(idChucnang)
            End If

        End If
    End Sub

    Private Sub grdViewDSChucnang_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSChucnang.FocusedRowChanged
        ' Hiển thị danh sách nội dung phản hồi
        Dim idChucnang = grdViewDSChucnang.GetFocusedRowCellValue("Id")
        SelectNoidungphanhoi(idChucnang)

        cboTrangthaichucnang.Properties.ReadOnly = False
        _flagUpdatePhanhoi = False

        If Not grdViewDSChucnang.GetFocusedRowCellValue("Trangthaichucnang") Is Nothing Then
            Dim trangthaichucnang = grdViewDSChucnang.GetFocusedRowCellValue("Trangthaichucnang").ToString()
            If trangthaichucnang.Contains("Chờ xử lý") Or trangthaichucnang.Contains("Đợi xác nhận") Then
                btLuuLai.Enabled = False
            Else
                If trangthaichucnang.Contains("Hoàn thành") Then
                    If _idRoles.Count > 0 Then
                        If _idRoles.Contains(3) = false Then
                            btLuuLai.Enabled = False
                        Else
                            cboTrangthaichucnang.EditValue = cboTrangthaichucnang.Properties.GetKeyValueByDisplayText(trangthaichucnang)
                            btLuuLai.Enabled = True
                        End If
                    Else
                        btLuuLai.Enabled = False
                    End If
                Else
                    cboTrangthaichucnang.EditValue = cboTrangthaichucnang.Properties.GetKeyValueByDisplayText(trangthaichucnang)
                    btLuuLai.Enabled = True
                End If

            End If
        End If
    End Sub

    ' Tìm kiếm chức năng
    Private Sub btnTim_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTim.ItemClick
        Dim sql As String
        Dim idPhanhe = grdViewDSPhanhe.GetFocusedRowCellValue("Id")

        Dim idNguoinhanviec = ""
        If cboNguoinhanviec.EditValue Is Nothing Then
            idNguoinhanviec = ""
        Else
            idNguoinhanviec = cboNguoinhanviec.EditValue
        End If

        Dim idNguoiTest = ""
        If cboNguoitest.EditValue Is Nothing Then
            idNguoiTest = ""
        Else
            idNguoiTest = cboNguoitest.EditValue
        End If

        Dim trangthaiChucnang = ""
        If cboTrangthai.EditValue Is Nothing Then
            trangthaiChucnang = ""
        Else
            trangthaiChucnang = cboTrangthai.EditValue
        End If

        sql = "exec [sp_DSChucnang_IT] @activity = 'tim', @IdPhanhe = " & idPhanhe & ", @FromDate = '" & txtTungay.EditValue & "', @ToDate = '" & txtDenngay.EditValue & "'" & ", @Nguoinhanviec = '" & idNguoinhanviec & "', @Nguoitest = '" & idNguoiTest & "', @TrangthaiChucnang = '" & trangthaiChucnang & "'"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDSChucnang.DataSource = dt
    End Sub

    Private Sub btnXacnhanNhanviec_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXacnhanNhanviec.ItemClick

        If KiemtraRole() = False Then
            ShowCanhBao("Bạn không có quyền xác nhận. Liên hệ phụ trách dự án.")
            Exit Sub
        End If

        If grdViewDSPhanhe.RowCount = 0 Or grdViewDSChucnang.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn phân hệ/chức năng trước khi xác nhận.")
            Exit Sub
        End If

        If ShowCauHoi("Bạn xác nhận Coder nhận việc?") Then
            Dim _iD As Object
            Dim _iD2 As Object

            ' Thay đổi trạng thái chức năng là đang xử lý
            ' Chuyển _Giaoviec = 1 Giao việc
            Dim strIdChucnang = ""
            If grdViewDSChucnang.DataRowCount > 0 Then

                strIdChucnang = "("
                For i = 0 To grdViewDSChucnang.DataRowCount - 1
                    If grdViewDSChucnang.GetRowCellValue(i, "checkbox") Then
                        ' Kiểm tra chỉ có những chức năng đang chờ xác nhận của quản trị mới thực hiện được xác nhận
                        Dim trangthaichucnang = grdViewDSChucnang.GetFocusedRowCellValue("Trangthaichucnang").ToString()
                        If trangthaichucnang.Contains("Đợi xác nhận của quản trị") Then
                            strIdChucnang = strIdChucnang & grdViewDSChucnang.GetRowCellValue(i, "Id") & ","
                        End If
                    End If
                Next i

                strIdChucnang = strIdChucnang.Substring(0, strIdChucnang.Trim().Length - 1) & ")"
            End If

            If strIdChucnang.Trim() = ")" Then
                ShowCanhBao("Bạn phải tích chọn chức năng trước khi xác nhận.")
                Exit Sub
            End If

            ' Bắt đầu phiên
            BeginTransaction()
            _iD = ExecuteSQLNonQuery("Update tblDSChucnang_IT set TrangthaiChucnang = " & Trangthaichucnang.Dangxuly & " where Id in " & strIdChucnang)
            _iD2 = ExecuteSQLNonQuery("Update tblGiaoNhanViec_IT set NgaynhanviecThucte = convert(datetime,'" & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & "',103) where IdChucnang in " & strIdChucnang)

            If _iD Is Nothing Or _iD2 Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công!")
                ' Xác nhận phiên
                ComitTransaction()

                '' Gửi email ở đây
                '' Gửi người nhận việc
                Dim tb As DataTable = ExecuteSQLDataTable("exec sp_QuanlyVattu_Phanmem_IT @activity = 'emailnv', " & " @idChucnangs = '" & strIdChucnang & "'")
                Dim strEmail = ""
                If tb.Rows.Count > 0 Then
                    For Each item As DataRow In tb.Rows
                        strEmail &= item(1).ToString() & ","
                    Next
                End If

                Utils.Email.SendToList(DataSourceDSFile(strEmail, "Email", ","), "Bộ phận IT-Phần mềm BAC thông báo xác nhận nhận việc", "Quản trị Bộ phận IT-Phần mềm thông báo cho bạn biết: quản trị đã xác nhận nhận việc của bạn. Bạn truy cập ngay phần mềm BAC để xem chi tiết công việc.".ToString.Replace(Chr(10), " <BR /> "), EmailNguoiDung)

                ' Tải lại dữ liệu của gridview sau khi thêm mới
                btnTim_ItemClick(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub RepositoryItemLookUpEdit5_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit5.ButtonClick
        If e.Button.Index = 1 Then
            cboNguoinhanviec.EditValue = Nothing
        End If
    End Sub

    Private Sub RepositoryItemLookUpEdit6_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit6.ButtonClick
        If e.Button.Index = 1 Then
            cboNguoitest.EditValue = Nothing
        End If
    End Sub

    Private Sub RepositoryItemLookUpEdit7_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit7.ButtonClick
        If e.Button.Index = 1 Then
            cboTrangthai.EditValue = Nothing
        End If
    End Sub

    Private Sub btnXemtiendo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXemtiendo.ItemClick
        Dim idVatTu = grdViewDSVattu.GetFocusedRowCellValue("IdVatTu")

        If idVatTu Is Nothing Then
            Exit Sub
        End If

        Dim formXemTiendo As frmXemTiendo
        formXemTiendo = New frmXemTiendo
        formXemTiendo._idVattuPhanmem = idVatTu
        formXemTiendo.ShowDialog()
    End Sub

    Private Sub btnCoderNhanviec_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCoderNhanviec.ItemClick
        If grdViewDSPhanhe.RowCount = 0 Or grdViewDSChucnang.RowCount = 0 Then
            ShowCanhBao("Bạn phải chọn phân hệ/chức năng trước khi xác nhận.")
            Exit Sub
        End If

        If grdViewDSChucnang.GetFocusedRowCellValue("Nguoinhanviec").ToString().Contains(NguoiDung) = False Then
            ShowCanhBao("Bạn phải chọn phân hệ/chức năng trước khi xác nhận.")
            Exit Sub
        End If

        If ShowCauHoi("Bạn chắc chắn nhận việc?") Then
            Dim _iD As Object
            Dim _iD2 As Object

            ' Thay đổi trạng thái chức năng là đang xử lý
            ' Chuyển _Giaoviec = 1 Giao việc
            Dim strIdChucnang = ""
            If grdViewDSChucnang.DataRowCount > 0 Then

                strIdChucnang = "("
                For i = 0 To grdViewDSChucnang.DataRowCount - 1
                    If grdViewDSChucnang.GetRowCellValue(i, "checkbox") Then
                        ' Kiểm tra chỉ có những chức năng đang chờ xác nhận của quản trị mới thực hiện được xác nhận
                        Dim trangthaichucnang = grdViewDSChucnang.GetFocusedRowCellValue("Trangthaichucnang").ToString()
                        If trangthaichucnang.Contains("Đợi xác nhận của coder") Then
                            strIdChucnang = strIdChucnang & grdViewDSChucnang.GetRowCellValue(i, "Id") & ","
                        End If
                    End If
                Next i

                strIdChucnang = strIdChucnang.Substring(0, strIdChucnang.Trim().Length - 1) & ")"
            End If

            If strIdChucnang.Trim() = ")" Then
                ShowCanhBao("Bạn phải tích chọn chức năng trước khi xác nhận.")
                Exit Sub
            End If

            ' Bắt đầu phiên
            BeginTransaction()
            _iD = ExecuteSQLNonQuery("Update tblDSChucnang_IT set TrangthaiChucnang = " & Trangthaichucnang.Dangxuly & " where Id in " & strIdChucnang)
            _iD2 = ExecuteSQLNonQuery("Update tblGiaoNhanViec_IT set NgaynhanviecThucte = convert(datetime,'" & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & "',103) where IdChucnang in " & strIdChucnang)

            ' Gửi email ở đây

            If _iD Is Nothing Or _iD2 Is Nothing Then
                ' Có lỗi thì Huỷ phiên
                RollBackTransaction()
                ' Báo lỗi
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công!")
                ' Xác nhận phiên
                ComitTransaction()

                ' Tải lại dữ liệu của gridview sau khi thêm mới
                btnTim_ItemClick(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub cbo500_EditValueChanged(sender As Object, e As EventArgs) Handles cbo500.EditValueChanged
        KiemtraTuychinh()
    End Sub

    Private Sub btnXem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXem.ItemClick
        SelectDanhsachphanmem()
        btLuuLai.Enabled = False
    End Sub

    Private Sub btThemFile_Click(sender As Object, e As EventArgs) Handles btThemFile.Click
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each file In openFile.FileNames
                Dim dr As DataRow = dt.NewRow()
                dr(0) = IO.Path.GetFileName(file)
                dt.Rows.Add(dr)
            Next
            gdvFile.DataSource = dt
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As Object, e As EventArgs) Handles btXoaFile.Click
        If gdvFileCT.FocusedRowHandle <0 Then Exit Sub
        gdvFileCT.DeleteSelectedRows()
    End Sub

    Private Sub RepositoryItemLookUpEdit8_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit8.ButtonClick
        If e.Button.Index = 1 Then
            cboKH.EditValue = Nothing
        End If
    End Sub

    Private Sub cboTrangthaichucnang_EditValueChanged(sender As Object, e As EventArgs) Handles cboTrangthaichucnang.EditValueChanged
        If grdViewDSChucnang.RowCount = 0 Then
            btLuuLai.Enabled = False
            Exit Sub
        End If

        Dim trangthaiChucnang = grdViewDSChucnang.GetFocusedRowCellValue("Trangthaichucnang")
        If trangthaiChucnang.ToString().Contains("Chờ xử lý") Or
                trangthaiChucnang.ToString().Contains("Đợi xác nhận") Then
            btLuuLai.Enabled = False
            Exit Sub
        End If

        If cboTrangthaichucnang.Text.Contains("Hoàn thành") Or cboTrangthaichucnang.Text.Contains("Test có lỗi") Then
            If _idRoles.Count > 0 Then
                If _idRoles.Contains(3) = false Then
                    ShowCanhBao("Chỉ cho phép Tester thay đổi trạng thái này.")
                    btLuuLai.Enabled = False
                Else
                    btLuuLai.Enabled = True
                End If
            Else
                btLuuLai.Enabled = False
            End If
        Else
            If _idRoles.Count > 0 Then
                btLuuLai.Enabled = True
            End If
        End If
    End Sub

    Private Sub btnFiles_Click(sender As Object, e As EventArgs) Handles btnFiles.Click
        If groupFiles.Visible Then
            groupFiles.Visible = False
        Else
            groupFiles.Visible = True
        End If
    End Sub

    Private Sub grdViewNoidungphanhoi_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewNoidungphanhoi.FocusedRowChanged
        'txtNoidungphanhoi.Text = grdViewNoidungphanhoi.GetFocusedRowCellValue("NoidungPhanhoi")
        'cboTrangthaichucnang.EditValue = grdViewNoidungphanhoi.GetFocusedRowCellValue("TrangthaiChucnang")
        'cboTrangthaichucnang.Properties.ReadOnly = True
        '_flagUpdatePhanhoi = True

        'Dim _Files = grdViewNoidungphanhoi.GetFocusedRowCellValue("Filedinhkem")
        'If Not _Files Is Nothing Then

        '    For Each file In _Files.Split(";")
        '        If file.Trim().Length > 0 Then

        '            Dim dr As DataRow = dt.NewRow()
        '            dr(0) = file
        '            dt.Rows.Add(dr)

        '        End If
        '    Next

        '    gdvFile.DataSource = dt
        'End If
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

    Private Sub RepositoryItemPopupContainerEdit1_Popup(sender As Object, e As EventArgs) Handles RepositoryItemPopupContainerEdit1.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
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

    Private Sub gdvListFileCT_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            'OpenFileOnLocal(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & e.CellValue, e.CellValue)
            OpenFileOnLocal(UrlITPhanmem & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub gdvListFileCT1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT1.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            'OpenFileOnLocal(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & e.CellValue, e.CellValue)
            OpenFileOnLocal(UrlITPhanmem & e.CellValue, e.CellValue)
        End If
    End Sub
End Class
