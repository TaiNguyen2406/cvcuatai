Imports BACSOFT.Db.SqlHelper

Public Class frmUpdateHeThongTkThue

    Private Sub frmUpdateHeThongTkThue_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        LoadDsTaiKhoan()

        If TrangThai.isUpdate Then
            Dim sql As String = "SELECT * FROM TAIKHOANTHUE WHERE TaiKhoan = @TaiKhoan"
            AddParameter("@TaiKhoan", MaTuDien)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                txtTaiKhoan.EditValue = dt.Rows(0)("TaiKhoan")
                cmbTaiKhoanCha.EditValue = dt.Rows(0)("TaiKhoanCha")
                txtTenGoi.EditValue = dt.Rows(0)("TenGoi")
            End If
            'txtTaiKhoan.Enabled = False
        End If

    End Sub


    Private Sub LoadDsTaiKhoan()

        Dim sql As String = "SELECT TaiKhoan,TaiKhoanCha,TenGoi FROM TAIKHOANTHUE ORDER BY TaiKhoan "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        Dim tb2 As DataTable = tb.Copy
        tb2.Rows.Clear()

        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = tb.Rows(i)("TenGoi")
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), 1, tb2)
            End If
        Next

        cmbTaiKhoanCha.Properties.DataSource = tb2

    End Sub

    Private Sub deQuy(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then Continue For
            If tb.Rows(i)("TaiKhoanCha") = idCha Then
                Dim strTen As String = ""
                For j As Integer = 0 To level - 1
                    strTen &= "-- "
                Next
                strTen = " " & strTen & tb.Rows(i)("TenGoi")
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = strTen
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), level + 1, tb2)
            End If
        Next
    End Sub


    Private Sub cmbTaiKhoanCha_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmbTaiKhoanCha.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbTaiKhoanCha.EditValue = DBNull.Value
        End If
    End Sub

    Private Function LuuDuLieu() As Boolean

        If txtTaiKhoan.Text.Trim = "" Then
            ShowBaoLoi("Chưa nhập tài khoản!")
            txtTaiKhoan.Focus()
            Return False
        End If

        If txtTenGoi.Text.Trim = "" Then
            ShowBaoLoi("Chưa nhập tên tài khoản!")
            txtTenGoi.Focus()
            Return False
        End If

        AddParameter("@TaiKhoan", txtTaiKhoan.EditValue)
        AddParameter("@TaiKhoanCha", cmbTaiKhoanCha.EditValue)
        AddParameter("@TenGoi", txtTenGoi.EditValue)

        If TrangThai.isAddNew Then
            MaTuDien = txtTaiKhoan.EditValue
            If doInsert("TAIKHOANTHUE") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Return False
            End If
            Return True
        ElseIf TrangThai.isUpdate Then
            AddParameterWhere("@dk_TaiKhoan", MaTuDien)
            If doUpdate("TAIKHOANTHUE", "TaiKhoan = @dk_TaiKhoan") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Return False
            End If
            Return True
        End If

        Return False

    End Function

    Private Sub btLuuVaDong_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaDong.Click
        If LuuDuLieu() Then
            Me.Close()
        End If
    End Sub

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        If LuuDuLieu() Then
            TrangThai.isAddNew = True
            txtTaiKhoan.EditValue = ""
            txtTenGoi.EditValue = ""
            txtTaiKhoan.Focus()
        End If
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

End Class