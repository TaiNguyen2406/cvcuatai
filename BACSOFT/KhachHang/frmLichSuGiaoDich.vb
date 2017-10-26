Imports BACSOFT.Db.SqlHelper

Public Class frmLichSuGiaoDich
    Public _IDKH As Object

    Private Sub frmLichSuGiaoDich_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadTakecare()
        loadHinhThuc()
        If TrangThai.isAddNew Then
            Me.Text = "Thêm lịch sử giao dịch với KH"
            tbThoiGian.EditValue = GetServerTime()
        Else
            AddParameterWhere("@ID", objID)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT * FROM GIAODICHKH WHERE ID=@ID")
            If Not dt Is Nothing Then
                tbThoiGian.EditValue = dt.Rows(0)("ThoiGian")
                tbNoiDung.EditValue = dt.Rows(0)("NoiDungGiaoDich")
                cbTakecare.EditValue = dt.Rows(0)("IDTakeCare")
                'If Not IsDBNull(dt.Rows(0)("HinhThuc")) Then
                cbHinhThuc.EditValue = dt.Rows(0)("HinhThuc")
                'End If

                Me.Text = "Cập nhật thông tin giao dịch khách hàng"
            End If
            tbThoiGian.Enabled = False
            cbTakecare.Enabled = False
        End If

    End Sub

    Private Sub loadTakecare()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            cbTakecare.Properties.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub loadHinhThuc()
        Dim sql As String = ""
        sql &= " SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma "
        AddParameterWhere("@Loai", LoaiTuDien.GiaoDichKH)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            cbHinhThuc.Properties.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub btThem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btThem.Click
        If tbNoiDung.EditValue.ToString = "" Then
            ShowCanhBao("Chưa có nội dung giao dịch")
            Exit Sub
        End If
        GhiLai()
        TrangThai.isAddNew = True
        tbNoiDung.EditValue = ""
        tbThoiGian.EditValue = GetServerTime()
    End Sub

    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click
        If tbNoiDung.EditValue.ToString = "" Then
            ShowCanhBao("Chưa có nội dung giao dịch")
            Exit Sub
        End If
        GhiLai()
        Me.Close()
    End Sub

    Private Sub GhiLai()
        Try

            AddParameter("@ThoiGian", tbThoiGian.EditValue)
            AddParameter("@IDTakeCare", cbTakecare.EditValue)
            AddParameter("@NoiDungGiaoDich", tbNoiDung.EditValue)
            AddParameter("@HinhThuc", cbHinhThuc.EditValue)
            AddParameter("@IDKH", _IDKH)
            If TrangThai.isAddNew Then
                If doInsert("GIAODICHKH") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@ID", objID)
                If doUpdate("GIAODICHKH", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mDSKhachHang" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKhachHang).loadDS()
                End If
            Next

            ShowAlert(Me.Text & " thành công !")

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub


    Private Sub btDong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub cbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTakecare.ButtonClick
        If e.Button.Index = 1 Then
            cbTakecare.EditValue = Nothing
        End If
    End Sub


End Class