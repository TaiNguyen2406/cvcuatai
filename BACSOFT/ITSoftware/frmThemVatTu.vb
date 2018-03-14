Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraGrid.Columns

Public Class frmThemVatTu
    ' _flagForm = false thì form làm nhiệm vụ thêm mới. _flagForm = true thì form  làm nhiệm vụ cập nhật
    Public _flagForm As Boolean = False

    ' Những biến này lưu giá trị nhận về từ gridview Danh sách vật tư
    Public _Tennhom As String
    Public _Tenvattu As String
    Public _TenhangSX As String
    Public _Model As String
    Public _Code As String
    Public _Soluong As String
    Public _Tendonvitinh As String
    Public _Dongia As String
    Public _Trangthai As String
    Public _GhiChu As String
    Public _SoYC As String
    Public _recordId As String

    Dim _idTempSaved As Object

    ' Select yêu cầu bên kinh doanh. Đoạn code này để tạm SQL dài dòng vậy, ko liên quan IT.
    Sub SelectYeucau()
        Dim sql = ""
        If cbo500.EditValue = "Top 500" Then
            sql = "Select top 500 "
        Else
            sql = "Select "
        End If

        sql &= " BANGYEUCAU.ID,Ngaythang,Sophieu, KHACHHANG.ttcMa,"
         sql &= "  KHACHHANG.Ten AS TenKH, NHANSU1.Ten as TakeCare,NGUOILAP.Ten AS NguoiLap,"
          sql &= " NHANSU2.Ten AS NguoiGD, BANGYEUCAU.NoiDung, tbl1.Noidung as Trangthai "
          sql &= " FROM BANGYEUCAU "
          sql &= " LEFT OUTER JOIN KHACHHANG ON BANGYEUCAU.IDKhachhang=KHACHHANG.ID "
          sql &= "  LEFT OUTER JOIN NHANSU AS NHANSU1 ON BANGYEUCAU.IDTakecare=NHANSU1.ID "
          sql &= "  LEFT OUTER JOIN NHANSU AS NHANSU2 ON BANGYEUCAU.IDNgd=NHANSU2.ID "
           sql &= " LEFT OUTER JOIN NHANSU AS NGUOILAP ON BANGYEUCAU.IDUser=NGUOILAP.ID "
          sql &= "  Left join tblTuDien as tbl1 on tbl1.Ma = BANGYEUCAU.TrangThai and tbl1.Loai = 0 "
          sql &= " LEFT OUTER JOIN NHANSU AS NGUOIXL ON BANGYEUCAU.IDNhanXL=NGUOIXL.ID where CongTrinh = 2 and 1 = 1 "

        If cbo500.EditValue = "Tuỳ chỉnh" Then
            If Not txtTungay.EditValue Is Nothing And Not txtDenngay.EditValue Is Nothing Then
                AddParameterWhere("@TuNgay", txtTungay.EditValue)
                AddParameterWhere("@DenNgay", txtDenngay.EditValue)
                sql &= " AND CONVERT(datetime,CONVERT(nvarchar,BANGYEUCAU.Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
            End If

            If Not cboKH.EditValue Is Nothing Then
                sql &= " AND BANGYEUCAU.IDKhachhang = '" & cboKH.EditValue & "' "
            End If

            If Not cboTrangthaiYC.EditValue Is Nothing Then
                sql &= " AND BANGYEUCAU.TrangThai = " & cboTrangthaiYC.EditValue
            End If
        End If

        sql = sql & " order by Ngaythang desc"


        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            grdDSYeucau.DataSource = tb

            If _flagForm Then
                Dim rowHandle = grdViewDSYeucau.LocateByValue("Sophieu", _SoYC)
                grdViewDSYeucau.Columns("Sophieu").FilterInfo = New ColumnFilterInfo("Sophieu = " & _SoYC)
                If (rowHandle <> DevExpress.XtraGrid.GridControl.InvalidRowHandle) Then
                    grdViewDSYeucau.FocusedRowHandle = rowHandle
                End If
            End If
        Else
                ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Select danh sách KH
    Sub SelectDSKH()
        Dim tb As DataTable = ExecuteSQLDataTable("Select distinct ID, ttcMa, Ten from KhachHang")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit1.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Select trạng thái yêu cầu
    Sub SelectTrangthaiYC()
        Dim tb As DataTable = ExecuteSQLDataTable("Select Ma, NoiDung FROM tblTuDien WHERE Loai = 0 ORDER BY Ma")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit2.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Select bảng CSDL TENNHOM vào combobox cboNhom
    Sub SelectNhom()
        Dim tb As DataTable = ExecuteSQLDataTable("Select ID, Ten FROM TENNHOM WHERE CHARINDEX(N'Phần mềm', Ten) > 0")
        If Not tb Is Nothing Then
            cboNhom.Properties.DataSource = tb
            cboNhom.ItemIndex = 0
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Select bảng CSDL TENVATTU vào combobox cboTenvattu
    Sub SelectTenvattu(idTennhom As Integer)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID, Ten FROM tblTenvattuphanmem_IT where IdTennhom = " & idTennhom)
        If Not tb Is Nothing Then
            cboTenvattu.Properties.DataSource = tb
            cboTenvattu.ItemIndex = 0
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Select bảng CSDL TENVATTU vào combobox cboHang
    Sub SelectTenhang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID, Ten FROM TENHANGSANXUAT Where Ten = 'BAA'")
        If Not tb Is Nothing Then
            cboHang.Properties.DataSource = tb
            cboHang.ItemIndex = 0
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Select bảng CSDL TENVATTU vào combobox cboDonvi
    Sub SelectDonvitinh()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID, Ten FROM TENDONVITINH")
        If Not tb Is Nothing Then
            cboDonvi.Properties.DataSource = tb
            cboDonvi.SelectedText = "Gói"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Hàm này khởi tạo dữ liệu khi cập nhật bản ghi
    Public Sub Init()
        ' Nếu form làm nhiệm vụ cập nhật thì điền dữ liệu vào Form.
        If _flagForm Then
            cboTenvattu.SelectedText = _Tenvattu
            'cboDonvi.SelectedText = _Tendonvitinh
            txtModel.Text = _Model
            txtCode.Text = _Code
            txtGhichu.Text = _GhiChu
            cboTrangthai.Text = _Trangthai
            Text = "Sửa vật tư"
        Else
            Text = "Thêm vật tư"
        End If
    End Sub

    ' Form load
    Private Sub frmThemVatTu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowWaiting("Đang tải dữ liệu ...")
        SelectYeucau()
        SelectNhom()
        SelectTenhang()
        SelectDonvitinh()
        SelectTrangthaiYC()
        SelectDSKH()
        CloseWaiting()

        If _flagForm Then
            btLuuLai.Text = "Lưu lại"
            btnLuuthem.Visible = False
        End If

        ' Khởi tạo Điền dữ liệu vào form khi form làm nhiệm vụ cập nhật
        Init()
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    ' Lưu dữ liệu vào CSDL
    Sub SaveForm()
        If txtSophieu.Text.Trim() = "" Then
            ShowCanhBao("Bạn phải chọn số phiếu.")
            Exit Sub
        End If

        If cboNhom.EditValue Is Nothing Then
            ShowCanhBao("Bạn phải chọn nhóm vật tư.")
            Exit Sub
        End If

        If cboTenvattu.EditValue Is Nothing Then
            ShowCanhBao("Bạn phải chọn tên vật tư.")
            Exit Sub
        End If

        Dim idTenVattu = Kiemtratenvattu(cboTenvattu.Text)

        ' Truyền tham số và giá trị để insert/update dữ liệu
        AddParameter("@IDTennhom", cboNhom.EditValue)
        AddParameter("@IDTenvattu", idTenVattu)
        AddParameter("@IDHangSanxuat", cboHang.EditValue)
        AddParameter("@Model", txtModel.Text)
        AddParameter("@Code", txtCode.Text)
        AddParameter("@Ghichu", txtGhichu.Text)

        ' Nếu form làm nhiệm vụ thêm mới thì thêm những thông tin này
        If _flagForm = False Then
            AddParameter("@Ngay", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
            AddParameter("@IDUser", TaiKhoan)
            AddParameter("@Dongia1", 0)
            AddParameter("@IDDonvitinh", cboDonvi.EditValue)
        End If

        ' Bắt đầu phiên
        BeginTransaction()
        Dim _iD As Object

        If _flagForm Then
            ' Update bảng CSDL VATTU
            AddParameterWhere("@Id", _recordId)
            _iD = doUpdate("VATTU", "Id = @Id")

            ' Update lại số phiếu của yêu cầu đến
            Dim sql2 = "Delete from YEUCAUDEN where IDVattu = '" & _recordId & "'"
            ExecuteSQLNonQuery(sql2)

            Dim sqls = "Insert into YEUCAUDEN(Sophieu, Noidung, Soluong, Mucdocan, IDVattu, NgayChuyenma, IDChuyenma) values('"
            sqls = sqls & txtSophieu.Text & "', N'Phần mềm', 1, 1, " & _recordId.ToString() & ", convert(datetime,'" & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & "', 103), '" & TaiKhoan & "')"

            ExecuteSQLNonQuery(sqls)
        Else

            If _idTempSaved Is Nothing Then
                _iD = doInsert("VATTU")
            Else
                ' Update bảng CSDL VATTU
                AddParameterWhere("@Id", _idTempSaved)
                _iD = doUpdate("VATTU", "Id = @Id")

                ' Update lại số phiếu của yêu cầu đến
                Dim sql2 = "Delete from YEUCAUDEN where IDVattu = '" & _idTempSaved & "'"
                ExecuteSQLNonQuery(sql2)

                Dim sqls = "Insert into YEUCAUDEN(Sophieu, Noidung, Soluong, Mucdocan, IDVattu, NgayChuyenma, IDChuyenma) values('"
                sqls = sqls & txtSophieu.Text & "', N'Phần mềm', 1, 1, " & _idTempSaved.ToString() & ", convert(datetime,'" & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & "', 103), '" & TaiKhoan & "')"

                ExecuteSQLNonQuery(sqls)
            End If

        End If

        If _iD Is Nothing Then
            ' Có lỗi thì Huỷ phiên
            RollBackTransaction()
            ' Báo lỗi 
            ShowBaoLoi(LoiNgoaiLe)
        Else
            If _flagForm Then
                ShowAlert("Đã cập nhật thành công!")
            End If

            ' Xác nhận phiên
            ComitTransaction()

            If _flagForm = False Then
                ' Nếu form là insert thì mới insert vật tư vào bảng Yêu cầu đến
                Dim sqls = "Insert into YEUCAUDEN(Sophieu, Noidung, Soluong, Mucdocan, IDVattu, NgayChuyenma, IDChuyenma) values('"
                sqls = sqls & txtSophieu.Text & "', N'Phần mềm', 1, 1, " & _iD.ToString() & ", convert(datetime,'" & DateTime.Now.ToString("dd/MM/yyyy HH:mm") & "', 103), '" & TaiKhoan & "')"

                Dim val = ExecuteSQLNonQuery(sqls)
                If Not val Is Nothing Then
                    ShowAlert("Đã cập nhật thành công!")
                Else
                    ' Báo lỗi
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If

            If _idTempSaved Is Nothing Then
                _idTempSaved = _iD
            End If

            ' Tải lại dữ liệu của gridview sau khi thêm mới
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhsachVatTu).SelectDanhsachphanmem()
            End If
    End Sub

    Function Kiemtratenvattu(TenVattu As String) As String
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID, Ten FROM TENVATTU Where Ten = N'" & TenVattu & "'")
        Dim maTenVattu = "0"
        Dim _iD As Object
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                ' Nhận về mã
                maTenVattu = tb.Rows(0)(0).ToString()
            Else
                ' Thêm mới
                AddParameter("@ten", TenVattu)
                AddParameter("@Ten_ENG", TenVattu)
                _iD = doInsert("TENVATTU")

                If _iD Is Nothing Then
                    ' Có lỗi thì Huỷ phiên
                    RollBackTransaction()
                    ' Báo lỗi
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ' Xác nhận phiên
                    ComitTransaction()
                    maTenVattu = _iD
                End If
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        Return maTenVattu
    End Function

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        SaveForm()

        'If _flagForm = False Then
        '    Close()
        'End If
    End Sub

    Private Sub grdViewDSYeucau_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSYeucau.FocusedRowChanged
        Dim makh = grdViewDSYeucau.GetFocusedRowCellValue("ttcMa")
        Dim soyc = grdViewDSYeucau.GetFocusedRowCellValue("Sophieu")
        txtModel.EditValue = makh
        txtSophieu.EditValue = soyc
    End Sub

    Private Sub btnXem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXem.ItemClick
        SelectYeucau()
    End Sub

    Private Sub cboTenvattu_KeyUp(sender As Object, e As KeyEventArgs) Handles cboTenvattu.KeyUp
        'If e.KeyCode = Keys.Enter Then
        '    Dim temp = cboTenvattu.Properties.GetKeyValueByDisplayText(cboTenvattu.Text)
        '    Dim temptext = ""
        '    If temp Is Nothing Then
        '        AddParameter("@ten", cboTenvattu.Text)
        '        AddParameter("@Ten_ENG", cboTenvattu.Text)
        '        temptext = cboTenvattu.Text

        '        ' Bắt đầu phiên
        '        BeginTransaction()
        '        Dim _iD As Object
        '        _iD = doInsert("TENVATTU")

        '        If _iD Is Nothing Then
        '            ' Có lỗi thì Huỷ phiên
        '            RollBackTransaction()
        '            ' Báo lỗi
        '            ShowBaoLoi(LoiNgoaiLe)
        '        Else
        '            ' Xác nhận phiên
        '            ComitTransaction()
        '        End If

        '        SelectTenvattu()
        '        cboTenvattu.SelectedText = temptext
        '    End If
        'End If
    End Sub

    Private Sub RepositoryItemLookUpEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit1.ButtonClick
        If e.Button.Index = 1 Then
            cboKH.EditValue = Nothing
        End If
    End Sub

    Private Sub RepositoryItemLookUpEdit2_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemLookUpEdit2.ButtonClick
        If e.Button.Index = 1 Then
            cboTrangthaiYC.EditValue = Nothing
        End If
    End Sub

    Private Sub cbo500_EditValueChanged(sender As Object, e As EventArgs) Handles cbo500.EditValueChanged
        KiemtraTuychinh()
    End Sub

    ' Method này Kiểm tra tuỳ chỉnh chọn combobox 500
    Sub KiemtraTuychinh()
        If cbo500.EditValue = "Tuỳ chỉnh" Then
            cboKH.Enabled = True
            cboTrangthaiYC.Enabled = True
            txtTungay.Enabled = True
            txtDenngay.Enabled = True
        Else
            cboKH.Enabled = False
            cboTrangthaiYC.Enabled = False
            txtTungay.Enabled = False
            txtDenngay.Enabled = False
        End If
    End Sub

    Private Sub btnLuuthem_Click(sender As Object, e As EventArgs) Handles btnLuuthem.Click
        'SaveForm()

        If _flagForm = False Then
            cboTenvattu.EditValue = Nothing
            cboNhom.EditValue = Nothing
            txtCode.Text = ""
            txtGhichu.Text = ""
            _idTempSaved = Nothing
        End If
    End Sub

    Private Sub cboNhom_EditValueChanged(sender As Object, e As EventArgs) Handles cboNhom.EditValueChanged
        SelectTenvattu(cboNhom.EditValue)
    End Sub
End Class