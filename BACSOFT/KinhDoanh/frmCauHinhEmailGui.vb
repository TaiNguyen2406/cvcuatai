Imports BACSOFT.Db.SqlHelper

Public Class frmCauHinhEmailGui

    Private Sub frmCauHinhEmailGui_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim sql As String = " "
        sql &= " IF  EXISTS (SELECT ID,NoiDung,MoTa FROM tblTuDien WHERE Loai=" & LoaiTuDien.EmailMarketing & ")"
        sql &= " BEGIN"
        sql &= " 	SELECT ID,NoiDung,MoTa FROM tblTuDien WHERE Loai=" & LoaiTuDien.EmailMarketing
        sql &= " END"
        sql &= " ELSE"
        sql &= " BEGIN"
        sql &= " 	INSERT INTO tblTuDien(NoiDung,MoTa,Loai)"
        sql &= " 	VALUES(N'baoan@baoanjsc.com.vn',''," & LoaiTuDien.EmailMarketing & ")"
        sql &= " 	SELECT ID,NoiDung,MoTa FROM tblTuDien WHERE Loai=" & LoaiTuDien.EmailMarketing
        sql &= " END"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            tbEmail.EditValue = tb.Rows(0)("NoiDung")
            tbEmail.Tag = tb.Rows(0)("ID")
            If tb.Rows(0)("MoTa").ToString.Trim <> "" Then
                tbMatKhau.EditValue = Utils.BaoMat.GiaiMaMaStr(tb.Rows(0)("MoTa").ToString, key)
            End If
        End If

    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        AddParameter("@NoiDung", tbEmail.EditValue.ToString.Trim)
        AddParameter("@MoTa", Utils.BaoMat.MaHoaStr(tbMatKhau.EditValue.ToString.Trim, key))
        AddParameterWhere("@IDt", tbEmail.Tag)
        If doUpdate("tblTuDien", "ID=@IDt") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật !")
        End If
    End Sub

    Private Sub btHienMK_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btHienMK.MouseDown
        tbMatKhau.Properties.PasswordChar = Nothing
    End Sub

    Private Sub btHienMK_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btHienMK.MouseUp
        tbMatKhau.Properties.PasswordChar = "*"
    End Sub
End Class