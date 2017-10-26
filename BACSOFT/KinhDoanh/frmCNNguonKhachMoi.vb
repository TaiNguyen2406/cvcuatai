Imports DevExpress.XtraTreeList.Nodes
Imports BACSOFT.Db.SqlHelper

Public Class frmCNNguonKhachMoi

    Private Sub frmCNYeuCauNoiBo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSNhom()
        If TrangThai.isAddNew Then
            Me.Text = "Thêm liên hệ từ khách mới"

        Else
            Me.Text = "Cập nhật thông tin liên hệ từ khách mới"
            Dim sql As String = ""
            AddParameterWhere("@ID", objID)

            sql = "SELECT * FROM tblNguonKhachMoi WHERE ID=@ID"

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tbThoiGianLap.EditValue = tb.Rows(0)("ThoiGian")
                tbNoiDung.EditValue = tb.Rows(0)("NoiDung")
                cbNhom.EditValue = Convert.ToByte(tb.Rows(0)("IDNguon"))
            End If
        End If
    End Sub

    Private Sub LoadDSNhom()
        AddParameterWhere("@Loai", LoaiTuDien.NguonKhachMoi)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            cbNhom.Properties.DataSource = tb
        End If
    End Sub


    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click

        Dim tg As DateTime = GetServerTime()
        AddParameter("@ThoiGian", tg)
        AddParameter("@IDNguon", cbNhom.EditValue)
        AddParameter("@NoiDung", tbNoiDung.EditValue)

        If TrangThai.isAddNew Then
            AddParameter("@IDNhanVien", TaiKhoan)
            objID = doInsert("tblNguonKhachMoi")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                TrangThai.isUpdate = True
                Me.Text = "Cập nhật liên hệ khách mới"
                'If chkGuiMail.Checked Then
                '    BACSOFT.Utils.Email.SendToList(
                'End If
                tbThoiGianLap.EditValue = tg
                ShowAlert("Đã thêm liên hệ !")
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNguonKhachMoi).LoadDS()

            End If
        Else
            AddParameterWhere("@ID", objID)
            If doUpdate("tblNguonKhachMoi", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật liên hệ !")
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmNguonKhachMoi).LoadDS()
            End If
        End If
    End Sub

    Private Sub cbNhom_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNhom.ButtonClick
        If e.Button.Index = 1 Then
            cbNhom.EditValue = Nothing
        End If
    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        TrangThai.isAddNew = True
        tbThoiGianLap.EditValue = Nothing
        tbNoiDung.EditValue = ""
        Me.Text = "Thêm liên hệ từ khách mới"
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub
End Class