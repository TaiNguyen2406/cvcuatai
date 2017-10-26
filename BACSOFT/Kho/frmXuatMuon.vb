Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmXuatMuon
    Dim _exit As Boolean = False

    Private Sub frmXuatMuon_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        _exit = True
        'tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
        'tbDenNgay.EditValue = tg
        _exit = False
        LoadDSNhanVien()
        btTaiDS.PerformClick()
    End Sub

    Public Sub LoadDSNhanVien()
        On Error Resume Next
        Dim sql As String = ""
        sql = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 AND TrangThai=1 "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbNhanVien.DataSource = tb
            ' rcbNV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rtbTuNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbTuNgay.ButtonClick
        If e.Button.Index = 1 Then
            tbTuNgay.EditValue = Nothing
        End If
    End Sub

    Private Sub rtbDenNgay_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rtbDenNgay.ButtonClick
        If e.Button.Index = 1 Then
            tbDenNgay.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT tblXuatMuon.ID,IDVatTu,SoLuong,"
        sql &= " 	(CASE tblXuatMuon.TrangThai  WHEN 1 THEN (Datediff(day,thoigianmuon,ISNULL(ThoiGianTra,getdate()))-7) ELSE (Datediff(day,thoigianmuon,getdate())-7) END) AS QuaHan,"
        sql &= "    IDNguoiXuat,IDNguoiMuon,NGUOIMUON.Ten AS NguoiMuon,tblXuatMuon.IDVatTu,"
        sql &= " 	ThoiGianMuon,ThoiGianTra,TinhTrangMuon,TinhTrangTra,VATTU.Model,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX,"
        sql &= " 	(CASE tblXuatMuon.TrangThai WHEN 0 THEN N'Đang mượn' WHEN 1 THEN N'Đã trả' WHEN 2 THEN N'Thất lạc' END) AS TrangThai,tblXuatMuon.GhiChu,tblXuatMuon.GhiChuKD,tblXuatMuon.SoPhieuCG"
        sql &= " FROM tblXuatMuon"
        sql &= " INNER JOIN NHANSU AS NGUOIMUON ON NGUOIMUON.ID=tblXuatMuon.IDNguoiMuon AND NGUOIMUON.Noictac=74"
        sql &= " INNER JOIN VATTU ON VATTU.ID=tblXuatMuon.IDVatTu"
        sql &= " LEFT JOIN TENVATTU ON TENVATTU.ID=VATTU.IDTenVatTu"
        sql &= " LEFT JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID=VATTU.IDHangSanXuat"
        sql &= " WHERE 1=1 "

        If tbTuNgay.EditValue Is Nothing And Not tbDenNgay.EditValue Is Nothing Then
            sql &= " AND convert(datetime,Convert(nvarchar, tblXuatMuon.ThoiGianMuon,103),103) <= @DenNgay "
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        ElseIf Not tbTuNgay.EditValue Is Nothing And tbDenNgay.EditValue Is Nothing Then
            sql &= " AND convert(datetime,Convert(nvarchar, tblXuatMuon.ThoiGianMuon,103),103) >= @TuNgay "
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        ElseIf Not tbTuNgay.EditValue Is Nothing And Not tbDenNgay Is Nothing Then
            sql &= " AND convert(datetime,Convert(nvarchar, tblXuatMuon.ThoiGianMuon,103),103) Between @TuNgay And @DenNgay "
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If

        Select Case cbTrangThai.EditValue
            Case "Đang mượn"
                sql &= " AND tblXuatMuon.TrangThai=0 "
            Case "Đã trả"
                sql &= " AND tblXuatMuon.TrangThai=1 "
            Case "Thất lạc"
                sql &= " AND tblXuatMuon.TrangThai=2 "
        End Select

        If Not cbNhanVien.EditValue Is Nothing Then
            sql &= " AND tblXuatMuon.IDNguoiMuon =@NV "
            AddParameterWhere("@NV", cbNhanVien.EditValue)
        End If

        sql &= " ORDER BY ThoiGianMuon "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        CloseWaiting()
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThem.ItemClick
        'If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        'TrangThai.isAddNew = True
        'Dim f As New frmCNMuonVT
        'f.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        ''If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub

        'If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        'TrangThai.isUpdate = True
        ''If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And gdvCT.GetFocusedRowCellValue("TrangThai") = "Đã trả" Then
        ''    ShowCanhBao("Vật tư đã trả, không được sửa đổi !")
        ''    Exit Sub
        ''End If
        'objID = gdvCT.GetFocusedRowCellValue("ID")
        'Dim f As New frmCNMuonVT
        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
        '    f.tbThoiGianMuon.Enabled = False
        '    f.tbTinhTrangMuon.Enabled = False
        '    f.tbModel.Enabled = False
        '    f.gdvVatTu.Enabled = False
        '    f.btTimVatTu.Enabled = False
        '    f.tbSoChaoGia.Enabled = False
        '    f.tbSoLuong.Enabled = False
        '    If gdvCT.GetFocusedRowCellValue("TrangThai") = "Đã trả" Then
        '        f.tbThoiGianTra.Enabled = False
        '        f.tbTinhTrangTra.Enabled = False
        '        f.tbSoChaoGia.Enabled = False
        '        f.cbTrangThai.Enabled = False
        '    End If

        '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
        '        If gdvCT.GetFocusedRowCellValue("IDNguoiMuon") <> TaiKhoan Then
        '            ShowCanhBao("Bạn không được phép sửa nội dung mượn của người khác!")
        '            Exit Sub
        '        End If
        '        f.tbThoiGianTra.Enabled = False
        '        f.tbTinhTrangTra.Enabled = False
        '        f.tbGhiChu.Enabled = False
        '        f.tbSoChaoGia.Enabled = False
        '        f.cbNguoiMuon.Enabled = False
        '        f.tbSoLuong.Enabled = False
        '        f.cbTrangThai.Enabled = False
        '    End If

        'End If
        'f.ShowDialog()
    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mTinhTrangVT_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mTinhTrangVT.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvCT.GetFocusedRowCellValue("IDVatTu")
        f.ShowDialog()
    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        If e.Column.FieldName = "QuaHan" Then
            If e.CellValue > 0 Then
                e.Appearance.BackColor = Color.Red
            End If
        End If
    End Sub
End Class
