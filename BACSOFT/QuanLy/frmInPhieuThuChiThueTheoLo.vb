Imports BACSOFT.Db.SqlHelper
Public Class frmInPhieuThuChiThueTheoLo

    Private Sub InPhieuThuChiThueTheoLo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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

            Dim sql As String = "SELECT DISTINCT a.ID FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE TaiKhoanCo Like N'111%' "
            sql &= " AND MONTH(a.NgayCT) = @thang AND YEAR(a.NgayCT) = @nam"
            AddParameter("@thang", txtThang.EditValue)
            AddParameter("@nam", txtNam.EditValue)

            Application.DoEvents()

            Dim dtId As DataTable = ExecuteSQLDataTable(sql)
            If dtId Is Nothing Then Throw New Exception(LoiNgoaiLe)


            prc.EditValue = 0
            prc.Properties.Maximum = dtId.Rows.Count
            prc.Properties.Minimum = 0

            Dim f As New frmIn("In phiếu chi theo lô")
            Dim mainRpt As New DevExpress.XtraReports.UI.XtraReport
            mainRpt.CreateDocument()

            For i As Integer = 0 To dtId.Rows.Count - 1

                Application.DoEvents()
                prc.EditValue = i + 1

                Me.Text = "In phiếu chi theo lô (" & (i + 1) & "/" & dtId.Rows.Count & ") ..."
                sql = ""
                sql &= "SELECT  a.ID,a.NgayCT,a.SoCT,a.LoaiCT,a.LoaiCT2,a.DienGiai,b.ThanhTien,a.NguoiDaiDien,a.TenKH,a.DiaChi,a.DienGiai,b.TaiKhoanNo "
                sql &= "FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE TaiKhoanCo Like N'111%' "
                sql &= "AND a.ID = @Id "
                AddParameter("@Id", dtId.Rows(i)(0))

                Dim dt As DataTable = ExecuteSQLDataTable(sql)

                Dim rpt As New rptPhieuChiThue
                rpt.DataSource = dt
                Dim tt As Double = 0
                Dim tkNo As String = ""
                For j As Integer = 0 To dt.Rows.Count - 1
                    tt += dt.Rows(j)("ThanhTien")
                    tkNo += dt.Rows(j)("TaiKhoanNo") & ","
                Next
                If tkNo.Length > 1 Then tkNo = tkNo.Substring(0, tkNo.Length - 1)
                rpt.lblSoTien.Text = String.Format("{0:N0}", tt)
                rpt.lblSoTienChu1.Text = Utils.StringHelper.VIE2String(tt, False, "đồng", "lẻ", "phẩy", 2)
                rpt.lblSoTienChu2.Text = Utils.StringHelper.VIE2String(tt, False, "đồng", "lẻ", "phẩy", 2)
                Dim ngayct As DateTime = Convert.ToDateTime(dt.Rows(0)("NgayCT"))
                rpt.lblNgayCT.Text = String.Format("Ngày {0} tháng {1} năm {2}", ngayct.Day, ngayct.Month, ngayct.Year)
                rpt.lblSoCT.Text = "Số: " & ChungTu.TienToCT(dt.Rows(0)("LoaiCT"), dt.Rows(0)("LoaiCT2")) & dt.Rows(0)("SoCT")
                If dt.Rows(0)("NguoiDaiDien").ToString = "" Then
                    rpt.lblNguoiNhan.Text = "Họ và tên người nhận tiền: " & dt.Rows(0)("TenKH")
                Else
                    rpt.lblNguoiNhan.Text = "Họ và tên người nhận tiền: " & dt.Rows(0)("NguoiDaiDien") & " - " & dt.Rows(0)("TenKH")
                End If
                rpt.lblDiaChi.Text = "Địa chỉ: " & dt.Rows(0)("DiaChi").ToString
                rpt.lblLyDoChi.Text = "Lý do chi: " & dt.Rows(0)("DienGiai").ToString
                rpt.lblNo.Text = "Nợ: " & tkNo

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