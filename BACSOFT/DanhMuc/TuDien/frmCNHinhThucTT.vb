Imports BACSOFT.Db.SqlHelper

Public Class frmCNHinhThucTT

    Private Sub frmCNHinhThucTT_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadThoiDiem()
        Dim sql As String = ""
        If TrangThai.isAddNew Then
            Me.Text = "Thêm hình thức thanh toán"
            Dim tb As New DataTable
            tb.Columns.Add("ID")
            tb.Columns.Add("ThanhToanPT")
            tb.Columns.Add("Ngay")
            tb.Columns.Add("IDThoiDiemTT")
            gdvTT.DataSource = tb
        Else
            Me.Text = "Cập nhật hình thức thanh toán"
            sql &= "SELECT * FROM tblHinhThucTTKH WHERE ID=@ID "
            sql &= "  SELECT * FROM tblHinhThucTTKHCT WHERE IDHinhThucTTKH = @ID "
            AddParameterWhere("@ID", MaTuDien)
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If Not ds Is Nothing Then
                tbHinhThucTT_VIE.EditValue = ds.Tables(0).Rows(0)("HinhThucTT_VIE")
                tbHinhThucTT_ENG.EditValue = ds.Tables(0).Rows(0)("HinhThucTT_ENG")
                gdvTT.DataSource = ds.Tables(1)
            End If
        End If
    End Sub

    Private Sub loadThoiDiem()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=5 ORDER By Ma")
        If Not tb Is Nothing Then
            rcbThoiDiemTT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btLuuVaDong_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaDong.Click
        GhiLai()
        Me.Close()
    End Sub

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        GhiLai()
        TrangThai.isAddNew = True
        tbHinhThucTT_ENG.EditValue = ""
        tbHinhThucTT_VIE.EditValue = ""
        Dim tb As New DataTable
        tb.Columns.Add("ID")
        tb.Columns.Add("ThanhToanPT")
        tb.Columns.Add("Ngay")
        tb.Columns.Add("IDThoiDiemTT")
        gdvTT.DataSource = tb
    End Sub

    Private Sub GhiLai()
        gdvTTCT.CloseEditor()
        gdvTTCT.UpdateCurrentRow()
        Try
            BeginTransaction()
            AddParameter("@HinhThucTT_VIE", tbHinhThucTT_VIE.EditValue)
            AddParameter("@HinhThucTT_ENG", tbHinhThucTT_ENG.EditValue)
            If TrangThai.isAddNew Then
                MaTuDien = doInsert("tblHinhThucTTKH")
                If MaTuDien Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@ID", MaTuDien)
                If doUpdate("tblHinhThucTTKH", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            AddParameterWhere("@ID", MaTuDien)
            If doDelete("tblHinhThucTTKHCT", "IDHinhThucTTKH=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            For i As Integer = 0 To gdvTTCT.RowCount - 2
                AddParameter("@IDHinhThucTTKH", MaTuDien)
                AddParameter("@ThanhToanPT", gdvTTCT.GetRowCellValue(i, "ThanhToanPT"))
                AddParameter("@Ngay", gdvTTCT.GetRowCellValue(i, "Ngay"))
                AddParameter("@IDThoiDiemTT", gdvTTCT.GetRowCellValue(i, "IDThoiDiemTT"))
                If doInsert("tblHinhThucTTKHCT") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next

            ComitTransaction()

            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmHinhThucTT).LoadDS()
            ShowAlert(Me.Text & " thành công!")

        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try


    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub
End Class