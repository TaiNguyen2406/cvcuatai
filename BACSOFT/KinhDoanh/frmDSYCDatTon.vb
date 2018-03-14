Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports BACSOFT.BAC
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO
Imports DevExpress.XtraPrinting
Imports System.Runtime.Serialization
Imports SpreadsheetGear
Imports System.Globalization
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class frmDSYCDatTon

    Private Sub frmDSYCDatTon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim hientai As DateTime = GetServerTime().Date
        tbTuNgay.EditValue = New DateTime(hientai.Year, hientai.Month, 1)
        tbDenNgay.EditValue = hientai
        deTuNgayXK.EditValue = hientai.AddYears(-2)
        deDenNgayXK.EditValue = hientai
        rCbNhanVien.DataSource = ExecuteSQLDataTable("Select ID, Ten from NHANSU where NoiCtac=74")
        loadDSKH()
        rcbTrangThaiDH.DataSource = tableDatTon()
        lueTrangThai.DataSource = tableDatTon()
        If Me.Tag = "frmDSYCDatTon" Then
            '    btNhanVien.EditValue = TaiKhoan
            mnu_ChuyenSangDH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
                cbTrangThai.EditValue = 0
            End If
        Else
            btThem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btXoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnu_Them.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnu_Sua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            mnu_Huy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btDuyetDT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            cbTrangThai.EditValue = 1
            cbTrangThai.Enabled = False
        End If
      
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            '  btNhanVien.Enabled = False
        End If
       
        loadData()
    End Sub

    Private Sub loadData()
        loadGV()
    End Sub
    Private Sub loadDSKH()
        Dim sql As String = ""
        sql &= " SELECT distinct KHACHHANG. * from KHACHHANG left join NHANSU on KHACHHANG .ID=NHANSU .Noictac left join NHANSU TAKECARE on TAKECARE .ID =NHANSU .Chamsoc where 1=1"
        If btNhanVien.EditValue IsNot Nothing Then
            sql &= " and (TAKECARE .ID=@Chamsoc  OR KHACHHANG.IDTakecare is null  )"

            AddParameterWhere("@Chamsoc", btNhanVien.EditValue)
        End If
        sql &= " order by ttcMa"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt IsNot Nothing Then
            riLueMaKH.DataSource = dt
        End If

    End Sub
    Private Sub loadGV()
        AddParameter("@TuNgay", tbTuNgay.EditValue)
        AddParameter("@DenNgay", tbDenNgay.EditValue)
        AddParameter("@TuNgayXK", deTuNgayXK.EditValue)
        AddParameter("@DenNgayXK", deDenNgayXK.EditValue)
        If btNhanVien.EditValue Is Nothing Then
            AddParameter("@IdTakeCare", "")
        Else
            AddParameter("@IdTakeCare", btNhanVien.EditValue)
        End If
        If lueMaKH.EditValue Is Nothing Then
            AddParameter("@IdKhachHang", "")
        Else
            AddParameter("@IdKhachHang", lueMaKH.EditValue)
        End If
        'If cbTrangThai.EditValue Is Nothing Then
        '    AddParameter("@isDuyet", "")
        'Else
        '    AddParameter("@isDuyet", cbTrangThai.EditValue)
        'End If
        AddParameter("@isDuyet", cbTrangThai.EditValue)
        Dim dt As DataTable = ExecutePrcDataTable("prc_DS_DAT_TON")
        If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)
        gc.DataSource = dt
    End Sub

    Private Sub mnu_ChuyenSangDH_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_ChuyenSangDH.ItemClick
        listVTCX = New DataTable
        listVTCX.Columns.Add("IDVatTu", GetType(Integer))
        listVTCX.Columns.Add("SoLuong", GetType(Double))
        Dim count As Integer = 0
        For i As Integer = 0 To gv.RowCount - 1
            If gv.GetRowCellValue(i, "SoLuong") > 0 Then
                Dim r As DataRow = listVTCX.NewRow
                r("IDVatTu") = gv.GetRowCellValue(i, "IdVatTu")
                r("SoLuong") = gv.GetRowCellValue(i, "SoLuong")
                listVTCX.Rows.Add(r)
                count += 1
            End If
        Next
        _LocCXNhomVT = Nothing
        _LocCXHangVT = Nothing
        _LocCXTenVT = Nothing
        _LocCXModelVT = Nothing

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mYeuCauDi_DatHang.Name, DanhMucQuyen.QuyenThem) Or Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mYeuCauDi_DatHang.Name, DanhMucQuyen.QuyenSua) Then
            Exit Sub
        End If

        deskTop.OpenTab("Yêu cầu đi - Đặt hàng", deskTop.mYeuCauDi_DatHang.Tag, New frmYeuCauDi_DatHang, True, Nothing, deskTop.mYeuCauDi_DatHang.Name)
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDi_DatHang).DatTon = True
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDi_DatHang).btCanXuat.PerformClick()


        ShowAlert("Đã lưu lại hàng cần xuất !")
    End Sub

    Private Sub btXem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        loadGV()
    End Sub

    Private Sub gv_MouseDown(sender As Object, e As MouseEventArgs) Handles gv.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gv.CalcHitInfo(gc.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gc.PointToScreen(e.Location))
        Else

        End If
    End Sub

    Private Sub btThem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick
        Dim f As frmCNDatTon = New frmCNDatTon
        TrangThai.isAddNew = True
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
        loadGV()
    End Sub

    Private Sub btSua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick
        If gv.GetFocusedRowCellValue("IdTakeCare") = CType(TaiKhoan, Int32) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            Dim f As frmCNDatTon = New frmCNDatTon
            TrangThai.isUpdate = True
            f._SoPhieu = gv.GetFocusedRowCellValue("SoPhieu")
            f.Tag = Me.Parent.Tag
            f.ShowDialog()
            Dim row = gv.FocusedRowHandle
            loadGV()
            gv.FocusedRowHandle = row
        Else
            ShowCanhBao("Bạn không có quyền sửa yêu cầu của người khác !")
        End If
      
    End Sub
    Private Sub btnXoa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnHuy.ItemClick
        If gv.GetFocusedRowCellValue("IdTakeCare") = CType(TaiKhoan, Int32) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If gv.GetFocusedRowCellValue("isDuyet") = 0 Then
                If ShowCauHoi("Bạn có muốn hủy đặt tồn không ?") Then
                    AddParameter("@isDuyet", 2)
                    AddParameter("@NgayDuyet", DBNull.Value)
                    AddParameterWhere("@Id", gv.GetFocusedRowCellValue("Id"))
                    If doUpdate("DAT_TON_CHI_TIET", "Id=@Id") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        Exit Sub
                    Else
                        ShowAlert("Đã hủy!")
                    End If
                End If

            Else
                If ShowCauHoi("Bạn có muốn bỏ hủy không ?") Then
                    If gv.GetFocusedRowCellValue("isDuyet") = 2 Then
                        AddParameter("@isDuyet", 0)
                        AddParameter("@NgayDuyet", DBNull.Value)
                        AddParameterWhere("@Id", gv.GetFocusedRowCellValue("Id"))
                        If doUpdate("DAT_TON_CHI_TIET", "Id=@Id") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                            Exit Sub
                        Else
                            ShowAlert("Đã bỏ hủy!")
                        End If
                    End If
                End If

            End If
            Dim row = gv.FocusedRowHandle
            loadGV()
            gv.FocusedRowHandle = row
        Else
            ShowCanhBao("Bạn không có quyền xóa yêu cầu của người khác !")
        End If
      
    End Sub
    Private Sub gv_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv.FocusedRowChanged
        gv.Invalidate()
        If gv.GetFocusedRowCellValue("isDuyet") = 2 Then
            btnHuy.Caption = "Bỏ hủy"
        Else
            btnHuy.Caption = "Hủy"
        End If
        mnu_Huy.Caption = btnHuy.Caption
    End Sub
    Private Sub gv_LeftCoordChanged(sender As System.Object, e As System.EventArgs) Handles gv.LeftCoordChanged
        gv.Invalidate()
    End Sub
    Private Sub gc_Paint(sender As Object, e As PaintEventArgs) Handles gc.Paint

        Dim viewInfo As GridViewInfo = gv.GetViewInfo()
        Dim handl As Integer = gv.FocusedRowHandle

        Dim _Top As Integer = handl
        If _Top >= 0 Then
            While gv.GetRowCellValue(_Top, "SoPhieu").ToString() = "" And _Top > 0
                _Top = _Top - 1
            End While

            Dim rowInfo2 As GridRowInfo = viewInfo.GetGridRowInfo(_Top)
            If rowInfo2 Is Nothing Then Exit Sub
            Dim rect2 As System.Drawing.Rectangle = rowInfo2.DataBounds
            Dim pen2 As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.LightPink, 2)
            e.Graphics.DrawLine(pen2, rect2.X, rect2.Y, rect2.X + rect2.Width, rect2.Y)
        End If


        Dim _Bottom As Integer = handl + 1

        If _Bottom >= 0 Then
            If handl = gv.RowCount - 1 Then
                _Bottom = handl
            Else
                While gv.GetRowCellValue(_Bottom, "SoPhieu").ToString() = "" And _Bottom < gv.RowCount - 1
                    _Bottom = _Bottom + 1
                End While
            End If



            Dim rowInfo3 As GridRowInfo = viewInfo.GetGridRowInfo(_Bottom)
            If rowInfo3 Is Nothing Then Exit Sub
            Dim rect3 As System.Drawing.Rectangle = rowInfo3.DataBounds
            Dim pen3 As System.Drawing.Pen = New System.Drawing.Pen(System.Drawing.Brushes.LightPink, 2)
            If _Bottom = handl Then
                e.Graphics.DrawLine(pen3, rect3.X, rect3.Y + rect3.Height, rect3.X + rect3.Width, rect3.Y + rect3.Height)
            Else
                e.Graphics.DrawLine(pen3, rect3.X, rect3.Y, rect3.X + rect3.Width, rect3.Y)
            End If


        End If
    End Sub
    Private Sub rCbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            btNhanVien.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueMaKH.ButtonClick
        If e.Button.Index = 1 Then
            lueMaKH.EditValue = Nothing
        End If
    End Sub
    Private Sub cbTrangThai_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTrangThaiDH.ButtonClick
        If e.Button.Index = 1 Then
            cbTrangThai.EditValue = Nothing
        End If
    End Sub
    Private Sub btDuyetDH_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDuyetDT.ItemClick

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            ShowBaoLoi("Bạn không có quyền thực hiện thao tác này !")
            Exit Sub
        End If





        Try
            Dim tg As DateTime = GetServerTime()
            BeginTransaction()
            Dim sql As String = ""
            If gv.GetFocusedRowCellValue("isDuyet") = 0 Then
                AddParameter("@isDuyet", 1)
                AddParameter("@NgayDuyet", tg)
                AddParameterWhere("@Id", gv.GetFocusedRowCellValue("Id"))
                If doUpdate("DAT_TON_CHI_TIET", "Id=@Id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub

                End If
            Else
                If gv.GetFocusedRowCellValue("isDuyet") = 1 Then
                    AddParameter("@isDuyet", 0)
                    AddParameter("@NgayDuyet", DBNull.Value)
                    AddParameterWhere("@Id", gv.GetFocusedRowCellValue("Id"))
                    If doUpdate("DAT_TON_CHI_TIET", "Id=@Id") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                        Exit Sub
                    End If
                End If

            End If

            ComitTransaction()
            If gv.GetFocusedRowCellValue("isDuyet") = 0 Then
                Dim str As String = "- Đặt tồn: " & gv.GetFocusedRowCellValue("SoPhieu") & " <Br/>"
                str &= "- Vật tư: " & gv.GetFocusedRowCellValue("TenVT") & " <Br/>"
                str &= "- Người duyệt: " & NguoiDung & " <Br/>"
                str &= "- Thời gian: " & tg.ToString("dd/MM/yyyy HH:mm")
                ThemThongBaoChoNV("Đặt tồn: " & gv.GetFocusedRowCellValue("SoPhieu") & "- Vật tư: " & gv.GetFocusedRowCellValue("TenVT") & " đã được duyệt", gv.GetFocusedRowCellValue("IdTakeCare"))
                Utils.Email.Send(gv.GetFocusedRowCellValue("Email"), "Đặt hàng: " & gv.GetFocusedRowCellValue("SoPhieu") & "- Vật tư: " & gv.GetFocusedRowCellValue("TenVT") & " đã được duyệt", str)
            Else
                If gv.GetFocusedRowCellValue("isDuyet") = 1 Then
                    Dim str As String = "- Đặt tồn: " & gv.GetFocusedRowCellValue("SoPhieu") & " <Br/>"
                    str &= "- Vật tư: " & gv.GetFocusedRowCellValue("TenVT") & " <Br/>"
                    str &= "- Người bỏ duyệt: " & NguoiDung & " <Br/>"
                    str &= "- Thời gian: " & tg.ToString("dd/MM/yyyy HH:mm")
                    ThemThongBaoChoNV("Đặt tồn: " & gv.GetFocusedRowCellValue("SoPhieu") & "- Vật tư: " & gv.GetFocusedRowCellValue("TenVT") & " đã bị hủy", gv.GetFocusedRowCellValue("IdTakeCare"))
                    Utils.Email.Send(gv.GetFocusedRowCellValue("Email"), "Đặt hàng: " & gv.GetFocusedRowCellValue("SoPhieu") & "- Vật tư: " & gv.GetFocusedRowCellValue("TenVT") & " đã bị hủy", str)
                End If

            End If



            Dim index As Object = gv.FocusedRowHandle
            gv.FocusedRowHandle = index

            ShowAlert("Đã cập nhật trạng thái !")
            Dim row = gv.FocusedRowHandle
            loadGV()
            gv.FocusedRowHandle = row
        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub mnu_Them_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_Them.ItemClick
        btThem.PerformClick()
    End Sub

    Private Sub mnu_Sua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_Sua.ItemClick
        btSua.PerformClick()
    End Sub
    Private Sub mnu_Huy_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_Huy.ItemClick
        btnHuy.PerformClick()
    End Sub
  
    Private Sub mnu_PhanHoi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_PhanHoi.ItemClick
        btnPhanHoi.PerformClick()
    End Sub
    Private Sub mDeNghiDuyetDatTon_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDeNghiDuyetDatTon.ItemClick
        If gv.GetFocusedRowCellValue("isDuyet") = 0 Then
            Dim f As New frmDatTon_PhanHoi
            f.Text = "Đề nghị duyệt đặt tồn"
            f._EmailPhuTrach = gv.GetFocusedRowCellValue("Email")
            f._NCC = gv.GetFocusedRowCellValue("MaKH")
            f._PhuTrach = gv.GetFocusedRowCellValue("NguoiLap")
            f.lbThongTin.Text = "Thông tin đề nghị duyệt đặt hàng"
            f._SoPhieu = gv.GetFocusedRowCellValue("SoPhieu")
            f._TenVT = gv.GetFocusedRowCellValue("TenVT") & " (" & gv.GetFocusedRowCellValue("Model") & ")"
            f._DeNghi = True
            f.ShowDialog()
        End If
      
    End Sub
    Private Sub btnPhanHoi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPhanHoi.ItemClick
        Dim f As New frmDatTon_PhanHoi
        f._Id = gv.GetFocusedRowCellValue("Id")
        f.Tag = Me.Parent.Tag
        f._DeNghi = False
        f.ShowDialog()
        Dim row = gv.FocusedRowHandle
        loadGV()
        gv.FocusedRowHandle = row
    End Sub

    Private Sub mnu_TinhTrangVT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_TinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gv.GetFocusedRowCellValue("IdVatTu")
        f._HienThongTinNX = True
        f._HienNCC = True
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
                f._HienCGXK = False
            Else
                f._HienCGXK = True
            End If
        Else
            f._HienCGXK = True
        End If
        f.ShowDialog()
    End Sub

    Private Sub mnu_DuyetDT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnu_DuyetDT.ItemClick
        btDuyetDT.PerformClick()
    End Sub

    Private Sub chkLoc_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkLoc.CheckedChanged
        gv.OptionsView.ShowAutoFilterRow = Not gv.OptionsView.ShowAutoFilterRow
    End Sub

   
    Private Sub gv_CellMerge(sender As Object, e As CellMergeEventArgs) Handles gv.CellMerge
        If e.Column.Name = "gcolNguoiLap" Then
            Dim view As GridView = CType(sender, GridView)
            Dim val1 As Object = (view.GetRowCellValue(e.RowHandle1, e.Column.FieldName))
            Dim val2 As Object = (view.GetRowCellValue(e.RowHandle2, e.Column.FieldName))
            If view.GetRowCellValue(e.RowHandle1, "SoPhieu") = view.GetRowCellValue(e.RowHandle2, "SoPhieu") Then
                e.Merge = True
            Else
                e.Merge = False
            End If

            e.Handled = True
        End If
    End Sub
End Class