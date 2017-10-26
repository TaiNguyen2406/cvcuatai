
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Security.Cryptography
Imports System.Xml
Public Class MySecurity
#Region "md5 encryption"
    Public Shared Function MD5string(v As String) As String
        Dim temp As String = ""
        Dim md5 As New MD5CryptoServiceProvider()
        Dim ac2 As New ASCIIEncoding()
        Dim b As Byte() = New Byte(255) {}
        b = ac2.GetBytes(v)
        b = md5.ComputeHash(b)
        For i As Integer = 0 To b.Length - 1
            temp = temp & b(i).ToString("x2").ToLower()
        Next
        Return temp
    End Function
#End Region
#Region "simple encrypt with key"
    'encrypt with key
    Public Shared Function EnCryptWithKey(strEnCrypt As String, key As String) As String
        Try
            Dim keyArr As Byte()
            Dim EnCryptArr As Byte() = UTF8Encoding.UTF8.GetBytes(strEnCrypt)
            Dim MD5Hash As New MD5CryptoServiceProvider()
            keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            Dim tripDes As New TripleDESCryptoServiceProvider()
            tripDes.Key = keyArr
            tripDes.Mode = CipherMode.ECB
            tripDes.Padding = PaddingMode.PKCS7
            Dim transform As ICryptoTransform = tripDes.CreateEncryptor()
            Dim arrResult As Byte() = transform.TransformFinalBlock(EnCryptArr, 0, EnCryptArr.Length)
            Return Convert.ToBase64String(arrResult, 0, arrResult.Length)
        Catch ex As Exception
        End Try
        Return ""
    End Function
    'decrypt with key
    Public Shared Function DeCryptWithKey(strDecypt As String, key As String) As String
        Try
            Dim keyArr As Byte()
            Dim DeCryptArr As Byte() = Convert.FromBase64String(strDecypt)
            Dim MD5Hash As New MD5CryptoServiceProvider()
            keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            Dim tripDes As New TripleDESCryptoServiceProvider()
            tripDes.Key = keyArr
            tripDes.Mode = CipherMode.ECB
            tripDes.Padding = PaddingMode.PKCS7
            Dim transform As ICryptoTransform = tripDes.CreateDecryptor()
            Dim arrResult As Byte() = transform.TransformFinalBlock(DeCryptArr, 0, DeCryptArr.Length)
            Return UTF8Encoding.UTF8.GetString(arrResult)
        Catch ex As Exception
        End Try
        Return ""
    End Function
#End Region
#Region "hash encryption"
    'encrypt
    Public Shared Function EncryptHas(ToEncrypt As String, useHasing As Boolean, key As String) As String
        Dim keyArray As Byte()
        Dim toEncryptArray As Byte() = UTF8Encoding.UTF8.GetBytes(ToEncrypt)
        If useHasing Then
            Dim hashmd5 As New MD5CryptoServiceProvider()
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            hashmd5.Clear()
        Else
            keyArray = UTF8Encoding.UTF8.GetBytes(key)
        End If
        Dim tDes As New TripleDESCryptoServiceProvider()
        tDes.Key = keyArray
        tDes.Mode = CipherMode.ECB
        tDes.Padding = PaddingMode.PKCS7
        Dim cTransform As ICryptoTransform = tDes.CreateEncryptor()
        Dim resultArray As Byte() = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length)
        tDes.Clear()
        Return Convert.ToBase64String(resultArray, 0, resultArray.Length)
    End Function
    'decrypt
    Public Shared Function DecryptHas(cypherString As String, useHasing As Boolean, key As String) As String
        Dim keyArray As Byte()
        Dim toDecryptArray As Byte() = Convert.FromBase64String(cypherString)
        If useHasing Then
            Dim hashmd As New MD5CryptoServiceProvider()
            keyArray = hashmd.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            hashmd.Clear()
        Else
            keyArray = UTF8Encoding.UTF8.GetBytes(key)
        End If
        Dim tDes As New TripleDESCryptoServiceProvider()
        tDes.Key = keyArray
        tDes.Mode = CipherMode.ECB
        tDes.Padding = PaddingMode.PKCS7
        Dim cTransform As ICryptoTransform = tDes.CreateDecryptor()
        Try
            Dim resultArray As Byte() = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length)

            tDes.Clear()
            Return UTF8Encoding.UTF8.GetString(resultArray, 0, resultArray.Length)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "write file and encrypt"
    'Encrypt and write file
    Public Shared Sub WriteFileAndEncrypt(inputFileString As String, tokenKey As String, outputFilePath As String)
        Try
            Dim keyBytes As Byte()
            keyBytes = Encoding.Unicode.GetBytes(tokenKey)
            Dim derivedKey As New Rfc2898DeriveBytes(tokenKey, keyBytes)
            Dim rijndaelCSP As New RijndaelManaged()
            rijndaelCSP.Key = derivedKey.GetBytes(rijndaelCSP.KeySize / 8)
            rijndaelCSP.IV = derivedKey.GetBytes(rijndaelCSP.BlockSize / 8)

            Dim encryptor As ICryptoTransform = rijndaelCSP.CreateEncryptor()
            Dim encoding__1 As New UTF8Encoding()
            Dim inputFileData As Byte() = encoding__1.GetBytes(inputFileString)
            Dim outputFileStream As New FileStream(outputFilePath, FileMode.Create, FileAccess.Write)
            Dim encryptStream As New CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write)
            encryptStream.Write(inputFileData, 0, inputFileData.Length)
            encryptStream.FlushFinalBlock()
            rijndaelCSP.Clear()
            encryptStream.Close()
            outputFileStream.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'Decrypt and Read file
    Public Shared Function ReadFileAndDecrypt(inputFilePath As String, tokenKey As String) As String
        Try
            Dim keyBytes As Byte() = Encoding.Unicode.GetBytes(tokenKey)

            Dim derivedKey As New Rfc2898DeriveBytes(tokenKey, keyBytes)

            Dim rijndaelCSP As New RijndaelManaged()
            rijndaelCSP.Key = derivedKey.GetBytes(rijndaelCSP.KeySize / 8)
            rijndaelCSP.IV = derivedKey.GetBytes(rijndaelCSP.BlockSize / 8)
            Dim decryptor As ICryptoTransform = rijndaelCSP.CreateDecryptor()

            Dim inputFileStream As New FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)

            Dim decryptStream As New CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read)

            Dim inputFileData As Byte() = New Byte(CInt(inputFileStream.Length) - 1) {}
            decryptStream.Read(inputFileData, 0, CInt(inputFileStream.Length))
            Dim enc As New UTF8Encoding()
            Return enc.GetString(inputFileData)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
#Region "AES 256bit encryption"
    'encrypt
    Public Shared Function EncryptAES(PlainText As String, Password As String, Optional Salt As String = "ObamaSidaKaka", Optional HashAlgorithm As String = "SHA1", Optional PasswordIterations As Integer = 2, Optional InitialVector As String = "OFRna73m*aze01xY", _
     Optional KeySize As Integer = 256) As String
        If String.IsNullOrEmpty(PlainText) Then
            Return ""
        End If
        Dim InitialVectorBytes As Byte() = Encoding.ASCII.GetBytes(InitialVector)
        Dim SaltValueBytes As Byte() = Encoding.ASCII.GetBytes(Salt)
        Dim PlainTextBytes As Byte() = Encoding.UTF8.GetBytes(PlainText)
        Dim DerivedPassword As New PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations)
        Dim KeyBytes As Byte() = DerivedPassword.GetBytes(KeySize / 8)
        Dim SymmetricKey As New RijndaelManaged()
        SymmetricKey.Mode = CipherMode.CBC
        Dim CipherTextBytes As Byte() = Nothing
        Using Encryptor As ICryptoTransform = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes)
            Using MemStream As New MemoryStream()
                Using CryptoStream As New CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write)
                    CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length)
                    CryptoStream.FlushFinalBlock()
                    CipherTextBytes = MemStream.ToArray()
                    MemStream.Close()
                    CryptoStream.Close()
                End Using
            End Using
        End Using
        SymmetricKey.Clear()
        Return Convert.ToBase64String(CipherTextBytes)
    End Function
    'decrypt
    Public Shared Function DecryptAES(CipherText As String, Password As String, Optional Salt As String = "ObamaSidaKaka", Optional HashAlgorithm As String = "SHA1", Optional PasswordIterations As Integer = 2, Optional InitialVector As String = "OFRna73m*aze01xY", _
     Optional KeySize As Integer = 256) As String
        If String.IsNullOrEmpty(CipherText) Then
            Return ""
        End If
        Dim InitialVectorBytes As Byte() = Encoding.ASCII.GetBytes(InitialVector)
        Dim SaltValueBytes As Byte() = Encoding.ASCII.GetBytes(Salt)
        Dim CipherTextBytes As Byte() = Convert.FromBase64String(CipherText)
        Dim DerivedPassword As New PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations)
        Dim KeyBytes As Byte() = DerivedPassword.GetBytes(KeySize / 8)
        Dim SymmetricKey As New RijndaelManaged()
        SymmetricKey.Mode = CipherMode.CBC
        Dim PlainTextBytes As Byte() = New Byte(CipherTextBytes.Length - 1) {}
        Dim ByteCount As Integer = 0
        Using Decryptor As ICryptoTransform = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes)
            Using MemStream As New MemoryStream(CipherTextBytes)
                Using CryptoStream As New CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read)

                    ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length)
                    MemStream.Close()
                    CryptoStream.Close()
                End Using
            End Using
        End Using
        SymmetricKey.Clear()
        Return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount)
    End Function
#End Region
End Class
