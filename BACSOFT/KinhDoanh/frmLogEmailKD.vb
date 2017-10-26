Imports System.IO
Imports System.Drawing
Imports BACSOFT.Db.SqlHelper
Imports System.Xml
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Public Class frmLogEmailKD
    Public _IdEmail As Object
    Private Sub frmLogEmailKD_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim sql As String = "SELECT Id,TenNguoiNhan,EmailNguoiNhan,EmailNguoiGui,ThoiGian,TrangThai,GhiChu,IdNguoiGui from LOG_EMAILKD WHERE IdEmail = " & _IdEmail & " ORDER BY ThoiGian DESC"
        gdvEmail.DataSource = ExecuteSQLDataTable(sql)
    End Sub

End Class