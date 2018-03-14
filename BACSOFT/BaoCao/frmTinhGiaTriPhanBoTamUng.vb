Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmTinhGiaTriPhanBoTamUng


    Private Sub frmTinhGiaTriPhanBoTamUng_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim tg As DateTime = GetServerTime()
        txtTuNgay.EditValue = New Date(tg.Year, tg.Month, 1)
        txtDenNgay.EditValue = New Date(tg.Year, tg.Month, DateTime.DaysInMonth(tg.Year, tg.Month))
        Dim dtHTTT As DataTable = ExecuteSQLDataTable("SELECT ID,SoTT,GiaiThich,Nhom,N''TenNhom FROM DM_HINH_THUC_TT where TrangThai=1 ORDER BY Nhom asc, SoTT, GiaiThich asc")
        If Not dtHTTT Is Nothing Then
            Dim dtNhom As DataTable = TAI.tableNhomHinhThucTT()
            For i As Integer = 0 To dtHTTT.Rows.Count - 1
                dtHTTT.Rows(i)("TenNhom") = dtNhom.Select("Id = " & dtHTTT.Rows(i)("Nhom"))(0)("TenNhom")
            Next
            cbHinhThucTT2.Properties.DataSource = dtHTTT
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick

        If Me.Tag = "TinhGiaTriPhanBoTamUngXuatKho" Then
            LoadDanhSachChaoGia()
        ElseIf Me.Tag = "TinhGiaTriPhanBoTamUngNhapKho" Then
            LoadDanhSachDatHang()
        End If

    End Sub


    Private Sub gdvChaoGia_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvChaoGia.RowCellClick
        If e.RowHandle >= 0 And e.Button = System.Windows.Forms.MouseButtons.Left Then
            Try
                gdvChaoGia.SetMasterRowExpanded(e.RowHandle, Not gdvChaoGia.GetMasterRowExpanded(e.RowHandle))
            Catch ex As Exception
            End Try

        End If
    End Sub


    Private Sub btnTinhPhanBo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTinhPhanBo.ItemClick

        If Me.Tag = "TinhGiaTriPhanBoTamUngXuatKho" Then
            TinhPhanBoXuatKho()
        ElseIf Me.Tag = "TinhGiaTriPhanBoTamUngNhapKho" Then
            TinhPhanBoNhapKho()
        End If

    End Sub


    Private Sub btnCapNhatGiaTri_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCapNhatGiaTri.ItemClick


        If Me.Tag = "TinhGiaTriPhanBoTamUngXuatKho" Then
            LuuPhanBoXuatKho()
        ElseIf Me.Tag = "TinhGiaTriPhanBoTamUngNhapKho" Then
            LuuPhanBoNhapKho()
        End If


    End Sub

    Private Sub LoadDanhSachChaoGia()

        Dim sql As String = ""

        sql &= "SELECT * FROM( "
        sql &= "SELECT ID,'CG' + SoPhieu as SoPhieu,NgayThang,IDKhachHang, (select ttcMa From khachhang where id = BANGCHAOGIA.IDKhachHang)MaDT,  "
        sql &= "(isnull(TienTruocThue,0)+isnull(TienThue,0) * TyGia) GiaTri, isnull(DaTamUng,0)DaTamUng,isnull(TamUngConLai,0)TamUngConLai, "
        sql &= " "
        sql &= "ltrim( "
        sql &= "    isnull((SELECT 'PT' + Sophieu + ' ' FROM THU WHERE MucDich = 100 AND PhieuTC0 = BANGCHAOGIA.SoPhieu  for XML path('')),'') "
        sql &= "    + ' ' +  "
        sql &= "    isnull((SELECT 'TNH' + Sophieu + ' ' FROM THUNH WHERE MucDich = 100 AND PhieuTC0 = BANGCHAOGIA.SoPhieu  for XML path('')),'') "
        sql &= ") "
        sql &= "PhieuTC, "
        sql &= ""
        sql &= " (SELECT GiaiThich + ' (' + convert(nvarchar,TraTruoc1) + '%,' + convert(nvarchar,TraTruoc2) + '% - ' + convert(nvarchar,TraSau1) + '%,' + convert(nvarchar,TraSau2) + ')' "
        sql &= " FROM DM_HINH_THUC_TT WHERE ID = BANGCHAOGIA.IDHinhThucTT2)HinhThucThanhToan, IDHinhThucTT2 as IdHTTT2,GhiChu "
        sql &= ""
        'sql &= "from BANGCHAOGIA WHERE SoPhieu IN (select SoPhieu FROM BANGCHAOGIA WHERE Convert(datetime,CONVERT(nvarchar,Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay) "
        sql &= "from BANGCHAOGIA WHERE Convert(datetime,CONVERT(nvarchar,Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= ")TBL WHERE PhieuTC <> ' ' "
        sql &= "ORDER BY NgayThang "
        sql &= ""



        sql &= "select ID,'XK' + SoPhieu as SoPhieu,NgayThang,'CG' + SoPhieuCG as SoPhieuTC,((TienTruocThue+TienThue)*TyGia)GiaTri,isnull(PhanBoTamUng,0)PhanBoTamUng,isnull(SoDuTamUng,0)SoDuTamUng from phieuxuatkho  "
        sql &= "WHERE SophieuCG IN ( "
        sql &= "    select SoPhieu FROM BANGCHAOGIA WHERE Convert(datetime,CONVERT(nvarchar,Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= "    and ((select count(id) from thu where mucdich = 100 and phieutc0 = phieuxuatkho.sophieucg) + (select count(id) from thunh where mucdich = 100 and phieutc0 = phieuxuatkho.sophieucg))  > 0 "
        sql &= ") "
        sql &= "ORDER BY NgayThang "
        sql &= ""


        AddParameter("@TuNgay", txtTuNgay.EditValue)
        AddParameter("@DenNgay", txtDenNgay.EditValue)

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If ds Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If


        Dim _relation As New System.Data.DataRelation("table2", ds.Tables(0).Columns("SoPhieu"), ds.Tables(1).Columns("SoPhieuTC"), False)

        ds.Relations.Add(_relation)

        ds.Tables(0).TableName = "table1"
        ds.Tables(1).TableName = "table2"

        gdv.DataMember = "table1"


        ds.Tables(0).Columns.Add(New DataColumn("DaTamUngX", Type.GetType("System.Double")))
        ds.Tables(0).Columns.Add(New DataColumn("TamUngConLaiX", Type.GetType("System.Double")))

        ds.Tables(1).Columns.Add(New DataColumn("PhanBoTamUngX", Type.GetType("System.Double")))
        ds.Tables(1).Columns.Add(New DataColumn("SoDuTamUngX", Type.GetType("System.Double")))

        gdv.DataSource = ds



        gdvChaoGia.Columns("DaTamUngX").Visible = False
        gdvChaoGia.Columns("TamUngConLaiX").Visible = False

        gdvXuatKho.Columns("PhanBoTamUngX").Visible = False
        gdvXuatKho.Columns("SoDuTamUngX").Visible = False


        'Dim info As GridViewInfo = gdvChaoGia.GetViewInfo
        'colSize.Width = info.ViewRects.ColumnPanelWidth - info.ViewRects.ColumnTotalWidth




    End Sub

    Private Sub TinhPhanBoXuatKho()
        Dim frmDoi As New DevExpress.XtraEditors.XtraForm
        frmDoi.StartPosition = FormStartPosition.CenterScreen
        frmDoi.FormBorderStyle = FormBorderStyle.None
        frmDoi.Width = 450
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

        Application.DoEvents()
        frmDoi.TopMost = True
        frmDoi.Show()
        frmDoi.TopMost = True

        Application.DoEvents()

        'Me.Enabled = False

        Try



            gdvChaoGia.Columns("DaTamUngX").Visible = True
            gdvChaoGia.Columns("TamUngConLaiX").Visible = True
            colSize.VisibleIndex = 9997
            colHinhThucThanhToan.VisibleIndex = 9998
            colGhiChu.VisibleIndex = 9999

            gdvXuatKho.Columns("PhanBoTamUngX").Visible = True
            gdvXuatKho.Columns("SoDuTamUngX").Visible = True
            'gdvChaoGia.BeginUpdate()



            Dim sql As String = ""

            For _index As Integer = 0 To gdvChaoGia.RowCount - 1
                Application.DoEvents()

                prc.Text = " Đang tính phẩn bổ xuất kho " & "(" & Math.Round(((_index + 1) / gdvChaoGia.RowCount) * 100, 0, MidpointRounding.AwayFromZero) & " %)"

                gdvChaoGia.FocusedRowHandle = _index
                Dim _SoCG As String = gdvChaoGia.GetRowCellValue(_index, "SoPhieu").ToString.Replace("CG", "")

                'Lấy tổng tiền tạm ứng cho chào giá
                'Mục đích 100 là bán hàng
                sql = "select "
                sql &= "(select isnull(sum(Sotien*TyGia),0) FROM THU where MucDich = 100 AND PhieuTC0 = @SoCG) "
                sql &= " + "
                sql &= "(select isnull(sum(sotien*TyGia),0) from THUNH WHERE MucDich = 100 AND PhieuTC0 = @SoCG) "
                AddParameter("@SoCG", _SoCG)
                Dim TienTamUng As Double = ExecuteSQLDataTable(sql).Rows(0)(0)
                gdvChaoGia.SetRowCellValue(_index, "DaTamUngX", TienTamUng)

                If gdvChaoGia.IsMasterRow(_index) And Not gdvChaoGia.IsMasterRowEmpty(_index) Then
                    gdvChaoGia.SetMasterRowExpanded(_index, True)
                    Dim detailView As GridView = gdvChaoGia.GetDetailView(_index, 0)

                    'Lấy mức phần trăm phân bổ
                    sql = "select TraTruoc1,TraTruoc2,TraSau1 FROM DM_HINH_THUC_TT WHERE ID = (select IDHinhThucTT2 from bangchaogia where SoPhieu = @SoCG) "
                    AddParameter("@SoCG", _SoCG)
                    Dim dtMucPhanBo As DataTable = ExecuteSQLDataTable(sql)
                    If dtMucPhanBo Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Dim TyLePhanBo As Double = 1

                    If dtMucPhanBo.Rows.Count > 0 Then
                        Dim r As DataRow = dtMucPhanBo.Rows(0)
                        If r("TraTruoc1") > 0 And r("TraTruoc2") > 0 And r("TraSau1") >= 0 Then
                            TyLePhanBo = (r("TraTruoc1") + r("TraTruoc2")) / 100.0
                        Else
                            TyLePhanBo = r("TraTruoc1") / 100.0
                        End If
                    End If

                    '********************************************
                    'Lấy mức phân bổ tối đa theo tiền tạm ứng
                    Dim _tienPhanBoMax As Double = TienTamUng
                    '********************************************

                    'Duyệt qua từng xuất kho
                    Dim SoDuTamUng As Double = TienTamUng
                    For i As Integer = 0 To detailView.RowCount - 1

                        Dim tienPhanBo As Double = detailView.GetRowCellValue(i, "GiaTri") * TyLePhanBo

                        '**********************************************
                        'Lấy mức phân bổ tối đa theo tiền tạm ứng
                        If tienPhanBo > _tienPhanBoMax Then
                            tienPhanBo = _tienPhanBoMax
                        End If
                        _tienPhanBoMax = _tienPhanBoMax - tienPhanBo
                        '********************************************

                        If TienTamUng <> 0 Then
                            'TienTamUng = TienTamUng - tienPhanBo

                            detailView.SetRowCellValue(i, "PhanBoTamUngX", tienPhanBo)

                            SoDuTamUng = SoDuTamUng - tienPhanBo

                            detailView.SetRowCellValue(i, "SoDuTamUngX", SoDuTamUng)
                            gdvChaoGia.SetRowCellValue(_index, "TamUngConLaiX", SoDuTamUng)

                        Else
                            detailView.SetRowCellValue(i, "PhanBoTamUngX", 0)
                            detailView.SetRowCellValue(i, "SoDuTamUngX", 0)
                            gdvChaoGia.SetRowCellValue(_index, "TamUngConLaiX", 0)
                        End If

                    Next

                Else

                    gdvChaoGia.SetRowCellValue(_index, "TamUngConLaiX", TienTamUng)

                End If


            Next

            'gdvChaoGia.EndUpdate()
            frmDoi.Close()
            ShowThongBao("Đã tính phân bổ xuất kho xong!")

        Catch ex As Exception
            'Me.Enabled = True
            frmDoi.Close()
            ShowBaoLoi(ex.Message)
        Finally
            'Me.Enabled = True
            frmDoi.Close()
        End Try
    End Sub

    Private Sub LuuPhanBoXuatKho()

        If Not ShowCauHoi("Lưu giá trị tính phân bổ xuất kho ?") Then Exit Sub

        Dim frmDoi As New DevExpress.XtraEditors.XtraForm
        frmDoi.StartPosition = FormStartPosition.CenterScreen
        frmDoi.FormBorderStyle = FormBorderStyle.None
        frmDoi.Width = 450
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

        Application.DoEvents()
        frmDoi.TopMost = True
        frmDoi.Show()
        frmDoi.TopMost = True

        Application.DoEvents()

        'Me.Enabled = False

        Try

            gdvChaoGia.Columns("DaTamUngX").Visible = True
            gdvChaoGia.Columns("TamUngConLaiX").Visible = True
            colSize.VisibleIndex = 9997
            colHinhThucThanhToan.VisibleIndex = 9998
            colGhiChu.VisibleIndex = 9999


            gdvXuatKho.Columns("PhanBoTamUngX").Visible = True
            gdvXuatKho.Columns("SoDuTamUngX").Visible = True
            'gdvChaoGia.BeginUpdate()


            Dim sql As String = ""

            'Cap nhat du lieu reset lai
            sql = "UPDATE BANGCHAOGIA SET DaTamUng = 0, TamUngConLai = 0 WHERE Convert(datetime,CONVERT(nvarchar,Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
            AddParameter("@TuNgay", txtTuNgay.EditValue)
            AddParameter("@DenNgay", txtDenNgay.EditValue)
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            sql = "UPDATE PHIEUXUATKHO SET PhanBoTamUng = 0, SoDuTamUng = 0 WHERE SoPhieuCG IN "
            sql &= "(SELECT Sophieu FROM BANGCHAOGIA WHERE Convert(datetime,CONVERT(nvarchar,Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay) "
            AddParameter("@TuNgay", txtTuNgay.EditValue)
            AddParameter("@DenNgay", txtDenNgay.EditValue)
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)

            For _index As Integer = 0 To gdvChaoGia.RowCount - 1

                Application.DoEvents()
                prc.Text = " Đang lưu phẩn bổ xuất kho " & "(" & Math.Round(((_index + 1) / gdvChaoGia.RowCount) * 100, 0, MidpointRounding.AwayFromZero) & " %)"
                gdvChaoGia.FocusedRowHandle = _index
                'Update chao gia
                AddParameter("@DaTamUng", gdvChaoGia.GetRowCellValue(_index, "DaTamUngX"))
                AddParameter("@TamUngConLai", gdvChaoGia.GetRowCellValue(_index, "TamUngConLaiX"))
                AddParameterWhere("@Sophieu", gdvChaoGia.GetRowCellValue(_index, "SoPhieu").ToString.Replace("CG", ""))
                If doUpdate("BANGCHAOGIA", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                'Update xuat kho
                If gdvChaoGia.IsMasterRow(_index) And Not gdvChaoGia.IsMasterRowEmpty(_index) Then
                    gdvChaoGia.SetMasterRowExpanded(_index, True)
                    Dim detailView As GridView = gdvChaoGia.GetDetailView(_index, 0)
                    For i As Integer = 0 To detailView.RowCount - 1
                        AddParameter("@PhanBoTamUng", detailView.GetRowCellValue(i, "PhanBoTamUngX"))
                        AddParameter("@SoDuTamUng", detailView.GetRowCellValue(i, "SoDuTamUngX"))
                        AddParameterWhere("@Sophieu", detailView.GetRowCellValue(i, "SoPhieu").ToString.Replace("XK", ""))
                        If doUpdate("PHIEUXUATKHO", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Next
                End If

            Next

            'gdvChaoGia.EndUpdate()
            frmDoi.Close()
            ShowThongBao("Đã lưu phân bổ xuất kho xong!")

            LoadDanhSachChaoGia()
        Catch ex As Exception
            'Me.Enabled = True
            frmDoi.Close()
            ShowBaoLoi(ex.Message)
        Finally
            'Me.Enabled = True
            frmDoi.Close()
        End Try
    End Sub


    Private Sub LoadDanhSachDatHang()

        Dim sql As String = ""

        sql &= "SELECT * FROM( "
        sql &= "SELECT ID,N'ĐH' + SoPhieu as SoPhieu,NgayDat as NgayThang,IDKhachHang, (select ttcMa From khachhang where id = PHIEUDATHANG.IDKhachHang)MaDT,  "
        sql &= "(isnull(TienTruocThue,0)+isnull(TienThue,0)) * TyGia GiaTri, isnull(DaTamUng,0)DaTamUng,isnull(TamUngConLai,0)TamUngConLai, "
        sql &= " "
        sql &= "ltrim( "
        sql &= "    isnull((SELECT 'PC' + Sophieu + ' ' FROM CHI WHERE MucDich = 210 AND PhieuTC0 = PHIEUDATHANG.SoPhieu  for XML path('')),'') "
        sql &= "    + ' ' +  "
        sql &= "    isnull((SELECT 'UNC' + Sophieu + ' ' FROM UNC WHERE MucDich = 210 AND PhieuTC0 = PHIEUDATHANG.SoPhieu  for XML path('')),'') "
        sql &= ") "
        sql &= "PhieuTC, "
        sql &= ""
        sql &= " (SELECT GiaiThich + ' (' + convert(nvarchar,TraTruoc1) + '%,' + convert(nvarchar,TraTruoc2) + '% - ' + convert(nvarchar,TraSau1) + '%,' + convert(nvarchar,TraSau2) + ')' "
        sql &= " FROM DM_HINH_THUC_TT WHERE ID = PHIEUDATHANG.IDHinhThucTT2)HinhThucThanhToan, IDHinhThucTT2 as IdHTTT2,GhiChu "
        sql &= ""
        'sql &= "from PHIEUDATHANG WHERE SoPhieu IN (select SoPhieu FROM PHIEUDATHANG WHERE Convert(datetime,CONVERT(nvarchar,NgayDat,103),103) BETWEEN @TuNgay AND @DenNgay) "
        sql &= "from PHIEUDATHANG WHERE Convert(datetime,CONVERT(nvarchar,NgayDat,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= ")TBL WHERE PhieuTC <> ' ' "
        sql &= "ORDER BY NgayThang "
        sql &= ""



        sql &= "select ID,'NK' + SoPhieu as SoPhieu,NgayThang,N'ĐH' + SoPhieuDH as SoPhieuTC,((TienTruocThue+TienThue)*TyGia)GiaTri,isnull(PhanBoTamUng,0)PhanBoTamUng,isnull(SoDuTamUng,0)SoDuTamUng from phieunhapkho  "
        sql &= "WHERE SoPhieuDH IN ( "
        sql &= "    select SoPhieu FROM PHIEUDATHANG WHERE Convert(datetime,CONVERT(nvarchar,NgayDat,103),103) BETWEEN @TuNgay AND @DenNgay "
        sql &= "    and ((select count(id) from chi where mucdich = 210 and phieutc0 = phieunhapkho.SoPhieuDH) + (select count(id) from unc where mucdich = 210 and phieutc0 = phieunhapkho.SoPhieuDH))  > 0 "
        sql &= ") "
        sql &= "ORDER BY NgayThang "
        sql &= ""


        AddParameter("@TuNgay", txtTuNgay.EditValue)
        AddParameter("@DenNgay", txtDenNgay.EditValue)

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If ds Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If


        Dim _relation As New System.Data.DataRelation("table2", ds.Tables(0).Columns("SoPhieu"), ds.Tables(1).Columns("SoPhieuTC"), False)

        ds.Relations.Add(_relation)

        ds.Tables(0).TableName = "table1"
        ds.Tables(1).TableName = "table2"

        gdv.DataMember = "table1"


        ds.Tables(0).Columns.Add(New DataColumn("DaTamUngX", Type.GetType("System.Double")))
        ds.Tables(0).Columns.Add(New DataColumn("TamUngConLaiX", Type.GetType("System.Double")))

        ds.Tables(1).Columns.Add(New DataColumn("PhanBoTamUngX", Type.GetType("System.Double")))
        ds.Tables(1).Columns.Add(New DataColumn("SoDuTamUngX", Type.GetType("System.Double")))

        gdv.DataSource = ds



        gdvChaoGia.Columns("DaTamUngX").Visible = False
        gdvChaoGia.Columns("TamUngConLaiX").Visible = False

        gdvXuatKho.Columns("PhanBoTamUngX").Visible = False
        gdvXuatKho.Columns("SoDuTamUngX").Visible = False


        'Dim info As GridViewInfo = gdvChaoGia.GetViewInfo
        'colSize.Width = info.ViewRects.ColumnPanelWidth - info.ViewRects.ColumnTotalWidth




    End Sub

    Private Sub TinhPhanBoNhapKho()

        Dim frmDoi As New DevExpress.XtraEditors.XtraForm
        frmDoi.StartPosition = FormStartPosition.CenterScreen
        frmDoi.FormBorderStyle = FormBorderStyle.None
        frmDoi.Width = 450
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

        Application.DoEvents()
        frmDoi.TopMost = True
        frmDoi.Show()
        frmDoi.TopMost = True

        Application.DoEvents()

        'Me.Enabled = False

        Try



            gdvChaoGia.Columns("DaTamUngX").Visible = True
            gdvChaoGia.Columns("TamUngConLaiX").Visible = True
            colSize.VisibleIndex = 9997
            colHinhThucThanhToan.VisibleIndex = 9998
            colGhiChu.VisibleIndex = 9999


            gdvXuatKho.Columns("PhanBoTamUngX").Visible = True
            gdvXuatKho.Columns("SoDuTamUngX").Visible = True
            'gdvChaoGia.BeginUpdate()



            Dim sql As String = ""

            For _index As Integer = 0 To gdvChaoGia.RowCount - 1
                Application.DoEvents()

                prc.Text = " Đang tính phẩn bổ nhập kho " & "(" & Math.Round(((_index + 1) / gdvChaoGia.RowCount) * 100, 0, MidpointRounding.AwayFromZero) & " %)"

                gdvChaoGia.FocusedRowHandle = _index
                Dim _SoDH As String = gdvChaoGia.GetRowCellValue(_index, "SoPhieu").ToString.Replace("ĐH", "")

                'Lấy tổng tiền tạm ứng cho chào giá
                'Mục đích 210 là mua hàng
                sql = "select "
                sql &= "(select isnull(sum(Sotien*TyGia),0) FROM CHI where MucDich = 210 AND PhieuTC0 = @SoDH) "
                sql &= " + "
                sql &= "(select isnull(sum(sotien*TyGia),0) from UNC WHERE MucDich = 210 AND PhieuTC0 = @SoDH) "
                AddParameter("@SoDH", _SoDH)
                Dim TienTamUng As Double = ExecuteSQLDataTable(sql).Rows(0)(0)
                gdvChaoGia.SetRowCellValue(_index, "DaTamUngX", TienTamUng)

                If gdvChaoGia.IsMasterRow(_index) And Not gdvChaoGia.IsMasterRowEmpty(_index) Then
                    gdvChaoGia.SetMasterRowExpanded(_index, True)
                    Dim detailView As GridView = gdvChaoGia.GetDetailView(_index, 0)

                    'Lấy mức phần trăm phân bổ
                    sql = "select TraTruoc1,TraTruoc2,TraSau1 FROM DM_HINH_THUC_TT WHERE ID = (select IDHinhThucTT2 from phieudathang where SoPhieu = @SoDH) "
                    AddParameter("@SoDH", _SoDH)
                    Dim dtMucPhanBo As DataTable = ExecuteSQLDataTable(sql)
                    If dtMucPhanBo Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Dim TyLePhanBo As Double = 1

                    If dtMucPhanBo.Rows.Count > 0 Then
                        Dim r As DataRow = dtMucPhanBo.Rows(0)
                        If r("TraTruoc1") > 0 And r("TraTruoc2") > 0 And r("TraSau1") >= 0 Then
                            TyLePhanBo = (r("TraTruoc1") + r("TraTruoc2")) / 100.0
                        Else
                            TyLePhanBo = r("TraTruoc1") / 100.0
                        End If
                    End If


                    '********************************************
                    'Lấy mức phân bổ tối đa theo tiền tạm ứng
                    Dim _tienPhanBoMax As Double = TienTamUng
                    '********************************************


                    'Duyệt qua từng nhập kho
                    Dim SoDuTamUng As Double = TienTamUng
                    For i As Integer = 0 To detailView.RowCount - 1

                        Dim tienPhanBo As Double = detailView.GetRowCellValue(i, "GiaTri") * TyLePhanBo

                        '**********************************************
                        'Lấy mức phân bổ tối đa theo tiền tạm ứng
                        If tienPhanBo > _tienPhanBoMax Then
                            tienPhanBo = _tienPhanBoMax
                        End If
                        _tienPhanBoMax = _tienPhanBoMax - tienPhanBo
                        '********************************************

                        If TienTamUng <> 0 Then
                            'TienTamUng = TienTamUng - tienPhanBo

                            detailView.SetRowCellValue(i, "PhanBoTamUngX", tienPhanBo)

                            SoDuTamUng = SoDuTamUng - tienPhanBo

                            detailView.SetRowCellValue(i, "SoDuTamUngX", SoDuTamUng)
                            gdvChaoGia.SetRowCellValue(_index, "TamUngConLaiX", SoDuTamUng)

                        Else
                            detailView.SetRowCellValue(i, "PhanBoTamUngX", 0)
                            detailView.SetRowCellValue(i, "SoDuTamUngX", 0)
                            gdvChaoGia.SetRowCellValue(_index, "TamUngConLaiX", 0)
                        End If

                    Next

                Else

                    gdvChaoGia.SetRowCellValue(_index, "TamUngConLaiX", TienTamUng)

                End If


            Next

            'gdvChaoGia.EndUpdate()
            frmDoi.Close()
            ShowThongBao("Đã tính phân bổ nhập kho xong!")

        Catch ex As Exception
            'Me.Enabled = True
            frmDoi.Close()
            ShowBaoLoi(ex.Message)
        Finally
            'Me.Enabled = True
            frmDoi.Close()
        End Try
    End Sub


    Private Sub LuuPhanBoNhapKho()

        If Not ShowCauHoi("Lưu giá trị tính phân bổ nhập kho ?") Then Exit Sub

        Dim frmDoi As New DevExpress.XtraEditors.XtraForm
        frmDoi.StartPosition = FormStartPosition.CenterScreen
        frmDoi.FormBorderStyle = FormBorderStyle.None
        frmDoi.Width = 450
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

        Application.DoEvents()
        frmDoi.TopMost = True
        frmDoi.Show()
        frmDoi.TopMost = True

        Application.DoEvents()

        'Me.Enabled = False

        Try

            gdvChaoGia.Columns("DaTamUngX").Visible = True
            gdvChaoGia.Columns("TamUngConLaiX").Visible = True
            colSize.VisibleIndex = 9997
            colHinhThucThanhToan.VisibleIndex = 9998
            colGhiChu.VisibleIndex = 9999


            gdvXuatKho.Columns("PhanBoTamUngX").Visible = True
            gdvXuatKho.Columns("SoDuTamUngX").Visible = True
            'gdvChaoGia.BeginUpdate()


            Dim sql As String = ""

            'Cap nhat du lieu reset lai
            sql = "UPDATE PHIEUDATHANG SET DaTamUng = 0, TamUngConLai = 0 WHERE Convert(datetime,CONVERT(nvarchar,Ngaydat,103),103) BETWEEN @TuNgay AND @DenNgay "
            AddParameter("@TuNgay", txtTuNgay.EditValue)
            AddParameter("@DenNgay", txtDenNgay.EditValue)
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
            sql = "UPDATE PHIEUNHAPKHO SET PhanBoTamUng = 0, SoDuTamUng = 0 WHERE SoPhieuDH IN "
            sql &= "(SELECT Sophieu FROM PHIEUDATHANG WHERE Convert(datetime,CONVERT(nvarchar,Ngaydat,103),103) BETWEEN @TuNgay AND @DenNgay) "
            AddParameter("@TuNgay", txtTuNgay.EditValue)
            AddParameter("@DenNgay", txtDenNgay.EditValue)
            If ExecuteSQLNonQuery(sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)


            For _index As Integer = 0 To gdvChaoGia.RowCount - 1

                Application.DoEvents()

                prc.Text = " Đang lưu phẩn bổ nhập kho " & "(" & Math.Round(((_index + 1) / gdvChaoGia.RowCount) * 100, 0, MidpointRounding.AwayFromZero) & " %)"

                gdvChaoGia.FocusedRowHandle = _index

                'Update đặt hàng
                AddParameter("@DaTamUng", gdvChaoGia.GetRowCellValue(_index, "DaTamUngX"))
                AddParameter("@TamUngConLai", gdvChaoGia.GetRowCellValue(_index, "TamUngConLaiX"))
                AddParameterWhere("@Sophieu", gdvChaoGia.GetRowCellValue(_index, "SoPhieu").ToString.Replace("ĐH", ""))
                If doUpdate("PHIEUDATHANG", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                'Update nhập kho
                If gdvChaoGia.IsMasterRow(_index) And Not gdvChaoGia.IsMasterRowEmpty(_index) Then
                    gdvChaoGia.SetMasterRowExpanded(_index, True)
                    Dim detailView As GridView = gdvChaoGia.GetDetailView(_index, 0)
                    For i As Integer = 0 To detailView.RowCount - 1
                        AddParameter("@PhanBoTamUng", detailView.GetRowCellValue(i, "PhanBoTamUngX"))
                        AddParameter("@SoDuTamUng", detailView.GetRowCellValue(i, "SoDuTamUngX"))
                        AddParameterWhere("@Sophieu", detailView.GetRowCellValue(i, "SoPhieu").ToString.Replace("NK", ""))
                        If doUpdate("PHIEUNHAPKHO", "Sophieu = @Sophieu") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Next
                End If

            Next

            'gdvChaoGia.EndUpdate()
            frmDoi.Close()
            ShowThongBao("Đã lưu phân bổ nhập kho xong!")

            LoadDanhSachDatHang()
        Catch ex As Exception
            'Me.Enabled = True
            frmDoi.Close()
            ShowBaoLoi(ex.Message)
        Finally
            'Me.Enabled = True
            frmDoi.Close()
        End Try
    End Sub

    'Private Sub gdv_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles gdv.Paint
    '    Dim viewInfo As GridViewInfo = CType(gdvChaoGia.GetViewInfo(), GridViewInfo)
    '    Dim handl As Integer = gdvChaoGia.FocusedRowHandle

    '    Dim _Top As Integer = handl

    '    Dim rowInfo2 As GridRowInfo = viewInfo.GetGridRowInfo(_Top)
    '    If rowInfo2 Is Nothing Then Exit Sub
    '    Dim rect2 As System.Drawing.Rectangle = rowInfo2.DataBounds
    '    Dim pen2 As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.Red, 2)
    '    e.Graphics.DrawLine(pen2, rect2.X, rect2.Y, rect2.X + rect2.Width, rect2.Y)


    '    Dim _Bottom As Integer = handl + 1

    '    If _Bottom <= gdvChaoGia.RowCount - 1 Then

    '        Dim rowInfo3 As GridRowInfo = viewInfo.GetGridRowInfo(_Bottom)
    '        If rowInfo3 Is Nothing Then Exit Sub
    '        Dim rect3 As System.Drawing.Rectangle = rowInfo3.DataBounds
    '        Dim pen3 As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.Red, 2)
    '        e.Graphics.DrawLine(pen3, rect3.X, rect3.Y, rect3.X + rect3.Width, rect3.Y)

    '    End If

    'End Sub


    'Private Sub gdvChaoGia_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvChaoGia.FocusedRowChanged
    '    gdvChaoGia.Invalidate()
    'End Sub

    'Private Sub gdvChaoGia_LeftCoordChanged(sender As System.Object, e As System.EventArgs) Handles gdvChaoGia.LeftCoordChanged
    '    gdvChaoGia.Invalidate()
    'End Sub

    Private Sub btCNDuLieuChung_Click(sender As System.Object, e As System.EventArgs) Handles btCNDuLieuChung.Click

        If Not ShowCauHoi("Cập nhật hình thức thanh toán mới cho " & lblSoCG.Text & " ?") Then
            groupHTT.Visible = True
            Exit Sub
        End If

        If Me.Tag = "TinhGiaTriPhanBoTamUngXuatKho" Then

            AddParameter("@IDHinhThucTT2", cbHinhThucTT2.EditValue)
            AddParameter("@GhiChu", txtGhiChu.Text)
            AddParameterWhere("@Sophieu", lblSoCG.Tag.ToString)
            If Not doUpdate("BANGCHAOGIA", "SoPhieu=@SoPhieu") Is Nothing Then
                'Tính phân bổ lại
                '************* PHAN BO TAM UNG ************************'
                frmCNXuatKho.CapNhatPhanBoTamUng(lblSoCG.Tag.ToString)
                ShowAlert("Đã cập nhật hình thức thanh toán mới cho " & lblSoCG.Text & " .")
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        ElseIf Me.Tag = "TinhGiaTriPhanBoTamUngNhapKho" Then



            AddParameter("@IDHinhThucTT2", cbHinhThucTT2.EditValue)
            AddParameter("@GhiChu", txtGhiChu.Text)
            AddParameterWhere("@Sophieu", lblSoCG.Tag.ToString)
            If Not doUpdate("PHIEUDATHANG", "SoPhieu=@SoPhieu") Is Nothing Then
                'Tính phân bổ lại
                '************* PHAN BO TAM UNG ************************'
                frmCNNhapKho.CapNhatPhanBoTamUng(lblSoCG.Tag.ToString)
                ShowAlert("Đã cập nhật hình thức thanh toán mới cho " & lblSoCG.Text & " .")
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If



        groupHTT.Visible = False

        btnTaiDuLieu.PerformClick()

    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        groupHTT.Visible = False
    End Sub

    Private Sub btnDoiHinhThucThanhToan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDoiHinhThucThanhToan.ItemClick
        If gdvChaoGia.FocusedRowHandle < 0 Then Exit Sub
        cbHinhThucTT2.EditValue = gdvChaoGia.GetFocusedRowCellValue("IdHTTT2")
        txtGhiChu.Text = gdvChaoGia.GetFocusedRowCellValue("GhiChu").ToString
        If Me.Tag = "TinhGiaTriPhanBoTamUngXuatKho" Then
            lblSoCG.Tag = gdvChaoGia.GetFocusedRowCellValue("SoPhieu").ToString.Replace("CG", "")
        ElseIf Me.Tag = "TinhGiaTriPhanBoTamUngNhapKho" Then
            lblSoCG.Tag = gdvChaoGia.GetFocusedRowCellValue("SoPhieu").ToString.Replace("ĐH", "")
        End If

        lblSoCG.Text = gdvChaoGia.GetFocusedRowCellValue("SoPhieu").ToString
        groupHTT.Visible = True
    End Sub

    Private Sub groupHTT_VisibleChanged(sender As System.Object, e As System.EventArgs) Handles groupHTT.VisibleChanged
        If groupHTT.Visible Then
            gdv.Enabled = False
        Else
            gdv.Enabled = True
        End If
    End Sub


End Class
