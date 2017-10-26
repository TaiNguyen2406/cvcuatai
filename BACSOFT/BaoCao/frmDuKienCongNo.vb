Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering

Public Class frmDuKienCongNo

    Private Sub frmDuKienCongNo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            mCapNhatThanhToan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mCapNhatDaChi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        btTaiLai.PerformClick()
    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        gdv.DataSource = Nothing
        Application.DoEvents()
        gdvCT.Columns.Clear()
        Application.DoEvents()
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"

        sql &= " SELECT Round( (ISNULL((SELECT SUM(SoTien) FROM THUNH WHERE THUNH.NgayThangCT<GetDate()),0) "
        sql &= " 		+ ISNULL((SELECT SUM(SoTien) FROM CHI WHERE CHI.NgayThangCT<GetDate() AND MucDich=211),0)"
        sql &= " 		- ISNULL((SELECT SUM(SoTien) FROM UNC WHERE UNC.NgayThang<Getdate()),0)"
        sql &= " 		- ISNULL((SELECT SUM(SoTien) FROM THU WHERE THU.NgayThangCT<GetDate() AND MucDich=101),0)"
        sql &= " ) + (ISNULL((SELECT SUM(SoTien) FROM THU WHERE THU.NgayThangCT<GetDate()),0) - ISNULL((SELECT SUM(SoTien) FROM Chi WHERE CHI.NgayThangCT<Getdate()),0)),0)"

        sql &= " DECLARE @COL AS NVARCHAR(MAX),"
        sql &= " @SQL  AS NVARCHAR(MAX)"

        sql &= " SELECT @COL = COALESCE(@COL +',','') + QUOTENAME(Convert(Nvarchar,NgayCongNo,103))"
        sql &= " FROM"
        sql &= " ("
        sql &= " SELECT DISTINCT NgayCongNo"
        sql &= " FROM tblCongNo WHERE TrangThai <>0"
        sql &= " )AS B"
        sql &= " ORDER BY B.NgayCongNo"

        sql &= " SET @SQL='"
        sql &= " WITH PivotData As"
        sql &= " ("
        sql &= " 	SELECT Loai, NgayCongNo,SoTien"
        sql &= " 	FROM tblCongNo WHERE TrangThai<>0"
        sql &= " )"
        sql &= " SELECT (Case WHEN Loai=0 THEN ''  Thu'' ELSE ''  Chi'' END ) AS '' '','+@COL+'"
        sql &= " FROM PivotData"
        sql &= " PIVOT"
        sql &= " ("
        sql &= "  SUM (SoTien)"
        sql &= "  FOR NgayCongNo IN ('+ @COL +')"
        sql &= " ) AS PivotResult"
        sql &= " ORDER BY Loai'"
        sql &= " EXEC(@SQL)"
        ShowWaiting("Đang tải báo cáo")
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            Dim tb As DataTable = ds.Tables(1)
            Dim r As DataRow = tb.NewRow
            Dim r1 As DataRow = tb.NewRow
            ' r(0)(0) = "Đầu kỳ"
            tb.Rows.InsertAt(r, 0)
            tb.Rows.InsertAt(r1, 3)
            tb.Rows(0)(" ") = "Đầu kỳ"

            tb.Rows(3)(" ") = "Cuối kỳ"
            If tb.Columns.Count > 1 Then
                tb.Rows(0)(1) = ds.Tables(0).Rows(0)(0)
                tb.Rows(3)(1) = ds.Tables(0).Rows(0)(0) + convertDouble(tb.Rows(1)(1)) - convertDouble(tb.Rows(2)(1))
            End If

            For i As Integer = 0 To tb.Columns.Count - 1
                If i > 1 Then
                    tb.Rows(0)(i) = tb.Rows(3)(i - 1)
                    tb.Rows(3)(i) = tb.Rows(3)(i - 1) + convertDouble(tb.Rows(1)(i)) - convertDouble(tb.Rows(2)(i))
                End If
            Next

            gdv.DataSource = tb
            FormatGdv()
            CloseWaiting()
            LoadPhaiThu()
            LoadPhaiTra()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadPhaiThu()
        Application.DoEvents()
        tabPhaiThu.Text = "Phải thu " & "(đang tải ...)"
        Dim sql As String = ""
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY tblCongNo.NgayCongNo) AS STT,tblCongNo.ID,"
        sql &= " tblCongNo.NgayCongNo,tblCongNo.SoTien,tblCongNo.SoPhieu1,tblCongNo.SoPhieu2,tblCongNo.GhiChu,"
        sql &= " PT.Ten AS PhuTrach,KHACHHANG.ttcMa"
        sql &= " FROM tblCongNo"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=tblCongNo.SoPhieu1"
        sql &= " INNER JOIN NHANSU AS PT ON PT.ID=BANGCHAOGIA.IDTakeCare"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"
        sql &= " WHERE tblCongNo.Loai=0 AND tblCongNo.TrangThai <>0"
        sql &= " ORDER BY TblCongNo.NgayCongNo"
        Application.DoEvents()
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdvPhaiThu.DataSource = tb
        End If
        Application.DoEvents()
        tabPhaiThu.Text = "Phải thu"
    End Sub

    Private Sub LoadPhaiTra()
        Application.DoEvents()
        tabPhaiTra.Text = "Phải chi " & "(đang tải ...)"
        Dim sql As String = ""
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY tblCongNo.NgayCongNo) AS STT,tblCongNo.ID,"
        sql &= " tblCongNo.NgayCongNo,tblCongNo.SoTien,tblCongNo.SoPhieu1,tblCongNo.SoPhieu2,tblCongNo.GhiChu,"
        sql &= " PT.Ten AS PhuTrach,KHACHHANG.ttcMa"
        sql &= " FROM tblCongNo"
        sql &= " INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=tblCongNo.SoPhieu1"
        sql &= " INNER JOIN NHANSU AS PT ON PT.ID=PHIEUDATHANG.IDTakeCare"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=PHIEUDATHANG.IDKhachHang"
        sql &= " WHERE tblCongNo.Loai=1 AND tblCongNo.TrangThai <>0"
        sql &= " ORDER BY TblCongNo.NgayCongNo"
        Application.DoEvents()
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdvPhaiTra.DataSource = tb
        End If
        Application.DoEvents()
        tabPhaiTra.Text = "Phải chi "


    End Sub

    Private Function convertDouble(ByVal value As Object) As Double

        If IsDBNull(value) Then
            Return 0
        End If

        Return value

    End Function


    Public Sub FormatGdv()
        gdvCT.BeginUpdate()
        For i As Integer = 0 To gdvCT.Columns.Count - 1
            If i = 0 Then
                gdvCT.Columns(i).Width = 60
                gdvCT.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                Continue For
            End If

            gdvCT.Columns(i).Width = 95
            gdvCT.Columns(i).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            gdvCT.Columns(i).DisplayFormat.FormatString = "N0"
            'If i > 1 Then
            '    gdvCT.SetRowCellValue()
            'End If
        Next
        gdvCT.EndUpdate()
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Du kien cong no " & Now.ToString("dd-MM-yyyy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)

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
        If IsDBNull(e.CellValue) Then Exit Sub
        If e.Column.FieldName <> " " Then
            If e.CellValue < 0 Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub


    Private Sub gdvCT_FocusedColumnChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles gdvCT.FocusedColumnChanged
        On Error Resume Next
        If e.FocusedColumn.VisibleIndex = 0 Then
            gdvPhaiThuCT.Columns("NgayCongNo").ClearFilter()
            gdvPhaiTraCT.Columns("NgayCongNo").ClearFilter()
        Else
            Dim FilterString, FilterDisplayText, FilterDisplayText2 As String
            Dim FilterPhaiThu, FilterPhaiChi As ColumnFilterInfo
            Dim BinaryFilter(2) As CriteriaOperator
            BinaryFilter(0) = New GroupOperator(New BinaryOperator("NgayCongNo", Convert.ToDateTime(e.FocusedColumn.FieldName), BinaryOperatorType.GreaterOrEqual))
            BinaryFilter(1) = New GroupOperator(New BinaryOperator("NgayCongNo", DateAdd(DateInterval.Day, 1, Convert.ToDateTime(e.FocusedColumn.FieldName)), BinaryOperatorType.Less))

            FilterString = BinaryFilter(0).ToString() & " AND " & BinaryFilter(1).ToString()
            FilterDisplayText = "[Ngày thu] >= '" & Convert.ToDateTime(e.FocusedColumn.FieldName) & "'" & " AND [Ngày thu] < '" & DateAdd(DateInterval.Day, 1, Convert.ToDateTime(e.FocusedColumn.FieldName)) & "'"
            FilterDisplayText2 = "[Ngày chi] >= '" & Convert.ToDateTime(e.FocusedColumn.FieldName) & "'" & " AND [Ngày chi] < '" & DateAdd(DateInterval.Day, 1, Convert.ToDateTime(e.FocusedColumn.FieldName)) & "'"

            FilterPhaiThu = New ColumnFilterInfo(FilterString, FilterDisplayText)
            FilterPhaiChi = New ColumnFilterInfo(FilterString, FilterDisplayText2)
            gdvPhaiThuCT.Columns("NgayCongNo").FilterInfo = FilterPhaiThu
            gdvPhaiTraCT.Columns("NgayCongNo").FilterInfo = FilterPhaiChi
            '  gdvPhaiThuCT.Columns("NgayCongNo").FilterInfo = New ColumnFilterInfo("[Ngày thu] = '" & e.FocusedColumn.FieldName & "'", e.FocusedColumn.FieldName)
            'gdvPhaiThuCT.ActiveFilterString = "[Ngày thu] = '" & e.FocusedColumn.FieldName & "'"
        End If
    End Sub

    Private Sub gdvCT_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If e.FocusedRowHandle = 1 Then
            XtraTabControl1.SelectedTabPage = tabPhaiThu
        ElseIf e.FocusedRowHandle = 2 Then
            XtraTabControl1.SelectedTabPage = tabPhaiTra
        End If
    End Sub

    Private Sub mCapNhatThanhToan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCapNhatThanhToan.ItemClick
        If Not ShowCauHoi("Cập nhật các khoản đã thu ?") Then Exit Sub
        Dim sql As String = ""
        sql &= " DECLARE @tbDaThu table"
        sql &= " ("
        sql &= " 	SoPhieuXK nvarchar(10),"
        sql &= " 	SoPhieuCG nvarchar(10),"
        sql &= " 	PhaiThu float,"
        sql &= " 	TienThu float,"
        sql &= " 	SoTien2 float"
        sql &= " )"

        sql &= " INSERT INTO @tbDaThu(SoPhieuXK,SoPhieuCG,PhaiThu,TienThu,SoTien2)"
        sql &= " SELECT   PHIEUXUATKHO.Sophieu AS SoPhieuXK,PHIEUXUATKHO.SoPhieuCG, "
        sql &= "   (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia AS PhaiThu, ISNULL(tbThu.Sotien, 0) AS TienThu,ISNULL(SoTien2,0)SoTien2"
        sql &= " FROM PHIEUXUATKHO "
        sql &= " LEFT JOIN (SELECT SoTien,SoTien AS SoTien2,(N'TT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THU"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,SoTien as SoTien2,(N'CK ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THUNH"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI WHERE MucDich=230"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC WHERE MucDich=230"
        sql &= " )tbThu ON PHIEUXUATKHO.Sophieu = tbThu.PhieuTC1 OR PHIEUXUATKHO.SophieuCG = tbThu.PhieuTC0  "
        sql &= " WHERE PHIEUXUATKHO.SoPhieu IN (SELECT DISTINCT SoPhieu2 FROM tblCongNo WHERE Loai=0 AND TrangThai<>0)"
        sql &= " OR PHIEUXUATKHO.SoPhieuCG IN (SELECT DISTINCT SoPhieu1 FROM tblCongNo WHERE Loai=0 AND TrangThai<>0)"

        sql &= " Update tblCongNo SET "
        sql &= "         TrangThai = 2"
        sql &= "         WHERE"
        sql &= " Loai=0 AND TrangThai <> 0 AND SoPhieu2 IN ("
        sql &= " Select SoPhieuXK"
        sql &= " FROM("
        sql &= " SELECT [@tbDaThu].SoPhieuXK,[@tbDaThu].PhaiThu,SUM([@tbDaThu].SoTien2)DaThu FROM @tbDaThu "
        sql &= " GROUP BY [@tbDaThu].SoPhieuXK,[@tbDaThu].PhaiThu)tb"
        sql &= " WHERE DaThu >0 )"

        sql &= " Update tblCongNo SET "
        sql &= "         TrangThai = 0"
        sql &= "         WHERE"
        sql &= " Loai=0 AND TrangThai <>0 AND SoPhieu2 IN ("
        sql &= " Select SoPhieuXK"
        sql &= " FROM("
        sql &= " SELECT [@tbDaThu].SoPhieuXK,[@tbDaThu].PhaiThu,SUM([@tbDaThu].SoTien2)DaThu FROM @tbDaThu "
        sql &= " GROUP BY [@tbDaThu].SoPhieuXK,[@tbDaThu].PhaiThu"
        sql &= " )tb2 WHERE (PhaiThu >50000 AND abs(PhaiThu-DaThu) <=50000) OR (PhaiThu <=50000 AND abs(PhaiThu-DaThu) <=2000)"
        sql &= " )"

        sql &= " Update tblCongNo SET "
        sql &= "         TrangThai = 0"
        sql &= "         WHERE"
        sql &= " Loai=0 AND TrangThai <>0 AND SoPhieu1 IN ("
        sql &= " Select SoPhieuCG"
        sql &= " FROM("
        sql &= " SELECT [@tbDaThu].SoPhieuCG,[@tbDaThu].PhaiThu,SUM([@tbDaThu].SoTien2)DaThu FROM @tbDaThu "
        sql &= " GROUP BY [@tbDaThu].SoPhieuCG,[@tbDaThu].PhaiThu"
        sql &= " )tb2 WHERE (PhaiThu >50000 AND abs(PhaiThu-DaThu) <=50000) OR (PhaiThu <=50000 AND abs(PhaiThu-DaThu) <=2000)"
        sql &= " )"


        Application.DoEvents()
        ShowWaiting("Đang update các khoản thu ...")
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CloseWaiting()
            btTaiLai.PerformClick()
        End If


        'sql = ""

        'sql &= " Update tblCongNo SET "
        'sql &= "         TrangThai = 2"
        'sql &= "         WHERE"
        'sql &= " Loai=1 AND SoPhieu2 IN ("
        'sql &= " Select SoPhieuNK"
        'sql &= " FROM("
        'sql &= " SELECT SoPhieuNK,PhaiChi,Sum(TienChi) AS TienChi FROM ("
        'sql &= " SELECT PHIEUNHAPKHO.Sophieu AS SoPhieuNK,"
        'sql &= "   (PHIEUNHAPKHO.Tientruocthue + PHIEUNHAPKHO.Tienthue) * PHIEUNHAPKHO.Tygia AS PhaiChi, ISNULL(tbChi.Sotien, 0) AS TienChi"
        'sql &= " FROM PHIEUNHAPKHO "
        'sql &= " LEFT JOIN (SELECT SoTien,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI"
        'sql &= " UNION ALL "
        'sql &= " SELECT SoTien,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC)tbChi ON PHIEUNHAPKHO.Sophieu = tbChi.PhieuTC1 OR PHIEUNHAPKHO.SophieuDH = tbChi.PhieuTC0 "
        'sql &= " )tb GROUP BY SoPhieuNK,PhaiChi )tb2 WHERE TienChi > 0)"

        'sql &= " Update tblCongNo SET "
        'sql &= "         TrangThai = 0"
        'sql &= "         WHERE"
        'sql &= " Loai=1 AND SoPhieu2 IN ("
        'sql &= " Select SoPhieuNK"
        'sql &= " FROM("
        'sql &= " SELECT SoPhieuNK,PhaiChi,Sum(TienChi) AS TienChi FROM ("
        'sql &= " SELECT PHIEUNHAPKHO.Sophieu AS SoPhieuNK,"
        'sql &= "   (PHIEUNHAPKHO.Tientruocthue + PHIEUNHAPKHO.Tienthue) * PHIEUNHAPKHO.Tygia AS PhaiChi, ISNULL(tbChi.Sotien, 0) AS TienChi"
        'sql &= " FROM PHIEUNHAPKHO "
        'sql &= " LEFT JOIN (SELECT SoTien,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI"
        'sql &= " UNION ALL "
        'sql &= " SELECT SoTien,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC)tbChi ON PHIEUNHAPKHO.Sophieu = tbChi.PhieuTC1 OR PHIEUNHAPKHO.SophieuDH = tbChi.PhieuTC0 "
        'sql &= " )tb GROUP BY SoPhieuNK,PhaiChi )tb2 WHERE (PhaiChi >50000 AND (PhaiChi-TienChi) <=50000) OR (PhaiChi <=50000 AND (PhaiChi-TienChi) <=2000))"
        'Application.DoEvents()
        'ShowWaiting("Đang update các khoản chi ...")
        'If ExecuteSQLNonQuery(sql) Is Nothing Then
        '    ShowBaoLoi(LoiNgoaiLe)
        'End If
        'CloseWaiting()

        btTaiLai.PerformClick()
    End Sub

    Private Sub gdvPhaiThuCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvPhaiThuCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdvPhaiThu.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menuT.ShowPopup(gdvPhaiThu.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mXacNhanDaThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXacNhanDaThu.ItemClick
        If ShowCauHoi("Xác nhận đã thu ?") Then
            Dim c As Integer = 0
            For i As Integer = 0 To gdvPhaiThuCT.SelectedRowsCount - 1
                AddParameter("@TrangThai", 0)
                AddParameterWhere("@IDD", gdvPhaiThuCT.GetRowCellValue(gdvPhaiThuCT.GetSelectedRows(i), "ID"))
                If doUpdate("tblCongNo", "ID=@IDD") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    c += 1
                End If
            Next
            If c > 0 Then
                ShowAlert("Đã cập nhật !")
            End If
            btTaiLai.PerformClick()
        End If
    End Sub

    Private Sub mXacNhanDaChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXacNhanDaChi.ItemClick
        If ShowCauHoi("Xác nhận đã chi ?") Then
            Dim c As Integer = 0
            For i As Integer = 0 To gdvPhaiTraCT.SelectedRowsCount - 1
                AddParameter("@TrangThai", 0)
                AddParameterWhere("@IDD", gdvPhaiTraCT.GetRowCellValue(gdvPhaiTraCT.GetSelectedRows(i), "ID"))
                If doUpdate("tblCongNo", "ID=@IDD") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    c += 1
                End If
            Next
            If c > 0 Then
                ShowAlert("Đã cập nhật !")
            End If
            btTaiLai.PerformClick()
        End If
    End Sub

    Private Sub gdvPhaiTraCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvPhaiTraCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdvPhaiTra.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menuC.ShowPopup(gdvPhaiTra.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mCapNhatDaChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mCapNhatDaChi.ItemClick
        If Not ShowCauHoi("Cập nhật các khoản đã chi ?") Then Exit Sub
        Dim sql As String = ""
        sql &= " DECLARE @tbDaChi table"
        sql &= " ("
        sql &= " 	SoPhieuNK nvarchar(10),"
        sql &= " 	SoPhieuDH nvarchar(10),"
        sql &= " 	PhaiChi float,"
        sql &= " 	TienChi float"
        sql &= " )"

        sql &= " INSERT INTO @tbDaChi(SoPhieuNK,SoPhieuDH,PhaiChi,TienChi)"
        sql &= " SELECT SoPhieuNK,SoPhieuDH,PhaiChi,Sum(TienChi) AS TienChi FROM ("
        sql &= " SELECT PHIEUNHAPKHO.Sophieu AS SoPhieuNK,PHIEUNHAPKHO.SoPhieuDH,"
        sql &= "   (PHIEUNHAPKHO.Tientruocthue + PHIEUNHAPKHO.Tienthue) * PHIEUNHAPKHO.Tygia AS PhaiChi, ISNULL(tbChi.Sotien, 0) AS TienChi"
        sql &= " FROM PHIEUNHAPKHO "
        sql &= " LEFT JOIN (SELECT SoTien,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC)tbChi "
        sql &= " ON PHIEUNHAPKHO.Sophieu = tbChi.PhieuTC1 OR PHIEUNHAPKHO.SophieuDH = tbChi.PhieuTC0 "
        sql &= " WHERE PHIEUNHAPKHO.SoPhieu IN (SELECT SoPhieu2 FROM tblCOngNo WHERE Loai=1 AND TrangThai <>0) OR PHIEUNHAPKHO.SoPhieuDH IN (SELECT SoPhieu1 FROM tblCOngNo WHERE Loai=1 AND TrangThai <>0)"
        sql &= " )tb GROUP BY SoPhieuNK,SoPhieuDH,PhaiChi "

        sql &= " Update tblCongNo SET "
        sql &= "         TrangThai = 2"
        sql &= "         WHERE"
        sql &= " Loai=1 AND TrangThai <>0 AND SoPhieu2 IN ("
        sql &= " SELECT [@tbDaChi].SoPhieuNK FROM @tbDaChi WHERE [@tbDaChi].TienChi > 0)"

        sql &= " Update tblCongNo SET "
        sql &= "         TrangThai = 0"
        sql &= "         WHERE"
        sql &= " Loai=1 AND TrangThai <>0 AND SoPhieu2 IN ("
        sql &= " Select [@tbDaChi].SoPhieuNK FROM @tbDaChi "
        sql &= " 	WHERE ([@tbDaChi].PhaiChi >50000 AND abs([@tbDaChi].PhaiChi-[@tbDaChi].TienChi) <=50000) "
        sql &= " 		OR ([@tbDaChi].PhaiChi <=50000 AND abs([@tbDaChi].PhaiChi-[@tbDaChi].TienChi) <=2000))"

        sql &= " Update tblCongNo SET "
        sql &= "         TrangThai = 0"
        sql &= "         WHERE"
        sql &= " Loai=1 AND TrangThai <>0 AND SoPhieu1 IN ("
        sql &= " Select [@tbDaChi].SoPhieuDH FROM @tbDaChi "
        sql &= " 	WHERE ([@tbDaChi].PhaiChi >50000 AND abs([@tbDaChi].PhaiChi-[@tbDaChi].TienChi) <=50000) "
        sql &= " 		OR ([@tbDaChi].PhaiChi <=50000 AND abs([@tbDaChi].PhaiChi-[@tbDaChi].TienChi) <=2000))"


        Application.DoEvents()
        ShowWaiting("Đang update các khoản đã chi ...")
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CloseWaiting()
            btTaiLai.PerformClick()
        End If

    End Sub
End Class
