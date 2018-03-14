Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmHangYCXuat
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmCanXuat_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        LoadTuDien()
        btTaiLai.PerformClick()
    End Sub



#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa")
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
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
        sql &= " SELECT BANGCHAOGIA.IDKhachHang,KHACHHANG.ttcMa, 0 as STT,BANGCHAOGIA.IDTakeCare, BANGCHAOGIA.SoPhieu,BANGCHAOGIA.SoPhieu AS SoPhieu2, BANGCHAOGIA.Ngaygiao AS NgayXuat,BANGCHAOGIA.Ngaygiao AS NgayXuat2,  TENVATTU.Ten AS TenVT, "
        sql &= "      TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,CHAOGIA.IDVatTu, CHAOGIA.CanXuat AS SLXuat,"
        sql &= "      (SELECT ISNULL(SUM(Soluong), 0) AS Expr1"
        sql &= "       FROM NHAPKHO"
        sql &= "       WHERE (IDVattu = VATTU.ID)) -"
        sql &= "       (SELECT ISNULL(SUM(Soluong), 0) AS Expr1"
        sql &= "        FROM  XUATKHO"
        sql &= "        WHERE (IDvattu = VATTU.ID)) AS SLTon, "
        sql &= " (select isnull(SUM(CanXuat),0) from CHAOGIA CG2 where CG2.IDVattu=CHAOGIA.IDVatTu) CanXuat,"
        sql &= " 	ISNULL(V_DANGVE.Sluong,0) AS SLVe,V_DANGVE.ngaythang AS NgayVe,V_DANGVE.NgayVe2, BANGCHAOGIA.NgayNhan AS NgayXN,BANGCHAOGIA.NgayNhan AS NgayXN2, BANGCHAOGIA.IDTakeCare,NHANSU.Ten AS TakeCare, VATTU.ID"
        sql &= " ,ThoiGianCanXuat,NoiDungYeuCauKho,Convert(bit,0)Modify,CHAOGIA.ID as IDCG,Convert(bit,0)Chon,TGPhanHoiCuaKho,NoiDungPhanHoiCuaKho,ThoiGianLapYCCX"
        sql &= ",  isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVatTu),0)  "
        sql &= " - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVatTu),0) "
        sql &= " - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu = CHAOGIA.IDVatTu AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu = CHAOGIA.IDVatTu and SlXuatKho > 0)),0) "
        sql &= " as XuatTam"
        sql &= " FROM  CHAOGIA "
        sql &= " 	INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu = CHAOGIA.SoPhieu "

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDKhachhang=" & btfilterMaKH.EditValue
        End If

        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDTakeCare=" & btfilterTakecare.EditValue
        End If

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
        sql &= " WHERE (CHAOGIA.Canxuat <> 0) AND CHAOGIA.ThoiGianCanXuat is not null "

        If cbLocTheo.EditValue = "Yêu cầu mới" Then
            sql &= " AND CHAOGIA.TGPhanHoiCuaKho is null "
        End If

        sql &= " ORDER BY CHAOGIA.ThoiGianCanXuat, BANGCHAOGIA.SoPhieu DESC,CHAOGIA.AZ"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then

            colCXMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
            colCXMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
            colCXPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
            colCXPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
            colCXSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
            colCXSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True

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


    'Private Sub gdvCanXuatCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCanXuatCT.RowCellStyle
    '    On Error Resume Next
    '    'If Not _LoadStyle Then Exit Sub
    '    Select Case e.Column.FieldName
    '        Case "SoPhieu"
    '            If e.CellValue <> "" Then
    '                Dim _wDay As Integer = DateDiff(DateInterval.Day, _ToDay, gdvCanXuatCT.GetRowCellValue(e.RowHandle, "NgayXuat2"))
    '                If _wDay >= 0 And _wDay <= 3 Then
    '                    e.Appearance.BackColor = Color.Yellow
    '                ElseIf _wDay < 0 Then
    '                    e.Appearance.BackColor = Color.Red
    '                End If
    '            End If
    '        Case "SLXuat"
    '            If e.CellValue > gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLTon") + gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLVe") Then
    '                e.Appearance.BackColor = Color.Red
    '            End If
    '        Case "NgayVe"
    '            If Not e.CellValue Is Nothing Then
    '                If IsDBNull(e.CellValue) Or e.CellValue Is Nothing Then
    '                    If (gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLTon") + gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLVe")) < gdvCanXuatCT.GetRowCellValue(e.RowHandle, "SLXuat") Then
    '                        e.Appearance.BackColor = Color.Red
    '                    End If
    '                Else
    '                    If Convert.ToDateTime(e.CellValue).Date > Convert.ToDateTime(gdvCanXuatCT.GetRowCellValue(e.RowHandle, "NgayXuat2")).Date Then
    '                        e.Appearance.BackColor = Color.Red
    '                    ElseIf Convert.ToDateTime(e.CellValue).Date = Convert.ToDateTime(gdvCanXuatCT.GetRowCellValue(e.RowHandle, "NgayXuat2")).Date Then
    '                        e.Appearance.BackColor = Color.Yellow
    '                    End If
    '                End If

    '            End If

    '    End Select

    'End Sub



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

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        gdvCanXuatCT.CloseEditor()
        gdvCanXuatCT.UpdateCurrentRow()

        For i As Integer = 0 To gdvCanXuatCT.RowCount - 1
            If gdvCanXuatCT.GetRowCellValue(i, "Modify") Then
                If IsDBNull(gdvCanXuatCT.GetRowCellValue(i, "ThoiGianCanXuat")) Or gdvCanXuatCT.GetRowCellValue(i, "ThoiGianCanXuat") Is Nothing Then Continue For
                AddParameter("@ThoiGianCanXuat", gdvCanXuatCT.GetRowCellValue(i, "ThoiGianCanXuat"))
                AddParameter("@NoiDungYeuCauKho", gdvCanXuatCT.GetRowCellValue(i, "NoiDungYeuCauKho"))

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


    Private Sub mHoanTatXuLy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mHoanTatXuLy.ItemClick, mHoanTat.ItemClick
        ' For i As Integer = 0 to gdvCanXuatCT.
        gdvCanXuatCT.CloseEditor()
        gdvCanXuatCT.UpdateCurrentRow()
        Dim tg As DateTime = GetServerTime()
        Dim str As String = ""
        For i As Integer = 0 To gdvCanXuatCT.RowCount - 1
            If gdvCanXuatCT.GetRowCellValue(i, "Chon") Then
                str = ""
                If gdvCanXuatCT.GetRowCellValue(i, "NoiDungPhanHoiCuaKho").ToString.Trim <> "" Then
                    AddParameter("@NoiDungPhanHoiCuaKho", gdvCanXuatCT.GetRowCellValue(i, "NoiDungPhanHoiCuaKho"))
                    str = gdvCanXuatCT.GetRowCellValue(i, "NoiDungPhanHoiCuaKho")
                Else
                    AddParameter("@NoiDungPhanHoiCuaKho", "Đã chuẩn bị xong.")
                    str = "Đã chuẩn bị xong."
                End If
                AddParameter("@TGPhanHoiCuaKho", tg)
                AddParameterWhere("@IDD", gdvCanXuatCT.GetRowCellValue(i, "IDCG"))
                If doUpdate("CHAOGIA", "ID=@IDD") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvCanXuatCT.SetRowCellValue(i, "TGPhanHoiCuaKho", tg)
                    If gdvCanXuatCT.GetRowCellValue(i, "NoiDungPhanHoiCuaKho").ToString.Trim = "" Then
                        gdvCanXuatCT.SetRowCellValue(i, "NoiDungPhanHoiCuaKho", "Đã chuẩn bị xong.")
                    End If
                    ThemThongBaoChoNV("CG: " + gdvCanXuatCT.GetRowCellValue(i, "SoPhieu") + "-" + gdvCanXuatCT.GetRowCellValue(i, "ttcMa") + ": Model: " & gdvCanXuatCT.GetRowCellValue(i, "Model") + " " + str, gdvCanXuatCT.GetRowCellValue(i, "IDTakeCare"))
                End If
            End If

        Next
        ShowAlert("Đã cập nhật !")
        btTaiLai.PerformClick()
    End Sub

    Private Sub gdvCanXuatCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCanXuatCT.RowCellClick
        If e.Column.FieldName = "Chon" Then
            gdvCanXuatCT.SetRowCellValue(e.RowHandle, "Chon", Not e.CellValue)
            gdvCanXuatCT.CloseEditor()
            gdvCanXuatCT.UpdateCurrentRow()
        End If
    End Sub
End Class
