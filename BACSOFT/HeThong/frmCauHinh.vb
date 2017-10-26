Imports System.IO
Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Utils
Imports System.Xml
Imports BACSOFT.Utils.BaoMat

Public Class frmCauHinh
    Dim xmlDoc As New XmlDocument
    Public _exit As Boolean = False

    Private Sub frmCauHinh_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LayThongTinHeThong()
    End Sub
#Region "Lấy thông tin đăng nhập"
    Public Sub LayThongTinHeThong()
        Try
            xmlDoc.Load(Application.StartupPath & "\Config.xml")
            'txtMayChu.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("Server"), XmlElement).GetAttribute("Value")
            'txtCSDL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("Database"), XmlElement).GetAttribute("Value")
            'txtTKSQL.Text = CType(xmlDoc.DocumentElement.SelectSingleNode("UsDb"), XmlElement).GetAttribute("Value")
            'txtMKSQL.Text = BaoMat.GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("PwDb"), XmlElement).GetAttribute("Value"), key)

            txtMayChu.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("Server")
            txtCSDL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("Database")
            txtTKSQL.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("UsDb")
            _exit = True
            chkNhoMKLan.Checked = Convert.ToBoolean(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("SavePw"))
            _exit = False
            If chkNhoMKLan.Checked Then
                txtMKSQL.EditValue = BaoMat.GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).GetAttribute("PwDb"), key)
            Else
                txtMKSQL.EditValue = ""
            End If


            tbMayChuInternet.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("Server")
            tbCSDLInternet.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("Database")
            tbTKSQLInternet.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("UsDb")
            _exit = True
            chkNhoMKInternet.Checked = Convert.ToBoolean(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("SavePw"))
            _exit = False
            If chkNhoMKInternet.Checked Then
                tbMKSQLInternet.EditValue = BaoMat.GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).GetAttribute("PwDb"), key)
            Else
                tbMKSQLInternet.EditValue = ""
            End If

            tbMayChuCN.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("Server")
            tbCSDLCN.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("Database")
            tbTKSQLCN.EditValue = CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("UsDb")
            _exit = True
            chkNhoMKLocal.Checked = Convert.ToBoolean(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("SavePw"))
            _exit = False
            If chkNhoMKLocal.Checked Then
                tbMKSQLCN.EditValue = BaoMat.GiaiMaMaStr(CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).GetAttribute("PwDb"), key)
            Else
                tbMKSQLCN.EditValue = ""
            End If

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try

    End Sub
#End Region

#Region "lưu thông tin đăng nhập hệ thống"
    Public Function LuuThongTinHeThong(ByVal tenMayChuLan As String, ByVal tenCSDLLan As String, ByVal taiKhoanLan As String, ByVal MatKhauLan As String,
                                       ByVal tenMayChuInternet As String, ByVal tenCSDLInternet As String, ByVal taiKhoanInternet As String, ByVal MatKhauInternet As String,
                                       ByVal tenMayChuLocal As String, ByVal tenCSDLLocal As String, ByVal taiKhoanLocal As String, ByVal MatKhauLocal As String) As Boolean
        Dim kt As Boolean = True
        If File.Exists(Application.StartupPath & "\Config.xml") Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("Server", tenMayChuLan)
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("Database", tenCSDLLan)
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("UsDb", taiKhoanLan)
                If chkNhoMKLan.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("PwDb", MatKhauLan)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("PwDb", "")
                End If

                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("SavePw", chkNhoMKLan.Checked.ToString)


                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("Server", tenMayChuInternet)
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("Database", tenCSDLInternet)
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("UsDb", taiKhoanInternet)
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("PwDb", MatKhauInternet)

                If chkNhoMKInternet.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("PwDb", MatKhauInternet)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("PwDb", "")
                End If

                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("SavePw", chkNhoMKInternet.Checked.ToString)


                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("Server", tenMayChuLocal)
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("Database", tenCSDLLocal)
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("UsDb", taiKhoanLocal)
                If chkNhoMKLocal.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("PwDb", MatKhauLocal)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("PwDb", "")
                End If

                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("SavePw", chkNhoMKLocal.Checked.ToString)

                xmlDoc.Save(Application.StartupPath & "\Config.xml")


                kt = True
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
                kt = False
            End Try
        End If
        Return kt
    End Function
#End Region

    Private Sub btnKiemTra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKiemTra.Click
        If rdInternet.Checked Then
            If tbCSDLInternet.EditValue <> "" And tbMayChuInternet.EditValue <> "" And tbTKSQLInternet.EditValue <> "" And tbMKSQLInternet.EditValue <> "" Then
                Start(tbMayChuInternet.EditValue, tbCSDLInternet.EditValue, tbTKSQLInternet.EditValue, tbMKSQLInternet.EditValue, SQLAuthenticationType.SQLServerAuthentication)
                If testConnection() Then
                    ShowAlert("Kết nối thành công !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            Else
                ShowCanhBao("Điền đầy đủ các thông tin trước khi kiểm tra !")
            End If
        ElseIf rdLan.Checked Then
            If txtCSDL.EditValue <> "" And txtMayChu.EditValue <> "" And txtTKSQL.EditValue <> "" And txtMKSQL.EditValue <> "" Then
                Start(txtMayChu.EditValue, txtCSDL.EditValue, txtTKSQL.EditValue, txtMKSQL.EditValue, SQLAuthenticationType.SQLServerAuthentication)
                If testConnection() Then
                    ShowAlert("Kết nối thành công !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            Else
                ShowCanhBao("Điền đầy đủ các thông tin trước khi kiểm tra !")
            End If
        Else
            If tbMayChuCN.EditValue <> "" And tbCSDLCN.EditValue <> "" And tbTKSQLCN.EditValue <> "" And tbMKSQLCN.EditValue <> "" Then
                Start(tbMayChuCN.EditValue, tbCSDLCN.EditValue, tbTKSQLCN.EditValue, tbMKSQLCN.EditValue, SQLAuthenticationType.SQLServerAuthentication)
                If testConnection() Then
                    ShowAlert("Kết nối thành công !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            Else
                ShowCanhBao("Điền đầy đủ các thông tin trước khi kiểm tra !")
            End If
        End If
        
    End Sub

    Private Sub btnGhiLai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGhiLai.Click
        If LuuThongTinHeThong(txtMayChu.EditValue, txtCSDL.EditValue, txtTKSQL.EditValue, BaoMat.MaHoaStr(txtMKSQL.EditValue, key),
            tbMayChuInternet.EditValue, tbCSDLInternet.EditValue, tbTKSQLInternet.EditValue, BaoMat.MaHoaStr(tbMKSQLInternet.EditValue, key),
            tbMayChuCN.EditValue, tbCSDLCN.EditValue, tbTKSQLCN.EditValue, BaoMat.MaHoaStr(tbMKSQLCN.EditValue, key)
                    ) Then
            ShowCanhBao("Khởi động lại chương trình !")
            Application.Restart()
        End If
    End Sub

    Private Sub btnDong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub

    Private Sub chkNhoMKInternet_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkNhoMKInternet.CheckedChanged
        If _exit = True Then Exit Sub
        If File.Exists(Application.StartupPath & "\Config.xml") Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")
                Dim _MatKhau As Object = MaHoaStr(tbMKSQLInternet.EditValue, key)
                If chkNhoMKInternet.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("PwDb", _MatKhau)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("PwDb", "")
                End If
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrInternet"), XmlElement).SetAttribute("SavePw", chkNhoMKInternet.Checked.ToString)

                xmlDoc.Save(Application.StartupPath & "\Config.xml")
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub chkNhoMKLan_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkNhoMKLan.CheckedChanged
        If _exit = True Then Exit Sub
        If File.Exists(Application.StartupPath & "\Config.xml") Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")
                Dim _MatKhau As Object = MaHoaStr(txtMKSQL.EditValue, key)
                If chkNhoMKLan.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("PwDb", _MatKhau)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("PwDb", "")
                End If
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLan"), XmlElement).SetAttribute("SavePw", chkNhoMKLan.Checked.ToString)

                xmlDoc.Save(Application.StartupPath & "\Config.xml")
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub chkNhoMKLocal_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkNhoMKLocal.CheckedChanged
        If _exit = True Then Exit Sub
        If File.Exists(Application.StartupPath & "\Config.xml") Then
            Try
                xmlDoc.Load(Application.StartupPath & "\Config.xml")
                Dim _MatKhau As Object = MaHoaStr(tbMKSQLCN.EditValue, key)
                If chkNhoMKLocal.Checked Then
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("PwDb", _MatKhau)
                Else
                    CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("PwDb", "")
                End If
                CType(xmlDoc.DocumentElement.SelectSingleNode("cnstrLocal"), XmlElement).SetAttribute("SavePw", chkNhoMKLocal.Checked.ToString)

                xmlDoc.Save(Application.StartupPath & "\Config.xml")

            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub
End Class