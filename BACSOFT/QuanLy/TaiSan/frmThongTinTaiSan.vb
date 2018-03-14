Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports System.IO
Imports System.Runtime.Serialization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting

Public Class frmThongTinTaiSan
    Private Shared query As String
    Private Shared dt As DataTable
    Public check As Integer = 1
    Private Sub loadGV()
        Dim hientai As DateTime = GetServerTime()
        Dim query As String = " SELECT "
        If barCbbXem.EditValue = "Top 500" Then
            query &= "  TOP 500 "
        End If
        query &= " id, idvattu, sophieu, idloaitaisan, ghichutaisan, idxuatkho, thoigiankh, IdBoPhan, CheckLuu, IdGop, Gop, isnull(DonGia,GiaTri) DonGia, isnull(TenVT,TenTaiSan) TenVT, isnull(Model,MaTS) Model, isnull(NgayThang,NgayNhap) NgayThang, isnull(TenHang,HangSX) TenHang, isnull(ThongSo,ThongSoTS) ThongSo, isnull(SoLuong,SoLuongTS) SoLuong,SoNgayKH, Congtrinh, (select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@Time and idtaisan =tb.id) SLHong, (SoLuong- (select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@Time and idtaisan =tb.id)) SoLuongConLai,tongtien"
        query &= ", case when SoNgayKH< datediff(day, NgayThang,@time) then 0 else tongtien- ((SoLuong-SoLuongHongDauKy)*DonGia/SoNgayKH*  datediff(day, NgayThang,@time)+ (SoLuongHongDauKy)*DonGia) end as saukh"
        query &= " from("
        query &= " select TaiSan_TaiSan.*, PHIEUXUATKHO.NgayThang, SoLuong, DonGia, SoLuong*DonGia as tongtien,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang, VATTU.Model, VATTU.Thongso AS ThongSo,  datediff(day, NgayThang,DATEADD (month,thoigiankh,NgayThang)) as SoNgayKH,Congtrinh ,(select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id) as SoLuongHongDauKy "
        query &= " from TaiSan_TaiSan inner join XUATKHO on TaiSan_TaiSan.idxuatkho=XUATKHO.ID and isnull(isCT,0)=0 inner JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID inner JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu  LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID   where isnull(0,Congtrinh)=0"
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            query &= " AND Convert(datetime,CONVERT(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            query &= " and CheckLuu=0"
        End If
        query &= " union"
        query &= " select TaiSan_TaiSan.*, PHIEUXUATKHO.NgayThang, 1 as SoLuong, BANGCHAOGIA.TienTruocthue as DonGia, BANGCHAOGIA.TienTruocthue as tongtien,TenDuan AS TenVT, null AS TenHang,  null as Model, null AS ThongSo, datediff(day, PHIEUXUATKHO.NgayThang,DATEADD (month,thoigiankh,PHIEUXUATKHO.NgayThang)) as SoNgayKH, PHIEUXUATKHO.Congtrinh,(select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id) as SoLuongHongDauKy from TaiSan_TaiSan inner join BANGCHAOGIA on TaiSan_TaiSan.idxuatkho=BANGCHAOGIA.ID and isCT=1 inner join PHIEUXUATKHO on PHIEUXUATKHO .SophieuCG =BANGCHAOGIA.Sophieu where PHIEUXUATKHO.Congtrinh=1"
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            query &= " AND Convert(datetime,CONVERT(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            query &= " and CheckLuu=0"
        End If
        query &= " union"
        query &= " select TaiSan_TaiSan.*, NgayNhap NgayThang, SoLuongTS as SoLuong, GiaTri as DonGia,GiaTri*SoLuongTS as tongtien,TenTaiSan AS TenVT, HangSX AS TenHang,  MaTS as Model, ThongSoTS AS ThongSo, datediff(day, NgayNhap,DATEADD (month,thoigiankh,NgayNhap)) as SoNgayKH, CongTrinhTS,(select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id) as SoLuongHongDauKy from TaiSan_TaiSan where sophieu is null"

        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", barDeTuNgay.EditValue)
            AddParameterWhere("@DenNgay", barDeDenNgay.EditValue)
            AddParameterWhere("@time", barDeDenNgay.EditValue)
            query &= " AND Convert(datetime,CONVERT(nvarchar,NgayNhap,103),103) BETWEEN @TuNgay AND @DenNgay "
        Else
            AddParameterWhere("@time", hientai)
        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            query &= " and CheckLuu=0"
        End If

        query &= ")tb where IdGop is null  "
        If barCbbTinhTrang.EditValue = "Còn khấu hao" Then
            query &= " and SoNgayKH>=datediff(day, NgayThang,Getdate())"
        ElseIf barCbbTinhTrang.EditValue = "Khấu hao hết" Then
            query &= " and SoNgayKH<datediff(day, NgayThang,Getdate())"
        End If
        query &= " order by NgayThang desc, Id desc "
        query &= "select "
        If barCbbXem.EditValue = "Top 500" Then
            query &= "  TOP 500 "
        End If
        query &= " id, idvattu, sophieu, idloaitaisan, ghichutaisan, idxuatkho, thoigiankh, IdBoPhan, CheckLuu, IdGop, Gop, isnull(DonGia,GiaTri) DonGia, isnull(TenVT,TenTaiSan) TenVT, isnull(Model,MaTS) Model, isnull(NgayThang,NgayNhap) NgayThang, isnull(TenHang,HangSX) TenHang, isnull(ThongSo,ThongSoTS) ThongSo, isnull(SoLuong,SoLuongTS) SoLuong,SoNgayKH, Congtrinh, (select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@Time and idtaisan =tb.id) SLHong, (SoLuong- (select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@Time and idtaisan =tb.id)) SoLuongConLai,tongtien from("
        query &= " select TaiSan_TaiSan.*, PHIEUXUATKHO.NgayThang, SoLuong, DonGia, SoLuong*DonGia as tongtien,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang, VATTU.Model, VATTU.Thongso AS ThongSo,  datediff(day, NgayThang,DATEADD (month,thoigiankh,NgayThang)) as SoNgayKH,Congtrinh "
        query &= " from TaiSan_TaiSan inner join XUATKHO on TaiSan_TaiSan.idxuatkho=XUATKHO.ID inner JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID inner JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID   where isnull(0,Congtrinh)=0 )tb where IdGop is not null order by NgayThang desc, Id desc"
        '   AddParameter("@Time", barDeTuNgay.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(query)
        gc.DataSource = Nothing
        If Not ds Is Nothing Then
            Dim _relation As New System.Data.DataRelation("table2", ds.Tables(0).Columns("id"), ds.Tables(1).Columns("IdGop"), False)

            ds.Relations.Add(_relation)
            ds.Tables(0).TableName = "table1"
            ds.Tables(1).TableName = "table2"
            '    Dim row = gv.FocusedRowHandle
            gc.DataMember = "table1"
            gc.DataSource = ds.Tables(0)
            '   gv.FocusedRowHandle = row
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
        '  loadLue()
        riLueBoPhan.DataSource = ExecuteSQLDataTable("select * from TAISAN_BOPHAN")
        riLueLoaiTS.DataSource = tableLoaiTS()
        gRiLueLoaiTS.DataSource = ExecuteSQLDataTable("select * from tblTudien where Loai=43")
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            btnLuu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            gv.OptionsBehavior.Editable = True
        Else
            btnLuu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        loadGV()

    End Sub
    Private Sub frmThongTinTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        barCbbXem.EditValue = "Top 500"
        barCbbTinhTrang.EditValue = "Tất cả"
        barDeTuNgay.Enabled = False
        barDeDenNgay.Enabled = False
        barDeTuNgay.EditValue = New DateTime(Today.Year, 1, 1)
        barDeDenNgay.EditValue = BAC.GetServerTime()
        loadData()
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.KeToan) Then
                Exit Sub
            End If
        Else
            If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KeToan) Then
                Exit Sub
            End If
        End If
     
        Dim id = If(gv.GetFocusedRowCellValue("id") Is Nothing, "0", gv.GetFocusedRowCellValue("id"))
        If id <> 0 Then
            If ShowCauHoi("Bạn có muốn xóa tài sản: """ + gv.GetFocusedRowCellValue("TenVT").ToString + " không ?") Then
                AddParameterWhere("@idtaisan", gv.GetFocusedRowCellValue("id"))
                If doDelete("TaiSan_ChiTietTaiSan", "idtaisan=@idtaisan") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    AddParameterWhere("@id", gv.GetFocusedRowCellValue("id"))
                    If doDelete("TaiSan_TaiSan", "id=@id") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        AddParameterWhere("@id", gv.GetFocusedRowCellValue("id"))
                        If doDelete("TaiSan_TaiSan", "IdGop=@id") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            loadData()
                        End If


                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        loadData()
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
    Private Sub riLueLoaiTS_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueLoaiTS.ButtonClick
        If e.Button.Index = 1 Then
            barLueLoaiTS.EditValue = Nothing
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

    Private Sub gvTaiSan_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.Delete Then
            btnXoa.PerformClick()
        End If
        'If e.KeyCode = Keys.Enter Then
        '    btnChiTietTaiSan.PerformClick()
        'End If
    End Sub

    Private Sub btnChiTietTaiSan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnChiTietTaiSan.ItemClick
        Dim frm = New frmChiTietTaiSan
        frm.Tag = Me.Parent.Tag
        frm.Message = gv.GetFocusedRowCellValue("id").ToString()
        Dim row = gv.FocusedRowHandle
        frm.ShowDialog()
        loadGV()
        gv.FocusedRowHandle = row
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        If gv.RowCount < 1 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub BarCheckItem1_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If barCiLoc.Checked = True Then
            gv.OptionsView.ShowAutoFilterRow = True
        Else
            gv.OptionsView.ShowAutoFilterRow = False
        End If
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


    Private Sub barLueNhomVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueNhomVT.EditValueChanged
        loadLue()
    End Sub

    Private Sub barLueHang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueHang.EditValueChanged
        '  loadGV()
        loadLue()
    End Sub

    Private Sub barLueTenVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueTenVT.EditValueChanged
        '  loadGV()
    End Sub

    Private Sub barTxtMaTS_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barTxtMaVT.EditValueChanged
        '   loadGV()
    End Sub

    Private Sub barLueLoaiTS_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueLoaiTS.EditValueChanged
        '   loadGV()
    End Sub

    Private Sub btnTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiLai.ItemClick
        loadData()
    End Sub

    Private Sub barCiLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barCiLoc.CheckedChanged
        If barCiLoc.Checked = True Then
            gv.OptionsView.ShowAutoFilterRow = True
        Else
            gv.OptionsView.ShowAutoFilterRow = False
        End If
    End Sub

    Private Sub gvTaiSan_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv.RowUpdated
        If KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KeToan) Then
            If gv.GetFocusedRowCellValue("CheckLuu") = 1 Then
                gv.CloseEditor()
                gv.UpdateCurrentRow()
                AddParameter("@idloaitaisan", gv.GetFocusedRowCellValue("idloaitaisan"))
                AddParameter("@ghichutaisan", gv.GetFocusedRowCellValue("ghichutaisan"))
                AddParameter("@thoigiankh", gv.GetFocusedRowCellValue("thoigiankh"))
                AddParameter("@IdBoPhan", gv.GetFocusedRowCellValue("IdBoPhan"))
                If IsDBNull(gv.GetFocusedRowCellValue("sophieu")) Then
                   
                End If
                AddParameter("@TenTaiSan", gv.GetFocusedRowCellValue("TenVT"))
                AddParameter("@MaTS", gv.GetFocusedRowCellValue("Model"))
                AddParameter("@NgayNhap", gv.GetFocusedRowCellValue("NgayThang"))
                AddParameter("@HangSX", gv.GetFocusedRowCellValue("TenHang"))
                AddParameter("@ThongSoTS", gv.GetFocusedRowCellValue("ThongSo"))
                AddParameter("@SoLuongTS", gv.GetFocusedRowCellValue("SoLuong"))
                ' AddParameter("@CheckLuu", 1)
                AddParameterWhere("@id", gv.GetFocusedRowCellValue("id"))
                If doUpdate("Taisan_TaiSan", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    Dim r = gv.FocusedRowHandle
                    loadGV()
                    gv.FocusedRowHandle = r
                End If

            End If
        End If
      

    End Sub

    Private Sub griLueLoaiTS_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gRiLueLoaiTS.EditValueChanged
        gv.PostEditor()
        gv.UpdateCurrentRow()
    End Sub

    Private Sub BarButtonItem6_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoa.ItemClick
        btnXoa.PerformClick()
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        Dim frm = New frmKhauHaoTaiSan()
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KeToan) Then
            Exit Sub
        End If
        Dim frm = New frmDinhMucKhauHao()
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnNSD.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            Exit Sub
        End If
        Dim frm = New frmNguoiSuDungTS()
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTSHong.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            Exit Sub
        End If
        Dim frm = New frmTaiSanHong()
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub BarButtonItem11_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXem.ItemClick
        loadGV()
    End Sub

    Private Sub riLueBoPhan_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueBoPhan.ButtonClick
        If e.Button.Index = 1 Then
            gv.SetFocusedRowCellValue("IdBoPhan", DBNull.Value)
        End If
    End Sub

    Private Sub btnLuu_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLuu.ItemClick
        If KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.KeToan) Then
            gv.PostEditor()
            gv.UpdateCurrentRow()

            For i As Integer = 0 To gv.RowCount - 1
                AddParameter("@idloaitaisan", gv.GetRowCellValue(i, "idloaitaisan"))
                AddParameter("@ghichutaisan", gv.GetRowCellValue(i, "ghichutaisan"))
                AddParameter("@thoigiankh", gv.GetRowCellValue(i, "thoigiankh"))
                AddParameter("@IdBoPhan", gv.GetRowCellValue(i, "IdBoPhan"))
                If IsDBNull(gv.GetRowCellValue(i, "sophieu")) Then
                   
                End If
                AddParameter("@TenTaiSan", gv.GetRowCellValue(i, "TenVT"))
                AddParameter("@MaTS", gv.GetRowCellValue(i, "Model"))
                AddParameter("@NgayNhap", gv.GetRowCellValue(i, "NgayThang"))
                AddParameter("@HangSX", gv.GetRowCellValue(i, "TenHang"))
                AddParameter("@ThongSoTS", gv.GetRowCellValue(i, "ThongSo"))
                AddParameter("@SoLuongTS", gv.GetRowCellValue(i, "SoLuong"))
                AddParameter("@GiaTri", gv.GetRowCellValue(i, "DonGia"))
                AddParameter("@CongTrinhTS", gv.GetRowCellValue(i, "Congtrinh"))
                AddParameter("@CheckLuu", 1)
                AddParameterWhere("@id", gv.GetRowCellValue(i, "id"))
                If doUpdate("Taisan_TaiSan", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                Else
                    gv.SetRowCellValue(i, "CheckLuu", 1)

                    If IsDBNull(gv.GetRowCellValue(i, "sophieu")) Then
                        AddParameter("@CheckLuu", 1)
                        AddParameterWhere("@IdGop", gv.GetRowCellValue(i, "id"))
                        If doUpdate("Taisan_TaiSan", "IdGop=@IdGop") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                            Exit Sub
                        Else
                            '  loadGV()
                            ShowAlert("Đã lưu")
                        End If
                    End If

                End If
            Next

        End If

        ' loadGV()
    End Sub

    Private Sub frmThongTinTaiSan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            For i As Integer = 0 To gv.RowCount - 1
                If gv.GetRowCellValue(i, "CheckLuu") = 0 Then
                    AddParameterWhere("@idtaisan", gv.GetRowCellValue(i, "id"))
                    If doDelete("TaiSan_ChiTietTaiSan", "idtaisan=@idtaisan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        AddParameterWhere("@CheckLuu", 0)
                        AddParameterWhere("@id", gv.GetRowCellValue(i, "id"))
                        If doDelete("TaiSan_TaiSan", "id=@id and CheckLuu=@CheckLuu") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            If IsDBNull(gv.GetRowCellValue(i, "sophieu")) Then
                                AddParameterWhere("@CheckLuu", 0)
                                AddParameterWhere("@IdGop", gv.GetRowCellValue(i, "id"))
                                If doDelete("TaiSan_TaiSan", "IdGop=@IdGop and CheckLuu=@CheckLuu") Is Nothing Then
                                    ShowBaoLoi(LoiNgoaiLe)
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
            Next
            '  loadData()
        End If
    End Sub

    Private Sub gvTaiSan_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gv.RowCellStyle
        If e.Column.FieldName = "IdBoPhan" And e.Column.FieldName = "thoigiankh" Then
            e.Appearance.ForeColor = Color.OrangeRed

        End If
        If IsDBNull(gv.GetRowCellValue(e.RowHandle, "sophieu")) Then
            e.Appearance.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub btnKetXuat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKetXuat.ItemClick

        Dim saveDialog As SaveFileDialog = New SaveFileDialog() 'tgchuyentuchaogia

        Try
            saveDialog.FileName = "Danh sách tài sản"
            saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx"

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
                    Case ".xls"
                        Try
                            gv.ExportToXls(exportFilePath, tuychon)
                        Catch ex As Exception
                            ShowBaoLoi(LoiNgoaiLe)
                        End Try

                    Case (".xlsx")
                        Try
                            gv.ExportToXlsx(exportFilePath, tuychonx)
                        Catch ex As Exception
                            ShowBaoLoi(LoiNgoaiLe)
                        End Try

                End Select

                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    '                       
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
            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
            CloseWaiting()
        End Try
    End Sub
  Private Sub btnSua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.KeToan) Then
            Exit Sub
        End If
        gv.OptionsBehavior.Editable = Not gv.OptionsBehavior.Editable
        If btnSua.Caption = "Sửa" Then
            btnSua.Caption = "Đã sửa xong"
        Else
            btnSua.Caption = "Sửa"
        End If
        SendKeys.Send("{Enter}")
    End Sub
    Private Sub gv_HiddenEditor(sender As Object, e As EventArgs) 'Handles gv.HiddenEditor
        If deskTop.tabMain.SelectedTabPage.Text <> "Hàng hóa xuất cho Bảo An" Then
            gv.OptionsBehavior.Editable = False
        End If

    End Sub
    Private Sub gdvCGCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gv.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gv.CalcHitInfo(gc.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gc.PointToScreen(e.Location))
        End If
    End Sub
  
    Private Sub gv_CalcRowHeight(sender As Object, e As RowHeightEventArgs) Handles gv.CalcRowHeight
        If e.RowHeight > 70 Then
            e.RowHeight = 70
        End If
    End Sub
    Private Sub barDeDenNgay_EditValueChanged(sender As Object, e As EventArgs) Handles barDeDenNgay.EditValueChanged
        Dim time As DateTime = barDeDenNgay.EditValue
        barDeTuNgay.EditValue = New DateTime(time.Year, time.Month, 1)
    End Sub
End Class