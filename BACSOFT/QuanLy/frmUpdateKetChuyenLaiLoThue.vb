Imports BACSOFT.Db.SqlHelper
Public Class frmUpdateKetChuyenLaiLoThue

    Private Sub frmKetChuyenLaiLo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim tg As DateTime = GetServerTime()
        txtThang.EditValue = tg.Month
        txtNam.EditValue = tg.Year

        LoadDsTaiKhoan()


        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("DienGiai", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("TaiKhoanNo", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("TaiKhoanCo", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("ThanhTien", Type.GetType("System.Double")))

        Dim r As DataRow


        r = dt.NewRow
        r("DienGiai") = "Kết chuyển doanh thu bán hàng và cung cấp dịch vụ"
        r("TaiKhoanNo") = "511"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("DienGiai") = "Kết chuyển giá vốn hàng bán "
        r("TaiKhoanNo") = "632"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("DienGiai") = "Kết chuyển chi phí hoạt động tài chính"
        r("TaiKhoanNo") = "635"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("DienGiai") = "Kết chuyển chi phí quản lý doanh nghiệp"
        r("TaiKhoanNo") = "6421"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("DienGiai") = "Kết chuyển chi phí quản lý doanh nghiệp"
        r("TaiKhoanNo") = "6422"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("DienGiai") = "Kết chuyển chi phí thuế TNDN"
        r("TaiKhoanNo") = "821"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("DienGiai") = "Kết chuyển doanh thu hoạt động tài chính"
        r("TaiKhoanNo") = "515"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("DienGiai") = "Kết chuyển thu nhập khác"
        r("TaiKhoanNo") = "711"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("DienGiai") = "Kết chuyển chi phí khác phát sinh trong kỳ"
        r("TaiKhoanNo") = "811"
        r("TaiKhoanCo") = "911"
        r("ThanhTien") = 0
        dt.Rows.InsertAt(r, dt.Rows.Count)



        gdv.DataSource = dt



    End Sub


    Private Sub LoadDsTaiKhoan()
        Dim sql As String = "SELECT TaiKhoan,TaiKhoanCha,TenGoi FROM TAIKHOANTHUE ORDER BY TaiKhoan "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        Dim tb2 As DataTable = tb.Copy
        tb2.Rows.Clear()
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = tb.Rows(i)("TenGoi")
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), 1, tb2)
            End If
        Next
        rcmbTaiKhoan.DataSource = tb2
    End Sub


    Private Sub deQuy(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then Continue For
            If tb.Rows(i)("TaiKhoanCha") = idCha Then
                Dim strTen As String = ""
                For j As Integer = 0 To level - 1
                    strTen &= "-- "
                Next
                strTen = " " & strTen & tb.Rows(i)("TenGoi")
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = strTen
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), level + 1, tb2)
            End If
        Next
    End Sub


    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick


        Dim sql As String = ""
        Dim dt As DataTable = Nothing


        sql = "SELECT COUNT(ID) FROM CHUNGTU WHERE YEAR(NgayHD) = " & txtNam.EditValue & " AND MONTH(NgayHD) = " & txtThang.EditValue & " "
        sql &= "AND LoaiCT IN (1,2) AND GhiSo = 0 "
        dt = ExecuteSQLDataTable(sql)
        If dt.Rows(0)(0) > 0 Then
            If Not ShowCauHoi("Tháng " & txtThang.EditValue & " vẫn còn " & dt.Rows(0)(0) & " hóa đơn chưa ghi sổ, vẫn tiếp tục tính?") Then Exit Sub
        End If


        sql = "SELECT ISNULL(SUM(b.ThanhTien*isnull(a.TyGia,1)),0) FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.GhiSo = 1 "
        sql &= "AND YEAR(NgayCT) = " & txtNam.EditValue & " AND MONTH(NgayCT) = " & txtThang.EditValue & " "


        Dim s As String = " "

        s = " AND b.TaiKhoanCo like N'511%' "
        gdvData.SetRowCellValue(0, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

        s = " AND b.TaiKhoanNo like N'632%' "
        gdvData.SetRowCellValue(1, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

        s = " AND b.TaiKhoanNo like N'635%' "
        gdvData.SetRowCellValue(2, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

        s = " AND b.TaiKhoanNo like N'6421%' "
        gdvData.SetRowCellValue(3, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

        s = " AND b.TaiKhoanNo like N'6422%' "
        gdvData.SetRowCellValue(4, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

        s = " AND b.TaiKhoanNo like N'821%' "
        gdvData.SetRowCellValue(5, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

        s = " AND b.TaiKhoanCo like N'515%' "
        gdvData.SetRowCellValue(6, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

        s = " AND b.TaiKhoanCo like N'711%' "
        gdvData.SetRowCellValue(7, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

        s = " AND b.TaiKhoanNo like N'811%' "
        gdvData.SetRowCellValue(8, "ThanhTien", ExecuteSQLDataTable(sql & s).Rows(0)(0))

    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs)
        Dim sql As String = ""
        Dim dt As DataTable

        sql = "SELECT COUNT(ID) FROM CHUNGTU WHERE YEAR(NgayHD) = " & txtNam.EditValue & " AND MONTH(NgayHD) = " & txtThang.EditValue & " "
        sql &= "AND LoaiCT IN (1,2) AND GhiSo = 0 "
        dt = ExecuteSQLDataTable(sql)
        If dt.Rows(0)(0) > 0 Then
            If Not ShowCauHoi("Tháng " & txtThang.EditValue & " vẫn còn " & dt.Rows(0)(0) & " hóa đơn chưa ghi sổ, vẫn tiếp tục tính?") Then Exit Sub
        End If

        Dim frmDoi As New DevExpress.XtraEditors.XtraForm
        frmDoi.StartPosition = FormStartPosition.CenterScreen
        frmDoi.FormBorderStyle = FormBorderStyle.None
        frmDoi.Width = 350
        frmDoi.Height = 25
        frmDoi.TopLevel = True
        frmDoi.TopMost = True
        Dim prc As New DevExpress.XtraEditors.MarqueeProgressBarControl
        prc.Properties.ShowTitle = True
        prc.Properties.Appearance.Font = New Font(Me.Font.Name, 10, FontStyle.Bold)
        prc.Properties.Appearance.ForeColor = Color.Red
        prc.Properties.MarqueeAnimationSpeed = 30
        prc.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Broken
        prc.Dock = DockStyle.Fill

        frmDoi.Controls.Add(prc)
        frmDoi.Show()


        For i As Integer = 0 To gdvData.DataRowCount - 1
            Application.DoEvents()
            gdvData.FocusedRowHandle = i

            prc.Text = "Đang xử lý " & Math.Round(((i + 1) / gdvData.DataRowCount) * 100, 0, MidpointRounding.AwayFromZero) & " %"

            'Tính đơn giá
            sql = " select top 1 b.DonGia from CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
            sql &= "where b.IdVatTu = " & gdvData.GetFocusedRowCellValue("ID") & " AND b.ButToan = " & ChungTu.LoaiButToan.GiaVon & " "
            sql &= "and b.DonGia > 0 and year(a.NgayCT) = " & txtNam.EditValue & " and month(a.NgayCT) < " & txtThang.EditValue & " "
            sql &= "ORDER BY a.NgayCT DESC "
            dt = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                gdvData.SetFocusedRowCellValue("DGTon", dt.Rows(0)(0))
            Else
                sql = "select isnull(round(giatri/dauky,0),0) from tonkhovattuthue where Nam = " & txtNam.EditValue & " and IdVatTu = " & gdvData.GetFocusedRowCellValue("ID")
                dt = ExecuteSQLDataTable(sql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    gdvData.SetFocusedRowCellValue("DGTon", dt.Rows(0)(0))
                Else
                    gdvData.SetFocusedRowCellValue("DGTon", 0)
                End If
            End If

            'Tính tồn đầu kỳ
            sql = "SELECT "
            sql &= "ISNULL((SELECT DauKy FROM TONKHOVATTUTHUE WHERE IdVatTu = " & gdvData.GetFocusedRowCellValue("ID") & " AND Nam=" & txtNam.EditValue & "),0)"
            sql &= " + "
            sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTUCHITIET RIGHT OUTER JOIN CHUNGTU ON CHUNGTUCHITIET.Id_CT = CHUNGTU.Id "
            sql &= "WHERE month(CHUNGTU.NgayHD) < " & txtThang.EditValue & " AND YEAR(CHUNGTU.NgayHD) = " & txtNam.EditValue & " AND CHUNGTU.LoaiCT = 2 AND CHUNGTU.GhiSo = 1 AND CHUNGTUCHITIET.ButToan = 1 "
            sql &= "AND CHUNGTUCHITIET.IdVatTu = " & gdvData.GetFocusedRowCellValue("ID") & "),0)"
            sql &= " - "
            sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTUCHITIET RIGHT OUTER JOIN CHUNGTU ON CHUNGTUCHITIET.Id_CT = CHUNGTU.Id "
            sql &= "WHERE month(CHUNGTU.NgayHD) < " & txtThang.EditValue & " AND YEAR(CHUNGTU.NgayHD) = " & txtNam.EditValue & " AND CHUNGTU.LoaiCT = 1 AND CHUNGTU.GhiSo = 1 AND CHUNGTU.TrangThai = 3 AND CHUNGTUCHITIET.ButToan = 1 "
            sql &= "AND CHUNGTUCHITIET.IdVatTu = " & gdvData.GetFocusedRowCellValue("ID") & "),0) "
            dt = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                gdvData.SetFocusedRowCellValue("SLTon", dt.Rows(0)(0))
            Else
                gdvData.SetFocusedRowCellValue("SLTon", 0)
            End If

            'Giá trị nhập và số lượng nhập trong kỳ
            sql = " select isnull(SUM(b.SoLuong),0)SoLuong,isnull(SUM(b.ThanhTien),0)ThanhTien from CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT  "
            sql &= "where b.IdVatTu = " & gdvData.GetFocusedRowCellValue("ID") & " and year(a.NgayHD) = " & txtNam.EditValue & " and month(a.NgayHD) = " & txtThang.EditValue & " "
            sql &= "and a.GhiSo = 1 AND a.LoaiCT = 2 AND b.ButToan = 1 "
            dt = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                gdvData.SetFocusedRowCellValue("TTNhap", dt.Rows(0)("ThanhTien"))
                gdvData.SetFocusedRowCellValue("SLNhap", dt.Rows(0)("SoLuong"))
            Else
                gdvData.SetFocusedRowCellValue("TTNhap", 0)
                gdvData.SetFocusedRowCellValue("SLNhap", 0)
            End If

            'Tính chi phí
            sql = " select isnull(SUM(b.ChiPhi),0)ChiPhi from CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT  "
            sql &= "where b.IdVatTu = " & gdvData.GetFocusedRowCellValue("ID") & " and year(a.NgayHD) = " & txtNam.EditValue & " and month(a.NgayHD) = " & txtThang.EditValue & " "
            sql &= "and a.GhiSo = 1 AND a.LoaiCT = 2 AND b.ButToan = 1 "
            dt = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                gdvData.SetFocusedRowCellValue("ChiPhi", dt.Rows(0)("ChiPhi"))
            Else
                gdvData.SetFocusedRowCellValue("ChiPhi", 0)
            End If

            'Tính giá xuất kho
            If gdvData.GetFocusedRowCellValue("SLTon") + gdvData.GetFocusedRowCellValue("SLNhap") = 0 Then
                gdvData.SetFocusedRowCellValue("DGXK", 0)
                Continue For
            End If
            Dim giaXK As Double = 0
            giaXK = (gdvData.GetFocusedRowCellValue("DGTon") * gdvData.GetFocusedRowCellValue("SLTon"))
            giaXK += (gdvData.GetFocusedRowCellValue("TTNhap") + gdvData.GetFocusedRowCellValue("ChiPhi"))
            giaXK = Math.Round(giaXK / (gdvData.GetFocusedRowCellValue("SLTon") + gdvData.GetFocusedRowCellValue("SLNhap")), 0, MidpointRounding.AwayFromZero)
            gdvData.SetFocusedRowCellValue("DGXK", giaXK)




        Next

        frmDoi.Close()

    End Sub





    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click

        If Not ShowCauHoi("Cập nhật chứng từ kết chuyển lãi lỗ tháng " & txtThang.EditValue & " năm " & txtNam.EditValue & " ?") Then Exit Sub


        Try

            Dim thanhtien As Double = 0

            For i As Integer = 0 To gdvData.RowCount - 1
                thanhtien += gdvData.GetRowCellValue(i, "ThanhTien")
            Next

            AddParameter("@NgayCT", New Date(txtNam.EditValue, txtThang.EditValue, Date.DaysInMonth(txtNam.EditValue, txtThang.EditValue)))
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.KetChuyenLaiLo)
            AddParameter("@ThanhTien", thanhtien)
            AddParameter("@GhiSo", 1)
            AddParameter("@NguoiLap", TaiKhoan)
            AddParameter("@DienGiai", "Kết chuyển lãi lỗ tháng " & txtThang.EditValue & " năm " & txtNam.EditValue)

            Dim idCT As Object = doInsert("CHUNGTU")
            If idCT Is Nothing Then Throw New Exception(LoiNgoaiLe)

            For i As Integer = 0 To gdvData.RowCount - 1
                AddParameter("@Id_CT", idCT)
                AddParameter("@DienGiai", gdvData.GetRowCellValue(i, "DienGiai"))
                AddParameter("@ThanhTien", gdvData.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@TaiKhoanNo", gdvData.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvData.GetRowCellValue(i, "TaiKhoanCo"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)
                If doInsert("CHUNGTUCHITIET") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next

            ShowAlert("Kết chuyển lãi lỗ tháng " & txtThang.EditValue & " năm " & txtNam.EditValue & " thành công!")
            Me.Close()

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try


    End Sub


    'Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
    '    Me.Close()
    'End Sub


    'Private Sub bg_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bg.DoWork


    'End Sub

    'Private Sub bg_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bg.ProgressChanged

    'End Sub

    'Private Sub bg_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bg.RunWorkerCompleted

    'End Sub
End Class