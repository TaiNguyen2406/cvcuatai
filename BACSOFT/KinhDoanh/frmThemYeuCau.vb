Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmThemYeuCau
    Public tmpTrangThai As New Utils.TrangThai
    Public tmpMaTuDien As Object
    Public _Exit As Boolean = False
    'Public SoYeuCau As Object


    Private Sub frmThemYeuCau_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadCbTrangThai()
        loadKhachHang()
        loadTakeCare()
        setSourceGdvFileDinhKem()
        loadDSLoaiCongTrinh()
        LoadDSNguonKH()
        If TrangThai.isAddNew Then
            Me.Text = "Thêm yêu cầu"
            cbTrangThai.EditValue = Convert.ToByte(TrangThaiYeuCau.CanChaoGia)
        Else
            Me.Text = "Cập nhật yêu cầu"
            AddParameterWhere("@MaYC", MaTuDien)
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM BANGYEUCAU WHERE ID=@MaYC")
            If Not tb Is Nothing Then
                tbNgay.EditValue = tb.Rows(0)("Ngaythang")
                tbSoPhieu.EditValue = tb.Rows(0)("Sophieu")
                cbTakeCare.EditValue = tb.Rows(0)("IDTakecare")

                tbYeuCau.EditValue = tb.Rows(0)("Noidung")
                cbLoaiYeuCau.EditValue = tb.Rows(0)("IDLoaiYeuCau")
                _Exit = True
                gdvMaKH.EditValue = tb.Rows(0)("IDKhachhang")
                cbNguoiGD.EditValue = tb.Rows(0)("IDNgd")
                _Exit = False
                cbTrangThai.EditValue = tb.Rows(0)("TrangThai")
                If IsDBNull(tb.Rows(0)("Congtrinh")) Then
                    chkCongTrinh.Checked = False
                Else
                    chkCongTrinh.Checked = tb.Rows(0)("Congtrinh")
                End If

                If tb.Rows(0)("FileDinhKem").ToString <> "" Then
                    Dim str() As String = tb.Rows(0)("FileDinhKem").ToString.Split(New Char() {";c"})
                    For Each _St In str
                        gdvFileCT.AddNewRow()
                        gdvFileCT.SetFocusedRowCellValue("File", _St)
                    Next
                    gdvFileCT.CloseEditor()
                    gdvFileCT.UpdateCurrentRow()
                End If
                'SoYeuCau = 
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
        gdvMaKH.Focus()
    End Sub

    Private Sub LoadDSNguonKH()
        AddParameterWhere("@Loai", LoaiTuDien.NguonKhachMoi)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            cbNguonKH.Properties.DataSource = tb
        End If
    End Sub

    Public Sub setSourceGdvFileDinhKem()
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvFile.DataSource = tb
    End Sub

    Public Sub loadDSLoaiCongTrinh()

        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung FROM KTPhanBoLoiNhuanCT ORDER BY STT")
        If Not tb Is Nothing Then
            cbLoaiYeuCau.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadCbTrangThai()
        AddParameter("@Loai", LoaiTuDien.YeuCauDen)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            cbTrangThai.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadKhachHang()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,IDTakecare FROM KHACHHANG ORDER BY ttcMa")
        If Not tb Is Nothing Then
            gdvMaKH.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadNguoiGD()
        AddParameterWhere("@IDCTY", gdvMaKH.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,Chamsoc FROM NHANSU WHERE Noictac=@IDCTY")
        If Not tb Is Nothing Then
            cbNguoiGD.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadTakeCare()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not tb Is Nothing Then
            cbTakeCare.Properties.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbTakeCare.EditValue = Convert.ToInt32(TaiKhoan)
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub gdvMaKH_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gdvMaKH.EditValueChanged
        On Error Resume Next
        If gdvMaKH.IsPopupOpen Then Exit Sub
        loadNguoiGD()
        cbNguoiGD.Focus()
        If _Exit Then Exit Sub
        Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        cbTakeCare.EditValue = dr("IDTakecare")
    End Sub

    Private Sub cbNguoiGD_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNguoiGD.EditValueChanged
        On Error Resume Next
        If _Exit Then Exit Sub
        Dim edit As LookUpEdit = CType(sender, LookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        cbTakeCare.EditValue = dr("Chamsoc")
    End Sub

    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click
        GhiLai()
        Me.Close()
    End Sub

    Private Sub btThem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btThem.Click
        GhiLai()
        TrangThai.isAddNew = True
        tbSoPhieu.EditValue = ""
        tbNgay.EditValue = Nothing
        cbTakeCare.EditValue = Convert.ToInt32(TaiKhoan)
        gdvMaKH.EditValue = Nothing
        cbNguoiGD.EditValue = Nothing
        tbYeuCau.EditValue = ""
        chkCongTrinh.Checked = False
        For i As Integer = 0 To gdvFileCT.RowCount - 1
            gdvFileCT.DeleteRow(i)
        Next
        gdvMaKH.Focus()
    End Sub

    Private Sub GhiLai()
        Dim tg As DateTime = GetServerTime()
        Dim path As String = ""
        Dim _ListFile As String = ""
        For i As Integer = 0 To gdvFileCT.RowCount - 1
            _ListFile &= gdvFileCT.GetRowCellValue(i, "File")
            If i < gdvFileCT.RowCount - 1 Then
                _ListFile &= ";"
            End If
        Next
        Try

            Dim sql As String = ""

            Dim _SPYC As Object = LaySoPhieu("BANGYEUCAU")
            If TrangThai.isAddNew Then
                If _SPYC Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If
            End If


            sql &= " SET DATEFORMAT DMY"

            sql &= " IF @IsAddnew = 1"
            sql &= "    BEGIN"
            'sql &= "        DECLARE @DoDai AS NVARCHAR(4) "
            'sql &= "        SET @DoDai = N'0000'"
            'sql &= "        DECLARE @SP AS INT"
            sql &= "        DECLARE @SoPhieu AS NVARCHAR(9) "
            'sql &= "        DECLARE @Thang AS NVARCHAR(2)"
            'sql &= "        DECLARE @Nam AS NVARCHAR(2)"
            'sql &= "        SET @Thang = LEFT('00',2-LEN(CONVERT(NVARCHAR,DATEPART(mm,getdate())))) +  CONVERT(NVARCHAR,DATEPART(mm,getdate()))"
            'sql &= "        SET @Nam = LEFT('00',2-LEN(RIGHT( CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2))) + RIGHT(CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2)"
            'sql &= "        SET @SP = (SELECT ISNULL(MAX(CONVERT(INT,ISNULL(RIGHT(Sophieu,4),0))),0)+1  "
            'sql &= "                    FROM BANGYEUCAU "
            'sql &= "                    WHERE"
            'sql &= "				            LEFT(Sophieu,2)=@Nam AND "
            'sql &= "				            SUBSTRING(Sophieu,3,2)=@Thang AND LEN(Sophieu)=8"
            'sql &= "			            )"
            '   sql &= "        SET @SoPhieu = @Nam +  @Thang  + CONVERT(NVARCHAR,LEFT(@DoDai,LEN(@DoDai)-LEN(@SP))) + CONVERT(NVARCHAR,@SP)"
            sql &= "        SET @SoPhieu= '" & _SPYC.ToString & "'"
            sql &= "        INSERT INTO BANGYEUCAU (Ngaythang,Sophieu,IDkhachhang,IDUser,IDNgd,IDTakecare,NoiDung,FileDinhKem,CongTrinh,TrangThai,IDLoaiYeuCau)"
            sql &= "        VALUES(getdate(),@SoPhieu,@IDKhachHang,@IDUser,@IDNgd,@IDTakecare,@NoiDung,@FileDinhKem,@CongTrinh,@TrangThai,@IDLoaiYeuCau); SELECT @SoPhieu,SCOPE_IDENTITY();"
            'sql &= "        SELECT @SoPhieu"
            sql &= "    End"
            sql &= " Else"
            sql &= "    BEGIN"
            sql &= "        Update BANGYEUCAU "
            sql &= "        SET IDkhachhang=@IDKhachhang,IDModify=@IDUser,IDNgd=@IDNgd,IDTakecare=@IDTakecare,NoiDung=@NoiDung,FileDinhKem=@FileDinhKem,CongTrinh=@CongTrinh,TrangThai=@TrangThai,IDLoaiYeuCau=@IDLoaiYeuCau "
            sql &= "        WHERE ID=@ID"
            sql &= "    SELECT SoPhieu FROM BANGYEUCAU WHERE ID=@ID"
            sql &= "    End"

            AddParameter("@IDTakecare", cbTakeCare.EditValue)
            AddParameter("@IDKhachhang", gdvMaKH.EditValue)
            AddParameter("@IDNgd", cbNguoiGD.EditValue)
            AddParameter("@IDUser", Convert.ToInt32(TaiKhoan))
            AddParameter("@NoiDung", tbYeuCau.EditValue)
            AddParameter("@IDLoaiYeuCau", cbLoaiYeuCau.EditValue)
            If TrangThai.isUpdate Then
                AddParameter("@FileDinhKem", _ListFile)
            Else
                AddParameter("@FileDinhKem", "")
            End If

            AddParameter("@Congtrinh", chkCongTrinh.Checked)
            AddParameter("@ID", MaTuDien)
            AddParameter("@IsAddnew", TrangThai.isAddNew)
            AddParameter("@TrangThai", cbTrangThai.EditValue)

            Dim tb As DataTable = ExecuteSQLDataTable(sql)

            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)

            If TrangThai.isAddNew Then
                _ListFile = ""
                Dim Impersonator As New Impersonator()
                Impersonator.BeginImpersonation()
                If Not System.IO.Directory.Exists(RootUrl & UrlKinhDoanh & gdvMaKH.Text) Then
                    System.IO.Directory.CreateDirectory(RootUrl & UrlKinhDoanh & gdvMaKH.Text)
                End If
                ShowWaiting("Đang chuyển file lên server ...")
                For i As Integer = 0 To gdvFileCT.RowCount - 1
                    path = "YC" & tb.Rows(0)(0) & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(gdvFileCT.GetRowCellValue(i, "File"))
                    'ShowCanhBao(path)
                    IO.File.Copy(gdvFileCT.GetRowCellValue(i, "File"), RootUrl & UrlKinhDoanh & gdvMaKH.Text & "\" & path)
                    _ListFile &= path & ";"
                Next
                _ListFile = _ListFile.TrimEnd(New Char() {";"c})
                Impersonator.EndImpersonation()
                CloseWaiting()
                AddParameter("@FileDinhKem", _ListFile)
                AddParameterWhere("@ID", tb.Rows(0)(1))
                If doUpdate("BANGYEUCAU", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If

                If Not cbNguonKH.EditValue Is Nothing Then
                    AddParameter("@ThoiGian", DateTime.Now)
                    AddParameter("@IDNguon", cbNguonKH.EditValue)
                    AddParameter("@NoiDung", tbYeuCau.EditValue)
                    AddParameter("@IDNgd", cbNguoiGD.EditValue)
                    AddParameter("@IDKhachHang", gdvMaKH.EditValue)

                    AddParameter("@IDNhanVien", TaiKhoan)

                    If doInsert("tblNguonKhachMoi") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                    End If

                End If

            End If


            If TrangThai.isUpdate Then
                If ShowCauHoi("Bạn có muốn chuyển trạng thái toàn bộ yêu cầu chi tiết theo trạng thái yêu cầu chính không ?") Then
                    AddParameter("@TrangThai", cbTrangThai.EditValue)
                    If cbTrangThai.EditValue = TrangThaiYeuCau.CanHoiGiaHang Then
                        AddParameter("@NgayNhanYeuCau", tg)
                    End If
                    AddParameterWhere("@SP", tbSoPhieu.EditValue)
                    If doUpdate("YEUCAUDEN", "SoPhieu=@SP") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                End If
            End If

            TrangThai.isUpdate = True
            If TypeOf (deskTop.tabMain.SelectedTabPage.Controls(0)) Is frmYeuCauDen Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDen).LoadDSYeuCau()
            ElseIf TypeOf (deskTop.tabMain.SelectedTabPage.Controls(0)) Is frmXuLyYCCongTrinh Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYCCongTrinh).LoadDSYeuCau()
            End If

            ShowAlert(Me.Text & " thành công!")
        Catch ex As Exception
            Try
                IO.File.Delete(path)
            Catch

            End Try
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btDong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub gdvMaKH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvMaKH.KeyPress
        If gdvMaKH.IsPopupOpen Then
            Exit Sub
        Else
            gdvMaKH.ShowPopup()
        End If
    End Sub

    Private Sub cbNguoiGD_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNguoiGD.ButtonClick
        tmpTrangThai.isAddNew = TrangThai.isAddNew
        tmpTrangThai.isUpdate = TrangThai.isUpdate
        tmpMaTuDien = MaTuDien
        Select Case e.Button.Index
            Case 1
                TrangThai.isAddNew = True
                Dim f As New frmCNNguoiGiaoDich
                f.Tag = "CNYEUCAUDEN"
                If Not IsDBNull(gdvMaKH.EditValue) Then
                    If gdvMaKH.EditValue <> 74 Then
                        f.cbNoiCongTac.EditValue = gdvMaKH.EditValue
                    End If

                End If
                If Not IsDBNull(cbTakeCare.EditValue) Then
                    f.cbTakeCare.EditValue = cbTakeCare.EditValue
                End If
                f.ShowDialog()
                loadNguoiGD()
                cbNguoiGD.EditValue = DBNull.Value
                cbNguoiGD.EditValue = Convert.ToInt32(MaTuDien)
            Case 2

                TrangThai.isUpdate = True
                MaTuDien = cbNguoiGD.EditValue
                Dim f As New frmCNNguoiGiaoDich
                f.Tag = "CNYEUCAUDEN"
                If Not IsDBNull(gdvMaKH.EditValue) Then

                    If gdvMaKH.EditValue <> 74 Then
                        f.cbNoiCongTac.EditValue = gdvMaKH.EditValue
                    End If
                End If
                f.ShowDialog()
                loadNguoiGD()
        End Select

        TrangThai.isAddNew = tmpTrangThai.isAddNew
        TrangThai.isUpdate = tmpTrangThai.isUpdate
        MaTuDien = tmpMaTuDien
    End Sub

    Private Sub gdvFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvFileCT.RowCellClick
        If e.Column.Name = "colFile" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub gdvFileCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvFileCT.KeyDown
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        If e.KeyCode = Keys.Delete Then
            btXoaFile.PerformClick()
        End If
    End Sub

    Private Sub gdvMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gdvMaKH.ButtonClick
        tmpTrangThai.isAddNew = TrangThai.isAddNew
        tmpTrangThai.isUpdate = TrangThai.isUpdate
        tmpMaTuDien = MaTuDien
        Select Case e.Button.Index
            Case 1
                TrangThai.isAddNew = True
                fCNKhachHang = New frmCNKhachHang
                fCNKhachHang.Tag = "CNYEUCAUDEN"
                If Not IsDBNull(cbTakeCare.EditValue) Then
                    fCNKhachHang.cbTakecare.EditValue = cbTakeCare.EditValue
                End If
                fCNKhachHang.ShowDialog()
                loadKhachHang()
                gdvMaKH.EditValue = DBNull.Value
                gdvMaKH.EditValue = Convert.ToInt32(MaTuDien)
            Case 2
                If IsDBNull(gdvMaKH.EditValue) Then Exit Select
                TrangThai.isUpdate = True
                MaTuDien = gdvMaKH.EditValue
                fCNKhachHang = New frmCNKhachHang
                fCNKhachHang.Tag = "CNYEUCAUDEN"
                fCNKhachHang.ShowDialog()
                loadKhachHang()
        End Select

        TrangThai.isAddNew = tmpTrangThai.isAddNew
        TrangThai.isUpdate = tmpTrangThai.isUpdate
        MaTuDien = tmpMaTuDien
    End Sub

    Private Sub frmThemYeuCau_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If TrangThai.isAddNew Then
            For i As Integer = 0 To gdvFileCT.RowCount - 1
                Try
                    IO.File.Delete(RootUrl & UrlKinhDoanh & gdvMaKH.Text & "\" & gdvFileCT.GetRowCellValue(i, "File"))
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        If gdvMaKH.Text = "" Then
            ShowCanhBao("Chưa có mã khách hàng !")
            Exit Sub
        End If
        Dim path As String = ""
        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(RootUrl & UrlKinhDoanh & gdvMaKH.Text) Then
                System.IO.Directory.CreateDirectory(RootUrl & UrlKinhDoanh & gdvMaKH.Text)
            End If
            For Each file In OpenFile.FileNames
                If TrangThai.isUpdate Then
                    ShowWaiting("Đang chuyển file lên server ...")
                    path = "YC" & tbSoPhieu.EditValue & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(file)
                    Try
                        IO.File.Copy(file, RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & path)
                        gdvFileCT.AddNewRow()
                        gdvFileCT.SetFocusedRowCellValue("File", path)
                    Catch ex As Exception
                        ShowBaoLoi(ex.Message)
                    End Try
                    CloseWaiting()
                ElseIf TrangThai.isAddNew Then
                    gdvFileCT.AddNewRow()
                    gdvFileCT.SetFocusedRowCellValue("File", file)
                End If

            Next
            Impersonator.EndImpersonation()
        End If
        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        Try
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            IO.File.Delete(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & gdvFileCT.GetFocusedRowCellValue("File"))
            Impersonator.EndImpersonation()
            gdvFileCT.DeleteSelectedRows()
        Catch ex As Exception
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not IO.File.Exists(RootUrlOld & Convert.ToDateTime(tbNgay.EditValue).Year.ToString & "\" & UrlKinhDoanh & gdvMaKH.Text & "\" & gdvFileCT.GetFocusedRowCellValue("File")) Then
                gdvFileCT.DeleteSelectedRows()
            End If
            Impersonator.EndImpersonation()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub cbLoaiYeuCau_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbLoaiYeuCau.KeyPress
        If Not cbLoaiYeuCau.IsPopupOpen Then
            cbLoaiYeuCau.ShowPopup()
        End If
    End Sub

    Private Sub cbLoaiYeuCau_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbLoaiYeuCau.ButtonClick
        If e.Button.Index = 1 Then
            cbLoaiYeuCau.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub cbNguonKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNguonKH.ButtonClick
        If e.Button.Index = 1 Then
            cbNguonKH.EditValue = Nothing
        End If
    End Sub
End Class