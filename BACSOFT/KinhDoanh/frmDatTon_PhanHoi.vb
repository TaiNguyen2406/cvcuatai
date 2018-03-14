Imports BACSOFT.Db.SqlHelper

Public Class frmDatTon_PhanHoi
    Public _Id As Integer
    Public _SoPhieu As Object
    Public _PhuTrach As Object
    Public _EmailPhuTrach As String
    Public _DeNghi As Boolean = True
    Public _TenVT As Object
    Public _NCC As String
    Private Sub frmDatTon_PhanHoi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sql As String = "select PhanHoi from DAT_TON_CHI_TIET where Id=@Id"
        AddParameterWhere("@Id", _Id)
        tbNoiDung.EditValue = ExecuteSQLScalar(sql)

    End Sub

    Private Sub btXacNhan_Click(sender As Object, e As EventArgs) Handles btXacNhan.Click
        Dim tg As DateTime = GetServerTime()
        If _DeNghi Then
            Dim str As String = tg.ToString("dd/MM/yyyy HH:mm") & ":" & vbCrLf
            str &= tbNoiDung.EditValue & vbCrLf

            ThemThongBaoChoNV("Đặt tồn: " & _TenVT & " Cho KH: " & _NCC & " của KD: " & _PhuTrach & " đang đề nghị duyệt" & vbCrLf & str, 1)
            Dim dt As DataTable = ExecuteSQLDataTable(meSql.EditValue)
            ' Utils.Email.Send("baoanjsc@gmail.com", "Đặt hàng: " & _SoPhieu & " - NCC: " & _NCC & " đang đề nghị duyệt", str)
            For i = 0 To dt.Rows.Count - 1
                Utils.Email.Send(dt.Rows(i)(0).ToString, "Đặt tồn: " & _TenVT & " Cho KH: " & _NCC & " của KD: " & _PhuTrach & " đang đề nghị duyệt", str)
            Next
            ShowAlert("Đã cập nhật !")
            Me.Close()
        Else
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.KiemDuyet) Then
                AddParameter("@PhanHoi", tbNoiDung.EditValue)
                AddParameterWhere("@Id", _Id)
                If doUpdate("DAT_TON_CHI_TIET", "Id=@Id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã phản hồi ")
                    Me.Close()
                End If
            End If
        End If


    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Me.Close()
    End Sub
End Class