Imports System.Security.Cryptography
Imports System.Net.Mail

Namespace Utils
    Public Class Email

        Public Shared TenEmail As String = "Auto.BaoAn"
        Public Shared DiaChiEmail As String = "duytai.baoan@gmail.com" '"auto.baoan@gmail.com"
        Public Shared MatKhauEmail As String = "tai240693" '"adminbaoan"
        Public Shared ChuKyEmail As String = ""


        Public Shared Sub Send(ByVal mailTo As String, ByVal Subject As String, ByVal Content As String, Optional ByVal DSFile As DataTable = Nothing, Optional ByVal _DiaChiEmail As String = "auto.baoan@gmail.com", Optional ByVal _MatKhauEmail As String = "adminbaoan")
            Try
                ShowWaiting("Đang gửi mail ...")
                Dim MyMailMessage As New MailMessage()
                MyMailMessage.From = New MailAddress(DiaChiEmail)

                MyMailMessage.To.Add(mailTo)
                MyMailMessage.Subject = Subject
                MyMailMessage.IsBodyHtml = True
                MyMailMessage.Body = Content
                If Not DSFile Is Nothing Then
                    For i As Integer = 0 To DSFile.Rows.Count - 1
                        MyMailMessage.Attachments.Add(New Attachment(DSFile.Rows(i)("File").ToString))
                    Next
                End If
                'Create the SMTPClient object and specify the SMTP GMail server
                Dim SMTPServer As New SmtpClient("smtp.gmail.com")
                SMTPServer.Port = 25

                SMTPServer.Credentials = New System.Net.NetworkCredential(_DiaChiEmail, _MatKhauEmail)
                SMTPServer.EnableSsl = True


                SMTPServer.Send(MyMailMessage)
                CloseWaiting()
                ShowThongBao("Đã gửi thông tin vào mail " & mailTo)
            Catch ex As SmtpException
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End Sub


        Public Shared Sub SendToList(ByVal tbListMail As DataTable, ByVal Subject As String, ByVal Content As String, Optional ByVal BCCTo As String = "", Optional ByVal DSFile As DataTable = Nothing, Optional ByVal tbListMailCC As DataTable = Nothing)
            ShowWaiting("Đang gửi email ...")


            Dim MyMailMessage As New MailMessage()
            MyMailMessage.From = New MailAddress(DiaChiEmail)
            For i As Integer = 0 To tbListMail.Rows.Count - 1
                MyMailMessage.To.Add(tbListMail.Rows(i)("Email"))
            Next
            If Not tbListMailCC Is Nothing Then
                For i As Integer = 0 To tbListMailCC.Rows.Count - 1
                    MyMailMessage.CC.Add(tbListMailCC.Rows(i)("Email"))
                Next
            End If

            If BCCTo <> "" Then
                MyMailMessage.Bcc.Add(BCCTo)
            End If
            MyMailMessage.Subject = Subject
            MyMailMessage.IsBodyHtml = True

            MyMailMessage.Body = Content.Replace(vbCrLf, "<br/>") & " <br/> <br/> " & " Người lập: " & NguoiDung
            If Not DSFile Is Nothing Then
                For i As Integer = 0 To DSFile.Rows.Count - 1
                    MyMailMessage.Attachments.Add(New Attachment(DSFile.Rows(i)("File").ToString))
                Next
            End If

            'Create the SMTPClient object and specify the SMTP GMail server
            Dim SMTPServer As New SmtpClient("smtp.gmail.com")
            SMTPServer.Port = 25

            SMTPServer.Credentials = New System.Net.NetworkCredential(DiaChiEmail, MatKhauEmail)
            SMTPServer.EnableSsl = True
            Try
                SMTPServer.Send(MyMailMessage)
                CloseWaiting()
                ShowThongBao("Đã gửi mail !")
            Catch ex As SmtpException
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End Sub




    End Class

End Namespace

