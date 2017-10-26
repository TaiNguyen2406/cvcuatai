Imports BACSOFT.Db.SqlHelper

Public Class frmTieuChiBaoCaoCongNoThue

    Enum LoaiBaoCaoCongNo
        TongHopCongNoPhaiThu = 1
        TonngHopCongNoPhaiTra = 2
        ChiTietCongNoPhaiThu = 3
        ChiTietCongNoPhaiTra = 4
    End Enum

    Public LoaiBaoCao As LoaiBaoCaoCongNo

    Public Sub New(_LoaiBaoCao As LoaiBaoCaoCongNo)

        ' This call is required by the designer.
        InitializeComponent()

        LoaiBaoCao = _LoaiBaoCao

        Select Case LoaiBaoCao
            Case LoaiBaoCaoCongNo.ChiTietCongNoPhaiThu
                loadKhachHang()
            Case LoaiBaoCaoCongNo.ChiTietCongNoPhaiTra
                loadKhachHang()
        End Select

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmTieuChiBaoCaoCongNoThue_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Select Case LoaiBaoCao
            Case LoaiBaoCaoCongNo.TongHopCongNoPhaiThu
                Me.Text = "Tổng hợp công nợ phải thu"
                cmbDoiTuong.Enabled = False
                chkLaySoHoaDon.Enabled = False
            Case LoaiBaoCaoCongNo.TonngHopCongNoPhaiTra
                Me.Text = "Tổng hợp công nợ phải trả"
                cmbDoiTuong.Enabled = False
                chkLaySoHoaDon.Enabled = False
            Case LoaiBaoCaoCongNo.ChiTietCongNoPhaiThu
                Me.Text = "Chi tiết công nợ phải thu"
                cmbDoiTuong.Enabled = True
                chkLaySoHoaDon.Enabled = True
            Case LoaiBaoCaoCongNo.ChiTietCongNoPhaiTra
                Me.Text = "Chi tiết công nợ phải trả"
                cmbDoiTuong.Enabled = True
                chkLaySoHoaDon.Enabled = True
        End Select


        txtNam.Value = GetServerTime.Year

        LoadTieuChiBaoCao()



    End Sub


    Public Sub loadKhachHang()
        Dim sql As String = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cmbDoiTuong.Properties.DataSource = tb
        End If
        cmbDoiTuong.EditValue = DBNull.Value
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
        Select Case LoaiBaoCao
            Case LoaiBaoCaoCongNo.TongHopCongNoPhaiThu
                BaoCaoTongHopCongNoPhaiThu()
            Case LoaiBaoCaoCongNo.TonngHopCongNoPhaiTra
                BaoCaoTongHopCongNoPhaiTra()
            Case LoaiBaoCaoCongNo.ChiTietCongNoPhaiThu
                BaoCaoChiTietTongNoPhaiThu()
            Case LoaiBaoCaoCongNo.ChiTietCongNoPhaiTra
                BaoCaoChiTietTongNoPhaiTra()
        End Select
    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub


    Public Sub BaoCaoTongHopCongNoPhaiThu()

        Try

            Dim sql As String = txtTongHopCongNoPhaiThu.Text
            Dim obj As TieuChiThoiGian = cmbTieuChi.SelectedItem

            sql = sql.Replace("{@Nam}", txtNam.EditValue)
            sql = sql.Replace("{@TuNgay}", txtTuNgay.DateTime.ToString("dd/MM/yyyy"))
            sql = sql.Replace("{@DenNgay}", txtDenNgay.DateTime.ToString("dd/MM/yyyy"))


            ShowWaiting("Đang tải nội dung ...")

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)

            dt.Columns.Add(New DataColumn("NoLuyKe", Type.GetType("System.Double")))
            dt.Columns.Add(New DataColumn("CoLuyKe", Type.GetType("System.Double")))
            dt.Columns.Add(New DataColumn("NoCuoiKy", Type.GetType("System.Double")))
            dt.Columns.Add(New DataColumn("CoCuoiKy", Type.GetType("System.Double")))

            For Each r As DataRow In dt.Rows
                r("NoLuyKe") = r("NoDauKy") + r("NoPhatSinh")
                r("CoLuyKe") = r("CoDauKy") + r("CoPhatSinh")
                Dim chenhlech As Double = r("NoLuyKe") - r("CoLuyKe")
                If chenhlech > 0 Then
                    r("NoCuoiKy") = chenhlech
                ElseIf chenhlech < 0 Then
                    r("CoCuoiKy") = Math.Abs(chenhlech)
                End If
            Next


            Dim f As New frmIn("Tổng hợp công nợ phải thủ")
            Dim rpt As New rptTongHopCongNoPhaiThuPhaiTra
          


            rpt.lblTenBaoCao.Text = "TỔNG HỢP CÔNG NỢ PHẢI THU"
            rpt.lblTenTaiKhoan.Text = "Tài khoản 131 - Phải thu của khách hàng"

            If CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThi = "Tùy chỉnh" Then
                rpt.lblNgayThang.Text = "Từ ngày " & CType(txtTuNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & " đến ngày " & CType(txtDenNgay.EditValue, DateTime).ToString("dd/MM/yyyy")
            Else
                rpt.lblNgayThang.Text = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThiBaoCao
            End If

            'rpt.Parameters("prmTaiKhoan").Value = sotaikhoan & " - " & ExecuteSQLDataTable("select TenGoi from taikhoanthue where taikhoan = N'" & sotaikhoan & "'").Rows(0)(0)
            'rpt.txtTongTon.Text = FormatNumber(tondauky, 0)

            rpt.TuNgay = txtTuNgay.EditValue
            rpt.DenNgay = txtDenNgay.EditValue
            rpt.TenBaoCao = cmbTieuChi.SelectedItem
            rpt.LoaiBaoCao = LoaiBaoCaoCongNo.TongHopCongNoPhaiThu

            rpt.DataSource = dt


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


    Public Sub BaoCaoTongHopCongNoPhaiTra()

        Try

            Dim sql As String = txtTongHopCongNoPhaiTra.Text
            Dim obj As TieuChiThoiGian = cmbTieuChi.SelectedItem

            sql = sql.Replace("{@Nam}", txtNam.EditValue)
            sql = sql.Replace("{@TuNgay}", txtTuNgay.DateTime.ToString("dd/MM/yyyy"))
            sql = sql.Replace("{@DenNgay}", txtDenNgay.DateTime.ToString("dd/MM/yyyy"))


            ShowWaiting("Đang tải nội dung ...")

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)

            dt.Columns.Add(New DataColumn("NoLuyKe", Type.GetType("System.Double")))
            dt.Columns.Add(New DataColumn("CoLuyKe", Type.GetType("System.Double")))
            dt.Columns.Add(New DataColumn("NoCuoiKy", Type.GetType("System.Double")))
            dt.Columns.Add(New DataColumn("CoCuoiKy", Type.GetType("System.Double")))

            For Each r As DataRow In dt.Rows
                r("NoLuyKe") = r("NoDauKy") + r("NoPhatSinh")
                r("CoLuyKe") = r("CoDauKy") + r("CoPhatSinh")
                Dim chenhlech As Double = r("NoLuyKe") - r("CoLuyKe")
                If chenhlech > 0 Then
                    r("NoCuoiKy") = chenhlech
                ElseIf chenhlech < 0 Then
                    r("CoCuoiKy") = Math.Abs(chenhlech)
                End If
            Next


            Dim f As New frmIn("Tổng hợp công nợ phải trả")
            Dim rpt As New rptTongHopCongNoPhaiThuPhaiTra

            rpt.lblTenBaoCao.Text = "TỔNG HỢP CÔNG NỢ PHẢI TRẢ"
            rpt.lblTenTaiKhoan.Text = "Tài khoản 331 - Phải trả nhà cung cấp"

            If CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThi = "Tùy chỉnh" Then
                rpt.lblNgayThang.Text = "Từ ngày " & CType(txtTuNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & " đến ngày " & CType(txtDenNgay.EditValue, DateTime).ToString("dd/MM/yyyy")
            Else
                rpt.lblNgayThang.Text = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThiBaoCao
            End If

            'rpt.Parameters("prmTaiKhoan").Value = sotaikhoan & " - " & ExecuteSQLDataTable("select TenGoi from taikhoanthue where taikhoan = N'" & sotaikhoan & "'").Rows(0)(0)
            'rpt.txtTongTon.Text = FormatNumber(tondauky, 0)

            rpt.TuNgay = txtTuNgay.EditValue
            rpt.DenNgay = txtDenNgay.EditValue
            rpt.TenBaoCao = cmbTieuChi.SelectedItem
            rpt.LoaiBaoCao = LoaiBaoCaoCongNo.TonngHopCongNoPhaiTra

            rpt.DataSource = dt


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


    Public Sub BaoCaoChiTietTongNoPhaiThu()

        Try

            If cmbDoiTuong.EditValue Is DBNull.Value Then
                ShowCanhBao("Chưa chọn đối tượng cần báo cáo!")
                cmbDoiTuong.Focus()
                Exit Sub
            End If

            Dim drKH As DataRow = ExecuteSQLDataTable("SELECT ttcMa,Ten FROM KHACHHANG WHERE ID = " & cmbDoiTuong.EditValue).Rows(0)


            Dim sql As String = txtChiTietCongNoPhaiThu.Text
            Dim obj As TieuChiThoiGian = cmbTieuChi.SelectedItem

            sql = sql.Replace("{@Nam}", txtNam.EditValue)
            sql = sql.Replace("{@TuNgay}", txtTuNgay.DateTime.ToString("dd/MM/yyyy"))
            sql = sql.Replace("{@DenNgay}", txtDenNgay.DateTime.ToString("dd/MM/yyyy"))
            sql = sql.Replace("{@IdKH}", cmbDoiTuong.EditValue)

            ShowWaiting("Đang tải nội dung ...")

            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Dim dt As DataTable = ds.Tables(1)
            dt.Columns.Add(New DataColumn("SoDu", Type.GetType("System.Double")))
            dt.Columns.Add(New DataColumn("TenLoaiCT", Type.GetType("System.String")))

            Dim duno As Double = ds.Tables(0).Rows(0)(0)
            For Each r As DataRow In dt.Rows
                If chkLaySoHoaDon.Checked Then
                    If r("SoHD").ToString.Trim = "" Then
                        r("SoCT") = ChungTu.TienToCT(r("LoaiCT"), r("LoaiCT2")) & r("SoCT")
                    Else
                        r("SoCT") = r("SoHD")
                    End If
                Else
                    r("SoCT") = ChungTu.TienToCT(r("LoaiCT"), r("LoaiCT2")) & r("SoCT")
                End If
                r("TenLoaiCT") = ChungTu.TenLoaiCT(r("LoaiCT"), r("LoaiCT2"))
                duno = duno + r("PhatSinhNo") - r("PhatSinhCo")
                r("SoDu") = duno
            Next


            Dim f As New frmIn("Chi tiết công nợ phải thu")
            Dim rpt As New rptChiTietCongNoPhaiThuPhaiTra

            rpt.lblTenBaoCao.Text = "CHI TIẾT CÔNG NỢ PHẢI THU"
            rpt.lblTenTaiKhoan.Text = "Tài khoản 131 - Phải thu của khách hàng"
            rpt.lblTenDoiTuong.Text = "Mã đối tượng: " & drKH("ttcMa") & " - Tên đối tượng: " & drKH("Ten") & " - Số dư: " & String.Format("{0:N0}", ds.Tables(0).Rows(0)(0))

            If CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThi = "Tùy chỉnh" Then
                rpt.lblNgayThang.Text = "Từ ngày " & CType(txtTuNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & " đến ngày " & CType(txtDenNgay.EditValue, DateTime).ToString("dd/MM/yyyy")
            Else
                rpt.lblNgayThang.Text = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThiBaoCao
            End If

            'rpt.Parameters("prmTaiKhoan").Value = sotaikhoan & " - " & ExecuteSQLDataTable("select TenGoi from taikhoanthue where taikhoan = N'" & sotaikhoan & "'").Rows(0)(0)
            'rpt.txtTongTon.Text = FormatNumber(tondauky, 0)


            rpt.lblSoDuCuoi.Text = String.Format("{0:N0}", duno)


            rpt.DataSource = dt


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


    Public Sub BaoCaoChiTietTongNoPhaiTra()

        Try

            If cmbDoiTuong.EditValue Is DBNull.Value Then
                ShowCanhBao("Chưa chọn đối tượng cần báo cáo!")
                cmbDoiTuong.Focus()
                Exit Sub
            End If

            Dim drKH As DataRow = ExecuteSQLDataTable("SELECT ttcMa,Ten FROM KHACHHANG WHERE ID = " & cmbDoiTuong.EditValue).Rows(0)


            Dim sql As String = txtChiTietCongNoPhaiTra.Text
            Dim obj As TieuChiThoiGian = cmbTieuChi.SelectedItem

            sql = sql.Replace("{@Nam}", txtNam.EditValue)
            sql = sql.Replace("{@TuNgay}", txtTuNgay.DateTime.ToString("dd/MM/yyyy"))
            sql = sql.Replace("{@DenNgay}", txtDenNgay.DateTime.ToString("dd/MM/yyyy"))
            sql = sql.Replace("{@IdKH}", cmbDoiTuong.EditValue)

            ShowWaiting("Đang tải nội dung ...")

            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Dim dt As DataTable = ds.Tables(1)
            dt.Columns.Add(New DataColumn("SoDu", Type.GetType("System.Double")))
            dt.Columns.Add(New DataColumn("TenLoaiCT", Type.GetType("System.String")))

            Dim duno As Double = ds.Tables(0).Rows(0)(0)
            For Each r As DataRow In dt.Rows
                If chkLaySoHoaDon.Checked Then
                    If r("SoHD").ToString.Trim = "" Then
                        r("SoCT") = ChungTu.TienToCT(r("LoaiCT"), r("LoaiCT2")) & r("SoCT")
                    Else
                        r("SoCT") = r("SoHD")
                    End If
                Else
                    r("SoCT") = ChungTu.TienToCT(r("LoaiCT"), r("LoaiCT2")) & r("SoCT")
                End If
                r("TenLoaiCT") = ChungTu.TenLoaiCT(r("LoaiCT"), r("LoaiCT2"))
                duno = duno + r("PhatSinhCo") - r("PhatSinhNo")
                r("SoDu") = duno
            Next


            Dim f As New frmIn("Chi tiết công nợ phải trả")
            Dim rpt As New rptChiTietCongNoPhaiThuPhaiTra

            rpt.lblTenBaoCao.Text = "CHI TIẾT CÔNG NỢ PHẢI TRẢ"
            rpt.lblTenTaiKhoan.Text = "Tài khoản 331 - Phải trả nhà cung cấp"
            rpt.lblTenDoiTuong.Text = "Mã đối tượng: " & drKH("ttcMa") & " - Tên đối tượng: " & drKH("Ten") & " - Số dư: " & String.Format("{0:N0}", ds.Tables(0).Rows(0)(0))

            If CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThi = "Tùy chỉnh" Then
                rpt.lblNgayThang.Text = "Từ ngày " & CType(txtTuNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & " đến ngày " & CType(txtDenNgay.EditValue, DateTime).ToString("dd/MM/yyyy")
            Else
                rpt.lblNgayThang.Text = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThiBaoCao
            End If

            'rpt.Parameters("prmTaiKhoan").Value = sotaikhoan & " - " & ExecuteSQLDataTable("select TenGoi from taikhoanthue where taikhoan = N'" & sotaikhoan & "'").Rows(0)(0)
            'rpt.txtTongTon.Text = FormatNumber(tondauky, 0)

            rpt.lblSoDuCuoi.Text = String.Format("{0:N0}", duno)

            rpt.DataSource = dt


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

End Class