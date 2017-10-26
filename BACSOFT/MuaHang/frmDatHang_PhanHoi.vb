Imports BACSOFT.Db.SqlHelper

Public Class frmDatHang_PhanHoi
    Public _SoPhieu As Object
    Public _PhuTrach As Object
    Public _EmailPhuTrach As String
    Public _DeNghi As Boolean = True
    Public _NCC As String

    Private Sub frmDatHang_PhanHoi_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        Dim tg As DateTime = Now
        If _DeNghi Then

            Dim str As String = tg.ToString("dd/MM/yyyy HH:mm") & ":" & vbCrLf
            str &= tbNoiDung.EditValue & vbCrLf

            AddParameterWhere("@ND", str)
            AddParameterWhere("@TG", tg)
            AddParameterWhere("@SP", _SoPhieu)
            If ExecuteSQLNonQuery("UPDATE PHIEUDATHANG SET PheDuyet=2, NoiDungDeNghi = ISNULL(NoiDungDeNghi,'')+@ND WHERE SoPhieu=@SP") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else

                ThemThongBaoChoNV("Đặt hàng: " & _SoPhieu & " - NCC: " & _NCC & " đang đề nghị duyệt" & vbCrLf & str, 1)
                Utils.Email.Send("baoanjsc@gmail.com", "Đặt hàng: " & _SoPhieu & " - NCC: " & _NCC & " đang đề nghị duyệt", str)
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDi_DatHang).cbTrangThaiDH.EditValue = 2
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDi_DatHang).loadDSTongHopVatTuDatHang()
                ShowAlert("Đã cập nhật !")
                Me.Close()
            End If
        Else
            Dim str As String = tg.ToString("dd/MM/yyyy HH:mm") & ":" & vbCrLf
            str &= tbNoiDung.EditValue & vbCrLf
            AddParameterWhere("@ND", str)
            AddParameterWhere("@SP", _SoPhieu)
            If ExecuteSQLNonQuery("UPDATE PHIEUDATHANG SET PheDuyet=0,  NoiDungPhanHoi = ISNULL(NoiDungPhanHoi,'')+@ND WHERE SoPhieu=@SP") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else

                ThemThongBaoChoNV("Có phản hồi cho đơn hàng: " & _SoPhieu & " - NCC: " & _NCC & " đang đề nghị duyệt" & vbCrLf & str, _PhuTrach)
                Utils.Email.Send(_EmailPhuTrach, "Có phản hồi cho đơn hàng: " & _SoPhieu & " - NCC: " & _NCC & " đang đề nghị duyệt", str)
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDi_DatHang).loadDSTongHopVatTuDatHang()
                ShowAlert("Đã cập nhật !")
                Me.Close()
            End If
        End If

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub
End Class