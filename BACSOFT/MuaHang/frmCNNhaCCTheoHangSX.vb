Imports BACSOFT.Db.SqlHelper

Public Class frmCNNhaCCTheoHangSX

    Private Sub frmCNNhaCCTheoHangSX_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadCb()
        If TrangThai.isAddNew Then
            Me.Text = "Thêm nhà cung cấp cho một hãng vật tư/hàng hóa"
            cbPhuTrach.EditValue = Convert.ToInt32(TaiKhoan)
            gdvFile.DataSource = DataSourceDSFile("")
            tbNgay.EditValue = Today.Date
        Else
            Me.Text = "Cập nhật nhà cung cấp cho hãng vật tư/hàng hóa"
            AddParameter("@ID", objID)
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblNhaCCTHeoHangSX WHERE ID=@ID")
            If Not tb Is Nothing Then
                gdvNhaCC.EditValue = tb.Rows(0)("IDNCC")
                cbHangSX.EditValue = tb.Rows(0)("IDHangSX")
                cbPhuTrach.EditValue = tb.Rows(0)("IDPhuTrach")
                tbMoTa.EditValue = tb.Rows(0)("MoTa")
                tbNgay.EditValue = tb.Rows(0)("NgayNhap")
                gdvFile.DataSource = DataSourceDSFile(tb.Rows(0)("FileDinhKem").ToString)
            End If
        End If
    End Sub

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvFileCT.CalcHitInfo(gdvFile.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvFile_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvFile.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdvFile.PointToScreen(e.Location))
        End If
    End Sub

    Public Sub LoadCb()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        sql &= " SELECT ID,ttcMa,Ten FROM KHACHHANG WHERE ttcKhachhang <>1 order By ttcMa"
        sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 and TrangThai=1"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbHangSX.Properties.DataSource = ds.Tables(0)
            gdvNhaCC.Properties.DataSource = ds.Tables(1)
            cbPhuTrach.Properties.DataSource = ds.Tables(2)
        End If
    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        TrangThai.isAddNew = True
        cbHangSX.EditValue = Nothing
        gdvNhaCC.EditValue = Nothing
        cbPhuTrach.EditValue = Convert.ToInt32(TaiKhoan)
        tbMoTa.EditValue = Nothing
        gdvFile.DataSource = DataSourceDSFile("")
    End Sub

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        AddParameter("@IDHangSX", cbHangSX.EditValue)
        AddParameter("@IDNCC", gdvNhaCC.EditValue)
        AddParameter("@IDPhuTrach", cbPhuTrach.EditValue)
        AddParameter("@MoTa", tbMoTa.EditValue)
        AddParameter("@NgayNhap", tbNgay.EditValue)
        AddParameter("@FileDinhKem", StrDSFile(gdvFileCT))
        If TrangThai.isAddNew Then
            objID = doInsert("tblNhaCCTheoHangSX")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm nhà cung cấp theo hãng vật tư/hàng hóa")
                TrangThai.isUpdate = True
                Me.Text = "Cập nhật nhà cung cấp theo hãng vật tư/hàng hóa"
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNhaCCTheoHangSX).LoadDS()
            End If
        Else
            AddParameterWhere("@ID", objID)
            If doUpdate("tblNhaCCTheoHangSX", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật nhà cung cấp theo hãng vật tư/hàng hóa")
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNhaCCTheoHangSX).LoadDS()
            End If
        End If
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub mThemFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemFile.ItemClick
        If TrangThai.isAddNew Then
            ShowCanhBao("Phải lưu lại một lần trước khi thêm file đính kèm !")
        End If
        Dim path As String = ""
        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            If Not System.IO.Directory.Exists(UrlNhaCC) Then
                System.IO.Directory.CreateDirectory(UrlNhaCC)
            End If
            ShowWaiting("Đang chuyển file lên server ...")
            For Each file In OpenFile.FileNames

                path = IO.Path.GetFileNameWithoutExtension(file) & " " & TaiKhoan.ToString & " " & objID & IO.Path.GetExtension(file)
                Try
                    IO.File.Copy(file, UrlNhaCC & "\" & path)
                    gdvFileCT.AddNewRow()
                    gdvFileCT.SetFocusedRowCellValue("File", path)
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try

            Next
            Impersonator.EndImpersonation()
            CloseWaiting()
        End If
        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()
    End Sub

    Private Sub mXoaFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaFile.ItemClick
        If ShowCauHoi("Xóa file được chọn ?") Then
            If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()
            Try
                IO.File.Delete(UrlNhaCC & gdvFileCT.GetFocusedRowCellValue("File"))
                gdvFileCT.DeleteSelectedRows()
            Catch ex As Exception
                If Not IO.File.Exists(UrlNhaCC & gdvFileCT.GetFocusedRowCellValue("File")) Then
                    gdvFileCT.DeleteSelectedRows()
                End If
                ShowBaoLoi(ex.Message)
            End Try
            Impersonator.EndImpersonation()
            gdvFileCT.CloseEditor()
            gdvFileCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub cbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbHangSX.ButtonClick
        If e.Button.Index = 1 Then
            cbHangSX.EditValue = Nothing
        End If
    End Sub

    Private Sub gdvNhaCC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gdvNhaCC.ButtonClick
        If e.Button.Index = 1 Then
            gdvNhaCC.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhuTrach_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbPhuTrach.ButtonClick
        If e.Button.Index = 1 Then
            cbPhuTrach.EditValue = Nothing
        End If
    End Sub
End Class