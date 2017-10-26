Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper
Imports DevExpress.Utils.HorzAlignment
Imports DevExpress.XtraEditors

Public Class frmNoiDungDaoTao

    Private Sub frmMonHoc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDSNhomMH()
        LoadDSMonHoc()
    End Sub

    Public Sub LoadDSNhomMH()
        AddParameterWhere("@Loai", LoaiTuDien.NhomMonHoc)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY NoiDung")
        If Not tb Is Nothing Then
            rcbNhom.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSMonHoc()
        ShowWaiting("Đang tải dữ liệu...")
        Dim sql As String = ""
        sql &= " SELECT tblNoiDungDaoTao.*,tblTuDien.NoiDung AS NhomMH "
        sql &= " FROM tblNoiDungDaoTao LEFT JOIN tblTuDien ON tblNoiDungDaoTao.IDNhomMH = tblTuDien.ID WHERE Loai=7"
        sql &= " ORDER BY tblTuDien.NoiDung,tblNoiDungDaoTao.TenMH "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim tb2 As DataTable = DataSourceDSFile(tb.Rows(i)("NguoiGiangDay").ToString)
                tb.Rows(i)("NguoiGiangDay") = ""
                For j As Integer = 0 To tb2.Rows.Count - 1
                    AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                    Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                    If Not tb3 Is Nothing Then
                        tb.Rows(i)("NguoiGiangDay") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
                    End If
                Next
                tb.Rows(i)("NguoiGiangDay") = tb.Rows(i)("NguoiGiangDay").ToString.Trim
            Next
            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btCNNhomMH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCNNhomMH.ItemClick
        Dim f As New frmCNNhomDaoTao
        f.ShowDialog()
    End Sub

    Private Sub mThemMonHoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemMonHoc.ItemClick, btThemMonHoc.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        fCNMonHoc = New frmCNNoiDungDaoTao
        fCNMonHoc.ShowDialog()
    End Sub

    Private Sub btSuaThongTinMH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaThongTinMH.ItemClick, mSuaThongTinMH.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        Dim index As Integer = gdvCT.FocusedRowHandle
        objID = gdvCT.GetFocusedRowCellValue("ID")
        fCNMonHoc = New frmCNNoiDungDaoTao
        fCNMonHoc.ShowDialog()
        gdvCT.FocusedRowHandle = index
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

    Private Sub rPopup_Popup(sender As System.Object, e As System.EventArgs) Handles rPopup.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick

        Select Case e.Column.FieldName
            Case "BaiViet"
                gListFileCT.Text = "Danh sách bài viết tiêu biểu"
                gListFileCT.Tag = "BaiViet"
            Case "TaiLieu"
                gListFileCT.Text = "Danh sách tài liệu"
                gListFileCT.Tag = "TaiLieu"
            Case "DeThi"
                gListFileCT.Text = "Danh sách đề thi"
                gListFileCT.Tag = "DeThi"
            Case Else
                gListFileCT.Text = ""
                gListFileCT.Tag = ""
        End Select

    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            ShowWaiting("Đang mở file...")

            If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
            End If
            Application.DoEvents()
            Dim fileName As String = ""
            Select Case gListFileCT.Tag
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
        End If
    End Sub

    Private Sub btXoaMonHoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoaMonHoc.ItemClick, mXoa.ItemClick

    End Sub


End Class
