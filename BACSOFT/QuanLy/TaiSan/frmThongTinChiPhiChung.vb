Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports System.IO
Imports System.Runtime.Serialization
Imports DevExpress.XtraGrid.Views.Grid
Public Class frmThongTinChiPhiChung
    Private Shared query As String
    Private Shared dt As DataTable
    Public check As Integer = 1
    Private Sub loadGV()
        Dim query As String = " SELECT "
        If barCbbXem.EditValue = "Top 500" Then
            query &= "  TOP 500 "
        End If
        query &= " * from("
        query &= " select TaiSan_ChiPhiChung.*, PHIEUXUATKHO.NgayThang, SoLuong, DonGia, SoLuong*DonGia as tongtien,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang, VATTU.Model, VATTU.Thongso AS ThongSo,Congtrinh "
        query &= " from TaiSan_ChiPhiChung inner join XUATKHO on TaiSan_ChiPhiChung.idxuatkho=XUATKHO.ID INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID   where isnull(0,Congtrinh)=0"
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", barDeTuNgay.EditValue)
            AddParameterWhere("@DenNgay", barDeDenNgay.EditValue)
            query &= " AND Convert(datetime,CONVERT(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            query &= " and CheckLuu=0"
        End If
        query &= " union"
        query &= " select TaiSan_ChiPhiChung.*, PHIEUXUATKHO.NgayThang, 1 as SoLuong, BANGCHAOGIA.Tienthucthu as DonGia, BANGCHAOGIA.Tienthucthu as tongtien,TenDuan AS TenVT, null AS TenHang,  null as Model, null AS ThongSo, PHIEUXUATKHO.Congtrinh from TaiSan_ChiPhiChung inner join BANGCHAOGIA on TaiSan_ChiPhiChung.idxuatkho=BANGCHAOGIA.ID inner join PHIEUXUATKHO on PHIEUXUATKHO .SophieuCG =BANGCHAOGIA.Sophieu where PHIEUXUATKHO.Congtrinh=1"
        If barCbbXem.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", barDeTuNgay.EditValue)
            AddParameterWhere("@DenNgay", barDeDenNgay.EditValue)
            query &= " AND Convert(datetime,CONVERT(nvarchar,PHIEUXUATKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            query &= " and CheckLuu=0"
        End If
        'If barLueNhomVT.EditValue IsNot Nothing Then
        '    AddParameterWhere("@IDTennhom", barLueNhomVT.EditValue)
        '    query &= " and IDTennhom=@IDTennhom"
        'End If
        'If barLueHang.EditValue IsNot Nothing Then
        '    AddParameterWhere("@IDHangSanxuat", barLueHang.EditValue)
        '    query &= " and IDHangSanxuat=@IDHangSanxuat"
        'End If
        'If barLueTenVT.EditValue IsNot Nothing Then
        '    AddParameterWhere("@IDTenvattu", barLueTenVT.EditValue)
        '    query &= " and IDTenvattu=@IDTenvattu"
        'End If
        'If barTxtMaVT.EditValue IsNot Nothing Then
        '    query &= " and Model Like N'%" & barTxtMaVT.EditValue.ToString & "%' "
        'End If
        'If barLueLoaiTS.EditValue IsNot Nothing Then
        '    AddParameterWhere("@idloaitaisan", barLueLoaiTS.EditValue)
        '    query &= " and idloaitaisan=@idloaitaisan"
        'End If
        query &= ")tb order by NgayThang asc "
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If Not dt Is Nothing Then
            Dim row = gv.FocusedRowHandle
            gc.DataSource = dt
            gv.FocusedRowHandle = row
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
        gRiLueLoaiTS.DataSource = tableLoaiTS()
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            btnLuu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnLuu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        loadGV()

    End Sub
    Private Sub frmThongTinTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        barCbbXem.EditValue = "Top 500"
        barDeTuNgay.Enabled = False
        barDeDenNgay.Enabled = False
        barDeTuNgay.EditValue = New DateTime(Today.Year, 1, 1)
        barDeDenNgay.EditValue = BAC.GetServerTime()
        loadData()
    End Sub

    Private Sub btnXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXoa.ItemClick
        Dim id = If(gv.GetFocusedRowCellValue("id") Is Nothing, "0", gv.GetFocusedRowCellValue("id"))
        If id <> 0 Then
            If ShowCauHoi("Bạn có muốn hủy chi phí chung: """ + gv.GetFocusedRowCellValue("TenVT").ToString + " không ?") Then
                AddParameterWhere("@id", gv.GetFocusedRowCellValue("id"))
                If doDelete("TaiSan_ChiPhiChung", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    loadData()

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
        If e.KeyCode = Keys.Enter Then
            btnChiTietTaiSan.PerformClick()
        End If
    End Sub

    Private Sub btnChiTietTaiSan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnChiTietTaiSan.ItemClick
        Dim frm = New frmChiTietTaiSan
        frm.Message = gv.GetFocusedRowCellValue("id").ToString()
        Dim row = gv.FocusedRowHandle
        frm.ShowDialog()
        loadGV()
        gv.FocusedRowHandle = row
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
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

    Private Sub gv_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv.RowUpdated
        gv.CloseEditor()
        gv.UpdateCurrentRow()
        AddParameter("@idloaichiphi", gv.GetFocusedRowCellValue("idloaichiphi"))
        AddParameter("@ghichuchiphi", gv.GetFocusedRowCellValue("ghichuchiphi"))
        AddParameter("@thoigiankh", gv.GetFocusedRowCellValue("thoigiankh"))
        AddParameter("@IdBoPhan", gv.GetFocusedRowCellValue("IdBoPhan"))
        ' AddParameter("@CheckLuu", 1)
        AddParameterWhere("@id", gv.GetFocusedRowCellValue("id"))
        If doUpdate("TaiSan_ChiPhiChung", "id=@id") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
        loadGV()
    End Sub

    Private Sub griLueLoaiTS_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gRiLueLoaiTS.EditValueChanged
        gv.PostEditor()
        gv.UpdateCurrentRow()
    End Sub

    Private Sub BarButtonItem6_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem6.ItemClick
        btnXoa.PerformClick()
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        Dim frm = New frmKhauHaoTaiSan()
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        Dim frm = New frmDinhMucKhauHao()
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        Dim frm = New frmNguoiSuDungTS()
        frm.ShowDialog()
        loadGV()
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
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
        For i As Integer = 0 To gv.RowCount - 1
            AddParameter("@CheckLuu", 1)
            AddParameterWhere("@id", gv.GetRowCellValue(i, "id"))
            If doUpdate("TaiSan_ChiPhiChung", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gv.SetRowCellValue(i, "CheckLuu", 1)
            End If
        Next
        '  loadGV()
    End Sub

    Private Sub frmThongTinTaiSan_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If deskTop.tabMain.SelectedTabPage.Text = "Hàng hóa xuất cho Bảo An" Then
            For i As Integer = 0 To gv.RowCount - 1
                If gv.GetRowCellValue(i, "CheckLuu") = 0 Then
                    AddParameterWhere("@CheckLuu", 0)
                    AddParameterWhere("@id", gv.GetRowCellValue(i, "id"))
                    If doDelete("TaiSan_ChiPhiChung", "id=@id and CheckLuu=@CheckLuu") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                End If
            Next
            loadData()
        End If
    End Sub

    Private Sub gvTaiSan_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gv.RowCellStyle
        If e.Column.FieldName = "IdBoPhan" And e.Column.FieldName = "thoigiankh" Then
            e.Appearance.ForeColor = Color.OrangeRed

        End If
    End Sub
End Class