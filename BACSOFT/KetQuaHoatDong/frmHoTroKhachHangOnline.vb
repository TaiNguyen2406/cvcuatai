Imports System.Web.HttpUtility
Public Class frmHoTroKhachHangOnline


    Private Sub frmHoTroKhachHangOnline_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cmbTieuChi.EditValue = "Tháng"
        rtxtThoiGian.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        rtxtThoiGian.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        rtxtThoiGian.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        txtThoiGian.EditValue = New Date(Today.Year, Today.Month, Today.Day)

        gdvData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
    End Sub


    Private Sub cmbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbTieuChi.EditValueChanged
        Select Case cmbTieuChi.EditValue.ToString
            Case "Ngày"
                rtxtThoiGian.EditFormat.FormatString = "dd/MM/yyyy"
                rtxtThoiGian.DisplayFormat.FormatString = "dd/MM/yyyy"
                rtxtThoiGian.Mask.EditMask = "dd/MM/yyyy"
            Case "Tháng"
                rtxtThoiGian.EditFormat.FormatString = "MM/yyyy"
                rtxtThoiGian.DisplayFormat.FormatString = "MM/yyyy"
                rtxtThoiGian.Mask.EditMask = "MM/yyyy"
            Case "Năm"
                rtxtThoiGian.EditFormat.FormatString = "yyyy"
                rtxtThoiGian.DisplayFormat.FormatString = "yyyy"
                rtxtThoiGian.Mask.EditMask = "yyyy"
        End Select
    End Sub



    Private isTinNhan As Boolean = True
    Private Sub btnTaiYeuCau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiYeuCau.ItemClick

        prc.Visibility = True

        btnTaiYeuCau.Enabled = False
        btnThongKeTruyCap.Enabled = False

        radTrangThai.Enabled = False
        txtThoiGian.Enabled = False
        cmbTieuChi.Enabled = False

        data = ""

        bg.RunWorkerAsync()

    End Sub


    Public Function toTotalPhut(s1 As Object, s2 As Object) As String
        Dim d As TimeSpan = TimeSpan.FromSeconds(Convert.ToDouble(s2) - Convert.ToDouble(s1))
        Return d.Minutes & ":" & d.Seconds & "s"
    End Function

    Public Function toUnixTime(d As DateTime) As Double
        Dim unixTimeStamp As Double
        Dim zuluTime As DateTime = d.ToUniversalTime
        Dim unixEpoch As New DateTime(1970, 1, 1, 0, 0, 0)
        unixTimeStamp = (zuluTime.Subtract(unixEpoch)).TotalSeconds
        Return unixTimeStamp
    End Function

    Public Function toDateTime(s As Double) As String
        Dim unixEpoch As New DateTime(1970, 1, 1, 0, 0, 0)
        Return unixEpoch.AddHours(7).AddSeconds(s).ToString("HH:mm:ss dd/MM")
    End Function



    Private Sub gdv_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdv.MouseDoubleClick

        If Not isTinNhan Then Exit Sub

        Dim h As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gdvData.CalcHitInfo(e.Location)

        If h.InRowCell AndAlso h.Column.FieldName = "TieuDe" Then
            Try

                Dim data As String = ""

                prc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Application.DoEvents()
                btnTaiYeuCau.Enabled = False
                btnThongKeTruyCap.Enabled = False
                CheckForIllegalCrossThreadCalls = False
                radTrangThai.Enabled = False
                txtThoiGian.Enabled = False
                cmbTieuChi.Enabled = False
                'gdv.Enabled = False

                Dim thread As New Threading.Thread(
                    Sub()
                        Dim strKey = Utils.BaoMat.MaHoaStr(gdvData.GetRowCellValue(h.RowHandle, "Id"), key).Replace("+", "").Replace("=", "").Replace("/", "").Replace("\", "")
                        Dim wc As New System.Net.WebClient()
                        wc.Encoding = System.Text.Encoding.UTF8
                        data = wc.DownloadString("http://support.baoanjsc.com.vn/netapi/NoiDungChat.aspx?id=" & gdvData.GetRowCellValue(h.RowHandle, "Id") & "&key=" & strKey)
                    End Sub
                )
                thread.Start()

                While thread.IsAlive
                    Application.DoEvents()
                End While

                prc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never


                Dim f As New DevExpress.XtraEditors.XtraForm
                f.Text = "Nội dung hội thoại"
                f.FormBorderStyle = FormBorderStyle.Sizable
                f.StartPosition = FormStartPosition.CenterParent
                f.Width = 650
                f.Height = 450

                Dim t As New DevExpress.XtraEditors.MemoEdit
                t.Dock = DockStyle.Fill
                t.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                t.Properties.ReadOnly = True
                t.Properties.Appearance.BackColor = Color.White
                t.Properties.Appearance.Font = New Font(Me.Font.FontFamily, 10)
                t.Text = gdvData.GetRowCellValue(h.RowHandle, "TieuDe") & vbCrLf
                t.Text &= "***************************************" & vbCrLf
                t.Text &= data
                t.Properties.ScrollBars = ScrollBars.Both

                f.Controls.Add(t)
                t.Select(t.Text.Length, t.Text.Length)
                f.ShowDialog()
                f.Dispose()

            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            Finally
                prc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btnTaiYeuCau.Enabled = True
                btnThongKeTruyCap.Enabled = True
                CheckForIllegalCrossThreadCalls = True
                radTrangThai.Enabled = True
                txtThoiGian.Enabled = True
                cmbTieuChi.Enabled = True
                gdv.Enabled = True
            End Try

        ElseIf h.InRowCell AndAlso h.Column.FieldName = "TrangWeb" Then

            Process.Start(gdvData.GetRowCellValue(h.RowHandle, "TrangWeb"))

        End If

    End Sub


    Private data As String
    Private Sub bg_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bg.RunWorkerCompleted

        prc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        btnTaiYeuCau.Enabled = True
        btnThongKeTruyCap.Enabled = True
        CheckForIllegalCrossThreadCalls = True
        radTrangThai.Enabled = True
        txtThoiGian.Enabled = True
        cmbTieuChi.Enabled = True

        Dim dt As New DataTable
        dt.Columns.Add("Id")
        dt.Columns.Add("BatDau")
        dt.Columns.Add("ThoiGian")
        dt.Columns.Add("TieuDe")
        dt.Columns.Add("HoTen")
        dt.Columns.Add("Email")
        dt.Columns.Add("CongTy")
        dt.Columns.Add("DienThoai")
        dt.Columns.Add("IP")
        dt.Columns.Add("DoiTrong")
        dt.Columns.Add("DaKetNoi", Type.GetType("System.Boolean"))
        dt.Columns.Add("DaKetThuc", Type.GetType("System.Boolean"))
        dt.Columns.Add("TrangWeb")



        Dim arrData() = data.Split(New String() {"<br/>*****{END-LINE}*****<br/>"}, StringSplitOptions.RemoveEmptyEntries)


        For i As Integer = 0 To arrData.Length - 1
            Dim r As DataRow = dt.NewRow
            r("Id") = arrData(i).Split(";")(0)
            r("BatDau") = toDateTime(arrData(i).Split(";")(1))
            r("ThoiGian") = toTotalPhut(arrData(i).Split(";")(1), arrData(i).Split(";")(2))
            r("TieuDe") = UrlDecode(arrData(i).Split(";")(8).ToString)
            r("HoTen") = UrlDecode(arrData(i).Split(";")(3))
            r("Email") = UrlDecode(arrData(i).Split(";")(4))
            r("CongTy") = UrlDecode(arrData(i).Split(";")(5))
            r("DienThoai") = UrlDecode(arrData(i).Split(";")(6))
            r("IP") = arrData(i).Split(";")(7)
            r("DoiTrong") = toTotalPhut(0, arrData(i).Split(";")(9))
            r("DaKetNoi") = Convert.ToBoolean(arrData(i).Split(";")(10))
            r("DaKetThuc") = Convert.ToBoolean(arrData(i).Split(";")(11))
            r("TrangWeb") = UrlDecode(arrData(i).Split(";")(12))
            dt.Rows.InsertAt(r, dt.Rows.Count)
        Next

        gdv.DataSource = dt

        If Not dt Is Nothing Then
            If radTrangThai.EditValue = True Then
                gdvData.Columns("ThoiGian").Visible = True
                gdvData.Columns("DaKetThuc").Visible = True
                gdvData.Columns("DaKetNoi").Visible = True
                gdvData.Columns("DoiTrong").Visible = True
                gdvData.Columns("TieuDe").Caption = "Tiêu đề (double click vào cell xem nội dung chi tiết)"
            Else
                gdvData.Columns("ThoiGian").Visible = False
                gdvData.Columns("DaKetThuc").Visible = False
                gdvData.Columns("DaKetNoi").Visible = False
                gdvData.Columns("DoiTrong").Visible = False
                gdvData.Columns("TieuDe").Caption = "Tin nhắn Offline"
            End If
        End If


    End Sub


    Private Sub bg_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bg.DoWork

        CheckForIllegalCrossThreadCalls = False

        Dim _startTime As Double = 0
        Dim _endTime As Double = 0

        Dim d As DateTime = txtThoiGian.EditValue
        Select Case cmbTieuChi.EditValue.ToString
            Case "Ngày"
                _startTime = toUnixTime(New DateTime(d.Year, d.Month, d.Day, 0, 0, 0))
                _endTime = toUnixTime(New DateTime(d.Year, d.Month, d.Day, 23, 59, 59))
            Case "Tháng"
                _startTime = toUnixTime(New DateTime(d.Year, d.Month, 1, 0, 0, 0))
                _endTime = toUnixTime(New DateTime(d.Year, d.Month, DateTime.DaysInMonth(d.Year, d.Month), 23, 59, 59))
            Case "Năm"
                _startTime = toUnixTime(New DateTime(d.Year, 1, 1, 0, 0, 0))
                _endTime = toUnixTime(New DateTime(d.Year, 12, DateTime.DaysInMonth(d.Year, 12), 23, 59, 59))
        End Select

        Dim wc As New System.Net.WebClient()
        wc.Encoding = System.Text.Encoding.UTF8
        Dim url As String
        Dim strKey As String
        strKey = Utils.BaoMat.MaHoaStr(_startTime & _endTime, key).Replace("+", "").Replace("=", "").Replace("/", "").Replace("\", "")

        If radTrangThai.EditValue = True Then
            isTinNhan = True
            url = "http://support.baoanjsc.com.vn/netapi/Chat.aspx?starttime=" & _startTime & "&endtime=" & _endTime & "&key=" & strKey
        Else
            isTinNhan = False
            url = "http://support.baoanjsc.com.vn/netapi/Ticket.aspx?starttime=" & _startTime & "&endtime=" & _endTime & "&key=" & strKey
        End If

        data = wc.DownloadString(url)

    End Sub


    Private Sub btnThongKeTruyCap_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThongKeTruyCap.ItemClick

      

        Dim f As New DevExpress.XtraEditors.XtraForm
        f.Text = "Báo cáo khách truy cập website"
        f.FormBorderStyle = FormBorderStyle.Sizable
        f.WindowState = FormWindowState.Maximized
        f.StartPosition = FormStartPosition.CenterParent
        f.Width = 800
        f.Height = 600

        Dim d As DateTime = txtThoiGian.EditValue
        Dim ngay As Integer = 0
        Dim thang As Integer = 0
        Dim nam As Integer = 0
        Select Case cmbTieuChi.EditValue.ToString
            Case "Ngày"
                ngay = d.Day
                thang = d.Month
                nam = d.Year
            Case "Tháng"
                ngay = 0
                thang = d.Month
                nam = d.Year
            Case "Năm"
                ngay = 0
                thang = 0
                nam = d.Year
        End Select

        Dim w As New WebBrowser
        w.IsWebBrowserContextMenuEnabled = False
        w.Dock = DockStyle.Fill
        w.ScriptErrorsSuppressed = True
        w.Navigate(String.Format("http://support.baoanjsc.com.vn/report.php?h=bac&y={0}&m={1}&d={2}", nam, thang, ngay))


        f.Controls.Add(w)
        f.ShowDialog()
        f.Dispose()


    End Sub



End Class
