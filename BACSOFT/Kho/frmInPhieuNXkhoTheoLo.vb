Imports BACSOFT.Db.SqlHelper
Public Class frmInPhieuNXkhoTheoLo

    Private Sub frmInPhieuNXkhoTheoLo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        txtThang.EditValue = tg.Month
        txtNam.EditValue = tg.Year
    End Sub

    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick




    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs)
  

    End Sub





    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click

        Try

            txtThang.Enabled = False
            txtNam.Enabled = False
            btGhi.Enabled = False

            'Dim sql As String = "SELECT ID FROM CHUNGTU WHERE LoaiCT = @LoaiCT AND MONTH(NgayCT) = @Thang AND YEAR(NgayCT) = @Nam ORDER BY NgayCT"
            'AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
            'AddParameter("@thang", txtThang.EditValue)
            'AddParameter("@nam", txtNam.EditValue)

            Dim sql As String = "SELECT ID FROM (SELECT DISTINCT a.ID,a.NgayCT FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
            sql &= "WHERE (TaiKhoanNo Like N'156%' or TaiKhoanCo like N'156%') AND b.IdVatTu is not null "
            sql &= " AND a.LoaiCT = @LoaiCT AND MONTH(a.NgayCT) = @Thang AND YEAR(a.NgayCT) = @Nam)tbl ORDER BY NgayCT  "

            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
            AddParameter("@thang", txtThang.EditValue)
            AddParameter("@nam", txtNam.EditValue)

            Application.DoEvents()

            Dim dtId As DataTable = ExecuteSQLDataTable(sql)

            If dtId Is Nothing Then Throw New Exception(LoiNgoaiLe)

            prc.EditValue = 0
            prc.Properties.Maximum = dtId.Rows.Count
            prc.Properties.Minimum = 0

            Dim f As New frmIn("Xuất hóa đơn")
            Dim mainRpt As New DevExpress.XtraReports.UI.XtraReport
            mainRpt.CreateDocument()

            For i As Integer = 0 To dtId.Rows.Count - 1
                Application.DoEvents()
                prc.EditValue = i + 1
                Me.Text = "In phiếu nhập kho theo lô (" & (i + 1) & "/" & dtId.Rows.Count & ") ..."
                sql = ""
                sql &= "SELECT LoaiCT,LoaiCT2,NgayCT,SoCT,NgayHD,SoHD,TenKH,b.DienGiai, "
                sql &= "(select Model from vattu where id = b.IdVatTu)Model,b.DVT,b.SoLuong,b.DonGia,b.ThanhTien "
                sql &= "FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT  "
                sql &= "WHERE a.LoaiCT = @LoaiCT and b.ButToan = @ButToan AND a.ID = @Id "
                AddParameter("@Id", dtId.Rows(i)(0))
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
                AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
                Dim dt As DataTable = ExecuteSQLDataTable(sql)

                Dim rpt As New rptPhieuNhapKhoThue
                rpt.DataSource = dt
                Dim tt As Double = 0
                For j As Integer = 0 To dt.Rows.Count - 1
                    tt += dt.Rows(j)("ThanhTien")
                Next
                rpt.lblSoTienBangChu.Text = Utils.StringHelper.VIE2String(tt, False, "đồng", "lẻ", "phẩy", 2)
                Dim ngayct As DateTime = Convert.ToDateTime(dt.Rows(0)("NgayCT"))
                rpt.lblNgayCT.Text = String.Format("Ngày {0} tháng {1} năm {2}", ngayct.Day, ngayct.Month, ngayct.Year)
                rpt.lblSoCT.Text = "Số: " & ChungTu.TienToCT(dt.Rows(0)("LoaiCT"), dt.Rows(0)("LoaiCT2")) & dt.Rows(0)("SoCT")
                rpt.lblNguoiGiao.Text = "- Họ và tên người giao: " & dt.Rows(0)("TenKH").ToString
                Dim ngayhd As DateTime = Convert.ToDateTime(dt.Rows(0)("NgayHD"))
                rpt.lblTheoSoHD.Text = "- Theo hóa đơn số " & dt.Rows(0)("SoHD").ToString _
                    & " ngày " & ngayhd.Day & " tháng " & ngayhd.Month & " năm " & ngayhd.Year _
                    & " của " & dt.Rows(0)("TenKH").ToString

                rpt.RequestParameters = False
                rpt.CreateDocument()

                mainRpt.Pages.AddRange(rpt.Pages)
            Next

            f.printControl.PrintingSystem = mainRpt.PrintingSystem
            f.ShowDialog()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        Finally
            txtThang.Enabled = True
            txtNam.Enabled = True
            btGhi.Enabled = True
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