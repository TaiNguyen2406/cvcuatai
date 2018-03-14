Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraPrinting
Imports System.IO

Public Class frmTongHopCongNoPhaiTraMoi

    Private source As String

    Private Property txtTongHopCongNoPhaiThu As Object

    Private Sub frmTongHopCongNoPhaiThuPhaiTra_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim tg As DateTime = GetServerTime()
        txtTuNgay.EditValue = New Date(2017, 1, 1)
        'tg = tg.AddMonths(-1)
        txtDenNgay.EditValue = New Date(tg.Year, tg.Month, tg.Day)

        Select Case Me.Tag
            Case "TONGHOPCONGNOPHAITHU"
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI THU"
                tab2.Text = "CHI TIẾT CÔNG NỢ PHẢI THU"
            Case "TONGHOPCONGNOPHAITRA"
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI TRẢ"
                tab2.Text = "CHI TIẾT CÔNG NỢ PHẢI TRẢ"
                source = txtChiTietCongNoPhaiTra.EditValue
        End Select


        btnTaiDuLieu.PerformClick()

    End Sub

    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick
        Select Case Me.Tag
            Case "TONGHOPCONGNOPHAITHU"
                '    LoadTongHopCongNoPhaiThu()
                '  tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI THU"
                '  PhaiThu()
            Case "TONGHOPCONGNOPHAITRA"
                LoadTongHopCongNoPhaiTra()
                If gdvData.FocusedRowHandle >= 0 Then
                    LoadChiTietCongNoPhaiTra()
                End If

                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI TRẢ"
                PhaiTra()
        End Select

    End Sub
    Private Sub PhaiThu()
        For i As Integer = 0 To gdvData.Bands.VisibleBandCount - 1
            'gdvData.Bands(i).Caption = gdvData.Bands(i).Tag
            For Each b As DevExpress.XtraGrid.Views.BandedGrid.GridBand In gdvData.Bands(i).Children
                b.Caption = b.Tag
            Next
        Next
    End Sub
    Private Sub PhaiTra()
        For i As Integer = 0 To gdvData.Bands.VisibleBandCount - 1
            '  gdvData.Bands(i).Caption = gdvData.Bands(i).Tag
            For Each b As DevExpress.XtraGrid.Views.BandedGrid.GridBand In gdvData.Bands(i).Children
                b.Caption = b.ToolTip
            Next
        Next
    End Sub

    Private Sub LoadTongHopCongNoPhaiThu()

        Dim sql As String = txtTongHopCongNoPhaiTra.EditValue

        sql = sql.Replace("01/01/2016", Convert.ToDateTime(txtTuNgay.EditValue).ToString("dd/MM/yyyy"))
        sql = sql.Replace("31/12/2016", Convert.ToDateTime(txtDenNgay.EditValue).ToString("dd/MM/yyyy"))


        Dim dt As DataTable = Nothing
        Dim isOK As Boolean = False
        Dim tg As DateTime = Now

        Dim frmDoi As New DevExpress.XtraEditors.XtraForm
        frmDoi.StartPosition = FormStartPosition.CenterScreen
        frmDoi.FormBorderStyle = FormBorderStyle.None
        frmDoi.Width = 350
        frmDoi.Height = 25
        frmDoi.TopLevel = True
        frmDoi.TopMost = True
        Dim prc As New DevExpress.XtraEditors.MarqueeProgressBarControl
        prc.Properties.ShowTitle = True
        prc.Properties.Appearance.Font = New Font(Me.Font.Name, 10, FontStyle.Bold)
        prc.Properties.Appearance.ForeColor = Color.Red
        prc.Properties.MarqueeAnimationSpeed = 30
        prc.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Broken
        prc.Dock = DockStyle.Fill


        frmDoi.Controls.Add(prc)
        frmDoi.Show()

        Dim th As New Threading.Thread( _
           Sub()
               CheckForIllegalCrossThreadCalls = False
               dt = ExecuteSQLDataTable(sql)
               If dt Is Nothing Then
                   frmDoi.Close()
                   ShowBaoLoi(LoiNgoaiLe)
               End If
               isOK = True
           End Sub
       )
        th.Start()

        While Not isOK
            Application.DoEvents()
            prc.Text = "Tải dữ liệu, đang đợi " & DateDiff(DateInterval.Second, tg, Now) + 1 & "s ..."
        End While

        gdv.DataSource = dt
        frmDoi.Close()

    End Sub

    Private Sub LoadTongHopCongNoPhaiTra()

        Dim sql As String = txtTongHopCongNoPhaiTra.EditValue

        sql = sql.Replace("01/01/2016", Convert.ToDateTime(txtTuNgay.EditValue).ToString("dd/MM/yyyy"))
        sql = sql.Replace("31/12/2016", Convert.ToDateTime(txtDenNgay.EditValue).ToString("dd/MM/yyyy"))


        Dim dt As DataTable = Nothing
        Dim isOK As Boolean = False
        Dim tg As DateTime = Now

        Dim frmDoi As New DevExpress.XtraEditors.XtraForm
        frmDoi.StartPosition = FormStartPosition.CenterScreen
        frmDoi.FormBorderStyle = FormBorderStyle.None
        frmDoi.Width = 350
        frmDoi.Height = 25
        frmDoi.TopLevel = True
        frmDoi.TopMost = True
        Dim prc As New DevExpress.XtraEditors.MarqueeProgressBarControl
        prc.Properties.ShowTitle = True
        prc.Properties.Appearance.Font = New Font(Me.Font.Name, 10, FontStyle.Bold)
        prc.Properties.Appearance.ForeColor = Color.Red
        prc.Properties.MarqueeAnimationSpeed = 30
        prc.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Broken
        prc.Dock = DockStyle.Fill


        frmDoi.Controls.Add(prc)
        frmDoi.Show()

        Dim th As New Threading.Thread( _
           Sub()
               CheckForIllegalCrossThreadCalls = False
               dt = ExecuteSQLDataTable(sql)
               If dt Is Nothing Then
                   frmDoi.Close()
                   ShowBaoLoi(LoiNgoaiLe)
               End If

               isOK = True
           End Sub
       )
        th.Start()

        While Not isOK
            Application.DoEvents()
            prc.Text = "Tải dữ liệu, đang đợi " & DateDiff(DateInterval.Second, tg, Now) + 1 & "s ..."
        End While

        gdv.DataSource = dt
        frmDoi.Close()

    End Sub
    Private Sub LoadChiTietCongNoPhaiTra()


        Dim sql As String = txtChiTietCongNoPhaiTra.EditValue

        sql = sql.Replace("01/01/2016", Convert.ToDateTime(txtTuNgay.EditValue).ToString("dd/MM/yyyy"))
        sql = sql.Replace("31/12/2016", Convert.ToDateTime(txtDenNgay.EditValue).ToString("dd/MM/yyyy"))
        sql = sql.Replace("20 * 1 * 1 * 1", gdvData.GetFocusedRowCellValue("ID"))


        Dim dt As DataTable = Nothing
        Dim isOK As Boolean = False

        dt = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If

        Dim LuyKe As Double = dt.Rows(0)("LuyKe")
        Dim SoTT As Integer = 1
        If Not dt Is Nothing Then
            For i As Integer = 1 To dt.Rows.Count - 1
                If dt.Rows(i)("SoTT") Is DBNull.Value Then
                    dt.Rows(i)("SoTT") = SoTT
                    SoTT += 1
                Else
                    dt.Rows(i)("SoTT") = DBNull.Value
                End If
                '-- Tính dòng tạm ứng còn lại --
                If dt.Rows(i)("LoaiCT") = "Tạm ứng còn lại" Then
                    Dim TienTamUng As Double = obj2N(dt.Compute("SUM(TongTienTamUng)", "SoCG='" & dt.Rows(i)("SoCG") & "' AND (LoaiCT = 'Tạm ứng TM' OR LoaiCT ='Tạm ứng NH')"))
                    Dim TienPhanBo As Double = obj2N(dt.Compute("SUM(PhatSinhCo)", "SoCG='" & dt.Rows(i)("SoCG") & "' AND (LoaiCT = 'Phân bổ')"))
                    Dim HoanTamUng As Double = obj2N(dt.Compute("SUM(PhatSinhNo)", "SoCG='" & dt.Rows(i)("SoCG") & "' AND (LoaiCT = 'Hoàn tạm ứng TM' OR LoaiCT ='Hoàn tạm ứng NH')"))
                    dt.Rows(i)("ConLaiTamUng") = TienTamUng - TienPhanBo - HoanTamUng
                End If

                '-- Tính lũy kế -- - PsNo - Tong Tam Ung - Thu xuat kho tm,nh
                Select Case dt.Rows(i)("LoaiCT").ToString
                    Case "Xuất kho", "Xuất kho CT"
                        Continue For
                    Case "Phiếu XK"
                        LuyKe += obj2N(dt.Rows(i)("PhatSinhCo"))
                    Case "Tạm ứng TM", "Tạm ứng NH"
                        LuyKe -= obj2N(dt.Rows(i)("TongTienTamUng"))
                    Case "Thu xuất kho TM", "Thu xuất kho NH"
                        LuyKe -= obj2N(dt.Rows(i)("PhatSinhNo"))
                    Case "Hoàn tạm ứng TM", "Hoàn tạm ứng NH"
                        LuyKe += obj2N(dt.Rows(i)("PhatSinhCo"))
                    Case "Vay tiền TM", "Vay tiền NH"
                        LuyKe += obj2N(dt.Rows(i)("PhatSinhCo"))
                    Case "Trả tiền vay TM", "Trả tiền vay NH"
                        LuyKe -= obj2N(dt.Rows(i)("PhatSinhNo"))
                End Select

                dt.Rows(i)("LuyKe") = LuyKe
                _LuyKe = LuyKe
            Next
        End If

        'Dim rCuoiKy As DataRow = dt.NewRow
        'rCuoiKy("TenVatTu") = " -- Cuối kỳ:"
        'rCuoiKy("PhatSinhNo") = obj2N(dt.Compute("SUM(PhatSinhNo)", ""))
        'rCuoiKy("TongTienTamUng") = obj2N(dt.Compute("SUM(TongTienTamUng)", ""))
        'rCuoiKy("ConLaiTamUng") = obj2N(dt.Compute("SUM(TongTienTamUng)", "")) - obj2N(dt.Compute("SUM(PhatSinhCo)", "LoaiCT='Phân bổ'")) - obj2N(dt.Compute("SUM(PhatSinhNo)", "LoaiCT = 'Hoàn tạm ứng TM' OR LoaiCT ='Hoàn tạm ứng NH'"))
        'rCuoiKy("PhatSinhCo") = obj2N(dt.Compute("SUM(PhatSinhCo)", ""))
        'rCuoiKy("LuyKe") = LuyKe
        'dt.Rows.InsertAt(rCuoiKy, dt.Rows.Count)

        gdvChiTiet.DataSource = dt

    End Sub
    Private Function obj2N(obj As Object)
        Try
            If obj Is DBNull.Value Then Return 0
            Return Convert.ToDouble(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        If IsNumeric(e.CellValue) AndAlso e.CellValue = 0 Then
            e.DisplayText = ""
        End If
    End Sub

    Private Sub gdvData_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvData.FocusedRowChanged
        If e.FocusedRowHandle < 0 Then
            Select Case Me.Tag
                Case "TONGHOPCONGNOPHAITHU"
                    tab2.Text = "CHI TIẾT CÔNG NỢ PHẢI THU"
                Case "TONGHOPCONGNOPHAITRA"
                    tab2.Text = "CHI TIẾT CÔNG NỢ PHẢI TRẢ"
            End Select
        Else
            Try
                tab2.Text = gdvData.GetFocusedRowCellValue("ttcMa") & ": " & ExecuteSQLDataTable("SELECT Ten FROM KHACHHANG WHERE Id = " & gdvData.GetFocusedRowCellValue("ID")).Rows(0)(0)
                Select Case Me.Tag
                    Case "TONGHOPCONGNOPHAITHU"

                    Case "TONGHOPCONGNOPHAITRA"
                        LoadChiTietCongNoPhaiTra()
                End Select
            Catch ex As Exception
                tab2.Text = gdvData.GetFocusedRowCellValue("ttcMa")
            End Try

        End If
    End Sub
    Private Sub gdvDataChiTiet_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvDataChiTiet.CustomDrawCell

        If e.RowHandle = 0 Or e.RowHandle = gdvDataChiTiet.RowCount - 1 Then
            e.Appearance.BackColor = Color.LightPink
            e.Appearance.Font = New Font(Me.Font, FontStyle.Bold)
        End If

        If e.RowHandle <= 0 Then Exit Sub
        Select Case gdvDataChiTiet.GetRowCellValue(e.RowHandle, "LoaiCT").ToString
            Case "Phiếu NK"
                e.Appearance.ForeColor = Color.Orange
            Case "Phân bổ"
                e.Appearance.ForeColor = Color.DarkGreen
                '    Case "Nhập kho"
                '      e.Appearance.ForeColor = Color.Gray
            Case "Tạm ứng TM", "Tạm ứng NH"
                e.Appearance.ForeColor = Color.Blue
            Case "Vay tiền TM", "Vay tiền NH"
                e.Appearance.ForeColor = Color.SaddleBrown
            Case "Trả tiền vay TM", "Trả tiền vay NH"
                e.Appearance.ForeColor = Color.Teal
            Case "Thu nhập kho TM", "Thu nhập kho NH"
                e.Appearance.ForeColor = Color.Green
        End Select
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Alt Or Keys.O) Then
            If txtTongHopCongNoPhaiTra.Visible = True Then
                txtTongHopCongNoPhaiTra.Visible = False
                txtChiTietCongNoPhaiTra.Visible = False
            Else
                txtTongHopCongNoPhaiTra.Visible = True
                txtChiTietCongNoPhaiTra.Visible = True
            End If
            Return True
        End If
        Return False
    End Function

    Private Sub btnKetXuat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKetXuat.ItemClick
        Dim gvexprort As Object = gdvData
        Dim tu As DateTime = txtTuNgay.EditValue
        Dim den As DateTime = txtDenNgay.EditValue
        Dim filename As String = "Công nợ phải trả từ " & tu.ToString("dd-MM-yyyy") & " đến " & den.ToString("dd-MM-yyyy")
        Dim saveDialog As SaveFileDialog = New SaveFileDialog()
        Try
            saveDialog.Filter = "Excel (2010) (.xlsx)|*.xlsx"
            saveDialog.FileName = filename
            If saveDialog.ShowDialog() = DialogResult.OK Then
                ShowWaiting("Đang kết xuất ...")
                Dim exportFilePath As String = saveDialog.FileName
                Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
                Dim str As String
                Dim tuychon As XlsExportOptions = New XlsExportOptions
                Dim tuychonx As XlsxExportOptions = New XlsxExportOptions

                tuychon.ShowGridLines() = True
                tuychonx.ShowGridLines() = True
                Select Case fileExtenstion

                    Case (".xlsx")
                        Try
                            gvexprort.ExportToXlsx(exportFilePath, tuychonx)
                        Catch ex As Exception
                            ShowBaoLoi(LoiNgoaiLe)
                        End Try

                End Select

                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    CloseWaiting()
                    If File.Exists(exportFilePath) Then
                        Try
                            System.Diagnostics.Process.Start(exportFilePath)
                        Catch ex As Exception
                            str = "Không thể mở file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                            ShowBaoLoi(str)
                        End Try
                    Else
                        str = "Không thể lưu file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                        ShowBaoLoi(str)
                    End If
                End If
            End If

        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
            CloseWaiting()
        End Try
    End Sub
    Dim _LuyKe As Double
    Private Sub gdvDataChiTiet_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvDataChiTiet.CustomSummaryCalculate
        Dim dt As DataTable = CType(gdvChiTiet.DataSource, DataTable)
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
            Select Case TryCast(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName
                Case "TenVatTu"
                    e.TotalValue = " -- Cuối kỳ:"
                    e.TotalValueReady = True
                    Exit Select
                Case "PhatSinhNo"
                    e.TotalValue = obj2N(dt.Compute("SUM(PhatSinhNo)", ""))
                    e.TotalValueReady = True
                    Exit Select
                Case "TongTienTamUng"
                    e.TotalValue = obj2N(dt.Compute("SUM(TongTienTamUng)", ""))
                    e.TotalValueReady = True
                    Exit Select
                Case "ConLaiTamUng"
                    e.TotalValue = obj2N(dt.Compute("SUM(TongTienTamUng)", "")) - obj2N(dt.Compute("SUM(PhatSinhCo)", "LoaiCT='Phân bổ'")) - obj2N(dt.Compute("SUM(PhatSinhNo)", "(LoaiCT = 'Hoàn tạm ứng TM' OR LoaiCT ='Hoàn tạm ứng NH') and LoaiHoanTamUng=0"))
                    e.TotalValueReady = True
                    Exit Select
                Case "PhatSinhCo"
                    e.TotalValue = obj2N(dt.Compute("SUM(PhatSinhCo)", ""))
                    e.TotalValueReady = True
                    Exit Select
                Case "LuyKe"
                    e.TotalValue = _LuyKe
                    e.TotalValueReady = True
                    Exit Select
            End Select
        End If

    End Sub
    Private Sub txtDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        txtTuNgay.EditValue = New DateTime(Convert.ToDateTime(txtDenNgay.EditValue).Year, Convert.ToDateTime(txtTuNgay.EditValue).Month, 1)
    End Sub
End Class
