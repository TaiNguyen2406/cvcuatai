Imports BACSOFT.Db.SqlHelper

Public Class frmBaoCaoKetQuaHoatDongKinhDoanh






    Private Sub frmTieuChiBaoCaoCongNoThue_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load




        txtNam.Value = GetServerTime.Year

        LoadTieuChiBaoCao()



    End Sub


    Private Sub LoadTieuChiBaoCao()
        Dim obj As New TieuChiThoiGianCollection(txtNam.Value)
        cmbTieuChi.Properties.Items.Clear()
        For Each o As TieuChiThoiGian In obj.DsTieuChi
            cmbTieuChi.Properties.Items.Add(o)
        Next
        cmbTieuChi.EditValue = obj.DsTieuChi(0)
    End Sub


    Private Sub txtNam_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtNam.EditValueChanged
        LoadTieuChiBaoCao()
    End Sub

    Private Sub cmbTieuChi_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTieuChi.SelectedIndexChanged
        Dim obj = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian)
        If obj.HienThi = "Tùy chỉnh" Then
            txtTuNgay.Enabled = True
            txtDenNgay.Enabled = True
        Else
            txtTuNgay.Enabled = False
            txtDenNgay.Enabled = False
        End If
        txtTuNgay.EditValue = obj.TuNgay
        txtDenNgay.EditValue = obj.DenNgay
    End Sub


    Private Sub btInHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btInHoaDon.Click

        Try

            Dim sql As String = ""
            Dim obj As TieuChiThoiGian = cmbTieuChi.SelectedItem



            ShowWaiting("Đang tải nội dung ...")


            Dim f As New frmIn("Báo cáo kết quả hoạt động kinh doanh")
            Dim rpt As New rptKetQuaHoatDongKinhDoanh

            If CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThi = "Tùy chỉnh" Then
                rpt.prmThoiGian.Value = "Từ ngày " & CType(txtTuNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & " đến ngày " & CType(txtDenNgay.EditValue, DateTime).ToString("dd/MM/yyyy")
            Else
                rpt.prmThoiGian.Value = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThiBaoCao
            End If

            'rpt.Parameters("prmTaiKhoan").Value = sotaikhoan & " - " & ExecuteSQLDataTable("select TenGoi from taikhoanthue where taikhoan = N'" & sotaikhoan & "'").Rows(0)(0)
            'rpt.txtTongTon.Text = FormatNumber(tondauky, 0)

            Dim tungay As DateTime = txtTuNgay.EditValue
            Dim denngay As DateTime = txtDenNgay.EditValue


            Dim sqlMau As String = ""
            sqlMau &= "DECLARE @TuNgay as datetime "
            sqlMau &= "DECLARE @DenNgay as datetime "

            sqlMau &= "SET @TuNgay = Convert(datetime,'" & tungay.ToString("dd/MM/yyyy") & "',103) "
            sqlMau &= "SET @DenNgay = Convert(datetime,'" & denngay.ToString("dd/MM/yyyy") & "',103) "


            Dim sqlForm As String = "FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
            If chkGhiSo.Checked Then
                sqlForm &= "WHERE a.GhiSo = 1 "
            Else
                sqlForm &= "WHERE 1=1 "
            End If

            sqlForm &= "AND Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) >= @TuNgay "
            sqlForm &= "AND Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) <= @DenNgay "

            Dim sqlDK As String = ""
            Dim dt As DataTable = Nothing


            '** 01.** Doanh thu bán hàng và cung cấp dịch vụ:
            ' -- Phát sinh bên Có 511 --
            'sql = sqlMau & " SELECT SUM(b.ThanhTien*isnull(a.TyGia,1)) " & sqlForm
            'sql &= "AND  b.TaiKhoanCo like N'511%'"
            'dt = ExecuteSQLDataTable(sql)
            'Dim p_01_nay As Double = dt.Rows(0)(0)
            'rpt.p_01_nay.Value = p_01_nay
            'rpt.p_01_truoc.Value = DBNull.Value



            rpt.RequestParameters = False
            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub

    Public Sub BaoCaoTongHopCongNoPhaiThu()

        'Try

        '    Dim sql As String = txtTongHopCongNoPhaiThu.Text
        '    Dim obj As TieuChiThoiGian = cmbTieuChi.SelectedItem

        '    sql = sql.Replace("{@Nam}", txtNam.EditValue)
        '    sql = sql.Replace("{@TuNgay}", txtTuNgay.DateTime.ToString("dd/MM/yyyy"))
        '    sql = sql.Replace("{@DenNgay}", txtDenNgay.DateTime.ToString("dd/MM/yyyy"))

        '    ShowWaiting("Đang tải nội dung ...")

        '    Dim dt As DataTable = ExecuteSQLDataTable(sql)
        '    If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)

        '    dt.Columns.Add(New DataColumn("NoLuyKe", Type.GetType("System.Double")))
        '    dt.Columns.Add(New DataColumn("CoLuyKe", Type.GetType("System.Double")))
        '    dt.Columns.Add(New DataColumn("NoCuoiKy", Type.GetType("System.Double")))
        '    dt.Columns.Add(New DataColumn("CoCuoiKy", Type.GetType("System.Double")))

        '    For Each r As DataRow In dt.Rows
        '        r("NoLuyKe") = r("NoDauKy") + r("NoPhatSinh")
        '        r("CoLuyKe") = r("CoDauKy") + r("CoPhatSinh")
        '        Dim chenhlech As Double = r("NoLuyKe") - r("CoLuyKe")
        '        If chenhlech > 0 Then
        '            r("NoCuoiKy") = chenhlech
        '        ElseIf chenhlech < 0 Then
        '            r("CoCuoiKy") = Math.Abs(chenhlech)
        '        End If
        '    Next

        '    Dim f As New frmIn("Tổng hợp công nợ phải thủ")
        '    Dim rpt As New rptTongHopCongNoPhaiThuPhaiTra

        '    rpt.lblTenBaoCao.Text = "TỔNG HỢP CÔNG NỢ PHẢI THU"
        '    rpt.lblTenTaiKhoan.Text = "Tài khoản 131 - Phải thu của khách hàng"

        '    If CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThi = "Tùy chỉnh" Then
        '        rpt.lblNgayThang.Text = "Từ ngày " & CType(txtTuNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & " đến ngày " & CType(txtDenNgay.EditValue, DateTime).ToString("dd/MM/yyyy")
        '    Else
        '        rpt.lblNgayThang.Text = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThiBaoCao
        '    End If

        '    'rpt.Parameters("prmTaiKhoan").Value = sotaikhoan & " - " & ExecuteSQLDataTable("select TenGoi from taikhoanthue where taikhoan = N'" & sotaikhoan & "'").Rows(0)(0)
        '    'rpt.txtTongTon.Text = FormatNumber(tondauky, 0)

        '    rpt.TuNgay = txtTuNgay.EditValue
        '    rpt.DenNgay = txtDenNgay.EditValue
        '    rpt.TenBaoCao = cmbTieuChi.SelectedItem
        '    rpt.LoaiBaoCao = LoaiBaoCaoCongNo.TongHopCongNoPhaiThu

        '    rpt.DataSource = dt

        '    rpt.RequestParameters = False
        '    rpt.CreateDocument()

        '    f.printControl.PrintingSystem = rpt.PrintingSystem
        '    CloseWaiting()
        '    f.ShowDialog()
        'Catch ex As Exception
        '    CloseWaiting()
        '    ShowBaoLoi(ex.Message)
        'End Try

    End Sub



    Private Sub LabelControl6_Click(sender As System.Object, e As System.EventArgs) Handles LabelControl6.Click

    End Sub

    Private Sub LabelControl5_Click(sender As System.Object, e As System.EventArgs)

    End Sub
End Class