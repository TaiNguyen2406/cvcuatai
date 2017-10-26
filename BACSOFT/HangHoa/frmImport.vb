Imports BACSOFT.Db.SqlHelper
Imports SpreadsheetGear

Public Class frmImport

    Public tbVT As DataTable

    Private Sub frmImport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim sql As String = ""
        sql &= " SELECT column_name AS col1, N'' AS col2  "
        sql &= " FROM information_schema.columns WHERE table_name = 'VATTU' "
        sql &= " AND COLUMN_NAME <> convert(sysname,'ID')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'IDTenvattu')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'IDHangSanxuat')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'IDDonvitinh')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'IDTentailieu')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'MaLoi')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'DMTon')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'DatToithieu')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'AZ')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'Ton')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'Ngay')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'IDUser')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'HangTon')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'TaiLieu')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'IDTennhom')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'ThongDung')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'HinhAnh')"
        sql &= " AND COLUMN_NAME <> convert(sysname,'ThayDoi')"

        sql &= " SELECT column_name AS ID "
        sql &= " FROM information_schema.columns WHERE table_name = 'tblImportVT' "


        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdv.DataSource = ds.Tables(0)
            rcbCotExcel.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        ShowWaiting("Đang xử lý ...")
        Dim _updateTonHang As Boolean = False
        Dim sqlstrI As String = ""
        Dim sqlstrU As String = ""
        Dim sqlstrV As String = ""
        Dim sqlstrVI As String = ""
        Dim sql As String = ""
        Dim sqlIn As String = ""


        For i As Integer = 0 To gdvCT.RowCount - 1

            If gdvCT.GetRowCellValue(i, "col2").ToString <> "" Then
                sqlstrU &= gdvCT.GetRowCellValue(i, "col1") & ","
                If gdvCT.GetRowCellValue(i, "col1") = "TonNCC" Then
                    _updateTonHang = True
                End If
            End If
        Next
        sqlstrU = sqlstrU.Remove(sqlstrU.Length - 1, 1)
        If sqlstrU = "" Then
            ShowCanhBao("Chưa chọn cột cần nhập dữ liệu !")
            Exit Sub
        End If
        sqlstrU = ""
        For j As Integer = 0 To gdvCT.RowCount - 1
            If gdvCT.GetRowCellValue(j, "col2").ToString <> "" Then
                If gdvCT.GetRowCellValue(j, "col1").ToString <> "Model" And gdvCT.GetRowCellValue(j, "col1").ToString <> "Code" Then
                    sqlstrI &= gdvCT.GetRowCellValue(j, "col1") & ","
                End If

                If gdvCT.GetRowCellValue(j, "col1").ToString = "Mucthue1" Then
                    sqlstrV &= "ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",1000),"
                    sqlstrVI &= "ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",1000),"
                    sqlstrU &= "VATTU." & gdvCT.GetRowCellValue(j, "col1") & "=ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",1000),"
                ElseIf gdvCT.GetRowCellValue(j, "col1").ToString = "Thongso" Or gdvCT.GetRowCellValue(j, "col1").ToString = "ThongSo_ENG" Then
                    sqlstrV &= "ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",N''),"
                    sqlstrVI &= "ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",N''),"
                    sqlstrU &= "VATTU." & gdvCT.GetRowCellValue(j, "col1") & "=ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",N''),"
                Else
                    sqlstrV &= "ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",0),"
                    If gdvCT.GetRowCellValue(j, "col1").ToString <> "Model" And gdvCT.GetRowCellValue(j, "col1").ToString <> "Code" Then
                        sqlstrVI &= "ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",0),"
                    End If
                    sqlstrU &= "VATTU." & gdvCT.GetRowCellValue(j, "col1") & "=ISNULL(tblImportVT." & gdvCT.GetRowCellValue(j, "col2") & ",0),"
                End If

            End If
        Next
        Application.DoEvents()
        Threading.Thread.Sleep(1000)
        CloseWaiting()
        'sqlstrU = sqlstrU.Remove(sqlstrU.Length - 1, 1)
        'sqlstrV = sqlstrV.Remove(sqlstrV.Length - 1, 1)
        Try
            BeginTransaction()
            ' Đặt lại trạng thái trong bảng vật tự trước khi import     0: Ban đầu, 1: Cập nhật, 2: Thêm mới
            Application.DoEvents()
            Threading.Thread.Sleep(1000)

            ShowWaiting("Đang đặt lại trạng thái hàng hóa ...")
            sql &= " UPDATE VATTU SET Import=0"
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Application.DoEvents()
            Threading.Thread.Sleep(1000)
            CloseWaiting()

            If _updateTonHang = True Then
                Application.DoEvents()
                Threading.Thread.Sleep(1000)

                ShowWaiting("Đặt tồn hãng = 0 ...")
                sql = ""

                sql &= " UPDATE VATTU SET TonNCC=N'' WHERE IDHangSanXuat=" & CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterHangSX.EditValue

                If Not CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btfilterTenVT.EditValue Is Nothing Then
                    sql &= " AND IDTenVatTu=" & CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btfilterTenVT.EditValue
                End If

                If Not CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterNhomVT.EditValue Is Nothing Then
                    sql &= " AND IDTennhom=" & CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterNhomVT.EditValue
                End If

                If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Application.DoEvents()
                Threading.Thread.Sleep(1000)
                CloseWaiting()
            End If

            Application.DoEvents()
            Threading.Thread.Sleep(1000)

            ShowWaiting("Đang update hàng hóa cũ ...")
            sql = ""
            ' Update thông tin các vật tư có sẵn trong hệ thống
            sql &= " Update VATTU "
            sql &= " 	SET " & sqlstrU & "VATTU.Import=1"
            sql &= "         FROM VATTU, tblImportVT"
            If chkBaoGomCode.Checked Then
                If rdModelVT.Checked Then
                    If rdModelE.Checked Then
                        sql &= " WHERE RTrim(LTrim(VATTU.Model)) = RTrim(LTrim(tblImportVT.Model))"
                    Else
                        sql &= " WHERE RTrim(LTrim(VATTU.Model)) = RTrim(LTrim(tblImportVT.Code))"
                    End If
                Else
                    If rdModelE.Checked Then
                        sql &= " WHERE RTrim(LTrim(VATTU.Code)) = RTrim(LTrim(tblImportVT.Model))"
                    Else
                        sql &= " WHERE RTrim(LTrim(VATTU.Code)) = RTrim(LTrim(tblImportVT.Code))"
                    End If
                End If
            Else
                sql &= " WHERE RTrim(LTrim(VATTU.Model)) = RTrim(LTrim(tblImportVT.Model))"
            End If

            sql &= " AND VatTu.IDHangSanXuat =" & CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterHangSX.EditValue


            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Application.DoEvents()
            Threading.Thread.Sleep(1000)
            CloseWaiting()

            Application.DoEvents()
            Threading.Thread.Sleep(1000)

            ShowWaiting("Ghi lại trạng thái cập nhật ...")
            sql = ""

            ' Đặt trạng thái đã update cho các vật tư trong file excel
            sql &= " Update tblImportVT "
            sql &= " SET tblImportVT.TrangThai = 1"
            sql &= " FROM tblImportVT, VATTU "
            If chkBaoGomCode.Checked Then
                If rdModelVT.Checked Then
                    If rdModelE.Checked Then
                        sql &= " WHERE RTrim(LTrim(VATTU.Model)) = RTrim(LTrim(tblImportVT.Model))"
                    Else
                        sql &= " WHERE RTrim(LTrim(VATTU.Model)) = RTrim(LTrim(tblImportVT.Code))"
                    End If
                Else
                    If rdModelE.Checked Then
                        sql &= " WHERE RTrim(LTrim(VATTU.Code)) = RTrim(LTrim(tblImportVT.Model))"
                    Else
                        sql &= " WHERE RTrim(LTrim(VATTU.Code)) = RTrim(LTrim(tblImportVT.Code))"
                    End If
                End If
            Else
                sql &= " WHERE RTrim(LTrim(VATTU.Model)) = RTrim(LTrim(tblImportVT.Model))"
            End If

            sql &= " AND VATTU.Import = 1 "

            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Application.DoEvents()
            Threading.Thread.Sleep(1000)
            CloseWaiting()
            ' Thêm mới các vật tư không có sẵn trong file excel
            Application.DoEvents()
            Threading.Thread.Sleep(1000)
            If ShowCauHoi("Bạn có muốn thêm mã mới nếu chưa có trên hệ thống hay không ?") Then
                Application.DoEvents()
                Threading.Thread.Sleep(1000)
                ShowWaiting("Thêm mã mới ...")
                sql = ""

                sql &= " INSERT INTO VATTU(Model,Code,IDTenVatTu,IDHangSanXuat,IDDonViTinh,IDTenNhom," & sqlstrI & "Import)"
                If chkBaoGomCode.Checked Then
                    sql &= " SELECT Model,Code,"
                Else
                    sql &= " SELECT Model,Model AS Code,"
                End If

                If CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btfilterTenVT.EditValue Is Nothing Then
                    sql &= "1631,"
                Else
                    sql &= CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btfilterTenVT.EditValue & ","
                End If

                If CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterHangSX.EditValue Is Nothing Then
                    sql &= "896,"
                Else
                    sql &= CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterHangSX.EditValue & ","
                End If
                sql &= "94,"

                If CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterNhomVT.EditValue Is Nothing Then
                    sql &= "97,"
                Else
                    sql &= CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btFilterNhomVT.EditValue & ","
                End If

                sql &= sqlstrVI & "2 FROM tblImportVT WHERE tblImportVT.TrangThai=0"

                If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
            

            ComitTransaction()

            Application.DoEvents()
            Threading.Thread.Sleep(1000)
            CloseWaiting()
        Catch ex As Exception
            RollBackTransaction()
            CloseWaiting()
            ShowBaoLoi(ex.Message)
            Exit Sub
        End Try
        Application.DoEvents()
        Threading.Thread.Sleep(1000)

        ShowWaiting("Đang tạo file log ...")
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblImportVT")
        Dim fileName As String
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                Dim wb As IWorkbook = SpreadsheetGear.Factory.GetWorkbook
                'wb.
                Dim ws As IWorksheet = wb.Worksheets("Sheet1")
                ws.Name = "Import log"

                Dim range As IRange = ws.Cells("A1")

                range.CopyFromDataTable(tb, SpreadsheetGear.Data.SetDataFlags.None)

                ws.UsedRange.Columns.AutoFit()
                Try
                    fileName = Application.StartupPath & "\ImportLog" & Now.ToString("yyyyMMddhhmm") & ".xls"
                    wb.SaveAs(fileName, FileFormat.Excel8)
                    CloseWaiting()
                    If ShowCauHoi("Đã thực hiện xong, bạn có muốn mở file log hay không ?") Then
                        OpenFile(fileName)
                    End If

                Catch ex As Exception
                    CloseWaiting()
                    ShowBaoLoi(ex.Message)
                End Try


            Else
                CloseWaiting()
                ShowCanhBao("Không có kết quả !")
            End If

        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub


    Private Sub rcbCotExcel_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbCotExcel.ButtonClick
        If e.Button.Index = 1 Then
            gdvCT.SetFocusedRowCellValue("col2", Nothing)
        End If
    End Sub

    Private Sub chkBaoGomCode_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBaoGomCode.CheckedChanged
        '  If chkBaoGomCode.Checked Then
        rdCodeE.Enabled = chkBaoGomCode.Checked
        rdCodeVT.Enabled = chkBaoGomCode.Checked
        rdModelE.Enabled = chkBaoGomCode.Checked
        rdModelVT.Enabled = chkBaoGomCode.Checked
        '  Else
        'rdCodeE.Enabled = False
        'rdCodeVT.Enabled = False
        'rdModelE.Enabled = False
        'rdModelVT.Enabled = False
        'End If
    End Sub
End Class