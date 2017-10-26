Imports BACSOFT.Db.SqlHelper

Public Class frmPhanBoLoiNhuan

    Public _exit As Boolean = False
    Public _exit2 As Boolean = False

    Private Sub frmPhanBoLoiNhuan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tbThang.EditValue = Today.Month
        tbNam.EditValue = Today.Year
        'tbThang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        'tbNam.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        loadDSLoaiCT()
        loadDSPhanBoLN()
    End Sub

    Public Sub loadDSLoaiCT()
        Dim sql As String = ""
        Dim _Thang As String = ""
        If tbThang.EditValue.ToString.Length = 1 Then
            _Thang = "0" + tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString
        Else
            _Thang = tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString
        End If

        AddParameterWhere("@Thang", _Thang)
        sql = "SELECT * FROM LoaiCongTrinh "
        sql &= " LEFT JOIN LoaiCongTrinh_LNThietKe ON LoaiCongTrinh.ID=LoaiCongTrinh_LNThietKe.IDLoaiCT AND Thang=@Thang"
        sql &= " ORDER BY STT"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Public Sub loadDSPhanBoLN()
        Dim sql As String = ""

        sql = "SELECT * FROM PhanBoLoiNhuan "
        sql &= " ORDER BY right(thang,4) DESC, left(thang,2) DESC"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvPhanBoLN.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa: " & gdvCT.GetFocusedRowCellValue("NoiDung") & " ?") Then
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doDelete("LoaiCongTrinh", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    'Dim sql As String = "ALTER TABLE tblDinhMucDiem DROP Column C" & gdvCT.GetFocusedRowCellValue("ID")
                    'If ExecuteSQLNonQuery(sql) Is Nothing Then
                    '    ShowCanhBao(LoiNgoaiLe)
                    'End If
                    'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub

    Private Sub gdvPhanBoLNCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvPhanBoLNCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa: " & gdvPhanBoLNCT.GetFocusedRowCellValue("ID") & " ?") Then
                AddParameterWhere("@ID", gdvPhanBoLNCT.GetFocusedRowCellValue("ID"))
                If doDelete("PhanBoLoiNhuan", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    'Dim sql As String = "ALTER TABLE tblDinhMucDiem DROP Column C" & gdvCT.GetFocusedRowCellValue("ID")
                    'If ExecuteSQLNonQuery(sql) Is Nothing Then
                    '    ShowCanhBao(LoiNgoaiLe)
                    'End If
                    'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDinhMucDiemCongTrinh).LoadDSDinhMuc()
                    gdvPhanBoLNCT.DeleteSelectedRows()
                    ShowAlert("Đã xoá!")
                End If
            End If
        End If
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        If XtraTabControl1.SelectedTabPage Is XtraTabPage2 Then
            loadDSLoaiCT()
        Else
            loadDSPhanBoLN()
        End If

    End Sub

    Dim _IDMain As Integer = -1
    Dim _IDCT As Integer = -1

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Then
            If _exit And _IDMain > -1 Then
                _IDMain = -1
                _exit = False
            Else
                AddParameter("@STT", gdvCT.GetFocusedRowCellValue("STT"))
                AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
                AddParameter("@MoTa", gdvCT.GetFocusedRowCellValue("MoTa"))
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("LoaiCongTrinh", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    loadDSLoaiCT()
                End If
            End If

        Else
            AddParameter("@STT", gdvCT.GetFocusedRowCellValue("STT"))
            AddParameter("@NoiDung", gdvCT.GetFocusedRowCellValue("NoiDung"))
            AddParameter("@MoTa", gdvCT.GetFocusedRowCellValue("MoTa"))
            objID = doInsert("LoaiCongTrinh")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvCT.DeleteSelectedRows()
                loadDSLoaiCT()
            Else
                _exit = True
                _IDMain = objID
                gdvCT.SetRowCellValue(e.RowHandle, "ID", objID)
            End If
        End If

        Dim sql As String = ""
        Dim _Thang As String = ""
        If tbThang.EditValue.ToString.Length = 1 Then
            _Thang = "0" + tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString
        Else
            _Thang = tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString
        End If

        If Not IsDBNull(gdvCT.GetFocusedRowCellValue("IDLoaiCT")) Then
            If _exit2 And _IDCT > -1 Then
                _IDCT = -1
                _exit2 = False
            Else
                AddParameter("@LNThietKe", gdvCT.GetFocusedRowCellValue("LNThietKe"))
                AddParameterWhere("@Thang", _Thang)
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("IDLoaiCT"))
                If doUpdate("LoaiCongTrinh_LNThietKe", "IDLoaiCT=@ID AND Thang=@Thang") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    loadDSLoaiCT()
                Else
                    ShowAlert("Đã cập nhật!")
                End If
            End If

        Else
            If _exit And _IDMain > 0 Then
                AddParameter("@IDLoaiCT", _IDMain)
            Else
                AddParameter("@IDLoaiCT", gdvCT.GetFocusedRowCellValue("ID"))
            End If

            AddParameter("@LNThietKe", gdvCT.GetFocusedRowCellValue("LNThietKe"))
            AddParameter("@Thang", _Thang)
            ' objID = 
            If doInsert("LoaiCongTrinh_LNThietKe") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvCT.DeleteSelectedRows()
                loadDSLoaiCT()
            Else

                _exit2 = True
                _IDCT = 0
                gdvCT.SetFocusedRowCellValue("IDLoaiCT", gdvCT.GetFocusedRowCellValue("ID"))

                ShowAlert("Đã cập nhật!")

            End If
        End If
    End Sub

    Private Sub gdvPhanBoLNCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvPhanBoLNCT.RowUpdated
        If Not IsDBNull(gdvPhanBoLNCT.GetFocusedRowCellValue("ID")) Then
            If _exit And _IDMain > -1 Then
                _IDMain = -1
                _exit = False
            Else
                AddParameter("@Thang", gdvPhanBoLNCT.GetFocusedRowCellValue("Thang"))
                AddParameter("@XucTienKyHD", gdvPhanBoLNCT.GetFocusedRowCellValue("XucTienKyHD"))
                AddParameter("@PhuTrachHopDong", gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachHopDong"))
                AddParameter("@PhuTrachChaoGia", gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachChaoGia"))
                AddParameter("@PhuTrachQuanLyCT", gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachQuanLyCT"))
                AddParameter("@TongConLai", gdvPhanBoLNCT.GetFocusedRowCellValue("TongConLai"))
                AddParameter("@LoiNhuanChuyenMa", gdvPhanBoLNCT.GetFocusedRowCellValue("LoiNhuanChuyenMa"))
                AddParameter("@CT", gdvPhanBoLNCT.GetFocusedRowCellValue("CT"))
                AddParameterWhere("@ID", gdvPhanBoLNCT.GetFocusedRowCellValue("ID"))
                If doUpdate("PhanBoLoiNhuan", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    loadDSLoaiCT()
                End If
            End If

        Else
            AddParameter("@Thang", gdvPhanBoLNCT.GetFocusedRowCellValue("Thang"))
            AddParameter("@XucTienKyHD", gdvPhanBoLNCT.GetFocusedRowCellValue("XucTienKyHD"))
            AddParameter("@PhuTrachHopDong", gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachHopDong"))
            AddParameter("@PhuTrachChaoGia", gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachChaoGia"))
            AddParameter("@PhuTrachQuanLyCT", gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachQuanLyCT"))
            AddParameter("@TongConLai", gdvPhanBoLNCT.GetFocusedRowCellValue("TongConLai"))
            AddParameter("@LoiNhuanChuyenMa", gdvPhanBoLNCT.GetFocusedRowCellValue("LoiNhuanChuyenMa"))
            AddParameter("@CT", gdvPhanBoLNCT.GetFocusedRowCellValue("CT"))
            Dim _objID As Object = doInsert("PhanBoLoiNhuan")
            If _objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                gdvPhanBoLNCT.DeleteSelectedRows()
                loadDSPhanBoLN()
            Else
                _exit = True
                _IDMain = 0
                gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "ID", _objID)
            End If
        End If

       
    End Sub

    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetFocusedRowCellValue("STT", gdvCT.GetRowCellValue(gdvCT.RowCount - 2, "STT") + 1)
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Dinh Muc Diem.xls"
        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    'Private Sub XtraTabControl1_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
    '    If e.Page Is XtraTabPage2 Then
    '        tbThang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
    '        tbNam.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
    '    Else
    '        tbThang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
    '        tbNam.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
    '    End If
    'End Sub

    Private Sub gdvPhanBoLNCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvPhanBoLNCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "XucTienKyHD", "PhuTrachHopDong", "PhuTrachChaoGia", "PhuTrachQuanLyCT"
                Dim _v As Double = 100 - gdvPhanBoLNCT.GetFocusedRowCellValue("XucTienKyHD") - gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachHopDong") - gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachChaoGia") - gdvPhanBoLNCT.GetFocusedRowCellValue("PhuTrachQuanLyCT")
                gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "TongConLai", _v)
        End Select
    End Sub

    Private Sub gdvPhanBoLNCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvPhanBoLNCT.InitNewRow
        On Error Resume Next
        gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "XucTienKyHD", 0)
        gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "PhuTrachHopDong", 0)
        gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "PhuTrachChaoGia", 0)
        gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "PhuTrachQuanLyCT", 0)
        gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "TongConLai", 100)
        gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "LoiNhuanChuyenMa", 0)
        gdvPhanBoLNCT.SetRowCellValue(e.RowHandle, "CT", False)
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick

        If ShowCauHoi("Lấy dữ liệu tháng " & DateAdd(DateInterval.Day, -1, New DateTime(tbNam.EditValue, tbThang.EditValue, 1)).ToString("MM/yyyy") & " sang tháng " & New DateTime(tbNam.EditValue, tbThang.EditValue, 1).ToString("MM/yyyy")) Then
            If XtraTabControl1.SelectedTabPage Is XtraTabPage1 Then
                Dim sql As String = ""
                sql &= " INSERT INTO PhanBoLoiNhuan(Thang,XucTienKyHD,PhuTrachHopDong,PhuTrachChaoGia,PhuTrachQuanLyCT,TongConLai,LoiNhuanChuyenMa,CT)"
                sql &= " SELECT @Thang,XucTienKyHD,PhuTrachHopDong,PhuTrachChaoGia,PhuTrachQuanLyCT,TongConLai,LoiNhuanChuyenMa,CT"
                sql &= " FROM PhanBoLoiNhuan"
                sql &= " WHERE Thang=@ThangTruoc"
                AddParameterWhere("@ThangTruoc", DateAdd(DateInterval.Day, -1, New DateTime(tbNam.EditValue, tbThang.EditValue, 1)).ToString("MM/yyyy"))
                AddParameterWhere("@Thang", New DateTime(tbNam.EditValue, tbThang.EditValue, 1).ToString("MM/yyyy"))
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã cập nhật !")
                    btXem.PerformClick()
                End If
            Else
                Dim sql As String = ""
                sql &= " INSERT INTO LoaiCongTrinh_LNThietKe(IDLoaiCT,Thang,LNThietKe)"
                sql &= " SELECT IDLoaiCT,@Thang,LNThietKe "
                sql &= " FROM LoaiCongTrinh_LNThietKe"
                sql &= " WHERE Thang=@ThangTruoc"
                AddParameterWhere("@ThangTruoc", DateAdd(DateInterval.Day, -1, New DateTime(tbNam.EditValue, tbThang.EditValue, 1)).ToString("MM/yyyy"))
                AddParameterWhere("@Thang", New DateTime(tbNam.EditValue, tbThang.EditValue, 1).ToString("MM/yyyy"))
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã cập nhật !")
                    btXem.PerformClick()
                End If
            End If
            
        End If
    End Sub
End Class
