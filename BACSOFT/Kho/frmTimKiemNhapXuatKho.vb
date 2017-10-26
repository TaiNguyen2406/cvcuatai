Public Class frmTimKiemNhapXuatKho

    Public isOK As Boolean = False
    Public sqlWhere As String = ""
    Private Sub frmTimKiemNhapXuatKho_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        txtTuNgay.EditValue = tg.AddDays(-30)
        txtDenNgay.EditValue = tg
        If txtModel.Enabled Then
            txtModel.Focus()
        Else
            txtNoiDung.Focus()
        End If
    End Sub


    Private Sub btTimKiem_Click(sender As System.Object, e As System.EventArgs) Handles btTimKiem.Click

        'If txtModel.Enabled And txtModel.Text.Trim = "" Then
        '    ShowCanhBao("Chưa nhập mã model!")
        '    Exit Sub
        'ElseIf txtNoiDung.Enabled And txtNoiDung.Text = "" Then
        '    ShowCanhBao("Chưa nhập nội dung!")
        '    Exit Sub
        'End If

        isOK = True
        If Not txtNoiDung.Enabled Then
            sqlWhere = " AND Sophieu IN (SELECT DISTINCT Sophieu FROM NHAPKHO WHERE IDVattu IN (SELECT ID FROM VatTu WHERE Model LiKe N'%" & txtModel.Text & "%') "
            If chkLocTheoHD.Checked Then sqlWhere &= " AND Nhapthue = 0 "
            sqlWhere &= " ) "
        Else
            If txtModel.Text <> "" Then
                sqlWhere = " AND Sophieu IN (SELECT DISTINCT Sophieu FROM XUATKHO WHERE IDVattu IN (SELECT ID FROM VatTu WHERE Model LiKe N'%" & txtModel.Text & "%') "
                If chkLocTheoHD.Checked Then sqlWhere &= " AND Xuatthue = 0 "
                sqlWhere &= " ) "
            ElseIf txtNoiDung.Text <> "" Then
                sqlWhere = " AND Sophieu IN (SELECT DISTINCT Sophieu FROM XUATKHOAUX WHERE Noidung LiKe N'%" & txtNoiDung.Text & "%' "
                If chkLocTheoHD.Checked Then sqlWhere &= " AND Xuatthue = 0 "
                sqlWhere &= " ) "
            Else
                Dim dk As String = ""
                If chkLocTheoHD.Checked Then dk = " AND Xuatthue = 0 "
                sqlWhere = " AND Sophieu IN ( SELECT Sophieu from XUATKHO WHERE 1=1 " & dk
                sqlWhere &= " UNION ALL "
                sqlWhere &= " SELECT Sophieu from XUATKHOAUX WHERE 1=1 " & dk
                sqlWhere &= " ) "
            End If
        End If

        Me.Close()

    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub

    Private Sub frmTimKiemNhapXuatKho_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not isOK Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub txtModel_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtModel.EditValueChanged
        If txtModel.Text <> "" Then txtNoiDung.Text = ""
    End Sub

    Private Sub txtNoiDung_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtNoiDung.EditValueChanged
        If txtNoiDung.Text <> "" Then txtModel.Text = ""
    End Sub

    Private Sub txtDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        Dim tg As DateTime = txtDenNgay.EditValue
        txtTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub


End Class