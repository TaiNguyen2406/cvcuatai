Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmBCTongHop
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _DaThu As Double = 0

    Private Sub frmBCTongHop_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbNam.EditValue = _ToDay.Year
    End Sub

    Private Sub LoadDS()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"
        sql &= " Select "
        sql &= " N'Góp vốn' AS Col1, N'VNĐ' AS Col2, "
        Dim _thang As String = ""
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = (select sum(THU.Sotien) from THU Where THU.Mucdich = 103 and THU.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' )"
            If i < 12 Then
                sql &= ","
            End If
        Next

        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'TSCĐ' AS Col1, N'VNĐ' AS Col2, "
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = (select isnull( sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 214 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 214 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' )"
            If i < 12 Then
                sql &= ","
            End If
        Next
 
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'Tồn kho' AS Col1, N'VNĐ' AS Col2, "
        sql &= " Thang1  = NULL,"
        sql &= " Thang2  = NULL,"
        sql &= " Thang3  = NULL,"
        sql &= " Thang4  = NULL,"
        sql &= " Thang5  = NULL,"
        sql &= " Thang6  = NULL,"
        sql &= " Thang7  = NULL,"
        sql &= " Thang8  = NULL,"
        sql &= " Thang9  = NULL,"
        sql &= " Thang10 = NULL,"
        sql &= " Thang11 = NULL,"
        sql &= " Thang12 = NULL"

        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'Mua vào' AS Col1, N'VNĐ' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = (select isnull(sum((tientruocthue + tienthue)*tygia),0) from PHIEUNHAPKHO where PHIEUNHAPKHO.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' )"
            If i < 12 Then
                sql &= ","
            End If
        Next
   
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'Bán vào' AS Col1, N'VNĐ' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = (select isnull(sum((tientruocthue + tienthue)*tygia),0) from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' )"
            If i < 12 Then
                sql &= ","
            End If
        Next
     
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'Phải thu' AS Col1, N'VNĐ' AS Col2,"
        sql &= " Thang1  = NULL,"
        sql &= " Thang2  = NULL,"
        sql &= " Thang3  = NULL,"
        sql &= " Thang4  = NULL,"
        sql &= " Thang5  = NULL,"
        sql &= " Thang6  = NULL,"
        sql &= " Thang7  = NULL,"
        sql &= " Thang8  = NULL,"
        sql &= " Thang9  = NULL,"
        sql &= " Thang10 = NULL,"
        sql &= " Thang11 = NULL,"
        sql &= " Thang12 = NULL"

        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'Phải trả' AS Col1, N'VNĐ' AS Col2,"
        sql &= " Thang1  = NULL,"
        sql &= " Thang2  = NULL,"
        sql &= " Thang3  = NULL,"
        sql &= " Thang4  = NULL,"
        sql &= " Thang5  = NULL,"
        sql &= " Thang6  = NULL,"
        sql &= " Thang7  = NULL,"
        sql &= " Thang8  = NULL,"
        sql &= " Thang9  = NULL,"
        sql &= " Thang10 = NULL,"
        sql &= " Thang11 = NULL,"
        sql &= " Thang12 = NULL"

        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'Thu' AS Col1, N'C Nghiệp' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(sotien),0) from THU where Mucdich = 100 and THU.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(sotien),0) from THUNH where Mucdich = 100 and THUNH.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
    
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'D Dụng' AS Col2,"
        sql &= " Thang1  = NULL,"
        sql &= " Thang2  = NULL,"
        sql &= " Thang3  = NULL,"
        sql &= " Thang4  = NULL,"
        sql &= " Thang5  = NULL,"
        sql &= " Thang6  = NULL,"
        sql &= " Thang7  = NULL,"
        sql &= " Thang8  = NULL,"
        sql &= " Thang9  = NULL,"
        sql &= " Thang10 = NULL,"
        sql &= " Thang11 = NULL,"
        sql &= " Thang12 = NULL"

        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'KD T Chính' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(sotien),0) from THU where Mucdich = 110 and THU.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(sotien),0) from THUNH where Mucdich = 110 and THUNH.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
           sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'Chi' AS Col1, N'Mua CNghiệp' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 210 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 210 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
  
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Mua D Dụng' AS Col2,"
        sql &= " Thang1  = NULL,"
        sql &= " Thang2  = NULL,"
        sql &= " Thang3  = NULL,"
        sql &= " Thang4  = NULL,"
        sql &= " Thang5  = NULL,"
        sql &= " Thang6  = NULL,"
        sql &= " Thang7  = NULL,"
        sql &= " Thang8  = NULL,"
        sql &= " Thang9  = NULL,"
        sql &= " Thang10 = NULL,"
        sql &= " Thang11 = NULL,"
        sql &= " Thang12 = NULL"

        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'LCBCNV' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich =209 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' )+ (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich in (209,234) and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
              sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'BHXH' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 226 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 226 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
  
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'CPCVP' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich in (201,202,203,204,212,221,218,225) and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich in (201,202,203,204,212,221,218,225) and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
   
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'ĐThoại, Fax' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich =206 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 206  and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
 
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Điện SH' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 207 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 207 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
      
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Mua D Dụng' AS Col2,"

        sql &= " Thang1  = NULL,"
        sql &= " Thang2  = NULL,"
        sql &= " Thang3  = NULL,"
        sql &= " Thang4  = NULL,"
        sql &= " Thang5  = NULL,"
        sql &= " Thang6  = NULL,"
        sql &= " Thang7  = NULL,"
        sql &= " Thang8  = NULL,"
        sql &= " Thang9  = NULL,"
        sql &= " Thang10 = NULL,"
        sql &= " Thang11 = NULL,"
        sql &= " Thang12 = NULL"

        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Vận tải' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich =205 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' )+ (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich =205 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
             sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Tiếp khách' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 215 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 215 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
            sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Quảng cáo' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 222 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 222 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
              sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Tri ân KH' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 247 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 247 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
             sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Bảo hành' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 241 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 241 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
            sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Cổ tức' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 223 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' )+ (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 223 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
             sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Khác' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 240 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 240 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
       sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Ký quỹ' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 227 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' )+ (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 227 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
           sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Phí N Khẩu' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich = 228 and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich = 228 and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
              sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Thuế' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich IN (219,220) and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich IN (219,220) and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
   
        sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'Gửi giá' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(CHI.Sotien),0) from CHI Where CHI.Mucdich IN (200,224) and CHI.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ) + (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Mucdich IN(200,224) and UNC.Sophieu like  '" & tbNam.EditValue.ToString.Substring(2, 2) & _thang & "%' ))"
            If i < 12 Then
                sql &= ","
            End If
        Next
             sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'T Mặt' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select ISNULL(sum(THU.Sotien),0) from THU Where THU.NgaythangCT <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, i + 1, 0)) & "',103)) - (select isnull(sum(CHI.Sotien),0) from CHI Where CHI.NgaythangCT <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, i + 1, 0)) & "',103)))"
            If i < 12 Then
                sql &= ","
            End If
        Next
             sql &= " UNION ALL"
        sql &= " Select "
        sql &= " N'' AS Col1, N'N Hàng' AS Col2,"
        For i As Integer = 1 To 12
            If i < 10 Then
                _thang = "0" & i
            Else
                _thang = i
            End If
            sql &= " Thang" & i & " = ((select isnull(sum(THUNH.Sotien),0) from THUNH Where THUNH.NgaythangCT <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, i + 1, 0)) & "',103)) + (select ISNULL(sum(CHI.Sotien),0) from CHI Where CHI.NgaythangCT <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, i + 1, 0)) & "',103) and CHI.mucdich = 211) - (select isnull(sum(UNC.Sotien),0) from UNC Where UNC.Ngaythang <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, i + 1, 0)) & "',103)) - (select isnull(sum(THU.Sotien),0) from THU Where THU.Mucdich = 101 and THU.NgaythangCT <= convert(datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, i + 1, 0)) & "',103)))"
            If i < 12 Then
                sql &= ","
            End If
        Next
     

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            gdv.DataSource = tb
                        CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadDS()
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "BC Tong hop " & tbNam.EditValue.ToString & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)

                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    'Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs)
    '    btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    'End Sub


End Class