Imports BACSOFT.Db.SqlHelper

Public Class frmChuyenGiaoKH
    Public _IDKH As Object

    Private Sub frmChuyenGiaoKH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadTakecare()

        If TrangThai.isAddNew Then
            Me.Text = "Thêm quá trình chuyển giao khách hàng"
            tbThoiGian.EditValue = GetServerTime()
        Else
            AddParameterWhere("@ID", objID)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT * FROM GIAODICHKH WHERE ID=@ID")
            If Not dt Is Nothing Then
                tbThoiGian.EditValue = dt.Rows(0)("ThoiGian")
                tbNoiDung.EditValue = dt.Rows(0)("NoiDungGiaoDich")
                cbNguoiNhan.EditValue = dt.Rows(0)("IDTakeCare")
                cbNguoiChuyen.EditValue = dt.Rows(0)("IDNgChuyen")
                Me.Text = "Cập nhật thông tin chuyển giao khách hàng"
            End If
        End If

    End Sub

    Private Sub loadTakecare()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            cbNguoiChuyen.Properties.DataSource = dt
            cbNguoiNhan.Properties.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub btGhi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btGhi.Click
        If tbNoiDung.EditValue.ToString = "" Then
            ShowCanhBao("Chưa có nội dung giao dịch")
            Exit Sub
        End If
        Try
            BeginTransaction()
            AddParameter("@ThoiGian", tbThoiGian.EditValue)
            AddParameter("@IDTakeCare", cbNguoiNhan.EditValue)
            AddParameter("@NoiDungGiaoDich", tbNoiDung.EditValue)
            AddParameter("@ChuyenGiao", True)
            AddParameter("@IDNgChuyen", cbNguoiChuyen.EditValue)
            AddParameter("@IDKH", _IDKH)
            If TrangThai.isAddNew Then
                If doInsert("GIAODICHKH") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@ID", objID)
                If doUpdate("GIAODICHKH", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            AddParameter("@IDTakeCare", cbNguoiNhan.EditValue)
            AddParameterWhere("@ID", _IDKH)
            If doUpdate("KHACHHANG", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            ComitTransaction()

            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = "mDSKhachHang" Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmKhachHang).loadDS()
                End If
            Next


            ShowAlert(Me.Text & " thành công !")

        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btDong_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub cbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNguoiChuyen.ButtonClick, cbNguoiNhan.ButtonClick
        If e.Button.Index = 1 Then
            cbNguoiChuyen.EditValue = Nothing
        End If
    End Sub


End Class