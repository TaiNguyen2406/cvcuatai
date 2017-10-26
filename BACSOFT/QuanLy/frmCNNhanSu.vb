Imports BACSOFT.Db.SqlHelper

Public Class frmCNNhanSu

    Private Sub frmCNNhanSu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadDatacmb()
        loadDSQuyen()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.BanQuanTri) Then
            cbQuyen.Properties.ReadOnly = True
        End If


        If TrangThai.isAddNew Then
            Me.Text = "Thêm người giao dịch"
            gdvFile.DataSource = DataSourceDSFile()
        Else
            AddParameterWhere("@ID", MaTuDien)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT * FROM NHANSU WHERE ID=@ID")
            If Not dt Is Nothing Then
                tbTen.EditValue = dt.Rows(0)("Ten")
                tbNgaySinh.EditValue = dt.Rows(0)("Ngaysinh")
                tbCMND.EditValue = dt.Rows(0)("SoCMT")
                tbNgayCap.EditValue = dt.Rows(0)("Ngaycap")
                tbNoiCap.EditValue = dt.Rows(0)("Noicap")
                tbNguyenQuan.EditValue = dt.Rows(0)("Nguyenquan")
                tbDiaChi.EditValue = dt.Rows(0)("Diachi")
                tbDTNhaRieng.EditValue = dt.Rows(0)("DienthoaiNR")
                tbDienThoaiCQ.EditValue = dt.Rows(0)("DienthoaiCQ")
                tbDiDong1.EditValue = dt.Rows(0)("Mobile")
                tbDiDong2.EditValue = dt.Rows(0)("Mobile1")
                tbTKNganHang.EditValue = dt.Rows(0)("Taikhoan")
                tbNganHang.EditValue = dt.Rows(0)("Nganhang")
                cbTakeCare.EditValue = dt.Rows(0)("Chamsoc")
                tbEmail.EditValue = dt.Rows(0)("Email")
                tbNgayVaoCTy.EditValue = dt.Rows(0)("Ngayvaocty")
                tbNgayRoiCty.EditValue = dt.Rows(0)("Ngayroicty")
                tbHocVan.EditValue = dt.Rows(0)("Hocvan")
                tbChuyenMon.EditValue = dt.Rows(0)("Chuyenmon")
                tbNgoaiNgu.EditValue = dt.Rows(0)("Ngoaingu")
                tbSoThich.EditValue = dt.Rows(0)("Sothich")
                tbChucVu.EditValue = dt.Rows(0)("Chucvu")
                tbNgheDaLam.EditValue = dt.Rows(0)("Nganhnghedalam")
                tbKhenThuong.EditValue = dt.Rows(0)("Khenthuongkyluat")
                cbTakeCare.EditValue = dt.Rows(0)("Chamsoc")
                cbPhongBan.EditValue = dt.Rows(0)("IDDepatment")
                chkNVMoi.EditValue = dt.Rows(0)("Moi")
                chkConLamViec.EditValue = dt.Rows(0)("Trangthai")
                cbQuyen.EditValue = dt.Rows(0)("Matruycap").ToString.Trim
                If Not IsDBNull(dt.Rows(0)("GioiTinh")) Then
                    chkNam.Checked = dt.Rows(0)("GioiTinh")
                    chkNu.Checked = Not dt.Rows(0)("GioiTinh")
                End If
                cbBoPhan.EditValue = dt.Rows(0)("IDBoPhan")
                If dt.Rows(0)("HinhAnh").ToString <> "" Then
                    tbHinhAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhNhanSu & dt.Rows(0)("HinhAnh").ToString)
                    tbHinhAnh.Tag = dt.Rows(0)("HinhAnh").ToString
                End If

                gdvFile.DataSource = DataSourceDSFile(dt.Rows(0)("FileDinhKem").ToString)
                Me.Text = "Cập nhật thông tin: " & dt.Rows(0)("Ten")
            End If
        End If

        tbTen.Focus()
    End Sub

    Public Sub loadDSQuyen()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT MaTruyCap FROM QUYENTRUYCAP order by MaTruyCap")
        If Not tb Is Nothing Then
            cbQuyen.Properties.DataSource = tb
            cbQuyen.EditValue = "none"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDatacmb()

        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 and Trangthai = 1 and IDDepatment <> 7 order by Ten SELECT ID,Ten FROM DEPATMENT order by Ten SELECT Ma,Ten FROM NhanSu_BoPhan ORDER BY STT")
        If Not ds Is Nothing Then
            cbTakeCare.Properties.DataSource = ds.Tables(0)
            cbPhongBan.Properties.DataSource = ds.Tables(1)
            cbBoPhan.Properties.DataSource = ds.Tables(2)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub btThem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btThem.Click
        GhiLai()
        TrangThai.isAddNew = True
        tbTen.EditValue = ""
        tbNgaySinh.EditValue = DBNull.Value
        tbCMND.EditValue = ""
        tbNgayCap.EditValue = DBNull.Value
        tbNoiCap.EditValue = ""
        tbNguyenQuan.EditValue = ""
        tbDiaChi.EditValue = ""
        tbDTNhaRieng.EditValue = ""
        tbDienThoaiCQ.EditValue = ""
        tbDiDong1.EditValue = ""
        tbDiDong2.EditValue = ""
        tbTKNganHang.EditValue = ""
        tbNganHang.EditValue = ""
        tbEmail.EditValue = ""
        cbTakeCare.EditValue = Convert.ToInt32(TaiKhoan)
        tbNgayVaoCTy.EditValue = Nothing
        tbNgayRoiCty.EditValue = Nothing
        tbHocVan.EditValue = ""
        tbChuyenMon.EditValue = ""
        tbNgoaiNgu.EditValue = ""
        tbSoThich.EditValue = ""
        tbChucVu.EditValue = ""
        tbNgheDaLam.EditValue = ""
        tbKhenThuong.EditValue = ""
        cbTakeCare.EditValue = Nothing
        cbPhongBan.EditValue = Nothing
        chkNVMoi.EditValue = Nothing
        chkConLamViec.EditValue = True
        chkNVMoi.Checked = True
        gdvFile.DataSource = DataSourceDSFile()
        tbHinhAnh.Tag = Nothing
        tbHinhAnh.EditValue = Nothing
    End Sub

    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click
        GhiLai()
        Me.Close()
    End Sub

    Private Sub GhiLai()
        If tbTen.EditValue = "" Then
            ShowCanhBao("Chưa có tên người giao dịch !")
            tbTen.Focus()
            Exit Sub
        End If

        AddParameter("@Ten", tbTen.EditValue)
        AddParameter("@Ngaysinh", tbNgaySinh.EditValue)
        AddParameter("@SoCMT", tbCMND.EditValue)
        AddParameter("@Ngaycap", tbNgayCap.EditValue)
        AddParameter("@Noicap", tbNoiCap.EditValue)
        AddParameter("@Nguyenquan", tbNguyenQuan.EditValue)
        AddParameter("@Diachi", tbDiaChi.EditValue)
        AddParameter("@DienthoaiNR", tbDTNhaRieng.EditValue)
        AddParameter("@DienthoaiCQ", tbDienThoaiCQ.EditValue)
        AddParameter("@Mobile", tbDiDong1.EditValue)
        AddParameter("@Mobile1", tbDiDong2.EditValue)
        AddParameter("@Taikhoan", tbTKNganHang.EditValue)
        AddParameter("@Nganhang", tbNganHang.EditValue)
        AddParameter("@Chamsoc", cbTakeCare.EditValue)
        AddParameter("@Noictac", 74)
        AddParameter("@Email", tbEmail.EditValue)
        AddParameter("@Ngayvaocty", tbNgayVaoCTy.EditValue)
        AddParameter("@Ngayroicty", tbNgayRoiCty.EditValue)
        AddParameter("@Hocvan", tbHocVan.EditValue)
        AddParameter("@Chuyenmon", tbChuyenMon.EditValue)
        AddParameter("@Ngoaingu", tbNgoaiNgu.EditValue)
        AddParameter("@Sothich", tbSoThich.EditValue)
        AddParameter("@Chucvu", tbChucVu.EditValue)
        AddParameter("@Trangthai", chkConLamViec.Checked)
        AddParameter("@Nganhnghedalam", tbNgheDaLam.EditValue)
        AddParameter("@Khenthuongkyluat", tbKhenThuong.EditValue)
        AddParameter("@IDDepatment", cbPhongBan.EditValue)
        AddParameter("@IDBoPhan", cbBoPhan.EditValue)
        AddParameter("@Moi", chkNVMoi.Checked)
        AddParameter("@GioiTinh", chkNam.Checked)
        AddParameter("@HinhAnh", tbHinhAnh.Tag)
        AddParameter("@Matruycap", cbQuyen.EditValue)
        AddParameter("@FileDinhKem", StrDSFile(gdvFileCT))
        Try
            If TrangThai.isAddNew Then
                AddParameter("@Nhaplieu", Convert.ToInt32(TaiKhoan))
                MaTuDien = doInsert("NHANSU")
                If MaTuDien Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Dim sql As String = "ALTER TABLE NLNhanvienE ADD C" & MaTuDien.ToString & " INT NOT NULL DEFAULT 0"
                If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@ID", MaTuDien)
                If doUpdate("NHANSU", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
            Dim index As Object = CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNhanSu).gdvNhanSuCT.FocusedRowHandle

            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNhanSu).loadDSNhanVien()
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNhanSu).gdvNhanSuCT.FocusedRowHandle = index
            ShowAlert(Me.Text & " thành công !")

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btDong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub mSuaAnh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaAnh.ItemClick
        Dim openFile As New OpenFileDialog
        openFile.Filter = "Image File|*.jpeg;*.jpg;*.png;*.gif"
        If openFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not System.IO.Directory.Exists(UrlAnhNhanSu) Then
                System.IO.Directory.CreateDirectory(UrlAnhNhanSu)
            End If
            tbHinhAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(openFile.FileName)
            tbHinhAnh.Tag = MaTuDien & " " & System.IO.Path.GetFileName(openFile.FileName)
            System.IO.File.Copy(openFile.FileName, UrlAnhNhanSu & tbHinhAnh.Tag, True)
            If Not System.IO.Directory.Exists(UrlAnhNhanSu & "thumb\") Then
                System.IO.Directory.CreateDirectory(UrlAnhNhanSu & "thumb\")
            End If
            System.IO.File.Copy(Utils.ConvertImage.ResizeImgFromURL(openFile.FileName), UrlAnhNhanSu & "thumb\" & tbHinhAnh.Tag, True)
            AddParameter("@HinhAnh", tbHinhAnh.Tag)
            AddParameterWhere("@ID", MaTuDien)
            If doUpdate("NHANSU", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    Private Sub mXoaAnh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaAnh.ItemClick
        If ShowCauHoi("Xoá ảnh nhân viên ?") Then
            If ShowCauHoi("Xoá luôn file ảnh trên máy chủ ?") Then
                If System.IO.File.Exists(UrlAnhNhanSu & tbHinhAnh.Tag) Then
                    System.IO.File.Delete(UrlAnhNhanSu & tbHinhAnh.Tag)
                    If System.IO.File.Exists(UrlAnhNhanSu & "thumb\" & tbHinhAnh.Tag) Then
                        System.IO.File.Delete(UrlAnhNhanSu & "thumb\" & tbHinhAnh.Tag)
                    End If
                    ShowAlert("Đã xoá file ảnh trên máy chủ!")
                End If
            End If
            tbHinhAnh.EditValue = Nothing
            tbHinhAnh.Tag = Nothing
        End If


    End Sub

    Private Sub mThemFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemFile.ItemClick
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            If Not System.IO.Directory.Exists(UrlTaiLieuNhanSu) Then
                System.IO.Directory.CreateDirectory(UrlTaiLieuNhanSu)
            End If
            Dim path As String = ""
            For Each file In openFile.FileNames
                Try
                    path = MaTuDien & " " & System.IO.Path.GetFileName(file)
                    If System.IO.File.Exists(UrlTaiLieuNhanSu & path) Then
                        If ShowCauHoi("File: " & path & " đã có sẵn, bạn có muốn ghi đè không ?") Then
                            System.IO.File.Copy(file, UrlTaiLieuNhanSu & path, True)
                            gdvFileCT.AddNewRow()
                            gdvFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        System.IO.File.Copy(file, UrlTaiLieuNhanSu & path)
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

    Private Sub mXoaFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaFile.ItemClick
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        Try
            If ShowCauHoi("Xoá file được chọn ?") Then
                System.IO.File.Delete(UrlTaiLieuNhanSu & gdvFileCT.GetFocusedRowCellValue("File"))
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
            Dim psi As New ProcessStartInfo()
            With psi
                .FileName = UrlTaiLieuNhanSu & e.CellValue
                .UseShellExecute = True
            End With
            Try
                Process.Start(psi)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End If
    End Sub


    Private Sub cbBoPhan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbBoPhan.ButtonClick
        If e.Button.Index = 1 Then
            cbBoPhan.EditValue = Nothing
        End If
    End Sub
End Class