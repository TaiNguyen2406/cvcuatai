Imports BACSOFT.Db.SqlHelper
Imports DevExpress

Public Class frmPhanBoLoiNhuanNhomHT

    Private Sub frmPhanBoLoiNhuanNhomHT_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbThang.EditValue = Today.ToString("MM")
        tbNam.EditValue = Today.Year

        LoadDS()

    End Sub


    Public Sub LoadDS()
        Try
            ShowWaiting("Đang tải dữ liệu ...")

            Dim sql As String = " "
            sql &= " DECLARE @tbHSNhomTaoLN table"
            sql &= " ("
            sql &= " 	SoNV int,"
            sql &= " 	TrongSo float"
            sql &= " )"

            sql &= " DECLARE @tbHSNhomHT table"
            sql &= " ("
            sql &= " 	SoNV int,"
            sql &= " 	TrongSo float"
            sql &= " )"

            sql &= " DECLARE @tbLoiNhuanHT table"
            sql &= " ("
            sql &= " 	IDNhanVien int,"
            sql &= " 	TenNV nvarchar(250),"
            sql &= " 	BoPhan nvarchar(250),"
            sql &= " 	IDBoPhan tinyint,"
            sql &= " 	Level int,"
            sql &= " 	Class int,"
            sql &= " 	TrongSo float,"
            sql &= " 	SoNgayCong float,"
            sql &= " 	DiemDanhGia float,"
            sql &= " 	DiemHoanThanh float,"
            sql &= " 	MaBP Nvarchar(5)"
            sql &= " )"

            sql &= " DECLARE @LNHT float"
            sql &= " DECLARE @tbLN table"
            sql &= " ("
            sql &= " 	LoiNhuan float,"
            sql &= " 	IDNhanVien int"
            sql &= " )"
            sql &= " INSERT INTO @tbLN(LoiNhuan,IDNhanVien)"
            sql &= " SELECT SUM(PhuTrachHD + KhaoSatCG + XucTienHD + PhuTrachTC + ThiCong + PhatTrienSP) as LoiNhuan,IDNhanVien"
            sql &= " FROM PhieuXuatKho_LoiNhuan"
            sql &= " WHERE left(SoPhieu,4)=@ThangXK  AND IDNhanVien IN (SELECT IDNhanVien FROM LUONG "
            sql &= " INNER JOIN NhanSu_BoPhan ON LUONG.IDBoPhan=NhanSu_BoPhan.Ma AND NhanSu_BoPhan.IDNhom=1"
            sql &= " WHERE [Level]>=2 AND [Month]=@Thang)"
            sql &= " GROUP BY IDNhanVien"

            sql &= " INSERT INTO @tbHSNhomTaoLN(SoNV,TrongSo)"
            sql &= " SELECT Count( LUONG.IDNhanVien), NhanSu_BoPhan_TrongSo.TrongSo"
            sql &= " FROM LUONG "
            sql &= " INNER JOIN NhanSu_BoPhan ON LUONG.IDBoPhan=NhanSu_BoPhan.Ma AND NhanSu_BoPhan.IDNhom=1"
            sql &= " INNER JOIN NhanSu_BoPhan_TrongSo ON NhanSu_BoPhan_TrongSo.Ma=LUONG.IDBoPhan AND NhanSu_BoPhan_TrongSo.Thang=LUONG.[Month]"
            sql &= " WHERE [Level]>=2 "
            sql &= " AND [Month]=@Thang"
            sql &= " GROUP BY LUONG.IDBoPhan,NhanSu_BoPhan_TrongSo.TrongSo"

            sql &= " INSERT INTO @tbHSNhomHT(SoNV,TrongSo)"
            sql &= " SELECT Count( LUONG.IDNhanVien),NhanSu_BoPhan_TrongSo.TrongSo "
            sql &= " FROM LUONG "
            sql &= " INNER JOIN NhanSu_BoPhan ON LUONG.IDBoPhan=NhanSu_BoPhan.Ma AND NhanSu_BoPhan.IDNhom=2"
            sql &= " INNER JOIN NhanSu_BoPhan_TrongSo ON NhanSu_BoPhan_TrongSo.Ma=LUONG.IDBoPhan AND NhanSu_BoPhan_TrongSo.Thang=LUONG.[Month]"
            sql &= " AND LUONG.[Month]=@Thang"
            sql &= " GROUP BY LUONG.IDBoPhan,NhanSu_BoPhan_TrongSo.TrongSo"

            sql &= " SET @LNHT = ("
            sql &= " SELECT ((SELECT SUM([@tbLN].LoiNhuan) FROM @tbLN)/ "
            sql &= " ((SELECT SUM([@tbHSNhomTaoLN].SoNV*[@tbHSNhomTaoLN].TrongSo) FROM @tbHSNhomTaoLN ) + "
            sql &= " (SELECT SUM([@tbHSNhomHT].SoNV*[@tbHSNhomHT].TrongSo) FROM @tbHSNhomHT )))"
            sql &= " * (SELECT SUM([@tbHSNhomHT].SoNV*[@tbHSNhomHT].TrongSo) FROM @tbHSNhomHT ))"

            sql &= " INSERT INTO @tbLoiNhuanHT(IDNhanVien,TenNV ,BoPhan ,IDBoPhan ,Level ,Class ,TrongSo ,SoNgayCong ,DiemDanhGia ,DiemHoanThanh,MaBP )"
            sql &= " SELECT LUONG.IDNhanVien,NhanSu.Ten as TenNV,NHANSU_BoPhan.Ten as BoPhan,LUONG.IDBoPhan,LUONG.Level,LUONG.Class,"
            sql &= " NHANSU_BoPhan_TrongSo.TrongSo,ISNULL((tblTHChamCong.CongThuong+tblTHChamCong.CNLe1+tblTHChamCong.CNLe2),26)SoNgayCong,"
            sql &= " ISNULL(Round((tbKQ.ChuDongHTCongViec + tbKQ.ChuDongTimViecDeLam + tbKQ.LinhHoatBietViecDeLam)/3,2),"
            sql &= " ISNULL(Round((tbKQTC.ChuDongHTCongViec + tbKQTC.ChuDongTimViecDeLam + tbKQTC.LinhHoatBietViecDeLam)/3,2),0)) as DiemDanhGia,"
            sql &= " ISNULL(Round(tbKQ.MucDoHTCongViec,2),ISNULL(Round(tbKQTC.MucDoHTCongViec,2),0) ) as DiemHoanThanh,NHANSU_BoPhan.MaBP"
            sql &= " FROM LUONG"
            sql &= " INNER JOIN NHANSU ON NHANSU.ID=LUONG.IDNhanVien"
            sql &= " INNER JOIN NHANSU_BoPhan ON LUONG.IDBoPhan=NHANSU_BoPhan.Ma AND NHANSU_BoPhan.IDNhom=2"
            sql &= " INNER JOIN NHANSU_BoPhan_TrongSo ON NHANSU_BoPhan_TrongSo.Ma=LUONG.IDBoPhan "
            sql &= " AND NHANSU_BoPhan_TrongSo.Thang=@Thang"
            sql &= " LEFT JOIN tblTHChamCong ON tblTHChamCong.IDNhanVien=LUONG.IDNhanVien AND tblTHChamCong.Month=LUONG.Month"
            sql &= " LEFT JOIN"
            sql &= " ("
            sql &= " SELECT ISNULL(tb1.IDNhanVien,tb2.IDNhanVien)IDNhanVien,"
            sql &= " (ISNULL(tb1.ChuDongHTCongViec * 0.4,0) + ISNULL(tb2.ChuDongHTCongViec*0.6,0))ChuDongHTCongViec,"
            sql &= " (ISNULL(tb1.ChuDongTimViecDeLam * 0.4,0) + ISNULL(tb2.ChuDongTimViecDeLam*0.6,0))ChuDongTimViecDeLam,"
            sql &= " (ISNULL(tb1.LinhHoatBietViecDeLam * 0.4,0) + ISNULL(tb2.LinhHoatBietViecDeLam*0.6,0))LinhHoatBietViecDeLam,"
            sql &= " (ISNULL(tb1.MucDoHTCongViec * 0.4,0) + ISNULL(tb2.MucDoHTCongViec*0.6,0))MucDoHTCongViec"
            sql &= " FROM"
            sql &= " ( "
            sql &= " SELECT IDNhanVien,avg(ChuDongHTCongViec)ChuDongHTCongViec,avg(ChuDongTimViecDeLam)ChuDongTimViecDeLam,"
            sql &= "  avg(LinhHoatBietViecDeLam)LinhHoatBietViecDeLam, avg(MucDoHTCongViec)MucDoHTCongViec"
            sql &= "  FROM KTDiemDanhGiaNV"
            sql &= " WHERE NhomHoTro=1 AND CapQuanLy=0"
            sql &= " AND KTDiemDanhGiaNV.Thang=@Thang "
            sql &= "  GROUP BY IDNhanVien)tb1"
            sql &= " LEFT JOIN "
            sql &= " ("
            sql &= "  SELECT IDNhanVien,avg(ChuDongHTCongViec)ChuDongHTCongViec,avg(ChuDongTimViecDeLam)ChuDongTimViecDeLam,"
            sql &= "  avg(LinhHoatBietViecDeLam)LinhHoatBietViecDeLam, avg(MucDoHTCongViec)MucDoHTCongViec"
            sql &= "  FROM KTDiemDanhGiaNV"
            sql &= "  WHERE NhomHoTro=1 AND CapQuanLy=1  AND IDNguoiDanhGia<>IDNhanVien"
            sql &= " AND KTDiemDanhGiaNV.Thang=@Thang"
            sql &= "  GROUP BY IDNhanVien)tb2"
            sql &= " ON tb1.IDNhanVien =tb2.IDNhanVien"
            sql &= " )tbKQ ON tbKQ.IDNhanVien=LUONG.IDNhanVien"

            sql &= " LEFT JOIN"
            sql &= " ("
            sql &= " SELECT ISNULL(tb1.IDNhanVien,tb2.IDNhanVien)IDNhanVien,"
            sql &= " (ISNULL(tb1.ChuDongHTCongViec * 0.4,0) + ISNULL(tb2.ChuDongHTCongViec*0.6,0))ChuDongHTCongViec,"
            sql &= " (ISNULL(tb1.ChuDongTimViecDeLam * 0.4,0) + ISNULL(tb2.ChuDongTimViecDeLam*0.6,0))ChuDongTimViecDeLam,"
            sql &= " (ISNULL(tb1.LinhHoatBietViecDeLam * 0.4,0) + ISNULL(tb2.LinhHoatBietViecDeLam*0.6,0))LinhHoatBietViecDeLam,"
            sql &= " (ISNULL(tb1.MucDoHTCongViec * 0.4,0) + ISNULL(tb2.MucDoHTCongViec*0.6,0))MucDoHTCongViec"
            sql &= " FROM"
            sql &= " ( "
            sql &= " SELECT IDNhanVien,avg(ChuDongHTCongViec)ChuDongHTCongViec,avg(ChuDongTimViecDeLam)ChuDongTimViecDeLam,"
            sql &= "  avg(LinhHoatBietViecDeLam)LinhHoatBietViecDeLam, avg(MucDoHTCongViec)MucDoHTCongViec"
            sql &= "  FROM KTDiemDanhGiaNV"
            sql &= " WHERE NhomHoTro=1 AND CapQuanLy=0"
            sql &= " AND KTDiemDanhGiaNV.Thang=@ThangTruoc "
            sql &= "  GROUP BY IDNhanVien)tb1"
            sql &= " LEFT JOIN "
            sql &= " ("
            sql &= "  SELECT IDNhanVien,avg(ChuDongHTCongViec)ChuDongHTCongViec,avg(ChuDongTimViecDeLam)ChuDongTimViecDeLam,"
            sql &= "  avg(LinhHoatBietViecDeLam)LinhHoatBietViecDeLam, avg(MucDoHTCongViec)MucDoHTCongViec"
            sql &= "  FROM KTDiemDanhGiaNV"
            sql &= "  WHERE NhomHoTro=1 AND CapQuanLy=1  AND IDNguoiDanhGia<>IDNhanVien"
            sql &= " AND KTDiemDanhGiaNV.Thang=@ThangTruoc"
            sql &= "  GROUP BY IDNhanVien)tb2"
            sql &= " ON tb1.IDNhanVien =tb2.IDNhanVien"
            sql &= " )tbKQTC ON tbKQTC.IDNhanVien=LUONG.IDNhanVien"
            sql &= " WHERE LUONG.Month=@Thang"

            sql &= " DECLARE @MauSo float"
            sql &= " SET @MauSo=(SELECT SUM([@tbLoiNhuanHT].TrongSo * [@tbLoiNhuanHT].DiemDanhGia * [@tbLoiNhuanHT].DiemHoanThanh * [@tbLoiNhuanHT].SoNgayCong)"
            sql &= " FROM @tbLoiNhuanHT)"

            sql &= " SELECT *, (CASE WHEN @MauSo =0 THEN 0 ELSE"
            sql &= " (@LNHT/@MauSo)*([@tbLoiNhuanHT].TrongSo * [@tbLoiNhuanHT].DiemDanhGia * [@tbLoiNhuanHT].DiemHoanThanh * [@tbLoiNhuanHT].SoNgayCong) END) as LoiNhuan"
            sql &= " FROM @tbLoiNhuanHT"
            sql &= " ORDER BY [@tbLoiNhuanHT].MaBP,[@tbLoiNhuanHT].IDNhanVien"

            AddParameterWhere("@Thang", tbThang.EditValue + "/" + tbNam.EditValue.ToString)
            AddParameterWhere("@ThangXK", New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1).ToString("yyMM"))
            AddParameterWhere("@ThangTruoc", DateAdd(DateInterval.Day, -1, New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1)).ToString("MM/yyyy"))
        Dim tb As DataTable = ExecuteSQLDataTable(Sql)
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

        Private Sub rcbThang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbThang.ButtonClick
            If e.Button.Index = 1 Then
                tbThang.EditValue = Nothing
            End If
        End Sub


    Private Sub mChotSoLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChotSoLieu.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub

        Dim sql As String = ""
        sql &= " SELECT SUM(ISNULL(PhuTrachHD,0) + ISNULL(KhaoSatCG,0) + ISNULL(XucTienHD,0)"
        sql &= " + ISNULL(PhuTrachTC,0) + ISNULL(ThiCong,0) + ISNULL(PhatTrienSP,0) )"
        sql &= " LoiNhuan,IDNhanVien"
        sql &= " FROM PhieuXuatKHo_LoiNhuan"
        sql &= " WHERE Left(SoPhieu,4)=@Thang"
        sql &= " GROUP By IDNhanVien"
 
        AddParameterWhere("@Thang", New DateTime(tbNam.EditValue, tbThang.EditValue, 1).ToString("yyMM"))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If



        Try
            For i As Integer = 0 To tb.Rows.Count - 1
                AddParameter("@LoiNhuan1", tb.Rows(i)("LoiNhuan"))
                AddParameterWhere("@Month", tbThang.EditValue + "/" + tbNam.EditValue.ToString)
                AddParameterWhere("@IDNhanVien", tb.Rows(i)("IDNhanVien"))
                If doUpdate("tblTHChamCong", "Month=@Month AND IDNhanVien=@IDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Next

            For i As Integer = 0 To gdvCT.RowCount - 1
                'PTDiemTamUng
                If gdvCT.GetRowCellValue(i, "IDNhanVien") Is Nothing Then Continue For
                Dim dr() As DataRow = tb.Select("IDNhanVien=" & gdvCT.GetRowCellValue(i, "IDNhanVien"))
                If dr.Length > 0 Then
                    AddParameter("@LoiNhuan1", gdvCT.GetRowCellValue(i, "LoiNhuan") + dr(0)("LoiNhuan"))
                Else
                    AddParameter("@LoiNhuan1", gdvCT.GetRowCellValue(i, "LoiNhuan"))
                End If

                AddParameterWhere("@Month", tbThang.EditValue + "/" + tbNam.EditValue.ToString)
                AddParameterWhere("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                If doUpdate("tblTHChamCong", "Month=@Month AND IDNhanVien=@IDNhanVien") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Next
            ShowAlert("Đã cập nhật !")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub
End Class
