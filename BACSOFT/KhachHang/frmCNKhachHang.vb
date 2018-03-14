Imports BACSOFT.Db.SqlHelper
Imports System.Text.RegularExpressions
Public Class frmCNKhachHang

    Public _IdTakeCare As Object
    Public _TAG_QUYEN As String
    Private _MST As String
    Private Sub frmCNNguoiGiaoDich_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        loadTuDien()
        loadTakecare()
        loadHinhThucChungTu()
        cbNhomKH.Properties.DataSource = DataTableNhomKH()
        cbCapKH.Properties.DataSource = DataTableCapKH()
        Dim tb As New DataTable
        tb.Columns.Add("IDLinhVuc", Type.GetType("System.Int32"))
        gdvLinhVucSX.DataSource = tb

        If TrangThai.isAddNew Then
            Me.Text = "Thêm khách hàng mới"
        Else
            AddParameterWhere("@ID", MaTuDien)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT * FROM KHACHHANG WHERE ID=@ID")
            If Not dt Is Nothing Then
                tbMaKH.EditValue = dt.Rows(0)("ttcMa")
                tbTenKH.EditValue = dt.Rows(0)("Ten")
                tbDiaChiVP.EditValue = dt.Rows(0)("ttcDiachi")
                tbDiaChiTruSo.EditValue = dt.Rows(0)("ttcDiachiTs")
                tbDiaChiGH.EditValue = dt.Rows(0)("ttcDCGiaoHang")
                tbDienThoai.EditValue = dt.Rows(0)("ttcDienthoai")
                tbFaxKH.EditValue = dt.Rows(0)("ttcFax")
                tbWeb.EditValue = dt.Rows(0)("ttcWeb")
                tbEmail.EditValue = dt.Rows(0)("ttcEmail")
                _MST = dt.Rows(0)("ttcMasothue")
                tbMST.EditValue = dt.Rows(0)("ttcMasothue")
                tbTaiKhoanNH.EditValue = dt.Rows(0)("ttcTaikhoan")
                tbNoiMo.EditValue = dt.Rows(0)("ttcNoimo")
                tbNgayTL.EditValue = dt.Rows(0)("ttcNgaythanhlap")
                cbThanhPho.EditValue = dt.Rows(0)("IDTinhThanh")
                cbKhuCN.EditValue = dt.Rows(0)("IDKhuCN")
                gdvLinhVucSX.DataSource = DataSourceDSFile(dt.Rows(0)("IDLinhVucSX").ToString, "IDLinhVuc")
                tbNangLucKH.EditValue = dt.Rows(0)("DanhGia")
                tbKhaiThacDonHang.EditValue = dt.Rows(0)("KhaiThacDonHang")
                cbTakecare.EditValue = dt.Rows(0)("IDTakecare")

                If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) _
                    And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) Then
                    If dt.Rows(0)("IDTakecare") Is DBNull.Value Then
                        cbTakecare.Enabled = False
                    End If
                    cbHinhThucTT.Enabled = False
                    cbHinhThucTT2.Enabled = False
                End If

                _IdTakeCare = dt.Rows(0)("IDTakecare")
                cbHinhThucTT.EditValue = dt.Rows(0)("IDHinhThucTT")
                gdvFile.DataSource = DataSourceDSFile(dt.Rows(0)("FileDinhKem").ToString)
                cbLoaiHinhDN.EditValue = dt.Rows(0)("IDLoaiHinhDN")
                cbChuSH.EditValue = dt.Rows(0)("IDChuSoHuu")
                cbTinhTrang.EditValue = dt.Rows(0)("IDTinhTrang")
                'Tai
                cbHinhThucTT2.EditValue = dt.Rows(0)("IDHinhThucTT2")
                If Not dt.Rows(0)("HinhThucChungTu") Is DBNull.Value Then
                    cmbHTCT.EditValue = Convert.ToInt32(dt.Rows(0)("HinhThucChungTu"))
                Else
                    cmbHTCT.EditValue = DBNull.Value
                End If
                tbTenKH_ENG.EditValue = dt.Rows(0)("TenENG")
                tbDiaChiHQ.EditValue = dt.Rows(0)("ttcDiachiHQ")
                mePTTT.EditValue = dt.Rows(0)("PhuongThucThanhToanHQ")
                'Tai
                If Not IsDBNull(dt.Rows(0)("NhomKH")) Then
                    cbNhomKH.EditValue = CType(dt.Rows(0)("NhomKH"), Integer)
                End If
                If Not IsDBNull(dt.Rows(0)("CapKH")) Then
                    cbCapKH.EditValue = CType(dt.Rows(0)("CapKH"), Integer)
                End If


                Select Case Convert.ToInt16(dt.Rows(0)("ttcKhachhang"))
                    Case 0
                        chkKhachHang.Checked = False
                        chkNhaCungCap.Checked = True
                        chkDVVC.Checked = False
                    Case 1
                        chkKhachHang.Checked = True
                        chkNhaCungCap.Checked = False
                        chkDVVC.Checked = False
                    Case 2
                        chkKhachHang.Checked = True
                        chkNhaCungCap.Checked = True
                        chkDVVC.Checked = False
                    Case Else
                        chkKhachHang.Checked = False
                        chkNhaCungCap.Checked = False
                        chkDVVC.Checked = True
                End Select

                If Not IsDBNull(cbTakecare.EditValue) Then
                    If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) = False And KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) = False Then
                        cbTakecare.Properties.ReadOnly = True
                    End If
                End If

                If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                    tbMaKH.Enabled = False
                End If


                Me.Text = "Cập nhật thông tin khách hàng " & dt.Rows(0)("Ten")
            End If
        End If
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbLoaiHinhDN.Properties.Buttons(1).Visible = False
            cbChuSH.Properties.Buttons(1).Visible = False
            cbKhuCN.Properties.Buttons(1).Visible = False
            cbThanhPho.Properties.Buttons(1).Visible = False
            cbTinhTrang.Properties.Buttons(1).Visible = False
            rgdvLinhVucSX.Buttons(1).Visible = False
        End If

    End Sub

    Private Sub loadTakecare()
        Dim sql As String = ""
        sql &= " SELECT ID,HinhThucTT_VIE FROM tblHinhThucTTKH ORDER BY HinhThucTT_VIE "
        sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        'Tai
        sql &= " SELECT * FROM DM_HINH_THUC_TT where TrangThai=1 ORDER BY Nhom asc, GiaiThich asc "
        'Tai
        riLueNhom.DataSource = TAI.tableNhomHinhThucTT()
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbHinhThucTT.Properties.DataSource = ds.Tables(0)
            cbTakecare.Properties.DataSource = ds.Tables(1)
            cbHinhThucTT2.Properties.DataSource = ds.Tables(2)
            If ds.Tables(0).Rows.Count > 0 Then
                cbHinhThucTT.EditValue = ds.Tables(0).Rows(0)("ID")
            End If
            'Tai
            If ds.Tables(2).Rows.Count > 0 Then
                cbHinhThucTT2.EditValue = ds.Tables(2).Rows(0)("ID")
            End If
            'Tai
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub loadHinhThucChungTu()
        cmbHTCT.Properties.DataSource = HinhThucChungTu.GetDataTable
    End Sub

    Public Sub loadTuDien()
        AddParameterWhere("@TinhThanh", LoaiTuDien.TinhThanh)
        AddParameterWhere("@KhuCN", LoaiTuDien.KhuCN)
        AddParameterWhere("@LinhVuc", LoaiTuDien.LinhVucSX)
        AddParameterWhere("@LoaiDN", LoaiTuDien.LoaiHinhDN)
        AddParameterWhere("@LoaiHinhCSH", LoaiTuDien.LoaiHinhChuSoHuu)
        AddParameterWhere("@TinhTrangKH", LoaiTuDien.TinhTrangKH)
        Dim sql As String = ""
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@TinhThanh ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@KhuCN ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LinhVuc ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LoaiDN ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LoaiHinhCSH ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@TinhTrangKH ORDER BY ID "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbThanhPho.Properties.DataSource = ds.Tables(0)
            cbKhuCN.Properties.DataSource = ds.Tables(1)
            rgdvLinhVucSX.DataSource = ds.Tables(2)
            cbLoaiHinhDN.Properties.DataSource = ds.Tables(3)
            cbChuSH.Properties.DataSource = ds.Tables(4)
            cbTinhTrang.Properties.DataSource = ds.Tables(5)
            rgdvLinhVucSX.View.Columns.Clear()
            With rgdvLinhVucSX.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "Mã"
                colID.Visible = False
                Dim colTen = .AddField("NoiDung")
                colTen.Caption = "Lĩnh vực SX, KD"
                colTen.VisibleIndex = 0
                'colTen.Width = 200
                colTen.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btLuuVaDong.Click
        If GhiLai() Then
            Me.Close()
        End If

    End Sub

    Function GhiLai() As Boolean
        If tbMaKH.EditValue.ToString.Trim = "" Or tbTenKH.EditValue.ToString.Trim = "" Or IsDBNull(cmbHTCT.EditValue) Then
            ShowCanhBao("Điền đầy đủ thông tin vào các mục dấu có dấu  (*)")
            Return False
        End If
        If TrangThai.isAddNew Then
            If cbTakecare.EditValue Is Nothing Then
                ShowCanhBao("Chưa có thông tin người chăm sóc !")
                Return False
            End If

            If tbMST.EditValue.ToString() = "" Then
                ShowCanhBao("Chưa có thông tin MST !")
                Return False
            Else
                Dim regexItem = New Regex("^[0-9\-]*$")
                Dim mst As String = tbMST.EditValue
                If Not regexItem.IsMatch(mst) Then
                    ShowCanhBao("Mã số thuế chỉ được nhập số !")
                    Return False
                End If
                Dim str As String = checkMST(tbMST.EditValue)
                If str <> "" Then
                    ShowCanhBao("Khách hàng với MST này đã tồn tại: " & str)
                    Return False

                End If
                Dim mang As Char() = tbMST.EditValue.ToString().ToCharArray()
                For i = 0 To mang.Length - 1
                    If mang(i) = "-" And i <> 10 Then
                        ShowCanhBao("Chỉ được nhập 1 dấu - sau 10 ký tự mã số thuế")
                        Return False

                    End If
                Next

            End If
            If checkMaKH() Then
                ShowCanhBao("Mã khách hàng này đã tồn tại ")
                Return False
            End If
        Else
            Dim regexItem = New Regex("^[0-9\-]*$")
            Dim mst As String = tbMST.EditValue
            If Not regexItem.IsMatch(mst) Then
                ShowCanhBao("Mã số thuế chỉ được nhập số !")
                Return False
            End If
            If CType(tbMST.EditValue, String).Replace(" ", "").Replace("_", "") <> _MST.Replace(" ", "").Replace("_", "") Then

                Dim str As String = checkMST(tbMST.EditValue)
                If str <> "" Then
                    ShowCanhBao("Khách hàng với MST này đã tồn tại: " & str)
                    Return False
                End If
                Dim mang As Char() = tbMST.EditValue.ToString().ToCharArray()
                For i = 0 To mang.Length - 1
                    If mang(i) = "-" And i <> 10 Then
                        ShowCanhBao("Chỉ được nhập 1 dấu - sau 10 ký tự mã số thuế")
                        Return False

                    End If
                Next
            End If
        End If
        If Not chkKhachHang.Checked And Not chkNhaCungCap.Checked And Not chkDVVC.Checked Then
            ShowCanhBao("Bạn phải chọn loại khách hàng trước khi ghi lại !")
            Return False
        End If
        gdvLinhVucSXCT.CloseEditor()
        gdvLinhVucSXCT.UpdateCurrentRow()
        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()
        Dim tg As DateTime = GetServerTime()
        Try

            AddParameter("@ttcMa", tbMaKH.EditValue.ToString.Trim)
            AddParameter("@Ten", tbTenKH.EditValue)

            AddParameter("@ttcDiachi", tbDiaChiVP.EditValue)
            AddParameter("@ttcDiachiTs", tbDiaChiTruSo.EditValue)
            AddParameter("@ttcDCGiaoHang", tbDiaChiGH.EditValue)
            AddParameter("@ttcDienthoai", tbDienThoai.EditValue)
            AddParameter("@ttcFax", tbFaxKH.EditValue)
            AddParameter("@ttcWeb", tbWeb.EditValue)
            AddParameter("@ttcEmail", tbEmail.EditValue)
            AddParameter("@ttcMasothue", tbMST.EditValue)
            AddParameter("@ttcTaikhoan", tbTaiKhoanNH.EditValue)
            AddParameter("@ttcNoimo", tbNoiMo.EditValue)
            AddParameter("@ttcNgaythanhlap", tbNgayTL.EditValue)
            AddParameter("@IDKhuCN", cbKhuCN.EditValue)
            AddParameter("@IDTinhThanh", cbThanhPho.EditValue)
            AddParameter("@IDTakecare", cbTakecare.EditValue)
            AddParameter("@IDHinhThucTT", cbHinhThucTT.EditValue)
            'Tai
            AddParameter("@IDHinhThucTT2", cbHinhThucTT2.EditValue)
            AddParameter("@HinhThucChungTu", cmbHTCT.EditValue)
            AddParameter("@TenENG", tbTenKH_ENG.EditValue)
            AddParameter("@ttcDiachiHQ", tbDiaChiHQ.EditValue)
            AddParameter("@PhuongThucThanhToanHQ", mePTTT.EditValue)
            'Tai
            AddParameter("@FileDinhKem", StrDSFile(gdvFileCT))
            AddParameter("@IDLinhVucSX", StrDSFile(gdvLinhVucSXCT, "IDLinhVuc"))
            AddParameter("@DanhGia", tbNangLucKH.EditValue)
            AddParameter("@KhaiThacDonHang", tbKhaiThacDonHang.EditValue)
            AddParameter("@IDLoaiHinhDN", cbLoaiHinhDN.EditValue)
            AddParameter("@IDChuSoHuu", cbChuSH.EditValue)
            AddParameter("@IDTinhTrang", cbTinhTrang.EditValue)
            AddParameter("@NhomKH", cbNhomKH.EditValue)
            AddParameter("@CapKH", cbCapKH.EditValue)
            If chkDVVC.Checked Then
                AddParameter("@ttcKhachhang", 3)

            Else
                If chkKhachHang.Checked And chkNhaCungCap.Checked Then
                    AddParameter("@ttcKhachhang", 2)
                ElseIf chkKhachHang.Checked And chkNhaCungCap.Checked = False Then
                    AddParameter("@ttcKhachhang", 1)
                ElseIf chkKhachHang.Checked = False And chkNhaCungCap.Checked Then
                    AddParameter("@ttcKhachhang", 0)
                Else
                    AddParameter("@ttcKhachhang", 2)
                End If
            End If



            If TrangThai.isAddNew Then
                AddParameter("@Ngay", tg)
                AddParameter("@IDUser", Convert.ToInt32(TaiKhoan))
                MaTuDien = doInsert("KHACHHANG")
                If MaTuDien Is Nothing Then Throw New Exception(LoiNgoaiLe)

            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@ID", MaTuDien)
                If doUpdate("KHACHHANG", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            TrangThai.isUpdate = True

            If IsDBNull(_IdTakeCare) And Not cbTakecare.EditValue Is Nothing Then
                AddParameter("@ThoiGian", tg)
                AddParameter("@IDTakeCare", cbTakecare.EditValue)
                AddParameter("@NoiDungGiaoDich", "Nhận khách hàng từ danh sách chưa ai chăm sóc")
                AddParameter("@ChuyenGiao", False)
                AddParameter("@IDKH", MaTuDien)
                If doInsert("GIAODICHKH") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                _IdTakeCare = cbTakecare.EditValue
            ElseIf Not IsDBNull(_IdTakeCare) And cbTakecare.EditValue Is Nothing Then
                AddParameter("@ThoiGian", tg)
                AddParameter("@IDTakeCare", _IdTakeCare)
                AddParameter("@NoiDungGiaoDich", "Bỏ chăm sóc khách hàng")
                AddParameter("@ChuyenGiao", False)
                AddParameter("@IDKH", MaTuDien)
                If doInsert("GIAODICHKH") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If


            'Load lại danh sách khách hàng
            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mDSKhachHang" Then
                    'CType(deskTop.tabMain.TabPages(i).Controls(0), frmKhachHang).loadDS()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKhachHang).LoadDSById(MaTuDien)
                End If
            Next

            ShowAlert(Me.Text & " thành công !")
            Return True
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            Return False
        End Try
    End Function

    Private Sub mThemFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThem.ItemClick
        If tbMaKH.EditValue.ToString.Trim = "" Then
            ShowCanhBao("Chưa có mã khách hàng")
            Exit Sub
        End If
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")

            If Not System.IO.Directory.Exists(UrlKhachHang & tbMaKH.EditValue) Then
                System.IO.Directory.CreateDirectory(UrlKhachHang & tbMaKH.EditValue)
            End If
            Dim path As String = ""
            For Each file In openFile.FileNames
                Try
                    path = TaiKhoan.ToString & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(UrlKhachHang & tbMaKH.EditValue & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, UrlKhachHang & tbMaKH.EditValue & "\" & path, True)
                            gdvFileCT.AddNewRow()
                            gdvFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        System.IO.File.Copy(file, UrlKhachHang & tbMaKH.EditValue & "\" & path)
                        gdvFileCT.AddNewRow()
                        gdvFileCT.SetFocusedRowCellValue("File", path)
                    End If

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            CloseWaiting()
            gdvFileCT.CloseEditor()
            gdvFileCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub mXoaFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoa.ItemClick
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        Try
            If ShowCauHoi("Xoá file được chọn ?") Then
                System.IO.File.Delete(UrlKhachHang & tbMaKH.EditValue & "\" & gdvFileCT.GetFocusedRowCellValue("File"))
                gdvFileCT.DeleteSelectedRows()
            End If

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub gdvFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(UrlKhachHang & tbMaKH.EditValue & "\" & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub btDong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub cbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTakecare.ButtonClick
        If e.Button.Index = 1 Then
            cbTakecare.EditValue = Nothing
        End If
    End Sub

    Private Sub cbThanhPho_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbThanhPho.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNThongTinPhuKH
            f.Tag = Me.Tag
            f.XtraTabControl1.SelectedTabPageIndex = 0
            f.ShowDialog()
        ElseIf e.Button.Index = 2 Then
            cbThanhPho.EditValue = Nothing
        End If
    End Sub

    Private Sub cbLoaiHinhDN_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbLoaiHinhDN.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNThongTinPhuKH
            f.Tag = Me.Tag
            f.XtraTabControl1.SelectedTabPageIndex = 2
            f.ShowDialog()
        ElseIf e.Button.Index = 2 Then
            cbLoaiHinhDN.EditValue = Nothing
        End If
    End Sub

    Private Sub cbKhuCN_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbKhuCN.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNThongTinPhuKH
            f.Tag = Me.Tag
            f.XtraTabControl1.SelectedTabPageIndex = 0
            f.ShowDialog()
        ElseIf e.Button.Index = 2 Then
            cbKhuCN.EditValue = Nothing
        End If
    End Sub

    Private Sub cbChuSH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbChuSH.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNThongTinPhuKH
            f.Tag = Me.Tag
            f.XtraTabControl1.SelectedTabPageIndex = 2
            f.ShowDialog()
        ElseIf e.Button.Index = 2 Then
            cbChuSH.EditValue = Nothing
        End If
    End Sub

    Private Sub cbTinhTrang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTinhTrang.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNThongTinPhuKH
            f.Tag = Me.Tag
            f.XtraTabControl1.SelectedTabPageIndex = 3
            f.ShowDialog()
        ElseIf e.Button.Index = 2 Then
            cbTinhTrang.EditValue = Nothing
        End If
    End Sub

    Private Sub rgdvLinhVucSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rgdvLinhVucSX.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNThongTinPhuKH
            f.Tag = Me.Tag
            f.XtraTabControl1.SelectedTabPageIndex = 1
            f.ShowDialog()
        End If
    End Sub

    Private Sub gdvLinhVucSXCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvLinhVucSXCT.KeyDown
        If gdvLinhVucSXCT.FocusedRowHandle < 0 Then Exit Sub
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa dòng được chọn ?") Then
                gdvLinhVucSXCT.DeleteSelectedRows()
                gdvLinhVucSXCT.CloseEditor()
                gdvLinhVucSXCT.UpdateCurrentRow()
            End If
        End If
    End Sub

    Private Sub btThemMoi_Click(sender As System.Object, e As System.EventArgs) Handles btThemMoi.Click

        TrangThai.isAddNew = True
        tbMaKH.EditValue = ""
        tbTenKH.EditValue = ""
        tbDiaChiVP.EditValue = ""
        tbDiaChiTruSo.EditValue = ""
        tbDienThoai.EditValue = ""
        tbDiaChiGH.EditValue = ""
        tbFaxKH.EditValue = ""
        tbWeb.EditValue = ""
        tbEmail.EditValue = ""
        tbMST.EditValue = ""
        tbTaiKhoanNH.EditValue = ""
        tbNoiMo.EditValue = ""
        tbNgayTL.EditValue = ""
        cbKhuCN.EditValue = Nothing
        cbChuSH.EditValue = Nothing
        cbLoaiHinhDN.EditValue = Nothing
        cbThanhPho.EditValue = Nothing
        cbTinhTrang.EditValue = Nothing

        tbKhaiThacDonHang.EditValue = ""
        tbNangLucKH.EditValue = ""

        Dim tb As New DataTable
        tb.Columns.Add("IDLinhVuc", Type.GetType("System.Int32"))
        gdvLinhVucSX.DataSource = tb
        While gdvFileCT.RowCount > 0
            gdvFileCT.DeleteRow(0)
        End While
    End Sub

    Public Function checkMST(ByVal _MST As String) As String
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT (ISNULL(ttcMa,'')  + ' Takcare: ' + ISNULL((SELECT Ten FROM NHANSU WHERE ID=KHACHHANG.IDTakeCare),N'<Blank>'))KH FROM KHACHHANG WHERE replace(replace(ttcMaSoThue,' ',''),'-','')=N'" & Replace(_MST, "-", "").Trim & "'")
        If tb Is Nothing Then
            ShowBaoLoi("Lỗi KT MST: " & LoiNgoaiLe)
            Return ""
        Else
            If tb.Rows.Count > 0 Then
                Return tb.Rows(0)(0).ToString
            Else
                Return ""
            End If
        End If

    End Function

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        GhiLai()
    End Sub

    Private Sub tbMST_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbMST.EditValueChanged
        If TrangThai.isAddNew Then
            Dim str As String = checkMST(tbMST.EditValue)
            If str <> "" Then
                ShowCanhBao("MST đã tồn tại : " & str)
            End If
        Else
            If tbMST.EditValue <> _MST Then
                Dim str As String = checkMST(tbMST.EditValue)
                If str <> "" Then
                    ShowCanhBao("MST đã tồn tại : " & str)
                End If
            End If
        End If
     
    
    End Sub

    Function checkMaKH() As Boolean
        If TrangThai.isAddNew Then
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT ISNULL(ttcMa,'') FROM KHACHHANG WHERE RTrim(LTRIM(UPPER(ttcMa)))= N'" & tbMaKH.EditValue.ToString.Replace(" ", "").ToUpper() & "'")
            If tb Is Nothing Then
                ShowBaoLoi("Lỗi KT Mã KH: " & LoiNgoaiLe)
                Return True
            Else
                If tb.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Else
            Return False
        End If

    End Function

    Private Sub chkDVVC_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDVVC.CheckedChanged
        If chkDVVC.Checked Then
            chkKhachHang.Checked = False
            chkNhaCungCap.Checked = False
        End If
    End Sub

    Private Sub cmbHTCT_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmbHTCT.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbHTCT.EditValue = DBNull.Value
        End If
    End Sub
End Class