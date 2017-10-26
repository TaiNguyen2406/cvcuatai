Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmLoiNhuanNhanVienHT

    Private Sub frmLoiNhuanNhanVien_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        LoadBoPhan()
        ' LoadTakeCare()
        'If tb.Rows.Count > 0 Then
        'cbTakeCare.EditValue = TaiKhoan

        LoadDS()

    End Sub

    Public Sub LoadBoPhan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,Ten FROM NhanSu_BoPhan ORDER BY Ma")
        If Not tb Is Nothing Then
            rcbBoPhan.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadTakeCare()
        Dim sql As String = ""

        sql = " SELECT NHANSU.ID,NHANSU.Ten FROM NHANSU "
        sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=NHANSU.ID AND LUONG.Month=@Thang"
        sql &= " WHERE NHANSU.NoiCtac=74 ORDER BY NHANSU.ID"
        If Not cbBoPhan.EditValue Is Nothing Then
            sql &= " AND LUONG.IDBoPhan=@BP"
        End If

        If Not cbBoPhan.EditValue Is Nothing Then
            AddParameterWhere("@BP", cbBoPhan.EditValue)

        End If
        AddParameterWhere("@Thang", Convert.ToDateTime(tbTuNgay.EditValue).ToString("MM/yyyy"))

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbTakeCare.DataSource = tb

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadDS()
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            Dim sql As String = ""
            sql &= " SELECT NHANSU.Ten as TenNV,LoiNhuan_NhomHT.SoTien,(CASE LoiNhuan_NhomHT.LoaiLN WHEN 1 THEN N'Xuất kho' WHEN 2 THEN N'Thu tiền' END)LoaiLN"
            sql &= " FROM LoiNhuan_NhomHT "
            sql &= " INNER JOIN NHANSU ON NHANSU.ID=LoiNhuan_NhomHT.IDNhanVien"
            If Not cbTakeCare.EditValue Is Nothing Then
                AddParameterWhere("@IDNV", cbTakeCare.EditValue)
                sql &= " AND NHANSU.ID =@IDNV"
            End If
            sql &= " INNER JOIN LUONG ON LUONG.IDNhanVien=LoiNhuan_NhomHT.IDNhanVien AND LUONG.Month =LoiNhuan_NhomHT.Thang"
            If cbTakeCare.EditValue Is Nothing Then
                If Not cbBoPhan.EditValue Is Nothing Then
                    AddParameterWhere("@IDBP", cbBoPhan.EditValue)
                    sql &= " AND LUONG.IDBoPhan =@IDBP"
                End If

            End If
            sql &= " WHERE LoiNhuan_NhomHT.Thang=@Thang"

            AddParameterWhere("@Thang", Convert.ToDateTime(tbTuNgay.EditValue).ToString("MM/yyyy"))

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
            gdv.DataSource = tb

            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            CloseWaiting()
        End Try
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub

    Private Sub rcbTakeCare_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakeCare.ButtonClick
        If e.Button.Index = 1 Then
            cbTakeCare.EditValue = Nothing
        End If
    End Sub


    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Loi nhuan theo nhan vien.xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbBoPhan.ButtonClick
        If e.Button.Index = 1 Then
            cbBoPhan.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbBoPhan.EditValueChanged
        LoadTakeCare()
    End Sub


    Private Sub tbTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTuNgay.EditValueChanged
        LoadTakeCare()
    End Sub
End Class
