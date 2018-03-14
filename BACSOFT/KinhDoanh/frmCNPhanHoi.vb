Imports BACSOFT.Db.SqlHelper
Public Class frmCNPhanHoi
    Public _soyc As Object
    Public _phanhoi As Object
    Private Sub frmCNPhanHoi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not _soyc Is Nothing And Not IsDBNull(_soyc) Then
            txtSoYC.EditValue = _soyc.ToString
        End If
        If Not _phanhoi Is Nothing And Not IsDBNull(_phanhoi) Then
            mmoPhanHoi.EditValue = _phanhoi.ToString
        End If
    End Sub

    Private Sub btnDong_Click(sender As Object, e As EventArgs) Handles btnDong.Click
        _soyc = Nothing
        _phanhoi = Nothing
        Close()
    End Sub

    Private Sub btnLuu_Click(sender As Object, e As EventArgs) Handles btnLuu.Click
        AddParameter("@PhanHoi", mmoPhanHoi.EditValue)
        AddParameterWhere("@Sophieu", txtSoYC.EditValue)
        If doUpdate("YeuCauTuWeb", "Sophieu = @Sophieu") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        ShowAlert("Đã lưu dữ liệu!")
    End Sub
End Class