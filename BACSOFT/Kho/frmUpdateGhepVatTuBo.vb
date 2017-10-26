Imports BACSOFT.Db.SqlHelper
Imports System.Linq

Public Class frmUpdateGhepVatTuBo

    Private idVatTu As Object = Nothing

    Public idGhepVatTuBo As Object

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        txtNgayCT.EditValue = GetServerTime()

        cmbTenVT.Properties.DataSource = ExecuteSQLDataTable("select Id,ten from tenvattu order by ten")
        cmHangSX.Properties.DataSource = ExecuteSQLDataTable("select ID,TEN from tenhangsanxuat order by TEN")
        cmbDVT.Properties.DataSource = ExecuteSQLDataTable("select ID,TEN from tendonvitinh order by TEN")

        cmbTenVT.EditValue = DBNull.Value
        cmHangSX.EditValue = DBNull.Value
        cmbDVT.EditValue = DBNull.Value
        cmbTenVatTu.EditValue = DBNull.Value

        LoadDsNhomVT()
        LoadDsTenVT()
        LoadDsHangSX()


        Dim sql As String = ""

        sql = "SELECT convert(bigint,0)Id_CT,dbo.tblGhepVatTu.ID, dbo.tblGhepVatTu.SoLuong, dbo.TENVATTU.ten AS TenVatTu, dbo.TENHANGSANXUAT.TEN AS HangSX, dbo.TENNHOM.Ten AS NhomVT, "
        sql &= "dbo.TENDONVITINH.TEN AS DVT, dbo.VATTU.Model, dbo.VATTU.Code, dbo.VATTU.Thongso,  dbo.tblGhepVatTu.IDVatTuPhu, convert(float,0) GiaNhap,convert(float,0)ThanhTien ,convert(float,0)TonThue, convert(float,0)TonKho "
        sql &= "FROM dbo.VATTU INNER JOIN "
        sql &= "dbo.TENVATTU ON dbo.VATTU.IDTenvattu = dbo.TENVATTU.ID INNER JOIN "
        sql &= "dbo.TENHANGSANXUAT ON dbo.VATTU.IDHangSanxuat = dbo.TENHANGSANXUAT.ID INNER JOIN "
        sql &= "dbo.TENNHOM ON dbo.VATTU.IDTennhom = dbo.TENNHOM.ID INNER JOIN "
        sql &= "dbo.TENDONVITINH ON dbo.VATTU.IDDonvitinh = dbo.TENDONVITINH.ID RIGHT OUTER JOIN "
        sql &= "dbo.tblGhepVatTu ON dbo.VATTU.ID = dbo.tblGhepVatTu.IDVatTu "

        If Not idVatTu Is Nothing Then
            sql &= "WHERE dbo.tblGhepVatTu.IDVatTu =  " & idVatTu
        Else
            sql &= "WHERE dbo.tblGhepVatTu.IDVatTu =  -1 "
        End If


        gdv.DataSource = ExecuteSQLDataTable(sql)

    End Sub

    Private Sub frmUpdateGhepVatTuBo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If TrangThai.isUpdate Then


            Try
                Me.Visible = False


                ShowWaiting("Đang tải dữ liệu, vui lòng đợi ...")

                Dim sql As String = "SELECT * FROM CHUNGTU WHERE ID = " & idGhepVatTuBo & "; "

                idCT = idGhepVatTuBo

                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)

                txtNgayCT.EditValue = dt.Rows(0)("NgayCT")
                txtDienGiaiChung.EditValue = dt.Rows(0)("DienGiai")


                'Lấy thông tin nhập kho
                sql = " SELECT ID FROM CHUNGTU WHERE refId = " & idGhepVatTuBo & " AND LoaiCT = "
                sql &= ChungTu.LoaiChungTu.HoaDonDauVao & " AND LoaiCT2 = " & ChungTu.LoaiCT2.NhapKho
                Dim dtIdNhapKho As DataTable = ExecuteSQLDataTable(sql)
                If dtIdNhapKho Is Nothing Then Throw New Exception(LoiNgoaiLe)
                idNK = dtIdNhapKho.Rows(0)(0)

                Application.DoEvents()

                sql = " SELECT * FROM CHUNGTUCHITIET WHERE Id_CT = " & idNK & " AND ButToan = 1 "
                Dim dtNhapKho As DataTable = ExecuteSQLDataTable(sql)
                If dtNhapKho Is Nothing Then Throw New Exception(LoiNgoaiLe)
                txtSoLuong.EditValue = dtNhapKho.Rows(0)("SoLuong")
                idVatTu = dtNhapKho.Rows(0)("IdVatTu")
                idNK_ChiTiet = dtNhapKho.Rows(0)("ID")


                sql = " SELECT * FROM VATTU WHERE ID = " & dtNhapKho.Rows(0)("IdVatTu")
                Dim dtVatTu As DataTable = ExecuteSQLDataTable(sql)
                If dtVatTu Is Nothing Then Throw New Exception(LoiNgoaiLe)

                Application.DoEvents()

                cmbTenVT.EditValue = dtVatTu.Rows(0)("IDTenvattu")
                cmbDVT.EditValue = dtVatTu.Rows(0)("IDDonvitinh")
                cmHangSX.EditValue = dtVatTu.Rows(0)("IDHangSanxuat")
                txtModel.EditValue = dtVatTu.Rows(0)("Model")


                'lấy thông tin xuất kho
                sql = " SELECT ID FROM CHUNGTU WHERE refId = " & idGhepVatTuBo & " AND LoaiCT = "
                sql &= ChungTu.LoaiChungTu.HoaDonDauRa & " AND LoaiCT2 = " & ChungTu.LoaiCT2.XuatKho
                Dim dtIdXuatKho As DataTable = ExecuteSQLDataTable(sql)
                idXK = dtIdXuatKho.Rows(0)(0)

                Application.DoEvents()

                sql = "  SELECT * FROM CHUNGTUCHITIET WHERE Id_CT = " & idXK & " AND ButToan = 1 "
                Dim dtXuatKho As DataTable = ExecuteSQLDataTable(sql)
                If dtXuatKho Is Nothing Then Throw New Exception(LoiNgoaiLe)


                Dim ngay As DateTime = Convert.ToDateTime(txtNgayCT.EditValue)
                ngay = New DateTime(ngay.Year, ngay.Month, ngay.Day)
                Dim nam As Integer = ngay.Year

                Application.DoEvents()

                For i As Integer = 0 To dtXuatKho.Rows.Count - 1

                    gdvData.AddNewRow()
                    gdvData.SetFocusedRowCellValue("IDVatTuPhu", dtXuatKho.Rows(i)("IdVatTu"))

                    sql = "SELECT TenHoaDon,Model,Code, "
                    sql &= "(SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu)TenVatTu, "
                    sql &= "(select ten from tendonvitinh where id = vattu.iddonvitinh)DVT,  "
                    sql &= "(select ten from tenhangsanxuat where id = vattu.idhangsanxuat)HangSX,  "
                    sql &= "(select ten from tennhom where id = vattu.idhangsanxuat)TenNhom,  "
                    sql &= "  "
                    'sql &= "isnull((SELECT TOP 1 b.DonGia FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT   "
                    'sql &= "WHERE b.IdVatTu = VATTU.ID AND b.ButToan = 3 ORDER BY a.NgayCT DESC),0)GiaNhap,   "
                    sql &= "  "
                    sql &= "(ISNULL((select DauKy from tonkhovattuthue where IdVatTu = VATTU.ID and Nam =  " & nam & " ),0)   "
                    sql &= " +   "
                    sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT   "
                    sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (2) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) =  " & nam & "   "
                    sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay1 "
                    sql &= "AND CHUNGTUCHITIET.ButToan = 1),0)   "
                    sql &= "-  "
                    sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT   "
                    sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (1) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) =  " & nam & "   "
                    sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay2 "
                    sql &= "AND CHUNGTUCHITIET.ButToan = 1),0)   "
                    sql &= ")TonThue,  "

                    sql &= "  "
                    sql &= "( ISNULL((SELECT SUM(NHAPKHO.SoLuong) FROM NHAPKHO RIGHT OUTER JOIN PHIEUNHAPKHO    "
                    sql &= "ON NHAPKHO.Sophieu = PHIEUNHAPKHO.Sophieu WHERE NHAPKHO.IdVattu = VATTU.ID "
                    sql &= "    AND  Convert(datetime,Convert(nvarchar,PHIEUNHAPKHO.Ngaythang,103),103) <= @DenNgay3 "
                    sql &= "),0)   "
                    sql &= "-   "
                    sql &= "ISNULL((SELECT SUM(XUATKHO.SoLuong) FROM XUATKHO RIGHT OUTER JOIN PHIEUXUATKHO    "
                    sql &= "ON XUATKHO.Sophieu = PHIEUXUATKHO.Sophieu WHERE XUATKHO.IdVattu = VATTU.ID AND PHIEUXUATKHO.Congtrinh = 0   "
                    sql &= "AND  Convert(datetime,Convert(nvarchar,PHIEUXUATKHO.Ngaythang,103),103) <= @DenNgay4 "
                    sql &= "),0)  "
                    sql &= ")TonKho   "
                    sql &= " FROM VATTU WHERE ID =   " & dtXuatKho.Rows(i)("IdVatTu")
                    sql &= "  "

                    AddParameter("@DenNgay1", ngay)
                    AddParameter("@DenNgay2", ngay)
                    AddParameter("@DenNgay3", ngay)
                    AddParameter("@DenNgay4", ngay)

                    Dim dtX As DataTable = ExecuteSQLDataTable(sql)

                    gdvData.SetFocusedRowCellValue("TenVatTu", dtX.Rows(0)("TenVatTu"))
                    gdvData.SetFocusedRowCellValue("Model", dtX.Rows(0)("Model"))
                    gdvData.SetFocusedRowCellValue("Code", dtX.Rows(0)("Code"))
                    gdvData.SetFocusedRowCellValue("SoLuong", dtXuatKho.Rows(i)("SoLuong"))
                    'gdvData.SetFocusedRowCellValue("GiaNhap", dtX.Rows(0)("GiaNhap"))
                    gdvData.SetFocusedRowCellValue("TonKho", dtX.Rows(0)("TonKho"))
                    gdvData.SetFocusedRowCellValue("TonThue", dtX.Rows(0)("TonThue"))
                    gdvData.SetFocusedRowCellValue("DVT", dtX.Rows(0)("DVT"))
                    gdvData.SetFocusedRowCellValue("HangSX", dtX.Rows(0)("HangSX"))
                    gdvData.SetFocusedRowCellValue("NhomVT", dtX.Rows(0)("TenNhom"))

                    gdvData.SetFocusedRowCellValue("ID", dtXuatKho.Rows(i)("Id"))
                    gdvData.SetFocusedRowCellValue("Id_CT", dtXuatKho.Rows(i)("Id_CT"))

                    gdvData.SetFocusedRowCellValue("GiaNhap", dtXuatKho.Rows(i)("DonGia"))
                    gdvData.SetFocusedRowCellValue("ThanhTien", dtXuatKho.Rows(i)("ThanhTien"))

                Next

            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            Finally
                Me.Visible = True
                CloseWaiting()
            End Try

        End If

    End Sub

    Private Sub cmbTenVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbTenVT.EditValueChanged
        LayTenHoaDon()
    End Sub

    Private Sub txtModel_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtModel.EditValueChanged
        LayTenHoaDon()
    End Sub

    Private Sub LayTenHoaDon()
        txtTenHoaDon.EditValue = cmbTenVT.Text & " " & txtModel.Text
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
        cmbTenVatTu.EditValue = DBNull.Value
    End Sub


    Private Sub rCmbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCmbHangSX.ButtonClick
        cmbHangSX.EditValue = DBNull.Value
    End Sub


    Private Sub btnLocVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLocVT.ItemClick

        Dim ngay As DateTime = Convert.ToDateTime(txtNgayCT.EditValue)
        ngay = New DateTime(ngay.Year, ngay.Month, ngay.Day)
        Dim nam As Integer = ngay.Year

        Dim sql As String = "SELECT ID, Model,Code, "
        sql &= "(SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu)TenVatTu, "
        sql &= "(SELECT Ten FROM TENNHOM WHERE ID = VATTU.IDTennhom)NhomVT, "
        sql &= "(SELECT Ten FROM TENHANGSANXUAT WHERE ID = VATTU.IDHangSanxuat)HangSX, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT, "
        'sql &= "convert(float,0) GiaNhap, convert(float,0)TonThue, convert(float,0)TonKho "

        sql &= " isnull((SELECT TOP 1 b.DonGia FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
        sql &= " WHERE b.IdVatTu = VATTU.ID AND b.ButToan = 3 ORDER BY a.NgayCT DESC),0)GiaNhap, "

        sql &= "(	 "
        sql &= "ISNULL((select DauKy from tonkhovattuthue where IdVatTu = VATTU.ID and Nam = " & nam & "),0) "
        sql &= "+  "
        sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT "
        sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (2) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) = " & nam & "  "
        sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay1 "
        sql &= "AND CHUNGTUCHITIET.ButToan = 1),0) "
        sql &= "-  "
        sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT "
        sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (1) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) = " & nam & " "
        sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay2 "
        sql &= "AND CHUNGTUCHITIET.ButToan = 1),0) "
        sql &= ")TonThue, "


        sql &= " "
        sql &= "( "
        sql &= "    ISNULL((SELECT SUM(NHAPKHO.SoLuong) FROM NHAPKHO RIGHT OUTER JOIN PHIEUNHAPKHO  "
        sql &= "    ON NHAPKHO.Sophieu = PHIEUNHAPKHO.Sophieu WHERE NHAPKHO.IdVattu = VATTU.ID "
        sql &= "    AND  Convert(datetime,Convert(nvarchar,PHIEUNHAPKHO.Ngaythang,103),103) <= @DenNgay3 "
        sql &= "),0)  "
        sql &= "    -  "
        sql &= "    ISNULL((SELECT SUM(XUATKHO.SoLuong) FROM XUATKHO RIGHT OUTER JOIN PHIEUXUATKHO  "
        sql &= "    ON XUATKHO.Sophieu = PHIEUXUATKHO.Sophieu WHERE XUATKHO.IdVattu = VATTU.ID AND PHIEUXUATKHO.Congtrinh = 0 "
        sql &= "    AND  Convert(datetime,Convert(nvarchar,PHIEUXUATKHO.Ngaythang,103),103) <= @DenNgay4 "
        sql &= "    ),0) "

        sql &= ")TonKho "



        'ChungTu.LoaiButToan.GiaVon()

        sql &= " FROM VATTU WHERE Model Like N'%" & txtMaVT.EditValue.ToString & "%' "
        If Not cmbNhomVT.EditValue Is DBNull.Value Then
            sql &= " AND IDTennhom = " & cmbNhomVT.EditValue & " "
        End If
        If Not cmbTenVatTu.EditValue Is DBNull.Value Then
            sql &= " AND IDTenvattu = " & cmbTenVatTu.EditValue & " "
        End If
        If Not cmbHangSX.EditValue Is DBNull.Value Then
            sql &= " AND IDHangSanxuat = " & cmbHangSX.EditValue & " "
        End If
        sql &= " ORDER BY TenVatTu, Model "


        AddParameter("@DenNgay1", ngay)
        AddParameter("@DenNgay2", ngay)
        AddParameter("@DenNgay3", ngay)
        AddParameter("@DenNgay4", ngay)

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then ShowBaoLoi(LoiNgoaiLe)
        gdvVT.DataSource = dt


    End Sub


    Private Sub gdvDataVT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDataVT.MouseDown
        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvDataVT.CalcHitInfo(e.Location)
        If calTest.InRowCell Then
            PopupMenu1.ShowPopup(gdvVT.PointToScreen(e.Location))
        End If
    End Sub


    Private Sub mnuDuaVaoLaVtPhu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuDuaVaoLaVtPhu.ItemClick


        If gdvDataVT.FocusedRowHandle < 0 Then Exit Sub
        gdvDataVT.CloseEditor()
        gdvData.UpdateCurrentRow()


        Dim idVT As Object = gdvDataVT.GetFocusedRowCellValue("ID")
        For i As Integer = 0 To gdvData.RowCount - 1
            If gdvData.GetRowCellValue(i, "IDVatTu") = idVT Then
                ShowCanhBao("Vật tư này đã tồn tại, vui lòng cập nhật lại số lượng!")
                Exit Sub
            End If
        Next


        'Cảnh báo tồn vật tư
        If gdvDataVT.GetFocusedRowCellValue("TonThue") <= 0 Then
            If Not ShowCauHoi("Vật tư này không còn tồn kho thuế, tiếp tục đưa vào ghép không?") Then Exit Sub
        End If

        gdvData.AddNewRow()
        gdvData.SetFocusedRowCellValue("ID", 0)
        gdvData.SetFocusedRowCellValue("Id_CT", 0)
        gdvData.SetFocusedRowCellValue("IDVatTuPhu", idVT)
        gdvData.SetFocusedRowCellValue("TenVatTu", gdvDataVT.GetFocusedRowCellValue("TenVatTu"))
        gdvData.SetFocusedRowCellValue("Model", gdvDataVT.GetFocusedRowCellValue("Model"))
        gdvData.SetFocusedRowCellValue("Code", gdvDataVT.GetFocusedRowCellValue("Code"))
        gdvData.SetFocusedRowCellValue("SoLuong", 1)
        gdvData.SetFocusedRowCellValue("GiaNhap", gdvDataVT.GetFocusedRowCellValue("GiaNhap"))
        gdvData.SetFocusedRowCellValue("TonKho", gdvDataVT.GetFocusedRowCellValue("TonKho"))
        gdvData.SetFocusedRowCellValue("TonThue", gdvDataVT.GetFocusedRowCellValue("TonThue"))
        gdvData.SetFocusedRowCellValue("DVT", gdvDataVT.GetFocusedRowCellValue("DVT"))
        gdvData.SetFocusedRowCellValue("HangSX", gdvDataVT.GetFocusedRowCellValue("HangSX"))
        gdvData.SetFocusedRowCellValue("NhomVT", gdvDataVT.GetFocusedRowCellValue("NhomVT"))

    End Sub


    Private Sub cmbTenVT_ProcessNewValue(sender As System.Object, e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs) Handles cmbTenVT.ProcessNewValue

        If e.DisplayValue.ToString.Trim <> "" Then
            e.Handled = True
            If Not ShowCauHoi(e.DisplayValue & " chưa có trong hệ thống!" & vbCrLf & "Bạn có muốn thêm tên gọi này vào hệ thống không?") Then Exit Sub

            AddParameter("@ten", e.DisplayValue)
            AddParameter("@Ten_ENG", e.DisplayValue)
            Dim id As Object = doInsert("TENVATTU")
            If id Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

            Dim dt As DataTable = CType(cmbTenVT.Properties.DataSource, DataTable)
            Dim r As DataRow = dt.NewRow
            r("id") = id
            r("ten") = e.DisplayValue
            dt.Rows.InsertAt(r, 0)
            cmbTenVT.EditValue = id
            ShowAlert("Đã thêm " & e.DisplayValue & " vào hệ thống!")
            txtTenHoaDon.Focus()
        End If

    End Sub


    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown

        Dim calTest As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        If e.Button <> System.Windows.Forms.MouseButtons.Right Then Exit Sub
        calTest = gdvData.CalcHitInfo(e.Location)
        If calTest.InRowCell Then
            PopupMenu2.ShowPopup(gdv.PointToScreen(e.Location))
        End If

    End Sub


    Private Sub mnuXoaKhoiDanhSach_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaKhoiDanhSach.ItemClick




        'If gdvData.FocusedRowHandle < 0 Then Exit Sub



        gdvData.CloseEditor()
        gdvData.UpdateCurrentRow()

        If gdvData.SelectedRowsCount = 0 Then Exit Sub

        If Not ShowCauHoi("Xóa " & gdvData.SelectedRowsCount & " dòng bạn vừa chọn?") Then Exit Sub

        Try
            For i As Integer = 0 To gdvData.GetSelectedRows().Length - 1
                AddParameter("@Id", gdvData.GetRowCellValue(gdvData.GetSelectedRows()(i), "ID"))
                If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next
            gdvData.DeleteSelectedRows()
        Catch ex As Exception

        End Try

        'Try
        '    If gdvData.GetFocusedRowCellValue("ID") Is DBNull.Value Then
        '        Dim index As Integer = gdvData.FocusedRowHandle
        '        gdvData.DeleteRow(index)
        '    Else
        '        If Not ShowCauHoi("Xóa vật tư " & gdvData.GetFocusedRowCellValue("TenVatTu") & " bạn vừa chọn?") Then Exit Sub
        '        Dim index As Integer = gdvData.FocusedRowHandle
        '        AddParameter("@Id", gdvData.GetFocusedRowCellValue("ID"))
        '        If doDelete("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
        '        gdvData.DeleteRow(index)
        '    End If
        'Catch ex As Exception
        '    ShowBaoLoi(ex.Message)
        'End Try

    End Sub



    Public idCT As Object = Nothing
    Public idNK As Object = Nothing
    Public idXK As Object = Nothing
    Public idNK_ChiTiet As Object = Nothing
    Public idCT_ChiTiet As Object = Nothing


    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click

        If cmbTenVT.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn tên vật tư!")
            cmbTenVT.Focus()
            Exit Sub
        End If
        If cmHangSX.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn hãng sản xuất!")
            cmHangSX.Focus()
            Exit Sub
        End If
        If cmbDVT.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn tên đơn vị tính!")
            cmbDVT.Focus()
            Exit Sub
        End If
        If txtTenHoaDon.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa nhập tên hóa đơn!")
            txtTenHoaDon.Focus()
            Exit Sub
        End If

        For i As Integer = 0 To gdvData.RowCount - 1
            If gdvData.GetRowCellValue(i, "SoLuong") > gdvData.GetRowCellValue(i, "TonThue") Then
                ShowCanhBao(gdvData.GetRowCellValue(i, "TenVatTu") & " không đủ lượng tồn kho thuế !")
                gdvData.FocusedRowHandle = i
                'Exit Sub
            End If
        Next

        Try

            BeginTransaction()
            'Lấy thông tin của vật tư chính
            AddParameter("@IDTenvattu", cmbTenVT.EditValue)
            AddParameter("@IDHangSanxuat", cmHangSX.EditValue)
            AddParameter("@Model", txtModel.EditValue)
            AddParameter("@IDDonvitinh", cmbDVT.EditValue)
            AddParameter("@TenHoaDon", txtTenHoaDon.EditValue)
            AddParameter("@Bo", 1)
            If idVatTu Is Nothing Then
                idVatTu = doInsert("VATTU")
                If idVatTu Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@Id", idVatTu)
                If doUpdate("VATTU", "ID=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            'Chèn thông tin vật tư phụ
            gdvData.CloseEditor()
            gdvData.UpdateCurrentRow()
            For i As Integer = 0 To gdvData.RowCount - 1
                AddParameter("@IDVatTu", idVatTu)
                AddParameter("@IDVatTuPhu", gdvData.GetRowCellValue(i, "IDVatTuPhu"))
                AddParameter("@SoLuong", gdvData.GetRowCellValue(i, "SoLuong"))
                If gdvData.GetRowCellValue(i, "ID") Is DBNull.Value Then
                    Dim idVTP As Object = doInsert("tblGhepVatTu")
                    If idVTP Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvData.SetRowCellValue(i, "ID", idVTP)
                Else
                    AddParameterWhere("@dk_ID", gdvData.GetRowCellValue(i, "ID"))
                    If doUpdate("tblGhepVatTu", "ID = @dk_ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            Next

            'Tạo chứng từ ghép vật tư
            AddParameter("@NgayCT", txtNgayCT.EditValue)
            AddParameter("@NgayHD", txtNgayCT.EditValue)
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.GhepVatTu)
            AddParameter("@DienGiai", txtDienGiaiChung.EditValue)
            AddParameter("@GhiSo", 0)
            AddParameter("@NguoiLap", TaiKhoan)
            AddParameter("@ThanhTien", 0)
            'AddParameter("@refId", idVatTu)
            If idCT Is Nothing Then
                idCT = doInsert("CHUNGTU")
                If idCT Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@Id", idCT)
                If doUpdate("CHUNGTU", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            AddParameter("@Id_CT", idCT)
            AddParameter("@DienGiai", txtDienGiaiChung.EditValue)
            AddParameter("@ThanhTien", 0)
            AddParameter("@ThanhTienQD", 0)
            AddParameter("@TaiKhoanNo", "")
            AddParameter("@TaiKhoanCo", "")
            AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
            If idCT_ChiTiet Is Nothing Then
                idCT_ChiTiet = doInsert("CHUNGTUCHITIET")
                If idCT_ChiTiet Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@Id", idCT_ChiTiet)
                If doUpdate("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            'Tạo chứng từ nhập kho
            AddParameter("@NgayCT", txtNgayCT.EditValue)
            AddParameter("@NgayHD", txtNgayCT.EditValue)
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
            AddParameter("@LoaiCT2", ChungTu.LoaiCT2.NhapKho)
            AddParameter("@DienGiai", txtDienGiaiChung.EditValue)
            AddParameter("@GhiSo", 0)
            AddParameter("@ThanhTien", 0)
            AddParameter("@TienThue", 0)
            AddParameter("@TongTien", 0)
            AddParameter("@refId", idCT)
            If idNK Is Nothing Then
                idNK = doInsert("CHUNGTU")
                If idNK Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@Id", idNK)
                If doUpdate("CHUNGTU", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
            AddParameter("@Id_CT", idNK)
            AddParameter("@IdVatTu", idVatTu)
            AddParameter("@DienGiai", txtTenHoaDon.EditValue)
            AddParameter("@DVT", cmbDVT.Text)
            AddParameter("@SoLuong", txtSoLuong.EditValue)
            AddParameter("@DonGia", 0)
            AddParameter("@ThanhTien", 0)
            AddParameter("@ThanhTienQD", 0)
            AddParameter("@TaiKhoanNo", "1561")
            AddParameter("@TaiKhoanCo", "154")
            AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
            If idNK_ChiTiet Is Nothing Then
                idNK_ChiTiet = doInsert("CHUNGTUCHITIET")
                If idNK_ChiTiet Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@Id", idNK_ChiTiet)
                If doUpdate("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            'Tạo chứng từ xuất kho
            AddParameter("@NgayCT", txtNgayCT.EditValue)
            AddParameter("@NgayHD", txtNgayCT.EditValue)
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauRa)
            AddParameter("@LoaiCT2", ChungTu.LoaiCT2.XuatKho)
            AddParameter("@DienGiai", txtDienGiaiChung.EditValue)
            AddParameter("@GhiSo", 0)
            AddParameter("@ThanhTien", 0)
            AddParameter("@TienThue", 0)
            AddParameter("@TongTien", 0)
            AddParameter("@refId", idCT)

            If idXK Is Nothing Then
                idXK = doInsert("CHUNGTU")
                If idXK Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@Id", idXK)
                If doUpdate("CHUNGTU", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            gdvData.CloseEditor()
            gdvData.UpdateCurrentRow()

            For i As Integer = 0 To gdvData.RowCount - 1

                AddParameter("@Id_CT", idXK)
                AddParameter("@IdVatTu", gdvData.GetRowCellValue(i, "IDVatTuPhu"))
                AddParameter("@DienGiai", gdvData.GetRowCellValue(i, "TenVatTu") & " " & gdvData.GetRowCellValue(i, "Model"))
                AddParameter("@DVT", gdvData.GetRowCellValue(i, "DVT"))
                AddParameter("@SoLuong", gdvData.GetRowCellValue(i, "SoLuong"))
                AddParameter("@DonGia", 0)
                AddParameter("@ThanhTien", 0)
                AddParameter("@ThanhTienQD", 0)
                AddParameter("@TaiKhoanNo", "154")
                AddParameter("@TaiKhoanCo", "1561")
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)

                'If gdvData.GetRowCellValue(i, "Id_CT") Is DBNull.Value OrElse gdvData.GetRowCellValue(i, "Id_CT") = 0 Then
                If gdvData.GetRowCellValue(i, "ID") Is DBNull.Value OrElse gdvData.GetRowCellValue(i, "ID") = 0 Then
                    Dim idXK_CT As Object = doInsert("CHUNGTUCHITIET")
                    If idXK_CT Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    'gdvData.SetRowCellValue(i, "Id_CT", idXK_CT)
                    gdvData.SetRowCellValue(i, "ID", idXK_CT)
                Else
                    AddParameterWhere("@Id", gdvData.GetRowCellValue(i, "ID"))
                    If doUpdate("CHUNGTUCHITIET", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If

            Next

            ComitTransaction()

            'Thêm vật mới vào danh sách
            If Not HoaDonGTGT.CacheData.dataVatTu Is Nothing Then
                If HoaDonGTGT.CacheData.dataVatTu.Table.Select("ID=" & idVatTu).Count = 0 Then
                    Dim r As DataRowView = HoaDonGTGT.CacheData.dataVatTu.AddNew
                    r("ID") = idVatTu
                    r("TenVatTu") = txtTenHoaDon.EditValue
                    r("DVT") = cmbDVT.Text
                    r("Ton") = 0
                    r("isCongCuDungCu") = 0
                    r("isTaiSanCoDinh") = 0
                    'HoaDonGTGT.CacheData.dataVatTu.EndInit()
                End If
            End If
            ShowAlert("Cập nhật dữ liệu thành công!")

        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub


    Private Sub gdvData_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvData.RowCellClick
        If e.RowHandle >= 0 And (e.Column.FieldName = "TonThue" Or e.Column.FieldName = "TonKho" Or e.Column.FieldName = "GiaNhap") Then

            Try
                Dim ngay As DateTime = Convert.ToDateTime(txtNgayCT.EditValue)
                ngay = New DateTime(ngay.Year, ngay.Month, ngay.Day)
                Dim nam As Integer = ngay.Year

                Dim sql As String = "SELECT "

                sql &= " isnull((SELECT TOP 1 b.DonGia FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
                sql &= " WHERE b.IdVatTu = VATTU.ID AND b.ButToan = 3 ORDER BY a.NgayCT DESC),0)GiaNhap, "

                sql &= "(	 "
                sql &= "ISNULL((select DauKy from tonkhovattuthue where IdVatTu = VATTU.ID and Nam = " & nam & "),0) "
                sql &= "+  "
                sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT "
                sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (2) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) = " & nam & "  "
                sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay1 "
                sql &= "AND CHUNGTUCHITIET.ButToan = 1),0) "
                sql &= "-  "
                sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT "
                sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (1) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) = " & nam & " "
                sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay2 "
                sql &= "AND CHUNGTUCHITIET.ButToan = 1),0) "
                sql &= ")TonThue, "


                sql &= " "
                sql &= "( "
                sql &= "    ISNULL((SELECT SUM(NHAPKHO.SoLuong) FROM NHAPKHO RIGHT OUTER JOIN PHIEUNHAPKHO  "
                sql &= "    ON NHAPKHO.Sophieu = PHIEUNHAPKHO.Sophieu WHERE NHAPKHO.IdVattu = VATTU.ID "
                sql &= "    AND  Convert(datetime,Convert(nvarchar,PHIEUNHAPKHO.Ngaythang,103),103) <= @DenNgay3 "
                sql &= "),0)  "
                sql &= "    -  "
                sql &= "    ISNULL((SELECT SUM(XUATKHO.SoLuong) FROM XUATKHO RIGHT OUTER JOIN PHIEUXUATKHO  "
                sql &= "    ON XUATKHO.Sophieu = PHIEUXUATKHO.Sophieu WHERE XUATKHO.IdVattu = VATTU.ID AND PHIEUXUATKHO.Congtrinh = 0 "
                sql &= "    AND  Convert(datetime,Convert(nvarchar,PHIEUXUATKHO.Ngaythang,103),103) <= @DenNgay4 "
                sql &= "    ),0) "

                sql &= ")TonKho "

                sql &= " FROM VATTU WHERE ID =  " & gdvData.GetRowCellValue(e.RowHandle, "IDVatTuPhu")

                AddParameter("@DenNgay1", ngay)
                AddParameter("@DenNgay2", ngay)
                AddParameter("@DenNgay3", ngay)
                AddParameter("@DenNgay4", ngay)

                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                If dt Is Nothing Then ShowBaoLoi(LoiNgoaiLe)

                ShowAlert(gdvData.GetRowCellValue(e.RowHandle, "TenVatTu") & " Tồn thuế: " & dt.Rows(0)("TonThue") & " - Tồn kho: " & dt.Rows(0)("TonKho") & " đến ngày " & ngay.ToString("dd/MM/yyyy"))
                gdvData.SetRowCellValue(e.RowHandle, "TonThue", dt.Rows(0)("TonThue"))
                gdvData.SetRowCellValue(e.RowHandle, "TonKho", dt.Rows(0)("TonKho"))
                gdvData.SetRowCellValue(e.RowHandle, "GiaNhap", dt.Rows(0)("GiaNhap"))

            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try


        End If
    End Sub



    Private Sub mnuXemTonKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXemTonKho.ItemClick

        gdvData.CloseEditor()
        gdvData.UpdateCurrentRow()

        Dim ngay As DateTime = Convert.ToDateTime(txtNgayCT.EditValue)
        ngay = New DateTime(ngay.Year, ngay.Month, ngay.Day)
        Dim nam As Integer = ngay.Year

        Try

            For i As Integer = 0 To gdvData.RowCount - 1
                Dim sql As String = "SELECT "

                sql &= " isnull((SELECT TOP 1 a.ThanhTien FROM CHUNGTU a LEFT OUTER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "
                sql &= " WHERE b.IdVatTu = VATTU.ID AND b.ButToan = 3 ORDER BY a.NgayCT DESC),0)GiaNhap, "

                sql &= "(	 "
                sql &= "ISNULL((select DauKy from tonkhovattuthue where IdVatTu = VATTU.ID and Nam = " & nam & "),0) "
                sql &= "+  "
                sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT "
                sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (2) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) = " & nam & "  "
                sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay1 "
                sql &= "AND CHUNGTUCHITIET.ButToan = 1),0) "
                sql &= "-  "
                sql &= "ISNULL((SELECT SUM(CHUNGTUCHITIET.SoLuong) FROM CHUNGTU LEFT OUTER JOIN CHUNGTUCHITIET ON CHUNGTU.Id = CHUNGTUCHITIET.Id_CT "
                sql &= "WHERE CHUNGTUCHITIET.IdVatTu = VATTU.ID AND CHUNGTU.LOAICT IN (1) AND CHUNGTU.GhiSo = 1 AND YEAR(CHUNGTU.NgayHD) = " & nam & " "
                sql &= "AND Convert(datetime,Convert(nvarchar,CHUNGTU.NgayHD,103),103) <= @DenNgay2 "
                sql &= "AND CHUNGTUCHITIET.ButToan = 1),0) "
                sql &= ")TonThue, "


                sql &= " "
                sql &= "( "
                sql &= "    ISNULL((SELECT SUM(NHAPKHO.SoLuong) FROM NHAPKHO RIGHT OUTER JOIN PHIEUNHAPKHO  "
                sql &= "    ON NHAPKHO.Sophieu = PHIEUNHAPKHO.Sophieu WHERE NHAPKHO.IdVattu = VATTU.ID "
                sql &= "    AND  Convert(datetime,Convert(nvarchar,PHIEUNHAPKHO.Ngaythang,103),103) <= @DenNgay3 "
                sql &= "),0)  "
                sql &= "    -  "
                sql &= "    ISNULL((SELECT SUM(XUATKHO.SoLuong) FROM XUATKHO RIGHT OUTER JOIN PHIEUXUATKHO  "
                sql &= "    ON XUATKHO.Sophieu = PHIEUXUATKHO.Sophieu WHERE XUATKHO.IdVattu = VATTU.ID AND PHIEUXUATKHO.Congtrinh = 0 "
                sql &= "    AND  Convert(datetime,Convert(nvarchar,PHIEUXUATKHO.Ngaythang,103),103) <= @DenNgay4 "
                sql &= "    ),0) "

                sql &= ")TonKho "

                sql &= " FROM VATTU WHERE ID =  " & gdvData.GetRowCellValue(i, "IDVatTuPhu")

                AddParameter("@DenNgay1", ngay)
                AddParameter("@DenNgay2", ngay)
                AddParameter("@DenNgay3", ngay)
                AddParameter("@DenNgay4", ngay)

                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                If dt Is Nothing Then ShowBaoLoi(LoiNgoaiLe)


                gdvData.SetRowCellValue(i, "TonThue", dt.Rows(0)("TonThue"))
                gdvData.SetRowCellValue(i, "TonKho", dt.Rows(0)("TonKho"))
                gdvData.SetRowCellValue(i, "GiaNhap", dt.Rows(0)("GiaNhap"))

            Next

            ShowAlert("Đã tính lại lượng tồn đến ngày " & ngay.ToString("dd/MM/yyyy"))

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try


    End Sub



    Private Sub mnuChonCacDongBangKhong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuChonCacDongBangKhong.ItemClick

        gdvData.CloseEditor()
        gdvData.UpdateCurrentRow()

        For i As Integer = 0 To gdvData.RowCount - 1
            If gdvData.GetRowCellValue(i, "TonThue") = 0 Then
                gdvData.SelectRow(i)
            End If
        Next

    End Sub

    Private Sub gdvData_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvData.CellValueChanged
        On Error Resume Next
        If e.Column.FieldName = "SoLuong" Or e.Column.FieldName = "GiaNhap" Then
            Dim thanhtien As Double = gdvData.GetRowCellValue(e.RowHandle, "GiaNhap") * gdvData.GetRowCellValue(e.RowHandle, "SoLuong")
            gdvData.SetRowCellValue(e.RowHandle, "ThanhTien", Math.Round(thanhtien, 0, MidpointRounding.AwayFromZero))
        End If
    End Sub


End Class