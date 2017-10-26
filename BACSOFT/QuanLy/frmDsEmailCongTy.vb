Public Class frmDsEmailCongTy

    Private _URL As String = "http://192.168.1.20:8000/?action="

    Private Sub frmDsEmailCongTy_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load




        LayDsTrangThai()


        ' Dim thread2 As New Threading.Thread(
        '    Sub()
        '        LayDsGroup()
        '    End Sub
        ')
        ' thread2.Start()





        cmbTrangThai.EditValue = "1"
        cmbNhom.EditValue = DBNull.Value

        'TaiDsEmail()
        gdvData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None

    End Sub

    Private Sub LayDsGroup()
        Dim str As String = getRequest("LayDsGroup&key=" & createKey(New String() {"LayDsGroup"}))
        If str = "" Then rcmbNhom.DataSource = Nothing
        Dim dt As New DataTable
        dt.Columns.Add("GiaTri")
        Dim kq() As String = str.Split(New String() {"<br />"}, StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 0 To kq.Length - 1
            Dim r As DataRow = dt.NewRow
            r("GiaTri") = kq(i)
            dt.Rows.InsertAt(r, dt.Rows.Count)
        Next
        rcmbNhom.DataSource = dt
    End Sub

    Private Sub rcmbNhom_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcmbNhom.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbNhom.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub LayDsTrangThai()
        Dim dt As New DataTable
        dt.Columns.Add("GiaTri")
        dt.Columns.Add("HienThi")
        Dim r As DataRow
        r = dt.NewRow
        r("GiaTri") = "1"
        r("HienThi") = "Đang sử dụng"
        dt.Rows.InsertAt(r, 0)
        r = dt.NewRow
        r("GiaTri") = "0"
        r("HienThi") = "Ngừng sử dụng"
        dt.Rows.InsertAt(r, 1)
        rcmbTrangThai.DataSource = dt

    End Sub

    Private Sub rcmbTrangThai_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcmbTrangThai.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbTrangThai.EditValue = DBNull.Value
        End If
    End Sub


    Private Sub btnTaiDanhSach_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDanhSach.ItemClick
        TaiDsEmail()
    End Sub

    Private Sub TaiDsEmail()

        Try
            Dim str As String = ""

            prc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            cmbTrangThai.Enabled = False
            btnTaiDanhSach.Enabled = False
            btThem.Enabled = False
            btSuaNoiDung.Enabled = False
            btnXoa.Enabled = False

            Dim thread As New Threading.Thread(
                               Sub()
                                   If cmbNhom.EditValue Is DBNull.Value Then
                                       str = getRequest("LayDsMail&sudung=" & cmbTrangThai.EditValue.ToString & "&key=" & createKey(New String() {"LayDsMail"}))
                                   Else
                                       str = getRequest("LayDsMail&sudung=" & cmbTrangThai.EditValue.ToString & "&group=" & cmbNhom.EditValue.ToString & "&key=" & _
                                                        createKey(New String() {"LayDsMail", cmbNhom.EditValue.ToString}))
                                   End If
                               End Sub
                           )
            thread.Start()

            While thread.IsAlive
                Application.DoEvents()
            End While



            If str = "" Then gdv.DataSource = Nothing
            Dim dt As New DataTable

            dt.Columns.Add("TrangThai", Type.GetType("System.Boolean"))
            dt.Columns.Add("HoTen")
            dt.Columns.Add("Email")
            dt.Columns.Add("SuDung", Type.GetType("System.Double"))
            dt.Columns.Add("GioiHan")
            dt.Columns.Add("DangNhap")
            dt.Columns.Add("GhiChu")
            Dim kq() As String = str.Split(New String() {"<br />"}, StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To kq.Length - 1
                Dim r As DataRow = dt.NewRow
                r("TrangThai") = kq(i).Split(";")(5)
                r("HoTen") = kq(i).Split(";")(1)
                r("Email") = kq(i).Split(";")(0)
                r("SuDung") = Convert.ToDouble(kq(i).Split(";")(2))
                r("GioiHan") = kq(i).Split(";")(3)
                r("DangNhap") = kq(i).Split(";")(4)
                r("GhiChu") = ""
                dt.Rows.InsertAt(r, dt.Rows.Count)
            Next
            gdv.DataSource = dt

            str = getRequest("LayThongTinHeThong&key=0")
            lblKetNoi.Caption = str.Split(New String() {"<br/>{===*LINE*===}<br/>"}, StringSplitOptions.None)(0).Split(";")(0)
            lblSoThuDaXuLy.Caption = "Số thư đã xử lý: " & str.Split(New String() {"<br/>{===*LINE*===}<br/>"}, StringSplitOptions.None)(0).Split(";")(1)
            lblSoThuRac.Caption = "Số thư rác: " & str.Split(New String() {"<br/>{===*LINE*===}<br/>"}, StringSplitOptions.None)(0).Split(";")(2)
            lblSoThuBiVirus.Caption = "Số thư bị virus: " & str.Split(New String() {"<br/>{===*LINE*===}<br/>"}, StringSplitOptions.None)(0).Split(";")(3)

            lblSMTP.Caption = "SMTP: " & str.Split(New String() {"<br/>{===*LINE*===}<br/>"}, StringSplitOptions.None)(1).Split(";")(0)
            lblIMAP.Caption = "IMAP: " & str.Split(New String() {"<br/>{===*LINE*===}<br/>"}, StringSplitOptions.None)(1).Split(";")(1)
            lblPOP3.Caption = "POP3: " & str.Split(New String() {"<br/>{===*LINE*===}<br/>"}, StringSplitOptions.None)(1).Split(";")(2)




        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        Finally
            cmbTrangThai.Enabled = True
            btnTaiDanhSach.Enabled = True
            btThem.Enabled = True
            btSuaNoiDung.Enabled = True
            btnXoa.Enabled = True
            prc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End Try

    End Sub

    Public Function getRequest(url As String) As String
        Try
            Dim wc As New System.Net.WebClient()
            wc.Encoding = System.Text.Encoding.UTF8
            Return wc.DownloadString(_URL & url)
        Catch ex As System.Net.WebException
            Return ""
            Dim r As New System.IO.StreamReader(ex.Response.GetResponseStream)
            Dim er As String = r.ReadToEnd
            r.Close()
            er = er.Substring(er.IndexOf("<title>") + 7)
            er = er.Substring(0, er.IndexOf("</title>"))
            ShowBaoLoi(er)
        End Try
    End Function

    Public Function createKey(arr() As String) As String
        Dim str As String = ""
        For i As Integer = 0 To arr.Length - 1
            str &= arr(i)
            If i > arr.Length - 1 Then str &= ";"
        Next
        Return Utils.BaoMat.MaHoaStr(str, key).Replace("+", "").Replace("=", "").Replace("/", "").Replace("\", "")
    End Function

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick
        TrangThai.isAddNew = True
        Dim f As New frmUpdateDsMailCongTy
        f.Text = "Thêm mới địa chỉ email"
        If f.ShowDialog = DialogResult.OK Then
            TaiDsEmail()
        End If
    End Sub

    Private Sub btSuaNoiDung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaNoiDung.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        TrangThai.isUpdate = True
        Dim f As New frmUpdateDsMailCongTy
        f.Text = "Cập nhật địa chỉ email"
        f.txtDiaChiEmail.Properties.ReadOnly = True
        f.txtDiaChiEmail.EditValue = gdvData.GetFocusedDataRow(colEmail.FieldName).ToString.Replace("@baoanjsc.com.vn", "")
        If f.ShowDialog = DialogResult.OK Then
            TaiDsEmail()
        End If
    End Sub


    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick

        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        If Not ShowCauHoi("Bạn có chắc muốn xóa địa chỉ email: " & gdvData.GetFocusedDataRow(colEmail.FieldName) & " không?") Then Exit Sub

        Dim str As String = getRequest("XoaDiaChiEmail&email=" & gdvData.GetFocusedDataRow(colEmail.FieldName) & "&key=" & _
                         createKey(New String() {"XoaDiaChiEmail", gdvData.GetFocusedDataRow(colEmail.FieldName)}))

        If str <> "" Then
            ShowThongBao(str)
            TaiDsEmail()
        End If

    End Sub

    Private Sub btCauHinhEmailServer_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCauHinhEmailServer.ItemClick
        'Dim f As New frmUpdateMailServer
        'f.ShowDialog()
    End Sub


End Class
