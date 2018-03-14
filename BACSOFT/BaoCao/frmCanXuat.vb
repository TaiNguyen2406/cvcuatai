Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmCanXuat
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmCanXuat_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        LoadTuDien()

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            btfilterTakecare.Enabled = False
        End If

    End Sub

#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa")
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            btfilterTakecare.EditValue = Integer.Parse(TaiKhoan)

            rcbMaKH.DataSource = ds.Tables(1)
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

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            btFilterNhomVT.EditValue = Nothing
            LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
        End If
    End Sub

    Private Sub rcbMaKH_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btfilterMaKH.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbTakecare_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakecare.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTakecare.EditValue = Nothing
        End If
    End Sub

    Private Sub btfilterTenVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btfilterTenVT.EditValueChanged
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterHangSX_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterHangSX.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadDSNhomVT(btFilterHangSX.EditValue, btfilterTenVT.EditValue)
    End Sub

    Private Sub btFilterNhomVT_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btFilterNhomVT.EditValueChanged
        loadDSTenVT(btFilterHangSX.EditValue, btFilterNhomVT.EditValue)
        LoadcbHangSX(btFilterNhomVT.EditValue, btfilterTenVT.EditValue)
    End Sub

#End Region

    Private Sub LoadCanXuat()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SELECT BANGCHAOGIA.IDKhachHang,KHACHHANG.ttcMa, KHACHHANG.ttcMa ttcMa2, 0 as STT, BANGCHAOGIA.SoPhieu, BANGCHAOGIA.SoPhieu AS SoPhieu2, BANGCHAOGIA.Ngaygiao AS NgayXuat,BANGCHAOGIA.Ngaygiao AS NgayXuat2,  TENVATTU.Ten AS TenVT, "
        sql &= "      TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,CHAOGIA.IDVatTu, CHAOGIA.CanXuat AS SLXuat, (CHAOGIA.DonGia * BANGCHAOGIA.TyGia)DonGia,((CHAOGIA.DonGia * BANGCHAOGIA.TyGia)*CHAOGIA.CanXuat)ThanhTien,"
        sql &= "      (SELECT ISNULL(SUM(Soluong), 0) AS Expr1"
        sql &= "       FROM NHAPKHO"
        sql &= "       WHERE (IDVattu = VATTU.ID)) -"
        sql &= "       (SELECT ISNULL(SUM(Soluong), 0) AS Expr1"
        sql &= "        FROM  XUATKHO"
        sql &= "        WHERE (IDvattu = VATTU.ID)) AS SLTon, "
        sql &= " (select isnull(SUM(CanXuat),0) from CHAOGIA CG2 where CG2.IDVattu=CHAOGIA.IDVatTu) CanXuat,"
        sql &= " 	ISNULL(V_DANGVE.Sluong,0) AS SLVe,V_DANGVE.ngaythang AS NgayVe,V_DANGVE.NgayVe2, BANGCHAOGIA.NgayNhan AS NgayXN,BANGCHAOGIA.NgayNhan AS NgayXN2, BANGCHAOGIA.IDTakeCare,NHANSU.Ten AS TakeCare, NHANSU.Ten AS TakeCare2, VATTU.ID"
        sql &= " ,ThoiGianCanXuat,NoiDungYeuCauKho,Convert(bit,0)Modify,CHAOGIA.ID as IDCG,ThoiGianCanXuat as TG2,TGPhanHoiCuaKho,NoiDungPhanHoiCuaKho,BANGYEUCAU.IDNgd,BANGCHAOGIA.IDTakeCare"
        'Tai
        sql &= ",  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVatTu),0)  "
        sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVatTu),0) "
        sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu = CHAOGIA.IDVatTu AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu = CHAOGIA.IDVatTu and SlXuatKho > 0)),0) "
        sql &= " as XuatTam"

        sql &= " ,GhiChuDH, CHAOGIA.NgayCan, BANGCHAOGIA.CongTrinh, Cast(0 as bit) as Tichchon, (select top 1 Idvattu from tblLapYCX where IDVattu = CHAOGIA.IDVatTu  and SoCG = CHAOGIA.Sophieu ) as chkMau"

        'Tai
        sql &= " FROM  CHAOGIA "
        sql &= " 	INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = CHAOGIA.SoPhieu "

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        If Not btfilterTakecare.EditValue Is Nothing   Then
            sql &= " AND BANGCHAOGIA.IDTakeCare=" & btfilterTakecare.EditValue
        End If
        sql &= "    LEFT JOIN BANGYEUCAU ON BANGCHAOGIA.MaSoDatHang=BANGYEUCAU.SoPhieu"
        sql &= " 	INNER JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID = CHAOGIA.IDvattu "

        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '%" & btFilterMaVT.EditValue.ToString & "%'"
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
        sql &= " 	LEFT JOIN NHANSU ON BANGCHAOGIA.IDTakeCare = NHANSU.ID"
        sql &= " 	LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " 	LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " 	LEFT OUTER JOIN V_DANGVE ON V_DANGVE.IDVattu = VATTU.ID"
        'Tai
        ' sql &= "    INNER JOIN DATHANG on V_DANGVE.Sophieu = ISNULL(dbo.DATHANG.Sophieu, dbo.DATHANG.SoPhieuPhu)"
        'Tai
        sql &= " WHERE (CHAOGIA.Canxuat <> 0)"

        sql &= " ORDER BY BANGCHAOGIA.SoPhieu DESC,CHAOGIA.AZ"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                Dim _STT As Integer = 1
                tb.Rows(0)("STT") = _STT
                For i As Integer = 1 To tb.Rows.Count - 1
                    If tb.Rows(i)("SoPhieu") <> tb.Rows(i - 1)("SoPhieu2") Then
                        _STT = 1
                    Else
                        _STT += 1
                    End If
                    tb.Rows(i)("STT") = _STT

                    If _STT <> 1 And chkRutGon.Checked Then
                        tb.Rows(i)("ttcMa") = ""
                        tb.Rows(i)("SoPhieu") = ""
                        tb.Rows(i)("TakeCare") = ""
                        tb.Rows(i)("NgayXN") = DBNull.Value
                        tb.Rows(i)("NgayXuat") = DBNull.Value
                    End If


                Next
            End If
            If chkRutGon.Checked Then
                colCXMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXN.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXN.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXuat.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXuat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                chkTichchon.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            Else
                colCXMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXNgayXN.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXNgayXN.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXNgayXuat.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXuat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                chkTichchon.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
            End If

            gdvCanXuat.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        'If chkRutGon.Checked Then
        '    gdvCanXuatCT.ClearGrouping()
        '    gdvCanXuatCT.ClearSorting()
        'End If

        LoadCanXuat()
    End Sub


    Private Sub gdvCanXuatCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCanXuatCT.RowCellStyle
        On Error Resume Next
        'If Not _LoadStyle Then Exit Sub

        If e.Column.FieldName = "Tichchon" Then
            If gdvCanXuatCT.GetRowCellValue(e.RowHandle, "chkMau").ToString() <> "" Then
                e.Appearance.BackColor =Color.FromArgb(100,180, 224 )
            End If
        End If

        Select Case e.Column.FieldName
            Case "SoPhieu"
                If e.CellValue <> "" Then
                    '   Dim _wDay As Integer = DateDiff(DateInterval.Day, _ToDay, gdvCanXuatCT.GetRowCellValue(e.RowHandle, "NgayXuat2"))
                    Dim _wDay As Integer = DateDiff(DateInterval.Day, _ToDay, gdvCanXuatCT.GetRowCellValue(e.RowHandle, "NgayCan"))
                    If _wDay >= 0 And _wDay <= 3 Then
                        e.Appearance.BackColor = Color.Yellow
                    ElseIf _wDay < 0 Then
                        e.Appearance.BackColor = Color.Red
                    End If
                End If
            Case "SLXuat"
                If e.CellValue > gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLTon") + gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLVe") Then
                    e.Appearance.BackColor = Color.Red
                End If
            Case "NgayVe"
                If Not e.CellValue Is Nothing Then
                    If IsDBNull(e.CellValue) Or e.CellValue Is Nothing Then
                        If (gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLTon") + gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLVe")) < gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLXuat") Then
                            e.Appearance.BackColor = Color.Red
                        End If
                    Else
                        If Convert.ToDateTime(e.CellValue).Date > Convert.ToDateTime(gdvCanXuatCT.GetRowCellValue(e.RowHandle, "NgayCan")).Date Then
                            e.Appearance.BackColor = Color.Red
                        ElseIf Convert.ToDateTime(e.CellValue).Date = Convert.ToDateTime(gdvCanXuatCT.GetRowCellValue(e.RowHandle, "NgayCan")).Date Then
                            e.Appearance.BackColor = Color.Yellow
                        End If
                    End If

                End If
            Case "ttcMa"
                If gdvCanXuatCT.GetRowCellValue(e.RowHandle, "CongTrinh") = 1 Then
                    e.Appearance.BackColor = Color.Orange

                End If
        End Select

    End Sub


    Private Sub chkRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkRutGon.CheckedChanged
        If chkRutGon.Checked = True Then
            chkRutGon.Glyph = My.Resources.Checked
        Else
            chkRutGon.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub gdvCanXuatCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvCanXuatCT.CustomSummaryCalculate

        If e.IsGroupSummary Then
            If CType(e.Item, XtraGrid.GridGroupSummaryItem).FieldName = "DonGia" Then
                e.TotalValue = gdvCanXuatCT.GetGroupRowValue(e.GroupRowHandle, colCXThanhTien)
            End If
        End If

        If e.IsTotalSummary Then
            If CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "DonGia" Then
                e.TotalValue = gdvCanXuatCT.Columns("ThanhTien").SummaryItem.SummaryValue
            End If
        End If

    End Sub

    Private Sub mChuyenSangDH_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChuyenSangDH.ItemClick
        listVTCX = New DataTable
        listVTCX.Columns.Add("IDVatTu", GetType(Integer))
        listVTCX.Columns.Add("SoLuong", GetType(Double))
        Dim count As Integer = 0
        For i As Integer = 0 To gdvCanXuatCT.RowCount - 1
            If gdvCanXuatCT.GetRowCellValue(i, "CanXuat") > gdvCanXuatCT.GetRowCellValue(i, "SLVe") + gdvCanXuatCT.GetRowCellValue(i, "SLTon") Then
                Dim r As DataRow = listVTCX.NewRow
                r("IDVatTu") = gdvCanXuatCT.GetRowCellValue(i, "IDVatTu")
                r("SoLuong") = gdvCanXuatCT.GetRowCellValue(i, "SLXuat")
                listVTCX.Rows.Add(r)
                count += 1
            End If
        Next
        _LocCXNhomVT = btFilterNhomVT.EditValue
        _LocCXHangVT = btFilterHangSX.EditValue
        _LocCXTenVT = btfilterTenVT.EditValue
        _LocCXModelVT = btFilterMaVT.EditValue

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mYeuCauDi_DatHang.Name, DanhMucQuyen.QuyenThem) Or Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mYeuCauDi_DatHang.Name, DanhMucQuyen.QuyenSua) Then
            Exit Sub
        End If

        deskTop.OpenTab("Yêu cầu đi - Đặt hàng", deskTop.mYeuCauDi_DatHang.Tag, New frmYeuCauDi_DatHang, True, Nothing, deskTop.mYeuCauDi_DatHang.Name)
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauDi_DatHang).btCanXuat.PerformClick()
        'CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmThongSo).btTaiLai.PerformClick()

        ShowAlert("Đã lưu lại hàng cần xuất !")
    End Sub

    Private Sub gdvCanXuatCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCanXuatCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCanXuatCT.CalcHitInfo(gdvCanXuat.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenuCX.ShowPopup(gdvCanXuat.PointToScreen(e.Location))
        End If
    End Sub


    Private Sub pMenuCX_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuCX.BeforePopup

        mChuyenSangDH.Visibility = XtraBars.BarItemVisibility.Always

    End Sub

    Private Sub mTinhTrangVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVatTu.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag

        f._IDVatTu = gdvCanXuatCT.GetFocusedRowCellValue("IDVatTu")

        f.ShowDialog()
    End Sub

    Private Sub btLocDo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocDo.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SELECT * FROM("
        sql &= " SELECT CHAOGIA.AZ,BANGCHAOGIA.IDKhachHang,KHACHHANG.ttcMa, KHACHHANG.ttcMa ttcMa2, 0 as STT, BANGCHAOGIA.SoPhieu,BANGCHAOGIA.SoPhieu AS SoPhieu2, BANGCHAOGIA.Ngaygiao AS NgayXuat,BANGCHAOGIA.Ngaygiao AS NgayXuat2,  TENVATTU.Ten AS TenVT, "
        sql &= "      TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,CHAOGIA.IDVatTu, CHAOGIA.CanXuat AS SLXuat, (CHAOGIA.DonGia * BANGCHAOGIA.TyGia)DonGia,((CHAOGIA.DonGia * BANGCHAOGIA.TyGia)*CHAOGIA.CanXuat)ThanhTien,"
        sql &= "      (SELECT ISNULL(SUM(Soluong), 0) AS Expr1"
        sql &= "       FROM NHAPKHO"
        sql &= "       WHERE (IDVattu = VATTU.ID)) -"
        sql &= "       (SELECT ISNULL(SUM(Soluong), 0) AS Expr1"
        sql &= "        FROM  XUATKHO"
        sql &= "        WHERE (IDvattu = VATTU.ID)) AS SLTon, "
        sql &= " (select isnull(SUM(CanXuat),0) from CHAOGIA CG2 where CG2.IDVattu=CHAOGIA.IDVatTu) CanXuat, "
        sql &= " 	ISNULL(V_DANGVE.Sluong,0) AS SLVe,V_DANGVE.ngaythang AS NgayVe, BANGCHAOGIA.NgayNhan AS NgayXN,BANGCHAOGIA.NgayNhan AS NgayXN2, BANGCHAOGIA.IDTakeCare,NHANSU.Ten AS TakeCare,NHANSU.Ten AS TakeCare2, VATTU.ID"
        
        'Tai
        sql &= ",  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVatTu),0)  "
        sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVatTu),0) "
        sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu = CHAOGIA.IDVatTu AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu = CHAOGIA.IDVatTu and SlXuatKho > 0)),0) "
        sql &= " as XuatTam"

        'Tai
        sql &= " ,ThoiGianCanXuat,NoiDungYeuCauKho,Convert(bit,0)Modify,CHAOGIA.ID as IDCG,ThoiGianCanXuat as TG2,TGPhanHoiCuaKho,NoiDungPhanHoiCuaKho,BANGYEUCAU.IDNgd"
        sql &= " ,GhiChuDH, CHAOGIA.NgayCan, BANGCHAOGIA.CongTrinh, Cast(0 as bit) as Tichchon, (select top 1 Idvattu from tblLapYCX where IDVattu = CHAOGIA.IDVatTu and SoCG = CHAOGIA.Sophieu) as chkMau"
        'Tai
        sql &= " FROM  CHAOGIA "
        sql &= " 	INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = CHAOGIA.SoPhieu "
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDTakeCare=" & btfilterTakecare.EditValue
        End If
        sql &= "    LEFT JOIN BANGYEUCAU ON BANGCHAOGIA.MaSoDatHang=BANGYEUCAU.SoPhieu"
        sql &= " 	INNER JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID = CHAOGIA.IDvattu "

        If btFilterMaVT.EditValue <> "" Then
            sql &= " AND VATTU.Model LIKE '%" & btFilterMaVT.EditValue.ToString & "%'"
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
        sql &= " 	LEFT JOIN NHANSU ON BANGCHAOGIA.IDTakeCare = NHANSU.ID"
        sql &= " 	LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " 	LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangSanXuat=TENHANGSANXUAT.ID"
        sql &= " 	LEFT OUTER JOIN V_DANGVE ON V_DANGVE.IDVattu = VATTU.ID"

        sql &= " WHERE (CHAOGIA.Canxuat <> 0)"
        sql &= " )tbl "
        sql &= " WHERE (SLXuat > (ISNULL(SLTon,0)+ ISNULL(SLVe,0))) Or (Convert(datetime,convert(nvarchar,NgayVe,103),103) > Convert(datetime,convert(nvarchar,NgayCan,103),103)) "
        sql &= " ORDER BY SoPhieu DESC,AZ"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                Dim _STT As Integer = 1
                tb.Rows(0)("STT") = _STT
                For i As Integer = 1 To tb.Rows.Count - 1
                    If tb.Rows(i)("SoPhieu") <> tb.Rows(i - 1)("SoPhieu2") Then
                        _STT = 1
                    Else
                        _STT += 1
                    End If
                    tb.Rows(i)("STT") = _STT

                    If _STT <> 1 And chkRutGon.Checked Then
                        tb.Rows(i)("ttcMa") = ""
                        tb.Rows(i)("SoPhieu") = ""
                        tb.Rows(i)("TakeCare") = ""
                        tb.Rows(i)("NgayXN") = DBNull.Value
                        tb.Rows(i)("NgayXuat") = DBNull.Value
                    End If


                Next
            End If
            If chkRutGon.Checked Then
                colCXMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXN.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXN.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXuat.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXuat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            Else
                colCXMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXNgayXN.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXNgayXN.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXNgayXuat.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colCXNgayXuat.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colCXSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colCXSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
            End If

            gdvCanXuat.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Function CheckValid() As Boolean
        Dim flag = False
        Dim temp As New ArrayList

        For i As Integer = 0 To gdvCanXuatCT.RowCount - 1
             If gdvCanXuatCT.GetRowCellValue(i, "Tichchon") Then
                If temp.Count > 0 Then
                    If temp.Contains(gdvCanXuatCT.GetRowCellValue(i, "ttcMa2")) = false Then
                        flag = true ' Đã tồn tại
                    End If
                End If
                temp.Add(gdvCanXuatCT.GetRowCellValue(i, "ttcMa2"))
             End If
        Next

        Return flag
    End Function

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick
        gdvCanXuatCT.CloseEditor()
        gdvCanXuatCT.UpdateCurrentRow()
        Dim tg As DateTime = GetServerTime()
        For i As Integer = 0 To gdvCanXuatCT.RowCount - 1

            If gdvCanXuatCT.GetRowCellValue(i, "Modify") Then
                'If IsDBNull(gdvCanXuatCT.GetRowCellValue(i, "ThoiGianCanXuat")) Or gdvCanXuatCT.GetRowCellValue(i, "ThoiGianCanXuat") Is Nothing Then Continue For
                AddParameter("@ThoiGianCanXuat", gdvCanXuatCT.GetRowCellValue(i, "ThoiGianCanXuat"))
                AddParameter("@NoiDungYeuCauKho", gdvCanXuatCT.GetRowCellValue(i, "NoiDungYeuCauKho"))
                If gdvCanXuatCT.GetRowCellValue(i, "ThoiGianCanXuat").ToString <> gdvCanXuatCT.GetRowCellValue(i, "TG2").ToString Then
                    AddParameter("@ThoiGianLapYCCX", tg)
                End If
                AddParameterWhere("@IDD", gdvCanXuatCT.GetRowCellValue(i, "IDCG"))
                If doUpdate("CHAOGIA", "ID=@IDD") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvCanXuatCT.SetRowCellValue(i, "Modify", False)
                End If
            End If
        Next
        ShowAlert("Đã thực hiện xong !")
    End Sub

    Private Sub gdvCanXuatCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCanXuatCT.CellValueChanged
        If e.Column.FieldName <> "Modify" Then
            gdvCanXuatCT.SetRowCellValue(e.RowHandle, "Modify", True)
            ' gdvCanXuatCT.CloseEditor
        End If
    End Sub

    Private Sub mLapYeuCauXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mLapYeuCauXuat.ItemClick
        'pThoiGianXuat.Location = New Point(Cursor.Position.X, Cursor.Position.Y - 110)
        'pThoiGianXuat.Visible = True
            'AddParameterWhere("@SP", gdvCT.GetFocusedRowCellValue("Sophieu"))
            'Dim tb As DataTable = ExecuteSQLDataTable("SELECT TGKDCanXuat,GhiChuKD FROM BANGCHAOGIA WHERE SoPhieu=@SP")

        'tbThoiGianXuat.EditValue = Now
        'tbGhiChu.EditValue = DBNull.Value
         
        If CheckValid() Then
            ShowCanhBao("Bạn phải chọn chào giá/vật tư của cùng một khách hàng.")
            Exit sub
        End If

         Dim strSoCG = ""
         Dim arraySophieu As New ArrayList
         Dim arrayIDVattu As String = ""
         Dim maKH As String = "", takecare="", idtakecare = "", idkhachhang = ""

         For i As Integer = 0 To gdvCanXuatCT.RowCount - 1
             If gdvCanXuatCT.GetRowCellValue(i, "Tichchon") Then
                If gdvCanXuatCT.GetRowCellValue(i, "SoPhieu2") IsNot Nothing Then
                    If arraySophieu.Contains(gdvCanXuatCT.GetRowCellValue(i, "SoPhieu2")) = False Then
                        arraySophieu.Add(gdvCanXuatCT.GetRowCellValue(i, "SoPhieu2"))
                    End If
                End If
                If gdvCanXuatCT.GetRowCellValue(i, "IDVatTu") IsNot Nothing Then
                    arrayIDVattu = arrayIDVattu & gdvCanXuatCT.GetRowCellValue(i, "IDVatTu") & ","
                End If

                idkhachhang = gdvCanXuatCT.GetRowCellValue(i, "IDKhachHang")
                maKH = gdvCanXuatCT.GetRowCellValue(i, "ttcMa2")
                takecare = gdvCanXuatCT.GetRowCellValue(i, "TakeCare2")
                idtakecare = gdvCanXuatCT.GetRowCellValue(i, "IDTakeCare")
             End If
         Next
        If arraySophieu.Count = 0 Then
            ShowCanhBao("Bạn chưa chọn chào giá/vật tư")
            Exit sub
        End If
        Dim frmLapYCXuat As New frmLapYCXuat
        frmLapYCXuat.arraySophieu = arraySophieu
        frmLapYCXuat.arrayIDVattu = arrayIDVattu
        frmLapYCXuat.maKh = maKh
        frmLapYCXuat.idkhachhang = idkhachhang
        frmLapYCXuat.takecare = takecare
        frmLapYCXuat.idtakecare = idtakecare
        frmLapYCXuat.ShowDialog()
    End Sub

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click

        Dim tg As DateTime = GetServerTime()
        'Dim 
        For i As Integer = 0 To gdvCanXuatCT.SelectedRowsCount - 1
            AddParameter("@ThoiGianCanXuat", tbThoiGianXuat.EditValue)
            If tbThoiGianXuat.EditValue.ToString <> gdvCanXuatCT.GetRowCellValue(gdvCanXuatCT.GetSelectedRows(i), "TG2").ToString Then
                AddParameter("@ThoiGianLapYCCX", tg)
                AddParameter("@TGPhanHoiCuaKho", DBNull.Value)
                AddParameter("@NoiDungPhanHoiCuaKho", DBNull.Value)
            End If
            ' AddParameter("@ThoiGianLapYCCX", tg)
            AddParameter("@NoiDungYeuCauKho", tbGhiChu.EditValue)
            AddParameterWhere("@IDD", gdvCanXuatCT.GetRowCellValue(gdvCanXuatCT.GetSelectedRows(i), "IDCG"))
            If doUpdate("CHAOGIA", "ID=@IDD") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                gdvCanXuatCT.SetRowCellValue(gdvCanXuatCT.GetSelectedRows(i), "ThoiGianCanXuat", tbThoiGianXuat.EditValue)
                gdvCanXuatCT.SetRowCellValue(gdvCanXuatCT.GetSelectedRows(i), "NoiDungYeuCauKho", tbGhiChu.EditValue)
                'ThemThongBaoChoNV()
                ' ShowAlert("Đã cập nhật!")
                'pThoiGianXuat.Visible = False
            End If
        Next
        ShowAlert("Đã cập nhật!")
        pThoiGianXuat.Visible = False
        btTaiLai.PerformClick()
    End Sub

    Private Sub btHuy_Click(sender As System.Object, e As System.EventArgs) Handles btHuy.Click
        pThoiGianXuat.Visible = False
    End Sub

    Private Sub mInDiaChi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mInDiaChi.ItemClick
        If gdvCanXuatCT.FocusedRowHandle < 0 Then Exit Sub
        Dim sql As String = ""
        sql &= " SELECT Ten,ChucVu,Mobile FROM NHANSU WHERE ID=" & gdvCanXuatCT.GetFocusedRowCellValue("IDTakeCare")
        sql &= " SELECT Ten,ChucVu,Mobile FROM NHANSU WHERE ID=" & gdvCanXuatCT.GetFocusedRowCellValue("IDNgd")
        sql &= " SELECT Ten,ISNULL(ttcDCGiaoHang,N'Chưa nhập địa chỉ giao hàng')ttcDCGiaoHang FROM KHACHHANG WHERE ID=74 "
        sql &= " SELECT Ten,ISNULL(ttcDCGiaoHang,N'Chưa nhập địa chỉ giao hàng')ttcDCGiaoHang FROM KHACHHANG WHERE ID=" & gdvCanXuatCT.GetFocusedRowCellValue("IDKhachHang")
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            Dim f As New frmIn("In địa chỉ giao hàng")
            Dim rpt As New rptDiaChiGiaoHang
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.lbTenCTyGui.Text = ds.Tables(2).Rows(0)("Ten")
            rpt.lbDCGui.Text = "Đ/C: " & ds.Tables(2).Rows(0)("ttcDCGiaoHang").ToString
            rpt.lbCtyNhan.Text = ds.Tables(3).Rows(0)("Ten")
            rpt.lbDCNhan.Text = "Đ/C: " & ds.Tables(3).Rows(0)("ttcDCGiaoHang").ToString
            rpt.lbNguoiGui.Text = ds.Tables(0).Rows(0)("Ten") & " " & ds.Tables(0).Rows(0)("Mobile").ToString
            Try
                rpt.lbNguoiNhan.Text = ds.Tables(1).Rows(0)("Ten") & " " & ds.Tables(1).Rows(0)("Mobile").ToString
            Catch ex As Exception
                ShowBaoLoi("Không lấy được thông tin người nhận !")
            End Try

            rpt.CreateDocument()
            'f.PrintPreviewBarItem12.
            f.printControl.PrintingSystem = rpt.PrintingSystem

            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
End Class
