Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress.XtraEditors.Repository
Public Class frmThongSo
    Public _Exit As Boolean = False

    Private emptyEditor As RepositoryItemButtonEdit


    Private Sub frmThongSo_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        gdvCT.Columns("colSuDung").Visible = False
        gdvCT.Columns("colXoa").Visible = False

        'gdvCT.Columns("Model2").Visible = True

        emptyEditor = New RepositoryItemButtonEdit()
        emptyEditor.Buttons.Clear()
        emptyEditor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        gdv.RepositoryItems.Add(emptyEditor)

        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        LoadDSTuDien()
        loadDSLanDau()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Or Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then
            For i As Integer = 0 To gdvCT.Columns.Count - 1
                If gdvCT.Columns(i).FieldName <> "TaiLieu" Then
                    gdvCT.Columns(i).OptionsColumn.ReadOnly = True
                End If
            Next
        End If
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            colGDonGia.Visible = False
            colGGiaBan.Visible = False
            colGGiaNCC.Visible = False
            colGGiaNhap.Visible = False
            colGMucThue.Visible = False
            colGTienTe.Visible = False
            colGXuatThue.Visible = False
            colGGiaNKBAC.Visible = False
            ColGPTLNBB.Visible = False
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then

            colGGiaNK.Visible = False
            colGKhoiLuong.Visible = False
            colGMOQ1.Visible = False
            colGMOQ2.Visible = False
            colGMOQ3.Visible = False
            colGNK.Visible = False
            colGSLMOQ1.Visible = False
            colGSLMOQ2.Visible = False
            colGSLMOQ3.Visible = False
            colGTienTeNK.Visible = False
            colGThueNK.Visible = False
            colGVCQT.Visible = False
            colGVCTN.Visible = False
            colCode.Visible = False
            btXoaChuaSuDung.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

        tbHeSoGiaList.EditValue = 1.065 / 0.65
        gdvCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
    End Sub

#Region "Lọc vật tư"

    Public Sub LoadDSTuDien()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten,Ten_ENG FROM TENNHOM ORDER BY Ten"
        sql &= " SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        sql &= " SELECT ID,Ten FROM TENNUOC ORDER BY Ten"
        sql &= " SELECT ID,Ten FROm TENDONVITINH ORDER BY Ten"
        sql &= " SELECT ID,Ten FROm TENVATTU ORDER BY Ten"
        sql &= " SELECT ID,Ten FROm tblTienTe ORDER BY ID"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbNhom.DataSource = ds.Tables(0)
            cbCNNhomVT.Properties.DataSource = ds.Tables(0)
            rcbHang.DataSource = ds.Tables(1)
            rcbNuoc.DataSource = ds.Tables(2)
            cbXuatXu.Properties.DataSource = ds.Tables(2)
            If ds.Tables(2).Rows.Count > 0 Then
                cbXuatXu.EditValue = ds.Tables(2).Rows(0)(0)
            End If

            rcbDVT.DataSource = ds.Tables(3)
            cbDVT.Properties.DataSource = ds.Tables(3)
            rcbTen.DataSource = ds.Tables(4)
            cbCNTenVT.Properties.DataSource = ds.Tables(4)
            rcbTienTe.DataSource = ds.Tables(5)
            cbTienTe.Properties.DataSource = ds.Tables(5)
            If ds.Tables(5).Rows.Count > 0 Then
                cbTienTe.EditValue = ds.Tables(5).Rows(0)(0)
            End If
            cbTienTeNK.Properties.DataSource = ds.Tables(5)
            If ds.Tables(5).Rows.Count > 0 Then
                cbTienTeNK.EditValue = ds.Tables(5).Rows(1)(0)
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub loadDSTenVT(ByVal HangSX As Object, ByVal NhomVT As Object)
        Dim sqltb As String = ""

        Dim sql As String = ""
        If HangSX Is Nothing And NhomVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENVATTU ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTenvattu FROM TENVATTU WHERE ID=-1 "

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDTenvattu FROM VATTU WHERE IDTennhom=" & NhomVT
            End If

            sql = " SELECT ID,Ten FROM TENVATTU WHERE ID IN (SELECT DISTINCT IDTenvattu FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbTenVatTu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNhomVT(ByVal HangSX As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If HangSX Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENNHOM ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 ID AS IDTennhom FROM TENNHOM WHERE ID=-1"

            If Not HangSX Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDHangSanxuat=" & HangSX
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL"
                sqltb &= " SELECT IDTennhom FROM VATTU WHERE IDTenvattu=" & TenVT
            End If

            sql = " SELECT ID,Ten FROM TENNHOM WHERE ID IN (SELECT DISTINCT IDTennhom FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            rcbNhomVT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadcbHangSX(ByVal NhomVT As Object, ByVal TenVT As Object)
        Dim sqltb As String = ""
        Dim sql As String = ""
        If NhomVT Is Nothing And TenVT Is Nothing Then
            sql = "SELECT ID,Ten FROM TENHANGSANXUAT ORDER BY Ten"
        Else
            sqltb &= " SELECT TOP 1 IDHangSanxuat FROM VATTU WHERE ID=-1"

            If Not NhomVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTennhom=" & NhomVT
            End If


            If Not TenVT Is Nothing Then
                sqltb &= " UNION ALL "
                sqltb &= " SELECT IDHangSanxuat FROM VATTU WHERE IDTenvattu=" & TenVT
            End If
            sql = " SELECT ID,Ten FROM TENHANGSANXUAT WHERE ID IN (SELECT DISTINCT IDHangSanxuat FROM (" & sqltb & " )tb)"
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbHangSX.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub cbTenVatTu_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTenVatTu.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTenVT.EditValue = Nothing
            loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        End If
    End Sub

    Private Sub rcbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbHangSX.ButtonClick
        If e.Button.Index = 1 Then
            btFilterHangSX.EditValue = Nothing
            LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub rtbMaVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbMaVT.ButtonClick
        btFilterMaVT.EditValue = Nothing
    End Sub

    Private Sub rtbThongSo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbThongSo.ButtonClick
        btFilterThongSo.EditValue = Nothing
    End Sub

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub btfilterTenVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btfilterTenVT.EditValueChanged
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        If chkTuDong.Checked Then LoadDS()
    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        If chkTuDong.Checked Then LoadDS()
    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        If chkTuDong.Checked Then LoadDS()
    End Sub

#End Region

    Private Sub LoadDS()
        ShowWaiting("Đang tải dữ liệu ...")

        Dim sqlWhere As String = ""
        If Not chkLocMaTrung.Checked Then
            sqlWhere = " WHERE 1=1 "
        End If

        Dim sql As String = ""

        If chkLocMaTrung.Checked Then
            sql &= " Select (CASE WHEN tbNX.IDVatTu is  NULL THEN convert(bit,0) ELSE convert(bit,1) END)NX,Code,Convert(bit,0)Trung, VATTU.ID,IDTenVatTu,IDHangSanXuat,Model,IDDonViTinh,ThongSo,IDTenNuoc,IDTenTaiLieu,ConSX,MaLoi,DMTon,DatToiThieu,IDTenNhom,ThongDung,Ton,Ngay,ThongSo_ENG,HangTon,(convert(image,NULL))HienThi,HinhAnh,TENNHOM.Ten_ENG AS TenNhom_ENG,TENNHOM.Ten as TenNhom_VIE, TENHANGSANXUAT.Ten AS TenHang,TaiLieu,(Convert(bit,0)) AS Modify,GhiChu,ThayDoi, "
            sql &= " DonGia1,TienTe1,GiaNhap1,MucThue1,GiaBan1,GiaNCC1,XuatThue1,TNK1,KhoiLuong1,VCQT,VCTN,GiaMOQ1,GiaMOQ2,GiaMOQ3,SLMOQ1,SLMOQ2,SLMOQ3,NK,GiaNK,GiaNKBAC,TienTeNK,TonNCC,"
            sql &= " 	UPPER(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Model,char(13),''),char(10),''),' ',''),'_',''),'-',''),'.',''),',',''),'\',''),'/',''),'+',''),'x',''))Model2, "
            sql &= " (CASE ISNULL(GiaNKBAC,0) WHEN 0 Then 0 ELSE round((((ISNULL(GiaNCC1,0)/100 * ISNULL(DonGia1,0))-GiaNKBAC)/GiaNKBAC)*100,2) END) AS PTLNBB"
            sql &= " into #tb"
            sql &= " FROM VATTU"
            sql &= " Left Join"
            sql &= " (SELECT DISTINCT IDVatTu FROM"
            sql &= " (SELECT  IDVatTu FROM NHAPKHO "
            sql &= "    UNION ALL"
            sql &= " SELECT  IDVatTu FROM XUATKHO)tb)tbNX ON tbNX.IDVatTu=VATTU.ID"
            sql &= " LEFT JOIN TENNHOM ON TENNHOM.ID= VATTU.IDTennhom"
            sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangsanxuat "
            sql &= " WHERE 1=1 "
            If btFilterMaVT.EditValue <> "" Then
                sql &= " AND VATTU.Model LIKE N'" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sql &= " AND #tb.Thongso like N'%" & btFilterThongSo.EditValue.ToString & "%' "
            End If


            sql &= " SELECT convert(bit,0) colXoa, "
            sql &= " convert(bit,(CASE WHEN "
            sql &= " (SELECT count(IDVatTu) FROM YEUCAUDEN WHERE IdVatTu = #tb.ID) + "
            sql &= " (SELECT count(IDVatTu) FROM CHAOGIA WHERE IdVatTu = #tb.ID) + "
            sql &= " (SELECT count(IDVatTu) FROM DATHANG WHERE IdVatTu = #tb.ID) + "
            sql &= " (SELECT count(IDVatTu) FROM NHAPKHO WHERE IdVatTu = #tb.ID) + "
            sql &= " (SELECT count(IDVatTu) FROM XUATKHO WHERE IdVatTu = #tb.ID) "
            sql &= " <> 0 THEN 1 ELSE 0 END)) colSuDung "
            sql &= " ,* FROM #tb "



            sql &= " WHERE Model2 IN ("
            sql &= " SELECT Model2 FROM #tb where ID not in("
            sql &= " SELECT"
            sql &= " 	ID"
            sql &= " FROM"
            sql &= " ("
            sql &= " 	SELECT"
            sql &= " 		*,ROW_NUMBER() OVER (PARTITION BY Model2 ORDER BY ID) AS RN"
            sql &= " 	FROM"
            sql &= " 		#tb"
            sql &= " )tbTmp"
            sql &= " WHERE RN = 1)) "




            sqlWhere &= " ORDER BY Model2,colSuDung desc,ID,IDTennhom,IDTenvattu,IDHangsanxuat DROP table #tb "
        Else
            sql &= " SELECT (CASE WHEN tbNX.IDVatTu is  NULL THEN convert(bit,0) ELSE convert(bit,1) END)NX,Code,Convert(bit,0)Trung, VATTU.ID,IDTenVatTu,IDHangSanXuat,Model,Model AS Model2,IDDonViTinh,ThongSo,IDTenNuoc,IDTenTaiLieu,ConSX,MaLoi,DMTon,DatToiThieu,IDTenNhom,ThongDung,Ton,Ngay,ThongSo_ENG,HangTon,(convert(image,NULL))HienThi,HinhAnh,TENNHOM.Ten_ENG AS TenNhom_ENG,TENNHOM.Ten as TenNhom_VIE, TENHANGSANXUAT.Ten AS TenHang,TaiLieu,(Convert(bit,0)) AS Modify,GhiChu,ThayDoi, "
            sql &= " DonGia1,TienTe1,GiaNhap1,MucThue1,GiaBan1,GiaNCC1,XuatThue1,TNK1,KhoiLuong1,VCQT,VCTN,GiaMOQ1,GiaMOQ2,GiaMOQ3,SLMOQ1,SLMOQ2,SLMOQ3,NK,GiaNK,GiaNKBAC,TienTeNK,TonNCC,"
            sql &= " (CASE ISNULL(GiaNKBAC,0) WHEN 0 Then 0 ELSE Round((((ISNULL(GiaNCC1,0)/100 * ISNULL(DonGia1,0))-GiaNKBAC)/GiaNKBAC)*100,2) END) AS PTLNBB"
            sql &= " FROM VATTU LEFT JOIN TENNHOM ON TENNHOM.ID= VATTU.IDTennhom "
            sql &= " Left Join"
            sql &= " (SELECT DISTINCT IDVatTu FROM"
            sql &= " (SELECT  IDVatTu FROM NHAPKHO "
            sql &= "    UNION ALL"
            sql &= " SELECT  IDVatTu FROM XUATKHO)tb)tbNX ON tbNX.IDVatTu=VATTU.ID"
            sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangsanxuat "

            If btFilterMaVT.EditValue <> "" Then
                sqlWhere &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sqlWhere &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sqlWhere &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sqlWhere &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sqlWhere &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
            End If

        End If



        sql &= sqlWhere
        If Not chkLocMaTrung.Checked Then
            sql &= " ORDER BY VATTU.IDTennhom,VATTU.IDTenvattu,VATTU.IDHangsanxuat,VATTU.Model "
        End If

        Dim countErr As Integer = 0

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            If chkTaiAnh.Checked Then
                With tb
                    For i As Integer = 0 To .Rows.Count - 1
                        Application.DoEvents()
                        If .Rows(i)("HinhAnh").ToString <> "" Then
                            Try
                                .Rows(i)("HienThi") = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & .Rows(i)("TenNhom_ENG") & "\" & .Rows(i)("TenHang") & "\thumb\" & .Rows(i)("HinhAnh"))
                            Catch ex As Exception
                                countErr += 1
                            End Try
                        End If
                    Next
                End With
            End If

            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
        If countErr > 0 Then
            ShowCanhBao("Có " & countErr.ToString & " mục không tải được ảnh !")
        End If

        If chkLocMaTrung.Checked Then
            btXoaChuaSuDung.Enabled = True
            gdvCT.Columns("colSuDung").Visible = True
            gdvCT.Columns("colXoa").Visible = True
            Dim tmp As String = ""
            gdvCT.BeginUpdate()
            For i As Integer = 0 To gdvCT.DataRowCount - 1
                'If gdvCT.GetRowCellValue(i, "colSuDung") = True Then Continue For
                If gdvCT.GetRowCellValue(i, "Model2") = tmp And gdvCT.GetRowCellValue(i, "colSuDung") = False Then
                    gdvCT.SetRowCellValue(i, "colXoa", 1)
                Else
                    tmp = gdvCT.GetRowCellValue(i, "Model2")
                End If
            Next
            gdvCT.EndUpdate()
        Else
            btXoaChuaSuDung.Enabled = False
            gdvCT.Columns("colSuDung").Visible = False
            gdvCT.Columns("colXoa").Visible = False
        End If

    End Sub




    Public Sub loadDSLanDau()
        Dim sql As String = " SELECT convert(bit,0) AS NX,Code,VATTU.ID,IDTenVatTu,IDHangSanXuat,Model,IDDonViTinh,ThongSo,IDTenNuoc,IDTenTaiLieu,ConSX,MaLoi,DMTon,DatToiThieu,IDTenNhom,ThongDung,Ton,Ngay,ThongSo_ENG,HangTon,(convert(image,NULL))HienThi,HinhAnh,TENNHOM.Ten_ENG AS TenNhom_ENG,TENNHOM.Ten as TenNhom_VIE, TENHANGSANXUAT.Ten AS TenHang,TaiLieu,(Convert(bit,0)) AS Modify,GhiChu,ThayDoi,"
        sql &= " DonGia1,TienTe1,GiaNhap1,MucThue1,GiaBan1,GiaNCC1,XuatThue1,TNK1,KhoiLuong1,VCQT,VCTN,GiaMOQ1,GiaMOQ2,GiaMOQ3,SLMOQ1,SLMOQ2,SLMOQ3,NK,GiaNK,GiaNKBAC,TienTeNK"
        sql &= " FROM VATTU LEFT JOIN TENNHOM ON TENNHOM.ID= VATTU.IDTennhom "
        sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangsanxuat "
        sql &= " WHERE VATTU.ID = -1"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        End If
    End Sub

    Private Sub gdvCT_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        If e.KeyCode = Keys.Delete Then
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
            If ShowCauHoi("Xóa hàng hóa được chọn ?") Then
                Dim sql As String = ""
                Dim _IDVT As Object = gdvCT.GetFocusedRowCellValue("ID")
                AddParameterWhere("@ID", _IDVT)
                sql &= " SELECT COUNT(ID) FROM CHAOGIA WHERE IDvattu=@ID SELECT COUNT(ID) FROM DATHANG WHERE IDvattu=@ID "
                sql &= " SELECT COUNT(ID) FROM NHAPKHO WHERE IDvattu=@ID SELECT COUNT(ID) FROM XUATKHO WHERE IDvattu=@ID SELECT COUNT(ID) FROM YEUCAUDEN WHERE IDvattu=@ID "
                Dim ds As DataSet = ExecuteSQLDataSet(sql)
                If Not ds Is Nothing Then
                    Dim count As Integer = 0
                    For i As Integer = 0 To ds.Tables.Count - 1
                        count += Convert.ToInt32(ds.Tables(i).Rows(0)(0))
                    Next
                    If count > 0 Then
                        'If ShowCauHoi("Hàng hóa đã được sử dụng không thể xoá !")
                        Dim f As New frmDoiID
                        f._oldID = _IDVT
                        f._IsVatTu = True
                        f.ShowDialog()
                        Exit Sub
                    End If
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If

                AddParameterWhere("@ID", _IDVT)
                If Not doDelete("VATTU", "ID=@ID") Is Nothing Then
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa !")
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If

            End If
        ElseIf e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
        ElseIf e.KeyCode = Keys.L Then
            colLog.Visible = Not colLog.Visible
        ElseIf e.KeyCode = Keys.I Then
            colID.Visible = Not colID.Visible
        ElseIf e.KeyCode = Keys.Escape Then
            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
                gdvCT.DeleteSelectedRows()
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btLuuLai.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            mXemAnhLon.PerformClick()
        End If
    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadDS()
    End Sub

    Private Sub mSuaHinhAnh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaHinhAnh.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub

        If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "ID").ToString.Trim = "" Then
            ShowCanhBao("Cần lưu lại thông tin vật tư trước khi thêm hình ảnh !")
        End If

        If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "TenNhom_ENG").ToString.Trim = "" Or gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "TenHang").ToString.Trim = "" Then
            ShowCanhBao("Cần chọn nhóm và hãng sản xuất cụ thể để tránh việc thất lạc hình ảnh hàng hóa")
            Exit Sub
        End If

        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Filter = "Image File|*.jpeg;*.jpg;*.png;*.gif"
        If openFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải file lên máy chủ ...")
            If Not System.IO.Directory.Exists(UrlAnhVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang")) Then
                System.IO.Directory.CreateDirectory(UrlAnhVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang"))
            End If
            Try
                path = IO.Path.GetFileNameWithoutExtension(openFile.FileName).Replace("'", "") & " " & TaiKhoan.ToString & " " & Now().ToString("ddMMyyyyhhmmss") & IO.Path.GetExtension(openFile.FileName)
                IO.File.Copy(openFile.FileName, UrlAnhVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & path)
                If Not System.IO.Directory.Exists(UrlAnhVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\thumb\") Then
                    System.IO.Directory.CreateDirectory(UrlAnhVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\thumb\")
                End If
                IO.File.Copy(openFile.FileName, UrlAnhVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\thumb\" & path)


                gdvCT.SetFocusedRowCellValue("HinhAnh", path)
                gdvCT.SetFocusedRowCellValue("HienThi", Utils.ConvertImage.Image2ByteFromImgUrl(openFile.FileName))

                AddParameter("@HinhAnh", gdvCT.GetFocusedRowCellValue("HinhAnh"))
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("VATTU", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                SaveLog("-EDITP-", gdvCT.GetFocusedRowCellValue("ID"))
                ' gdvCT.SetFocusedRowCellValue("Modify", False)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
            CloseWaiting()
        End If
    End Sub

    Public Sub SaveLog(ByVal NoiDung As String, ByVal IDVatTu As Object)
        AddParameterWhere("@ID", IDVatTu)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ThayDoi FROM VATTU WHERE ID=@ID")
        If tb Is Nothing Then Throw New Exception(LoiNgoaiLe)
        AddParameter("@ThayDoi", tb.Rows(0)(0).ToString & ";" & vbCrLf & TaiKhoan.ToString & NoiDung & GetServerTime())
        AddParameterWhere("@ID", IDVatTu)
        If doUpdate("VATTU", "ID=@ID") Is Nothing Then
            ShowCanhBao(LoiNgoaiLe)
        End If

    End Sub

    Private Sub mBoHinhAnh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBoHinhAnh.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If ShowCauHoi("Xoá hình ảnh ?") Then
            Dim _IDVatTu As String = "("
            For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
                _IDVatTu &= gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "ID")
                If i < gdvCT.SelectedRowsCount - 1 Then _IDVatTu &= ","
            Next
            _IDVatTu &= ")"

            If ExecuteSQLNonQuery("UPDATE VATTU SET HinhAnh= NULL WHERE ID IN " & _IDVatTu) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                If ShowCauHoi("Bạn có muốn xoá luôn file hình ảnh trên máy chủ không ?") Then
                    For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
                        Try
                            System.IO.File.Delete(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang") & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "HinhAnh"))
                            System.IO.File.Delete(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang") & "\thumb\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "HinhAnh"))

                        Catch ex As Exception
                            ShowBaoLoi(ex.Message)
                        End Try
                        gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "HinhAnh", Nothing)
                        gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "HienThi", Nothing)
                        gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "Modify", False)
                        SaveLog("-DP-", gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "ID"))
                    Next
                Else
                    For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
                        gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "HinhAnh", Nothing)
                        gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "HienThi", Nothing)
                        gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "Modify", False)
                        SaveLog("-DELP-", gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "ID"))
                    Next
                End If

            End If
        End If
    End Sub

    Private Sub btSaoChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSaoChep.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        gdvCT.OptionsBehavior.Editable = True
        gdvCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Dim index As Integer = gdvCT.FocusedRowHandle
        gdvCT.AddNewRow()
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "IDTenVatTu", gdvCT.GetRowCellValue(index, "IDTenVatTu"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "IDHangSanXuat", gdvCT.GetRowCellValue(index, "IDHangSanXuat"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "Model", gdvCT.GetRowCellValue(index, "Model"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "IDDonViTinh", gdvCT.GetRowCellValue(index, "IDDonViTinh"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "ThongSo", gdvCT.GetRowCellValue(index, "ThongSo"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "IDTenNuoc", gdvCT.GetRowCellValue(index, "IDTenNuoc"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "ConSX", gdvCT.GetRowCellValue(index, "ConSX"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "MaLoi", gdvCT.GetRowCellValue(index, "MaLoi"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "IDTenNhom", gdvCT.GetRowCellValue(index, "IDTenNhom"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "ThongSo_ENG", gdvCT.GetRowCellValue(index, "ThongSo_ENG"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "HinhAnh", gdvCT.GetRowCellValue(index, "HinhAnh"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "HienThi", gdvCT.GetRowCellValue(index, "HienThi"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "TenNhom_ENG", gdvCT.GetRowCellValue(index, "TenNhom_ENG"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "TenNhom_VIE", gdvCT.GetRowCellValue(index, "TenNhom_VIE"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "TenHang", gdvCT.GetRowCellValue(index, "TenHang"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "TaiLieu", gdvCT.GetRowCellValue(index, "TaiLieu"))
        gdvCT.SetRowCellValue(gdvCT.FocusedRowHandle, "Modify", True)
        gdvCT.FocusedColumn = gdvCT.Columns("Model")
        SendKeys.Send("{Enter}")
        gdvCT.ShowEditor()
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "IDHangSanXuat"
                Dim tmpTenHang As String = gdvCT.GetRowCellValue(e.RowHandle, "TenHang").ToString
                gdvCT.SetRowCellValue(e.RowHandle, "TenHang", gdvCT.GetRowCellDisplayText(e.RowHandle, "IDHangSanXuat"))
                If gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh").ToString <> "" Then
                    If Not System.IO.Directory.Exists(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang")) Then
                        System.IO.Directory.CreateDirectory(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang"))
                    End If
                    System.IO.File.Copy(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & tmpTenHang & "\" & gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh"), UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh"))
                    If Not System.IO.Directory.Exists(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\thumb\") Then
                        System.IO.Directory.CreateDirectory(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\thumb\")
                    End If
                    System.IO.File.Copy(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & tmpTenHang & "\thumb\" & gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh"), UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\thumb\" & gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh"))

                End If
                If gdvCT.GetRowCellValue(e.RowHandle, "TaiLieu").ToString <> "" Then
                    If Not System.IO.Directory.Exists(UrlTaiLieuVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang")) Then
                        System.IO.Directory.CreateDirectory(UrlTaiLieuVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang"))
                    End If

                    Dim tb As DataTable = DataSourceDSFile(gdvCT.GetRowCellValue(e.RowHandle, "TaiLieu").ToString)
                    For i As Integer = 0 To tb.Rows.Count - 1
                        System.IO.File.Copy(UrlTaiLieuVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & tmpTenHang & "\" & tb.Rows(i)(0).ToString, UrlTaiLieuVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\" & tb.Rows(i)(0).ToString)
                    Next
                End If

            Case "IDTenNhom"
                Dim tmpTenNhom As String = gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG").ToString

                AddParameterWhere("@ID", e.Value)
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten,Ten_ENG FROM TENNHOM WHERE ID=@ID")
                If Not tb Is Nothing Then
                    gdvCT.SetRowCellValue(e.RowHandle, "TenNhom_ENG", tb.Rows(0)("Ten_ENG"))
                    gdvCT.SetRowCellValue(e.RowHandle, "TenNhom_VIE", tb.Rows(0)("Ten"))
                    If gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh").ToString <> "" Then
                        If Not System.IO.Directory.Exists(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang")) Then
                            System.IO.Directory.CreateDirectory(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang"))
                        End If
                        System.IO.File.Copy(UrlAnhVatTu & tmpTenNhom & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh"), UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh"))

                        If Not System.IO.Directory.Exists(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\thumb\") Then
                            System.IO.Directory.CreateDirectory(UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\thumb\")
                        End If
                        System.IO.File.Copy(UrlAnhVatTu & tmpTenNhom & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\thumb\" & gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh"), UrlAnhVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\thumb\" & gdvCT.GetRowCellValue(e.RowHandle, "HinhAnh"))

                        If gdvCT.GetRowCellValue(e.RowHandle, "TaiLieu").ToString <> "" Then
                            If Not System.IO.Directory.Exists(UrlTaiLieuVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang")) Then
                                System.IO.Directory.CreateDirectory(UrlTaiLieuVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang"))
                            End If

                            Dim tb2 As DataTable = DataSourceDSFile(gdvCT.GetRowCellValue(e.RowHandle, "TaiLieu").ToString)
                            For i As Integer = 0 To tb2.Rows.Count - 1
                                System.IO.File.Copy(UrlTaiLieuVatTu & tmpTenNhom & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\" & tb2.Rows(i)(0).ToString, UrlTaiLieuVatTu & gdvCT.GetRowCellValue(e.RowHandle, "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(e.RowHandle, "TenHang") & "\" & tb2.Rows(i)(0).ToString)
                            Next

                        End If
                    End If
                End If

        End Select
        If e.Column.FieldName <> "Modify" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If

    End Sub

    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetRowCellValue(e.RowHandle, "MaLoi", False)
        gdvCT.SetRowCellValue(e.RowHandle, "ConSX", True)
        gdvCT.SetRowCellValue(e.RowHandle, "DonGia1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "TienTe1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "TienTeNK", 1)
        gdvCT.SetRowCellValue(e.RowHandle, "GiaNhap1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "MucThue1", 10)
        gdvCT.SetRowCellValue(e.RowHandle, "GiaBan1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "GiaNCC1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "XuatThue1", False)
        gdvCT.SetRowCellValue(e.RowHandle, "TNK1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "KhoiLuong1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "VCQT", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "VCTN", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "GiaMOQ1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "GiaMOQ2", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "GiaMOQ3", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "SLMOQ1", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "SLMOQ2", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "SLMOQ3", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "NK", False)
        gdvCT.SetRowCellValue(e.RowHandle, "GiaNK", 0)
        gdvCT.SetRowCellValue(e.RowHandle, "NX", False)
        gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
    End Sub

    Private Sub btNhapMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNhapMoi.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        gdvCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        'gdvCT.AddNewRow()
        gdvCT.OptionsBehavior.Editable = True
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "DS VAT TU.xls"
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

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick, mLuuLai.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Or Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            ShowCanhBao("Bạn không có quyền lưu lại số liệu !")
            Exit Sub
        End If
        Dim index As Integer = gdvCT.FocusedRowHandle
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        Dim _Count As Integer = 0

        If gdvCT.RowCount > 20 Then
            ShowWaiting("Đang lưu lại...")
        End If
        Dim rowcount As Integer = 0
        If gdvCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom Then
            rowcount = gdvCT.RowCount - 2
        Else
            rowcount = gdvCT.RowCount - 1
        End If
        gdvCT.BeginUpdate()
        Try

            For i As Integer = 0 To rowcount
                If gdvCT.GetRowCellValue(i, "Modify") Then
                    If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Or gdvCT.GetRowCellValue(i, "ID") Is Nothing Then
                        AddParameterWhere("@MaVT", gdvCT.GetRowCellValue(i, "Model").ToString.Trim)
                        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM VATTU WHERE rtrim(ltrim(Model))=@MaVT")
                        If Not tb Is Nothing Then
                            If tb.Rows.Count > 0 Then
                                If Not ShowCauHoi("Mã: " & gdvCT.GetRowCellValue(i, "Model").ToString.Trim & " đã tồn tại, bạn có muốn tạo mới không ?") Then
                                    Continue For
                                End If
                            End If
                        End If
                    End If

                    AddParameter("@IDTenvattu", gdvCT.GetRowCellValue(i, "IDTenVatTu"))
                    AddParameter("@IDHangSanxuat", gdvCT.GetRowCellValue(i, "IDHangSanXuat"))
                    AddParameter("@Model", gdvCT.GetRowCellValue(i, "Model").ToString.Trim)
                    AddParameter("@IDDonvitinh", gdvCT.GetRowCellValue(i, "IDDonViTinh"))
                    AddParameter("@Thongso", gdvCT.GetRowCellValue(i, "ThongSo"))
                    AddParameter("@IDTennuoc", gdvCT.GetRowCellValue(i, "IDTenNuoc"))
                    AddParameter("@ConSX", gdvCT.GetRowCellValue(i, "ConSX"))
                    AddParameter("@MaLoi", gdvCT.GetRowCellValue(i, "MaLoi"))
                    AddParameter("@IDTennhom", gdvCT.GetRowCellValue(i, "IDTenNhom"))
                    AddParameter("@ThongSo_ENG", gdvCT.GetRowCellValue(i, "ThongSo_ENG"))
                    AddParameter("@HinhAnh", gdvCT.GetRowCellValue(i, "HinhAnh"))
                    AddParameter("@TaiLieu", gdvCT.GetRowCellValue(i, "TaiLieu"))
                    AddParameter("@GhiChu", gdvCT.GetRowCellValue(i, "GhiChu"))
                    If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                        AddParameter("@DonGia1", gdvCT.GetRowCellValue(i, "DonGia1"))
                        AddParameter("@TienTe1", gdvCT.GetRowCellValue(i, "TienTe1"))
                        AddParameter("@TonNCC", gdvCT.GetRowCellValue(i, "TonNCC"))
                        AddParameter("@XuatThue1", gdvCT.GetRowCellValue(i, "XuatThue1"))
                        AddParameter("@MucThue1", gdvCT.GetRowCellValue(i, "MucThue1"))
                        AddParameter("@GiaNhap1", gdvCT.GetRowCellValue(i, "GiaNhap1"))
                        AddParameter("@GiaBan1", gdvCT.GetRowCellValue(i, "GiaBan1"))
                        AddParameter("@GiaNCC1", gdvCT.GetRowCellValue(i, "GiaNCC1"))
                    End If

                    If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then

                        AddParameter("@GiaNKBAC", gdvCT.GetRowCellValue(i, "GiaNKBAC"))
                        AddParameter("@TNK1", gdvCT.GetRowCellValue(i, "TNK1"))
                        AddParameter("@KhoiLuong1", gdvCT.GetRowCellValue(i, "KhoiLuong1"))
                        AddParameter("@VCQT", gdvCT.GetRowCellValue(i, "VCQT"))
                        AddParameter("@VCTN", gdvCT.GetRowCellValue(i, "VCTN"))
                        AddParameter("@GiaMOQ1", gdvCT.GetRowCellValue(i, "GiaMOQ1"))
                        AddParameter("@GiaMOQ2", gdvCT.GetRowCellValue(i, "GiaMOQ2"))
                        AddParameter("@GiaMOQ3", gdvCT.GetRowCellValue(i, "GiaMOQ3"))
                        AddParameter("@SLMOQ1", gdvCT.GetRowCellValue(i, "SLMOQ1"))
                        AddParameter("@SLMOQ2", gdvCT.GetRowCellValue(i, "SLMOQ2"))
                        AddParameter("@SLMOQ3", gdvCT.GetRowCellValue(i, "SLMOQ3"))
                        AddParameter("@NK", gdvCT.GetRowCellValue(i, "NK"))
                        AddParameter("@GiaNK", gdvCT.GetRowCellValue(i, "GiaNK"))
                        AddParameter("@TienTeNK", gdvCT.GetRowCellValue(i, "TienTeNK"))
                    End If


                    If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Or gdvCT.GetRowCellValue(i, "ID") Is Nothing Then
                        AddParameter("@Code", gdvCT.GetRowCellValue(i, "Model").ToString.Trim)
                        objID = doInsert("VATTU")
                        If objID Is Nothing Then
                            'gdvCT.DeleteSelectedRows()
                            Throw New Exception(LoiNgoaiLe)
                        End If
                        gdvCT.SetRowCellValue(i, "ID", objID)
                        gdvCT.SetRowCellValue(i, "Code", gdvCT.GetRowCellValue(i, "Model").ToString.Trim)
                        SaveLog("-ADDNEW-", gdvCT.GetRowCellValue(i, "ID"))
                    Else
                        AddParameter("@Code", gdvCT.GetRowCellValue(i, "Code").ToString.Trim)
                        AddParameterWhere("@ID", gdvCT.GetRowCellValue(i, "ID"))
                        If doUpdate("VATTU", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        SaveLog("-EDITINF-", gdvCT.GetRowCellValue(i, "ID"))
                    End If
                    gdvCT.SetRowCellValue(i, "Modify", False)

                    _Count += 1
                End If
            Next

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
        gdvCT.EndUpdate()
        If gdvCT.RowCount > 20 Then
            CloseWaiting()
        End If
        gdvCT.FocusedRowHandle = index
        If _Count > 0 Then
            ShowAlert("Đã lưu lại số liệu !")
        End If
        gdvCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
    End Sub

#Region "File đính kèm"

    Private Sub rcbTaiLieu_Popup(sender As System.Object, e As System.EventArgs) Handles rcbTaiLieu.Popup
        If _Exit Then
            _Exit = False
            Exit Sub
        End If
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
    End Sub

    Private Sub rcbFileDinhKem_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rcbTaiLieu.Closed
        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        If _File = "" Then
            _File = Nothing
        End If
        CType(sender, PopupContainerEdit).EditValue = _File
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub

        If gdvCT.GetFocusedRowCellValue("ID").ToString.Trim = "" Then
            ShowCanhBao("Cần lưu lại vật tư trước khi thêm tài liệu !")
            Exit Sub
        End If

        If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "TenNhom_ENG").ToString.Trim = "" Or gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "TenHang").ToString.Trim = "" Then
            ShowCanhBao("Cần chọn nhóm và hãng sản xuất cụ thể để tránh việc thất lạc tài liệu hàng hóa ")
            Exit Sub
        End If


        Dim path As String = ""
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True
        If openFile.ShowDialog = DialogResult.OK Then
            If Not System.IO.Directory.Exists(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang")) Then
                System.IO.Directory.CreateDirectory(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang"))
            End If
            ShowWaiting("Đang tải file lên máy chủ ...")
            For Each file In openFile.FileNames
                Try
                    path = System.IO.Path.GetFileNameWithoutExtension(file) & " " & TaiKhoan.ToString & " " & Now().ToString("ddMMyyyyhhmmss") & System.IO.Path.GetExtension(file).Replace("'", "")
                    System.IO.File.Copy(file, UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & path)
                    gdvListFileCT.AddNewRow()
                    gdvListFileCT.SetFocusedRowCellValue("File", path)
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            Next
            SaveLog("-ADDCATALOG-", gdvCT.GetFocusedRowCellValue("ID"))
            CloseWaiting()
            gdvListFileCT.CloseEditor()
            gdvListFileCT.UpdateCurrentRow()

            _Exit = True
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub

        If ShowCauHoi("Bạn có muốn xoá luôn file gốc trên máy chủ ?") Then
            Try
                System.IO.File.Delete(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & gdvListFileCT.GetFocusedRowCellValue("File"))
                gdvListFileCT.DeleteSelectedRows()
            Catch ex As Exception
                If Not IO.File.Exists(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & gdvListFileCT.GetFocusedRowCellValue("File")) Then
                    gdvListFileCT.DeleteSelectedRows()
                End If
                ShowBaoLoi(ex.Message)
            End Try

        Else

            gdvListFileCT.DeleteSelectedRows()

        End If
        SaveLog("-DELCATALOG-", gdvCT.GetFocusedRowCellValue("ID"))
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
        _Exit = True
        gdvCT.Focus()
        SendKeys.Send("{F4}")

    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & e.CellValue, e.CellValue)
        End If
    End Sub
#End Region


    Private Sub mSaoChep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSaoChep.ItemClick
        btSaoChep.PerformClick()
    End Sub

    Private Sub mTaiAnhVeMay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTaiAnhVeMay.ItemClick
        If gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "HinhAnh").ToString <> "" Then
            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "Image file|*." & System.IO.Path.GetExtension(gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "HinhAnh"))
            saveFile.FileName = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "HinhAnh")
            If saveFile.ShowDialog = DialogResult.OK Then
                System.IO.File.Copy(UrlAnhVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "HinhAnh"), saveFile.FileName)
                If ShowCauHoi("Đã tải xong, bạn có muốn mở file vừa tải không ?") Then
                    OpenFile(saveFile.FileName)
                End If
            End If

        End If

    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
        If gdvCT.SelectedRowsCount > 1 Then
            mDungChungHinhAnh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            mDungChungTaiLieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            mDungChungHinhAnh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mDungChungTaiLieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        If gdvCT.SelectedRowsCount > 0 Then
            If gdvCT.GetFocusedRowCellValue("HinhAnh").ToString = "" Then
                mXemAnhLon.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mBoHinhAnh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                mXemAnhLon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mBoHinhAnh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If

        If gdvCT.SortedColumns.Count < 0 Then
            mBoSapXep.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            mBoSapXep.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub mDungChungTaiLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDungChungTaiLieu.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        Dim _str As String = ""
        Dim _IDVatTu As String = "("
        For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
            If gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TaiLieu").ToString <> "" Then
                _str &= gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TaiLieu") & ";"
            End If
            _IDVatTu &= gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "ID")
            If i < gdvCT.SelectedRowsCount - 1 Then _IDVatTu &= ","
        Next
        _IDVatTu &= ")"
        Dim tbDSFile As DataTable = DataSourceDSFile(_str.TrimEnd(New Char() {";c"}))
        tbDSFile.DefaultView.Sort = "File ASC"
        Dim dv As DataView = tbDSFile.DefaultView

        Dim kt As Boolean = False
        _str = dv.Item(0)("File")

        For i As Integer = 1 To dv.Count - 1
            If dv.Item(i)("File") <> dv.Item(i - 1)("File") Then
                _str &= ";" & dv.Item(i)("File").ToString
            End If
        Next

        'With dv

        '    For i As Integer = 1 To .Rows.Count - 1
        '        If .Rows(i)("File").ToString <> .Rows(i - 1)("File").ToString Then
        '            _str &= ";" & .Rows(i)("File").ToString

        '        End If


        '        'For j As Integer = 2 To .Rows.Count - 1
        '        '    If .Rows(j)("File").ToString = .Rows(i)("File").ToString Then
        '        '        kt = True
        '        '    Else
        '        '        If .Rows(i)("File").ToString <> tbDSFile.Rows(0)("File").ToString Then
        '        '            _str &= ";" & .Rows(i)("File").ToString
        '        '        End If
        '        '    End If
        '        'Next
        '        'If kt = False Then
        '        '    If .Rows(i)("File").ToString <> tbDSFile.Rows(0)("File").ToString Then
        '        '        _str &= ";" & .Rows(i)("File").ToString
        '        '    End If

        '        'End If
        '    Next
        'End With

        Dim time As DateTime = GetServerTime()

        If ExecuteSQLNonQuery("UPDATE VATTU SET ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-MULTICATALOG-' + '" & time & "', TaiLieu=N'" & _str & "' WHERE ID IN " & _IDVatTu) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            'ShowBaoLoi("UPDATE VATTU SET TaiLieu=N'" & _str & "' WHERE ID IN " & _IDVatTu)
        Else
            gdvCT.BeginUpdate()
            For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
                gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "TaiLieu", _str)
            Next
            gdvCT.EndUpdate()
            ShowAlert("Đã cập nhật tài liệu !")
        End If
        'Next

    End Sub

    Private Sub mDungChungHinhAnh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDungChungHinhAnh.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub

        Dim _strHinhAnh As String = ""
        Dim _IDVatTu As String = "("
        Dim _IndexHinhAnh As Integer = 0

        If gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "HinhAnh").ToString = "" Then
            ShowCanhBao("Dòng đầu tiên chưa có hình ảnh !")
            Exit Sub
        End If

        _strHinhAnh = gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "HinhAnh").ToString

        For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
            _IDVatTu &= gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "ID")
            If i < gdvCT.SelectedRowsCount - 1 Then _IDVatTu &= ","
        Next
        _IDVatTu &= ")"


        Dim time As DateTime = GetServerTime()
        If ExecuteSQLNonQuery("UPDATE VATTU SET ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-MULTIP-' + '" & time & "',HinhAnh=N'" & _strHinhAnh & "' WHERE ID IN " & _IDVatTu) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            ' ShowBaoLoi("UPDATE VATTU SET HinhAnh=N'" & _strHinhAnh & "' WHERE ID IN " & _IDVatTu)
        Else
            gdvCT.BeginUpdate()
            For i As Integer = 0 To gdvCT.SelectedRowsCount - 1
                If gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG").ToString <> gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "TenNhom_ENG").ToString Or gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang").ToString <> gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "TenHang").ToString Then
                    If Not System.IO.Directory.Exists(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG").ToString & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang").ToString) Then
                        System.IO.Directory.CreateDirectory(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG").ToString & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang").ToString)
                    End If
                    System.IO.File.Copy(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(_IndexHinhAnh, "TenHang") & "\" & _strHinhAnh, UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang") & "\" & _strHinhAnh, True)
                    If Not System.IO.Directory.Exists(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG").ToString & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang").ToString & "\thumb\") Then
                        System.IO.Directory.CreateDirectory(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG").ToString & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang").ToString & "\thumb\")
                    End If
                    System.IO.File.Copy(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(0), "TenHang") & "\thumb\" & _strHinhAnh, UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang") & "\thumb\" & _strHinhAnh, True)

                End If
                gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "HinhAnh", _strHinhAnh)
                gdvCT.SetRowCellValue(gdvCT.GetSelectedRows(i), "HienThi", Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenNhom_ENG") & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "TenHang") & "\" & gdvCT.GetRowCellValue(gdvCT.GetSelectedRows(i), "HinhAnh")))
            Next
            gdvCT.EndUpdate()
            ShowAlert("Đã cập nhật hình ảnh dùng chung cho hàng hóa !")
        End If

    End Sub


    Private Sub mMoThuMucLuuTruTaiLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mMoThuMucLuuTruTaiLieu.ItemClick
        If gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File").ToString <> "" Then
            If Not System.IO.File.Exists(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File")) Then
                ShowCanhBao("Không tìm thấy file !")
                Exit Sub
            End If

            Dim saveFile As New SaveFileDialog
            saveFile.Filter = "file|*." & System.IO.Path.GetExtension(gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File").ToString)
            saveFile.FileName = gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File").ToString
            If saveFile.ShowDialog = DialogResult.OK Then
                ShowWaiting("Đang tải file ...")
                System.IO.File.Copy(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & gdvListFileCT.GetRowCellValue(gdvListFileCT.FocusedRowHandle, "File"), saveFile.FileName)
                CloseWaiting()
                If ShowCauHoi("Đã tải xong, bạn có muốn mở file vừa tải không ?") Then
                    OpenFile(saveFile.FileName)
                End If
            End If


        End If
    End Sub

    Private Sub btCapNhatNhomVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCapNhatNhomVT.ItemClick
        'ShowAlert(gdvCT.RowCount)
        If Not ShowCauHoi("Cập nhật nhóm hàng hóa ?") Then Exit Sub

        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        If gdvCT.ActiveFilter.Count > 0 Or gdvCT.SortedColumns.Count > 0 Then
            ShowCanhBao("Tính năng này không được sử dụng khi dùng tính năng lọc hoặc sắp xếp trên grid ")
            Exit Sub
        End If
        'If ShowCauHoi("Bạn có chắc là muốn cập nhật nhóm cho toàn bộ danh sách đang hiển thị ?") Then
        ShowWaiting("Đang xử lý ...")
        Dim tb As DataTable = CType(gdv.Views.Item(0).DataSource, DataView).Table

        For i As Integer = 1 To tb.Rows.Count - 1
            'Application.DoEvents()
            'tb.Rows(i)("IDTenNhom") = tb.Rows(0)("IDTenNhom")
            If tb.Rows(i)("IDTenNhom") <> tb.Rows(0)("IDTenNhom") Then
                tb.Rows(i)("IDTenNhom") = tb.Rows(0)("IDTenNhom")
                tb.Rows(i)("Modify") = 1
            End If

            'gdvCT.TopRowIndex += 1
        Next
        gdv.DataSource = tb
        CloseWaiting()
        If chkTuDong.Checked Then
            btLuuLai.PerformClick()
        Else
            ShowCanhBao("Đã thực hiện xong thao tác, cần thực hiện thao tác lưu lại để tránh mất dữ liệu !")
        End If
    End Sub

    Private Sub mXemAnhLon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemAnhLon.ItemClick
        If gdvCT.GetFocusedRowCellValue("HinhAnh").ToString = "" Then Exit Sub
        Dim f As New frmXemAnh
        f.pAnh.EditValue = Utils.ConvertImage.Image2ByteFromImgUrl(UrlAnhVatTu & "\" & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & gdvCT.GetFocusedRowCellValue("HinhAnh").ToString)
        f.Text = "Ảnh: " & gdvCT.GetFocusedRowCellValue("Model").ToString
        f.ShowDialog()
    End Sub

    Private Sub mBoSapXep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mBoSapXep.ItemClick
        gdvCT.ClearSorting()
        gdvCT.FocusedRowHandle = 0
    End Sub

    Private Sub chkTuDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkTuDong.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            chkTuDong.Checked = False
        End If

    End Sub

    Private Sub mSapXep_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSapXep.ItemClick
        gdvCT.FocusedColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        gdvCT.FocusedRowHandle = 0
    End Sub

    Private Sub chkLocMaTrung_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLocMaTrung.CheckedChanged
        If chkLocMaTrung.Checked = True Then
            chkLocMaTrung.Glyph = My.Resources.Checked
        Else
            chkLocMaTrung.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub chkTuDong_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkTuDong.CheckedChanged
        If chkTuDong.Checked = True Then
            chkTuDong.Glyph = My.Resources.Checked
        Else
            chkTuDong.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub tbGiaNhap_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbGiaNhap.ButtonClick, tbTNK.ButtonClick, tbKhoiLuong.ButtonClick, tbMucThue.ButtonClick, tbGiaBB.ButtonClick, tbGiaMOQ3.ButtonClick, tbSLMOQ3.ButtonClick, tbGiaMOQ2.ButtonClick, tbSLMOQ2.ButtonClick, tbGiaMOQ1.ButtonClick, tbSLMOQ1.ButtonClick, tbGiaBL.ButtonClick, tbVCQT.ButtonClick, tbVCTN.ButtonClick
        If e.Button.Index = 1 Then
            If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
            'gdvCT.BeginUpdate()
            Dim time As DateTime = GetServerTime()


            Dim sql As String = ""
            Select Case CType(sender, SpinEdit).Name
                Case "tbGiaNhap"
                    sql &= "UPDATE VATTU SET GiaNhap1=@GiaNhap,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-GN-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@GiaNhap", tbGiaNhap.Value)
                Case "tbTNK"
                    sql &= "UPDATE VATTU SET TNK1=@TNK,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-TNK-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@TNK", tbTNK.Value)
                Case "tbKhoiLuong"
                    sql &= "UPDATE VATTU SET KhoiLuong1=@KhoiLuong,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-KL-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@KhoiLuong", tbKhoiLuong.Value)
                Case "tbMucThue"
                    sql &= "UPDATE VATTU SET MucThue1=@MucThue,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-MT-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@MucThue", tbMucThue.Value)
                Case "tbGiaBB"
                    sql &= "UPDATE VATTU SET GiaNCC1=@GiaNCC,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-GBB-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@GiaNCC", tbGiaBB.Value)
                Case "tbGiaMOQ3"
                    sql &= "UPDATE VATTU SET GiaMOQ3=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-GMOQ3-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbGiaMOQ3.Value)
                Case "tbSLMOQ3"
                    sql &= "UPDATE VATTU SET SLMOQ3=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-SLMOQ3-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbSLMOQ3.Value)
                Case "tbGiaMOQ2"
                    sql &= "UPDATE VATTU SET GiaMOQ2=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-GMOQ2-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbGiaMOQ2.Value)
                Case "tbSLMOQ2"
                    sql &= "UPDATE VATTU SET SLMOQ2=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-SLMOQ2-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbSLMOQ2.Value)
                Case "tbGiaMOQ1"
                    sql &= "UPDATE VATTU SET GiaMOQ1=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-GMOQ1-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbGiaMOQ1.Value)
                Case "tbSLMOQ1"
                    sql &= "UPDATE VATTU SET SLMOQ1=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-SLMOQ1-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbSLMOQ1.Value)
                Case "tbGiaBL"
                    sql &= "UPDATE VATTU SET GiaBan1=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-GBL-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbGiaBL.Value)
                Case "tbVCQT"
                    sql &= "UPDATE VATTU SET VCQT=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-VCQT-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbVCQT.Value)
                Case "tbVCTN"
                    sql &= "UPDATE VATTU SET VCTN=@A,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-VCTN-' + '" & time & "' WHERE ID <>0 "
                    AddParameterWhere("@A", tbVCTN.Value)
            End Select


            If btFilterMaVT.EditValue <> "" Then
                sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
            End If
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                LoadDS()
            End If
            'gdvCT.EndUpdate()
            ShowAlert("Đã thực hiện !")
        End If

    End Sub

    Private Sub cbTienTe_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTienTe.ButtonClick
        If e.Button.Index = 1 Then
            If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
            Dim sql As String = ""
            Dim time As DateTime = GetServerTime()
            sql &= "UPDATE VATTU SET TienTe1=" & cbTienTe.EditValue & ",ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-TienTe-' + '" & time & "' WHERE ID <>0 "
            If btFilterMaVT.EditValue <> "" Then
                sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
            End If
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                LoadDS()
            End If
            ShowAlert("Đã thực hiện !")
        End If

    End Sub

    Private Sub cbTienTeNK_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTienTeNK.ButtonClick
        If e.Button.Index = 1 Then
            If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
            Dim sql As String = ""
            Dim time As DateTime = GetServerTime()
            sql &= "UPDATE VATTU SET TienTeNK=" & cbTienTeNK.EditValue & ",ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-TienTeNK-' + '" & time & "' WHERE ID <>0 "
            If btFilterMaVT.EditValue <> "" Then
                sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
            End If
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                LoadDS()
            End If
            ShowAlert("Đã thực hiện !")
        End If

    End Sub

    Private Sub btApDungXT_Click(sender As System.Object, e As System.EventArgs) Handles btApDungXT.Click
        If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
        Dim sql As String = ""
        Dim time As DateTime = GetServerTime()
        If chkXuatThue.Checked Then
            sql &= "UPDATE VATTU SET XuatThue1=1,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-XT-' + '" & time & "' WHERE ID <>0 "
        Else
            sql &= "UPDATE VATTU SET XuatThue1=0,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-XT-' + '" & time & "' WHERE ID <>0 "
        End If

        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If btFilterThongSo.EditValue <> "" Then
            sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            LoadDS()
        End If

        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub btApDungNK_Click(sender As System.Object, e As System.EventArgs) Handles btApDungNK.Click
        If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
        Dim sql As String = ""
        Dim time As DateTime = GetServerTime()
        If chkNhapKhau.Checked Then
            sql &= "UPDATE VATTU SET NK=1,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-NK-' + '" & time & "' WHERE ID <>0 "
        Else
            sql &= "UPDATE VATTU SET NK=0,ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-NK-' + '" & time & "' WHERE ID <>0 "
        End If

        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If btFilterThongSo.EditValue <> "" Then
            sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            LoadDS()
        End If

        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub chkTaiAnh_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkTaiAnh.CheckedChanged
        If chkTaiAnh.Checked Then
            chkTaiAnh.Glyph = My.Resources.Checked
        Else
            chkTaiAnh.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub btNangCao_ShowingEditor(sender As System.Object, e As DevExpress.XtraBars.ItemCancelEventArgs) Handles btNangCao.ShowingEditor
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btTinhGiaVTNK_Click(sender As System.Object, e As System.EventArgs) Handles btTinhGiaVTNK.Click
        If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
        Dim sql As String = ""
        Dim time As DateTime = GetServerTime()

        sql &= "UPDATE VATTU SET TienTe1=0,GiaNKBAC = (CASE TNK1 WHEN 1000 THEN 0 ELSE ROUND(((GiaNK+GiaNK*(VCQT/100))*(SELECT TyGiaBAC FROM tblTienTe WHERE tblTienTe.ID=VATTU.TienTeNK)+(GiaNK+GiaNK*(VCQT/100))*(SELECT TyGiaHQ FROM tblTienTe WHERE tblTienTe.ID=VATTU.TienTeNK)*(TNK1/100))*(1+(VCTN/100)),-2) END) WHERE ID <>0 "

        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If btFilterThongSo.EditValue <> "" Then
            sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            LoadDS()
        End If

        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub btNhapTuFile_Click(sender As System.Object, e As System.EventArgs) Handles btNhapTuFile.Click
        If btFilterHangSX.EditValue Is Nothing Then
            ShowCanhBao("Bạn cần chọn hãng sản xuất trước khi tiến hành thao tác này !")
            Exit Sub
        End If

        Dim tbexists As DataTable = ExecuteSQLDataTable(" SELECT count( table_name) FROM information_schema.columns WHERE table_name='tblImportVT'")
        If Not tbexists Is Nothing Then
            If tbexists.Rows(0)(0) > 0 Then
                If ShowCauHoi("Bạn có muốn mở dữ liệu VT nhập gần nhất hay không ?") Then
                    Application.DoEvents()
                    Threading.Thread.Sleep(100)

                    ShowWaiting("Đang chuẩn lại dữ liệu ...")
                    Dim Sql As String = ""

                    ' Đặt trạng thái đã update cho các vật tư trong file excel
                    Sql &= " Update tblImportVT "
                    Sql &= " SET tblImportVT.TrangThai = NULL"

                    If ExecuteSQLNonQuery(Sql) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Application.DoEvents()
                    Threading.Thread.Sleep(100)
                    CloseWaiting()
                    Dim f As New frmImport
                    f.ShowDialog()
                    Exit Sub
                End If
            End If
        End If



        Dim openfile As New OpenFileDialog
        openfile.Filter = "Excel File|*.xls;*.xlsx"
        If openfile.ShowDialog = DialogResult.OK Then
            Dim cnstr As String = ""
            Select Case System.IO.Path.GetExtension(openfile.FileName)
                Case ".xls"
                    cnstr = "Microsoft.Jet.OLEDB.4.0"
                Case ".xlsx"
                    cnstr = "Microsoft.ACE.OLEDB.12.0"
            End Select
            Dim sql1 As String = ""
            Dim sql2 As String = ""
            Dim sql3 As String = ""
            Dim fileUpload As String = "\\192.168.1.109\bac vat tu$\" & IO.Path.GetFileNameWithoutExtension(openfile.FileName) & Now.ToString("yyyyMMddhhmm") & IO.Path.GetExtension(openfile.FileName)
            ShowWaiting("Đang tải file lên máy chủ ...")
            Try
                IO.File.Copy(openfile.FileName, fileUpload)
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
                Exit Sub
            End Try
            CloseWaiting()

            Application.DoEvents()
            Threading.Thread.Sleep(1000)

            sql1 &= " sp_configure 'show advanced options',1    "
            sql1 &= " reconfigure with override    "

            sql2 &= " sp_configure 'Ad Hoc Distributed Queries',1    "
            sql2 &= " reconfigure with override    "

            sql3 &= " if exists (SELECT table_name FROM information_schema.columns WHERE table_name='tblImportVT' ) "
            sql3 &= "     begin "
            sql3 &= "        drop table tblImportVT"
            sql3 &= "     End "

            sql3 &= " SELECT * into tblImportVT "
            sql3 &= " FROM OPENROWSET('" & cnstr & "','Excel 8.0;Database=" & fileUpload & ";IMEX=1','SELECT * FROM [VT$]')"
            sql3 &= " DELETE FROM tblImportVT"
            sql3 &= " WHERE rtrim(ltrim(Model))='' OR Model is null"

            ShowWaiting("Đang xử lý file đầu vào ...")
            Try
                If ExecuteSQLNonQuery(sql1) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                If ExecuteSQLNonQuery(sql2) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                If ExecuteSQLNonQuery(sql3) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                CloseWaiting()
                Dim f As New frmImport
                f.ShowDialog()
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try

            'Dim tb As DataTable = Utils.exportXLS2DataTable.getDataTableFromXLS(openfile.FileName, System.IO.Path.GetExtension(openfile.FileName), "VT")
            'If Not tb Is Nothing Then
            '    Dim tbCol As DataTable = New DataTable
            '    tbCol.Columns.Add("ID", GetType(String))
            '    For i As Integer = 0 To tb.Columns.Count - 1
            '        Dim r As DataRow = tbCol.NewRow
            '        r("ID") = tb.Columns(i).ColumnName
            '        tbCol.Rows.Add(r)
            '    Next
            '    Dim f As New frmImport
            '    f.rcbCotExcel.DataSource = tbCol
            '    f.tbVT = tb
            '    f.ShowDialog()
            'End If
        End If
    End Sub

    Private Sub cbXuatXu_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbXuatXu.ButtonClick
        If e.Button.Index = 1 Then
            If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
            Dim sql As String = ""
            Dim time As DateTime = GetServerTime()
            sql &= "UPDATE VATTU SET IDTennuoc=" & cbXuatXu.EditValue & ",ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-XuatXu-' + '" & time & "' WHERE ID <>0 "
            If btFilterMaVT.EditValue <> "" Then
                sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
            End If
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                LoadDS()
            End If
            ShowAlert("Đã thực hiện !")
        End If
    End Sub

    Private Sub cbDVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbDVT.ButtonClick
        If e.Button.Index = 1 Then
            If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
            Dim sql As String = ""
            Dim time As DateTime = GetServerTime()
            sql &= "UPDATE VATTU SET IDDonViTinh=" & cbDVT.EditValue & ",ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-DVT-' + '" & time & "' WHERE ID <>0 "
            If btFilterMaVT.EditValue <> "" Then
                sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
            End If
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                LoadDS()
            End If
            ShowAlert("Đã thực hiện !")
        End If
    End Sub


    Private Sub rtbModel_Enter(sender As System.Object, e As System.EventArgs) Handles rtbModel.Enter
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If gdvCT.GetFocusedRowCellValue("NX") Then

                ShowCanhBao("Mặt hàng này đã nhập xuất, không thể sửa đổi !")
                rtbModel.ReadOnly = True
            Else
                rtbModel.ReadOnly = False
            End If
        End If

    End Sub

    Private Sub btDoiKT_Click(sender As System.Object, e As System.EventArgs) Handles btDoiKT.Click
        If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub

        Dim sql As String = ""
        Dim time As DateTime = GetServerTime()
        sql &= "UPDATE VATTU SET Model= stuff(Model," & tbTienTo.EditValue.ToString.Length + tbBoQua.EditValue + 1 & "," & tbKTGoc.EditValue.ToString.Length & ",N'" & tbKTThay.EditValue & "'),Code= stuff(Code," & tbTienTo.EditValue.ToString.Length + tbBoQua.EditValue + 1 & "," & tbKTThay.EditValue.ToString.Length & ",N'" & tbKTThay.EditValue & "'),ThayDoi=ThayDoi + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-ThayKT-' + '" & time & "' WHERE ID <>0 "
        If btFilterMaVT.EditValue <> "" Then
            If tbBoQua.EditValue = 0 Then
                sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & tbKTGoc.EditValue.ToString & "%'"
            Else
                sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & "%'"
            End If

        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If btFilterThongSo.EditValue <> "" Then
            sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            LoadDS()
        End If
        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub btTrim_Click(sender As System.Object, e As System.EventArgs) Handles btTrim.Click
        If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub

        Dim sql As String = ""
        Dim time As DateTime = GetServerTime()
        sql &= "UPDATE VATTU SET Model= rtrim(ltrim(Model)),Code=rtrim(ltrim(Code)),ThayDoi=ISNULL(ThayDoi,'') + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-TRIM-' + '" & time & "' WHERE ID <>0 "
        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '" & btFilterMaVT.EditValue.ToString & tbKTGoc.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If btFilterThongSo.EditValue <> "" Then
            sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            LoadDS()
        End If
        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "NX" Then
            If gdvCT.GetRowCellValue(e.RowHandle, "MaLoi") Then
                e.Appearance.BackColor = Color.Red
            ElseIf Not gdvCT.GetRowCellValue(e.RowHandle, "ConSX") Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub btKiemTraTrangThai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKiemTraTrangThai.ItemClick
        gdvCT.Columns("colSuDung").Visible = True
        gdvCT.Columns("colXoa").Visible = True
        For i As Integer = 0 To gdvCT.DataRowCount - 1
            Dim sql As String = ""
            sql &= " SELECT (SELECT count(IDVatTu) FROM YEUCAUDEN WHERE IdVatTu = " & gdvCT.GetRowCellValue(i, "ID") & ") "
            sql &= " + (SELECT count(IDVatTu) FROM CHAOGIA WHERE IdVatTu = " & gdvCT.GetRowCellValue(i, "ID") & " ) "
            sql &= " + (SELECT count(IDVatTu) FROM DATHANG WHERE IdVatTu = " & gdvCT.GetRowCellValue(i, "ID") & " ) "
            sql &= " + (SELECT count(IDVatTu) FROM NHAPKHO WHERE IdVatTu = " & gdvCT.GetRowCellValue(i, "ID") & " ) "
            sql &= " + (SELECT count(IDVatTu) FROM XUATKHO WHERE IdVatTu = " & gdvCT.GetRowCellValue(i, "ID") & " ) "
            gdvCT.SetRowCellValue(i, "colSuDung", False)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows(0)(0) > 0 Then
                    gdvCT.SetRowCellValue(i, "colSuDung", True)
                End If
            End If
        Next
    End Sub

    Private Sub gdvCT_CustomRowCellEdit(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles gdvCT.CustomRowCellEdit
        If e.Column.Name = "colXoa" And gdvCT.GetRowCellValue(e.RowHandle, "colSuDung") = True Then
            e.RepositoryItem = emptyEditor
        End If
    End Sub


    Private Sub gdvCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvCT.CustomDrawCell
        If chkLocMaTrung.Checked Then
            If gdvCT.GetRowCellValue(e.RowHandle, "colXoa") = True And e.Column.FieldName <> "NX" Then
                'e.Appearance.Font = New Font(Me.Font, FontStyle.Strikeout)
                e.Appearance.BackColor = Color.LightPink
            End If
        End If
    End Sub

    Private Sub btXoaChuaSuDung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoaChuaSuDung.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If ShowCauHoi("Xóa các vật tư/ hàng hóa được chọn ?") Then
            Try
                Dim id As String = "("
                For i As Integer = 0 To gdvCT.DataRowCount - 1
                    If gdvCT.GetRowCellValue(i, "colXoa") = True Then
                        id &= gdvCT.GetRowCellValue(i, "ID") & ","
                    End If
                Next

                id &= "-1)"
                If id = "(-1)" Then
                    ShowCanhBao("Chưa có vật tư/ hàng hóa nào được chọn !")
                End If
                BeginTransaction()
                If doDelete("VATTU", "ID IN " & id) Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ComitTransaction()
                ShowAlert("Đã xóa !")
                LoadDS()
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
                RollBackTransaction()
            End Try
        End If
    End Sub

    Private Sub mTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvCT.GetFocusedRowCellValue("ID")
        f._HienThongTinNX = False
        f.ShowDialog()
    End Sub

    Private Sub gdvCT_CalcRowHeight(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs) Handles gdvCT.CalcRowHeight
        If e.RowHandle < 0 Then Exit Sub
        If e.RowHeight > 200 Then
            e.RowHeight = 200
        End If
    End Sub

    Private Sub cbCNNhomVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbCNNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
            Dim sql As String = ""
            Dim time As DateTime = GetServerTime()
            sql &= "UPDATE VATTU SET IDTennhom=" & cbCNNhomVT.EditValue & ",ThayDoi=ISNULL(ThayDoi,'') + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-NhomVT-' + '" & time & "' WHERE ID <>0 "
            If btFilterMaVT.EditValue <> "" Then
                sql &= " AND VATTU.Model LIKE N'" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
            End If
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
                LoadDS()
            End If
            ShowAlert("Đã thực hiện !")
        End If
    End Sub

    Private Sub cbCNTenVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbCNTenVT.ButtonClick
        If e.Button.Index = 1 Then
            If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
            Dim sql As String = ""
            Dim time As DateTime = GetServerTime()
            sql &= "UPDATE VATTU SET IDTenvattu=" & cbCNTenVT.EditValue & ",ThayDoi=ISNULL(ThayDoi,'') + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-TenVT-' + '" & time & "' WHERE ID <>0 "
            If btFilterMaVT.EditValue <> "" Then
                sql &= " AND VATTU.Model LIKE N'" & btFilterMaVT.EditValue.ToString & "%'"
            End If

            If Not btFilterNhomVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
            End If

            If Not btfilterTenVT.EditValue Is Nothing Then
                sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
            End If

            If Not btFilterHangSX.EditValue Is Nothing Then
                sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
            End If

            If btFilterThongSo.EditValue <> "" Then
                sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
            End If
            If ExecuteSQLNonQuery(sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
                LoadDS()
            End If
            ShowAlert("Đã thực hiện !")
        End If
    End Sub
    ' 1,065/0,65

    Private Sub btTinhGiaListOmron_Click(sender As System.Object, e As System.EventArgs) Handles btTinhGiaListOmron.Click
        If Not ShowCauHoi("Tính giá list cho danh sách hàng hóa được chọn ?") Then Exit Sub
        Dim sql As String = ""
        Dim time As DateTime = GetServerTime()
        AddParameterWhere("@HeSo", tbHeSoGiaList.EditValue)
        sql &= "UPDATE VATTU SET TienTe1=0, DonGia1=ROUND(ISNULL(GiaNKBAC,0)*@HeSo,-2) ,ThayDoi=ISNULL(ThayDoi,'') + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-GiaList-' + '" & time & "' WHERE ID <>0 "
        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE N'" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If btFilterThongSo.EditValue <> "" Then
            sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ' loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
            LoadDS()
        End If
        ShowAlert("Đã thực hiện !")
    End Sub

    Private Sub btHuyGiaList_Click(sender As System.Object, e As System.EventArgs) Handles btHuyGiaList.Click
        If Not ShowCauHoi("Thực hiện thao tác này sẽ không thể khôi phục lại được, bạn có chắc là thao tác đúng ?") Then Exit Sub
        Dim sql As String = ""
        Dim time As DateTime = GetServerTime()
        sql &= "UPDATE VATTU SET Dongia1=1,tiente1=0,ThayDoi=ISNULL(ThayDoi,'') + ';' + char(10)+ '" & TaiKhoan.ToString & "' + '-HuyGiaList-' + '" & time & "' WHERE ID <>0 "
        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE N'" & btFilterMaVT.EditValue.ToString & "%'"
        End If

        If Not btFilterNhomVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTennhom= " & btFilterNhomVT.EditValue.ToString
        End If

        If Not btfilterTenVT.EditValue Is Nothing Then
            sql &= " AND VATTU.IDTenvattu=" & btfilterTenVT.EditValue.ToString
        End If

        If Not btFilterHangSX.EditValue Is Nothing Then
            sql &= " AND VATTU.IDHangSanxuat=" & btFilterHangSX.EditValue.ToString
        End If

        If btFilterThongSo.EditValue <> "" Then
            sql &= " AND VATTU.Thongso like '%" & btFilterThongSo.EditValue.ToString & "%' "
        End If
        If ExecuteSQLNonQuery(sql) Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
            LoadDS()
        End If
        ShowAlert("Đã thực hiện !")

    End Sub

    Private Function ShowCauHoi(p1 As String) As Boolean
        Throw New NotImplementedException
    End Function

End Class


