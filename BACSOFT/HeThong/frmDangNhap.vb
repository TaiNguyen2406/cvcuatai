Imports System.IO
Imports System.Text.RegularExpressions
Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Utils.BaoMat
Imports System.Xml
Imports System.IO.Ports
Imports Microsoft.Win32

Public Class frmDangNhap
    Public sa, pwdsa As String
    Dim xmlDoc As New XmlDocument
    Public _exit As Boolean = False
    Public _defaultPwdsa As String = "" '"123456"
    Dim rk As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", True)
    Dim path As String = Process.GetCurrentProcess.MainModule.FileName


    Private Sub frmDangNhap_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ShowCanhBao(path)
        If rk.GetValue("BACBOSS.exe") <> path Then
            chkRunStartup.Checked = False
        Else
            chkRunStartup.Checked = True
        End If
        ktGhiNhoMK()
        LayThongTinDN()
        If chkNhoMK.Checked And My.Computer.Name = "BACBOSS" Then
            Threading.Thread.Sleep(10000)
            btnDangNhap.PerformClick()
        End If
    End Sub


#Region "Lấy thông tin đăng nhập"
    Public Sub LayThongTinDN()
        Try
            xmlDoc.Load(Application.StartupPath & "\Config.xml")
            Select Case CType(xmlDoc.DocumentElement.SelectSingleNode("cnType"), XmlElement).GetAttribute("Value")
                Case 1
                    rdInternet.Checked = True
                    tbMayChu.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("Server")
                    tbCSDL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("Database")
                    tbTaiKhoan.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("User")
                    If chkNhoMK.Checked Then
                        tbMatKhau.EditValue = GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("Pw"), XmlElement).GetAttribute("Value"), key)
                    End If

                    sa = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("UsDb")
                    pwdsa = GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("PwDb"), key)
                Case 2
                    rdLan.Checked = True
                    tbMayChu.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("Server")
                    tbCSDL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("Database")
                    tbTaiKhoan.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("User")
                    If chkNhoMK.Checked Then
                        tbMatKhau.EditValue = GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("Pw"), XmlElement).GetAttribute("Value"), key)
                    End If

                    sa = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("UsDb")
                    pwdsa = GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("PwDb"), key)
                Case Else
                    rdLocal.Checked = True
                    tbMayChu.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("Server")
                    tbCSDL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("Database")
                    tbTaiKhoan.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("User")
                    If chkNhoMK.Checked Then
                        tbMatKhau.EditValue = GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("Pw"), XmlElement).GetAttribute("Value"), key)
                    End If

                    sa = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("UsDb")
                    pwdsa = GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("PwDb"), key)
                    'pwdsa = "sa"
            End Select

        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
        End Try

    End Sub
#End Region

#Region "Lưu thông tin đăng nhập"
    Public Sub LuuThongTinDN(ByVal _MayChu As String, ByVal _CSDL As String, ByVal _taiKhoan As String, ByVal _MatKhau As String)
        If File.Exists(Application.StartupPath & "\Config.xml") Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")
                If rdInternet.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("Server", _MayChu)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("Database", _CSDL)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("User", _taiKhoan)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnType"), XmlElement).SetAttribute("Value", 1)
                ElseIf rdLan.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("Server", _MayChu)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("Database", _CSDL)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("User", _taiKhoan)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnType"), XmlElement).SetAttribute("Value", 2)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("Server", _MayChu)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("Database", _CSDL)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("User", _taiKhoan)
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnType"), XmlElement).SetAttribute("Value", 3)
                End If
                If chkNhoMK.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("Pw"), XmlElement).SetAttribute("Value", _MatKhau)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("Pw"), XmlElement).SetAttribute("Value", "")
                End If
                CType(xmlDoc.DocumentElement.SelectSingleNode("SavePw"), XmlElement).SetAttribute("Value", chkNhoMK.Checked.ToString)
                xmlDoc.Save(Application.StartupPath & "\Config.xml")
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub
#End Region

#Region "Kiểm tra trạng thái lưu mật khẩu"
    Public Sub ktGhiNhoMK()
        Try
            xmlDoc.Load(Application.StartupPath & "\Config.xml")
            _exit = True
            chkNhoMK.Checked = Convert.ToBoolean(CType(xmlDoc.DocumentElement.SelectSingleNode("SavePw"), XmlElement).GetAttribute("Value"))
            _exit = False
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub
#End Region


    Private Sub btnDangNhap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDangNhap.Click
        If rdLocal.Checked Then
            Start(tbMayChu.EditValue, tbCSDL.EditValue, sa, pwdsa, SQLAuthenticationType.SQLServerAuthentication)
        Else
            If _defaultPwdsa = "" Then
                Start(tbMayChu.EditValue, tbCSDL.EditValue, sa, pwdsa, SQLAuthenticationType.SQLServerAuthentication)
            Else
                Start(tbMayChu.EditValue, tbCSDL.EditValue, sa, _defaultPwdsa, SQLAuthenticationType.SQLServerAuthentication)
            End If
        End If

        prc.Visible = True
        g1.Enabled = False
        g2.Enabled = False
        btnCauHinh.Enabled = False
        btnDangNhap.Enabled = False

        bgW.RunWorkerAsync()



    End Sub

    Private Sub btnCauHinh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCauHinh.Click
        Dim f As New frmCauHinh
        'Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName)

        '  MsgBox(h.AddressList.GetValue(0).ToString)
        'Dim f As New Form1
        f.ShowDialog()
    End Sub

    Private Sub btnDong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDong.Click
        Application.Exit()
    End Sub

    Private Sub txtMatKhau_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tbMatKhau.KeyDown, tbTaiKhoan.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnDangNhap.PerformClick()
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs)
        Dim dirs() = System.IO.Directory.GetDirectories("\\192.168.1.109\BACTEST$\BAC VAT TU\HINH ANH\", "*", SearchOption.AllDirectories)
        For Each d In dirs
            'Dim d As String = "\\192.168.1.109\BAC NHAN SU$\HINH ANH\"
            Dim files = System.IO.Directory.GetFiles(d)
            If files.Length > 0 Then
                If Not System.IO.Directory.Exists(d & "\thumb\") Then
                    System.IO.Directory.CreateDirectory(d & "\thumb\")
                End If
                For Each f In files
                    Try
                        Dim sourceFile As String = Utils.ConvertImage.ResizeImgFromURL(f, 50)
                        System.IO.File.Copy(sourceFile, d & "\thumb\" & System.IO.Path.GetFileName(f), True)
                    Catch ex As Exception

                    End Try
                Next
            End If
        Next

        ShowAlert("OK !")
    End Sub


    Private Sub rdInternet_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdInternet.CheckedChanged
        If rdInternet.Checked Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")

                tbMayChu.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("Server")
                tbCSDL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("Database")
                sa = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("UsDb")
                pwdsa = Utils.BaoMat.GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("PwDb"), key)

            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If

    End Sub

    Private Sub rdLan_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdLan.CheckedChanged
        If rdLan.Checked Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")

                tbMayChu.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("Server")
                tbCSDL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("Database")
                sa = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("UsDb")
                pwdsa = Utils.BaoMat.GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("PwDb"), key)

            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub rdLocal_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdLocal.CheckedChanged
        If rdLocal.Checked Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")

                tbMayChu.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("Server")
                tbCSDL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("Database")
                sa = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("UsDb")
                pwdsa = Utils.BaoMat.GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("PwDb"), key)

            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub chkNhoMK_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkNhoMK.CheckedChanged
        If _exit = True Then Exit Sub
        If File.Exists(Application.StartupPath & "\Config.xml") Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")
                Dim _MatKhau As Object = MaHoaStr(tbMatKhau.EditValue, key)
                If chkNhoMK.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("Pw"), XmlElement).SetAttribute("Value", _MatKhau)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("Pw"), XmlElement).SetAttribute("Value", "")
                End If
                CType(xmlDoc.DocumentElement.SelectSingleNode("SavePw"), XmlElement).SetAttribute("Value", chkNhoMK.Checked.ToString)

                xmlDoc.Save(Application.StartupPath & "\Config.xml")
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private isKetNoi As Boolean = False
    Private frmWait As DevExpress.Utils.WaitDialogForm

    Private Sub bgW_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgW.DoWork
        isKetNoi = testConnection()
    End Sub


    Private Sub bgW_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgW.RunWorkerCompleted
        CheckForIllegalCrossThreadCalls = False
        prc.Visible = False
        g1.Enabled = True
        g2.Enabled = True
        btnCauHinh.Enabled = True
        btnDangNhap.Enabled = True
        If isKetNoi Then
            AddParameterWhere("@TK", tbTaiKhoan.EditValue)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT NHANSU.ID,NHANSU.Ten,ChucVu,Matkhau,MatKhauLT,QUYENTRUYCAP.Quyen,Email,NHANSU.MaTruyCap,IDDepatment FROM NHANSU LEFT OUTER JOIN QUYENTRUYCAP ON NHANSU.Matruycap=QUYENTRUYCAP.Matruycap WHERE NHANSU.ID = @TK")

            If dt Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

            If dt.Rows.Count = 0 Then
                ShowBaoLoi("Sai tài khoản đăng nhập !")
                Exit Sub
            ElseIf dt.Rows(0)("Matkhau") <> tbMatKhau.EditValue Then
                ShowBaoLoi("Sai mật khẩu !")
                Exit Sub
            End If
            LuuThongTinDN(tbMayChu.EditValue, tbCSDL.EditValue, tbTaiKhoan.EditValue, MaHoaStr(tbMatKhau.EditValue, key))
            TenMayChu = tbMayChu.EditValue
            TenCSDL = tbCSDL.EditValue
            TaiKhoan = tbTaiKhoan.EditValue
            MatKhau = tbMatKhau.EditValue
            MaPhongBan = dt.Rows(0)("IDDepatment")
            MatKhauLT = dt.Rows(0)("MatKhauLT").ToString
            EmailNguoiDung = dt.Rows(0)("Email").ToString
            MaTruyCap = dt.Rows(0)("MaTruyCap").ToString.Replace(" ", "").ToUpper
            'QuyenSD.AddRange(dt.Rows(0)("Quyen").ToString.Split(","))
            NguoiDung = dt.Rows(0)("Ten").ToString
            If rdLocal.Checked Then
                RootUrl = ServerName & "\BACTEST$\"
                RootUrlOld = ServerName & "\BACTEST$\"
                UrlAnhVatTu = ServerName & "\BACTEST$\BAC VAT TU\HINH ANH\"
                UrlTaiLieuVatTu = ServerName & "\BACTEST$\BAC VAT TU\TAI LIEU\"
                UrlAnhNhanSu = ServerName & "\BACTEST$\BAC NHAN SU\Hinh Anh\"
                UrlTaiLieuNhanSu = ServerName & "\BACTEST$\BAC NHAN SU\File\"
                UrlDaoTao = ServerName & "\BACTEST$\BAC DAO TAO\"
                UrlKhachHang = ServerName & "\BACTEST$\BAC KHACH HANG\"
                UrlThiNangLuc = ServerName & "\BACTEST$\THI NANG LUC\"
            Else
                RootUrl = ServerName & "\DATA$\BAC" & GetServerTime.Year.ToString & "\"
                RootUrlOld = ServerName & "\DATA$\BAC"
            End If

            Dim arr As New ArrayList
            QuyenSD = CauTrucQuyenTruyCap()
            arr.AddRange(dt.Rows(0)("Quyen").ToString.Split(New Char() {","c}))
            For i As Integer = 0 To arr.Count - 1
                Dim tmp As New ArrayList
                tmp.AddRange(arr(i).ToString.Split(New Char() {";"c}))
                Dim r = QuyenSD.NewRow()
                QuyenSD.Rows.Add(r)
                QuyenSD.Rows(QuyenSD.Rows.Count - 1)(0) = tmp(0)
                For j As Integer = 1 To tmp.Count - 1
                    QuyenSD.Rows(QuyenSD.Rows.Count - 1)(j) = CType(tmp(j), Boolean)
                Next
            Next

            deskTop = New fMain
            deskTop.txtTenMayCHu.Caption = "Máy chủ: " & TenMayChu
            deskTop.txtTenCSDL.Caption = "CSDL: " & TenCSDL
            deskTop.txtTenNguoiDung.Caption = dt.Rows(0)("Chucvu").ToString & " - " & NguoiDung
            deskTop.Show()

            Me.Hide()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub


    Private Sub prc_Click(sender As System.Object, e As System.EventArgs) Handles prc.Click
        Application.Restart()
    End Sub

    Private Sub chkRunStartup_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkRunStartup.CheckedChanged
        If chkRunStartup.Checked = True Then
            rk.SetValue("BACBOSS.exe", path)
        Else
            rk.DeleteValue("BACBOSS.exe", False)
        End If
    End Sub

    Private Sub LabelControl6_Click(sender As System.Object, e As System.EventArgs) Handles LabelControl6.Click
        'File.Copy("\\192.168.1.109\Phan mem\BACSOFT\BACSOFTNET\UPDATE.exe", Application.StartupPath & "\UPDATE.exe", True)
        'Dim psi As New ProcessStartInfo()
        'With psi
        '    .FileName = Application.StartupPath & "\UPDATE.exe"
        '    .UseShellExecute = True
        'End With
        'Process.Start(psi)

        'Dim objImpersonator As Impersonator = New Impersonator()
        'If (Not Impersonate(objImpersonator)) Then
        '    Throw New ApplicationException("share user not found.")
        'Else
        '    If (File.Exists("\\192.168.1.109\Test\t.txt")) Then
        '        File.Delete("\\192.168.1.109\Test\t.txt")
        '        ShowAlert("Đã xóa !")
        '    End If
        '    ' File.Copy(sourcePath, destinationPath)
        '    objImpersonator.UndoImpersonation()
        '    Return
        'End If


       


    End Sub


End Class