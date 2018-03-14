Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmTongHopDiemKyNang

    Private Sub frmTongHopDiemKyNang_Load(sender As Object, e As System.EventArgs) Handles Me.Load
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

            '  End If

            Dim sql As String = ""
            sql &= " SELECT NhanSu_DiemKyNang.Thang,NhanSu.Ten as NhanVien,NhanSU_DiemKyNang.Diem,"
            sql &= " tblTuDien.NoiDung as NhomKN"
            sql &= " FROM NhanSu_DiemKyNang"
            sql &= " INNER JOIN tblTuDien ON NhanSu_DiemKyNang.IDNhomKN=tblTuDien.Ma AND Loai=11"
            sql &= " INNER JOIN NHANSU ON NHANSU.ID=NhanSU_DiemKyNang.IDNhanVien and NHANSU.TrangThai=1"
            sql &= " WHERE 1=1 "
            If tbThang.EditValue Is Nothing Then
                If tbNam.EditValue > 0 Then
                    AddParameterWhere("@Nam", tbNam.EditValue.ToString)
                    sql &= " AND Right(NhanSu_DiemKyNang.Thang,4)=@Nam"
                End If
                colThang.Visible = True
                sql &= " ORDER BY Right(NhanSu_DiemKyNang.Thang,4),left(NhanSu_DiemKyNang.Thang,2)"
            Else
                If tbNam.EditValue = 0 Then
                    AddParameterWhere("@Thang", tbThang.EditValue)
                    sql &= " AND Left(NhanSu_DiemKyNang.Thang,2)=@Thang"
                    colThang.Visible = True
                    sql &= " ORDER BY Right(NhanSu_DiemKyNang.Thang,4),left(NhanSu_DiemKyNang.Thang,2)"
                Else
                    AddParameterWhere("@Thang", tbThang.EditValue + "/" + tbNam.EditValue.ToString)
                    sql &= " AND NhanSu_DiemKyNang.Thang=@Thang"
                    colThang.Visible = False
                End If
            End If
            If Not cbNhomKyNang.EditValue Is Nothing Then
                AddParameterWhere("@IDKN", cbNhomKyNang.EditValue)
                sql &= " AND NhanSu_DiemKyNang.IDNhomKN=@IDKN"
            End If

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

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadTakeCare()
    End Sub


    Private Sub rtbThang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbThang.ButtonClick
        If e.Button.Index = 1 Then
            tbThang.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub rtbNam1_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbNam1.ButtonClick
        If e.Button.Index = 1 Then
            tbNam.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub rcbNhomKN_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomKN.ButtonClick
        If e.Button.Index = 1 Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
            Dim f As New frmCNNhomKyNang
            f.ShowDialog()
        ElseIf e.Button.Index = 2 Then
            cbNhomKyNang.EditValue = Nothing
        End If
    End Sub

    Private Sub mChotSoLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChotSoLieu.ItemClick
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If ShowCauHoi("Chốt điểm kỹ năng của kỹ thuật cho tháng " & tbThang.EditValue + "/" + tbNam.EditValue.ToString & " ?") Then
            ShowWaiting("Đang xử lý...")
            Dim sql As String = " "
            sql &= " SET DATEFORMAT DMY "
            sql &= " DECLARE @Thang int"
            sql &= " DECLARE @Nam int"
            sql &= " DECLARE @ThangNam nvarchar(7)"
            sql &= " DECLARE @ThangTruoc nvarchar(7)"
            sql &= " DECLARE @NgayCuoiThang datetime"

            sql &= " SET @Thang=" & Convert.ToInt32(tbThang.EditValue)
            sql &= " SET @Nam= " & tbNam.EditValue.ToString
            sql &= " SET @ThangNam='" & tbThang.EditValue & "/" & tbNam.EditValue & "' "
            sql &= " SET @ThangTruoc='" & DateAdd(DateInterval.Month, -1, New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1)).ToString("MM/yyyy") & "'"
            sql &= " SET @NgayCuoiThang='" & New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), DateTime.DaysInMonth(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue))).ToString("dd/MM/yyyy") & "'"

            sql &= " DECLARE @tblDiemKyNang AS TABLE"
            sql &= " ("
            sql &= " 	IDNhanVien INT,"
            sql &= " 	IDKyNang INT,"
            sql &= " 	IDNhomKN INT,"
            sql &= " 	Diem FLOAT DEFAULT(0),"
            sql &= "    NgayThi DateTime"
            sql &= " )"
            sql &= " INSERT INTO @tblDiemKyNang(IDNhanVien,IDKyNang,IDNhomKN,NgayThi)"
            sql &= " SELECT "
            sql &= " tbNhanVien.IdNhanVien,"
            sql &= " tblKyNang.ID as IDKyNang, tblKyNang.IdNhomKN as IDNhomKN,@NgayCuoiThang"
            sql &= " FROM "
            sql &= " ("
            sql &= " SELECT DISTINCT IDNhanVien "
            sql &= " FROM NhanSu_DiemKyNang WHERE THang=@ThangTruoc AND IDNhomKN IN (1,2) "
            sql &= " UNION ALL  "
            sql &= " SELECT DISTINCT IDNgThucHien FROM tblBaoCaoLichThiCong "
            sql &= " WHERE GiaoViec=0 AND Duyet=1 AND Month(Ngay)=@Thang AND year(Ngay)=@Nam "
            sql &= " AND IDNgThucHien NOT IN(SELECT DISTINCT IDNhanVien FROM NhanSu_DiemKyNang WHERE THang=@ThangTruoc AND IDNhomKN IN (1,2)) "
            sql &= " )tbNhanVien"
            sql &= " CROSS JOIN (SELECT ID,IDNhomKN FROM NLDANHSACH WHERE Loai = 1 AND IDNhomKN is not null)tblKyNang"
            sql &= " ORDER BY tbNhanVien.IdNhanVien"

            sql &= " UPDATE @tblDiemKyNang"
            sql &= " SET Diem = ("
            sql &= " 	ISNULL("
            sql &= " 		(SELECT TOP 1 Diem FROM tblDiemThiKyNang "
            sql &= " 		WHERE IDNhanVien = [@tblDiemKyNang].IDNhanVien"
            sql &= " 		AND IDKyNang = [@tblDiemKyNang].IDKyNang"
            sql &= " 		AND NgayThi <= [@tblDiemKyNang].NgayThi ORDER BY NgayThi DESC),"
            sql &= " 		(SELECT ROUND(Diem/2.0,2) FROM NLDanhSach WHERE ID = [@tblDiemKyNang].IDKyNang)"
            sql &= " 	)"
            sql &= " )"
            sql &= " DELETE FROM NhanSu_DiemKyNang WHERE Thang=@ThangNam"

            sql &= " INSERT INTO NhanSu_DiemKyNang(IDNhanVien,Diem,IDNhomKN,Thang) "
            sql &= " SELECT IDNhanVien,DiemKN,IDNhomKN,@ThangNam FROM"
            sql &= " ("
            sql &= " SELECT IDNhanVien,IDNhomKN,SUM(Diem) DiemKN FROM @tblDiemKyNang"
            sql &= " GROUP BY IDNhomKN,IDNhanVien)tb"

            If ExecuteSQLNonQuery(sql) Is Nothing Then
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)
            Else
                CloseWaiting()
                ShowAlert("Đã cập nhật !")
            End If
        End If
    End Sub

    Private Sub rcbThang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbThang.ButtonClick
        If e.Button.Index = 1 Then
            tbThang.EditValue = Nothing
        End If
    End Sub
End Class
