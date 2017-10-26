Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmLNPhanBoTheoXK

    Private Sub frmLNPhanBoTheoXK_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbThang.EditValue = Today.ToString("MM")
        tbNam.EditValue = Today.Year
        LoadPhongBan()
        LoadTakeCare()
        LoadDSNhomKN()
        'If tb.Rows.Count > 0 Then
        'cbTakeCare.EditValue = TaiKhoan

        LoadDS()

    End Sub

    Public Sub LoadPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadDSNhomKN()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=11 ORDER BY Ma")
        If Not tb Is Nothing Then
            rcbNhomKN.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadTakeCare()
        Dim sql As String = ""
        If cbPhong.EditValue Is Nothing Then
            sql = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER BY ID"
        Else
            sql = " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment= " & cbPhong.EditValue & " ORDER BY ID "
        End If
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

            Dim sql As String = " SELECT SoPhieu,TenNV,Level,CongTrinh,PhuTrachHD,KhaoSatCG,XucTienHD,PhuTrachTC,ThiCong,PhatTrienSP,(PhuTrachHD + KhaoSatCG + XucTienHD + PhuTrachTC + ThiCong + PhatTrienSP)Tong"
            sql &= " FROM ("
            sql &= " SELECT PhieuXuatKho_LoiNHuan.SoPhieu,NHANSU.Ten as TenNV,PHIEUXUATKHO.CongTrinh,LUONG.Level,"
            sql &= " Sum(PhieuXuatKho_LoiNHuan.PhuTrachHD) as PhuTrachHD,"
            sql &= " Sum(PhieuXuatKho_LoiNHuan.KhaoSatCG) as KhaoSatCG,"
            sql &= " Sum(PhieuXuatKho_LoiNHuan.XucTienHD) as XucTienHD,"
            sql &= " Sum(PhieuXuatKho_LoiNHuan.PhuTrachTC) as PhuTrachTC,"
            sql &= " Sum(PhieuXuatKho_LoiNHuan.ThiCong) as ThiCong,"
            sql &= " Sum(PhieuXuatKho_LoiNHuan.PhatTrienSP) as PhatTrienSP"
            sql &= " FROM PhieuXuatKho_LoiNHuan"
            sql &= " INNER JOIN NHANSU ON NHANSU.ID=PhieuXuatKho_LoiNhuan.IDNhanVien"
            If cbTakeCare.EditValue Is Nothing Then
                If cbPhong.EditValue IsNot Nothing Then
                    sql &= " AND NHANSU.IDDepatment=@IDP"
                    AddParameterWhere("@IDP", cbPhong.EditValue)
                End If
            Else
                sql &= " AND NHANSU.ID=@IDNV"
                AddParameterWhere("@IDNV", cbTakeCare.EditValue)
            End If
            sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=PhieuXuatKho_LoiNhuan.SoPhieu"
            sql &= " LEFT JOIN LUONG ON LUONG.IDNhanVien=PhieuXuatKho_LoiNHuan.IDNhanVien AND LUONG.Month=Right(Convert(nvarchar,PhieuXuatKho.NgayThang,103),7)"

            sql &= " WHERE 1=1 "
            If tbThang.EditValue Is Nothing Then
                AddParameterWhere("@Nam", tbNam.EditValue.ToString)
                sql &= " AND year(PhieuXuatkho.NgayThang)=@Nam"

            Else
                AddParameterWhere("@TG", tbThang.EditValue + "/" + tbNam.EditValue.ToString)
                sql &= " AND Right(Convert(nvarchar,PhieuXuatKho.NgayThang,103),7)=@TG"
            End If
            sql &= " GROUP BY PhieuXuatKho_LoiNHuan.SoPhieu,NHANSU.Ten,PHIEUXUATKHO.CongTrinh,Level"
            sql &= " )tb"
            sql &= " ORDER BY SoPhieu"
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
        saveFile.FileName = "Loi nhuan theo xuat kho.xls"

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

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadTakeCare()
    End Sub


    Private Sub rcbThang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbThang.ButtonClick
        If e.Button.Index = 1 Then
            tbThang.EditValue = Nothing
        End If
    End Sub


End Class
