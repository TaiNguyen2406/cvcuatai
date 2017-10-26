Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmNhanSu

    Private Sub frmNhanSu_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadDSPhongBan()
        loadDSNhanVien()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.BanQuanTri) Then
            colFile.Visible = False
        Else
            colFile.Visible = True
        End If
    End Sub

    Public Sub LoadDSPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
            rcbPhongBan.DataSource = tb
            'PivotGridControl1.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSNhanVien()
        ShowWaiting("Đang tải danh sách nhân viên ...")
        Dim sql As String = "SELECT NhanSu.ID,NhanSu.Ten,ChucVu,NgaySinh,NgayVaoCTy,SoCMT, Mobile,Mobile1,DienThoaiCQ,Email,HinhAnh,convert(image,null)HienThi, "
        sql &= " IDDepatment,DiaChi,FileDinhKem, MaTruyCap,MatKhau,NhanSu_BoPhan.Ten as BoPhan "
        sql &= " FROM NHANSU "
        sql &= " LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=NhanSu.IDBoPhan"
        sql &= " WHERE Noictac=74 "
        If Not cbPhongBan.EditValue Is Nothing Then
            sql &= " AND IDDepatment = " & cbPhongBan.EditValue
        End If
        Select Case cbTrangThai.EditValue
            Case "Đã nghỉ"
                sql &= " AND Trangthai=0 "
            Case "Còn làm việc"
                sql &= " AND Trangthai=1 "
            Case Else

        End Select
        sql &= " ORDER By IDDepatment,NhanSu_BoPhan.MaBP,NhanSu.ChucVu, ID "

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            If ChkTaiAnh.Checked Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Application.DoEvents()
                    If tb.Rows(i)("HinhAnh").ToString = "" Then tb.Rows(i)("HinhAnh") = "no_avatar.png"
                    Try
                        tb.Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhNhanSu & "\thumb\" & tb.Rows(i)("HinhAnh"))
                    Catch ex As Exception

                    End Try
                Next
            End If
            '  PivotGridControl1.DataSource = tb
            gdvNhanSu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        CloseWaiting()
    End Sub

    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        loadDSNhanVien()
    End Sub

    Private Sub rcbPhongBan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhongBan.ButtonClick
        If e.Button.Index = 1 Then
            cbPhongBan.EditValue = Nothing
        End If
    End Sub


    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNNhanSu
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        TrangThai.isUpdate = True
        MaTuDien = gdvNhanSuCT.GetFocusedRowCellValue("ID")
        Dim f As New frmCNNhanSu
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
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

    Private Sub rcbFile_Popup(sender As System.Object, e As System.EventArgs) Handles rcbFile.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue.ToString)
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
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

    Private Sub gdvNhanSuCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvNhanSuCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvNhanSuCT.OptionsView.ShowAutoFilterRow = Not gdvNhanSuCT.OptionsView.ShowAutoFilterRow
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btThem.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btSua.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            mXemAnhLon.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.M Then
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                colMatKhau.Visible = Not colMatKhau.Visible
            End If
        End If
    End Sub

    Private Sub mXemAnhLon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemAnhLon.ItemClick
        Dim f As New frmXemAnh
        If gdvNhanSuCT.GetFocusedRowCellValue("HinhAnh").ToString = "" Then
            f.pAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhNhanSu & "\" & "no_avatar.png")
        Else
            f.pAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhNhanSu & "\" & gdvNhanSuCT.GetFocusedRowCellValue("HinhAnh").ToString)
        End If
        f.Text = "Ảnh: " & gdvNhanSuCT.GetFocusedRowCellValue("Ten")
        f.ShowDialog()
    End Sub

    Private Sub btXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoa.ItemClick

    End Sub


    Private Sub ChkTaiAnh_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles ChkTaiAnh.CheckedChanged
        If ChkTaiAnh.Checked Then
            ChkTaiAnh.Glyph = My.Resources.Checked
        Else
            ChkTaiAnh.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub gdvNhanSuCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvNhanSuCT.RowCellStyle
        'On Error Resume Next
        If e.Column.FieldName = "NgaySinh" Then
            If IsDBNull(e.CellValue) Then Exit Sub
            If Convert.ToDateTime(e.CellValue).ToString("dd/MM") = Today.Date.ToString("dd/MM") Then
                e.Appearance.BackColor = Color.LightCoral
                'e.Appearance.Font = New Font(e.Appearance
            End If
        End If
    End Sub
End Class
