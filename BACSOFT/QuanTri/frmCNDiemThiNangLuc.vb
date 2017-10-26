Imports BACSOFT.Db.SqlHelper

Public Class frmCNDiemThiNangLuc


    Private Sub frmCNDiemThiNangLuc_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSNhomNangLuc()
        LoadDSNangLuc()
        LoadDSNhanSu()

        If TrangThai.isAddNew Then

            Me.Text = "Thêm điểm thi năng lực"
            tbNgayThi.EditValue = Today.Date
            Dim tb As New DataTable
            tb.Columns.Add("ID", Type.GetType("System.Int32"))
            tb.Columns.Add("IDNhanVien", Type.GetType("System.Int32"))
            tb.Columns.Add("Diem", Type.GetType("System.Double"))
            tb.Columns.Add("Modify", Type.GetType("System.Boolean"))
            gdvFile.DataSource = DataSourceDSFile("")
            If Not tb Is Nothing Then
                gdvNhanVien.DataSource = tb
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        Else
            Me.Text = "Cập nhật điểm thi năng lực"
            'gdvNhanVienCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None

            AddParameterWhere("@ID", objID)
            AddParameterWhere("@LoaiTD", LoaiTuDien.NhomNangLuc)
            Dim ds As DataSet = ExecuteSQLDataSet("SELECT * FROM tblDiemThiNangLuc WHERE ID=@ID SELECT *,Convert(bit,0)Modify FROM tblDiemThiNangLucCT WHERE IDLanThi=@ID ORDER BY Diem DESC ")
            If Not ds Is Nothing Then
                cbNangLuc.EditValue = ds.Tables(0).Rows(0)("IDNangLuc")
                tbNgayThi.EditValue = ds.Tables(0).Rows(0)("NgayThi")
                tbGhiChu.EditValue = ds.Tables(0).Rows(0)("GhiChu").ToString
                gdvNhanVien.DataSource = ds.Tables(1)
                gdvFile.DataSource = DataSourceDSFile(ds.Tables(0).Rows(0)("FileDinhKem"))
                'tbDL = tb2.Copy
                'For i As Integer = 0 To gdvNhanVienCT.DataRowCount - 1
                '    If gdvNhanVienCT.GetRowCellValue(i, "ID") = objID Then
                '        gdvNhanVienCT.ClearSelection()
                '        gdvNhanVienCT.SelectRow(i)
                '        gdvNhanVienCT.FocusedRowHandle = i
                '        gdvNhanVienCT.FocusedColumn = gdvNhanVienCT.Columns("ThoiGian")
                '        gdvNhanVien.ForceInitialize()
                '        gdvNhanVienCT.MakeRowVisible(i, False)
                '        Exit For
                '    End If
                'Next
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If
    End Sub

    '  Private tbDL As New DataTable

    Public Sub LoadDSNhomNangLuc()
        AddParameterWhere("@Nhom", LoaiTuDien.NhomNangLuc)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Nhom ORDER BY Ma")
        If Not tb Is Nothing Then
            cbNhom.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNangLuc()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM tblNangLuc "
        If Not cbNhom.EditValue Is Nothing Then
            AddParameterWhere("@Nhom", cbNhom.EditValue)
            sql &= " WHERE IDNhom=@Nhom "
        End If
        sql &= " ORDER BY Ten "

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbNangLuc.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNhanSu()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 ORDER BY ID")
        If Not tb Is Nothing Then
            rgdvHocVien.DataSource = tb
            With rgdvHocVien.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "Mã"
                colID.Visible = False
                Dim colTen = .AddField("Ten")
                colTen.Caption = "Họ tên"
                colTen.VisibleIndex = 0
                'colTen.Width = 200
                colTen.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        'btTinhDiem.PerformClick()
        gdvNhanVienCT.CloseEditor()
        gdvNhanVienCT.UpdateCurrentRow()

        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()

        Try

            If gdvNhanVienCT.DataRowCount = 0 Then Throw New Exception("Chưa có thông tin nhân viên thi năng lực !")
            If cbNangLuc.EditValue Is Nothing Then Throw New Exception("Chưa có môn thi !")
            'Kiem tra du lieu mon thi ngay do
            'Dim isCoDuLieu As Boolean = False


            BeginTransaction()

            AddParameter("@IDNangLuc", cbNangLuc.EditValue)
            AddParameter("@NgayThi", tbNgayThi.EditValue)
            AddParameter("@GhiChu", tbGhiChu.EditValue)
            AddParameter("@FileDinhKem", StrDSFile(gdvFileCT))
            If TrangThai.isAddNew Then
                objID = doInsert("tblDiemThiNangLuc")
                If objID Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@ID", objID)
                If doUpdate("tblDiemThiNangLuc", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
            gdvNhanVienCT.BeginUpdate()
            For i As Integer = 0 To gdvNhanVienCT.RowCount - 1
                If gdvNhanVienCT.GetRowCellValue(i, "Modify") Then
                    AddParameter("@IDLanThi", objID)
                    AddParameter("@IDNhanVien", gdvNhanVienCT.GetRowCellValue(i, "IDNhanVien"))
                    AddParameter("@Diem", gdvNhanVienCT.GetRowCellValue(i, "Diem"))
                    If gdvNhanVienCT.GetRowCellValue(i, "ID").ToString.Trim = "" Then
                        Dim _tmp As Object = doInsert("tblDiemThiNangLucCT")
                        If _tmp Is Nothing Then
                            gdvNhanVienCT.EndUpdate()
                            Throw New Exception(LoiNgoaiLe)
                        End If

                        gdvNhanVienCT.SetRowCellValue(i, "ID", _tmp)
                    Else
                        AddParameterWhere("@ID", gdvNhanVienCT.GetRowCellValue(i, "ID"))
                        If doUpdate("tblDiemThiNangLucCT", "ID=@ID") Is Nothing Then
                            gdvNhanVienCT.EndUpdate()
                            Throw New Exception(LoiNgoaiLe)
                        End If

                    End If
                    gdvNhanVienCT.SetRowCellValue(i, "Modify", False)
                End If
            Next
            gdvNhanVienCT.EndUpdate()
            gdvNhanVienCT.CloseEditor()
            gdvNhanVienCT.UpdateCurrentRow()

            ComitTransaction()
            TrangThai.isUpdate = True


            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmTHDiemThiNangLuc).LoadDS()
            ShowAlert(Me.Text & " thành công !")

        Catch ex As Exception
            RollBackTransaction()
            If TrangThai.isAddNew = True Then
                gdvNhanVienCT.BeginUpdate()
                For i As Integer = 0 To gdvNhanVienCT.RowCount - 1
                    gdvNhanVienCT.SetRowCellValue(i, "ID", DBNull.Value)
                    gdvNhanVienCT.SetRowCellValue(i, "Modify", True)
                Next
                gdvNhanVienCT.EndUpdate()
            End If
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub gdvNhanVien_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvNhanVien.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuNV.ShowPopup(gdvNhanVien.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mThemNV_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemNV.ItemClick
        gdvNhanVienCT.AddNewRow()

        'SendKeys.Send("{F4}")

    End Sub

    Private Sub gdvNhanVienCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvNhanVienCT.InitNewRow
        gdvNhanVienCT.SetRowCellValue(e.RowHandle, "Diem", 0)
        gdvNhanVienCT.SetRowCellValue(e.RowHandle, "Modify", True)
        gdvNhanVienCT.Focus()
        gdvNhanVienCT.FocusedColumn = gdvNhanVienCT.Columns("IDNhanVien")
        SendKeys.Send("{F4}")
    End Sub

    Private Sub cbNhom_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNhom.ButtonClick
        If e.Button.Index = 1 Then
            cbNhom.EditValue = Nothing
        End If
    End Sub

    Private Sub frmCNNangLuc_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNangLuc.ButtonClick
        If e.Button.Index = 1 Then
            Dim _New As Boolean = TrangThai.isAddNew
            Dim _Update As Boolean = TrangThai.isUpdate
            Dim _tmpObjID As Object = objID
            If cbNangLuc.EditValue Is Nothing Then
                TrangThai.isAddNew = True
            Else
                TrangThai.isUpdate = True
                objID = cbNangLuc.EditValue
            End If
            fCNNangLuc = New frmCNNangLuc
            fCNNangLuc.Tag = "DIEMTHI"
            fCNNangLuc.cbNhom.EditValue = cbNhom.EditValue
            fCNNangLuc.ShowDialog()

            TrangThai.isAddNew = _New
            TrangThai.isUpdate = _Update
            objID = _tmpObjID


        ElseIf e.Button.Index = 2 Then
            cbNangLuc.EditValue = Nothing
        End If
    End Sub

    Private Sub cbNhom_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNhom.EditValueChanged
        LoadDSNangLuc()
    End Sub

    Private Sub mThemFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemFile.ItemClick
        If TrangThai.isAddNew Then
            ShowCanhBao("Bạn phải lưu lại trước khi thêm file!")
            Exit Sub
        End If
        Dim path As String = ""
        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not System.IO.Directory.Exists(UrlThiNangLuc) Then
                System.IO.Directory.CreateDirectory(UrlThiNangLuc)
            End If
            For Each file In OpenFile.FileNames
                If TrangThai.isUpdate Then
                    ShowWaiting("Đang chuyển file lên server ...")
                    path = IO.Path.GetFileNameWithoutExtension(file) & " " & TaiKhoan.ToString & IO.Path.GetExtension(file)
                    Try
                        IO.File.Copy(file, UrlThiNangLuc & path)
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
        End If
        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()
    End Sub

    Private Sub mXoaFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaFile.ItemClick
        If ShowCauHoi("Xóa file được chọn ?") Then
            If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
            Try
                IO.File.Delete(UrlThiNangLuc & "\" & gdvFileCT.GetFocusedRowCellValue("File"))
                gdvFileCT.DeleteSelectedRows()
            Catch ex As Exception
                If Not IO.File.Exists(UrlThiNangLuc & "\" & gdvFileCT.GetFocusedRowCellValue("File")) Then
                    gdvFileCT.DeleteSelectedRows()
                End If
                ShowBaoLoi(ex.Message)
            End Try
        End If
        
    End Sub

    Private Sub gdvFileCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvFileCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuFile.ShowPopup(gdvFile.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvNhanVienCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvNhanVienCT.CellValueChanged
        If e.Column.FieldName <> "Modify" Then
            gdvNhanVienCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub
End Class