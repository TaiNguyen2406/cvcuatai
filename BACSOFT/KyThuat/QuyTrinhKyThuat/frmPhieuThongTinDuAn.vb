Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Public Class frmPhieuThongTinDuAn
    Public _soycden As Object
    Public _email, _sdt As Object
    Private _id As Object
    Public _tag As Object
    Private Sub btnDong_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub frmPhieuThongTinDuAn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _soycden Is Nothing Or IsDBNull(_soycden) Then
            Exit Sub
        End If

        Dim dt As New DataTable
        Dim sql1 As String
        If _tag = "mYeuCauDen" Or _tag = "mXuLyYCCongTrinh" Then
            sql1 = "SELECT BYC.Sophieu as Masodathang,KH.Ten AS TenKH, BYC.IDkhachhang as MaKH, NS.ID AS NguoiGD, NS.Email, NS.Mobile, KH.IDLinhVucSX, BYC.IDTakecare, BYC.IDNhanXL, NS.Chucvu, KH.ttcDiachi as DiaChi  FROM BANGYEUCAU BYC LEFT JOIN KHACHHANG KH ON KH.ID = BYC.IDkhachhang LEFT JOIN NHANSU NS ON NS.ID = BYC.IDNgd WHERE BYC.Sophieu = @soycden"
        Else
            sql1 = "SELECT ISNULL(BCG.Masodathang,0) as Masodathang ,KH.Ten AS TenKH, BCG.IDkhachhang as MaKH, BCG.TenDuan AS NoiDung, NS.ID as NguoiGD, NS.Email, NS.Mobile, KH.IDLinhVucSX, BCG.IDTakecare, BCG.IDNgNhanXL as IDNhanXL, NS.Chucvu, KH.ttcDiachi as DiaChi  FROM BANGCHAOGIA BCG LEFT JOIN KHACHHANG KH ON KH.ID = BCG.IDkhachhang LEFT JOIN NHANSU NS ON NS.ID = BCG.IDNgd WHERE BCG.Sophieu = @soycden"
        End If
        AddParameterWhere("@soycden", _soycden.ToString)
        dt = ExecuteSQLDataTable(sql1)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        If _tag = "mYeuCauDen" Then
            If dt.Rows(0)("IDTakecare") <> Convert.ToInt32(TaiKhoan) Then
                btnLuu.Enabled = False
                btThemFile.Enabled = False
                btXoaFile.Enabled = False
            End If
        Else
            If dt.Rows(0)("IDNhanXL") Is Nothing Or IsDBNull(dt.Rows(0)("IDNhanXL")) Then
                btnLuu.Enabled = False
                btThemFile.Enabled = False
                btXoaFile.Enabled = False
            Else
                If dt.Rows(0)("IDNhanXL") <> Convert.ToInt32(TaiKhoan) Then
                    btnLuu.Enabled = False
                    btThemFile.Enabled = False
                    btXoaFile.Enabled = False
                End If
            End If
        End If

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim tb2 As DataTable = DataSourceDSFile(dt.Rows(i)("IDLinhVucSX").ToString)
                dt.Rows(i)("IDLinhVucSX") = ""
                For j As Integer = 0 To tb2.Rows.Count - 1
                    AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                    Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT NoiDung FROM tblTuDien WHERE Loai = 14 AND ID=@ID")
                    If Not tb3 Is Nothing Then
                        dt.Rows(i)("IDLinhVucSX") &= tb3.Rows(0)(0).ToString & "; "
                    End If
                Next
                dt.Rows(i)("IDLinhVucSX") = dt.Rows(i)("IDLinhVucSX").ToString.Trim
            Next
            txtMaKH.EditValue = dt.Rows(0)("MaKH")
            txtTenKH.EditValue = dt.Rows(0)("TenKH").ToString
            txtChucDanh.EditValue = dt.Rows(0)("Chucvu").ToString
            txtDiaChi.EditValue = dt.Rows(0)("DiaChi").ToString
            txtSDT.EditValue = dt.Rows(0)("Mobile").ToString
            txtEmail.EditValue = dt.Rows(0)("Email").ToString
            cbNguoiGD.EditValue = dt.Rows(0)("NguoiGD")
            cbNguoiXL.EditValue = dt.Rows(0)("IDNhanXL")
            cbTakeCare.EditValue = dt.Rows(0)("IDTakecare")

            If _tag = "mCongTrinhCanXuLy" Then
                If dt.Rows(0)("Masodathang") <> 0 Then
                    txtSoYC.EditValue = dt.Rows(0)("Masodathang") & "-" & _soycden
                Else
                    txtSoYC.EditValue = "CG" & _soycden
                End If
            Else
                txtSoYC.EditValue = _soycden
            End If
            _soycden = dt.Rows(0)("Masodathang")
            txtLinhVucHD.EditValue = dt.Rows(0)("IDLinhVucSX")
        End If

        ' Kiểm tra đã tồn tại thông tin dự án chưa
        Dim dt_ttda As New DataTable
        Dim sql As String
        sql = "select *, "
        sql &= " case  KhanCap when 0 then N'Rất khẩn cấp' when 1 then N'Khẩn cấp' when 2 then N'Bình thường' when 3 then N'Không khẩn cấp' end as KhanCap1,"
        sql &= " case  QuanTrong when 0 then N'Rất quan trọng' when 1 then N'Quan trọng' when 2 then N'Bình thường' when 3 then N'Không quan trọng' end as QuanTrong1"
        sql &= " from ThongTinDuAn TTDA WHERE SoYCDen = @soyc"

        AddParameterWhere("@soyc", _soycden.ToString)
        dt_ttda = ExecuteSQLDataTable(sql)
        If dt_ttda Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            Dim str As String = ""
            If dt_ttda.Rows.Count > 0 Then
                _id = dt_ttda.Rows(0)("ID")
                str = "SELECT ROW_NUMBER() OVER (ORDER BY ID) AS STT, *, convert(bit,0)Modify FROM ThongTinDuAnCT_NSKH WHERE IDThongTinDA = " & _id
                str &= " SELECT ROW_NUMBER() OVER (ORDER BY ID) AS STT, *, convert(bit,0)Modify FROM ThongTinDACT_DoiThuCT  WHERE IDThongTinDA = " & _id
                cbKhanCap.EditValue = dt_ttda.Rows(0)("KhanCap1")
                cbQuanTrong.EditValue = dt_ttda.Rows(0)("QuanTrong1")
                txtPhucTap.EditValue = dt_ttda.Rows(0)("PhucTap")
                txtKhoiLuongCV.EditValue = dt_ttda.Rows(0)("KhoiLuongCV")
                txtGiaTriDA.EditValue = dt_ttda.Rows(0)("GiaTriDA")

                txtNguonThongTin.EditValue = dt_ttda.Rows(0)("NguonTT")
                txtLinhVuc.EditValue = dt_ttda.Rows(0)("LinhVucHD")
                txtSoNamHD.EditValue = dt_ttda.Rows(0)("SoNamHD")
                txtNhaPhanPhoi.EditValue = dt_ttda.Rows(0)("NhaPhanPhoi")
                txtSLNS.EditValue = dt_ttda.Rows(0)("SoLuongNS")
                memoCT_KHTB.EditValue = dt_ttda.Rows(0)("KHTBBA")
                popupFileDinhKem.EditValue = dt_ttda.Rows(0)("FileDinhKem")

                If Not IsDBNull(dt_ttda.Rows(0)("MucDichLL")) And Not dt_ttda.Rows(0)("MucDichLL") Is Nothing Then
                    If dt_ttda.Rows(0)("MucDichLL") = "Hợp tác" Or dt_ttda.Rows(0)("MucDichLL") = "Lấy báo giá cạnh tranh" Or dt_ttda.Rows(0)("MucDichLL") = "Dự toán tham khảo" Or dt_ttda.Rows(0)("MucDichLL") = "Triển khai dự án" Or dt_ttda.Rows(0)("MucDichLL") = "Nhu cầu công trình" Or dt_ttda.Rows(0)("MucDichLL") = "Lấy SP cần tư vấn cài đặt" Or dt_ttda.Rows(0)("MucDichLL") = "Tư vấn SP" Or dt_ttda.Rows(0)("MucDichLL") = "Bảo hành" Then
                        cbMucDich.EditValue = dt_ttda.Rows(0)("MucDichLL")
                    Else
                        cbMucDich.EditValue = "Khác:"
                        txtChuThich.EditValue = dt_ttda.Rows(0)("MucDichLL")
                    End If
                Else
                    cbMucDich.EditValue = "Khác:"
                End If
                txtGiaCa.EditValue = dt_ttda.Rows(0)("GiaCa")
                txtThaiDoPV.EditValue = dt_ttda.Rows(0)("ThaiDoPhucVu")
                txtChatLuongSP.EditValue = dt_ttda.Rows(0)("ChatLuongSP")
                txtNangLucKT.EditValue = dt_ttda.Rows(0)("NangLucKT")
                txtUyTin.EditValue = dt_ttda.Rows(0)("UyTin")
                txtAnToanTC.EditValue = dt_ttda.Rows(0)("AnToanThiCong")
                txtNguonTTBA.EditValue = dt_ttda.Rows(0)("NguonThongTin")

            Else
                str = "SELECT ROW_NUMBER() OVER (ORDER BY ID) AS STT, *, convert(bit,0)Modify FROM ThongTinDuAnCT_NSKH WHERE 1 <> 1"
                str &= " SELECT ROW_NUMBER() OVER (ORDER BY ID) AS STT, *, convert(bit,0)Modify FROM ThongTinDACT_DoiThuCT WHERE 1 <> 1"
                cbKhanCap.EditValue = "Rất khẩn cấp"
                cbQuanTrong.EditValue = "Rất quan trọng"
                cbMucDich.EditValue = "Khác:"
            End If
            Dim dt_ct As New DataSet
            dt_ct = ExecuteSQLDataSet(str)
            If dt_ct Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvNSKH.DataSource = dt_ct.Tables(0)
                gdvDTCT.DataSource = dt_ct.Tables(1)
            End If

        End If
        loaddulieu()

    End Sub

    Public Sub loaddulieu()
        Dim dt As New DataTable
        dt = ExecuteSQLDataTable(" SELECT ID, Ten FROM NHANSU ")
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        cbTakeCare.Properties.DataSource = dt
        cbNguoiGD.Properties.DataSource = dt
        cbNguoiXL.Properties.DataSource = dt
        cbNguoiNhap.DataSource = dt
    End Sub

    Private Sub btnThemNS_Click(sender As Object, e As EventArgs) Handles btnThemNS.Click
        gdvNSKHCT.AddNewRow()
        gdvNSKHCT.Focus()
        gdvNSKHCT.SetFocusedRowCellValue("STT", gdvNSKHCT.RowCount)
        gdvNSKHCT.SetFocusedRowCellValue("NguoiNhap", Convert.ToInt32(TaiKhoan))
        gdvNSKHCT.SetFocusedRowCellValue("Modify", True)
    End Sub


    Private Sub gdvNSKHCT_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gdvNSKHCT.ShowingEditor
        If gdvNSKHCT.GetFocusedRowCellValue("NguoiNhap") <> Convert.ToInt32(TaiKhoan) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvNSKHCT_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvNSKHCT.CellValueChanged
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName <> "Modify" Then
            gdvNSKHCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub btnThemDTCT_Click(sender As Object, e As EventArgs) Handles btnThemDTCT.Click
        gdvDTCT_CT.AddNewRow()
        gdvDTCT_CT.Focus()
        gdvDTCT_CT.SetFocusedRowCellValue("Modify", True)
        gdvDTCT_CT.SetFocusedRowCellValue("STT", gdvDTCT_CT.RowCount)
    End Sub

    Private Sub btnXoa_Click(sender As Object, e As EventArgs) Handles btnXoa.Click
        If gdvNSKHCT.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If gdvNSKHCT.GetFocusedRowCellValue("ID") Is Nothing Or IsDBNull(gdvNSKHCT.GetFocusedRowCellValue("ID")) Then
            Exit Sub
        End If
        If gdvNSKHCT.GetFocusedRowCellValue("NguoiNhap") <> Convert.ToInt32(TaiKhoan) Then
            ShowCanhBao("Bạn không có quyền xóa bản ghi này!")
            Exit Sub
        End If
        AddParameterWhere("@IDNS", gdvNSKHCT.GetFocusedRowCellValue("ID"))
        If doDelete("ThongTinDuAnCT_NSKH", "ID = @IDCT") Is Nothing Then ShowBaoLoi("Lỗi khi lưu dữ liệu")

        gdvNSKHCT.DeleteRow(gdvNSKHCT.FocusedRowHandle)
    End Sub

    Private Sub btnXoaDTCT_Click(sender As Object, e As EventArgs) Handles btnXoaDTCT.Click
        If gdvDTCT_CT.GetFocusedRowCellValue("ID") Is Nothing Or IsDBNull(gdvDTCT_CT.GetFocusedRowCellValue("ID")) Then
            Exit Sub
        End If
        AddParameterWhere("@IDCT", gdvDTCT_CT.GetFocusedRowCellValue("ID"))
        If doDelete("ThongTinDACT_DoiThuCT", "ID = @IDCT") Is Nothing Then ShowBaoLoi("Lỗi khi lưu dữ liệu")
        gdvDTCT_CT.DeleteRow(gdvDTCT_CT.FocusedRowHandle)
    End Sub

    Private Sub btnLuu_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLuu.ItemClick
        Dim tg = GetServerTime()

        Select Case cbKhanCap.EditValue
            Case "Rất khẩn cấp"
                AddParameter("@KhanCap", 0)
            Case "Khẩn cấp"
                AddParameter("@KhanCap", 1)
            Case "Bình thường"
                AddParameter("@KhanCap", 2)
            Case "Không khẩn cấp"
                AddParameter("@KhanCap", 3)
        End Select

        Select Case cbQuanTrong.EditValue
            Case "Rất quan trọng"
                AddParameter("@QuanTrong", 0)
            Case "Quan trọng"
                AddParameter("@QuanTrong", 1)
            Case "Bình thường"
                AddParameter("@QuanTrong", 2)
            Case "Không quan trọng"
                AddParameter("@QuanTrong", 3)
        End Select

        AddParameter("@PhucTap", txtPhucTap.EditValue)
        AddParameter("@KhoiLuongCV", txtKhoiLuongCV.EditValue)
        AddParameter("@GiaTriDA", txtGiaTriDA.EditValue)
        AddParameter("@SoYCDen", _soycden)

        ' Đánh giá về Bảo An
        AddParameter("@NguonTT", txtNguonThongTin.EditValue)
        AddParameter("@LinhVucHD", txtLinhVuc.EditValue)
        AddParameter("@SoNamHD", txtSoNamHD.EditValue)
        AddParameter("@NhaPhanPhoi", txtNhaPhanPhoi.EditValue)
        AddParameter("@SoLuongNS", txtSLNS.EditValue)
        AddParameter("@KHTBBA", memoCT_KHTB.EditValue)
        AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)

        If cbMucDich.EditValue <> "Khác:" Then
            AddParameter("@MucDichLL", cbMucDich.EditValue)
        Else
            AddParameter("@MucDichLL", txtChuThich.EditValue)
        End If

        If Not IsDBNull(txtGiaCa.EditValue) And Not txtGiaCa.EditValue Is Nothing Then
            If txtGiaCa.EditValue.ToString = "" Then
                txtGiaCa.EditValue = Nothing
            End If
        End If
        
        If Not IsDBNull(txtThaiDoPV.EditValue) And Not txtThaiDoPV.EditValue Is Nothing Then
            If txtThaiDoPV.EditValue.ToString = "" Then
                txtThaiDoPV.EditValue = Nothing
            End If
        End If
       
        If Not IsDBNull(txtChatLuongSP.EditValue) And Not txtChatLuongSP.EditValue Is Nothing Then
            If txtChatLuongSP.EditValue.ToString = "" Then
                txtChatLuongSP.EditValue = Nothing
            End If
        End If
       
        If Not IsDBNull(txtNangLucKT.EditValue) And Not txtNangLucKT.EditValue Is Nothing Then
            If txtNangLucKT.EditValue.ToString = "" Then
                txtNangLucKT.EditValue = Nothing
            End If
        End If
        
        If Not IsDBNull(txtUyTin.EditValue) And Not txtUyTin.EditValue Is Nothing Then
            If txtUyTin.EditValue.ToString = "" Then
                txtUyTin.EditValue = Nothing
            End If
        End If
       
        If Not IsDBNull(txtAnToanTC.EditValue) And Not txtAnToanTC.EditValue Is Nothing Then
            If txtAnToanTC.EditValue.ToString = "" Then
                txtAnToanTC.EditValue = Nothing
            End If
        End If

        AddParameter("@GiaCa", txtGiaCa.EditValue)
        AddParameter("@ThaiDoPhucVu", txtThaiDoPV.EditValue)
        AddParameter("@ChatLuongSP", txtChatLuongSP.EditValue)
        AddParameter("@NangLucKT", txtNangLucKT.EditValue)
        AddParameter("@UyTin", txtUyTin.EditValue)
        AddParameter("@AnToanThiCong", txtAnToanTC.EditValue)
        AddParameter("@NguonThongTin", txtNguonTTBA.EditValue)

        If _id Is Nothing Or IsDBNull(_id) Then
            _id = doInsert("ThongTinDuAn")
            If _id Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            ' Thêm dữ liệu vào bảng nhân sự khách hàng
            gdvNSKHCT.CloseEditor()
            gdvNSKHCT.UpdateCurrentRow()
            gdvNSKHCT.BeginUpdate()
            For i = 0 To gdvNSKHCT.RowCount - 1
                If gdvNSKHCT.GetRowCellValue(i, "Modify") = True Then
                    AddParameter("@IDThongTinDA", _id)
                    AddParameter("@HoTen", gdvNSKHCT.GetRowCellValue(i, "HoTen"))
                    AddParameter("@ChucVu", gdvNSKHCT.GetRowCellValue(i, "ChucVu"))
                    AddParameter("@SDT", gdvNSKHCT.GetRowCellValue(i, "SDT"))
                    AddParameter("@Email", gdvNSKHCT.GetRowCellValue(i, "Email"))
                    AddParameter("@MucDoHopTac", gdvNSKHCT.GetRowCellValue(i, "MucDoHopTac"))
                    AddParameter("@DanhGia", gdvNSKHCT.GetRowCellValue(i, "DanhGia"))
                    AddParameter("@NguoiNhap", gdvNSKHCT.GetRowCellValue(i, "NguoiNhap"))
                    If doInsert("ThongTinDuAnCT_NSKH") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        gdvNSKHCT.SetRowCellValue(i, "Mofidy", False)
                    End If
                End If
            Next
            gdvNSKHCT.EndUpdate()
            ' Thêm dữ liệu vào bảng đối thủ cạnh tranh
            gdvDTCT_CT.CloseEditor()
            gdvDTCT_CT.UpdateCurrentRow()
            gdvDTCT_CT.BeginUpdate()
            For i = 0 To gdvDTCT_CT.RowCount - 1
                If gdvDTCT_CT.GetRowCellValue(i, "Modify") = True Then
                    AddParameter("@IDThongTinDA", _id)
                    AddParameter("@DoiThu", gdvDTCT_CT.GetRowCellValue(i, "DoiThu"))
                    AddParameter("@GiaTri", gdvDTCT_CT.GetRowCellValue(i, "GiaTri"))
                    AddParameter("@DanhGia", gdvDTCT_CT.GetRowCellValue(i, "DanhGia"))
                    If doInsert("ThongTinDACT_DoiThuCT") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        gdvDTCT_CT.SetRowCellValue(i, "Mofidy", False)
                    End If
                End If
            Next
            gdvDTCT_CT.EndUpdate()
        Else
            AddParameterWhere("@ID", _id)
            If doUpdate("ThongTinDuAn", "ID = @ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            gdvNSKHCT.CloseEditor()
            gdvNSKHCT.UpdateCurrentRow()
            gdvNSKHCT.BeginUpdate()
            For i = 0 To gdvNSKHCT.RowCount - 1
                ' Thêm dữ liệu vào bảng nhân sự khách hàng
                If gdvNSKHCT.GetRowCellValue(i, "Modify") = True Then
                    AddParameter("@IDThongTinDA", _id)
                    AddParameter("@HoTen", gdvNSKHCT.GetRowCellValue(i, "HoTen"))
                    AddParameter("@ChucVu", gdvNSKHCT.GetRowCellValue(i, "ChucVu"))
                    AddParameter("@SDT", gdvNSKHCT.GetRowCellValue(i, "SDT"))
                    AddParameter("@Email", gdvNSKHCT.GetRowCellValue(i, "Email"))
                    AddParameter("@MucDoHopTac", gdvNSKHCT.GetRowCellValue(i, "MucDoHopTac"))
                    AddParameter("@DanhGia", gdvNSKHCT.GetRowCellValue(i, "DanhGia"))
                    AddParameter("@NguoiNhap", gdvNSKHCT.GetRowCellValue(i, "NguoiNhap"))
                    If gdvNSKHCT.GetRowCellValue(i, "ID") Is Nothing Or IsDBNull(gdvNSKHCT.GetRowCellValue(i, "ID")) Then
                        If doInsert("ThongTinDuAnCT_NSKH") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            gdvNSKHCT.SetRowCellValue(i, "Mofidy", False)
                        End If

                    Else
                        AddParameterWhere("@IDNS", gdvNSKHCT.GetRowCellValue(i, "ID"))
                        If doUpdate("ThongTinDuAnCT_NSKH", "ID = @IDNS") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            gdvNSKHCT.SetRowCellValue(i, "Mofidy", False)
                        End If

                    End If
                End If
            Next
            gdvNSKHCT.EndUpdate()
            ' Bảng đối thủ cạnh tranh
            gdvDTCT_CT.CloseEditor()
            gdvDTCT_CT.UpdateCurrentRow()
            gdvDTCT_CT.BeginUpdate()
            For i = 0 To gdvDTCT_CT.RowCount - 1
                If gdvDTCT_CT.GetRowCellValue(i, "Modify") = True Then
                    AddParameter("@IDThongTinDA", _id)
                    AddParameter("@DoiThu", gdvDTCT_CT.GetRowCellValue(i, "DoiThu"))
                    AddParameter("@GiaTri", gdvDTCT_CT.GetRowCellValue(i, "GiaTri"))
                    AddParameter("@DanhGia", gdvDTCT_CT.GetRowCellValue(i, "DanhGia"))

                    If gdvDTCT_CT.GetRowCellValue(i, "ID") Is Nothing Or IsDBNull(gdvDTCT_CT.GetRowCellValue(i, "ID")) Then
                        If doInsert("ThongTinDACT_DoiThuCT") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            gdvDTCT_CT.SetRowCellValue(i, "Mofidy", False)
                        End If
                    Else
                        AddParameterWhere("@IDCT", gdvDTCT_CT.GetRowCellValue(i, "ID"))
                        If doUpdate("ThongTinDACT_DoiThuCT", "ID = @IDCT") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            gdvDTCT_CT.SetRowCellValue(i, "Mofidy", False)
                        End If
                    End If
                End If
            Next
            gdvDTCT_CT.EndUpdate()

        End If


        'Thêm vào lịch sử thay đổi
        Dim str As String = "SELECT COUNT(ID) FROM ThongTinDACT_DoiThuCT WHERE IDThongTinDA = @idda"
        str &= " SELECT COUNT(ID) FROM ThongTinDuAnCT_NSKH WHERE IDThongTinDA = @idda"
        Dim dt_ctda As New DataSet
        AddParameterWhere("@idda", _id)
        dt_ctda = ExecuteSQLDataSet(str)
        If Not dt_ctda Is Nothing Then
            AddParameter("@SLDoiThuCT", dt_ctda.Tables(0).Rows(0)(0).ToString)
            AddParameter("@SLNSKH", dt_ctda.Tables(1).Rows(0)(0).ToString)
        End If
        Select Case cbKhanCap.EditValue
            Case "Rất khẩn cấp"
                AddParameter("@KhanCap", 0)
            Case "Khẩn cấp"
                AddParameter("@KhanCap", 1)
            Case "Bình thường"
                AddParameter("@KhanCap", 2)
            Case "Không khẩn cấp"
                AddParameter("@KhanCap", 3)
        End Select

        Select Case cbQuanTrong.EditValue
            Case "Rất quan trọng"
                AddParameter("@QuanTrong", 0)
            Case "Quan trọng"
                AddParameter("@QuanTrong", 1)
            Case "Bình thường"
                AddParameter("@QuanTrong", 2)
            Case "Không quan trọng"
                AddParameter("@QuanTrong", 3)
        End Select

        AddParameter("@PhucTap", txtPhucTap.EditValue)
        AddParameter("@KhoiLuongCV", txtKhoiLuongCV.EditValue)
        AddParameter("@GiaTriDA", txtGiaTriDA.EditValue)
        AddParameter("@NguonTT", txtNguonThongTin.EditValue)
        AddParameter("@LinhVucHD", txtLinhVuc.EditValue)
        AddParameter("@SoNamHD", txtSoNamHD.EditValue)
        AddParameter("@NhaPhanPhoi", txtNhaPhanPhoi.EditValue)
        AddParameter("@SoLuongNS", txtSLNS.EditValue)
        AddParameter("@KHTBBA", memoCT_KHTB.EditValue)
        If cbMucDich.EditValue <> "Khác:" Then
            AddParameter("@MucDichLL", cbMucDich.EditValue)
        Else
            AddParameter("@MucDichLL", txtChuThich.EditValue)
        End If

        AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
        AddParameter("@GiaCa", txtGiaCa.EditValue)
        AddParameter("@ThaiDoPhucVu", txtThaiDoPV.EditValue)
        AddParameter("@ChatLuongSP", txtChatLuongSP.EditValue)
        AddParameter("@NangLucKT", txtNangLucKT.EditValue)
        AddParameter("@UyTin", txtUyTin.EditValue)
        AddParameter("@AnToanThiCong", txtAnToanTC.EditValue)
        AddParameter("@NguonThongTin", txtNguonTTBA.EditValue)
        AddParameter("@NgaySua", tg)
        AddParameter("@IDTTDA", _id)
        AddParameter("@IDNguoiSua", Convert.ToInt32(TaiKhoan))
        If doInsert("ThongTinDuAn_LichSu") Is Nothing Then ShowBaoLoi(LoiNgoaiLe)
        ShowAlert("Lưu dữ liệu thành công!")
    End Sub

    Private Sub cbMucDich_EditValueChanged(sender As Object, e As EventArgs) Handles cbMucDich.EditValueChanged
        If cbMucDich.EditValue = "Khác:" Then
            txtChuThich.Enabled = True
        Else
            txtChuThich.Enabled = False
        End If
    End Sub

    Private Sub txtGiaCa_EditValueChanged(sender As Object, e As EventArgs) Handles txtGiaCa.EditValueChanged, txtThaiDoPV.EditValueChanged, txtChatLuongSP.EditValueChanged, txtUyTin.EditValueChanged, txtNangLucKT.EditValueChanged, txtAnToanTC.EditValueChanged

        If Not IsDBNull(txtGiaCa.EditValue) And Not txtGiaCa.EditValue Is Nothing Then
            If txtGiaCa.EditValue.ToString = "" Then Exit Sub
            If Not IsNumeric(txtGiaCa.EditValue) Then Exit Sub
            If txtGiaCa.EditValue > 4 Or txtGiaCa.EditValue < 0 Then
                ShowCanhBao("Nhập số từ 0-> 4.")
                txtGiaCa.EditValue = Nothing
                Exit Sub
            End If
        End If

        If Not IsDBNull(txtThaiDoPV.EditValue) And Not txtThaiDoPV.EditValue Is Nothing Then
            If txtThaiDoPV.EditValue.ToString = "" Then Exit Sub
            If Not IsNumeric(txtThaiDoPV.EditValue) Then Exit Sub
            If txtThaiDoPV.EditValue > 4 Or txtThaiDoPV.EditValue < 0 Then
                ShowCanhBao("Nhập số từ 0-> 4.")
                txtThaiDoPV.EditValue = Nothing
                Exit Sub
            End If
        End If

        If Not IsDBNull(txtChatLuongSP.EditValue) And Not txtChatLuongSP.EditValue Is Nothing Then
            If txtChatLuongSP.EditValue.ToString = "" Then Exit Sub
            If Not IsNumeric(txtChatLuongSP.EditValue) Then Exit Sub
            If txtChatLuongSP.EditValue > 4 Or txtChatLuongSP.EditValue < 0 Then
                ShowCanhBao("Nhập số từ 0-> 4.")
                txtChatLuongSP.EditValue = Nothing
                Exit Sub
            End If
        End If

        If Not IsDBNull(txtUyTin.EditValue) And Not txtUyTin.EditValue Is Nothing Then
            If txtUyTin.EditValue.ToString = "" Then Exit Sub
            If Not IsNumeric(txtUyTin.EditValue) Then Exit Sub
            If txtUyTin.EditValue > 4 Or txtUyTin.EditValue < 0 Then
                ShowCanhBao("Nhập số từ 0-> 4.")
                txtUyTin.EditValue = Nothing
                Exit Sub
            End If
        End If

        If Not IsDBNull(txtNangLucKT.EditValue) And Not txtNangLucKT.EditValue Is Nothing Then
            If txtNangLucKT.EditValue.ToString = "" Then Exit Sub
            If Not IsNumeric(txtNangLucKT.EditValue) Then Exit Sub
            If txtNangLucKT.EditValue > 4 Or txtNangLucKT.EditValue < 0 Then
                ShowCanhBao("Nhập số từ 0-> 4.")
                txtNangLucKT.EditValue = Nothing
                Exit Sub
            End If
        End If

        If Not IsDBNull(txtAnToanTC.EditValue) And Not txtAnToanTC.EditValue Is Nothing Then
            If txtAnToanTC.EditValue.ToString = "" Then Exit Sub
            If Not IsNumeric(txtAnToanTC.EditValue) Then Exit Sub
            If txtAnToanTC.EditValue > 4 Or txtAnToanTC.EditValue < 0 Then
                ShowCanhBao("Nhập số từ 0-> 4.")
                txtAnToanTC.EditValue = Nothing
                Exit Sub
            End If
        End If

    End Sub

    Private Sub gdvDTCT_CT_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvDTCT_CT.CellValueChanged
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName <> "Modify" Then
            gdvDTCT_CT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub btThemFile_Click(sender As Object, e As EventArgs) Handles btThemFile.Click

        If _id Is Nothing Or IsDBNull(_id) Then
            ShowCanhBao("Chưa lưu bản ghi. Bạn không thể tải file đính kèm.")
            Exit Sub
        End If
        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            ShowWaiting("Đang tải file lên máy chủ ...")
            If Not System.IO.Directory.Exists(UrlQuyTrinhKyThuat & _soycden) Then
                System.IO.Directory.CreateDirectory(UrlQuyTrinhKyThuat & _soycden)
            End If
            For Each file In openFile.FileNames
                Try
                    path = TaiKhoan.ToString & IO.Path.GetFileName(file)
                    If System.IO.File.Exists(UrlQuyTrinhKyThuat & _soycden & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã tồn tại, bạn có muốn ghi đè không ?") Then
                            IO.File.Copy(file, UrlQuyTrinhKyThuat & _soycden & "\" & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                            popupFileDinhKem.EditValue &= path & ";"
                            AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
                            AddParameterWhere("@ID", _id)
                            If doUpdate("ThongTinDuAn", "ID = @ID") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                                Exit Sub
                            End If
                        End If
                    Else
                        IO.File.Copy(file, UrlQuyTrinhKyThuat & _soycden & "\" & path)
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", path)
                        popupFileDinhKem.EditValue &= path & ";"
                        AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
                        AddParameterWhere("@ID", _id)
                        If doUpdate("ThongTinDuAn", "ID = @ID") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                            Exit Sub
                        End If
                    End If

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            CloseWaiting()
            Impersonator.EndImpersonation()
            gdvListFileCT.CloseEditor()
            gdvListFileCT.UpdateCurrentRow()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As Object, e As EventArgs) Handles btXoaFile.Click
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub

        If ShowCauHoi("Xoá file được chọn ?") Then
          
            Try
                Dim Impersonator As New Impersonator()
                Impersonator.BeginImpersonation()
                IO.File.Delete(UrlQuyTrinhKyThuat & _soycden & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                Impersonator.EndImpersonation()
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
            gdvListFileCT.DeleteSelectedRows()
            popupFileDinhKem.EditValue = ""
            For i = 0 To gdvListFileCT.RowCount - 1
                popupFileDinhKem.EditValue &= gdvListFileCT.GetRowCellValue(i, "File") & ";"
            Next

            AddParameter("@FileDinhKem", popupFileDinhKem.EditValue)
            AddParameterWhere("@ID", _id)
            If doUpdate("ThongTinDuAn", "ID = @ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

            ShowAlert("Xóa file thành công!")
        End If
    End Sub


    Private Sub popupFileDinhKem_Popup(sender As System.Object, e As System.EventArgs) Handles popupFileDinhKem.Popup
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

    Private Sub gdvListFileCT_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gdvListFileCT.ShowingEditor
        e.Cancel = True
    End Sub

    Private Sub btnIn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnIn.ItemClick
        Try

            Dim tg = GetServerTime()

            Dim sql As String = "SELECT KH.Ten AS TenKH, BYC.IDkhachhang as MaKH, NS1.Ten AS NguoiGD, NS1.Email, NS1.Mobile, NS1.Chucvu,KH.IDLinhVucSX, BYC.IDTakecare, KH.ttcDiachi as DiaChi, NS2.Ten AS TakeCare, TTDA.* FROM BANGYEUCAU BYC"
            sql &= " LEFT JOIN KHACHHANG KH ON KH.ID = BYC.IDkhachhang "
            sql &= " LEFT JOIN NHANSU NS1 ON NS1.ID = BYC.IDNgd "
            sql &= " LEFT JOIN NHANSU NS2 ON NS2.ID = BYC.IDtakecare"
            sql &= " INNER JOIN ThongTinDuAn TTDA ON TTDA.SoYCDen = BYC.sophieu WHERE TTDA.SoYCDen = @soycden "

            sql &= " SELECT ROW_NUMBER() OVER (ORDER BY ID) AS STT, * FROM ThongTinDuAnCT_NSKH WHERE IDThongTinDA = (SELECT ID FROM ThongTinDuAn TTDA WHERE SoYCDen = @soycden)"
            sql &= " SELECT ROW_NUMBER() OVER (ORDER BY ID) AS STT, * FROM ThongTinDACT_DoiThuCT WHERE IDThongTinDA = (SELECT ID FROM ThongTinDuAn TTDA WHERE SoYCDen = @soycden)"
            AddParameterWhere("@soycden", _soycden)
            Dim dt As DataSet = ExecuteSQLDataSet(sql)
            If dt Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            If dt.Tables(0).Rows.Count = 0 Then
                ShowThongBao("Chưa có phiếu thông tin dự án cho bản ghi này. Bạn không thể in")
                Exit Sub
            End If
            ShowWaiting("Đang tải nội dung ...")
            dt.Tables(1).TableName = "xTable"
            dt.Tables(2).TableName = "TableDTCT"
            Dim f As New frmIn("Xuất hóa đơn")
            Dim rpt As New rptPhieuThongTinDuAn


            rpt.lblTenKH.Text = dt.Tables(0).Rows(0)("TenKH").ToString
            rpt.lblDiaChi.Text = dt.Tables(0).Rows(0)("DiaChi").ToString
            rpt.lblNguoiGD.Text = dt.Tables(0).Rows(0)("NguoiGD").ToString
            rpt.lblSDTGD.Text = dt.Tables(0).Rows(0)("Mobile").ToString
            rpt.lblEmail.Text = dt.Tables(0).Rows(0)("Email").ToString
            rpt.lblChucVuGD.Text = dt.Tables(0).Rows(0)("Chucvu").ToString

            For i As Integer = 0 To dt.Tables(0).Rows.Count - 1
                Dim tb2 As DataTable = DataSourceDSFile(dt.Tables(0).Rows(i)("IDLinhVucSX").ToString)
                dt.Tables(0).Rows(i)("IDLinhVucSX") = ""
                For j As Integer = 0 To tb2.Rows.Count - 1
                    AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                    Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT NoiDung FROM tblTuDien WHERE Loai = 14 AND ID=@ID")
                    If Not tb3 Is Nothing Then
                        dt.Tables(0).Rows(i)("IDLinhVucSX") &= tb3.Rows(0)(0).ToString & "; "
                    End If
                Next
                dt.Tables(0).Rows(i)("IDLinhVucSX") = dt.Tables(0).Rows(i)("IDLinhVucSX").ToString.Trim
            Next
            rpt.lblLinhVucHD.Text = dt.Tables(0).Rows(0)("IDLinhVucSX").ToString
            rpt.lblSoYC.Text = _soycden

            Select Case dt.Tables(0).Rows(0)("KhanCap")
                Case 0
                    rpt.ckRatKhanCap.Checked = True
                Case 1
                    rpt.ckKhapCap.Checked = True
                Case 2
                    rpt.ckKhapCapBT.Checked = True
                Case 3
                    rpt.ckKhongKhapCap.Checked = True
            End Select

            Select Case dt.Tables(0).Rows(0)("QuanTrong")
                Case 0
                    rpt.ckRatQuanTrong.Checked = True
                Case 1
                    rpt.ckQuanTrong.Checked = True
                Case 2
                    rpt.ckQuanTrongBT.Checked = True
                Case 3
                    rpt.ckKhongQuanTrong.Checked = True
            End Select

            rpt.lblPhucTap.Text = dt.Tables(0).Rows(0)("PhucTap").ToString
            rpt.lblKhoiLuongCV.Text = dt.Tables(0).Rows(0)("KhoiLuongCV").ToString
            rpt.lblGiaTriDuAn.Text = dt.Tables(0).Rows(0)("GiaTriDA").ToString
            rpt.lblNgaythang.Text = "Ngày " & tg.Day & " tháng " & tg.Month & " năm " & tg.Year
            rpt.lblKD.Text = dt.Tables(0).Rows(0)("TakeCare").ToString
            rpt.lblsoyeucau.Text = _soycden


            ' rpt 2 

            rpt.lblNguonTT.Text = dt.Tables(0).Rows(0)("NguonTT").ToString
            rpt.lblLinhVucHD.Text = dt.Tables(0).Rows(0)("LinhVucHD").ToString
            rpt.lblSLNS.Text = dt.Tables(0).Rows(0)("SoNamHD").ToString & " ; " & dt.Tables(0).Rows(0)("SoLuongNS").ToString
            rpt.lblCacHangBA.Text = dt.Tables(0).Rows(0)("NhaPhanPhoi").ToString

            rpt.lblGiaCa.Text = dt.Tables(0).Rows(0)("GiaCa").ToString
            rpt.lblThaiDoPV.Text = dt.Tables(0).Rows(0)("ThaiDoPhucVu").ToString
            rpt.lblChatLuongSP.Text = dt.Tables(0).Rows(0)("ChatLuongSP").ToString
            rpt.lblNangLucKyThuat.Text = dt.Tables(0).Rows(0)("NangLucKT").ToString
            rpt.lblUyTin.Text = dt.Tables(0).Rows(0)("UyTin").ToString
            rpt.lblAnToanTC.Text = dt.Tables(0).Rows(0)("AnToanThiCong").ToString
            rpt.lblNguonTTDG.Text = dt.Tables(0).Rows(0)("NguonThongTin").ToString
            rpt.lblGhiChu.Text = dt.Tables(0).Rows(0)("GhiChu").ToString

            If Not IsDBNull(dt.Tables(0).Rows(0)("MucDichLL")) And Not dt.Tables(0).Rows(0)("MucDichLL") Is Nothing Then
                If dt.Tables(0).Rows(0)("MucDichLL") = "Hợp tác" Then
                    rpt.ckHopTac.Checked = True
                End If
                If dt.Tables(0).Rows(0)("MucDichLL") = "Lấy báo giá cạnh tranh" Then
                    rpt.ckLayGiaCT.Checked = True
                End If
                If dt.Tables(0).Rows(0)("MucDichLL") = "Dự toán tham khảo" Then
                    rpt.ckDuToanThamKhao.Checked = True
                End If
                If dt.Tables(0).Rows(0)("MucDichLL") = "Triển khai dự án" Then
                    rpt.ckTrienKhaiDuAn.Checked = True
                End If
                If dt.Tables(0).Rows(0)("MucDichLL") = "Nhu cầu công trình" Then
                    rpt.ckNhuCauCT.Checked = True
                End If
                If dt.Tables(0).Rows(0)("MucDichLL") = "Lấy SP cần tư vấn cài đặt" Then
                    rpt.ckTuVanCaiDat.Checked = True
                End If
                If dt.Tables(0).Rows(0)("MucDichLL") = "Tư vấn SP" Then
                    rpt.ckTuVanSP.Checked = True
                End If
                If dt.Tables(0).Rows(0)("MucDichLL") = "Bảo hành" Then
                    rpt.ckBaoHanh.Checked = True
                End If
                If dt.Tables(0).Rows(0)("MucDichLL") <> "Hợp tác" And dt.Tables(0).Rows(0)("MucDichLL") <> "Lấy báo giá cạnh tranh" And dt.Tables(0).Rows(0)("MucDichLL") <> "Dự toán tham khảo" And dt.Tables(0).Rows(0)("MucDichLL") <> "Triển khai dự án" And dt.Tables(0).Rows(0)("MucDichLL") <> "Nhu cầu công trình" And dt.Tables(0).Rows(0)("MucDichLL") <> "Lấy SP cần tư vấn cài đặt" And dt.Tables(0).Rows(0)("MucDichLL") <> "Tư vấn SP" And dt.Tables(0).Rows(0)("MucDichLL") <> "Bảo hành" Then
                    rpt.ckKhac.Checked = True
                    rpt.lblKhac.Text = dt.Tables(0).Rows(0)("MucDichLL")
                End If
            End If
            rpt.XrSubreport1.ReportSource.DataSource = dt
            rpt.XrSubreport2.ReportSource.DataSource = dt
            rpt.RequestParameters = False
            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(UrlQuyTrinhKyThuat & _soycden & "\" & e.CellValue, e.CellValue)
        End If
    End Sub
End Class