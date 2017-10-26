Imports BACSOFT.Db.SqlHelper

Public Class frmCNNoiDungDaoTao

    Private Sub frmCNMonHoc_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        gdvGiaoVien.DataSource = DataSourceDSFile(, "IDNhanSu")
        gdvTaiLieu.DataSource = DataSourceDSFile(, "TaiLieu")
        gdvBaiViet.DataSource = DataSourceDSFile(, "BaiViet")
        gdvDeThi.DataSource = DataSourceDSFile(, "DeThi")
        loadDSTuDien()

        If TrangThai.isAddNew Then
            Me.Text = "Thêm môn học"
        Else
            Me.Text = "Cập nhật nội dung môn học"
            Dim sql As String = ""
            sql &= " SELECT * FROM tblNoiDungDaoTao WHERE ID=@ID"
            AddParameterWhere("@ID", objID)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing Then
                tbTenMonHoc.EditValue = dt.Rows(0)("TenMH")
                tbNoiDung.EditValue = dt.Rows(0)("NoiDung")
                cbNhomMH.EditValue = dt.Rows(0)("IDNhomMH")
                tbThoiLuong.EditValue = dt.Rows(0)("ThoiLuong")
                tbDiemChuan.EditValue = dt.Rows(0)("DiemChuan")
                gdvGiaoVien.DataSource = DataSourceDSFile(dt.Rows(0)("NguoiGiangDay").ToString, "IDNhanSu")
                gdvTaiLieu.DataSource = DataSourceDSFile(dt.Rows(0)("TaiLieu").ToString, "TaiLieu")
                gdvBaiViet.DataSource = DataSourceDSFile(dt.Rows(0)("BaiViet").ToString, "BaiViet")
                gdvDeThi.DataSource = DataSourceDSFile(dt.Rows(0)("DeThi").ToString, "DeThi")
            End If
        End If

    End Sub

    Public Sub loadDSTuDien()
        AddParameterWhere("@Loai", LoaiTuDien.NhomMonHoc)
        Dim sql As String = ""
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY NoiDung"
        sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74"
        Dim dt As DataSet = ExecuteSQLDataSet(sql)
        If Not dt Is Nothing Then
            cbNhomMH.Properties.DataSource = dt.Tables(0)
            If dt.Tables(0).Rows.Count > 0 Then
                cbNhomMH.EditValue = dt.Tables(0).Rows(0)(0)
            End If
            rcbNgGiangDay.DataSource = dt.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click
        GhiLai()

    End Sub

    Public Sub GhiLai()
        gdvGiaoVienCT.CloseEditor()
        gdvGiaoVienCT.UpdateCurrentRow()

        If tbTenMonHoc.EditValue.ToString = "" Then
            ShowCanhBao("Chưa có tên nội dung đào tạo !")
            Exit Sub
        End If

        Try
            AddParameter("@TenMH", tbTenMonHoc.EditValue)
            AddParameter("@NoiDung", tbNoiDung.EditValue)
            AddParameter("@IDNhomMH", cbNhomMH.EditValue)
            AddParameter("@DiemChuan", tbDiemChuan.EditValue)
            AddParameter("@ThoiLuong", tbThoiLuong.EditValue)
            AddParameter("@NguoiGiangDay", StrDSFile(gdvGiaoVienCT, "IDNhanSu"))
            AddParameter("@TaiLieu", StrDSFile(gdvTaiLieuCT, "TaiLieu"))
            AddParameter("@BaiViet", StrDSFile(gdvBaiVietCT, "BaiViet"))
            AddParameter("@DeThi", StrDSFile(gdvDeThiCT, "DeThi"))
            If TrangThai.isAddNew Then
                objID = doInsert("tblNoiDungDaoTao")
                If objID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                TrangThai.isUpdate = True
            Else
                AddParameterWhere("@ID", objID)
                If doUpdate("tblNoiDungDaoTao", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            If Me.Tag = "CNLichDaoTao" Then
                fCNLichDaoTao.loadDSNoiDungDaoTao()
            Else
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNoiDungDaoTao).LoadDSMonHoc()
            End If


            ShowAlert("Đã lưu lại !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub cbNhomMH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNhomMH.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNNhomDaoTao
            f.Tag = "CNMonHoc"
            f.ShowDialog()
        End If
    End Sub

    Private Sub mThemTaiLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemTaiLieu.ItemClick
        If TrangThai.isAddNew Then
            ShowCanhBao("Phải lưu lại trước khi thực hiện thao tác này !")
            Exit Sub
        End If
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not System.IO.Directory.Exists(UrlDaoTao & "TAI LIEU") Then
                Try
                    System.IO.Directory.CreateDirectory(UrlDaoTao & "TAI LIEU")
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If
            For Each File In openFile.FileNames
                Try
                    IO.File.Copy(File, UrlDaoTao & "TAI LIEU\" & objID & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(File), True)
                    gdvTaiLieuCT.AddNewRow()
                    gdvTaiLieuCT.SetFocusedRowCellValue("TaiLieu", objID & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(File))
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try

            Next
            gdvTaiLieuCT.CloseEditor()
            gdvTaiLieuCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub mXoaTaiLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaTaiLieu.ItemClick
        If Not gdvTaiLieuCT.FocusedRowHandle < 0 Then
            If ShowCauHoi("Xoá tài liệu được chọn") Then
                Try
                    IO.File.Delete(UrlDaoTao & "TAI LIEU\" & gdvTaiLieuCT.GetFocusedRowCellValue("TaiLieu"))
                    gdvTaiLieuCT.DeleteSelectedRows()
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If
            
        End If
    End Sub

    Private Sub mThemBaiViet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemBaiViet.ItemClick
        If TrangThai.isAddNew Then
            ShowCanhBao("Phải lưu lại trước khi thực hiện thao tác này !")
            Exit Sub
        End If
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not System.IO.Directory.Exists(UrlDaoTao & "BAI VIET") Then
                Try
                    System.IO.Directory.CreateDirectory(UrlDaoTao & "BAI VIET")
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If
            For Each File In openFile.FileNames
                Try
                    IO.File.Copy(File, UrlDaoTao & "BAI VIET\" & objID & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(File), True)
                    gdvBaiVietCT.AddNewRow()
                    gdvBaiVietCT.SetFocusedRowCellValue("BaiViet", objID & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(File))
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try

            Next
            gdvBaiVietCT.CloseEditor()
            gdvBaiVietCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub mXoaBaiViet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaBaiViet.ItemClick
        If Not gdvBaiVietCT.FocusedRowHandle < 0 Then
            If ShowCauHoi("Xoá bài viết được chọn ?") Then
                Try
                    IO.File.Delete(UrlDaoTao & "BAI VIET\" & gdvBaiVietCT.GetFocusedRowCellValue("BaiViet"))
                    gdvBaiVietCT.DeleteSelectedRows()
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If
           
        End If
    End Sub

    Private Sub mThemDeThi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemDeThi.ItemClick
        If TrangThai.isAddNew Then
            ShowCanhBao("Phải lưu lại trước khi thực hiện thao tác này !")
            Exit Sub
        End If
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not System.IO.Directory.Exists(UrlDaoTao & "DE THI") Then
                Try
                    System.IO.Directory.CreateDirectory(UrlDaoTao & "DE THI")
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If
            For Each File In openFile.FileNames
                Try
                    IO.File.Copy(File, UrlDaoTao & "DE THI\" & objID & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(File), True)
                    gdvDeThiCT.AddNewRow()
                    gdvDeThiCT.SetFocusedRowCellValue("DeThi", objID & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(File))
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try

            Next
            gdvDeThiCT.CloseEditor()
            gdvDeThiCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub mXoaDeThi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaDeThi.ItemClick
        If Not gdvDeThiCT.FocusedRowHandle < 0 Then
            If ShowCauHoi("Xoá đề thi được chọn ?") Then
                Try
                    IO.File.Delete(UrlDaoTao & "DE THI\" & gdvDeThiCT.GetFocusedRowCellValue("DeThi"))
                    gdvDeThiCT.DeleteSelectedRows()
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If
           
        End If
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        tbTenMonHoc.EditValue = ""
        tbNoiDung.EditValue = ""
        tbDiemChuan.EditValue = 0
        tbThoiLuong.EditValue = 0
        gdvGiaoVien.DataSource = DataSourceDSFile(, "IDNhanSu")
        gdvTaiLieu.DataSource = DataSourceDSFile(, "TaiLieu")
        gdvBaiViet.DataSource = DataSourceDSFile(, "BaiViet")
        gdvDeThi.DataSource = DataSourceDSFile(, "DeThi")
        TrangThai.isAddNew = True
    End Sub

    Private Sub gdvTaiLieuCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvTaiLieuCT.RowCellClick, gdvBaiVietCT.RowCellClick, gdvDeThiCT.RowCellClick
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            ShowWaiting("Đang mở file...")

            If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
            End If
            Application.DoEvents()

            Dim fileName As String = ""
            Select Case e.Column.FieldName
                Case "BaiViet"
                fileName = UrlDaoTao & "BAI VIET\" & e.CellValue
                Case "TaiLieu"
                fileName = UrlDaoTao & "TAI LIEU\" & e.CellValue
                Case "DeThi"
                fileName = UrlDaoTao & "DE THI\" & e.CellValue
            End Select

            Try
                System.IO.File.Copy(fileName, Application.StartupPath & "\tmp\" & e.CellValue, True)
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
                Exit Sub
            End Try

            CloseWaiting()

            Dim psi As New ProcessStartInfo()
            With psi
                .FileName = Application.StartupPath & "\tmp\" & e.CellValue
                .UseShellExecute = True
            End With
            Try
                Process.Start(psi)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
    End Sub

    Private Sub mXoaNgGiangDay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaNgGiangDay.ItemClick
        If gdvGiaoVienCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xoá dòng này ?") Then
            gdvGiaoVienCT.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gdvGiaoVienCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvGiaoVienCT.KeyDown
        mXoaNgGiangDay.PerformClick()
    End Sub

    Private Sub gdvTaiLieuCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvTaiLieuCT.KeyDown
        mXoaTaiLieu.PerformClick()
    End Sub

    Private Sub gdvDeThiCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvDeThiCT.KeyDown
        mXoaDeThi.PerformClick()
    End Sub

    Private Sub gdvBaiVietCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvBaiVietCT.KeyDown
        mXoaBaiViet.PerformClick()
    End Sub

    Private Sub frmCNNoiDungDaoTao_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If tbTenMonHoc.EditValue <> "" And tbNoiDung.EditValue <> "" Then
            GhiLai()
        End If


    End Sub
End Class