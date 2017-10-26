Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmPhieuDanhGiaNV
    Public _exit As Boolean = True
    Public CapQuanLy As Byte = 0
    Private Sub frmPhieuDanhGiaNV_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _exit = True
        cbNguoiDanhGia.Enabled = False
        cbCapQuanLy.Enabled = False
        tbThoiGian.EditValue = GetServerTime()
        LoadNVDanhGia()
        cbNguoiDanhGia.EditValue = CType(TaiKhoan, Integer)

        LoadCbBoPhan()
        LoadDefaultBP()

        LoadGdvData()
        _exit = False

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKyThuat) Then
            Me.Text &= " (Quản lý)"
            CapQuanLy = 1
            cbCapQuanLy.SelectedIndex = 1
        ElseIf KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) And TaiKhoan = 1 Then
            Me.Text &= " (Giám đốc)"
            CapQuanLy = 2
            cbCapQuanLy.SelectedIndex = 2
        End If
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.KiemDuyet) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            cbNguoiDanhGia.Enabled = True
            cbCapQuanLy.Enabled = True
        End If

    End Sub

    Private Sub LoadNVDanhGia()
        Dim sql As String = "SELECT NHANSU.ID,NHANSU.Ten,LUONG.NhomKN FROM LUONG INNER JOIN NHANSU ON NHANSU.ID=LUONG.IDNhanVien  "
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) And TaiKhoan = 1 Then
            sql &= " WHERE (LUONG.[Month]=@Thang AND LUONG.IDDepatment=2) OR (LUONG.[Month]=@Thang AND LUONG.IDNhanVien=1) "
        Else
            sql &= " WHERE LUONG.[Month]=@Thang AND LUONG.IDDepatment=2 "
        End If

        AddParameterWhere("@Thang", Convert.ToDateTime(tbThoiGian.EditValue).ToString("MM/yyyy"))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbNguoiDanhGia.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDefaultBP()
        AddParameterWhere("@IDNV", cbNguoiDanhGia.EditValue)
        AddParameterWhere("@Thang", Convert.ToDateTime(tbThoiGian.EditValue).ToString("MM/yyyy"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT NhomKN FROM LUONG WHERE [Month]=@Thang AND IDNhanVien=@IDNV")

        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If Not IsDBNull(tb.Rows(0)(0)) Then
                    cbBoPhan.EditValue = tb.Rows(0)(0)
                End If

            End If

        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Public Sub LoadCbBoPhan()
        AddParameterWhere("@Loai", LoaiTuDien.NhomNoiDungThiCong)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            cbBoPhan.Properties.DataSource = tb
            'If tb.Rows.Count > 0 Then

            'End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadGdvData()
        Dim sql As String = ""


        sql &= " SELECT  KTDiemDanhGiaNV.ID, KTDiemDanhGiaNV.IDNguoiDanhGia,KTDiemDanhGiaNV.IDBoPhan,Thang,"
        sql &= " IDNhanVien,ChuDongHTCongViec,ChuDongTimViecDeLam,LinhHoatBietViecDeLam, CapQuanLy,NHANSU.Ten as TenNV,Convert(bit,0)Modify"
        sql &= " INTO #tbKTDiemDanhGiaNV"
        sql &= " FROM KTDiemDanhGiaNV "
        sql &= " LEFT JOIN NHANSU ON KTDiemDanhGiaNV.IDNhanVien=NHANSU.ID "
        sql &= " WHERE KTDiemDanhGiaNV.IDBoPhan=@BoPhan AND Thang=@Thang AND IDNguoiDanhGia=@IDNV AND NhomHoTro=0"
        If CapQuanLy <> 2 Then
            sql &= " AND CapQuanLy<>2"
        Else
            sql &= " AND CapQuanLy=2"
        End If

        sql &= " IF EXISTS(SELECT KTDiemDanhGiaNV.*,N'Quản lý' as TenNV FROM KTDiemDanhGiaNV WHERE CapQuanLy =(@CapQuanLy +1)  AND IDNhanVien=@IDNV AND Thang=@Thang)"
        sql &= " BEGIN"
        sql &= "    SELECT KTDiemDanhGiaNV.ID, KTDiemDanhGiaNV.IDNguoiDanhGia,KTDiemDanhGiaNV.IDBoPhan,KTDiemDanhGiaNV.Thang,"
        sql &= "    IDNhanVien,ChuDongHTCongViec,ChuDongTimViecDeLam,LinhHoatBietViecDeLam, CapQuanLy,"
        sql &= "    N'Quản lý' as TenNV,Convert(bit,0)Modify "
        sql &= "    FROM KTDiemDanhGiaNV WHERE CapQuanLy =(@CapQuanLy +1) "
        sql &= "    AND IDNguoiDanhGia=@IDNV AND Thang=@Thang AND NhomHoTro=0"
        sql &= "    UNION ALL "
        sql &= "    SELECT * FROM #tbKTDiemDanhGiaNV"
        sql &= "    UNION ALL"
        sql &= "    SELECT  Convert(bigint,null)ID,0 as IDNguoiDanhGia,LUONG.NhomKN as IDBoPhan,LUONG.[Month] as Thang,"
        sql &= "    IDNhanVien,Convert(float,0)as ChuDongHTCongViec, Convert(float,0)as ChuDongTimViecDeLam, Convert(float,0)as LinhHoatBietViecDeLam,"
        sql &= "    @CapQuanLy as CapQuanLy,NHANSU.Ten as TenNV,Convert(bit,0)Modify"
        sql &= "    FROM LUONG "
        sql &= "    INNER JOIN NHANSU ON LUONG.IDNhanVien=NHANSU.ID "
        sql &= "    WHERE NhomKN=@BoPhan AND [Month]=@Thang AND LUONG.IDDepatment=2 AND LUONG.IDNhanVien NOT IN (SELECT IDNhanVien FROM #tbKTDiemDanhGiaNV)"
        If CapQuanLy = 2 Then
            sql &= " AND Upper(replace(NHANSU.MaTruyCap,' ',''))=N'PKTTRUONGPHONG'"
        End If
        sql &= " End"
        sql &= " ELSE"
        sql &= " BEGIN"
        sql &= "    SELECT 0 as ID, 0 as IDNguoiDanhGia,0 as IDBoPhan,N'' as Thang,0 as IDNhanVien,"
        sql &= "    Convert(float,null) as ChuDongHTCongViec,Convert(float,null) as ChuDongTimViecDeLam,Convert(float,null) as LinhHoatBietViecDeLam,(@CapQuanLy+1) as CapQuanLy,"
        sql &= "    N'Quản lý' as TenNV,Convert(bit,0)Modify "
        sql &= "    UNION ALL "
        sql &= "    SELECT * FROM #tbKTDiemDanhGiaNV"
        sql &= "    UNION ALL"
        sql &= "    SELECT  Convert(bigint,null)ID,0 as IDNguoiDanhGia,LUONG.NhomKN as IDBoPhan,LUONG.[Month] as Thang,"
        sql &= "    IDNhanVien,Convert(float,0)as ChuDongHTCongViec, Convert(float,0)as ChuDongTimViecDeLam, Convert(float,0)as LinhHoatBietViecDeLam,"
        sql &= "    @CapQuanLy as CapQuanLy,NHANSU.Ten as TenNV,Convert(bit,0)Modify"
        sql &= "    FROM LUONG "
        sql &= "    INNER JOIN NHANSU ON LUONG.IDNhanVien=NHANSU.ID "
        sql &= "    WHERE NhomKN=@BoPhan AND [Month]=@Thang AND LUONG.IDDepatment=2 AND LUONG.IDNhanVien NOT IN (SELECT IDNhanVien FROM #tbKTDiemDanhGiaNV)"
        If CapQuanLy = 2 Then
            sql &= " AND Upper(replace(NHANSU.MaTruyCap,' ',''))=N'PKTTRUONGPHONG'"
        End If
        sql &= " End"

        sql &= " DROP table #tbKTDiemDanhGiaNV"

        AddParameterWhere("@IDNV", cbNguoiDanhGia.EditValue)
        AddParameterWhere("@Thang", Convert.ToDateTime(tbThoiGian.EditValue).ToString("MM/yyyy"))
        AddParameterWhere("@BoPhan", cbBoPhan.EditValue)
        AddParameterWhere("@CapQuanLy", cbCapQuanLy.SelectedIndex)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btTaiLai_Click(sender As System.Object, e As System.EventArgs) Handles btTaiLai.Click
        LoadGdvData()
    End Sub

    Private Sub gdvCT_ShowingEditor(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles gdvCT.ShowingEditor
        On Error Resume Next
        If gdvCT.FocusedRowHandle = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btLuuLai_Click(sender As System.Object, e As System.EventArgs) Handles btLuuLai.Click
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        For i As Integer = 0 To gdvCT.RowCount - 1
            If gdvCT.GetRowCellValue(i, "Modify") Then
                If gdvCT.GetRowCellValue(i, "ChuDongHTCongViec") = 0 And gdvCT.GetRowCellValue(i, "ChuDongTimViecDeLam") = 0 And gdvCT.GetRowCellValue(i, "LinhHoatBietViecDeLam") = 0 Then
                    If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Then
                        Continue For
                    Else
                        AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "ID"))
                        If doDelete("KTDiemDanhGiaNV", "ID=@IDD") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        End If
                    End If
                Else
                    AddParameter("@IDNguoiDanhGia", cbNguoiDanhGia.EditValue)
                    AddParameter("@IDBoPhan", cbBoPhan.EditValue)
                    AddParameter("@Thang", Convert.ToDateTime(tbThoiGian.EditValue).ToString("MM/yyyy"))
                    AddParameter("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                    AddParameter("@ChuDongHTCongViec", gdvCT.GetRowCellValue(i, "ChuDongHTCongViec"))
                    AddParameter("@ChuDongTimViecDeLam", gdvCT.GetRowCellValue(i, "ChuDongTimViecDeLam"))
                    AddParameter("@LinhHoatBietViecDeLam", gdvCT.GetRowCellValue(i, "LinhHoatBietViecDeLam"))
                    AddParameter("@CapQuanLy", cbCapQuanLy.SelectedIndex)
                    If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Then
                        Dim obID As Object = doInsert("KTDiemDanhGiaNV")
                        If obID Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            gdvCT.SetRowCellValue(i, "ID", obID)
                            gdvCT.SetRowCellValue(i, "Modify", False)
                        End If
                    Else
                        AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "ID"))
                        If doUpdate("KTDiemDanhGiaNV", "ID=@IDD") Is Nothing Then
                            ShowBaoLoi(LoiNgoaiLe)
                        Else
                            gdvCT.SetRowCellValue(i, "Modify", False)
                        End If
                    End If
                End If

                
            End If
        Next
        ShowAlert("Đã thực hiện xong !")
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub tbThoiGian_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbThoiGian.EditValueChanged
        If _exit = True Then Exit Sub
        LoadGdvData()
        LoadNVDanhGia()
    End Sub

    Private Sub cbBoPhan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbBoPhan.EditValueChanged
        If _exit = True Then Exit Sub
        LoadGdvData()
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName <> "Modify" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub cbNguoiDanhGia_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNguoiDanhGia.EditValueChanged
        On Error Resume Next
        Dim edit As LookUpEdit = CType(sender, LookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        cbBoPhan.EditValue = dr("NhomKN")
    End Sub

    Private Sub frmPhieuDanhGiaNV_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        AddParameterWhere("@Thang", Convert.ToDateTime(tbThoiGian.EditValue).ToString("MM/yyyy"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Duyet FROM KTDuyetDiemDanhGiaNV WHERE Thang=@Thang")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If tb.Rows(0)(0) Then
                    btLuuLai.Enabled = False
                End If
            End If
        End If
    End Sub
End Class