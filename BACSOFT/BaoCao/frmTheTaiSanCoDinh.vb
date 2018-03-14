Imports BACSOFT.Db.SqlHelper

Public Class frmTheTaiSanCoDinh



    Private Sub frmBaoCaoSoQuyTienGui_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        Select Case Me.Tag
            Case "SoQuyTienMat"
                Me.Text = "Báo cáo sổ quỹ tiền mặt"
            Case "SoQuyTienGui"
                Me.Text = "Báo cáo sổ quỹ tiền gửi"
        End Select

        txtNam.Value = GetServerTime.Year
        LoadTieuChiBaoCao()
        LoadDsTaiKhoan()


        LoadDsNganHang()

        cmbTieuChi.Focus()

    End Sub

    Private Sub LoadDsNganHang()
        Dim sql As String = "SELECT ID,MaSo,Ten FROM TAIKHOAN ORDER BY Ten"
        cmbNganHang.Properties.DataSource = ExecuteSQLDataTable(sql)
        cmbNganHang.EditValue = DBNull.Value
    End Sub

    Private Sub LoadTieuChiBaoCao()
        Dim obj As New TieuChiThoiGianCollection(txtNam.Value)
        cmbTieuChi.Properties.Items.Clear()
        For Each o As TieuChiThoiGian In obj.DsTieuChi
            cmbTieuChi.Properties.Items.Add(o)
        Next
        cmbTieuChi.EditValue = obj.DsTieuChi(0)
    End Sub

    Private Sub txtNam_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtNam.EditValueChanged
        LoadTieuChiBaoCao()
    End Sub

    Private Sub cmbTieuChi_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTieuChi.SelectedIndexChanged
        Dim obj = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian)
        If obj.HienThi = "Tùy chỉnh" Then
            txtTuNgay.Enabled = True
            txtDenNgay.Enabled = True
        Else
            txtTuNgay.Enabled = False
            txtDenNgay.Enabled = False
        End If
        txtTuNgay.EditValue = obj.TuNgay
        txtDenNgay.EditValue = obj.DenNgay
    End Sub


    Private Sub LoadDsTaiKhoan()
        Dim tb As DataTable = Nothing
        Select Case Me.Tag
            Case "SoQuyTienMat"
                tb = ExecuteSQLDataTable("SELECT convert(bit,0)Chon, TaiKhoan,TaiKhoanCha,TenGoi FROM TAIKHOANTHUE Where TaiKhoan like '111%' order by TaiKhoan")
            Case "SoQuyTienGui"
                tb = ExecuteSQLDataTable("SELECT convert(bit,0)Chon, TaiKhoan,TaiKhoanCha,TenGoi FROM TAIKHOANTHUE Where TaiKhoan like '112%' order by TaiKhoan")
        End Select

        Dim tb2 As DataTable = tb.Copy
        tb2.Rows.Clear()
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = tb.Rows(i)("TenGoi")
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), 1, tb2)
            End If
        Next
        gdv.DataSource = tb2
    End Sub

    Private Sub deQuy(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then Continue For
            If tb.Rows(i)("TaiKhoanCha") = idCha Then
                Dim strTen As String = ""
                For j As Integer = 0 To level - 1
                    strTen &= "-- "
                Next
                strTen = " " & strTen & tb.Rows(i)("TenGoi")
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = strTen
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), level + 1, tb2)
            End If
        Next
    End Sub

    Private Sub gdvData_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvData.CellValueChanged
        '    If e.Column.FieldName = "Chon" Then
        '        Dim tk As String = gdvData.GetRowCellValue(e.RowHandle, "TaiKhoan")
        '        Dim chon As Object = gdvData.GetRowCellValue(e.RowHandle, "Chon")
        '        For i As Integer = 0 To gdvData.RowCount - 1
        '            'If gdvData.GetRowCellValue(i, "TaiKhoanCha").ToString = tk Then
        '            '    gdvData.SetRowCellValue(i, "Chon", chon)
        '            'End If
        '            If gdvData.GetRowCellValue(i, "TaiKhoan").ToString <> tk Then
        '                gdvData.SetRowCellValue(i, "Chon", False)
        '            End If
        '        Next
        '    End If
    End Sub

    'Private Sub gdvData_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvData.RowCellClick
    '    If e.Column.FieldName = "Chon" Then

    '        Dim tk As String = gdvData.GetRowCellValue(e.RowHandle, "TaiKhoan")
    '        Dim chon As Object = gdvData.GetRowCellValue(e.RowHandle, "Chon")
    '        For i As Integer = 0 To gdvData.RowCount - 1
    '            If gdvData.GetRowCellValue(i, "TaiKhoanCha").ToString = TaiKhoan Then
    '                gdvData.SetRowCellValue(i, "Chon", chon)
    '            End If
    '        Next

    '        gdvData.CloseEditor()
    '        gdvData.UpdateCurrentRow()

    '    End If
    'End Sub

    Private Sub btInHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btInHoaDon.Click


        If radLoai.EditValue = 1 Then
            BaoCaoSoQuyTienGuiChiTiet()
        End If



    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub



    Private Sub BaoCaoSoQuyTienGuiChiTiet()

        Try

            Dim sotaikhoan As String = ""
            For i As Integer = 0 To gdvData.RowCount - 1
                If Not gdvData.GetRowCellValue(i, "Chon") Is DBNull.Value AndAlso gdvData.GetRowCellValue(i, "Chon") = True Then
                    sotaikhoan = gdvData.GetRowCellValue(i, "TaiKhoan")
                    Exit For
                End If
            Next

            If sotaikhoan = "" Then
                ShowCanhBao("Chưa chọn tài khoản!")
                Exit Sub
            End If

            ShowWaiting("Đang tải nội dung ...")

            Dim dkGhiSo As String = ""
            If chkGhiSo.Checked Then dkGhiSo = " AND GhiSo = 1 "

            Dim sql As String = "DECLARE @Nam as INT "
            sql &= "DECLARE @TaiKhoan as NVARCHAR(10) "
            sql &= "DECLARE @TuNgay as datetime "
            sql &= "DECLARE @DenNgay as datetime "
            sql &= "SET DATEFORMAT DMY "

            sql &= "SET @Nam = " & txtNam.Value & " "
            sql &= "SET @TaiKhoan = '" & sotaikhoan & "' "
            sql &= "SET @TuNgay = '" & CType(txtTuNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & "' "
            sql &= "SET @DenNgay = '" & CType(txtDenNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & "' "


            If cmbNganHang.Text = "" Then
                sql &= "SELECT "
                sql &= "("
                sql &= "    ISNULL((SELECT SUM(DuNo) FROM SODUNGANHANGDAUKY WHERE Nam = @Nam),0) "
                sql &= "    + "
                sql &= "    ISNULL((SELECT SUM(b.ThanhTien*a.TyGia) FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
                sql &= "    WHERE TaiKhoanNo Like @TaiKhoan + '%' AND convert(nvarchar,ngayct,103) < @TuNgay " & dkGhiSo & "),0) "
                sql &= "    - "
                sql &= "    ISNULL((SELECT SUM(b.ThanhTien*a.TyGia) FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
                sql &= "    WHERE TaiKhoanCo Like @TaiKhoan + '%' AND convert(nvarchar,ngayct,103) < @TuNgay " & dkGhiSo & "),0) "
                sql &= ") "

                sql &= "SELECT a.NgayCT as NgayGhiSo,  a.NgayCT as NgayChungTu, a.SoCT, (b.ThanhTien*a.TyGia)ThanhTien, b.DienGiai, b.TaiKhoanNo, b.TaiKhoanCo, "
                sql &= "convert(nvarchar,null)SoPhieuThu,convert(nvarchar,null)SoPhieuChi, "
                sql &= "convert(bigint,null)TienThu,convert(bigint,null)TienChi,convert(bigint,null)Ton,a.LoaiCT,a.LoaiCT2 "
                sql &= "FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
                sql &= "WHERE ((TaiKhoanCo Like @TaiKhoan + '%') OR (TaiKhoanNo Like @TaiKhoan + '%')) "
                sql &= "AND convert(nvarchar,ngayct,103) >= @TuNgay AND convert(nvarchar,ngayct,103) <= @DenNgay " & dkGhiSo & " ORDER BY a.NgayCT "
            Else
                sql &= "SELECT "
                sql &= "("
                sql &= "    ISNULL((SELECT SUM(DuNo) FROM SODUNGANHANGDAUKY WHERE MaSo = N'" & cmbNganHang.Text & "' AND Nam = @Nam),0) "
                sql &= "    + "
                sql &= "    ISNULL((SELECT SUM(b.ThanhTien*a.TyGia) FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
                sql &= "    WHERE TaiKhoanNo Like @TaiKhoan + '%' AND SoTkNganHang = N'" & cmbNganHang.Text & "' AND convert(nvarchar,ngayct,103) < @TuNgay " & dkGhiSo & "),0) "
                sql &= "    - "
                sql &= "    ISNULL((SELECT SUM(b.ThanhTien*a.TyGia) FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
                sql &= "    WHERE TaiKhoanCo Like @TaiKhoan + '%' AND SoTkNganHang = N'" & cmbNganHang.Text & "' AND convert(nvarchar,ngayct,103) < @TuNgay " & dkGhiSo & "),0) "
                sql &= ") "

                sql &= "SELECT a.NgayCT as NgayGhiSo,  a.NgayCT as NgayChungTu, a.SoCT, (b.ThanhTien*a.TyGia)ThanhTien, b.DienGiai, b.TaiKhoanNo, b.TaiKhoanCo, "
                sql &= "convert(nvarchar,null)SoPhieuThu,convert(nvarchar,null)SoPhieuChi, "
                sql &= "convert(bigint,null)TienThu,convert(bigint,null)TienChi,convert(bigint,null)Ton,a.LoaiCT,a.LoaiCT2 "
                sql &= "FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
                sql &= "WHERE ((TaiKhoanCo Like @TaiKhoan + '%') OR (TaiKhoanNo Like @TaiKhoan + '%')) "
                sql &= " AND a.SoTkNganHang = N'" & cmbNganHang.Text & "' AND convert(nvarchar,ngayct,103) >= @TuNgay AND convert(nvarchar,ngayct,103) <= @DenNgay " & dkGhiSo & " ORDER BY a.NgayCT "
            End If



            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            Dim dt As DataTable = ds.Tables(1)

            Dim tondauky As Long = ds.Tables(0).Rows(0)(0)


            Dim rTonDauKy As DataRow = dt.NewRow
            rTonDauKy("DienGiai") = " - Số tồn đầu kỳ"
            rTonDauKy("Ton") = tondauky
            dt.Rows.InsertAt(rTonDauKy, 0)

            For i As Integer = 1 To dt.Rows.Count - 1
                Dim r As DataRow = dt.Rows(i)
                'phieu thu
                If r("TaiKhoanNo").ToString.IndexOf(sotaikhoan) = 0 Then
                    r("SoPhieuThu") = ChungTu.TienToCT(r("LoaiCT"), r("LoaiCT2")) & r("SoCT")
                    r("TienThu") = r("ThanhTien")
                    tondauky = tondauky + r("ThanhTien")
                Else 'phieu chi
                    r("SoPhieuChi") = ChungTu.TienToCT(r("LoaiCT"), r("LoaiCT2")) & r("SoCT")
                    r("TienChi") = r("ThanhTien")
                    tondauky = tondauky - r("ThanhTien")
                End If
                r("Ton") = tondauky
            Next

            Dim f As New frmIn("Báo cáo sổ quỹ tiền gửi")
            Dim rpt As New rptSoQuyTienGui

            If CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThi = "Tùy chỉnh" Then
                rpt.Parameters("prmThoiGian").Value = "Từ ngày " & CType(txtTuNgay.EditValue, DateTime).ToString("dd/MM/yyyy") & " đến ngày " & CType(txtDenNgay.EditValue, DateTime).ToString("dd/MM/yyyy")
            Else
                rpt.Parameters("prmThoiGian").Value = CType(cmbTieuChi.SelectedItem, TieuChiThoiGian).HienThiBaoCao
            End If

            If cmbNganHang.Text = "" Then
                rpt.Parameters("prmTaiKhoan").Value = sotaikhoan & " - " & ExecuteSQLDataTable("select TenGoi from taikhoanthue where taikhoan = N'" & sotaikhoan & "'").Rows(0)(0)
            Else
                rpt.Parameters("prmTaiKhoan").Value = sotaikhoan & " - " & ExecuteSQLDataTable("select TenGoi from taikhoanthue where taikhoan = N'" & sotaikhoan & "'").Rows(0)(0)
                rpt.Parameters("prmTaiKhoan").Value &= ", " & cmbNganHang.Text & " - " & ExecuteSQLDataTable("select Ten from taikhoan where maso = N'" & cmbNganHang.Text & "'").Rows(0)(0)
            End If

            rpt.txtTongTon.Text = FormatNumber(tondauky, 0)

            rpt.DataSource = dt






            rpt.RequestParameters = False
            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem
            CloseWaiting()
            f.ShowDialog()

        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub cmbSoTK_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cmbNganHang.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbNganHang.EditValue = DBNull.Value
        End If
    End Sub
    Private Sub cmbSoTK_Properties_ProcessNewValue(sender As System.Object, e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs) Handles cmbNganHang.Properties.ProcessNewValue

    End Sub


End Class