Imports BACSOFT.Db.SqlHelper

Imports System.Xml



Public Class frmThue_DoiChieu2Kho


    Private Sub frmThue_BangNhapXuatTon_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim tg As DateTime = GetServerTime()
        txtDenNgay.EditValue = tg
        txtTuNgay.EditValue = tg.AddDays(-30)

        LoadDsNhomVT()
        LoadDsTenVT()
        LoadDsHangSX()

        rcmbMaVT.DataSource = ExecuteSQLDataTable("SELECT N'' as Model")



    End Sub

    Private Sub rcmbMaVT_ProcessNewValue(sender As System.Object, e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs) Handles rcmbMaVT.ProcessNewValue
        If e.DisplayValue.ToString.Trim <> "" Then
            e.Handled = True
            Dim dt As DataTable = CType(rcmbMaVT.DataSource, DataTable)
            Dim r As DataRow = dt.NewRow
            r("Model") = e.DisplayValue
            dt.Rows.InsertAt(r, 0)
            cmbMa.EditValue = e.DisplayValue
        End If
    End Sub


    Private Sub txtDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtDenNgay.EditValueChanged
        Dim tg As DateTime = txtDenNgay.EditValue
        txtTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub


    Private Sub LoadDsNhomVT()
        Dim sql As String = "SELECT ID,Ten FROM tennhom ORDER BY Ten ASC"
        rCmbNhomVT.DataSource = ExecuteSQLDataTable(sql)
        cmbNhomVT.EditValue = DBNull.Value
    End Sub

    Private Sub LoadDsTenVT()
        Dim sql As String = "SELECT ID,ten from tenvattu ORDER BY ten ASC"
        rCmbTenVT.DataSource = ExecuteSQLDataTable(sql)
        cmbTenVT.EditValue = DBNull.Value
    End Sub

    Private Sub LoadDsHangSX()
        Dim sql As String = "SELECT ID,TEN from tenhangsanxuat ORDER BY ten ASC"
        rCmbHangSX.DataSource = ExecuteSQLDataTable(sql)
        cmbHangSX.EditValue = DBNull.Value
    End Sub

    Private Sub rCmbNhomVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCmbNhomVT.ButtonClick
        cmbNhomVT.EditValue = DBNull.Value
    End Sub

    Private Sub rCmbTenVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCmbTenVT.ButtonClick
        cmbTenVT.EditValue = DBNull.Value
    End Sub

    Private Sub rCmbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCmbHangSX.ButtonClick
        cmbHangSX.EditValue = DBNull.Value
    End Sub

    Private Sub rcmbMaVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcmbMaVT.ButtonClick
        cmbMa.EditValue = DBNull.Value
    End Sub

    Private Sub LoadModelVT(sender As System.Object, e As System.EventArgs) Handles cmbNhomVT.EditValueChanged, cmbTenVT.EditValueChanged, cmbHangSX.EditValueChanged
        If cmbNhomVT.EditValue Is DBNull.Value And cmbTenVT.EditValue Is DBNull.Value And cmbHangSX.EditValue Is DBNull.Value Then
            rcmbMaVT.DataSource = Nothing
            Exit Sub
        End If
        Dim sql As String = "SELECT NULL as Model UNION ALL SELECT Model FROM VATTU WHERE 1=1 "
        If Not cmbNhomVT.EditValue Is DBNull.Value Then sql &= "AND IDTennhom = " & cmbNhomVT.EditValue & " "
        If Not cmbTenVT.EditValue Is DBNull.Value Then sql &= "AND IDTenvattu = " & cmbTenVT.EditValue & " "
        If Not cmbHangSX.EditValue Is DBNull.Value Then sql &= "AND IDHangSanxuat = " & cmbHangSX.EditValue & " "
        sql &= "ORDER BY Model "
        rcmbMaVT.DataSource = ExecuteSQLDataTable(sql)
    End Sub


    Private Sub btnLocVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLocVT.ItemClick


        Dim sql As String = txtSQL.EditValue

        sql = sql.Replace("{[@Nam]}", Convert.ToDateTime(txtTuNgay.EditValue).Year)
        sql = sql.Replace("{[@Thang]}", Convert.ToDateTime(txtTuNgay.EditValue).ToString("MM/yyyy"))
        sql = sql.Replace("{[@DenNgay]}", Convert.ToDateTime(txtDenNgay.EditValue).ToString("dd/MM/yyyy"))



        Dim strLoc2 As String = ""
        If Not cmbMa.EditValue Is Nothing Then
            strLoc2 &= "AND TenHoaDon Like N'%" & cmbMa.EditValue.ToString & "%' "
        End If
        If Not cmbNhomVT.EditValue Is DBNull.Value Then
            strLoc2 &= " AND IDTennhom = " & cmbNhomVT.EditValue & " "
        End If
        If Not cmbTenVT.EditValue Is DBNull.Value Then
            strLoc2 &= " AND IDTenvattu = " & cmbTenVT.EditValue & " "
        End If
        If Not cmbHangSX.EditValue Is DBNull.Value Then
            strLoc2 &= " AND IDHangSanxuat = " & cmbHangSX.EditValue & " "
        End If

        sql = sql.Replace("{[DK_LOC_1]}", strLoc2)
        If chkBoXuatKhoCT.Checked Then
            sql = sql.Replace("{[DK_LOC_2]}", " AND PHIEUXUATKHO.Congtrinh = 0 ")
        Else
            sql = sql.Replace("{[DK_LOC_2]}", "")
        End If


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


        'Lọc hóa đơn chỉ nhập xuất chưa lọc theo thời gian?????????

        gdv.DataSource = dt
        frmDoi.Close()

    End Sub

    Private Sub gdvData_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvData.RowCellStyle


    End Sub

    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        If IsNumeric(e.CellValue) AndAlso e.CellValue = 0 Then e.DisplayText = ""
    End Sub

    Private Sub btnLoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLoc.ItemClick
        If gdvData.FocusedColumn Is Nothing Then
            ShowCanhBao("Chọn cột cần lọc trước!")
            Exit Sub
        End If
        gdvData.ShowFilterEditor(gdvData.FocusedColumn)
    End Sub

    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown
        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvData.CalcHitInfo(e.Location)
        If calTest.InRowCell Then
            pMnuLichSu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mnuNhapXuatTrongNamThue_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuNhapXuatTrongNamThue.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmLichSuNhapXuatKhoThue
        f.Text = "Lịch sử nhập xuất kho " & gdvData.GetFocusedRowCellValue("TenHoaDon") & " năm " & Convert.ToDateTime(txtTuNgay.EditValue).Year
        f.idVatTu = gdvData.GetFocusedRowCellValue("ID")
        f.Nam = Convert.ToDateTime(txtTuNgay.EditValue).Year
        f.idChungTu = -1
        f.ShowDialog()
    End Sub

    Private Sub txtTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtTuNgay.EditValueChanged
        Dim tgDenNgay As DateTime = Convert.ToDateTime(txtDenNgay.EditValue)
        Dim tgTuNgay As DateTime = Convert.ToDateTime(txtTuNgay.EditValue)
        If tgTuNgay.Year < tgDenNgay.Year Then
            txtTuNgay.EditValue = New DateTime(tgDenNgay.Year, 1, 1)
        End If
    End Sub


    Private Sub txtSQL_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtSQL.EditValueChanged

    End Sub
End Class

