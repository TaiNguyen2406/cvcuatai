Imports SpreadsheetGear
Imports BACSOFT.Db.SqlHelper

Public Class frmBangExeclInovice

    Dim workbook As IWorkbook

    Private Sub frmBangKeHoaDonGTGT_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim tg As DateTime = GetServerTime()
        txtNam.EditValue = tg.Year

        Select Case tg.Month
            Case 1, 2, 3
                cmbQuy.EditValue = "I"
            Case 4, 5, 6
                cmbQuy.EditValue = "II"
            Case 7, 8, 9
                cmbQuy.EditValue = "III"
            Case 10, 11, 12
                cmbQuy.EditValue = "IV"
        End Select

        Select Case Me.Tag
            Case "BangKeHoaDonBanRa"
                workbook = Factory.GetWorkbook(".\Excel\THUE\bangkebanra.xls", System.Globalization.CultureInfo.CurrentCulture)
            Case "BangKeHoaDonMuaVao"
                workbook = Factory.GetWorkbook("\Excel\THUE\thue\bangkemuavao.xls", System.Globalization.CultureInfo.CurrentCulture)
        End Select

        excelViewer.ActiveWorkbook = workbook

    End Sub


    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick


        Select Case Me.Tag
            Case "BangKeHoaDonBanRa"
                workbook = Factory.GetWorkbook("E:\BACSOFTNET\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N0.xls", System.Globalization.CultureInfo.CurrentCulture)
            Case "BangKeHoaDonMuaVao"
                workbook = Factory.GetWorkbook("E:\BACSOFTNET\Excel\CHAOGIA\_MAUCHAOGIA_VIE_N0_L.xls", System.Globalization.CultureInfo.CurrentCulture)
        End Select

        excelViewer.ActiveWorkbook = workbook

        Dim sql As String = "SELECT SoHD,NgayHD,TenKH,MaSoThue,ThanhTien,TienThue,Thue,TyGia, "
        sql &= "convert(int,sohd)SoHDX,Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103)NgayHDX FROM CHUNGTU "
        sql &= "WHERE YEAR(NgayHD) = " & txtNam.EditValue & " AND MONTH(NgayHD) IN "
        Select Case cmbQuy.EditValue
            Case "I"
                sql &= " (1,2,3) "
            Case "II"
                sql &= " (4,5,6) "
            Case "III"
                sql &= " (7,8,9) "
            Case "IV"
                sql &= " (10,11,12) "
        End Select
        If chkGhiSo.EditValue = True Then
            sql &= " AND GhiSo = 1 "
        End If
        Select Case Me.Tag
            Case "BangKeHoaDonBanRa"
                sql &= " AND LoaiCT = " & ChungTu.LoaiChungTu.HoaDonDauRa & " "
                sql &= "order by  ngayhd asc, SoHDX asc "
            Case "BangKeHoaDonMuaVao"
                sql &= " AND LoaiCT = " & ChungTu.LoaiChungTu.HoaDonDauVao & " "
                sql &= "order by ngayhd asc "
        End Select

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        excelViewer.GetLock()
        Dim ws As IWorksheet = excelViewer.ActiveWorkbook.Sheets(0)


        Dim _cells As IRange = ws.Cells

        Select Case Me.Tag
            Case "BangKeHoaDonBanRa"

                Dim _RowIndex As Integer = 18
                Dim _ColumnCount As Integer = 11

                '-- Không thuế --
                Dim _sttKT As Integer = 1
                Dim arrRow = dt.Select("Thue is null", "NgayHDX asc, SoHDX asc")
                For Each r As DataRow In arrRow
                    _cells(_RowIndex, 1, _RowIndex, 11).Insert(InsertShiftDirection.Down)
                    _cells("B" & _RowIndex).Value = _sttKT
                    _cells("E" & _RowIndex).Value = r("SoHD")
                    _cells("F" & _RowIndex).Value = CType(r("NgayHD"), DateTime).ToString("dd/MM/yyyy")
                    _cells("G" & _RowIndex).Value = r("TenKH")
                    _cells("H" & _RowIndex).Value = r("MaSoThue")
                    _cells("J" & _RowIndex).Value = Math.Round(r("ThanhTien") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _cells("K" & _RowIndex).Value = Math.Round(r("TienThue") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _sttKT = _sttKT + 1
                    _RowIndex = +_RowIndex + 1
                Next

                '-- Thuế 0 % --
                _RowIndex = _RowIndex + 3
                _sttKT = 1
                arrRow = dt.Select("Thue = 0", "NgayHDX asc, SoHDX asc")
                For Each r As DataRow In arrRow
                    _cells(_RowIndex, 1, _RowIndex, 11).Insert(InsertShiftDirection.Down)
                    _cells("B" & _RowIndex).Value = _sttKT
                    _cells("E" & _RowIndex).Value = r("SoHD")
                    _cells("F" & _RowIndex).Value = CType(r("NgayHD"), DateTime).ToString("dd/MM/yyyy")
                    _cells("G" & _RowIndex).Value = r("TenKH")
                    _cells("H" & _RowIndex).Value = r("MaSoThue")
                    _cells("J" & _RowIndex).Value = Math.Round(r("ThanhTien") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _cells("K" & _RowIndex).Value = Math.Round(r("TienThue") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _sttKT = _sttKT + 1
                    _RowIndex = +_RowIndex + 1
                Next

                '-- Thuế 5 % --
                _RowIndex = _RowIndex + 3
                _sttKT = 1
                arrRow = dt.Select("Thue = 5", "NgayHDX asc, SoHDX asc")
                For Each r As DataRow In arrRow
                    _cells(_RowIndex, 1, _RowIndex, 11).Insert(InsertShiftDirection.Down)
                    _cells("B" & _RowIndex).Value = _sttKT
                    _cells("E" & _RowIndex).Value = r("SoHD")
                    _cells("F" & _RowIndex).Value = CType(r("NgayHD"), DateTime).ToString("dd/MM/yyyy")
                    _cells("G" & _RowIndex).Value = r("TenKH")
                    _cells("H" & _RowIndex).Value = r("MaSoThue")
                    _cells("J" & _RowIndex).Value = Math.Round(r("ThanhTien") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _cells("K" & _RowIndex).Value = Math.Round(r("TienThue") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _sttKT = _sttKT + 1
                    _RowIndex = +_RowIndex + 1
                Next


                '-- Thuế 10 % --
                _RowIndex = _RowIndex + 3
                _sttKT = 1
                arrRow = dt.Select("Thue = 10", "NgayHDX asc, SoHDX asc")
                For Each r As DataRow In arrRow
                    _cells(_RowIndex, 1, _RowIndex, 11).Insert(InsertShiftDirection.Down)
                    _cells("B" & _RowIndex).Value = _sttKT
                    _cells("E" & _RowIndex).Value = r("SoHD")
                    _cells("F" & _RowIndex).Value = CType(r("NgayHD"), DateTime).ToString("dd/MM/yyyy")
                    _cells("G" & _RowIndex).Value = r("TenKH")
                    _cells("H" & _RowIndex).Value = r("MaSoThue")
                    _cells("J" & _RowIndex).Value = Math.Round(r("ThanhTien") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _cells("K" & _RowIndex).Value = Math.Round(r("TienThue") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _sttKT = _sttKT + 1
                    _RowIndex = +_RowIndex + 1
                Next


            Case "BangKeHoaDonMuaVao"


                Dim _RowIndex As Integer = 18
                Dim _ColumnCount As Integer = 11

                Dim _sttKT As Integer = 1
                For Each r As DataRow In dt.Rows
                    _cells(_RowIndex, 1, _RowIndex, 12).Insert(InsertShiftDirection.Down)
                    _cells("B" & _RowIndex).Value = _sttKT
                    _cells("E" & _RowIndex).Value = r("SoHD")
                    _cells("F" & _RowIndex).Value = CType(r("NgayHD"), DateTime).ToString("dd/MM/yyyy")
                    _cells("G" & _RowIndex).Value = r("TenKH")
                    _cells("H" & _RowIndex).Value = r("MaSoThue")
                    _cells("J" & _RowIndex).Value = Math.Round(r("ThanhTien") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _cells("L" & _RowIndex).Value = Math.Round(r("TienThue") * r("TyGia"), 0, MidpointRounding.AwayFromZero)
                    _sttKT = _sttKT + 1
                    _RowIndex = +_RowIndex + 1
                Next


        End Select

        excelViewer.ReleaseLock()

    End Sub


    Private Sub btnKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKetXuat.ItemClick
        Dim saveDlg As New SaveFileDialog
        saveDlg.Filter = "Excel files (*.xls)|*.xls"


        Select Case Me.Tag
            Case "BangKeHoaDonBanRa"
                saveDlg.FileName = "BangKeBanRa_Quy_" & cmbQuy.EditValue & "_" & txtNam.EditValue
            Case "BangKeHoaDonMuaVao"
                saveDlg.FileName = "BangKeMuaVao_Quy_" & cmbQuy.EditValue & "_" & txtNam.EditValue
        End Select



        If saveDlg.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            excelViewer.GetLock()
            workbook.SaveAs(saveDlg.FileName, FileFormat.Excel8)
            excelViewer.ReleaseLock()
            If ShowCauHoi("Bạn có muốn mở file bảng kê vừa kết xuất không ?") Then
                Dim p As New System.Diagnostics.Process
                p.StartInfo.FileName = saveDlg.FileName
                p.Start()
            End If
        End If



    End Sub





End Class
