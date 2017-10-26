Imports BACSOFT.Db.SqlHelper

Public Class frmThuongCuoiNam

    Private Sub frmThuongCuoiNam_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuThang.EditValue = New DateTime(Today.Year, 1, 1)
        tbDenThang.EditValue = Today.Date
        LoadPhongBan()
        LoadChucVu()
        tbNam.Value = Today.Year
        tbNamXemDS.EditValue = Today.Year
    End Sub

    Public Sub LoadPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            cbPhong.Properties.DataSource = tb
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadChucVu()
        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Int32"))
        tb.Columns.Add("Ten", Type.GetType("System.String"))
        Dim r As DataRow = tb.NewRow
        r("ID") = 1
        r("Ten") = "NV"
        tb.Rows.Add(r)
        Dim r2 As DataRow = tb.NewRow
        r2("ID") = 2
        r2("Ten") = "TP"
        tb.Rows.Add(r2)
        rcbChucVu.DataSource = tb
    End Sub

    Private Sub btXem_Click(sender As System.Object, e As System.EventArgs) Handles btXem.Click
        Dim sql As String = ""
        Dim _i As Integer = DateDiff(DateInterval.Month, tbTuThang.EditValue, tbDenThang.EditValue)
        Dim _month As String = ""
        Dim _monthIn As String = ""
        sql &= " SET DATEFORMAT DMY "

        sql &= " select ROW_NUMBER() OVER(ORDER BY IDDepatment,ID) AS STT,  IDDepatment,ID,[Nhân viên]"
        For i As Integer = Convert.ToDateTime(tbTuThang.EditValue).Month To Convert.ToDateTime(tbTuThang.EditValue).Month + _i
            _month &= ",[" & DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbTuThang.EditValue).Year, i + 1, 0)).ToString("MM/yyyy") & "]"
        Next
        sql &= _month & ",convert(float,0) AS [Trung bình], convert(float,0) AS [% TB]"

        sql &= " FROM"
        sql &= " (SELECT NHANSU.IDDepatment,NHANSU.ID,NHANSU.Ten AS N'Nhân viên',round(Diem1,0)Diem1,[Month] FROM tblTHChamCong "
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=tblTHChamCong.IDNhanVien AND NHANSU.Noictac=74"
        sql &= " WHERE [Month] IN ("
        _i = DateDiff(DateInterval.Month, tbTuThang.EditValue, tbDenThang.EditValue)

        For i As Integer = Convert.ToDateTime(tbTuThang.EditValue).Month To Convert.ToDateTime(tbTuThang.EditValue).Month + _i
            _monthIn &= ",'" & DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbTuThang.EditValue).Year, i + 1, 0)).ToString("MM/yyyy") & "'"
        Next
        sql &= _monthIn.Substring(1, _monthIn.Length - 1) & ")"

        sql &= " )p"
        sql &= " PIVOT"
        sql &= " ("
        sql &= " 	SUM(Diem1)"
        sql &= " 	FOR [Month] IN ("
        sql &= _month.Substring(1, _month.Length - 1)
        sql &= " )"
        sql &= " ) AS PVT"
        sql &= " ORDER BY IDDepatment,ID"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvCT.BeginUpdate()
            gdvCT.Columns.Clear()
            gdv.DataSource = dt
            gdvCT.Columns("IDDepatment").Visible = False
            gdvCT.Columns("ID").Visible = False

            gdvCT.Columns("STT").Width = 40
            gdvCT.Columns("STT").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("Nhân viên").Width = 150
            gdvCT.Columns("Nhân viên").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            For j As Integer = 0 To gdvCT.Columns.Count - 1
                If gdvCT.Columns(j).FieldName <> "STT" And gdvCT.Columns(j).FieldName <> "Nhân viên" And gdvCT.Columns(j).FieldName <> "% TB" Then
                    gdvCT.Columns(j).ColumnEdit = tbN2
                End If
            Next
            Dim _SoThang As Integer = 0
            Dim _TongDiem As Double = 0
            For i As Integer = 0 To gdvCT.RowCount - 1
                _SoThang = 0
                _TongDiem = 0
                For j As Integer = 0 To gdvCT.Columns.Count - 1
                    If gdvCT.Columns(j).FieldName <> "STT" And gdvCT.Columns(j).FieldName <> "Nhân viên" And gdvCT.Columns(j).FieldName <> "% TB" And gdvCT.Columns(j).FieldName <> "Trung bình" And gdvCT.Columns(j).FieldName <> "ID" And gdvCT.Columns(j).FieldName <> "IDDepatment" Then
                        If Not IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName)) Then
                            If gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName) > 0 Then
                                _TongDiem += gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName)
                                _SoThang += 1
                            End If
                        End If
                    End If
                Next
                If _TongDiem = 0 Then
                    gdvCT.SetRowCellValue(i, "Trung bình", 0)
                Else
                    gdvCT.SetRowCellValue(i, "Trung bình", Math.Round(_TongDiem / _SoThang))
                End If

                If _TongDiem = 0 Then
                    gdvCT.SetRowCellValue(i, "% TB", 0)
                Else
                    gdvCT.SetRowCellValue(i, "% TB", Math.Round(((_TongDiem / _SoThang) / 4000) * 100, 2))
                End If

            Next

            gdvCT.EndUpdate()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As System.EventArgs) Handles btXuatExcel.Click
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "Diem NV " & Convert.ToDateTime(tbTuThang.EditValue).ToString("MM-yyyy") & "  " & Convert.ToDateTime(tbDenThang.EditValue).ToString("MM-yyyy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)
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


    Private Sub btXemBangTinhLuong_Click(sender As System.Object, e As System.EventArgs) Handles btXemBangTinhLuong.Click
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " DECLARE @TongDiem AS Float"
        sql &= " SET @TongDiem=(SELECT SUM(DiemTB) FROM tblBangTinhThuong WHERE Nam=@Nam AND CoThuongDiem=1)"
        sql &= " DECLARE @QuyThuongDiem As float"
        sql &= " SET @QuyThuongDiem=(SELECT QuyThuongDiem FROM tblHeSoThuongNam WHERE Nam=@Nam)"

       

        sql &= " SELECT *,(ISNULL(Thuong30415,0)+ISNULL(Thuong29,0)+ISNULL(ThuongT1,0)+ISNULL(ThuongT2,0)+ISNULL(ThuongT3,0)+ISNULL(ThuongT4,0)+ISNULL(ThuongT5,0)+ISNULL(ThuongT6,0)+ISNULL(ThuongT7,0)+ISNULL(ThuongT8,0)+ISNULL(ThuongT9,0)+ISNULL(ThuongT10,0)+ISNULL(ThuongT11,0)+ISNULL(ThuongT12,0)+ "
        sql &= " 			ISNULL(TongThuongTheoDiem,0) + (Case WHEN ISNULL(ThuongTP,0)> ISNULL(ThuongDoanhThu,0) Then ISNULL(ThuongTP,0) ELSE ISNULL(ThuongDoanhThu,0) END) )TongThuongKD,"
        sql &= " 		(ISNULL(Thuong30415,0)+ISNULL(Thuong29,0)+ISNULL(ThuongT1,0)+ISNULL(ThuongT2,0)+ISNULL(ThuongT3,0)+ISNULL(ThuongT4,0)+ISNULL(ThuongT5,0)+ISNULL(ThuongT6,0)+ISNULL(ThuongT7,0)+ISNULL(ThuongT8,0)+ISNULL(ThuongT9,0)+ISNULL(ThuongT10,0)+ISNULL(ThuongT11,0)+ISNULL(ThuongT12,0)+ "
        sql &= " 		ISNULL(TongThuongTheoDiem,0) + (Case WHEN ISNULL(ThuongTP,0)> ISNULL(ThuongDoanhThu,0) Then ISNULL(ThuongTP,0) ELSE ISNULL(ThuongDoanhThu,0) END) + ISNULL(LuongT13,0) + ISNULL(ThuongDiemT13,0) )TongThuong,"
        sql &= " 		(ISNULL(Thuong30415,0)+ISNULL(Thuong29,0)+ISNULL(ThuongT1,0)+ISNULL(ThuongT2,0)+ISNULL(ThuongT3,0)+ISNULL(ThuongT4,0)+ISNULL(ThuongT5,0)+ISNULL(ThuongT6,0)+ISNULL(ThuongT7,0)+ISNULL(ThuongT8,0)+ISNULL(ThuongT9,0)+ISNULL(ThuongT10,0)+ISNULL(ThuongT11,0)+ISNULL(ThuongT12,0)+ "
        sql &= " 			ISNULL(TongThuongTheoDiem,0)  )TongDaLinh,"
        sql &= " 			((ISNULL(Thuong30415,0)+ISNULL(Thuong29,0)+ISNULL(ThuongT1,0)+ISNULL(ThuongT2,0)+ISNULL(ThuongT3,0)+ISNULL(ThuongT4,0)+ISNULL(ThuongT5,0)+ISNULL(ThuongT6,0)+ISNULL(ThuongT7,0)+ISNULL(ThuongT8,0)+ISNULL(ThuongT9,0)+ISNULL(ThuongT10,0)+ISNULL(ThuongT11,0)+ISNULL(ThuongT12,0)+ "
        sql &= " 		ISNULL(TongThuongTheoDiem,0) + (Case WHEN ISNULL(ThuongTP,0)> ISNULL(ThuongDoanhThu,0) Then ISNULL(ThuongTP,0) ELSE ISNULL(ThuongDoanhThu,0) END) + ISNULL(LuongT13,0) + ISNULL(ThuongDiemT13,0) ) -"
        sql &= " 		(ISNULL(Thuong30415,0)+ISNULL(Thuong29,0)+ISNULL(ThuongT1,0)+ISNULL(ThuongT2,0)+ISNULL(ThuongT3,0)+ISNULL(ThuongT4,0)+ISNULL(ThuongT5,0)+ISNULL(ThuongT6,0)+ISNULL(ThuongT7,0)+ISNULL(ThuongT8,0)+ISNULL(ThuongT9,0)+ISNULL(ThuongT10,0)+ISNULL(ThuongT11,0)+ISNULL(ThuongT12,0)+ "
        sql &= " 			ISNULL(TongThuongTheoDiem,0)  ))TongConLinh"

        sql &= " FROM"
        sql &= " ("
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY NHANSU.IDDepatment, NHANSU.Id ) AS STT, tblBangTinhThuong.ID,NHANSU.Ten AS HoTen,tblBangTinhThuong.IdNhanVien,tblBangTinhThuong.ChucVu,tblBangTinhThuong.LuongCB,tblBangTinhThuong.ThangLV,"
        sql &= " 	tblBangTinhThuong.LuongT1,tblBangTinhThuong.LuongT2,tblBangTinhThuong.LuongT3,tblBangTinhThuong.LuongT4,"
        sql &= " 	tblBangTinhThuong.LuongT5,tblBangTinhThuong.LuongT6,tblBangTinhThuong.LuongT7,tblBangTinhThuong.LuongT8,"
        sql &= " 	tblBangTinhThuong.LuongT9,tblBangTinhThuong.LuongT10,tblBangTinhThuong.LuongT11,tblBangTinhThuong.LuongT12,"
        sql &= " 	(ISNULL(tblBangTinhThuong.LuongT1,0)+ISNULL(tblBangTinhThuong.LuongT2,0)+ISNULL(tblBangTinhThuong.LuongT3,0)+ISNULL(tblBangTinhThuong.LuongT4,0)+"
        sql &= " 	ISNULL(tblBangTinhThuong.LuongT5,0)+ISNULL(tblBangTinhThuong.LuongT6,0)+ISNULL(tblBangTinhThuong.LuongT7,0)+ISNULL(tblBangTinhThuong.LuongT8,0)+"
        sql &= " 	ISNULL(tblBangTinhThuong.LuongT9,0)+ISNULL(tblBangTinhThuong.LuongT10,0)+ISNULL(tblBangTinhThuong.LuongT11,0)+ISNULL(tblBangTinhThuong.LuongT12,0))TongLuongNam,"
        sql &= " 	tblBangTinhThuong.Thuong30415,tblBangTinhThuong.Thuong29,tblBangTinhThuong.ThuongT1,tblBangTinhThuong.ThuongT2,tblBangTinhThuong.ThuongT3,tblBangTinhThuong.ThuongT4,"
        sql &= " 	tblBangTinhThuong.ThuongT5,tblBangTinhThuong.ThuongT6,tblBangTinhThuong.ThuongT7,tblBangTinhThuong.ThuongT8,"
        sql &= " 	tblBangTinhThuong.ThuongT9,tblBangTinhThuong.ThuongT10,tblBangTinhThuong.ThuongT11,tblBangTinhThuong.ThuongT12,"
        sql &= " 	((ISNULL(tblBangTinhThuong.ThuongT1,0)+ISNULL(tblBangTinhThuong.ThuongT2,0)+ISNULL(tblBangTinhThuong.ThuongT3,0)+ISNULL(tblBangTinhThuong.ThuongT4,0)+"
        sql &= " 	ISNULL(tblBangTinhThuong.ThuongT5,0)+ISNULL(tblBangTinhThuong.ThuongT6,0)+ISNULL(tblBangTinhThuong.ThuongT7,0)+ISNULL(tblBangTinhThuong.ThuongT8,0)+"
        sql &= " 	ISNULL(tblBangTinhThuong.ThuongT9,0)+ISNULL(tblBangTinhThuong.ThuongT10,0)+ISNULL(tblBangTinhThuong.ThuongT11,0)+ISNULL(tblBangTinhThuong.ThuongT12,0)) * ISNULL((SELECT HSThuongDiemKD FROM tblHeSoThuongNam WHERE Nam=@Nam),1)) AS TongThuongTheoDiem,"
        sql &= "    NHANSU.IDDepatment,tblBangTinhThuong.ThuongDoanhThu,tblBangTinhThuong.ThuongTP,"
        sql &= "    (CASE CoLuongT13 WHEN 1 THEN round((LuongCB/12) * ThangLV,0) ELSE 0 END) AS LuongT13,"
        sql &= "    (CASE CoThuongDiem WHEN 1 THEN round((DiemTB/@TongDiem)*@QuyThuongDiem,0) ELSE 0 END) AS ThuongDiemT13,"
        sql &= "    CoLuongT13,CoThuongDiem,"
        sql &= "    Convert(bit,0)Modify"
        sql &= " FROM tblBangTinhThuong"
        sql &= " INNER JOIN NHANSU ON NHANSU.Id=tblBangTinhThuong.IDNhanVien"
        sql &= " WHERE Nam=@Nam )tb1"
        sql &= " ORDER BY IDDepatment,Id"
        AddParameter("@Nam", tbNam.Value)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvBangTinhThuong.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvBangTinhThuongCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvBangTinhThuongCT.CellValueChanged
        If e.Column.FieldName <> "Modify" Then
            gdvBangTinhThuongCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub btLuuBangTinhThuong_Click(sender As System.Object, e As System.EventArgs) Handles btLuuBangTinhThuong.Click
        gdvBangTinhThuongCT.CloseEditor()
        gdvBangTinhThuongCT.UpdateCurrentRow()
        Dim count = 0
        For i As Integer = 0 To gdvBangTinhThuongCT.RowCount - 1
            If gdvBangTinhThuongCT.GetRowCellValue(i, "Modify") Then
                'Thông tin cơ bản
                AddParameter("@ChucVu", gdvBangTinhThuongCT.GetRowCellValue(i, "ChucVu"))
                AddParameter("@LuongCB", gdvBangTinhThuongCT.GetRowCellValue(i, "LuongCB"))
                AddParameter("@ThangLV", gdvBangTinhThuongCT.GetRowCellValue(i, "ThangLV"))
                'Thông tin thưởng
                AddParameter("@ThuongT1", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT1"))
                AddParameter("@ThuongT2", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT2"))
                AddParameter("@ThuongT3", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT3"))
                AddParameter("@ThuongT4", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT4"))
                AddParameter("@ThuongT5", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT5"))
                AddParameter("@ThuongT6", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT6"))
                AddParameter("@ThuongT7", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT7"))
                AddParameter("@ThuongT8", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT8"))
                AddParameter("@ThuongT9", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT9"))
                AddParameter("@ThuongT10", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT10"))
                AddParameter("@ThuongT11", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT11"))
                AddParameter("@ThuongT12", gdvBangTinhThuongCT.GetRowCellValue(i, "ThuongT12"))
                AddParameter("@Thuong30415", gdvBangTinhThuongCT.GetRowCellValue(i, "Thuong30415"))
                AddParameter("@Thuong29", gdvBangTinhThuongCT.GetRowCellValue(i, "Thuong29"))
                AddParameter("@CoLuongT13", gdvBangTinhThuongCT.GetRowCellValue(i, "CoLuongT13"))
                AddParameter("@CoThuongDiem", gdvBangTinhThuongCT.GetRowCellValue(i, "CoThuongDiem"))

                AddParameterWhere("@Id", gdvBangTinhThuongCT.GetRowCellValue(i, "ID"))
                If doUpdate("tblBangTinhThuong", "ID=@Id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    count += 1
                End If
            End If
        Next
        If count > 0 Then
            ShowAlert("Đã lưu !")
        End If
    End Sub

    Private Sub btCapNhatHeSo_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatHeSo.Click
        Dim f As New frmCNHeSoThuong
        f.ShowDialog()
    End Sub

    Private Sub btXuatBangTinhThuong_Click(sender As System.Object, e As System.EventArgs) Handles btXuatBangTinhThuong.Click
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "Bang tinh thuong tet NV nam " & tbNam.EditValue.ToString & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvBangTinhThuongCT, False)
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

    Private Sub btXemHeSoThuongTruongPhong_Click(sender As System.Object, e As System.EventArgs) Handles btXemHeSoThuongTruongPhong.Click
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"
        sql &= " INSERT INTO tblDMThuongNam(IDPhong,Nam) "
        sql &= " SELECT ID,@Nam FROM DEPATMENT WHERE ID NOT IN (SELECT IDPhong FROM tblDMThuongNam WHERE Nam=@Nam)"

        sql &= " SELECT *,(SELECT Ten FROM DEPATMENT WHERE ID=tblDMThuongNam.IDPhong)Phong FROM tblDMThuongNam WHERE Nam=@Nam ORDER BY IDPhong"
        'sql &= " SELECT  *"
        'sql &= " FROM"
        'sql &= " ("
        'sql &= " Select SUM(XUATKHO.Soluong * XUATKHO.DonGia) AS DoanhSo,NHANSU.ID AS IDNhanVien, NHANSU.Ten AS HoTen,N'Doanh số Omron' AS Nhom "
        'sql &= " FROM XUATKHO"
        'sql &= " INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
        'sql &= " INNER JOIN"
        'sql &= " (SELECT DISTINCT "
        'sql &= " SoPhieuXK FROM"
        'sql &= " ("
        'sql &= " Select DISTINCT"
        'sql &= "    PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
        'sql &= "     (CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        'sql &= " 		WHEN 0 THEN 0 "
        'sql &= " 		ELSE "
        'sql &= " 			(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        'sql &= " 				- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"
        'sql &= " FROM PHIEUXUATKHO "
        'sql &= " INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
        'sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        'sql &= " LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        'sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        'sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
        'sql &= " where year(PHIEUXUATKHO.NgayThang)=@Nam"
        'sql &= " )tb where PTLN>=5)tbDonHang ON tbDonHang.SoPhieuXK=PHIEUXUATKHO.SoPhieu"

        'sql &= " INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID"
        'sql &= " WHERE XUATKHO.IDVatTu IN (SELECT ID FROM VATTU WHERE IDHangSanXuat=15) AND year(PHIEUXUATKHO.NgayThang)=@Nam"
        'sql &= "     AND PHIEUXUATKHO.IDKhachHang <> 74"
        'sql &= " GROUP By NHANSU.Ten,NHANSU.ID"
        'sql &= " )tb1"
        'sql &= " UNION ALL"
        'sql &= " SELECT * FROM"
        'sql &= " ("
        'sql &= " Select SUM(XUATKHO.Soluong * XUATKHO.DonGia) AS DoanhSo,NHANSU.ID AS IDNhanVien, NHANSU.Ten AS HoTen,N'Doanh số SMC' AS Nhom  "
        'sql &= " FROM XUATKHO"
        'sql &= " INNER  JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
        'sql &= " INNER JOIN"
        'sql &= " (SELECT DISTINCT "
        'sql &= " SoPhieuXK FROM"
        'sql &= " ("
        'sql &= " Select DISTINCT"
        'sql &= "    PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
        'sql &= "     (CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        'sql &= " 		WHEN 0 THEN 0 "
        'sql &= " 		ELSE "
        'sql &= " 			(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        'sql &= " 				- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"
        'sql &= " FROM PHIEUXUATKHO "
        'sql &= " INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
        'sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        'sql &= " LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        'sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        'sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu"
        'sql &= " where year(PHIEUXUATKHO.NgayThang)=@Nam"
        'sql &= "  )tb where PTLN>=5)tbDonHang ON tbDonHang.SoPhieuXK=PHIEUXUATKHO.SoPhieu"
        'sql &= " INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID"
        'sql &= " WHERE XUATKHO.IDVatTu IN (SELECT ID FROM VATTU WHERE IDHangSanXuat=265) AND year(PHIEUXUATKHO.NgayThang)=@Nam"
        'sql &= "     AND PHIEUXUATKHO.IDKhachHang <> 74"
        'sql &= " GROUP By NHANSU.Ten,NHANSU.ID)tb2"
        'sql &= " ORDER BY DoanhSo"
        AddParameterWhere("@Nam", tbNamXemDS.Value)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvDMThuongNam.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)

        End If

    End Sub

    Private Sub btXuatDS_Click(sender As System.Object, e As System.EventArgs) Handles btXuatDS.Click
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "He so thuong nam " & tbNamXemDS.EditValue.ToString & " cua truong phong.xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvDMThuongNamCT, False)
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


    Private Sub btCapNhatDiemTB_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatDiemTB.Click
        If ShowCauHoi("Cập nhật điểm trung bình của NV sang bảng tính thưởng ?") Then
            Dim count = 0
            Dim countE = 0
            For i As Integer = 0 To gdvCT.DataRowCount - 1
                AddParameter("@DiemTB", gdvCT.GetRowCellValue(i, "Trung bình"))
                AddParameterWhere("@IdNV", gdvCT.GetRowCellValue(i, "ID"))
                AddParameterWhere("@Nam", Convert.ToDateTime(tbDenThang.EditValue).Year)
                If doUpdate("tblBangTinhThuong", "IdNhanVien=@IdNv AND Nam=@Nam") Is Nothing Then
                    countE += 1
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    count += 1
                End If
            Next
            ShowThongBao("Đã cập nhật: " & count.ToString & ", lỗi: " & countE.ToString)
        End If
    End Sub

    Private Sub btTinhDoanhSo_Click(sender As System.Object, e As System.EventArgs) Handles btTinhDoanhSo.Click
        Application.DoEvents()
        ShowWaiting("Đang cập nhật doanh số phòng Omron ...")
        Dim sql As String = ""
        sql &= " if not EXISTS(SELECT ISNULL(DSPhong,0) FROM tblDMThuongNam WHERE IDPhong=4 AND Nam=@Nam)"
        sql &= " BEGIN"
        sql &= "    INSERT INTO tblDMThuongNam(Nam,IDPhong) VALUES(@Nam,4)"
        sql &= " END"

        sql &= " 	UPDATE tblDMThuongNam SET DSPhong="
        sql &= " 		(Select SUM(XUATKHO.Soluong * (XUATKHO.DonGia-ISNULL(CHAOGIA.ChietKhau,0))) AS DoanhSoOmronSMC "
        sql &= " 		FROM XUATKHO"
        sql &= " 		INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
        sql &= "        LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
        sql &= " 		INNER JOIN"
        sql &= " 		(SELECT DISTINCT "
        sql &= " 		SoPhieuXK FROM"
        sql &= " 		("
        sql &= " 		Select DISTINCT"
        sql &= " 		   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
        sql &= " 			(CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        sql &= " 				WHEN 0 THEN 0 "
        sql &= " 				ELSE "
        sql &= " 					(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= " 						- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"
        sql &= " 		FROM PHIEUXUATKHO "
        sql &= " 		INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
        sql &= " 		LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " 		LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
        sql &= " 		where year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 		)tb where PTLN>=5)tbDonHang ON tbDonHang.SoPhieuXK=PHIEUXUATKHO.SoPhieu"
        sql &= " 		INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID AND NHANSU.IDDepatment=4"
        sql &= " 		WHERE XUATKHO.IDVatTu IN (SELECT ID FROM VATTU WHERE IDHangSanXuat= 15 OR IDHangSanXuat= 265) AND year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 			AND PHIEUXUATKHO.IDKhachHang <> 74)"
        sql &= " 		WHERE IDPhong=4 AND Nam=@Nam"

        AddParameterWhere("@Nam", tbNamXemDS.EditValue)
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CloseWaiting()
        End If

        Application.DoEvents()

        System.Threading.Thread.Sleep(1000)
        ShowWaiting("Đang cập nhật doanh số phòng khác tạo cho phòng Omron ...")
        sql = ""

        sql &= " 	    UPDATE tblDMThuongNam SET DSTuPhongKhac="
        sql &= " 		(Select SUM(XUATKHO.Soluong * (XUATKHO.DonGia-ISNULL(CHAOGIA.ChietKhau,0))) AS DoanhSoOmronSMC "
        sql &= " 		FROM XUATKHO"
        sql &= " 		INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
        sql &= "        LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
        sql &= " 		INNER JOIN"
        sql &= " 		(SELECT DISTINCT "
        sql &= " 		SoPhieuXK FROM"
        sql &= " 		("
        sql &= " 		Select DISTINCT"
        sql &= " 		   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
        sql &= " 			(CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        sql &= " 				WHEN 0 THEN 0 "
        sql &= " 				ELSE "
        sql &= " 					(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= " 						- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"
        sql &= " 		FROM PHIEUXUATKHO "
        sql &= " 		INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
        sql &= " 		LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " 		LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
        sql &= " 		where year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 		)tb where PTLN>=5)tbDonHang ON tbDonHang.SoPhieuXK=PHIEUXUATKHO.SoPhieu"
        sql &= " 		INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID AND NHANSU.IDDepatment<>4"
        sql &= " 		WHERE XUATKHO.IDVatTu IN (SELECT ID FROM VATTU WHERE IDHangSanXuat= 15 OR IDHangSanXuat= 265) AND year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 			AND PHIEUXUATKHO.IDKhachHang <> 74)"
        sql &= " 		WHERE IDPhong=4 AND Nam=@Nam"

        AddParameterWhere("@Nam", tbNamXemDS.EditValue)
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CloseWaiting()
        End If

        Application.DoEvents()

        System.Threading.Thread.Sleep(1000)
        ShowWaiting("Đang cập nhật doanh số phòng Omron tạo ra cho phòng khác ...")
        sql = ""

        sql &= " 	UPDATE tblDMThuongNam SET DSChoPhongKhac="
        sql &= " 		(Select SUM(XUATKHO.Soluong * (XUATKHO.DonGia-ISNULL(CHAOGIA.ChietKhau,0))) AS DoanhSoOmronSMC "
        sql &= " 		FROM XUATKHO"
        sql &= " 		INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
        sql &= "        LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
        sql &= " 		INNER JOIN"
        sql &= " 		(SELECT DISTINCT "
        sql &= " 		SoPhieuXK FROM"
        sql &= " 		("
        sql &= " 		Select DISTINCT"
        sql &= " 		   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
        sql &= " 			(CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        sql &= " 				WHEN 0 THEN 0 "
        sql &= " 				ELSE "
        sql &= " 					(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= " 						- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"
        sql &= " 		FROM PHIEUXUATKHO "
        sql &= " 		INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
        sql &= " 		LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " 		LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
        sql &= " 		where year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 		)tb where PTLN>=5)tbDonHang ON tbDonHang.SoPhieuXK=PHIEUXUATKHO.SoPhieu"
        sql &= " 		INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID AND NHANSU.IDDepatment=4"
        sql &= " 		WHERE XUATKHO.IDVatTu IN (SELECT ID FROM VATTU WHERE IDHangSanXuat<> 15 AND IDHangSanXuat<> 265) AND year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 			AND PHIEUXUATKHO.IDKhachHang <> 74)"
        sql &= " 		WHERE IDPhong=4 AND Nam=@Nam"

        AddParameterWhere("@Nam", tbNamXemDS.EditValue)
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CloseWaiting()
        End If

        Application.DoEvents()

        System.Threading.Thread.Sleep(1000)

        ShowWaiting("Đang cập nhật doanh số phòng tổng hợp ...")

        sql &= ""

        sql &= " if not EXISTS(SELECT ISNULL(DSPhong,0) FROM tblDMThuongNam WHERE IDPhong=3 AND Nam=@Nam)"
        sql &= " BEGIN"
        sql &= "    INSERT INTO tblDMThuongNam(Nam,IDPhong) VALUES(@Nam,3)"
        sql &= " END"

        sql &= " 	UPDATE tblDMThuongNam SET DSPhong="
        sql &= " 		(Select SUM(XUATKHO.Soluong * (XUATKHO.DonGia-ISNULL(CHAOGIA.ChietKhau,0))) AS DoanhSoTongHop "
        sql &= " 		FROM XUATKHO"
        sql &= " 		INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
        sql &= "        LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
        sql &= " 		INNER JOIN"
        sql &= " 		(SELECT DISTINCT "
        sql &= " 		SoPhieuXK FROM"
        sql &= " 		("
        sql &= " 		Select DISTINCT"
        sql &= " 		   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
        sql &= " 			(CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        sql &= " 				WHEN 0 THEN 0 "
        sql &= " 				ELSE "
        sql &= " 					(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= " 						- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"
        sql &= " 		FROM PHIEUXUATKHO "
        sql &= " 		INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
        sql &= " 		LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " 		LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
        sql &= " 		where year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 		)tb where PTLN>=5)tbDonHang ON tbDonHang.SoPhieuXK=PHIEUXUATKHO.SoPhieu"
        sql &= " 		INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID AND NHANSU.IDDepatment=3"
        sql &= " 		WHERE XUATKHO.IDVatTu IN (SELECT ID FROM VATTU WHERE IDHangSanXuat<> 15 AND IDHangSanXuat<> 265) AND year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 			AND PHIEUXUATKHO.IDKhachHang <> 74)"
        sql &= " 		WHERE IDPhong=3 AND Nam=@Nam"

        AddParameterWhere("@Nam", tbNamXemDS.EditValue)
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CloseWaiting()
        End If

        Application.DoEvents()

        System.Threading.Thread.Sleep(1000)
        ShowWaiting("Đang cập nhật doanh số phòng Omron tạo cho phòng tổng hợp ...")
        sql = ""

        sql &= " 	    UPDATE tblDMThuongNam SET DSTuPhongKhac="
        sql &= " 		(Select SUM(XUATKHO.Soluong * (XUATKHO.DonGia-ISNULL(CHAOGIA.ChietKhau,0))) AS DoanhSoTonghop "
        sql &= " 		FROM XUATKHO"
        sql &= " 		INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
        sql &= "        LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
        sql &= " 		INNER JOIN"
        sql &= " 		(SELECT DISTINCT "
        sql &= " 		SoPhieuXK FROM"
        sql &= " 		("
        sql &= " 		Select DISTINCT"
        sql &= " 		   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
        sql &= " 			(CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        sql &= " 				WHEN 0 THEN 0 "
        sql &= " 				ELSE "
        sql &= " 					(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= " 						- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"
        sql &= " 		FROM PHIEUXUATKHO "
        sql &= " 		INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
        sql &= " 		LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " 		LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
        sql &= " 		where year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 		)tb where PTLN>=5)tbDonHang ON tbDonHang.SoPhieuXK=PHIEUXUATKHO.SoPhieu"
        sql &= " 		INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID AND NHANSU.IDDepatment<>3"
        sql &= " 		WHERE XUATKHO.IDVatTu IN (SELECT ID FROM VATTU WHERE IDHangSanXuat<> 15 AND IDHangSanXuat<> 265) AND year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 			AND PHIEUXUATKHO.IDKhachHang <> 74)"
        sql &= " 		WHERE IDPhong=3 AND Nam=@Nam"

        AddParameterWhere("@Nam", tbNamXemDS.EditValue)
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CloseWaiting()
        End If

        Application.DoEvents()

        System.Threading.Thread.Sleep(1000)
        ShowWaiting("Đang cập nhật doanh số phòng tổng hợp tạo cho phòng Omron ...")
        sql = ""

        sql &= " 	UPDATE tblDMThuongNam SET DSChoPhongKhac="
        sql &= " 		(Select SUM(XUATKHO.Soluong * (XUATKHO.DonGia-ISNULL(CHAOGIA.ChietKhau,0))) AS DoanhSo"
        sql &= " 		FROM XUATKHO"
        sql &= " 		INNER JOIN PHIEUXUATKHO ON XUATKHO.SoPhieu=PHIEUXUATKHO.SoPhieu"
        sql &= "        LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
        sql &= " 		INNER JOIN"
        sql &= " 		(SELECT DISTINCT "
        sql &= " 		SoPhieuXK FROM"
        sql &= " 		("
        sql &= " 		Select DISTINCT"
        sql &= " 		   PHIEUXUATKHO.SoPhieu AS SoPhieuXK,"
        sql &= " 			(CASE PHIEUXUATKHO.Tientruocthue* PHIEUXUATKHO.Tygia "
        sql &= " 				WHEN 0 THEN 0 "
        sql &= " 				ELSE "
        sql &= " 					(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(V_XuatkhoGianhap.tongnhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= " 						- ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15))) /(PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia)*100 END)  AS PTLN"
        sql &= " 		FROM PHIEUXUATKHO "
        sql &= " 		INNER JOIN NHANSU ON NHANSU.ID=PHIEUXUATKHO.IDTakecare"
        sql &= " 		LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
        sql &= " 		LEFT JOIN V_XuatkhoGianhap ON PHIEUXUATKHO.Sophieu = V_XuatkhoGianhap.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
        sql &= " 		LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
        sql &= " 		where year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 		)tb where PTLN>=5)tbDonHang ON tbDonHang.SoPhieuXK=PHIEUXUATKHO.SoPhieu"
        sql &= " 		INNER JOIN NHANSU ON PHIEUXUATKHO.IDTakecare=NHANSU.ID AND NHANSU.IDDepatment=3"
        sql &= " 		WHERE XUATKHO.IDVatTu IN (SELECT ID FROM VATTU WHERE IDHangSanXuat= 15 OR IDHangSanXuat= 265) AND year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " 			AND PHIEUXUATKHO.IDKhachHang <> 74)"
        sql &= " 		WHERE IDPhong=3 AND Nam=@Nam"


        AddParameterWhere("@Nam", tbNamXemDS.EditValue)
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CloseWaiting()
            ShowThongBao("Đã xong !")
            btXem.PerformClick()
        End If
    End Sub

    Private Sub btCapNhatThuongTPKD_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatThuongTPKD.Click
        Dim count = 0
        For i As Integer = 0 To gdvDMThuongNamCT.DataRowCount - 1
            If gdvDMThuongNamCT.GetRowCellValue(i, "IDPhong") = 3 Then
                AddParameter("@ThuongTP", gdvDMThuongNamCT.GetRowCellValue(i, "DSPhong") + gdvDMThuongNamCT.GetRowCellValue(i, "DSTuPhongKhac"))
                AddParameterWhere("@IDNhanVien", 602)
                AddParameterWhere("@Nam", tbNamXemDS.EditValue)
                If doUpdate("tblBangTinhThuong", "IDNhanVien=@IDNhanVien AND Nam=@Nam") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    count += 1
                End If
            ElseIf gdvDMThuongNamCT.GetRowCellValue(i, "IDPhong") = 4 Then
                AddParameter("@ThuongTP", gdvDMThuongNamCT.GetRowCellValue(i, "DSPhong") + gdvDMThuongNamCT.GetRowCellValue(i, "DSTuPhongKhac"))
                AddParameterWhere("@IDNhanVien", 971)
                AddParameterWhere("@Nam", tbNamXemDS.EditValue)
                If doUpdate("tblBangTinhThuong", "IDNhanVien=@IDNhanVien AND Nam=@Nam") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    count += 1
                End If
            End If
        Next
        If count > 0 Then
            ShowThongBao("Đã cập nhật !")
        End If

    End Sub

    Private Sub btLuuHeSoTinhThuong_Click(sender As System.Object, e As System.EventArgs) Handles btLuuHeSoTinhThuong.Click
        If ShowCauHoi("Cập nhật hệ số tính thưởng trưởng phòng ?") Then
            Dim countE = 0
            For i As Integer = 0 To gdvDMThuongNamCT.DataRowCount - 1
                AddParameter("@PTDiemQL", gdvDMThuongNamCT.GetRowCellValue(i, "PTDiemQL"))
                AddParameter("@HSThuongTP", gdvDMThuongNamCT.GetRowCellValue(i, "HSThuongTP"))
                AddParameter("@DMThuong", gdvDMThuongNamCT.GetRowCellValue(i, "DMThuong"))
                AddParameter("@DMDoanhSo", gdvDMThuongNamCT.GetRowCellValue(i, "DMDoanhSo"))
                AddParameter("@DMLoiNhuan", gdvDMThuongNamCT.GetRowCellValue(i, "DMLoiNhuan"))
                AddParameterWhere("@IDPhong", gdvDMThuongNamCT.GetRowCellValue(i, "IDPhong"))
                AddParameterWhere("@Nam", tbNamXemDS.EditValue)
                If doUpdate("tblDMThuongNam", "IDPhong=@IDPhong AND Nam=@Nam") Is Nothing Then
                    countE += 1
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            Next
            If countE = 0 Then
                ShowAlert("Đã cập nhật!")
            End If
        End If
    End Sub
End Class
