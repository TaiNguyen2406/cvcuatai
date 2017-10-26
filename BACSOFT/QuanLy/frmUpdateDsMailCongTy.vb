Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Web.HttpUtility

Public Class frmUpdateDsMailCongTy

    Private _URL As String = "http://192.168.1.20:8000/?action="

    Private strChuKy As String = ""

    Private isOK As Boolean = False

    Private Sub frmUpdateDsMailCongTy_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If TrangThai.isUpdate Then
            Dim str As String = ""
            str = getRequest("LayThongTinEmail&email=" & txtDiaChiEmail.EditValue & "@baoanjsc.com.vn&key=" & createKey(New String() {"LayThongTinEmail", txtDiaChiEmail.EditValue & "@baoanjsc.com.vn"}))
            If str = "" Then
                Me.Dispose()
                Exit Sub
            End If
            Dim kq() As String = str.Split(New String() {"<br/>{===*LINE*===}<br/>"}, StringSplitOptions.None)
            chkEmailDangSuDung.Checked = Convert.ToBoolean(kq(1))
            txtTenGoi.EditValue = kq(2)
            txtMatKhau.Text = ""
            txtHoDem.EditValue = kq(3)
            txtDungLuongToiDa.Value = Convert.ToInt32(kq(4))
            chkChuyenTiep.Checked = Convert.ToBoolean(kq(5))
            txtEmailChuyenTiep.EditValue = kq(6)
            chkLuuGiuBanGoc.Checked = Convert.ToBoolean(kq(7))
            chkKichHoatChuKy.Checked = Convert.ToBoolean(kq(8))
            strChuKy = kq(9)
        End If
    End Sub

    Private Sub chkChuyenTiep_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkChuyenTiep.CheckedChanged
        txtEmailChuyenTiep.Enabled = chkChuyenTiep.Checked
        chkLuuGiuBanGoc.Enabled = chkChuyenTiep.Checked
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



    Private Sub lblChuKyEmail_Click(sender As System.Object, e As System.EventArgs) Handles lblChuKyEmail.Click
        If Not chkKichHoatChuKy.Checked Then Exit Sub
        Dim f As New DevExpress.XtraEditors.XtraForm
        f.Text = "Thiết lập chữ ký email"
        f.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        f.ShowIcon = False
        f.Width = 800
        f.Height = 600
        f.StartPosition = FormStartPosition.CenterParent

        Dim web As New WebBrowser
        web.Dock = DockStyle.Fill
        web.ScriptErrorsSuppressed = True
        web.IsWebBrowserContextMenuEnabled = False
        web.Navigate(Application.StartupPath & "\ckeditor\index.htm")

        AddHandler web.DocumentCompleted, AddressOf web_DocumentCompleted
        AddHandler f.FormClosed, AddressOf DongFormChuKy

        f.Controls.Add(web)
        If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strChuKy = web.Document.InvokeScript("LayGiaTri")
        End If

        f.Dispose()

    End Sub

    Private Sub web_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)
        If TrangThai.isUpdate Then
            CType(sender, WebBrowser).Document.InvokeScript("GanGiaTri", New String() {strChuKy})
        End If
    End Sub

    Private Sub DongFormChuKy(sender As Object, e As FormClosedEventArgs)
        If ShowCauHoi("Bạn có muốn lưu lại nội dung chữ ký này không ?") Then
            CType(sender, DevExpress.XtraEditors.XtraForm).DialogResult = Windows.Forms.DialogResult.OK
        Else
            CType(sender, DevExpress.XtraEditors.XtraForm).DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub


    Private Sub btnCapNhat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCapNhat.ItemClick
        Try

            If txtDiaChiEmail.Text.Trim = "" Then Throw New Exception("Vui lòng nhập địa chỉ email!")

            Dim key As String = ""
            Dim request As HttpWebRequest = Nothing

            If TrangThai.isAddNew Then
                key = createKey(New String() {"ThemEmailMoi", txtDiaChiEmail.Text & "@baoanjsc.com.vn"})
                request = DirectCast(WebRequest.Create(_URL & "ThemEmailMoi&email=" & UrlEncodeX(txtDiaChiEmail.Text) & "@baoanjsc.com.vn&key=" & key), HttpWebRequest)
            ElseIf TrangThai.isUpdate Then
                key = createKey(New String() {"CapNhatEmail", txtDiaChiEmail.Text & "@baoanjsc.com.vn"})
                request = DirectCast(WebRequest.Create(_URL & "CapNhatEmail&email=" & UrlEncodeX(txtDiaChiEmail.Text) & "@baoanjsc.com.vn&key=" & key), HttpWebRequest)
            End If

            request.Method = "POST"
            Dim postData As String = String.Format("email={0}@baoanjsc.com.vn&", UrlEncodeX(txtDiaChiEmail.Text))
            postData &= String.Format("hoatdong={0}&", chkEmailDangSuDung.Checked)
            postData &= String.Format("tengoi={0}&", UrlEncodeX(txtTenGoi.Text))
            postData &= String.Format("hoten={0}&", UrlEncodeX(txtHoDem.Text))
            If txtMatKhau.Text <> "" Then postData &= String.Format("matkhau={0}&", UrlEncodeX(txtMatKhau.Text))
            postData &= String.Format("dungluong={0}&", txtDungLuongToiDa.Value)
            postData &= String.Format("chuyentiep={0}&", chkChuyenTiep.Checked)
            postData &= String.Format("emailchuyentiep={0}&", UrlEncodeX(txtEmailChuyenTiep.Text))
            postData &= String.Format("luuthugoc={0}&", chkLuuGiuBanGoc.Checked)
            postData &= String.Format("kichhoatchuky={0}&", chkKichHoatChuKy.Checked)
            postData &= String.Format("chuky={0}", UrlEncodeX(strChuKy))

            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)

            request.ContentType = "application/x-www-form-urlencoded;"
            'request.ContentType = "text/html; charset=UTF-8"

            request.ContentLength = byteArray.Length
            request.KeepAlive = True


            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response = CType(request.GetResponse, HttpWebResponse)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            ShowThongBao(responseFromServer)
            TrangThai.isUpdate = True
            txtDiaChiEmail.Properties.ReadOnly = True
            isOK = True

        Catch ex As System.Net.WebException
            Dim r As New System.IO.StreamReader(ex.Response.GetResponseStream)
            Dim er As String = r.ReadToEnd
            r.Close()
            Clipboard.SetText(er)
            er = er.Substring(er.IndexOf("<title>") + 7)
            er = er.Substring(0, er.IndexOf("</title>"))
            ShowBaoLoi(er)

        Catch ex2 As Exception
            ShowBaoLoi("e:" & ex2.Message)
        End Try
    End Sub

    Public Function UrlEncodeX(str As String) As String
        If str.Trim = "" Then Return ""
        Return UrlEncode(str)
    End Function

    Private Sub btnDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDong.ItemClick
        Me.Close()
    End Sub


    Private Sub txtMatKhau_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtMatKhau.EditValueChanged
        If txtMatKhau.EditValue.ToString = "" Then txtMatKhau.EditValue = DBNull.Value
    End Sub

    Private Sub frmUpdateDsMailCongTy_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If isOK Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub


End Class