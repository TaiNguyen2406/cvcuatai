Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmBaoCaoHangNgay
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmBaoCaoHangNgay_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadTuDien()
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbNgayBaoCao.EditValue = Today.Date
        btfilterNgThucHien.EditValue = Convert.ToInt32(TaiKhoan)
        LoadHoatDongHangNgay()
        LoadBaoCaoVanHoa()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) And Not _
        KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            colDuyet.OptionsColumn.AllowEdit = False
        End If
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT IDDepatment FROM NHANSU WHERE ID=" & TaiKhoan)
        If Not tb Is Nothing Then
            btFilterPhongBan.EditValue = tb.Rows(0)(0)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

#Region "Tải dữ liệu các combobox"

    Public Sub LoadTuDien()
        Dim sql As String = " SELECT ID,Ten FROM DEPATMENT "
        sql &= " SELECT ID,Ten FROM HDNhom ORDER BY Ten"
        sql &= " SELECT ID,Ten FROM HDTen ORDER BY Ten"
        sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 "
        sql &= " SELECT HDDanhSach.ID,HDNhom.Ten AS TenNhom,HDTen.Ten AS TenCV,HDDanhSach.Diem,HDDanhSach.MoTa"
        sql &= " FROM HDDanhSach"
        sql &= " INNER JOIN HDNhom ON HDNhom.ID=HDDanhSach.IDNhom"
        sql &= " INNER JOIN HDTen ON HDTen.ID=HDDanhSach.IDTen"
        sql &= " ORDER BY TenNhom,TenCV"

        sql &= " SELECT VHDanhSach.ID,VHNhom.Ten AS TenNhom,VHTen.Ten AS TenCV,VHDanhSach.Diem,VHDanhSach.MoTa"
        sql &= " FROM VHDanhSach"
        sql &= " INNER JOIN VHNhom ON VHNhom.ID=VHDanhSach.IDNhom"
        sql &= " INNER JOIN VHTen ON VHTen.ID=VHDanhSach.IDTen"
        sql &= " ORDER BY TenNhom,TenCV"

        sql &= " SELECT NLDanhSach.ID,NLNhom.Ten AS TenNhom,NLTen.Ten AS TenCV,NLDanhSach.Diem,NLDanhSach.MoTa"
        sql &= " FROM NLDanhSach"
        sql &= " INNER JOIN NLNhom ON NLNhom.ID=NLDanhSach.IDNhom"
        sql &= " INNER JOIN NLTen ON NLTen.ID=NLDanhSach.IDTen"
        sql &= " ORDER BY TenNhom,TenCV"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbPhongBan.DataSource = ds.Tables(0)
            rcbNhomCV.DataSource = ds.Tables(1)
            rcbNhomBC.DataSource = ds.Tables(1)
            rcbCV.DataSource = ds.Tables(2)
            rcbTenBC.DataSource = ds.Tables(2)
            rcbThucHien.DataSource = ds.Tables(3)
            rcbNhanVien.DataSource = ds.Tables(3)
            rcbNVVH.DataSource = ds.Tables(3)
            rcbNVNL.DataSource = ds.Tables(3)
            rcbNoiDungCV.DataSource = ds.Tables(4)
            With rcbNoiDungCV.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                Dim colTenNhom = .AddField("TenNhom")
                colTenNhom.Caption = "Nhóm công việc"
                colTenNhom.VisibleIndex = 0
                colTenNhom.GroupIndex = 0
                Dim colTenCV = .AddField("TenCV")
                colTenCV.Caption = "Công Việc"
                colTenCV.VisibleIndex = 1
                colTenCV.Width = 350
                colTenCV.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colDiem = .AddField("Diem")
                colDiem.Caption = "Điểm"
                colDiem.VisibleIndex = 2
                colDiem.Width = 60
                Dim colMoTa = .AddField("MoTa")
                colMoTa.Caption = "Điểm"
                colMoTa.Visible = False
            End With

            rcbNoiDungBCVH.DataSource = ds.Tables(5)
            With rcbNoiDungBCVH.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                Dim colTenNhom = .AddField("TenNhom")
                colTenNhom.Caption = "Nhóm"
                colTenNhom.VisibleIndex = 0
                colTenNhom.GroupIndex = 0
                Dim colTenCV = .AddField("TenCV")
                colTenCV.Caption = "Nội dung"
                colTenCV.VisibleIndex = 1
                colTenCV.Width = 350
                colTenCV.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colDiem = .AddField("Diem")
                colDiem.Caption = "Điểm"
                colDiem.VisibleIndex = 2
                colDiem.Width = 60
                Dim colMoTa = .AddField("MoTa")
                colMoTa.Caption = "Điểm"
                colMoTa.Visible = False

            End With

            rcbNoiDungNL.DataSource = ds.Tables(6)
            With rcbNoiDungNL.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                Dim colTenNhom = .AddField("TenNhom")
                colTenNhom.Caption = "Nhóm"
                colTenNhom.VisibleIndex = 0
                colTenNhom.GroupIndex = 0
                Dim colTenCV = .AddField("TenCV")
                colTenCV.Caption = "Nội dung"
                colTenCV.VisibleIndex = 1
                colTenCV.Width = 350
                colTenCV.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colDiem = .AddField("Diem")
                colDiem.Caption = "Điểm"
                colDiem.VisibleIndex = 2
                colDiem.Width = 60
                Dim colMoTa = .AddField("MoTa")
                colMoTa.Caption = "Điểm"
                colMoTa.Visible = False

            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadTuDienVH()
        Dim sql As String = " SELECT ID,Ten FROM VHNhom ORDER By Ten"
        sql &= " SELECT ID,Ten FROM VHTen ORDER BY Ten"
     
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbNhomCV.DataSource = ds.Tables(0)
            rcbCV.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadTuDienHDHangNgay()
        Dim sql As String = " SELECT ID,Ten FROM HDNhom ORDER By Ten"
        sql &= " SELECT ID,Ten FROM HDTen ORDER BY Ten"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbNhomCV.DataSource = ds.Tables(0)
            rcbCV.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadTuDienNangLuc()
        Dim sql As String = " SELECT ID,Ten FROM NLNhom ORDER By Ten"
        sql &= " SELECT ID,Ten FROM NLTen ORDER BY Ten"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbNhomCV.DataSource = ds.Tables(0)
            rcbCV.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadCongViec()
        Dim sql As String = " SELECT ID,Ten FROM HDTen"
        If Not btfilterNhomCV.EditValue Is Nothing Then
            sql &= " WHERE ID IN (SELECT IDTen FROM HDDANHSACH WHERE IDNhom=" & btfilterNhomCV.EditValue & ")"
        End If
        sql &= " ORDER BY Ten"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbCV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadVanHoa()
        Dim sql As String = " SELECT ID,Ten FROM VHTen"
        If Not btfilterNhomCV.EditValue Is Nothing Then
            sql &= " WHERE ID IN (SELECT IDTen FROM VHDANHSACH WHERE IDNhom=" & btfilterNhomCV.EditValue & ")"
        End If
        sql &= " ORDER BY Ten"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbCV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub loadNangLuc()
        Dim sql As String = " SELECT ID,Ten FROM NLTen"
        If Not btfilterNhomCV.EditValue Is Nothing Then
            sql &= " WHERE ID IN (SELECT IDTen FROM NLDANHSACH WHERE IDNhom=" & btfilterNhomCV.EditValue & ")"
        End If
        sql &= " ORDER BY Ten"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbCV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub cbTenVatTu_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomCV.ButtonClick
        If e.Button.Index = 1 Then
            btfilterNhomCV.EditValue = Nothing

        End If
    End Sub

    Private Sub rcbHangSX_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbCV.ButtonClick
        If e.Button.Index = 1 Then
            btFilterCV.EditValue = Nothing

        End If
    End Sub

    Private Sub cbNhomVT_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhongBan.ButtonClick
        If e.Button.Index = 1 Then
            btFilterPhongBan.EditValue = Nothing

        End If
    End Sub


    Private Sub rcbTakecare_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbThucHien.ButtonClick
        If e.Button.Index = 1 Then
            btfilterNgThucHien.EditValue = Nothing
        End If
    End Sub


#End Region

    Private Sub LoadHoatDongHangNgay()
        Dim Sql As String = " SET DATEFORMAT DMY "
        Sql &= " SELECT 0 AS STT,HDNhanVien.ID, HDNhanVien.NgayBaoCao,HDNhanVien.NgayNhapLieu,HDNhanVien.IDDanhSach, HDDanhSach.IDNhom,HDDanhSach.IDTen,HDDanhSach.MoTa,HDDanhSach.Diem AS DiemChuan,"
        Sql &= " HDNhanVien.ChiTiet,HDNhanVien.SoLuong,HDNhanVien.KhoiLuong,HDNhanVien.Diem,HDNhanVien.BaoCao,HDNhanVien.PhanHoi,HDNhanVien.Duyet,HDNhanVien.IDNhanVien,"
        Sql &= " HDNhanVien.IDUser,HDNhanVien.YearMonth,HDNhanVien.IDDanhSach,convert(bit,0)Modify"
        Sql &= " FROM HDNhanVien"
        Sql &= " INNER JOIN HDDANHSACH ON HDNhanVien.IDDanhSach=HDDanhSach.ID"
        If Not btfilterNhomCV.EditValue Is Nothing Then
            Sql &= " AND HDDANHSACH.IDNhom=" & btfilterNhomCV.EditValue
        End If

        If Not btFilterCV.EditValue Is Nothing Then
            Sql &= " AND HDDANHSACH.IDTen=" & btFilterCV.EditValue
        End If

        Sql &= " WHERE HDNhanVien.NgayBaoCao BETWEEN @TuNgay AND @DenNgay "
        If Not btFilterPhongBan.EditValue Is Nothing Then
            Sql &= " AND HDNhanVien.IDNhanVien IN (SELECT ID FROM NHANSU WHERE IDDepatment=" & btFilterPhongBan.EditValue & " AND Noictac=74)"
        End If

        If Not btfilterNgThucHien.EditValue Is Nothing Then
            Sql &= " AND HDNhanVien.IDNhanVien =" & btfilterNgThucHien.EditValue
        End If

        Sql &= " ORDER BY HDNhanVien.NgayBaoCao"
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbNgayBaoCao.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(Sql)
        If Not tb Is Nothing Then
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next
            gdvHoatDong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadBaoCaoVanHoa()
        Dim Sql As String = " SET DATEFORMAT DMY "
        Sql &= " SELECT 0 AS STT,VHNhanVien.ID, VHNhanVien.NgayBaoCao,VHNhanVien.NgayNhapLieu,VHNhanVien.IDDanhSach, VHDanhSach.IDNhom,VHDanhSach.IDTen,VHDanhSach.MoTa,VHDanhSach.Diem AS DiemChuan,"
        Sql &= " VHNhanVien.ChiTiet,VHNhanVien.SoLuong,VHNhanVien.KhoiLuong,VHNhanVien.Diem,VHNhanVien.BaoCao,VHNhanVien.PhanHoi,VHNhanVien.Duyet,VHNhanVien.IDNhanVien,"
        Sql &= " VHNhanVien.IDUser,VHNhanVien.YearMonth,VHNhanVien.IDDanhSach,convert(bit,0)Modify"
        Sql &= " FROM VHNhanVien"
        Sql &= " INNER JOIN VHDANHSACH ON VHNhanVien.IDDanhSach=VHDanhSach.ID"
        If Not btfilterNhomCV.EditValue Is Nothing Then
            Sql &= " AND VHDANHSACH.IDNhom=" & btfilterNhomCV.EditValue
        End If

        If Not btFilterCV.EditValue Is Nothing Then
            Sql &= " AND VHDANHSACH.IDTen=" & btFilterCV.EditValue
        End If

        Sql &= " WHERE VHNhanVien.NgayBaoCao BETWEEN @TuNgay AND @DenNgay "
        If Not btFilterPhongBan.EditValue Is Nothing Then
            Sql &= " AND VHNhanVien.IDNhanVien IN (SELECT ID FROM NHANSU WHERE IDDepatment=" & btFilterPhongBan.EditValue & " AND Noictac=74)"
        End If

        If Not btfilterNgThucHien.EditValue Is Nothing Then
            Sql &= " AND VHNhanVien.IDNhanVien =" & btfilterNgThucHien.EditValue
        End If

        Sql &= " ORDER BY VHNhanVien.NgayBaoCao"
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbNgayBaoCao.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(Sql)
        If Not tb Is Nothing Then
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next
            gdvVanHoa.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadBaoCaoNangLuc()
        Dim Sql As String = " SET DATEFORMAT DMY "
        Sql &= " SELECT 0 AS STT,NLNhanVien.ID, NLNhanVien.NgayBaoCao,NLNhanVien.NgayNhapLieu,NLNhanVien.IDDanhSach, NLDanhSach.IDNhom,NLDanhSach.IDTen,NLDanhSach.MoTa,NLDanhSach.Diem AS DiemChuan,"
        Sql &= " NLNhanVien.ChiTiet,NLNhanVien.SoLuong,NLNhanVien.KhoiLuong,NLNhanVien.Diem,NLNhanVien.BaoCao,NLNhanVien.PhanHoi,NLNhanVien.Duyet,NLNhanVien.IDNhanVien,"
        Sql &= " NLNhanVien.IDUser,NLNhanVien.YearMonth,NLNhanVien.IDDanhSach,convert(bit,0)Modify"
        Sql &= " FROM NLNhanVien"
        Sql &= " INNER JOIN NLDANHSACH ON NLNhanVien.IDDanhSach=NLDanhSach.ID"
        If Not btfilterNhomCV.EditValue Is Nothing Then
            Sql &= " AND NLDANHSACH.IDNhom=" & btfilterNhomCV.EditValue
        End If

        If Not btFilterCV.EditValue Is Nothing Then
            Sql &= " AND NLDANHSACH.IDTen=" & btFilterCV.EditValue
        End If

        Sql &= " WHERE NLNhanVien.NgayBaoCao BETWEEN @TuNgay AND @DenNgay "
        If Not btFilterPhongBan.EditValue Is Nothing Then
            Sql &= " AND NLNhanVien.IDNhanVien IN (SELECT ID FROM NHANSU WHERE IDDepatment=" & btFilterPhongBan.EditValue & " AND Noictac=74)"
        End If

        If Not btfilterNgThucHien.EditValue Is Nothing Then
            Sql &= " AND NLNhanVien.IDNhanVien =" & btfilterNgThucHien.EditValue
        End If

        Sql &= " ORDER BY NLNhanVien.NgayBaoCao"
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbNgayBaoCao.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(Sql)
        If Not tb Is Nothing Then
            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next
            gdvNangLuc.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        Select Case tabBC.SelectedTabPage.Name
            Case tabVanHoaCongTy.Name
                LoadBaoCaoVanHoa()
            Case tabNangLucChuyenMon.Name
                LoadBaoCaoNangLuc()
            Case tabHoatDongHangNgay.Name
                LoadHoatDongHangNgay()
        End Select
        TinhDiem()
    End Sub

    Private Sub btfilterNhomCV_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterNhomCV.EditValueChanged
        Select Case tabBC.SelectedTabPage.Name
            Case tabVanHoaCongTy.Name
                loadVanHoa()
            Case tabNangLucChuyenMon.Name
                loadNangLuc()
            Case tabHoatDongHangNgay.Name
                loadCongViec()
            Case tabDoiMoiSangTao.Name

        End Select

    End Sub

    Public Sub TinhDiem()
        tbDiemDaDuyet.EditValue = 0
        tbKhoiLuongDaDuyet.EditValue = 0

        Select Case tabBC.SelectedTabPage.Name
            Case tabVanHoaCongTy.Name
                For i As Integer = 0 To gdvVanHoaCT.RowCount - 1
                    If gdvVanHoaCT.GetRowCellValue(i, "Duyet") Then
                        tbDiemDaDuyet.EditValue += gdvVanHoaCT.GetRowCellValue(i, "Diem")
                    End If
                Next
            Case tabNangLucChuyenMon.Name
                For i As Integer = 0 To gdvNangLucCT.RowCount - 1
                    If gdvNangLucCT.GetRowCellValue(i, "Duyet") Then
                        tbDiemDaDuyet.EditValue += gdvNangLucCT.GetRowCellValue(i, "Diem")
                    End If
                Next
            Case tabHoatDongHangNgay.Name
                For i As Integer = 0 To gdvHoatDongCT.RowCount - 1
                    If gdvHoatDongCT.GetRowCellValue(i, "Duyet") Then
                        tbDiemDaDuyet.EditValue += gdvHoatDongCT.GetRowCellValue(i, "Diem")
                        tbKhoiLuongDaDuyet.EditValue += gdvHoatDongCT.GetRowCellValue(i, "KhoiLuong")
                    End If
                Next
            Case tabDoiMoiSangTao.Name

        End Select
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick
        If btfilterNgThucHien.EditValue Is Nothing Then
            ShowCanhBao("Bạn cần chọn người thực hiện trước khi tiến hành báo cáo !")
            Exit Sub
        End If
        Dim tg As DateTime = GetServerTime()
        Select Case tabBC.SelectedTabPage.Name
            Case tabVanHoaCongTy.Name
                gdvVanHoaCT.AddNewRow()
                gdvVanHoaCT.SetFocusedRowCellValue("STT", gdvVanHoaCT.RowCount)
                gdvVanHoaCT.SetFocusedRowCellValue("NgayBaoCao", tbNgayBaoCao.EditValue)
                gdvVanHoaCT.SetFocusedRowCellValue("NgayNhapLieu", tg.Date)
                gdvVanHoaCT.SetFocusedRowCellValue("IDUser", Convert.ToInt32(TaiKhoan))
                gdvVanHoaCT.SetFocusedRowCellValue("IDNhanVien", btfilterNgThucHien.EditValue)
                gdvVanHoaCT.SetFocusedRowCellValue("IDNhom", btfilterNhomCV.EditValue)
                gdvVanHoaCT.SetFocusedRowCellValue("IDTen", btFilterCV.EditValue)
                gdvVanHoaCT.SetFocusedRowCellValue("SoLuong", 0)
                gdvVanHoaCT.SetFocusedRowCellValue("KhoiLuong", 0)
                gdvVanHoaCT.SetFocusedRowCellValue("Diem", 0)
                gdvVanHoaCT.SetFocusedRowCellValue("DiemChuan", 0)
                gdvVanHoaCT.SetFocusedRowCellValue("Duyet", False)
                gdvVanHoaCT.SetFocusedRowCellValue("Modify", True)
                gdvVanHoaCT.Focus()
                gdvVanHoaCT.FocusedColumn = gdvVanHoaCT.Columns("IDDanhSach")
                SendKeys.Send("{F4}")
            Case tabNangLucChuyenMon.Name
                gdvNangLucCT.AddNewRow()
                gdvNangLucCT.SetFocusedRowCellValue("STT", gdvNangLucCT.RowCount)
                gdvNangLucCT.SetFocusedRowCellValue("NgayBaoCao", tbNgayBaoCao.EditValue)
                gdvNangLucCT.SetFocusedRowCellValue("NgayNhapLieu", tg.Date)
                gdvNangLucCT.SetFocusedRowCellValue("IDUser", Convert.ToInt32(TaiKhoan))
                gdvNangLucCT.SetFocusedRowCellValue("IDNhanVien", btfilterNgThucHien.EditValue)
                gdvNangLucCT.SetFocusedRowCellValue("IDNhom", btfilterNhomCV.EditValue)
                gdvNangLucCT.SetFocusedRowCellValue("IDTen", btFilterCV.EditValue)
                gdvNangLucCT.SetFocusedRowCellValue("SoLuong", 0)
                gdvNangLucCT.SetFocusedRowCellValue("KhoiLuong", 0)
                gdvNangLucCT.SetFocusedRowCellValue("Diem", 0)
                gdvNangLucCT.SetFocusedRowCellValue("DiemChuan", 0)
                gdvNangLucCT.SetFocusedRowCellValue("Duyet", False)
                gdvNangLucCT.SetFocusedRowCellValue("Modify", True)
                gdvNangLucCT.Focus()
                gdvNangLucCT.FocusedColumn = gdvNangLucCT.Columns("IDDanhSach")
                SendKeys.Send("{F4}")
            Case tabHoatDongHangNgay.Name
                gdvHoatDongCT.AddNewRow()
                gdvHoatDongCT.SetFocusedRowCellValue("STT", gdvHoatDongCT.RowCount)
                gdvHoatDongCT.SetFocusedRowCellValue("NgayBaoCao", tbNgayBaoCao.EditValue)
                gdvHoatDongCT.SetFocusedRowCellValue("NgayNhapLieu", tg.Date)
                gdvHoatDongCT.SetFocusedRowCellValue("IDUser", Convert.ToInt32(TaiKhoan))
                gdvHoatDongCT.SetFocusedRowCellValue("IDNhanVien", btfilterNgThucHien.EditValue)
                gdvHoatDongCT.SetFocusedRowCellValue("IDNhom", btfilterNhomCV.EditValue)
                gdvHoatDongCT.SetFocusedRowCellValue("IDTen", btFilterCV.EditValue)
                gdvHoatDongCT.SetFocusedRowCellValue("SoLuong", 0)
                gdvHoatDongCT.SetFocusedRowCellValue("KhoiLuong", 0)
                gdvHoatDongCT.SetFocusedRowCellValue("Diem", 0)
                gdvHoatDongCT.SetFocusedRowCellValue("DiemChuan", 0)
                gdvHoatDongCT.SetFocusedRowCellValue("Duyet", False)
                gdvHoatDongCT.SetFocusedRowCellValue("Modify", True)
                gdvHoatDongCT.Focus()
                gdvHoatDongCT.FocusedColumn = gdvHoatDongCT.Columns("IDDanhSach")
                SendKeys.Send("{F4}")
            Case tabDoiMoiSangTao.Name

        End Select


    End Sub

    Private Sub gdvHoatDongCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvHoatDongCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "KhoiLuong"
                Select Case gdvHoatDongCT.GetRowCellValue(e.RowHandle, "IDDanhSach")
                    Case 35, 36, 37, 38, 39, 40
                        gdvHoatDongCT.SetRowCellValue(e.RowHandle, "SoLuong", Math.Round(e.Value / 1000000, 2))
                End Select
            Case "IDDanhSach"
                Dim dr As DataRowView = rcbNoiDungCV.GetRowByKeyValue(e.Value)
                gdvHoatDongCT.SetRowCellValue(e.RowHandle, "DiemChuan", dr("Diem"))
                gdvHoatDongCT.SetRowCellValue(e.RowHandle, "MoTa", dr("MoTa"))
            Case "SoLuong", "DiemChuan"
                gdvHoatDongCT.SetRowCellValue(e.RowHandle, "Diem", gdvHoatDongCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvHoatDongCT.GetRowCellValue(e.RowHandle, "DiemChuan"))
        End Select
        If e.Column.FieldName <> "Modify" Then
            gdvHoatDongCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub gdvVanHoaCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvVanHoaCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "IDDanhSach"
                Dim dr As DataRowView = rcbNoiDungBCVH.GetRowByKeyValue(e.Value)
                gdvVanHoaCT.SetRowCellValue(e.RowHandle, "DiemChuan", dr("Diem"))
                gdvVanHoaCT.SetRowCellValue(e.RowHandle, "MoTa", dr("MoTa"))
            Case "SoLuong", "DiemChuan"
                gdvVanHoaCT.SetRowCellValue(e.RowHandle, "Diem", gdvVanHoaCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvVanHoaCT.GetRowCellValue(e.RowHandle, "DiemChuan"))
        End Select
        If e.Column.FieldName <> "Modify" Then
            gdvVanHoaCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub gdvNangLucCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvNangLucCT.CellValueChanged
        On Error Resume Next
        Select Case e.Column.FieldName
            Case "IDDanhSach"
                Dim dr As DataRowView = rcbNoiDungNL.GetRowByKeyValue(e.Value)
                gdvNangLucCT.SetRowCellValue(e.RowHandle, "DiemChuan", dr("Diem"))
                gdvNangLucCT.SetRowCellValue(e.RowHandle, "MoTa", dr("MoTa"))
            Case "SoLuong", "DiemChuan"
                gdvNangLucCT.SetRowCellValue(e.RowHandle, "Diem", gdvNangLucCT.GetRowCellValue(e.RowHandle, "SoLuong") * gdvNangLucCT.GetRowCellValue(e.RowHandle, "DiemChuan"))
        End Select
        If e.Column.FieldName <> "Modify" Then
            gdvNangLucCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Public Sub LuuHoatDongHangNgay()
        gdvHoatDongCT.CloseEditor()
        gdvHoatDongCT.UpdateCurrentRow()
        gdvHoatDongCT.BeginUpdate()
        With gdvHoatDongCT
            Try
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "Modify") Then
                        AddParameter("@NgayBaoCao", .GetRowCellValue(i, "NgayBaoCao"))
                        AddParameter("@NgayNhapLieu", .GetRowCellValue(i, "NgayNhapLieu"))
                        AddParameter("@IDDanhSach", .GetRowCellValue(i, "IDDanhSach"))
                        AddParameter("@ChiTiet", .GetRowCellValue(i, "ChiTiet"))
                        AddParameter("@SoLuong", .GetRowCellValue(i, "SoLuong"))
                        AddParameter("@KhoiLuong", .GetRowCellValue(i, "KhoiLuong"))
                        AddParameter("@Diem", .GetRowCellValue(i, "Diem"))
                        AddParameter("@BaoCao", .GetRowCellValue(i, "BaoCao"))
                        AddParameter("@PhanHoi", .GetRowCellValue(i, "PhanHoi"))
                        AddParameter("@IDNhanVien", .GetRowCellValue(i, "IDNhanVien"))
                        AddParameter("@IDUser", .GetRowCellValue(i, "IDUser"))
                        AddParameter("@YearMonth", Convert.ToDateTime(.GetRowCellValue(i, "NgayBaoCao")).ToString("yyyyMM"))
                        If .GetRowCellValue(i, "ID") Is Nothing Or IsDBNull(.GetRowCellValue(i, "ID")) Then
                            Dim _IDBC As New Object
                            _IDBC = doInsert("HDNhanVien")
                            If _IDBC Is Nothing Then Throw New Exception(LoiNgoaiLe)

                            .SetRowCellValue(i, "ID", _IDBC)

                        Else
                            AddParameterWhere("@IDBC", .GetRowCellValue(i, "ID"))
                            If doUpdate("HDNhanVien", "ID=@IDBC") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        End If
                        .SetRowCellValue(i, "Modify", False)

                    End If

                Next
                ShowAlert("Đã lưu dữ liệu !")
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End With
        gdvHoatDongCT.EndUpdate()

    End Sub

    Public Sub LuuHoatBCVanHoa()
        gdvVanHoaCT.CloseEditor()
        gdvVanHoaCT.UpdateCurrentRow()
        gdvVanHoaCT.BeginUpdate()
        With gdvVanHoaCT
            Try
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "Modify") Then
                        AddParameter("@NgayBaoCao", .GetRowCellValue(i, "NgayBaoCao"))
                        AddParameter("@NgayNhapLieu", .GetRowCellValue(i, "NgayNhapLieu"))
                        AddParameter("@IDDanhSach", .GetRowCellValue(i, "IDDanhSach"))
                        AddParameter("@ChiTiet", .GetRowCellValue(i, "ChiTiet"))
                        AddParameter("@SoLuong", .GetRowCellValue(i, "SoLuong"))
                        AddParameter("@KhoiLuong", .GetRowCellValue(i, "KhoiLuong"))
                        AddParameter("@Diem", .GetRowCellValue(i, "Diem"))
                        AddParameter("@BaoCao", .GetRowCellValue(i, "BaoCao"))
                        AddParameter("@PhanHoi", .GetRowCellValue(i, "PhanHoi"))
                        AddParameter("@IDNhanVien", .GetRowCellValue(i, "IDNhanVien"))
                        AddParameter("@IDUser", .GetRowCellValue(i, "IDUser"))
                        AddParameter("@YearMonth", Convert.ToDateTime(.GetRowCellValue(i, "NgayBaoCao")).ToString("yyyyMM"))
                        If .GetRowCellValue(i, "ID") Is Nothing Or IsDBNull(.GetRowCellValue(i, "ID")) Then
                            Dim _IDBC As New Object
                            _IDBC = doInsert("VHNhanVien")
                            If _IDBC Is Nothing Then Throw New Exception(LoiNgoaiLe)

                            .SetRowCellValue(i, "ID", _IDBC)

                        Else
                            AddParameterWhere("@IDBC", .GetRowCellValue(i, "ID"))
                            If doUpdate("VHNhanVien", "ID=@IDBC") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        End If
                        .SetRowCellValue(i, "Modify", False)

                    End If

                Next
                ShowAlert("Đã lưu dữ liệu !")
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End With
        gdvVanHoaCT.EndUpdate()

    End Sub

    Public Sub LuuHoatBCNangLuc()
        gdvNangLucCT.CloseEditor()
        gdvNangLucCT.UpdateCurrentRow()
        gdvNangLucCT.BeginUpdate()
        With gdvNangLucCT
            Try
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "Modify") Then
                        AddParameter("@NgayBaoCao", .GetRowCellValue(i, "NgayBaoCao"))
                        AddParameter("@NgayNhapLieu", .GetRowCellValue(i, "NgayNhapLieu"))
                        AddParameter("@IDDanhSach", .GetRowCellValue(i, "IDDanhSach"))
                        AddParameter("@ChiTiet", .GetRowCellValue(i, "ChiTiet"))
                        AddParameter("@SoLuong", .GetRowCellValue(i, "SoLuong"))
                        AddParameter("@KhoiLuong", .GetRowCellValue(i, "KhoiLuong"))
                        AddParameter("@Diem", .GetRowCellValue(i, "Diem"))
                        AddParameter("@BaoCao", .GetRowCellValue(i, "BaoCao"))
                        AddParameter("@PhanHoi", .GetRowCellValue(i, "PhanHoi"))
                        AddParameter("@IDNhanVien", .GetRowCellValue(i, "IDNhanVien"))
                        AddParameter("@IDUser", .GetRowCellValue(i, "IDUser"))
                        AddParameter("@YearMonth", Convert.ToDateTime(.GetRowCellValue(i, "NgayBaoCao")).ToString("yyyyMM"))
                        If .GetRowCellValue(i, "ID") Is Nothing Or IsDBNull(.GetRowCellValue(i, "ID")) Then
                            Dim _IDBC As New Object
                            _IDBC = doInsert("NLNhanVien")
                            If _IDBC Is Nothing Then Throw New Exception(LoiNgoaiLe)

                            .SetRowCellValue(i, "ID", _IDBC)

                        Else
                            AddParameterWhere("@IDBC", .GetRowCellValue(i, "ID"))
                            If doUpdate("NLNhanVien", "ID=@IDBC") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        End If
                        .SetRowCellValue(i, "Modify", False)

                    End If

                Next
                ShowAlert("Đã lưu dữ liệu !")
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End With
        gdvNangLucCT.EndUpdate()

    End Sub

    Private Sub btLuu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuu.ItemClick
        Select Case tabBC.SelectedTabPage.Name
            Case tabVanHoaCongTy.Name
                LuuHoatBCVanHoa()
            Case tabNangLucChuyenMon.Name
                LuuHoatBCNangLuc()
            Case tabHoatDongHangNgay.Name
                LuuHoatDongHangNgay()
        End Select
    End Sub

    Private Sub rcbNoiDungCV_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles rcbNoiDungBCVH.KeyDown
        If _Exit = False Then
            rcbNoiDungCV.ImmediatePopup = True
        End If
    End Sub

    Private Sub rcbNoiDungBCVH_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles rcbNoiDungBCVH.KeyDown
        If _Exit = False Then
            rcbNoiDungBCVH.ImmediatePopup = True
        End If
    End Sub

    Private Sub rcbNoiDungNL_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles rcbNoiDungNL.KeyDown
        If _Exit = False Then
            rcbNoiDungNL.ImmediatePopup = True
        End If
    End Sub

    Private Sub rcbNoiDungCV_CloseUp(sender As System.Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles rcbNoiDungCV.CloseUp, rcbNoiDungBCVH.CloseUp, rcbNoiDungNL.CloseUp
        _Exit = True
    End Sub

    Private Sub rcbNoiDungCV_Popup(sender As System.Object, e As System.EventArgs) Handles rcbNoiDungCV.Popup, rcbNoiDungBCVH.Popup, rcbNoiDungNL.Popup
        _Exit = False
    End Sub

    Private Sub gdvHoatDongCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvHoatDongCT.RowCellClick
        If e.Column.FieldName = "Duyet" Then
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Or _
                KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                gdvHoatDongCT.CloseEditor()
                gdvHoatDongCT.UpdateCurrentRow()
                gdvHoatDongCT.SetRowCellValue(e.RowHandle, "Duyet", Not gdvHoatDongCT.GetRowCellValue(e.RowHandle, "Duyet"))
                gdvHoatDongCT.CloseEditor()
                gdvHoatDongCT.UpdateCurrentRow()
            End If

        End If
    End Sub

    Private Sub gdvVanHoaCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvVanHoaCT.RowCellClick
        If e.Column.FieldName = "Duyet" Then
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Or _
                KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                gdvVanHoaCT.CloseEditor()
                gdvVanHoaCT.UpdateCurrentRow()
                gdvVanHoaCT.SetRowCellValue(e.RowHandle, "Duyet", Not gdvVanHoaCT.GetRowCellValue(e.RowHandle, "Duyet"))
                gdvVanHoaCT.CloseEditor()
                gdvVanHoaCT.UpdateCurrentRow()
            End If

        End If
    End Sub

    Private Sub gdvNangLucCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvNangLucCT.RowCellClick
        If e.Column.FieldName = "Duyet" Then
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Or _
                KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                gdvNangLucCT.CloseEditor()
                gdvNangLucCT.UpdateCurrentRow()
                gdvNangLucCT.SetRowCellValue(e.RowHandle, "Duyet", Not gdvNangLucCT.GetRowCellValue(e.RowHandle, "Duyet"))
                gdvNangLucCT.CloseEditor()
                gdvNangLucCT.UpdateCurrentRow()
            End If

        End If
    End Sub

    Private Sub btDuyetDiem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDuyetDiem.ItemClick
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Or _
              KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            Select Case tabBC.SelectedTabPage.Name
                Case tabVanHoaCongTy.Name
                    gdvVanHoaCT.CloseEditor()
                    gdvVanHoaCT.UpdateCurrentRow()
                    gdvVanHoaCT.BeginUpdate()
                    With gdvVanHoaCT
                        Try
                            For i As Integer = 0 To .RowCount - 1
                                If .GetRowCellValue(i, "Modify") Then
                                    If Not .GetRowCellValue(i, "ID") Is Nothing And Not IsDBNull(.GetRowCellValue(i, "ID")) Then
                                        AddParameter("@Duyet", .GetRowCellValue(i, "Duyet"))
                                        AddParameterWhere("@IDBC", .GetRowCellValue(i, "ID"))
                                        If doUpdate("VHNhanVien", "ID=@IDBC") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                                        .SetRowCellValue(i, "Modify", False)
                                    End If
                                End If
                            Next
                            ShowAlert("Đã duyệt báo cáo !")
                        Catch ex As Exception
                            ShowBaoLoi(ex.Message)
                        End Try

                    End With
                    gdvVanHoaCT.EndUpdate()
                Case tabNangLucChuyenMon.Name
                    gdvNangLucCT.CloseEditor()
                    gdvNangLucCT.UpdateCurrentRow()
                    gdvNangLucCT.BeginUpdate()
                    With gdvNangLucCT
                        Try
                            For i As Integer = 0 To .RowCount - 1
                                If .GetRowCellValue(i, "Modify") Then
                                    If Not .GetRowCellValue(i, "ID") Is Nothing And Not IsDBNull(.GetRowCellValue(i, "ID")) Then
                                        AddParameter("@Duyet", .GetRowCellValue(i, "Duyet"))
                                        AddParameterWhere("@IDBC", .GetRowCellValue(i, "ID"))
                                        If doUpdate("NLNhanVien", "ID=@IDBC") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                                        .SetRowCellValue(i, "Modify", False)
                                    End If

                                End If

                            Next
                            ShowAlert("Đã duyệt báo cáo !")
                        Catch ex As Exception
                            ShowBaoLoi(ex.Message)
                        End Try

                    End With
                    gdvNangLucCT.EndUpdate()
                Case tabHoatDongHangNgay.Name
                    gdvHoatDongCT.CloseEditor()
                    gdvHoatDongCT.UpdateCurrentRow()
                    gdvHoatDongCT.BeginUpdate()
                    With gdvHoatDongCT
                        Try
                            For i As Integer = 0 To .RowCount - 1
                                If .GetRowCellValue(i, "Modify") Then
                                    If Not .GetRowCellValue(i, "ID") Is Nothing And Not IsDBNull(.GetRowCellValue(i, "ID")) Then
                                        AddParameter("@Duyet", .GetRowCellValue(i, "Duyet"))
                                        AddParameterWhere("@IDBC", .GetRowCellValue(i, "ID"))
                                        If doUpdate("HDNhanVien", "ID=@IDBC") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                                        .SetRowCellValue(i, "Modify", False)
                                    End If

                                End If

                            Next
                            ShowAlert("Đã duyệt báo cáo !")
                        Catch ex As Exception
                            ShowBaoLoi(ex.Message)
                        End Try

                    End With
                    gdvHoatDongCT.EndUpdate()
                Case tabDoiMoiSangTao.Name

            End Select
            
        End If
        TinhDiem()
    End Sub

    Private Sub btUpdateDiem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btUpdateDiem.ItemClick
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Or _
         KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            Select Case tabBC.SelectedTabPage.Name
                Case tabVanHoaCongTy.Name
                    gdvVanHoaCT.BeginUpdate()
                    For i As Integer = 0 To gdvVanHoaCT.RowCount - 1
                        gdvVanHoaCT.SetRowCellValue(i, "Diem", gdvVanHoaCT.GetRowCellValue(i, "SoLuong") * gdvVanHoaCT.GetRowCellValue(i, "DiemChuan"))
                    Next
                    gdvVanHoaCT.EndUpdate()
                    ShowAlert("Đã update lại điểm !")
                Case tabNangLucChuyenMon.Name
                    gdvNangLucCT.BeginUpdate()
                    For i As Integer = 0 To gdvNangLucCT.RowCount - 1
                        gdvNangLucCT.SetRowCellValue(i, "Diem", gdvNangLucCT.GetRowCellValue(i, "SoLuong") * gdvNangLucCT.GetRowCellValue(i, "DiemChuan"))
                    Next
                    gdvNangLucCT.EndUpdate()
                    ShowAlert("Đã update lại điểm !")
                Case tabHoatDongHangNgay.Name
                    gdvHoatDongCT.BeginUpdate()
                    For i As Integer = 0 To gdvHoatDongCT.RowCount - 1
                        gdvHoatDongCT.SetRowCellValue(i, "Diem", gdvHoatDongCT.GetRowCellValue(i, "SoLuong") * gdvHoatDongCT.GetRowCellValue(i, "DiemChuan"))
                    Next
                    gdvHoatDongCT.EndUpdate()
                    ShowAlert("Đã update lại điểm !")
                Case tabDoiMoiSangTao.Name

            End Select

        End If
        TinhDiem()
    End Sub

    Private Sub tabBC_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabBC.SelectedPageChanged
        Select Case tabBC.SelectedTabPage.Name
            Case tabVanHoaCongTy.Name
                btfilterNhomCV.Caption = "Nhóm"
                btFilterCV.Caption = "Tên"
                LoadTuDienVH()
            Case tabNangLucChuyenMon.Name
                btfilterNhomCV.Caption = "Nhóm"
                btFilterCV.Caption = "Tên"
                LoadTuDienNangLuc()
            Case tabHoatDongHangNgay.Name
                btfilterNhomCV.Caption = "Nhóm CV"
                btFilterCV.Caption = "Công việc"
                LoadTuDienHDHangNgay()
            Case tabDoiMoiSangTao.Name
                btfilterNhomCV.Caption = "Nhóm"
                btFilterCV.Caption = "Tên"
        End Select
    End Sub

    Private Sub gdvHoatDongCT_ShowingEditor(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles gdvHoatDongCT.ShowingEditor
        If gdvHoatDongCT.GetFocusedRowCellValue("Duyet") Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) And _
                Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub tbNgayBaoCao_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbNgayBaoCao.EditValueChanged
        tbTuNgay.EditValue = New DateTime(Convert.ToDateTime(tbNgayBaoCao.EditValue).Year, Convert.ToDateTime(tbNgayBaoCao.EditValue).Month, 1)
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Or _
            KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKyThuat) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            Select Case tabBC.SelectedTabPage.Name
                Case tabVanHoaCongTy.Name
                    gdvVanHoaCT.CloseEditor()
                    gdvVanHoaCT.UpdateCurrentRow()
                    Dim _check = Not gdvVanHoaCT.GetRowCellValue(0, "Duyet")
                    gdvVanHoaCT.BeginUpdate()
                    For i As Integer = 0 To gdvVanHoaCT.RowCount - 1
                        gdvVanHoaCT.SetRowCellValue(i, "Duyet", _check)
                    Next
                    gdvVanHoaCT.EndUpdate()
                Case tabNangLucChuyenMon.Name
                    gdvNangLucCT.CloseEditor()
                    gdvNangLucCT.UpdateCurrentRow()
                    Dim _check = Not gdvNangLucCT.GetRowCellValue(0, "Duyet")
                    gdvNangLucCT.BeginUpdate()
                    For i As Integer = 0 To gdvNangLucCT.RowCount - 1
                        gdvNangLucCT.SetRowCellValue(i, "Duyet", _check)
                    Next
                    gdvNangLucCT.EndUpdate()
                Case tabHoatDongHangNgay.Name
                    gdvHoatDongCT.CloseEditor()
                    gdvHoatDongCT.UpdateCurrentRow()
                    Dim _check = Not gdvHoatDongCT.GetRowCellValue(0, "Duyet")
                    gdvHoatDongCT.BeginUpdate()
                    For i As Integer = 0 To gdvHoatDongCT.RowCount - 1
                        gdvHoatDongCT.SetRowCellValue(i, "Duyet", _check)
                    Next
                    gdvHoatDongCT.EndUpdate()
            End Select
        End If

    End Sub


    Private Sub gdvHoatDongCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvHoatDongCT.KeyDown, gdvVanHoaCT.KeyDown, gdvNangLucCT.KeyDown

        If e.Control AndAlso e.KeyCode = Keys.Space Then
            'btThem.PerformClick()
        ElseIf e.KeyCode = Keys.Enter Then
            If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.FieldName = "DiemChuan" Then
                CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).CloseEditor()
                CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).UpdateCurrentRow()
                If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).RowCount - 1 Then
                    btThem.PerformClick()
                Else
                    CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle += 1
                End If
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle < 0 Then Exit Sub
            If Not CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID") Is DBNull.Value Then Exit Sub
            If Not ShowCauHoi("Xóa dữ liệu vừa chọn ?") Then Exit Sub
            CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).DeleteRow(CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle)
        End If

        'If btfilterNgThucHien.EditValue Is Nothing Then
        '    ShowCanhBao("Bạn cần chọn người thực hiện trước khi tiến hành báo cáo !")
        '    Exit Sub
        'End If
        'Dim tg As DateTime = GetServerTime()
        'Select Case tabBC.SelectedTabPage.Name
        '    Case tabVanHoaCongTy.Name
        '        gdvVanHoaCT.AddNewRow()
        '        gdvVanHoaCT.SetFocusedRowCellValue("STT", gdvVanHoaCT.RowCount)
        '        gdvVanHoaCT.SetFocusedRowCellValue("NgayBaoCao", tbNgayBaoCao.EditValue)
        '        gdvVanHoaCT.SetFocusedRowCellValue("NgayNhapLieu", tg.Date)
        '        gdvVanHoaCT.SetFocusedRowCellValue("IDUser", Convert.ToInt32(TaiKhoan))
        '        gdvVanHoaCT.SetFocusedRowCellValue("IDNhanVien", btfilterNgThucHien.EditValue)
        '        gdvVanHoaCT.SetFocusedRowCellValue("IDNhom", btfilterNhomCV.EditValue)
        '        gdvVanHoaCT.SetFocusedRowCellValue("IDTen", btFilterCV.EditValue)
        '        gdvVanHoaCT.SetFocusedRowCellValue("SoLuong", 0)
        '        gdvVanHoaCT.SetFocusedRowCellValue("KhoiLuong", 0)
        '        gdvVanHoaCT.SetFocusedRowCellValue("Diem", 0)
        '        gdvVanHoaCT.SetFocusedRowCellValue("DiemChuan", 0)
        '        gdvVanHoaCT.SetFocusedRowCellValue("Duyet", False)
        '        gdvVanHoaCT.SetFocusedRowCellValue("Modify", True)
        '        ' gdvVanHoaCT.Focus()
        '        gdvVanHoaCT.FocusedColumn = gdvVanHoaCT.Columns("IDDanhSach")
        '        '  SendKeys.Send("{F4}")
        '    Case tabNangLucChuyenMon.Name
        '        gdvNangLucCT.AddNewRow()
        '        gdvNangLucCT.SetFocusedRowCellValue("STT", gdvNangLucCT.RowCount)
        '        gdvNangLucCT.SetFocusedRowCellValue("NgayBaoCao", tbNgayBaoCao.EditValue)
        '        gdvNangLucCT.SetFocusedRowCellValue("NgayNhapLieu", tg.Date)
        '        gdvNangLucCT.SetFocusedRowCellValue("IDUser", Convert.ToInt32(TaiKhoan))
        '        gdvNangLucCT.SetFocusedRowCellValue("IDNhanVien", btfilterNgThucHien.EditValue)
        '        gdvNangLucCT.SetFocusedRowCellValue("IDNhom", btfilterNhomCV.EditValue)
        '        gdvNangLucCT.SetFocusedRowCellValue("IDTen", btFilterCV.EditValue)
        '        gdvNangLucCT.SetFocusedRowCellValue("SoLuong", 0)
        '        gdvNangLucCT.SetFocusedRowCellValue("KhoiLuong", 0)
        '        gdvNangLucCT.SetFocusedRowCellValue("Diem", 0)
        '        gdvNangLucCT.SetFocusedRowCellValue("DiemChuan", 0)
        '        gdvNangLucCT.SetFocusedRowCellValue("Duyet", False)
        '        gdvNangLucCT.SetFocusedRowCellValue("Modify", True)
        '        ' gdvNangLucCT.Focus()
        '        gdvNangLucCT.FocusedColumn = gdvNangLucCT.Columns("IDDanhSach")
        '        ' SendKeys.Send("{F4}")
        '    Case tabHoatDongHangNgay.Name
        '        gdvHoatDongCT.AddNewRow()
        '        gdvHoatDongCT.SetFocusedRowCellValue("STT", gdvHoatDongCT.RowCount)
        '        gdvHoatDongCT.SetFocusedRowCellValue("NgayBaoCao", tbNgayBaoCao.EditValue)
        '        gdvHoatDongCT.SetFocusedRowCellValue("NgayNhapLieu", tg.Date)
        '        gdvHoatDongCT.SetFocusedRowCellValue("IDUser", Convert.ToInt32(TaiKhoan))
        '        gdvHoatDongCT.SetFocusedRowCellValue("IDNhanVien", btfilterNgThucHien.EditValue)
        '        gdvHoatDongCT.SetFocusedRowCellValue("IDNhom", btfilterNhomCV.EditValue)
        '        gdvHoatDongCT.SetFocusedRowCellValue("IDTen", btFilterCV.EditValue)
        '        gdvHoatDongCT.SetFocusedRowCellValue("SoLuong", 0)
        '        gdvHoatDongCT.SetFocusedRowCellValue("KhoiLuong", 0)
        '        gdvHoatDongCT.SetFocusedRowCellValue("Diem", 0)
        '        gdvHoatDongCT.SetFocusedRowCellValue("DiemChuan", 0)
        '        gdvHoatDongCT.SetFocusedRowCellValue("Duyet", False)
        '        gdvHoatDongCT.SetFocusedRowCellValue("Modify", True)
        '        ' gdvHoatDongCT.Focus()
        '        gdvHoatDongCT.FocusedColumn = gdvHoatDongCT.Columns("IDDanhSach")
        '        'SendKeys.Send("{F4}{F4}")
        '    Case tabDoiMoiSangTao.Name

        'End Select


    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
        'On Error Resume Next
        If keyData = Keys.Enter Then
            If tabBC.SelectedTabPage Is tabHoatDongHangNgay And Not gdvHoatDong.DataSource Is Nothing AndAlso gdvHoatDongCT.FocusedColumn.FieldName = "ChiTiet" Then
                SendKeys.Send("{TAB}")
                Return True
            ElseIf tabBC.SelectedTabPage Is tabVanHoaCongTy And Not gdvVanHoa.DataSource Is Nothing AndAlso gdvVanHoaCT.FocusedColumn.FieldName = "ChiTiet" Then
                SendKeys.Send("{TAB}")
                Return True
            ElseIf tabBC.SelectedTabPage Is tabNangLucChuyenMon And Not gdvNangLuc.DataSource Is Nothing AndAlso gdvNangLucCT.FocusedColumn.FieldName = "ChiTiet" Then
                SendKeys.Send("{TAB}")
                Return True
            End If
        End If
        Return False
    End Function

    
End Class