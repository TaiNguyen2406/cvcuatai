Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmMHDangVe
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmMHDangVe_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadcbHangSX(Nothing, Nothing)
        LoadDSNhomVT(Nothing, Nothing)
        loadDSTenVT(Nothing, Nothing)
        LoadTuDien()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            gdvDangVeCT.OptionsBehavior.ReadOnly = True
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            btfilterTakecare.Enabled = False
        End If

    End Sub

#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa")
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            btfilterTakecare.EditValue = TaiKhoan

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

    Private Sub LoadDangVe()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SELECT DATHANG.ID, KHACHHANG.ttcMa,PHIEUDATHANG.IDKhachhang,PHIEUDATHANG.SoPhieu,PHIEUDATHANG.SoPhieu AS SoPhieu2, 0 AS STT, TENVATTU.Ten as TenVT, "
        sql &= "     TENHANGSANXUAT.Ten AS HangSX, VATTU.Model,DATHANG.IDVatTu, DATHANG.Cannhap AS SLuong,(DATHANG.DonGia * PHIEUDATHANG.TyGia)DonGia,((DATHANG.DonGia * PHIEUDATHANG.TyGia)*DATHANG.Cannhap)ThanhTien,"
        sql &= "     (SELECT     ISNULL(SUM(Soluong), 0) AS Expr1"
        sql &= "       FROM          dbo.NHAPKHO"
        sql &= "       WHERE      (IDVattu = dbo.VATTU.ID)) -"
        sql &= "     (SELECT     ISNULL(SUM(Soluong), 0) AS Expr1"
        sql &= "       FROM          dbo.XUATKHO"
        sql &= "       WHERE      (IDvattu = dbo.VATTU.ID)) AS SLTon, (CASE WHEN PHIEUDATHANG.LoaiDH=2 THEN PHIEUDATHANG.NgayNhan ELSE ISNULL(DATHANG.NgayVe,PHIEUDATHANG.NgayNhan) END )NgayNhan ,"
        sql &= "     (CASE WHEN DATHANG.NgayVe2 <> DATHANG.NgayVe THEN DATHANG.NgayVe2 ELSE NULL END )NgayVe2,NHANSU.Ten AS TakeCare,DATHANG.GhiChu"
        sql &= " FROM  DATHANG "
        sql &= " 	INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.Sophieu = ISNULL(DATHANG.Sophieu,DATHANG.SoPhieuPhu)"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND PHIEUDATHANG.IDKhachhang=" & btfilterMaKH.EditValue
        End If
        If Not btfilterTakecare.EditValue Is Nothing Then
            sql &= " AND PHIEUDATHANG.IDTakeCare=" & btfilterTakecare.EditValue
        End If
        sql &= " 	INNER JOIN KHACHHANG ON KHACHHANG.ID = PHIEUDATHANG.IDKhachhang"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID = DATHANG.IDvattu"
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
        sql &= " 	LEFT JOIN TENVATTU ON VATTU.IDTenVatTu=TENVATTU.ID"
        sql &= " 	LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangSanXuat"
        sql &= " 	LEFT JOIN NHANSU ON PHIEUDATHANG.IDTakeCare = NHANSU.ID"
        sql &= " WHERE DATHANG.Cannhap <> 0  "
        sql &= " ORDER BY SoPhieu DESC,DATHANG.ID  "

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
                    End If


                Next
            End If
            If chkRutGon.Checked Then
                colDVMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDVMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDVNgayNhan.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDVNgayNhan.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDVPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDVPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                colDVSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                colDVSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            Else
                colDVMaKH.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDVMaKH.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDVNgayNhan.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDVNgayNhan.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDVPhuTrach.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDVPhuTrach.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                colDVSoPhieu.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                colDVSoPhieu.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
            End If

            gdvDangVe.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        If chkRutGon.Checked Then
            gdvDangVeCT.ClearGrouping()
            gdvDangVeCT.ClearSorting()
        End If

        LoadDangVe()

    End Sub

    Private Sub gdvDangVeCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvDangVeCT.RowCellStyle
        On Error Resume Next
        'If Not _LoadStyle Then Exit Sub
        Select Case e.Column.FieldName
            Case "SoPhieu"
                If e.CellValue <> "" Then
                    Dim _wDay As Integer = DateDiff(DateInterval.Day, _ToDay, gdvDangVeCT.GetRowCellValue(e.RowHandle, "NgayNhan"))
                    If _wDay >= 0 And _wDay <= 3 Then
                        e.Appearance.BackColor = Color.Yellow
                    ElseIf _wDay < 0 Then
                        e.Appearance.BackColor = Color.Red
                    End If
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

    Private Sub gdvDangVeCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvDangVeCT.CustomSummaryCalculate

        'If e.IsGroupSummary Then
        '    If CType(e.Item, XtraGrid.GridGroupSummaryItem).FieldName = "DonGia" Then
        '        e.TotalValue = gdvDangVeCT.GetGroupRowValue(e.GroupRowHandle, colDVThanhTien)
        '    End If
        'End If

        'If e.IsTotalSummary Then
        '    If CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName = "DonGia" Then
        '        e.TotalValue = gdvDangVeCT.Columns("ThanhTien").SummaryItem.SummaryValue
        '    End If
        'End If


    End Sub

    Private Sub gdvDangVeCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvDangVeCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvDangVeCT.CalcHitInfo(gdvDangVe.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenuCX.ShowPopup(gdvDangVe.PointToScreen(e.Location))
        End If
    End Sub


    Private Sub mTinhTrangVatTu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVatTu.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvDangVeCT.GetFocusedRowCellValue("IDVatTu")
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            f._HienNCC = False
        End If
        f._HienCGXK = False
        f.ShowDialog()
    End Sub

    Private Sub gdvDangVeCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvDangVeCT.CellValueChanged
        If e.Column.FieldName = "GhiChu" Then
            AddParameter("@GhiChu", e.Value)
            AddParameterWhere("@IDD", gdvDangVeCT.GetRowCellValue(e.RowHandle, "ID"))
            If doUpdate("DATHANG", "ID=@IDD") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm ghi chú")
            End If
        End If
    End Sub

    Private Sub gdvDangVeCT_LostFocus(sender As System.Object, e As System.EventArgs) Handles gdvDangVeCT.LostFocus
        gdvDangVeCT.CloseEditor()
        gdvDangVeCT.UpdateCurrentRow()
    End Sub
End Class
