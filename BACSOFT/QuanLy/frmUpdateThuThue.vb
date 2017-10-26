Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmUpdateThuChiThue

    Public LoaiCT As ChungTu.LoaiChungTu

    Public idChungTu As Object
    Public soPhieuT As Object

    Private Sub frmUpdateThuThue_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        tbNgayCT.EditValue = GetServerTime()
        Dim sql As String = "SELECT ID,ttcMa,Ten,IDTakecare,ttcTaiKhoan,ttcNoiMo FROM KHACHHANG ORDER BY ttcMa "
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        gdvMaKH.Properties.DataSource = dt

        LoadDsTaiKhoan()

        Select Case LoaiCT
            Case ChungTu.LoaiChungTu.PhieuThuTienMat
                Me.Text = "phiếu thu tiền mặt"
                cbTkNo.Text = "1111"
                cbTkCo.Text = "131"
            Case ChungTu.LoaiChungTu.PhieuChiTienMat
                Me.Text = "phiếu chi tiền mặt"
                cbTkNo.Text = "331"
                cbTkCo.Text = "1111"
            Case ChungTu.LoaiChungTu.NopTienNganHang
                Me.Text = "phiếu nộp tiền ngân hàng"
                cbTkNo.Text = "1121"
                cbTkCo.Text = "131"
            Case ChungTu.LoaiChungTu.UyNhiemChi
                Me.Text = "ủy nhiệm chi"
                cbTkNo.Text = "331"
                cbTkCo.Text = "1121"
        End Select

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            Me.Text = "Thêm mới " & Me.Text
            btThem.Enabled = False
        ElseIf TrangThai.isUpdate Then
            Me.Text = "Cập nhật " & Me.Text
        End If

        If TrangThai.isUpdate Or TrangThai.isCopy Then

            If soPhieuT Is Nothing Then
                sql = "SELECT * FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.Id = " & idChungTu
            Else
                sql = "SELECT * FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.SoCT = '" & soPhieuT & "' AND a.LoaiCT = " & LoaiCT
            End If

            dt = ExecuteSQLDataTable(sql)
            If TrangThai.isUpdate Then
                tbSoPhieu.EditValue = dt.Rows(0)("SoCT").ToString
                idChungTu = dt.Rows(0)("Id")
            End If
            tbNgayCT.EditValue = dt.Rows(0)("NgayCT")
            gdvMaKH.EditValue = dt.Rows(0)("IdKH")
            cbNguoiNop.EditValue = dt.Rows(0)("NguoiLienHe")
            txtNoiDung.EditValue = dt.Rows(0)("DienGiai")
            tbSoTien.EditValue = dt.Rows(0)("ThanhTien")
            cbTkNo.EditValue = dt.Rows(0)("TaiKhoanNo").ToString
            cbTkCo.EditValue = dt.Rows(0)("TaiKhoanCo").ToString
            chkGhiSo.Checked = dt.Rows(0)("GhiSo")
        End If

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

        Select Case LoaiCT
            Case ChungTu.LoaiChungTu.PhieuThuTienMat
                For Each r As DataRow In tb2.Select("TaiKhoan like '111*'")
                    cbTkNo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
                Next
                For Each r As DataRow In tb2.Rows
                    cbTkCo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
                Next
            Case ChungTu.LoaiChungTu.PhieuChiTienMat
                For Each r As DataRow In tb2.Select("TaiKhoan like '111*'")
                    cbTkCo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
                Next
                For Each r As DataRow In tb2.Rows
                    cbTkNo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
                Next
            Case ChungTu.LoaiChungTu.NopTienNganHang
                For Each r As DataRow In tb2.Select("TaiKhoan like '112*'")
                    cbTkNo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
                Next
                For Each r As DataRow In tb2.Rows
                    cbTkCo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
                Next
            Case ChungTu.LoaiChungTu.UyNhiemChi
                For Each r As DataRow In tb2.Select("TaiKhoan like '112*'")
                    cbTkCo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
                Next
                For Each r As DataRow In tb2.Rows
                    cbTkNo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
                Next
        End Select

        'For Each r As DataRow In tb2.Rows
        '    cbTkNo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
        '    cbTkCo.Properties.Items.Add(New ObjectItem(r("TaiKhoan") & " " & r("TenGoi"), r("TaiKhoan")))
        'Next


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

#Region " - ObjectItem - "
    Private Class ObjectItem
        Private _HienThi As String
        Public Property HienThi() As String
            Get
                Return _HienThi
            End Get
            Set(ByVal value As String)
                _HienThi = value
            End Set
        End Property
        Private _GiaTri As Object
        Public Property GiaTri() As Object
            Get
                Return _GiaTri
            End Get
            Set(ByVal value As Object)
                _GiaTri = value
            End Set
        End Property

        Public Sub New(__HienThi As String, __GiaTri As Object)
            _HienThi = __HienThi
            _GiaTri = __GiaTri
        End Sub

        Public Overrides Function ToString() As String
            Return _HienThi
        End Function

    End Class
#End Region

    Private Sub cbTk_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTkNo.EditValueChanged, cbTkCo.EditValueChanged
        Dim cb = CType(sender, DevExpress.XtraEditors.ComboBoxEdit)
        Dim str = cb.Text
        If str.IndexOf(" ") >= 0 Then
            cb.Text = str.Substring(0, str.IndexOf(" "))
        End If
    End Sub

    Private Sub cbTkNo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTkNo.ButtonClick, cbTkCo.ButtonClick
        Dim cb = CType(sender, DevExpress.XtraEditors.ComboBoxEdit)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cb.Text = ""
        End If
    End Sub



    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub


    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click

        If txtNoiDung.Text = "" Then
            ShowBaoLoi("Chưa có nội dung!")
            Exit Sub
        End If
        If gdvMaKH.EditValue Is DBNull.Value Then
            ShowBaoLoi("Chưa nhập đối tượng!")
            Exit Sub
        End If
        If tbSoTien.Value = 0 Then
            ShowBaoLoi("Chưa nhập số tiền!")
            Exit Sub
        End If
        If cbTkNo.Text = "" Then
            ShowBaoLoi("Chưa nhập tài khoản nợ!")
            Exit Sub
        End If
        If cbTkCo.Text = "" Then
            ShowBaoLoi("Chưa nhập tài khoản có!")
            Exit Sub
        End If

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            tbSoPhieu.EditValue = ChungTu.LaySoPhieu(LoaiCT)
        End If

        If tbSoPhieu.Text = "" Then
            ShowBaoLoi("Chưa lấy được số phiếu")
            Exit Sub
        End If

        AddParameter("@NgayCT", tbNgayCT.EditValue)
        AddParameter("@SoCT", tbSoPhieu.EditValue)
        AddParameter("@LoaiCT", LoaiCT)
        AddParameter("@IdKH", gdvMaKH.EditValue)
        AddParameter("@TenKH", CType(gdvMaKH.Properties.DataSource, DataTable).Select("ID=" & gdvMaKH.EditValue)(0)("Ten"))
        AddParameter("@NguoiLienHe", cbNguoiNop.EditValue)
        AddParameter("@DienGiai", txtNoiDung.EditValue)
        AddParameter("@ThanhTien", tbSoTien.EditValue)
        AddParameter("@TongTien", tbSoTien.EditValue)
        AddParameter("@TienTe", 0)
        AddParameter("@TyGia", 1)
        AddParameter("@GhiSo", chkGhiSo.Checked)

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            AddParameter("@NguoiLap", TaiKhoan)
        End If


        If TrangThai.isAddNew Or TrangThai.isCopy Then
            idChungTu = doInsert("CHUNGTU")
        Else
            AddParameterWhere("@Id", idChungTu)
            doUpdate("CHUNGTU", "Id=@Id")
        End If

        '--Ben chi tiet--

        AddParameter("@DienGiai", txtNoiDung.EditValue)
        AddParameter("@ThanhTien", tbSoTien.EditValue)

        AddParameter("@TaiKhoanNo", cbTkNo.Text)
        AddParameter("@TaiKhoanCo", cbTkCo.Text)

        AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            AddParameter("@Id_CT", idChungTu)
            doInsert("CHUNGTUCHITIET")
        Else
            AddParameterWhere("@Id_CT", idChungTu)
            doUpdate("CHUNGTUCHITIET", "Id_CT=@Id_CT")
        End If

        TrangThai.isUpdate = True
        If TrangThai.isAddNew Or TrangThai.isCopy Then
            ShowAlert("Thêm mới dữ liệu thành công")
        Else
            ShowAlert("Cập nhật dữ liệu thành công")
        End If
        btThem.Enabled = True



        If Not Me.Owner Is Nothing AndAlso Me.Owner.Name = "frmCNThu2" Then
            CType(Me.Owner, frmCNThu2).txtNoiDungPhieuThu.Text = txtNoiDung.Text
        ElseIf Not Me.Owner Is Nothing AndAlso Me.Owner.Name = "frmCNChi2" Then
            CType(Me.Owner, frmCNChi2).txtNoiDungPhieuThu.Text = txtNoiDung.Text
        End If


    End Sub



    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        tbSoPhieu.EditValue = ""
        tbNgayCT.EditValue = GetServerTime()
        gdvMaKH.EditValue = DBNull.Value
        cbNguoiNop.EditValue = ""
        cbTkNo.Text = ""
        cbTkCo.Text = ""
        txtNoiDung.EditValue = ""
        tbSoTien.EditValue = 0
        chkGhiSo.Checked = True
        TrangThai.isAddNew = True

        Select Case LoaiCT
            Case ChungTu.LoaiChungTu.PhieuThuTienMat
                Me.Text = "Thêm mới phiếu thu tiền mặt"
            Case ChungTu.LoaiChungTu.PhieuChiTienMat
                Me.Text = "Thêm mới phiếu chi tiền mặt"
            Case ChungTu.LoaiChungTu.NopTienNganHang
                Me.Text = "Thêm mới phiếu nộp tiền ngân hàng"
            Case ChungTu.LoaiChungTu.UyNhiemChi
                Me.Text = "Thêm mới ủy nhiệm chi"
        End Select


    End Sub

    Private Sub chkGhiSo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkGhiSo.CheckedChanged
        If chkGhiSo.Checked Then
            chkGhiSo.ForeColor = Color.Green
        Else
            chkGhiSo.ForeColor = Color.Black
        End If
        chkGhiSo.Refresh()
    End Sub
End Class