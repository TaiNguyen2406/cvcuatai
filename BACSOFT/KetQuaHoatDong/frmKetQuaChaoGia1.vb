Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmKetQuaChaoGia1

    Private Sub frmKetQuaChaoGia_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        LoadPhongBan()
        LoadTakeCare()
        ' If tb.Rows.Count > 0 Then
        cbTakeCare.EditValue = TaiKhoan
        ' End If
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbTakeCare.Enabled = True
            cbPhong.Enabled = True
        Else
            cbTakeCare.Enabled = False
            cbPhong.Enabled = False
        End If
    End Sub

    Public Sub LoadPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadTakeCare()
        Dim sql As String = ""
        If cbPhong.EditValue Is Nothing Then
            sql = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER BY ID"
        Else
            sql = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment= " & cbPhong.EditValue & " ORDER BY ID "
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbTakeCare.DataSource = tb

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub
    'Load dùng proc
    Public Sub LoadDS2()
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            If cbTakeCare.EditValue Is Nothing Then
                AddParameterWhere("@IDTakeCare", 1)
                AddParameterWhere("@TatCaTakeCare", True)
            Else
                AddParameterWhere("@IDTakeCare", cbTakeCare.EditValue)
                AddParameterWhere("@TatCaTakeCare", False)
            End If

            Dim tb As DataTable = ExecutePrcDataTable("prcKetQuaXuatKho")
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)

            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub
    'Load thông thường
    Public Sub LoadDS()
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            Dim sql As String = ""
            sql &= " SELECT KHACHHANG.ttcMa,BANGCHAOGIA.IDTakeCare,"
            sql &= "   (CASE (SELECT Count(ID) FROM BANGCHAOGIA AS B WHERE B.IDKhachHang=BANGCHAOGIA.IDKhachHang AND "
            sql &= "    datediff(day,B.NgayNhan,BANGCHAOGIA.NgayNhan)<365 AND  BANGCHAOGIA.NgayNhan > B.NgayNhan "
            sql &= "    AND RIGHT(CONVERT(nvarchar, B.NgayNhan,103),7) <> RIGHT(CONVERT(nvarchar, BANGCHAOGIA.NgayNhan,103),7) AND B.TrangThai=2 )"
            sql &= "    WHEN 0 THEN Convert(bit, 1) ELSE convert(bit, 0) END) KHMoi,"
            sql &= "    DEPATMENT.Ten AS Phong,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.CongTrinh,BANGCHAOGIA.NgayThang,BANGCHAOGIA.NgayNhan,"
            sql &= " 	(BANGCHAOGIA.TienTruocThue+BANGCHAOGIA.TienThue)*BANGCHAOGIA.TyGia AS TongTien,"
            sql &= "    (CASE BANGCHAOGIA.TienTruocThue WHEN 0 THEN 0 ELSE Round(((ISNULL(BANGCHAOGIA.tienchietkhau, 0) /  (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END)))/BANGCHAOGIA.TienTruocThue),2)*100 END)PTCK,"
            sql &= "    (CASE BANGCHAOGIA.TienTruocThue WHEN 0 THEN 0 ELSE Round(((BANGCHAOGIA.TienTruocThue-(ISNULL(BANGCHAOGIA.tienchietkhau, 0) /  (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))))*BANGCHAOGIA.TyGia-ISNULL(tb.GiaGoc,0))/(BANGCHAOGIA.TienTruocThue*BANGCHAOGIA.TyGia),2)*100 END)PTLN,"
            sql &= " 	BANGCHAOGIA.TienTruocThue*BANGCHAOGIA.TyGia AS TruocThue,(BANGCHAOGIA.TienThue*BANGCHAOGIA.TyGia) AS TienThue,"
            sql &= " 	BANGCHAOGIA.TienChietKhau*BANGCHAOGIA.TyGia AS ChietKhau,((BANGCHAOGIA.TienTruocThue-(ISNULL(BANGCHAOGIA.tienchietkhau, 0) /  (1 - (CASE WHEN BANGCHAOGIA.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))))*BANGCHAOGIA.TyGia-ISNULL(tb.GiaGoc,0))LoiNhuan,TakeCare.Ten AS TakeCare,NgGiaoDich.Ten AS Ngd,(Case BANGCHAOGIA.TrangThai WHEN 2 THEN Convert(bit,1) ELSE Convert(bit,0) END)XacNhan,"
            sql &= "    tblTuDien.NoiDung AS TenTrangThai,BANGCHAOGIA.TrangThai, '' AS CBTT, (CASE WHEN tbTT.SoTien is null then 0 ELSE (CASE WHEN tbTT.SoTien <> (BANGCHAOGIA.TienTruocThue + BANGCHAOGIA.TienThue) THEN 2 ELSE 1 END) END) AS TTTT"
            sql &= " FROM BANGCHAOGIA"
            sql &= " LEFT JOIN tblTuDien ON tblTuDien.Ma=BANGCHAOGIA.TrangThai AND tblTuDien.Loai=2 "
            sql &= " INNER JOIN NHANSU AS TakeCare ON BANGCHAOGIA.IDTakecare=TakeCare.ID"
            If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                sql &= " AND TakeCare.IDDepatment= " & cbPhong.EditValue
            End If
            sql &= " LEFT JOIN (SELECT Sum(SoTien)SoTien, SoPhieu1 FROM tblCongNo WHERE Loai=0 GROUP BY SoPhieu1)tbTT ON tbTT.SoPhieu1=BANGCHAOGIA.SoPhieu "


            sql &= " LEFT JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
            sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(BANGCHAOGIA.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(BANGCHAOGIA.Ngaythang) "
            sql &= " LEFT JOIN DEPATMENT ON DEPATMENT.ID=TakeCare.IDDepatment"

            sql &= " LEFT JOIN NHANSU AS NgGiaoDich ON BANGCHAOGIA.IDNgd=NgGiaoDich.ID"
            'sql &= " LEFT JOIN (SELECT DISTINCT IDKhachHang FROM BANGCHAOGIA AS B WHERE datediff(Day, B.NgayNhan,BANGCHAOGIA.NgayNhan)<365 "
            ''  If chkDaXacNhan.Checked Then
            'sql &= " AND  BANGCHAOGIA.NgayNhan > B.NgayNhan AND RIGHT(CONVERT(nvarchar, B.NgayNhan,103),7) <> RIGHT(CONVERT(nvarchar, BANGCHAOGIA.NgayNhan,103),7) AND  BANGCHAOGIA.TrangThai=2 "
            '' End If
            'sql &= " )tbKHMoi ON BANGCHAOGIA.IDKhachHang=tbKHMoi.IDKhachHang "
            sql &= " LEFT JOIN (SELECT SoPhieu,SUM(GiaNhap) AS GiaGoc FROM"
            sql &= " View_ChaoGiaGiaNhapTB WHERE View_ChaoGiaGiaNhapTB.SoPhieu IN "
            sql &= " (SELECT SoPhieu FROM BANGCHAOGIA WHERE "
            If chkDaXacNhan.Checked Then
                sql &= " BANGCHAOGIA.TrangThai=2 AND convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103) Between Convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
            Else
                sql &= " convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103) Between Convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
            End If
            sql &= " ) GROUP BY View_ChaoGiaGiaNhapTB.SoPhieu) tb ON tb.SoPhieu=BANGCHAOGIA.SoPhieu"
            If chkDaXacNhan.Checked Then
                sql &= " WHERE BANGCHAOGIA.TrangThai=2 AND convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103) Between Convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
            Else
                sql &= " WHERE convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayThang,103),103) Between Convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
            End If

            If Not cbTakeCare.EditValue Is Nothing Then
                sql &= " AND BANGCHAOGIA.IDTakeCare =" & cbTakeCare.EditValue
            End If
            sql &= " ORDER BY BANGCHAOGIA.SoPhieu"

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub

    Private Sub rcbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakeCare.ButtonClick
        If e.Button.Index = 1 Then
            cbTakeCare.EditValue = Nothing
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        tbTuNgay.EditValue = New DateTime(Convert.ToDateTime(tbDenNgay.EditValue).Year, Convert.ToDateTime(tbDenNgay.EditValue).Month, 1)
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        LoadDS2()
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Loi nhuan theo chao gia.xls"

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

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "LoiNhuan" Then
            If e.CellValue = 0 Then
                e.Appearance.BackColor = Color.Yellow
            ElseIf e.CellValue < 0 Then
                e.Appearance.BackColor = Color.Red
            End If
        ElseIf e.Column.FieldName = "CBTT" Then
            If gdvCT.GetRowCellValue(e.RowHandle, "TTTT") = 0 Then
                e.Appearance.BackColor = Color.Red
            ElseIf gdvCT.GetRowCellValue(e.RowHandle, "TTTT") = 2 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub chkDaXacNhan_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkDaXacNhan.CheckedChanged
        If chkDaXacNhan.Checked Then
            chkDaXacNhan.Glyph = My.Resources.Checked
        Else
            chkDaXacNhan.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub gdvCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvCT.CustomSummaryCalculate
        On Error Resume Next
        If e.IsGroupSummary Then
            If CType(e.Item, XtraGrid.GridGroupSummaryItem).FieldName = "PTLN" Then
                e.TotalValue = (gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn13) / (gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn5) + gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn6))) * 100
            End If
            If CType(e.Item, XtraGrid.GridGroupSummaryItem).FieldName = "PTCK" Then
                e.TotalValue = (gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn9) / (gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn5) + gdvCT.GetGroupRowValue(e.GroupRowHandle, GridColumn6))) * 100
            End If
        End If

        If e.IsTotalSummary Then
            If e.IsTotalSummary And CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "PTLN" Then
                e.TotalValue = (gdvCT.Columns("LoiNhuan").SummaryItem.SummaryValue / (gdvCT.Columns("TruocThue").SummaryItem.SummaryValue + gdvCT.Columns("TienThue").SummaryItem.SummaryValue)) * 100
            End If
            If e.IsTotalSummary And CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "PTCK" Then
                e.TotalValue = (gdvCT.Columns("ChietKhau").SummaryItem.SummaryValue / (gdvCT.Columns("TruocThue").SummaryItem.SummaryValue + gdvCT.Columns("TienThue").SummaryItem.SummaryValue)) * 100
            End If
        End If

    End Sub

    Private Sub btXemChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemChaoGia.ItemClick
        If isShowing Then
            ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
            Exit Sub
        End If

        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvCT.GetFocusedRowCellValue("TrangThai") = TrangThaiChaoGia.DaXacNhan Or gdvCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                Exit Sub
            End If
        End If

        TrangThai.isUpdate = True
        fCNChaoGia = New frmCNChaoGia
        fCNChaoGia.TrangThaiCG.isUpdate = True
        fCNChaoGia.Tag = deskTop.mChaoGia.Name
        fCNChaoGia.SPChaoGia = gdvCT.GetFocusedRowCellValue("SoPhieu")
        fCNChaoGia.Show()
    End Sub

    Private Sub btLocKHMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            Dim sql As String = ""
            sql &= " SELECT KHACHHANG.ttcMa,DEPATMENT.Ten AS Phong,BANGCHAOGIA.SoPhieu,BANGCHAOGIA.CongTrinh,BANGCHAOGIA.NgayThang,BANGCHAOGIA.NgayNhan,"
            sql &= " 	(BANGCHAOGIA.TienTruocThue+BANGCHAOGIA.TienThue)*BANGCHAOGIA.TyGia AS TongTien,"
            sql &= "    (CASE BANGCHAOGIA.TienTruocThue WHEN 0 THEN 0 ELSE Round((BANGCHAOGIA.TienChietKhau/BANGCHAOGIA.TienTruocThue),2)*100 END)PTCK,"
            sql &= "    (CASE BANGCHAOGIA.TienTruocThue WHEN 0 THEN 0 ELSE Round(((BANGCHAOGIA.TienTruocThue-BANGCHAOGIA.TienChietKhau)*BANGCHAOGIA.TyGia-ISNULL(tb.GiaGoc,0))/(BANGCHAOGIA.TienTruocThue*BANGCHAOGIA.TyGia),2)*100 END)PTLN,"
            sql &= " 	BANGCHAOGIA.TienTruocThue*BANGCHAOGIA.TyGia AS TruocThue,(BANGCHAOGIA.TienThue*BANGCHAOGIA.TyGia) AS TienThue,"
            sql &= " 	BANGCHAOGIA.TienChietKhau*BANGCHAOGIA.TyGia AS ChietKhau,((BANGCHAOGIA.TienTruocThue-BANGCHAOGIA.TienChietKhau)*BANGCHAOGIA.TyGia-ISNULL(tb.GiaGoc,0))LoiNhuan,TakeCare.Ten AS TakeCare,NgGiaoDich.Ten AS Ngd,(Case BANGCHAOGIA.TrangThai WHEN 2 THEN Convert(bit,1) ELSE Convert(bit,0) END)XacNhan"
            sql &= " FROM BANGCHAOGIA"
            sql &= " INNER JOIN NHANSU AS TakeCare ON BANGCHAOGIA.IDTakecare=TakeCare.ID"

            If Not cbPhong.EditValue Is Nothing And cbTakeCare.EditValue Is Nothing Then
                sql &= " AND TakeCare.IDDepatment= " & cbPhong.EditValue
            End If

            sql &= " LEFT JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"

            sql &= " LEFT JOIN DEPATMENT ON DEPATMENT.ID=TakeCare.IDDepatment"
            sql &= " LEFT JOIN NHANSU AS NgGiaoDich ON BANGCHAOGIA.IDNgd=NgGiaoDich.ID"
            sql &= " LEFT JOIN (SELECT SoPhieu,SUM(GiaNhap) AS GiaGoc FROM"
            sql &= " View_ChaoGiaGiaNhapTB WHERE View_ChaoGiaGiaNhapTB.SoPhieu IN "
            sql &= " (SELECT SoPhieu FROM BANGCHAOGIA WHERE "
            If chkDaXacNhan.Checked Then
                sql &= " BANGCHAOGIA.TrangThai=2 AND convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103) Between Convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
            Else
                sql &= " convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103) Between Convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
            End If
            sql &= " ) GROUP BY View_ChaoGiaGiaNhapTB.SoPhieu) tb ON tb.SoPhieu=BANGCHAOGIA.SoPhieu"
            If chkDaXacNhan.Checked Then
                sql &= " WHERE BANGCHAOGIA.TrangThai=2 AND convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103) Between Convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
            Else
                sql &= " WHERE convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayThang,103),103) Between Convert(datetime,'" & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) AND convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103)"
            End If
            If Not cbTakeCare.EditValue Is Nothing Then
                sql &= " AND BANGCHAOGIA.IDTakeCare =" & cbTakeCare.EditValue
            End If
            sql &= " AND BANGCHAOGIA.IDKhachHang Not IN (SELECT DISTINCT IDKhachHang FROM PHIEUXUATKHO WHERE datediff(month, PHIEUXUATKHO.NgayThang,convert(datetime, '" & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103))>3)"
            sql &= " ORDER BY BANGCHAOGIA.SoPhieu"

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadTakeCare()
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub mDuKienThanhToan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuKienThanhToan.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmDuKienThanhToan
        f._SoPhieuCGDH = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieu")
        f._PhaiTra = False
        f._Buoc1 = True
        f.ShowDialog()
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            PopupMenu1.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Public Sub LoadDSLSGiaoDich(ByVal _SoPhieu As Object)
        AddParameterWhere("@SP", _SoPhieu)
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ROW_NUMBER() OVER(ORDER BY ThoiGian,ID ) as STT,* FROM BANGCHAOGIA_LSGIAODICH WHERE SoPhieu=@SP ORDER BY ThoiGian DESC,ID DESC")
        If Not dt Is Nothing Then
            gdvLSGD.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If e.FocusedRowHandle < 0 Then
            LoadDSLSGiaoDich("-1")

        Else
            LoadDSLSGiaoDich(gdvCT.GetFocusedRowCellValue("SoPhieu"))
        End If
    End Sub
End Class
