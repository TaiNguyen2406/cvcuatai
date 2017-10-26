Imports BACSOFT.Db.SqlHelper
Public Class frmTinhGiaXuatKhoThue

    Private Sub frmTinhGiaXuatKhoThue_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        txtThang.EditValue = tg.Month
        txtNam.EditValue = tg.Year
    End Sub

    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick


        Dim sql As String = "select ID,TenHoaDon TenVatTu,(select ten from tendonvitinh where id = vattu.iddonvitinh)DVT from vattu "
        sql &= "where ID IN ( "
        sql &= "    SELECT DISTINCT b.IdVatTu FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT  "
        sql &= "    WHERE IdVatTu is not null AND YEAR(a.NgayHD) = " & txtNam.EditValue & " and MONTH(a.NgayHD) = " & txtThang.EditValue & " "
        sql &= ") "
        sql &= "ORDER BY TenHoaDon "
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        dt.Columns.Add(New DataColumn("DGTon", Type.GetType("System.Int64")))
        dt.Columns.Add(New DataColumn("SLTon", Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("TTNhap", Type.GetType("System.Int64")))
        dt.Columns.Add(New DataColumn("SLNhap", Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("ChiPhi", Type.GetType("System.Int64")))
        dt.Columns.Add(New DataColumn("DGXK", Type.GetType("System.Int64")))
        gdv.DataSource = dt



    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
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

        If Not ShowCauHoi("Hệ thống sẽ tự động tạo các bút toán nợ 632 và có 1561 với đơn giá vừa tính trên hóa đơn bán hàng?") Then Exit Sub

        Dim sql As String = ""
        Dim dt As DataTable

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
        Me.Enabled = False


        Try
            'BeginTransaction()
            'xóa hết bút toán tính giá vốn cũ đi
            sql = " delete from chungtuchitiet where ButToan = 3 and Id_CT IN ( "
            sql &= "select id from chungtu where year(ngayhd)=" & txtNam.EditValue & " and month(ngayhd)=" & txtThang.EditValue & " "
            sql &= "and loaiCT = " & ChungTu.LoaiChungTu.HoaDonDauRa & " )"
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)

            'tạo bút toán tự động
            sql = "select b.Id_Ct,b.ref,b.IdVatTu,b.DienGiai,b.DVT,b.SoLuong,b.IdChiTiet "
            sql &= "from CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
            sql &= "where a.LoaiCT = 1 and b.idVatTu is not null and b.buttoan = 1 "
            sql &= "and year(ngayhd) = " & txtNam.EditValue & " and month(ngayhd) = " & txtThang.EditValue & " and a.ghiso = 1 "
            dt = ExecuteSQLDataTable(sql)
            If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Dim index As Integer = 0
            For Each r As DataRow In dt.Rows
                index += 1
                frmDoi.Text = "Đang xử lý " & Math.Round(((index + 1) / dt.Rows.Count) * 100, 0, MidpointRounding.AwayFromZero) & " %"
                AddParameter("@Id_CT", r("Id_Ct"))
                AddParameter("@ref", r("ref"))
                AddParameter("@IdVatTu", r("IdVatTu"))
                AddParameter("@DienGiai", r("DienGiai"))
                AddParameter("@DVT", r("DVT"))
                AddParameter("@SoLuong", r("SoLuong"))
                Dim rDonGia = CType(gdv.DataSource, DataTable).Select("ID=" & r("IdVatTu"))
                If rDonGia.Length > 0 Then
                    AddParameter("@DonGia", rDonGia(0)("DGXK"))
                    AddParameter("@ThanhTien", Math.Round(r("SoLuong") * rDonGia(0)("DGXK"), 0, MidpointRounding.AwayFromZero))
                Else
                    AddParameter("@DonGia", 0)
                    AddParameter("@ThanhTien", 0)
                End If
                AddParameter("@TaiKhoanNo", "632")
                AddParameter("@TaiKhoanCo", "1561")
                AddParameter("@ButToan", ChungTu.LoaiButToan.GiaVon)
                AddParameter("@IdChiTiet", r("IdChiTiet"))
                If doInsert("CHUNGTUCHITIET") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next
            frmDoi.Close()
            'ComitTransaction()
            ShowThongBao("Đã cập nhật giá vốn thành công!")
        Catch ex As Exception
            frmDoi.Close()
            'RollBackTransaction()
            ShowBaoLoi(ex.Message)
        Finally
            Me.Enabled = True
        End Try





    End Sub


    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub


    Private Sub bg_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bg.DoWork


    End Sub

    Private Sub bg_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bg.ProgressChanged

    End Sub

    Private Sub bg_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bg.RunWorkerCompleted

    End Sub
End Class