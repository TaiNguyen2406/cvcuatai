Imports BACSOFT.Db.SqlHelper

Public Class frmCNNguoiGiaoDich
    Private Sub frmCNNguoiGiaoDich_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadTakeCare()
        loadNoiCongTac()
        cbDoiTuongNhanEmail.Properties.DataSource = getDataCbDoiTuongNhanEmail()
        cbDoiTuongNhanEmail.EditValue = Convert.ToByte(4)
        If TrangThai.isAddNew Then
            Me.Text = "Thêm người giao dịch"
            chkConLamViec.Checked = True
        Else
            AddParameterWhere("@ID", MaTuDien)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT * FROM NHANSU WHERE ID=@ID")
            If Not dt Is Nothing Then
                tbTenNgd.EditValue = dt.Rows(0)("Ten")
                tbNgaySinh.EditValue = dt.Rows(0)("Ngaysinh")
                tbCMND.EditValue = dt.Rows(0)("SoCMT")
                tbNgayCap.EditValue = dt.Rows(0)("Ngaycap")
                tbNoiCap.EditValue = dt.Rows(0)("Noicap")
                tbNguyenQuan.EditValue = dt.Rows(0)("Nguyenquan")
                tbDiaChi.EditValue = dt.Rows(0)("Diachi")
                tbDTNhaRieng.EditValue = dt.Rows(0)("DienthoaiNR")
                tbDienThoaiCQ.EditValue = dt.Rows(0)("DienthoaiCQ")
                tbFax.EditValue = dt.Rows(0)("Fax")
                tbDiDong1.EditValue = dt.Rows(0)("Mobile")
                tbDiDong2.EditValue = dt.Rows(0)("Mobile1")
                tbTKNganHang.EditValue = dt.Rows(0)("Taikhoan")
                tbNganHang.EditValue = dt.Rows(0)("Nganhang")
                tbChucVu.EditValue = dt.Rows(0)("Chucvu")
                chkConLamViec.Checked = dt.Rows(0)("Trangthai")
                txtXungHo.EditValue = dt.Rows(0)("XungHo")
                If Not IsDBNull(dt.Rows(0)("Chamsoc")) Then
                    cbTakeCare.EditValue = dt.Rows(0)("Chamsoc")
                End If
                If Not IsDBNull(dt.Rows(0)("Noictac")) Then
                    cbNoiCongTac.EditValue = dt.Rows(0)("Noictac")
                End If
                cbDoiTuongNhanEmail.EditValue = dt.Rows(0)("DoiTuongNhanEmail")
                tbEmail.EditValue = dt.Rows(0)("Email")
                Me.Text = "Cập nhật thông tin người giao dịch " & dt.Rows(0)("Ten")
                If Not IsDBNull(dt.Rows(0)("Chamsoc")) Then
                    If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) = False And KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) = False Then
                        cbTakeCare.Properties.ReadOnly = True
                    End If
                End If

               
            End If
        End If

        tbTenNgd.Focus()
    End Sub

    Function getDataCbDoiTuongNhanEmail() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Byte"))
        tb.Columns.Add("Ten", Type.GetType("System.String"))
        Dim r1 As DataRow
        r1 = tb.NewRow
        r1("ID") = 0
        r1("Ten") = "Chính hãng"
        Dim r2 As DataRow
        r2 = tb.NewRow
        r2("ID") = 1
        r2("Ten") = "Đăng ký không làm phiền"
        Dim r3 As DataRow
        r3 = tb.NewRow
        r3("ID") = 2
        r3("Ten") = "End user - Kỹ thuật"
        Dim r4 As DataRow
        r4 = tb.NewRow
        r4("ID") = 3
        r4("Ten") = "End user - Mua hàng, Lãnh đạo"
        Dim r5 As DataRow
        r5 = tb.NewRow
        r5("ID") = 4
        r5("Ten") = "Thương mại"
        Dim r6 As DataRow
        r6 = tb.NewRow
        r6("ID") = 5
        r6("Ten") = "Trường học"
        Dim r7 As DataRow
        r7 = tb.NewRow
        r7("ID") = 5
        r7("Ten") = "Email lỗi"
        tb.Rows.Add(r1)
        tb.Rows.Add(r2)
        tb.Rows.Add(r3)
        tb.Rows.Add(r4)
        tb.Rows.Add(r5)
        tb.Rows.Add(r6)
        tb.Rows.Add(r7)
        Return tb
    End Function

    Public Sub loadTakeCare()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not tb Is Nothing Then
            cbTakeCare.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadNoiCongTac()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG Where ID <> 74 ORDER BY ttcMa")
        If Not tb Is Nothing Then
            cbNoiCongTac.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btThem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btThem.Click
        If TrangThai.isAddNew Then
            If cbTakeCare.EditValue Is Nothing Or cbNoiCongTac.EditValue Is Nothing Then
                ShowCanhBao("Cần điền đủ thông tin nơi công tác và người chăm sóc")
                Exit Sub
            End If
        End If
        GhiLai()
        TrangThai.isAddNew = True
        tbTenNgd.EditValue = ""
        tbNgaySinh.EditValue = DBNull.Value
        tbCMND.EditValue = ""
        tbNgayCap.EditValue = DBNull.Value
        tbNoiCap.EditValue = ""
        tbNguyenQuan.EditValue = ""
        tbDiaChi.EditValue = ""
        tbDTNhaRieng.EditValue = ""
        tbDienThoaiCQ.EditValue = ""
        tbFax.EditValue = ""
        tbDiDong1.EditValue = ""
        tbDiDong2.EditValue = ""
        tbTKNganHang.EditValue = ""
        tbNganHang.EditValue = ""
        tbEmail.EditValue = ""
        tbChucVu.EditValue = ""
        chkConLamViec.Checked = True
        cbTakeCare.EditValue = Convert.ToInt32(TaiKhoan)

    End Sub

    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click
        If TrangThai.isAddNew Then
            If cbTakeCare.EditValue Is Nothing Or cbNoiCongTac.EditValue Is Nothing Then
                ShowCanhBao("Cần điền đủ thông tin nơi công tác và người chăm sóc")
                Exit Sub
            End If
        End If
        GhiLai()
        Me.Close()
    End Sub

    Private Sub GhiLai()
        If tbTenNgd.EditValue = "" Then
            ShowCanhBao("Chưa có tên người giao dịch !")
            tbTenNgd.Focus()
            Exit Sub
        End If

        AddParameter("@Ten", tbTenNgd.EditValue)
        AddParameter("@Ngaysinh", tbNgaySinh.EditValue)
        AddParameter("@SoCMT", tbCMND.EditValue)
        AddParameter("@Ngaycap", tbNgayCap.EditValue)
        AddParameter("@Noicap", tbNoiCap.EditValue)
        AddParameter("@Nguyenquan", tbNguyenQuan.EditValue)
        AddParameter("@Diachi", tbDiaChi.EditValue)
        AddParameter("@DienthoaiNR", tbDTNhaRieng.EditValue)
        AddParameter("@DienthoaiCQ", tbDienThoaiCQ.EditValue)
        AddParameter("@Fax", tbFax.EditValue)
        AddParameter("@Mobile", tbDiDong1.EditValue)
        AddParameter("@Mobile1", tbDiDong2.EditValue)
        AddParameter("@Taikhoan", tbTKNganHang.EditValue)
        AddParameter("@Nganhang", tbNganHang.EditValue)
        AddParameter("@Chamsoc", cbTakeCare.EditValue)
        AddParameter("@Noictac", cbNoiCongTac.EditValue)
        AddParameter("@Email", tbEmail.EditValue)
        AddParameter("@Chucvu", tbChucVu.EditValue)
        AddParameter("@Trangthai", chkConLamViec.Checked)
        AddParameter("@XungHo", txtXungHo.EditValue)
        If Not IsDBNull(cbDoiTuongNhanEmail.EditValue) Then
            AddParameter("@DoiTuongNhanEmail", cbDoiTuongNhanEmail.EditValue)
        Else
            AddParameter("@DoiTuongNhanEmail", 4)
        End If

        Try
            If TrangThai.isAddNew Then
                AddParameter("@Nhaplieu", Convert.ToInt32(TaiKhoan))
                MaTuDien = doInsert("NHANSU")
                If MaTuDien Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@ID", MaTuDien)
                If doUpdate("NHANSU", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mDSKhachHang" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKhachHang).loadDS()
                ElseIf deskTop.tabMain.TabPages(i).Tag = "mDSNguoiGiaoDich" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmNguoiGiaoDich).loadDS()
                End If
            Next

            ShowAlert(Me.Text & " thành công !")

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btDong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDong.Click

        Me.Close()
    End Sub

    Private Sub cbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTakeCare.ButtonClick
        cbTakeCare.EditValue = Nothing
    End Sub
End Class