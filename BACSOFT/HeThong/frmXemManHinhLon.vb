Imports BACSOFT.Db.SqlHelper

Public Class frmXemManHinhLon
    Public Shared CountChuyenMa As Integer = 0
    Public Shared CountHoiGia As Integer = 0

    Private Sub frmXemManHinhLon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'PhanQuyen()
        Application.DoEvents()
        ShowWaiting("Đang tải dữ liệu...")
        LoadDuLieuChuyenMa()
        Application.DoEvents()
        LoadDuLieuHoiGia()
        Application.DoEvents()
        TimeLoadData.Interval = tbSoGiay.EditValue * 1000
        TimeLoadData.Start()
        Application.DoEvents()
        LoadDsPhong()
        Application.DoEvents()
        LoadDsNhanVien()
        Application.DoEvents()
        LoadDsNhomChiTieu()
        tbNam.EditValue = Today.Year
        CloseWaiting()
        If My.Computer.Name <> "BACBOSS" Then
            cbPhong.EditValue = Convert.ToInt32(MaPhongBan)
            cbNhanVien.EditValue = Convert.ToInt32(TaiKhoan)
            tabChinh.SelectedTabPage = tabChiTieu
        End If

    End Sub

#Region "Yêu cầu cần xử lý"

    Public Sub LoadDuLieuChuyenMa()

        Dim sql As String = "SELECT YEUCAUDEN.Sophieu, YEUCAUDEN.NgayNhanYeuCau,KHACHHANG.ttcMa,KHACHHANG.Ten,YEUCAUDEN.Noidung,NGUOIXULY.Ten AS NguoiXuLy,CHAMSOC.Ten AS TakeCare,YEUCAUDEN.Trangthai"
        sql &= " FROM YEUCAUDEN LEFT OUTER JOIN BANGYEUCAU ON YEUCAUDEN.Sophieu=BANGYEUCAU.Sophieu"
        sql &= " LEFT OUTER JOIN KHACHHANG ON BANGYEUCAU.IDkhachhang=KHACHHANG.ID"
        sql &= " LEFT OUTER JOIN NHANSU AS CHAMSOC ON BANGYEUCAU.IDTakecare=CHAMSOC.ID"
        sql &= " LEFT OUTER JOIN NHANSU AS NGUOIXULY ON YEUCAUDEN.IDNhanChuyenMa=NGUOIXULY.ID"
        sql &= " WHERE YEUCAUDEN.Trangthai=1 ORDER BY NgayNhanYeuCau "


        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvChuyenMa.DataSource = tb
            lbSoYCCanXuLy.Text = tb.Rows.Count

        Else

            ShowBaoLoi(LoiNgoaiLe)
        End If
        CountChuyenMa = 0
    End Sub

    Public Sub LoadDuLieuHoiGia()
        Dim sql As String = "SELECT YEUCAUDEN.Sophieu, YEUCAUDEN.NgayNhanYeuCau,KHACHHANG.ttcMa,KHACHHANG.Ten,YEUCAUDEN.Noidung,NGUOIXULY.Ten AS NguoiXuLy,CHAMSOC.Ten AS TakeCare,YEUCAUDEN.Trangthai,VATTU.Model,TENHANGSANXUAT.Ten AS HangSX"
        sql &= " FROM YEUCAUDEN LEFT OUTER JOIN BANGYEUCAU ON YEUCAUDEN.Sophieu=BANGYEUCAU.Sophieu"
        sql &= " LEFT OUTER JOIN KHACHHANG ON BANGYEUCAU.IDkhachhang=KHACHHANG.ID"
        sql &= " LEFT OUTER JOIN NHANSU AS CHAMSOC ON BANGYEUCAU.IDTakecare=CHAMSOC.ID"
        sql &= " LEFT OUTER JOIN VATTU ON YEUCAUDEN.IDVatTu=VATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN NHANSU AS NGUOIXULY ON YEUCAUDEN.IDNhanBaoGia=NGUOIXULY.ID"
        sql &= " WHERE YEUCAUDEN.Trangthai=3 ORDER BY Ngaythang "


        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvHoiGia.DataSource = tb
            lbSoYCCanHoiGia.Text = tb.Rows.Count

        Else

            ShowBaoLoi(LoiNgoaiLe)
        End If
        CountHoiGia = 0
    End Sub

    Public Sub loadVatTuDangVe()
        Dim sqlCanXuat As String = "Select KHACHHANG.ttcMa,KHACHHANG.Ten AS TenKH,Sophieu, ngaythang, "
        sqlCanXuat &= " TENVATTU.Ten AS TenVT, TENHANGSANXUAT.Ten AS TenHang, Model, sluong,SLtON,sluongve,"
        sqlCanXuat &= " Lichve,V_CANXUAT.ID,Ngaynhan, NHANSU.Ten AS TakeCare"
        sqlCanXuat &= " from  V_CANXUAT LEFT OUTER JOIN KHACHHANG ON V_CANXUAT.IDKhachhang=KHACHHANG.ID"
        sqlCanXuat &= " LEFT OUTER JOIN TENVATTU ON V_CANXUAT.IDTenvattu=TENVATTU.ID"
        sqlCanXuat &= " LEFT OUTER JOIN TENHANGSANXUAT ON V_CANXUAT.IDHangsanxuat=TENHANGSANXUAT.ID"
        sqlCanXuat &= " LEFT OUTER JOIN NHANSU ON V_CANXUAT.IDTakecare=NHANSU.ID"
        sqlCanXuat &= " where IDTenvattu >= 0 AND sLuong > (ISNULL(SLtON,0) + ISNULL(sluongve,0))"

        Dim tb As DataTable = ExecuteSQLDataTable(sqlCanXuat)
        If Not tb Is Nothing Then
            'gdvCanXuat.DataSource = tb

        Else

            ShowBaoLoi(LoiNgoaiLe)
        End If
        ' CountCanXuat = 0
    End Sub

#End Region

#Region "Chỉ tiêu"
    Public Sub LoadDsPhong()
        Dim sql As String = "SELECT ID,Ten FROM DEPATMENT"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDsNhanVien()
        Dim sql As String = ""
        If cbPhong.EditValue Is Nothing Then
            sql &= "SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER By ID"
        Else
            sql &= "SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment=@IDPhong ORDER By ID"
            AddParameterWhere("@IDPhong", cbPhong.EditValue)
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbNhanVien.DataSource = tb
            'tbNhanVien = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDsNhomChiTieu()
        AddParameterWhere("@Loai", LoaiTuDien.NhomChiTieu)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            rcbNhomChiTieu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Public Sub LoadDSChiTieuPhong()
        Application.DoEvents()
        ShowWaiting("Đang tải dữ liệu...")
        Dim sql As String = ""
        sql &= " SELECT * FROM"
        sql &= " ("
        sql &= " SELECT DISTINCT  IDChiTieu, tblTuDien.IDP AS IDNhom, tblTuDien.NoiDung, "

        For i As Integer = 1 To 12
            If i < 10 Then
                sql &= " Convert(float,0) AS [0" & i.ToString & "/" & tbNam.EditValue.ToString & "],"
            Else
                sql &= " Convert(float,0) AS [" & i.ToString & "/" & tbNam.EditValue.ToString & "],"
            End If
        Next

        sql &= " Convert(float,0) AS Tong,	tblTuDien.Ma"
        sql &= " FROM tblChiTieu"
        sql &= " INNER JOIN tblTuDien ON  tblTuDien.Loai=@ChiTieu AND tblChiTieu.IDChiTieu=tblTuDien.ID"
        sql &= " WHERE IDThucHien=@Phong AND tblChiTieu.Loai=0"
        sql &= " )tb"
        sql &= " ORDER BY Ma"

        sql &= " SELECT IDChiTieu,Thang,ChiTieu FROM tblChiTieu WHERE right(Thang,4)=@Nam AND IDThucHien=@Phong "

        AddParameterWhere("@Phong", cbPhong.EditValue)
        AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
        AddParameterWhere("@Nam", tbNam.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvCT.Columns.Clear()
            gdv.DataSource = ds.Tables(0)
            gdvCT.BeginUpdate()
            gdvCT.Columns("IDChiTieu").Visible = False
            gdvCT.Columns("Ma").Visible = False
            gdvCT.Columns("IDNhom").GroupIndex = 0
            gdvCT.Columns("IDNhom").Caption = "Nhóm"
            gdvCT.Columns("IDNhom").ColumnEdit = rcbNhomChiTieu

            gdvCT.Columns("NoiDung").OptionsColumn.ReadOnly = True
            gdvCT.Columns("NoiDung").VisibleIndex = 0
            gdvCT.Columns("NoiDung").Width = 200
            gdvCT.Columns("NoiDung").Caption = "Chỉ tiêu"

            gdvCT.Columns("Tong").Caption = "Tổng"
            gdvCT.Columns("Tong").OptionsColumn.ReadOnly = True
            For i As Integer = 3 To gdvCT.Columns.Count - 1
                gdvCT.Columns(i).ColumnEdit = tbN0
            Next

            For i As Integer = 0 To gdvCT.DataRowCount - 1
                Dim _Tong As Double = 0
                For j As Integer = 0 To gdvCT.Columns.Count - 1
                    Dim r() As DataRow = ds.Tables(1).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.Columns(j).FieldName.ToString & "'")
                    If r.Length > 0 Then
                        gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, r(0)("ChiTieu"))
                        _Tong += r(0)("ChiTieu")
                    End If
                Next
                gdvCT.SetRowCellValue(i, "Tong", _Tong)

            Next
            gdvCT.EndUpdate()
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()

            CloseWaiting()

        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSChiTieuMotNhanVien()
        ShowWaiting("Đang tải dữ liệu...")
        Dim sql As String = ""
        sql &= " SELECT * FROM"
        sql &= " ("
        sql &= " SELECT DISTINCT  IDChiTieu, tblTuDien.IDP AS IDNhom, tblTuDien.NoiDung, "

        For i As Integer = 1 To 12
            If i < 10 Then
                sql &= " Convert(float,0) AS [0" & i.ToString & "/" & tbNam.EditValue.ToString & "],"
            Else
                sql &= " Convert(float,0) AS [" & i.ToString & "/" & tbNam.EditValue.ToString & "],"
            End If
        Next

        sql &= " Convert(float,0) AS Tong,	tblTuDien.Ma"
        sql &= " FROM tblChiTieu"
        sql &= " INNER JOIN tblTuDien ON  tblTuDien.Loai=@ChiTieu AND tblChiTieu.IDChiTieu=tblTuDien.ID"
        sql &= " WHERE IDThucHien=@NhanVien AND tblChiTieu.Loai=1"
        sql &= " )tb"
        sql &= " ORDER BY Ma"

        sql &= " SELECT IDChiTieu,Thang,ChiTieu FROM tblChiTieu WHERE right(Thang,4)=@Nam AND IDThucHien=@NhanVien AND tblChiTieu.Loai=1 "

        AddParameterWhere("@NhanVien", cbNhanVien.EditValue)
        AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
        AddParameterWhere("@Nam", tbNam.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvCT.Columns.Clear()
            gdv.DataSource = ds.Tables(0)
            gdvCT.BeginUpdate()
            gdvCT.Columns("IDChiTieu").Visible = False
            gdvCT.Columns("Ma").Visible = False
            gdvCT.Columns("IDNhom").GroupIndex = 0
            gdvCT.Columns("IDNhom").Caption = "Nhóm"
            gdvCT.Columns("IDNhom").ColumnEdit = rcbNhomChiTieu

            gdvCT.Columns("NoiDung").OptionsColumn.ReadOnly = True
            gdvCT.Columns("NoiDung").VisibleIndex = 0
            gdvCT.Columns("NoiDung").Width = 200
            gdvCT.Columns("NoiDung").Caption = "Chỉ tiêu"

            gdvCT.Columns("Tong").Caption = "Tổng"
            gdvCT.Columns("Tong").OptionsColumn.ReadOnly = True
            For i As Integer = 3 To gdvCT.Columns.Count - 1
                gdvCT.Columns(i).ColumnEdit = tbN0
            Next


            For i As Integer = 0 To gdvCT.DataRowCount - 1
                Dim _Tong As Double = 0
                For j As Integer = 0 To gdvCT.Columns.Count - 1
                    Dim r() As DataRow = ds.Tables(1).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.Columns(j).FieldName.ToString & "'")
                    If r.Length > 0 Then
                        gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, r(0)("ChiTieu"))
                        _Tong += r(0)("ChiTieu")
                    End If
                Next
                gdvCT.SetRowCellValue(i, "Tong", _Tong)

            Next
            gdvCT.EndUpdate()
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()

            CloseWaiting()

        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSChiTieuTatCaNhanVien()
        ShowWaiting("Đang tải dữ liệu...")
        Dim tbNV As DataTable = rcbNhanVien.DataSource
        Dim sql As String = ""
        sql &= " SELECT DISTINCT tblChiTieu.IDChiTieu,tblChiTieu.Thang,DSCHITIEU.NoiDung,(SELECT NoiDung FROM tblTuDien WHERE tblTuDien.ID=DSCHITIEU.IDP) AS NhomCT,"
        For i As Integer = 0 To tbNV.Rows.Count - 1
            sql &= " Convert(float,0) AS [" & tbNV.Rows(i)("ID").ToString & "],"
        Next

        sql &= " Convert(float,0) AS Tong, Convert(float,0) AS ChiTieuPhong"
        sql &= " FROM tblChiTieu"
        sql &= " INNER JOIN tblTuDien As DSCHITIEU ON DSCHITIEU.ID=tblChiTieu.IDChiTieu"
        sql &= " WHERE right(tblChiTieu.Thang,4)=@Nam AND tblChiTieu.Loai=1 AND tblChiTieu.IDThucHien IN (SELECT ID FROM NHANSU WHERE Noictac=74 AND iddepatment=@IDPhong)"

        sql &= " SELECT IDChiTieu,Thang,ChiTieu,IDThucHien FROM tblChiTieu WHERE right(Thang,4)=@Nam AND IDThucHien IN (SELECT ID FROM NHANSU WHERE Noictac=74 AND iddepatment=@IDPhong) AND tblChiTieu.Loai=1 "
        sql &= " SELECT IDChiTieu,Thang,ChiTieu,IDThucHien FROM tblChiTieu WHERE right(Thang,4)=@Nam AND IDThucHien =@IDPhong AND tblChiTieu.Loai=0 "

        AddParameterWhere("@IDPhong", cbPhong.EditValue)
        AddParameterWhere("@Nam", tbNam.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvCT.Columns.Clear()
            gdv.DataSource = ds.Tables(0)
            gdvCT.BeginUpdate()
            gdvCT.Columns("IDChiTieu").Visible = False
            gdvCT.Columns("Thang").Caption = "Tháng"
            gdvCT.Columns("Thang").GroupIndex = 0

            gdvCT.Columns("NhomCT").Caption = "Nhóm"
            gdvCT.Columns("NhomCT").GroupIndex = 1

            gdvCT.Columns("NoiDung").OptionsColumn.ReadOnly = True
            gdvCT.Columns("NoiDung").VisibleIndex = 0
            gdvCT.Columns("NoiDung").Width = 200
            gdvCT.Columns("NoiDung").Caption = "Chỉ tiêu"
            gdvCT.Columns("NoiDung").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("ChiTieuPhong").Caption = "Chỉ tiêu phòng"
            gdvCT.Columns("ChiTieuPhong").OptionsColumn.ReadOnly = True
            gdvCT.Columns("ChiTieuPhong").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
                gdvCT.Columns("ChiTieuPhong").VisibleIndex = -1
                gdvCT.Columns("ChiTieuPhong").Visible = False
            Else
                gdvCT.Columns("ChiTieuPhong").VisibleIndex = 1
            End If


            gdvCT.Columns("Tong").Caption = "Tổng"
            gdvCT.Columns("Tong").OptionsColumn.ReadOnly = True
            gdvCT.Columns("Tong").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("Tong").VisibleIndex = 2


            For i As Integer = 0 To gdvCT.Columns.Count - 1
                If IsNumeric(gdvCT.Columns(i).FieldName) Then
                    Dim r() As DataRow = tbNV.Select("ID=" & gdvCT.Columns(i).FieldName)
                    If r.Length > 0 Then
                        gdvCT.Columns(i).Caption = r(0)("Ten")
                    End If
                    gdvCT.Columns(i).ColumnEdit = tbN0
                End If

            Next


            For i As Integer = 0 To gdvCT.DataRowCount - 1
                Dim _Tong As Double = 0
                For j As Integer = 0 To gdvCT.Columns.Count - 1
                    If IsNumeric(gdvCT.Columns(j).FieldName) Then
                        Dim r() As DataRow = ds.Tables(1).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.GetRowCellValue(i, "Thang") & "'" & " AND IDThucHien=" & gdvCT.Columns(j).FieldName)
                        If r.Length > 0 Then
                            gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, r(0)("ChiTieu"))
                            _Tong += r(0)("ChiTieu")
                        End If
                    End If
                Next

                gdvCT.SetRowCellValue(i, "Tong", _Tong)
            Next

            For i As Integer = 0 To gdvCT.DataRowCount - 1
                Dim r() As DataRow = ds.Tables(2).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.GetRowCellValue(i, "Thang") & "'")
                If r.Length > 0 Then
                    gdvCT.SetRowCellValue(i, "ChiTieuPhong", r(0)("ChiTieu"))
                End If
            Next

            gdvCT.EndUpdate()
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
            CloseWaiting()

        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Chi tieu.xls"
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

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        If cbPhong.EditValue Is Nothing And cbLoaiChiTieu.EditValue = "Phòng" Then
            gdv.DataSource = Nothing
            Exit Sub
        End If

        If cbLoaiChiTieu.EditValue = "Phòng" Then
            gdvCT.ColumnPanelRowHeight = -1
            LoadDSChiTieuPhong()
        Else
            If cbPhong.EditValue Is Nothing Then

            Else
                If cbNhanVien.EditValue Is Nothing Then
                    gdvCT.ColumnPanelRowHeight = 50
                    loadDSChiTieuTatCaNhanVien()
                Else
                    gdvCT.ColumnPanelRowHeight = -1
                    loadDSChiTieuMotNhanVien()
                End If

            End If
        End If
    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        If cbLoaiChiTieu.EditValue = "Phòng" Then
            btXem.PerformClick()
        Else
            LoadDsNhanVien()
        End If
    End Sub

    Private Sub cbLoaiChiTieu_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbLoaiChiTieu.EditValueChanged
        If cbLoaiChiTieu.EditValue = "Phòng" Then
            cbNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            rcbPhong.Buttons(1).Visible = True
            gdv.DataSource = Nothing
        Else
            cbNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            rcbPhong.Buttons(1).Visible = False
            If cbPhong.EditValue Is Nothing Then
                cbPhong.EditValue = 1
            End If
            LoadDsNhanVien()
            gdv.DataSource = Nothing
        End If
    End Sub

    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub


    Private Sub cbNhanVien_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNhanVien.EditValueChanged
        btXem.PerformClick()
    End Sub


#End Region

#Region "Thoát chương trình"

    Private Sub fMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        TimeLoadData.Stop()
        deskTop.Show()
    End Sub

#End Region

    Private Sub TimeLoadData_Tick(sender As System.Object, e As System.EventArgs) Handles TimeLoadData.Tick
        If CountChuyenMa = gdvChuyenMaCT.RowCount - 1 Then
            LoadDuLieuChuyenMa()
        End If
        If CountHoiGia = gdvHoiGiaCT.RowCount - 1 Then
            LoadDuLieuHoiGia()
        End If
        CountHoiGia += 1
        CountChuyenMa += 1
        gdvChuyenMaCT.TopRowIndex += 1
        gdvHoiGiaCT.TopRowIndex += 1


        'LoadDuLieu()
    End Sub

    Private Sub btDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDong.ItemClick
        Me.Close()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        If keyData = (Keys.Control Or Keys.Shift Or Keys.A) Then
            On Error Resume Next
            If btTuDong.Checked Then
                TimeLoadData.Interval = tbSoGiay.EditValue * 1000
                TimeLoadData.Start()
            Else
                TimeLoadData.Stop()
            End If
            Return True
        ElseIf keyData = (Keys.Control Or Keys.Shift Or Keys.H) Then
            toolbar.Visible = Not toolbar.Visible
        End If
        Return False
    End Function

    Private Sub btTuDong_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTuDong.CheckedChanged
        On Error Resume Next
        If btTuDong.Checked Then
            TimeLoadData.Interval = tbSoGiay.EditValue * 1000
            TimeLoadData.Start()
        Else
            TimeLoadData.Stop()
        End If
    End Sub
End Class