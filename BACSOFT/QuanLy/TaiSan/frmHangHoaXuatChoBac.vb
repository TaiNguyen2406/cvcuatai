Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Public Class frmHangHoaXuatChoBac
    Private Sub loadGV()
       
        Dim query As String = " SELECT "
        If barCbbXem.EditValue = "Top 500" Then
            query &= "  TOP 500 "
        End If
        query &= " *, isTS isTS2, isCCDC isCCDC2, isCPC isCPC2 from("
        query &= " select PHIEUXUATKHO.NgayThang, IDTenNhom,IDHangSanxuat,IDTenvattu,  PHIEUXUATKHO.CongTrinh,XUATKHO.AZ, XUATKHO.Id ,XUATKHO.SoPhieu, XUATKHO.IDvattu AS IDvattu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,TENDONVITINH.Ten AS TenDVT, SoLuong, DonGia, SoLuong*DonGia as tongtien,XUATKHO.SoHoaDon, cast (case when XUATKHO.IDvattu not in(select idvattu from Taisan_TaiSan where Taisan_TaiSan.idxuatkho=XUATKHO.ID) then 0 else 1 end as bit) as isTS, cast(case when XUATKHO.IDvattu not in(select idvattu from Taisan_CongCuDungCu where Taisan_CongCuDungCu.idxuatkho=XUATKHO.ID) then 0 else 1 end as bit)  as isCCDC, cast(case when XUATKHO.IDvattu not in(select idvattu from Taisan_ChiPhiChung where Taisan_ChiPhiChung.idxuatkho=XUATKHO.ID) then 0 else 1 end as bit)  as isCPC  "
        'query &= ", cast (case when XUATKHO.IDvattu not in(select idvattu from Taisan_TaiSan where Taisan_TaiSan.idxuatkho=XUATKHO.ID) then 0 else 1 end as bit) as isTS2, cast(case when XUATKHO.IDvattu not in(select idvattu from Taisan_CongCuDungCu where Taisan_CongCuDungCu.idxuatkho=XUATKHO.ID) then 0 else 1 end as bit)  as isCCDC2"
        query &= " FROM XUATKHO INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID WHERE IDkhachhang =74  and PHIEUXUATKHO .Congtrinh =0"
        query &= "union select PHIEUXUATKHO.NgayThang, null,null,null, PHIEUXUATKHO.CongTrinh,null AZ, BANGCHAOGIA.ID ,PHIEUXUATKHO.SoPhieu, BANGCHAOGIA.ID AS IDvattu,BANGCHAOGIA.TenDuan AS TenVT,null AS TenHang,null,null AS ThongSo,N'Bộ' AS TenDVT, 1 as SoLuong, BANGCHAOGIA.Tienthucthu, BANGCHAOGIA.Tienthucthu as tongtien,null, cast (case when BANGCHAOGIA.ID not in(select idvattu from Taisan_TaiSan where Taisan_TaiSan.idvattu =BANGCHAOGIA.ID) then 0 else 1 end as bit) as isTS, cast(case when BANGCHAOGIA.ID not in(select idvattu from Taisan_CongCuDungCu where Taisan_CongCuDungCu.idxuatkho=BANGCHAOGIA.ID) then 0 else 1 end as bit)  as isCCDC, cast(case when BANGCHAOGIA.ID not in(select idvattu from Taisan_ChiPhiChung where Taisan_ChiPhiChung.idxuatkho=BANGCHAOGIA.ID) then 0 else 1 end as bit)  as isCPC  FROM PHIEUXUATKHO INNER JOIN BANGCHAOGIA ON PHIEUXUATKHO .SophieuCG =BANGCHAOGIA.Sophieu WHERE PHIEUXUATKHO.IDkhachhang =74 and PHIEUXUATKHO .Congtrinh =1 "
        query &= ")tb where 1=1"
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
           
            AddParameterWhere("@TuNgay", barDeTuNgay.EditValue)
            AddParameterWhere("@DenNgay", barDeDenNgay.EditValue)
            query &= " AND Convert(datetime,CONVERT(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If
        If barLueNhomVT.EditValue IsNot Nothing Then
            AddParameterWhere("@IDTennhom", barLueNhomVT.EditValue)
            query &= " and IDTennhom=@IDTennhom"
        End If
        If barLueHang.EditValue IsNot Nothing Then
            AddParameterWhere("@IDHangSanxuat", barLueHang.EditValue)
            query &= " and IDHangSanxuat=@IDHangSanxuat"
        End If
        If barLueTenVT.EditValue IsNot Nothing Then
            AddParameterWhere("@IDTenvattu", barLueTenVT.EditValue)
            query &= " and IDTenvattu=@IDTenvattu"
        End If
        If barTxtMaVT.EditValue IsNot Nothing Then
            query &= " and Model Like N'%" & barTxtMaVT.EditValue.ToString & "%' "
        End If
        If barCbbTrangThai.EditValue = "Hàng hóa" Then
            query &= " and tb.IDvattu not in(select idvattu from Taisan_TaiSan where Taisan_TaiSan.sophieu=tb.SoPhieu) and tb.IDvattu not in(select idvattu from Taisan_CongCuDungCu where Taisan_CongCuDungCu.sophieu=tb.SoPhieu) and  tb.IDvattu not in(select idvattu from Taisan_ChiPhiChung where Taisan_ChiPhiChung.sophieu=tb.SoPhieu)"
        End If
        If barCbbTrangThai.EditValue = "Tài sản" Then
            query &= " and IDvattu in(select idvattu from Taisan_TaiSan where Taisan_TaiSan.sophieu=tb.SoPhieu)"
        End If
        If barCbbTrangThai.EditValue = "Công cụ, dụng cụ" Then
            query &= " and IDvattu in(select idvattu from Taisan_CongCuDungCu where Taisan_CongCuDungCu.sophieu=tb.SoPhieu)"
        End If
        If barCbbTrangThai.EditValue = "Chi phí chung" Then
            query &= " and IDvattu in(select idvattu from Taisan_ChiPhiChung where Taisan_ChiPhiChung.sophieu=tb.SoPhieu)"
        End If
        query &= " order by tb.NgayThang"
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If Not dt Is Nothing Then
            Dim row = gvHHXuatChoBAC.FocusedRowHandle
            gcHHXuatChoBAC.DataSource = dt
            gvHHXuatChoBAC.FocusedRowHandle = row
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
    Private Sub loadLue()
        riLueNhomVT.DataSource = ExecuteSQLDataTable("select TENNHOM.* from TENNHOM ORDER BY Ten ASC")
        Dim query = "select TENHANGSANXUAT.* from TENHANGSANXUAT inner join VATTU on TENHANGSANXUAT.ID=IDHangSanxuat"
        If barLueNhomVT.EditValue IsNot Nothing Then
            query &= " where IDTennhom=@IDTennhom"
            AddParameterWhere("@IDTennhom", barLueNhomVT.EditValue)
        End If
        query &= " ORDER BY ten ASC"
        riLueHang.DataSource = ExecuteSQLDataTable(query)
        query = "select TENVATTU.* from TENVATTU inner join VATTU on TENVATTU.ID=IDTenvattu inner join TENHANGSANXUAT on TENHANGSANXUAT.ID=IDHangSanxuat where 1=1"
        If barLueNhomVT.EditValue IsNot Nothing Then
            query &= " and IDTennhom=@IDTennhom"
            AddParameterWhere("@IDTennhom", barLueNhomVT.EditValue)
        End If
        If barLueHang.EditValue IsNot Nothing Then
            query &= " and IDHangSanxuat=@IDHangSanxuat"
            AddParameterWhere("@IDHangSanxuat", barLueHang.EditValue)
        End If
        query &= " ORDER BY ten ASC"
        riLueTenVT.DataSource = ExecuteSQLDataTable(query)
    End Sub
    Private Sub loadData()
        loadLue()
        Dim row = gvHHXuatChoBAC.FocusedRowHandle
        loadGV()
        gvHHXuatChoBAC.FocusedRowHandle = row
    End Sub
    Private Sub frmHangHoaXuatChoBac_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        barCbbXem.EditValue = "Top 500"
        barDeTuNgay.Enabled = False
        barDeDenNgay.Enabled = False
        barDeTuNgay.EditValue = New DateTime(Today.Year, 1, 1)
        barDeDenNgay.EditValue = Today.Date
        barCbbTrangThai.EditValue = "Hàng hóa"
        loadData()
    End Sub

    Private Sub barCbbXem_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barCbbXem.EditValueChanged
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            barDeTuNgay.Enabled = True
            barDeDenNgay.Enabled = True
            Exit Sub
        End If
        barDeTuNgay.Enabled = False
        barDeDenNgay.Enabled = False
    End Sub

    Private Sub barDeTuNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeTuNgay.EditValueChanged
        '  loadGV()
    End Sub

    Private Sub barDeDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barDeDenNgay.EditValueChanged
        '  loadGV()
    End Sub

    Private Sub barLueNhomVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueNhomVT.EditValueChanged
        loadLue()
    End Sub

    Private Sub barLueHang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueHang.EditValueChanged
        '  loadGV()
        loadLue()
    End Sub

    Private Sub barLueTenVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueTenVT.EditValueChanged
        '   loadGV()
    End Sub

    Private Sub barTxtMaTS_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barTxtMaVT.EditValueChanged
        '  loadGV()
    End Sub

    Private Sub barCbbTrangThai_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barCbbTrangThai.EditValueChanged
        '   loadGV()
    End Sub

    Private Sub riLueNhomVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            barLueNhomVT.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueHang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueHang.ButtonClick
        If e.Button.Index = 1 Then
            barLueHang.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueTenVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueTenVT.ButtonClick
        If e.Button.Index = 1 Then
            barLueTenVT.EditValue = Nothing
        End If
    End Sub
    Private Sub riCbbTrangThai_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riCbbTrangThai.ButtonClick
        If e.Button.Index = 1 Then
            barCbbTrangThai.EditValue = "Tất cả"
        End If
    End Sub
    Private Sub riDeTuNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riDeTuNgay.ButtonClick
        If e.Button.Index = 1 Then
            barDeTuNgay.EditValue = New DateTime(Today.Year, 1, 1)
        End If
    End Sub
    Private Sub riDeDenNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riDeDenNgay.ButtonClick
        If e.Button.Index = 1 Then
            barDeTuNgay.EditValue = Today
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        loadGV()
    End Sub

    Private Sub riCeTS_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles riCeTS.EditValueChanged
        gvHHXuatChoBAC.PostEditor()
        gvHHXuatChoBAC.UpdateCurrentRow()
       
    End Sub
    Private Sub riCeCCDC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles riCeCCDC.EditValueChanged
        gvHHXuatChoBAC.PostEditor()
        gvHHXuatChoBAC.UpdateCurrentRow()
       
    End Sub

    Private Sub gvHHXuatChoBAC_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gvHHXuatChoBAC.RowUpdated
        gvHHXuatChoBAC.UpdateCurrentRow()
       
    End Sub

    Private Sub barCiLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barCiLoc.CheckedChanged
        If barCiLoc.Checked = True Then
            gvHHXuatChoBAC.OptionsView.ShowAutoFilterRow = True
        Else
            gvHHXuatChoBAC.OptionsView.ShowAutoFilterRow = False
        End If
    End Sub

    Private Sub gvHHXuatChoBAC_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gvHHXuatChoBAC.CellValueChanged
        Dim check As String = ""
        If e.Column.FieldName = "isTS" Then
            If gvHHXuatChoBAC.GetFocusedRowCellValue("isTS") = True Then
                ' check = "select idxuatkho from Taisan_CongCuDungCu  inner join Taisan_ChiTietCCDC  on Taisan_CongCuDungCu.id=Taisan_ChiTietCCDC.idccdc where idxuatkho=@idxuatkho "
                ' AddParameterWhere("@idxuatkho", gvHHXuatChoBAC.GetFocusedRowCellValue("Id"))
                If gvHHXuatChoBAC.GetFocusedRowCellValue("isTS2") = False Then 'ExecuteSQLDataTable(check).Rows.Count < 1 Then
                    gvHHXuatChoBAC.SetFocusedRowCellValue("isCCDC", False)
                Else
                    gvHHXuatChoBAC.SetFocusedRowCellValue("isTS", False)
                End If
            End If
        End If
        If e.Column.FieldName = "isCCDC" Then
            If gvHHXuatChoBAC.GetFocusedRowCellValue("isCCDC") = True Then
                '  check = "select idxuatkho from Taisan_TaiSan inner join Taisan_ChiTietTaiSan on Taisan_TaiSan.id=Taisan_ChiTietTaiSan.idtaisan where idxuatkho=@idxuatkho "
                '  AddParameterWhere("@idxuatkho", gvHHXuatChoBAC.GetFocusedRowCellValue("Id"))
                If gvHHXuatChoBAC.GetFocusedRowCellValue("isCCDC2") = False Then 'ExecuteSQLDataTable(check).Rows.Count < 1 Then
                    gvHHXuatChoBAC.SetFocusedRowCellValue("isTS", False)
                Else
                    gvHHXuatChoBAC.SetFocusedRowCellValue("isCCDC", False)
                End If

            End If
        End If

    End Sub

    Private Sub BarButtonItem6_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        loadGV()
    End Sub

    Private Sub gvHHXuatChoBAC_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gvHHXuatChoBAC.RowCellClick
        
    End Sub


    Private Sub gvHHXuatChoBAC_MouseDown(sender As Object, e As MouseEventArgs) Handles gvHHXuatChoBAC.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gvHHXuatChoBAC.CalcHitInfo(gcHHXuatChoBAC.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gcHHXuatChoBAC.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mnu_ChuyenThanhTS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_ChuyenThanhTS.ItemClick
        AddParameterWhere("@idxuatkho", gvHHXuatChoBAC.GetFocusedRowCellValue("Id"))
        Dim ktTS = "select id from Taisan_TaiSan where idxuatkho=@idxuatkho "
        Dim dtTS = ExecuteSQLDataTable(ktTS)
        For j As Integer = 0 To gvHHXuatChoBAC.RowCount - 1
            If gvHHXuatChoBAC.GetRowCellValue(j, "isTS") = True And gvHHXuatChoBAC.GetRowCellValue(j, "isTS2") = False Then 'And gvHHXuatChoBAC.GetFocusedRowCellValue("isCCDC") = False

                If dtTS.Rows.Count() <= 0 Then
                    AddParameter("@idvattu", gvHHXuatChoBAC.GetRowCellValue(j, "IDvattu"))
                    AddParameter("@idloaitaisan", 2)
                    AddParameter("@sophieu", gvHHXuatChoBAC.GetRowCellValue(j, "SoPhieu"))
                    AddParameter("@idxuatkho", gvHHXuatChoBAC.GetRowCellValue(j, "Id"))
                    AddParameter("@CheckLuu", 0)
                    If doInsert("Taisan_TaiSan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else


                        Dim idtaisan = ExecuteSQLScalar("select top 1 id from TaiSan_TaiSan order by id desc")
                        Dim i As Integer = 1
                        For i = 1 To gvHHXuatChoBAC.GetRowCellValue(j, "SoLuong")
                            AddParameter("@idtaisan", idtaisan)
                            If gvHHXuatChoBAC.GetRowCellValue(j, "SoLuong") > 1 Then
                                AddParameter("@tenchitiettaisan", gvHHXuatChoBAC.GetRowCellValue(j, "TenVT").ToString + " số " + i.ToString())
                            Else
                                AddParameter("@tenchitiettaisan", gvHHXuatChoBAC.GetRowCellValue(j, "TenVT").ToString())
                            End If

                            AddParameter("@idtinhtrang", 1)
                            If doInsert("TaiSan_ChiTietTaiSan") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            End If
                        Next
                    End If
                End If



                'Else
                '    If dtTS.Rows.Count() > 0 Then
                '        Dim idtaisan = dtTS.Rows(0).Item(0)
                '        AddParameterWhere("@idtaisan", idtaisan)
                '        If doDelete("TaiSan_ChiTietTaiSan", "idtaisan=@idtaisan") Is Nothing Then
                '            ShowBaoLoi(LoiNgoaiLe)
                '        Else
                '            AddParameterWhere("@idxuatkho", gvHHXuatChoBAC.GetFocusedRowCellValue("Id"))
                '            If doDelete("Taisan_TaiSan", "idxuatkho=@idxuatkho") Is Nothing Then '"idvattu=@idvattu and sophieu=@sophieu"
                '                ShowBaoLoi(LoiNgoaiLe)
                '            Else
                '                ShowAlert("Đã xóa tài sản ")
                '            End If
                '        End If
                '    End If
            End If
        Next
        ShowAlert("Đã chuyển thành tài sản ")
        Dim row = gvHHXuatChoBAC.FocusedRowHandle
        loadGV()
        gvHHXuatChoBAC.FocusedRowHandle = row
        Dim f As frmThongTinTaiSan = New frmThongTinTaiSan()
        f.check = 0
        f.ShowDialog()
        loadGV()
    End Sub

    Private Sub mnu_ChuyenThanhCCDC_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_ChuyenThanhCCDC.ItemClick
        AddParameterWhere("@idxuatkho", gvHHXuatChoBAC.GetFocusedRowCellValue("Id"))
        Dim ktCCDC = "select id from Taisan_CongCuDungCu where  idxuatkho=@idxuatkho"
        Dim dtCCDC = ExecuteSQLDataTable(ktCCDC)
        For j As Integer = 0 To gvHHXuatChoBAC.RowCount - 1
            If gvHHXuatChoBAC.GetRowCellValue(j, "isCCDC") = True And gvHHXuatChoBAC.GetRowCellValue(j, "isCCDC2") = False Then 'And gvHHXuatChoBAC.GetFocusedRowCellValue("isTS") = False
                If dtCCDC.Rows.Count() <= 0 Then
                    AddParameter("@idvattu", gvHHXuatChoBAC.GetRowCellValue(j, "IDvattu"))
                    AddParameter("@idloaiccdc", 1)
                    AddParameter("@sophieu", gvHHXuatChoBAC.GetRowCellValue(j, "SoPhieu"))
                    AddParameter("@idxuatkho", gvHHXuatChoBAC.GetRowCellValue(j, "Id"))
                    AddParameter("@CheckLuu", 0)
                    If doInsert("Taisan_CongCuDungCu") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else

                        Dim idccdc = ExecuteSQLScalar("select top 1 id from TaiSan_CongCuDungCu order by id desc")
                        Dim i As Integer = 1
                        For i = 1 To gvHHXuatChoBAC.GetRowCellValue(j, "SoLuong")
                            AddParameter("@idccdc", idccdc)
                            If gvHHXuatChoBAC.GetRowCellValue(j, "SoLuong") > 1 Then
                                AddParameter("@tenchitietccdc", gvHHXuatChoBAC.GetRowCellValue(j, "TenVT").ToString + " số " + i.ToString())
                            Else
                                AddParameter("@tenchitietccdc", gvHHXuatChoBAC.GetRowCellValue(j, "TenVT").ToString())
                            End If

                            AddParameter("@idtinhtrang", 1)
                            If doInsert("TaiSan_ChiTietCCDC") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            End If

                        Next
                    End If
                End If

                'Else
                '    If dtCCDC.Rows.Count() > 0 Then
                '        Dim idccdc = dtCCDC.Rows(0).Item(0)
                '        AddParameterWhere("@idccdc", idccdc)
                '        If doDelete("TaiSan_ChiTietCCDC", "idccdc=@idccdc") Is Nothing Then
                '            ShowBaoLoi(LoiNgoaiLe)
                '        Else
                '            AddParameterWhere("@idxuatkho", gvHHXuatChoBAC.GetFocusedRowCellValue("Id"))
                '            If doDelete("Taisan_CongCuDungCu", "idxuatkho=@idxuatkho") Is Nothing Then
                '                ShowBaoLoi(LoiNgoaiLe)
                '            Else
                '                ShowAlert("Đã xóa công cụ, dụng cụ ")
                '            End If
                '        End If
                '    End If
            End If
        Next
        ShowAlert("Đã chuyển thành công cụ dụng cụ ")
        Dim row = gvHHXuatChoBAC.FocusedRowHandle
        loadGV()
        gvHHXuatChoBAC.FocusedRowHandle = row
        Dim f As frmThongTinCCDC = New frmThongTinCCDC()
        f.check = 0
        f.ShowDialog()
        loadGV()
    End Sub

    Private Sub mnu_ChuyenThanhCPC_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_ChuyenThanhCPC.ItemClick
        AddParameterWhere("@idxuatkho", gvHHXuatChoBAC.GetFocusedRowCellValue("Id"))
        Dim ktCPC = "select id from Taisan_ChiPhiChung where  idxuatkho=@idxuatkho"
        Dim dtCPC = ExecuteSQLDataTable(ktCPC)
        For j As Integer = 0 To gvHHXuatChoBAC.RowCount - 1
            If gvHHXuatChoBAC.GetRowCellValue(j, "isCPC") = True And gvHHXuatChoBAC.GetRowCellValue(j, "isCPC2") = False Then
                If dtCPC.Rows.Count() <= 0 Then
                    AddParameter("@idvattu", gvHHXuatChoBAC.GetRowCellValue(j, "IDvattu"))
                    '    AddParameter("@idloaicpc", 1)
                    AddParameter("@sophieu", gvHHXuatChoBAC.GetRowCellValue(j, "SoPhieu"))
                    AddParameter("@idxuatkho", gvHHXuatChoBAC.GetRowCellValue(j, "Id"))
                    AddParameter("@CheckLuu", 0)
                    If doInsert("Taisan_ChiPhiChung") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)

                    End If
                End If

            End If
        Next
        ShowAlert("Đã chuyển thành chi phí ")
        Dim row = gvHHXuatChoBAC.FocusedRowHandle
        loadGV()
        gvHHXuatChoBAC.FocusedRowHandle = row
        Dim f As frmThongTinChiPhiChung = New frmThongTinChiPhiChung()
        f.check = 0
        f.ShowDialog()
        loadGV()
    End Sub

    Private Sub mnu_ChonVatTuCungXuatKho_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_ChonVatTuCungXuatKho.ItemClick
        Dim sophieuxk = gvHHXuatChoBAC.GetFocusedRowCellValue("SoPhieu")
        For i As Integer = 0 To gvHHXuatChoBAC.RowCount - 1
            If gvHHXuatChoBAC.GetRowCellValue(i, "SoPhieu") = sophieuxk And gvHHXuatChoBAC.GetRowCellValue(i, "isCPC2") = False Then
                gvHHXuatChoBAC.SetRowCellValue(i, "isCPC", True)
            End If
        Next
    End Sub

    Private Sub btnThemBoPhan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemBoPhan.ItemClick
        Dim f As frmBoPhanSuDung = New frmBoPhanSuDung
        f.ShowDialog()
    End Sub

    Private Sub gvHHXuatChoBAC_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gvHHXuatChoBAC.RowStyle
        If gvHHXuatChoBAC.GetRowCellValue(e.RowHandle, "isTS2") = True Or gvHHXuatChoBAC.GetRowCellValue(e.RowHandle, "isCCDC2") = True Or gvHHXuatChoBAC.GetRowCellValue(e.RowHandle, "isCPC2") = True Then
            e.Appearance.BackColor = Color.LightPink

        End If
    End Sub

    Private Sub gvHHXuatChoBAC_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gvHHXuatChoBAC.CustomDrawCell
        If e.Column.FieldName = "SoPhieu" Then
            e.Appearance.ForeColor = Color.Green

        End If
    End Sub
End Class