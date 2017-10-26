Imports BACSOFT.Db.SqlHelper

Public Class frmCNNangLuc


    Private Sub frmCNNangLuc_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSNhom()
        If TrangThai.isAddNew Then
            Me.Text = "Thêm năng lực"
            tbDiemChuan.EditValue = 10
        Else
            Me.Text = "Cập nhật năng lực"
            AddParameterWhere("@ID", objID)
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblNangLuc where ID=@ID")
            If Not tb Is Nothing Then
                tbTenNangLuc.EditValue = tb.Rows(0)("Ten")
                tbDiemChuan.EditValue = tb.Rows(0)("DiemChuan")
                cbNhom.EditValue = CType(tb.Rows(0)("IDNhom"), Byte)
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If
    End Sub

    Public Sub LoadDSNhom()
        AddParameterWhere("@Nhom", LoaiTuDien.NhomNangLuc)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung AS TenNhom FROM tblTuDien WHERE Loai=@Nhom ORDER BY Ma")
        If Not tb Is Nothing Then
            cbNhom.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Function GhiLai() As Boolean
        AddParameter("@Ten", tbTenNangLuc.EditValue)
        AddParameter("@IDNhom", cbNhom.EditValue)
        AddParameter("@DiemChuan", tbDiemChuan.EditValue)
        If TrangThai.isAddNew Then
            objID = doInsert("tblNangLuc")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Return False
            End If
        Else
            AddParameterWhere("@ID", objID)
            If doUpdate("tblNangLuc", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        If GhiLai() Then
            'tbTenKyNang.EditValue = ""
            tbTenNangLuc.EditValue = ""
            tbDiemChuan.EditValue = 10
        End If
    End Sub

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        If GhiLai() Then
            Me.Close()
        End If
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub


    Private Sub cbNhom_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNhom.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNNhomNangLuc
            f.ShowDialog()
        ElseIf e.Button.Index = 2 Then
            cbNhom.EditValue = Nothing
        End If
    End Sub

    Private Sub frmCNNangLuc_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not objID Is Nothing Then
            If Me.Tag = "DIEMTHI" Then
                fCNDiemThiNangLuc.LoadDSNangLuc()
                fCNDiemThiNangLuc.cbNangLuc.EditValue = CType(objID, Int32)
            ElseIf Me.Tag = "CNNL" Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmTHDiemThiNangLuc).loadDSNangLuc()
            End If
            
        End If
    End Sub
End Class