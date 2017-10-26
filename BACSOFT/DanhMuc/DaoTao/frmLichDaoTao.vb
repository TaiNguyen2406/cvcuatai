Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmLichDaoTao

    Private _exit As Boolean = False

    Private Sub frmLichHoc_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadLichDaoTao()
    End Sub

    Private Sub btKhoaDaoTao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKhoaDaoTao.ItemClick
        Dim f As New frmCNKhoaDaoTao
        f.ShowDialog()
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        TrangThai.isAddNew = True
        fCNLichDaoTao = New frmCNLichDaoTao
        fCNLichDaoTao.ShowDialog()
    End Sub

    Public Sub LoadLichDaoTao()
        Dim sql As String = ""
        sql &= " SELECT (0)STT, tblLichDaoTao.ID,tblLichDaoTao.PhuTrach,tblLichDaoTao.Ngay,"
        sql &= " 	(tblKhoaDaoTao.Ten + ': ' + convert(nvarchar,tblKhoaDaoTao.TuNgay,103) + ' - ' +  Convert(nvarchar,tblKhoaDaoTao.DenNgay,103))Khoa,tblKhoaDaoTao.ID AS IDKhoaDT,"
        sql &= " 	tblNoiDungDaoTao.TenMH AS NoiDungDaoTao,tblNoiDungDaoTao.NoiDung AS NoiDungChiTiet,tblNoiDungDaoTao.ThoiLuong,tblNoiDungDaoTao.DiemChuan,"
        sql &= "    tblNoiDungDaoTao.TaiLieu,tblNoiDungDaoTao.BaiViet,tblNoiDungDaoTao.DeThi "
        sql &= " FROM tblLichDaoTao"
        sql &= " LEFT JOIN tblKhoaDaoTao ON tblKhoaDaoTao.ID=tblLichDaoTao.IDKhoa"
        sql &= " LEFT JOIN tblNoiDungDaoTao ON tblNoiDungDaoTao.ID=tblLichDaoTao.IDNoiDung"
        sql &= " ORDER BY Khoa DESC,NoiDungDaoTao"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim stt As Integer = 0

            For i As Integer = 0 To tb.Rows.Count - 1

                tb.Rows(i)("STT") = stt + 1
                stt += 1
                If i + 1 <= tb.Rows.Count - 1 Then
                    If tb.Rows(i + 1)("IDKhoaDT") <> tb.Rows(i)("IDKhoaDT") Then
                        stt = 0
                    End If
                End If


                Dim tb2 As DataTable = DataSourceDSFile(tb.Rows(i)("PhuTrach").ToString, , ";")
                tb.Rows(i)("PhuTrach") = ""
                For j As Integer = 0 To tb2.Rows.Count - 1
                    AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                    Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                    If Not tb3 Is Nothing Then
                        tb.Rows(i)("PhuTrach") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
                    End If
                Next
                tb.Rows(i)("PhuTrach") = tb.Rows(i)("PhuTrach").ToString.Trim

                Dim tb4 As DataTable = DataSourceDSFile(tb.Rows(i)("Ngay").ToString, , ";")
                tb.Rows(i)("Ngay") = ""
                For j As Integer = 0 To tb4.Rows.Count - 1
                    tb.Rows(i)("Ngay") &= "- " & Convert.ToDateTime(tb4.Rows(j)(0)).ToString("HH:mm dddd, dd/MM/yyyy") & vbCrLf
                Next
                tb.Rows(i)("Ngay") = tb.Rows(i)("Ngay").ToString.Trim
            Next

            gdvLichHoc.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        ' LoadDSHocVien()
    End Sub

    Public Sub LoadDSHocVien()
        AddParameterWhere("@IDKhoaHoc", gdvLichHocCT.GetFocusedRowCellValue("ID"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT tblHocVien.ID,tblHocVien.DiemSo,tblHocVien.FileDinhKem,NHANSU.Ten AS HocVien,tblHocVien.IDNhanSu FROM tblHocVien INNER JOIN NHANSU ON NHANSU.ID = tblHocVien.IDNhanSu WHERE IDLichDaoTao=@IDKhoaHoc")
        If Not tb Is Nothing Then
            gdvHocVien.DataSource = tb
        Else
            For i As Integer = 0 To gdvHocVienCT.RowCount - 1
                gdvHocVienCT.DeleteRow(i)
            Next
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvLichHocCT_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvLichHocCT.FocusedRowChanged
        LoadDSHocVien()

    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick
        If gdvLichHocCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        Dim index As Integer = gdvLichHocCT.FocusedRowHandle
        TrangThai.isUpdate = True
        fCNLichDaoTao = New frmCNLichDaoTao
        objID = gdvLichHocCT.GetFocusedRowCellValue("ID")
        fCNLichDaoTao.Tag = Me.Parent.Tag
        fCNLichDaoTao.ShowDialog()
        gdvLichHocCT.FocusedRowHandle = index
    End Sub

    Private Sub btXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoa.ItemClick
        If gdvLichHocCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If ShowCauHoi("Bạn có chắc là muốn xoá lịch đào tạo này không ?") Then
            Try
                BeginTransaction()
                AddParameterWhere("@IDLichDaoTao", gdvLichHocCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblHocVien", "IDLichDaoTao=@IDLichDaoTao") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                AddParameterWhere("@IDLichDaoTao", gdvLichHocCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblLichDaoTao", "ID=@IDLichDaoTao") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                ComitTransaction()
                ShowAlert("Đã xoá !")
            Catch ex As Exception
                RollBackTransaction()
                ShowBaoLoi(LoiNgoaiLe)
            End Try

        End If
    End Sub

    Private Sub gdvHocVienCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvHocVienCT.CellValueChanged
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If e.Column.FieldName = "DiemSo" Then
            AddParameter("@DiemSo", e.Value)
            AddParameterWhere("@ID", gdvHocVienCT.GetRowCellValue(e.RowHandle, "ID"))
            If doUpdate("tblHocVien", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật điểm học viên: " & gdvHocVienCT.GetRowCellValue(e.RowHandle, "HocVien"))
            End If
        End If

    End Sub

    Private Sub rpopupLichDaoTao_Popup(sender As System.Object, e As System.EventArgs) Handles rpopupLichDaoTao.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue.ToString)
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

    Private Sub gdvLichHocCT_GotFocus(sender As System.Object, e As System.EventArgs) Handles gdvLichHocCT.GotFocus
        btThemFile.Visible = False
        btXoaFile.Visible = False
        rpopupLichDaoTao.PopupControl = popupFile
    End Sub

    Private Sub gdvHocVienCT_GotFocus(sender As System.Object, e As System.EventArgs) Handles gdvHocVienCT.GotFocus
        btThemFile.Visible = True
        btXoaFile.Visible = True
        rpopupFileHV.PopupControl = popupFile
    End Sub

    Private Sub rcbFileHV_Popup(sender As System.Object, e As System.EventArgs) Handles rpopupFileHV.Popup
        If _exit Then
            _exit = False
            Exit Sub
        End If
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue.ToString)
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        ' On Error Resume Next
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        Dim path As String = ""

        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            ShowWaiting("Đang tải file lên máy chủ ...")
            If Not System.IO.Directory.Exists(UrlDaoTao & gdvLichHocCT.GetFocusedRowCellValue("ID")) Then
                System.IO.Directory.CreateDirectory(UrlDaoTao & gdvLichHocCT.GetFocusedRowCellValue("ID"))
            End If
            For Each file In openFile.FileNames
                Try
                    path = TaiKhoan.ToString & " " & gdvHocVienCT.GetFocusedRowCellValue("IDNhanSu") & " " & IO.Path.GetFileName(file)
                    If System.IO.File.Exists(UrlDaoTao & gdvLichHocCT.GetFocusedRowCellValue("ID") & "\" & path) Then
                        If ShowCauHoi("File: " & path & " đã tồn tại, bạn có muốn ghi đè không ?") Then
                            IO.File.Copy(file, UrlDaoTao & gdvLichHocCT.GetFocusedRowCellValue("ID") & "\" & path, True)
                            gdvListFileCT.AddNewRow()
                            gdvListFileCT.SetFocusedRowCellValue("File", path)
                        End If
                    Else
                        IO.File.Copy(file, UrlDaoTao & gdvLichHocCT.GetFocusedRowCellValue("ID") & "\" & path)
                        gdvListFileCT.AddNewRow()
                        gdvListFileCT.SetFocusedRowCellValue("File", path)
                    End If

                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            CloseWaiting()
            Impersonator.EndImpersonation()
            gdvListFileCT.CloseEditor()
            gdvListFileCT.UpdateCurrentRow()
            _exit = True
            SendKeys.Send("{F4}")

        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub

        If ShowCauHoi("Xoá file được chọn ?") Then
            gdvListFileCT.DeleteSelectedRows()
            If ShowCauHoi("Xoá luôn file trong hệ thống ?") Then
                Try
                    Dim Impersonator As New Impersonator()
                    Impersonator.BeginImpersonation()
                    IO.File.Delete(UrlDaoTao & gdvLichHocCT.GetFocusedRowCellValue("ID") & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                    Impersonator.EndImpersonation()
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If

        End If

    End Sub

    Private Sub rpopupFileHV_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rpopupFileHV.Closed
        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        AddParameter("@FileDinhKem", _File)
        AddParameterWhere("@ID", gdvHocVienCT.GetFocusedRowCellValue("ID"))
        If doUpdate("tblHocVien", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CType(sender, PopupContainerEdit).EditValue = _File
        End If
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            If btThemFile.Visible = True Then
                OpenFileOnLocal(UrlDaoTao & gdvLichHocCT.GetFocusedRowCellValue("ID") & "\" & e.CellValue, e.CellValue)
            Else
                Select Case gdvLichHocCT.FocusedColumn.FieldName
                    Case Is = "BaiViet"
                        OpenFileOnLocal(UrlDaoTao & "BAI VIET\" & e.CellValue, e.CellValue)
                    Case Is = "TaiLieu"
                        OpenFileOnLocal(UrlDaoTao & "TAI LIEU\" & e.CellValue, e.CellValue)
                    Case Is = "DeThi"
                        OpenFileOnLocal(UrlDaoTao & "DE THI\" & e.CellValue, e.CellValue)
                End Select
            End If


        End If
    End Sub
End Class