Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmNguonKhachMoi

    Private Sub frmNguonKhachMoi_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        LoadDSNhanSu()
        LoadDSNhom()
        LoadDS()
    End Sub

    Private Sub LoadDSNhanSu()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 AND TrangThai=1")
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            rcbNguoiLap.DataSource = tb
        End If
    End Sub

    Private Sub LoadDSNhom()
        AddParameterWhere("@Loai", LoaiTuDien.NguonKhachMoi)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            rcbNhom.DataSource = tb
        End If
    End Sub

    Public Sub LoadDS()
        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = ""
        sql &= " SELECT tblNguonKhachMoi.ID,tblNguonKhachMoi.ThoiGian,IDNhanVien,IDNguon,tblNguonKhachMoi.NoiDung,"
        sql &= " 	 NHANVIEN.Ten As NhanVien,tblTuDien.NoiDung as Nguon,KHACHHANG.ttcMa,NGD.Ten as NguoiGiaoDich"
        sql &= " FROM tblNguonKhachMoi"
        sql &= " INNER JOIN NHANSU AS NHANVIEN ON NHANVIEN.ID=tblNguonKhachMoi.IDNhanVien"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=tblNguonKhachMoi.IDKhachHang"
        sql &= " INNER JOIN NHANSU AS NGD ON NGD.ID=tblNguonKhachMoi.IDNgd"
        sql &= " INNER JOIN tblTuDien ON tblTuDien.Ma=tblNguonKhachMoi.IDNguon AND tblTuDien.Loai=@Loai"
        sql &= " WHERE Convert(datetime,convert(nvarchar, tblNguonKhachMoi.ThoiGian,103),103) BETWEEN @TuNgay AND @DenNgay"
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        AddParameterWhere("@Loai", LoaiTuDien.NguonKhachMoi)

        If Not cbNguoiLap.EditValue Is Nothing Then
            sql &= " AND tblNguonKhachMoi.IDNhanVien = @IDNV"
            AddParameterWhere("@IDNV", cbNguoiLap.EditValue)
        End If

        If Not cbNhom.EditValue Is Nothing Then
            sql &= " AND tblNguonKhachMoi.IDNguon=@Nhom"
            AddParameterWhere("@Nhom", cbNhom.EditValue)
        End If

        sql &= " ORDER BY ThoiGian"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            
            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub gdv_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdv.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub


    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
    End Sub

    Private Sub pMenuXL_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs)
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvXuLyYC.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
    End Sub


    Private Sub rcbNguoiLap_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNguoiLap.ButtonClick
        If e.Button.Index = 1 Then
            cbNguoiLap.EditValue = Nothing
        End If
    End Sub


    Private Sub btLapYeuCau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThemLienHe.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNNguonKhachMoi
        f.ShowDialog()
    End Sub

    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        LoadDS()
    End Sub


    Private Sub btSuaYeuCau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaLienHe.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If CType(TaiKhoan, Int32) <> CType(gdvCT.GetFocusedRowCellValue("IDNhanVien"), Int32) Then
            ShowCanhBao("Bạn không có quyền sửa yêu cầu của người khác !")
            Exit Sub
        End If
        
        TrangThai.isUpdate = True
        Dim index As Integer = gdvCT.FocusedRowHandle
        Dim f As New frmCNNguonKhachMoi
        f.Tag = Me.Parent.Tag
        objID = gdvCT.GetFocusedRowCellValue("ID")
        f.ShowDialog()
        gdvCT.FocusedRowHandle = index
        gdvCT.SelectRow(index)
    End Sub

    Private Sub rcbNhom_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhom.ButtonClick
        If e.Button.Index = 1 Then
            cbNhom.EditValue = Nothing
        End If
    End Sub


End Class
