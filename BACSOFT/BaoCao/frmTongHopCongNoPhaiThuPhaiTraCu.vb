Imports BACSOFT.Db.SqlHelper

Public Class frmTongHopCongNoPhaiThuPhaiTraCu

    Private Sub frmTongHopCongNoPhaiThuPhaiTra_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim tg As DateTime = GetServerTime()
        txtDenNgay.EditValue = New Date(tg.Year, tg.Month, tg.Day)
        tg = tg.AddYears(-1)
        txtTuNgay.EditValue = New Date(tg.Year, tg.Month, tg.Day)

        Select Case Me.Tag
            Case "TONGHOPCONGNOPHAITHU"
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI THU"
                tab2.Text = "CHI TIẾT CÔNG NỢ PHẢI THU"
                PhaiThu()
            Case "TONGHOPCONGNOPHAITRA"
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI TRẢ"
                tab2.Text = "CHI TIẾT CÔNG NỢ PHẢI TRẢ"
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
    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick

        Select Case Me.Tag
            Case "TONGHOPCONGNOPHAITHU"
                LoadTongHopCongNoPhaiThu()
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI THU"

            Case "TONGHOPCONGNOPHAITRA"
                LoadTongHopCongNoPhaiTra()
                tab1.Text = "TỔNG HỢP CÔNG NỢ PHẢI TRẢ"
        End Select

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

               dt.Columns.Add(New DataColumn("PhaiThuDauKy", Type.GetType("System.Double")))
               dt.Columns.Add(New DataColumn("PhaiThuTrongKy", Type.GetType("System.Double")))

               dt.Columns.Add(New DataColumn("TamUngCuoiKy", Type.GetType("System.Double")))
               dt.Columns.Add(New DataColumn("PhaiThuCuoiKy", Type.GetType("System.Double")))

               For i As Integer = 0 To dt.Rows.Count - 1
                   Dim r As DataRow = dt.Rows(i)
                   r("PhaiThuDauKy") = r("XuatKhoDauKy") + r("DaChiHoanTamUngDauKy") - r("PhanBoDauKy") - r("DaThuXuatKhoDauKy")
                   r("PhaiThuTrongKy") = r("XuatKhoTrongKy") + r("DaChiHoanTamUngTrongKy") - r("PhanBoTrongKy") - r("DaThuXuatKhoTrongKy")

                   r("TamUngCuoiKy") = r("TamUngDauKy") + r("TamUngTrongKy")
                   r("PhaiThuCuoiKy") = r("PhaiThuDauKy") + r("XuatKhoTrongKy") - r("DaThuXuatKhoTrongKy") + r("DaChiHoanTamUngTrongKy")
               Next

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
            Catch ex As Exception
                tab2.Text = gdvData.GetFocusedRowCellValue("ttcMa")
            End Try

        End If
    End Sub


End Class
