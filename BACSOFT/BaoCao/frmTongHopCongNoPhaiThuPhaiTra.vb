Imports BACSOFT.Db.SqlHelper

Public Class frmTongHopCongNoPhaiThuPhaiTra

    Private Sub frmTongHopCongNoPhaiThuPhaiTra_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Dim tg As DateTime = GetServerTime()
        txtDenNgay.EditValue = New Date(tg.Year, tg.Month, tg.Day)
        tg = tg.AddMonths(-1)
        txtTuNgay.EditValue = New Date(tg.Year, tg.Month, tg.Day)

        Select Case Me.Tag
            Case "TONGHOPCONGNOPHAITHU"
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI THU"
                tab2.Text = "CHI TIẾT CÔNG NỢ PHẢI THU"
            Case "TONGHOPCONGNOPHAITRA"
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI TRẢ"
                tab2.Text = "CHI TIẾT CÔNG NỢ PHẢI TRẢ"
        End Select

        btnTaiDuLieu.PerformClick()


    End Sub

    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick
        Select Case Me.Tag
            Case "TONGHOPCONGNOPHAITHU"
                LoadTongHopCongNoPhaiThu()
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI THU"
                PhaiThu()
            Case "TONGHOPCONGNOPHAITRA"
                LoadTongHopCongNoPhaiTra()
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

        Dim sql As String = txtTongHopCongNoPhaiThu.EditValue

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
                tab2.Text = gdvData.GetFocusedRowCellValue("ttcMa") & ": " & ExecuteSQLDataTable("SELECT Ten FROM KHACHHANG WHERE ttcMa = N'" & gdvData.GetFocusedRowCellValue("ttcMa") & "'").Rows(0)(0)
                Select Case Me.Tag
                    Case "TONGHOPCONGNOPHAITHU"
                        ChiTietCongNoPhaiThu()
                    Case "TONGHOPCONGNOPHAITRA"

                End Select
            Catch ex As Exception
                tab2.Text = gdvData.GetFocusedRowCellValue("ttcMa")
            End Try

        End If
    End Sub



    Private Sub ChiTietCongNoPhaiThu()

        Dim sql As String = txtChiTietCongNoPhaiThu.EditValue

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
                        LuyKe += obj2N(dt.Rows(i)("PhatSinhNo"))
                    Case "Tạm ứng TM", "Tạm ứng NH"
                        LuyKe -= obj2N(dt.Rows(i)("TongTienTamUng"))
                    Case "Thu xuất kho TM", "Thu xuất kho NH"
                        LuyKe -= obj2N(dt.Rows(i)("PhatSinhCo"))
                    Case "Hoàn tạm ứng TM", "Hoàn tạm ứng NH"
                        LuyKe += obj2N(dt.Rows(i)("PhatSinhNo"))
                End Select

                dt.Rows(i)("LuyKe") = LuyKe
            Next
        End If

        Dim rCuoiKy As DataRow = dt.NewRow
        rCuoiKy("TenVatTu") = " -- Cuối kỳ:"
        rCuoiKy("PhatSinhNo") = obj2N(dt.Compute("SUM(PhatSinhNo)", ""))
        rCuoiKy("TongTienTamUng") = obj2N(dt.Compute("SUM(TongTienTamUng)", ""))
        rCuoiKy("ConLaiTamUng") = obj2N(dt.Compute("SUM(TongTienTamUng)", "")) - obj2N(dt.Compute("SUM(PhatSinhCo)", "LoaiCT='Phân bổ'")) - obj2N(dt.Compute("SUM(PhatSinhNo)", "LoaiCT = 'Hoàn tạm ứng TM' OR LoaiCT ='Hoàn tạm ứng NH'"))
        rCuoiKy("PhatSinhCo") = obj2N(dt.Compute("SUM(PhatSinhCo)", ""))
        rCuoiKy("LuyKe") = LuyKe
        dt.Rows.InsertAt(rCuoiKy, dt.Rows.Count)

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



    Private Sub gdvDataChiTiet_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvDataChiTiet.CustomDrawCell

        If e.RowHandle = 0 Or e.RowHandle = gdvDataChiTiet.RowCount - 1 Then
            e.Appearance.BackColor = Color.LightPink
            'e.Appearance.ForeColor = Color.White
            e.Appearance.Font = New Font(Me.Font, FontStyle.Bold)
        End If

        If e.RowHandle <= 0 Then Exit Sub
        Select Case gdvDataChiTiet.GetRowCellValue(e.RowHandle, "LoaiCT").ToString
            Case "Phiếu XK"
                e.Appearance.ForeColor = Color.Red
            Case "Phân bổ"
                e.Appearance.ForeColor = Color.Navy
            Case "Xuất kho", "Xuất kho CT"
                e.Appearance.ForeColor = Color.Gray
            Case "Tạm ứng TM", "Tạm ứng NH"
                e.Appearance.ForeColor = Color.Blue
            Case "Tạm ứng còn lại"
                e.Appearance.ForeColor = Color.Navy
            Case "Thu xuất kho TM", "Thu xuất kho NH"
                e.Appearance.ForeColor = Color.Green
        End Select
    End Sub




    Private Sub txtTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtTuNgay.EditValueChanged
        Dim ngay As Date = Convert.ToDateTime(txtTuNgay.EditValue)
        ngay = New Date(ngay.Year, ngay.Month, ngay.Day)
        Dim ngayChot As New Date(2017, 8, 1)
        If ngay < ngayChot Then txtTuNgay.EditValue = ngayChot
    End Sub


End Class
