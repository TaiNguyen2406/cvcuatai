Imports System.Security.Cryptography
Namespace Utils
    Public Class BaoMat
        Public Shared Function MaHoaStr(ByVal strText As String, ByVal strEncrKey As String) As String
            Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
            Try
                Dim bykey() As Byte = System.Text.Encoding.UTF8.GetBytes(strEncrKey)
                Dim InputByteArray() As Byte = System.Text.Encoding.UTF8.GetBytes(strText)
                Dim des As New DESCryptoServiceProvider
                Dim ms As New System.IO.MemoryStream
                Dim cs As New CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write)
                cs.Write(InputByteArray, 0, InputByteArray.Length)
                cs.FlushFinalBlock()
                Return Convert.ToBase64String(ms.ToArray())
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Public Shared Function GiaiMaMaStr(ByVal strText As String, ByVal sDecrKey As String) As String
            Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
            Dim inputByteArray(strText.Length) As Byte
            Try
                Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(sDecrKey)
                Dim des As New DESCryptoServiceProvider
                inputByteArray = Convert.FromBase64String(strText)
                Dim ms As New System.IO.MemoryStream
                Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function
    End Class

End Namespace

