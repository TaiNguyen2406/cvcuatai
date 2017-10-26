Imports BACSOFT.Db.SqlHelper

Public Class frmDMLuongThuong
    Private LuongThangKT As String = ""
    Private _exit As Boolean = False

    Private Sub frmDMLuongThuong_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbThang.EditValue = Now.ToString("MM")
        tbNam.EditValue = Today.Year
        LoadDSNhomKN()
        LoadDSDinhMucTinhDiem()
        LoadDSPhong()
        LoadDSBP()
        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Int32"))
        tb.Columns.Add("ChucVu", Type.GetType("System.String"))
        Dim r1 = tb.NewRow
        r1("ID") = 0
        r1("ChucVu") = "KS"
        tb.Rows.Add(r1)
        Dim r2 = tb.NewRow
        r2("ID") = 1
        r2("ChucVu") = "KT"
        tb.Rows.Add(r2)
        rcbChucVu.DataSource = tb
        'LoadDS()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btThangTruoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            GridColumn7.Visible = False
            GridColumn16.Visible = False
            GridColumn33.Visible = False
            GridColumn8.Visible = False
            GridColumn5.Visible = False
            GridColumn9.Visible = False
            GridColumn10.Visible = False
            GridColumn11.Visible = False
            GridColumn12.Visible = False
            GridColumn13.Visible = False
            GridColumn6.Visible = False
            colTongChi.Visible = False

            colLevel.OptionsColumn.ReadOnly = True
            colLevel.Visible = False
            colClass.Visible = False
            colLNGop.Visible = False
            colLuongThamChieu.Visible = False
            colDiemThamChieu.Visible = False
            colHSThuong.Visible = False
            colPTDiemMax.Visible = False
            colDiemThuong.Visible = False


        End If
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            gdvCT.OptionsBehavior.ReadOnly = True
        End If
    End Sub

    Public Sub LoadDSBP()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,Ten FROM NhanSu_BoPhan ORDER BY STT")
        If Not tb Is Nothing Then
            rcbBoPhan.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSDinhMucTinhDiem()
        AddParameterWhere("@Thang", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
        Dim sql As String = " SELECT DiemSo_DinhMuc.ID,DiemSo_DinhMuc.CapDo,DiemSo_DinhMuc.Level,DiemSo_DinhMuc.Class,NhanSu_BoPhan.Ten as BoPhan,"
        sql &= " DiemSo_DinhMuc.LoiNhuanGop,DiemSo_DinhMuc.LuongThamChieu,DiemSo_DinhMuc.DiemThamChieu,DiemSo_DinhMuc.HeSoThuong,DiemSo_DinhMuc.DiemPTMax,DiemSo_DinhMuc.DiemThuong"
        sql &= " FROM DiemSo_DinhMuc LEFT JOIN NhanSu_BoPhan ON DiemSo_DinhMuc.IDBoPhan=NhanSu_BoPhan.Ma WHERE DiemSo_DinhMuc.Thang=@Thang ORDER BY IDBoPhan,Level DESC,Class DESC"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rgdvDinhMucTinhDiem.DataSource = tb
            rgdvDinhMucTinhDiem.View.Columns.Clear()
            With rgdvDinhMucTinhDiem.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                Dim colCapDo = .AddField("CapDo")
                colCapDo.Caption = "Mô tả"
                colCapDo.VisibleIndex = 0
                colCapDo.Width = 150

                Dim colLevel = .AddField("Level")
                colLevel.Caption = "Level"
                colLevel.VisibleIndex = 1
                colLevel.Width = 60

                Dim colClass = .AddField("Class")
                colClass.Caption = "Class"
                colClass.VisibleIndex = 2
                colClass.Width = 60

                Dim colPhong = .AddField("BoPhan")
                colPhong.Caption = "Bộ phận"
                colPhong.VisibleIndex = 1
                colPhong.Width = 100
                colPhong.GroupIndex = 0

                Dim colLoiNhuanGop = .AddField("LoiNhuanGop")
                colLoiNhuanGop.Visible = False

                Dim colLuongThamChieu = .AddField("LuongThamChieu")
                colLuongThamChieu.Visible = False

                Dim colDiemThamChieu = .AddField("DiemThamChieu")
                colDiemThamChieu.Visible = False

                Dim colHeSoThuong = .AddField("HeSoThuong")
                colHeSoThuong.Visible = False

                Dim colDiemPTMax = .AddField("DiemPTMax")
                colDiemPTMax.Visible = False

                Dim colDiemThuong = .AddField("DiemThuong")
                colDiemThuong.Visible = False
            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNhomKN()
        AddParameterWhere("@Loai", LoaiTuDien.NhomNoiDungThiCong)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
        If Not tb Is Nothing Then
            rcbNhomKN.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDSDinhMucTinhDiem()
        If KiemTraDuyetLuong(cbThang.EditValue & "/" & tbNam.EditValue.ToString) Then
            lbDuyet.Caption = "Tháng lương đã được duyệt"
            lbDuyet.Glyph = My.Resources.Checked
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                gdvCT.OptionsBehavior.ReadOnly = False
                btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                gdvCT.OptionsBehavior.ReadOnly = True
                btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If
        Else
            lbDuyet.Caption = "Tháng lương chưa được duyệt"
            lbDuyet.Glyph = My.Resources.UnCheck
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                gdvCT.OptionsBehavior.ReadOnly = True
            Else
                btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                gdvCT.OptionsBehavior.ReadOnly = False
            End If
        End If
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"
        sql &= " SELECT NHANSU.Ten AS NhanVien, LUONG.ThamNien, "
        sql &= " 	LUONG.ID,LUONG.IDNhanVien,LUONG.LuongCB,LUONG.LuongThuong,ISNULL(ThuongDotXuat,0)ThuongDotXuat,LUONG.LuongBH,LUONG.PCXang,LUONG.PCAn,LUONG.PCDienThoai,LUONG.PCTrachNhiem,ISNULL(LUONG.MaThuong,0)MaThuong,"
        sql &= " 	LUONG.PC_DVKT_Luong,LUONG.PC_DVKT_Xang,LUONG.PC_DVKT_TrachNhiem,LUONG.HSVanHoa,LUONG.HSNangLuc,LUONG.HSHoatDong,LUONG.HSSangTao,LUONG.HSThamNien,"
        sql &= " 	LUONG.[Month],LUONG.HS_BH_CongTy,LUONG.HS_BH_NhanVien,LUONG.IDDepatment,LUONG.HSThuong,ISNULL(LUONG.DMThuong,0)DMThuong,LUONG.ChucVu,LUONG.NhomKN,"
        sql &= "    convert(bit,0)Modify,ISNULL(LUONG.CTKhongHT,0)CTKhongHT,ISNULL(LUONG.PhatChiTieu,10)PhatChiTieu, IDDinhMucTinhDiem,"
        sql &= "    (LUONG.LuongCB+LUONG.LuongThuong+ISNULL(ThuongDotXuat,0) + (LUONG.PCXang+LUONG.PCAn)*26+LUONG.PCDienThoai+LUONG.PCTrachNhiem + LUONG.PC_DVKT_Luong+LUONG.PC_DVKT_Xang*26+LUONG.PC_DVKT_TrachNhiem + LUONG.LuongBH*ISNULL(LUONG.HS_BH_CongTy,0.22))Tong,"
        sql &= "    LUONG.PTLuong,LUONG.DiemDo,LUONG.DiemVang,LUONG.DiemXanh,LUONG.IDBoPhan,"
        sql &= "    LUONG.LoiNhuanGop,LUONG.LuongThamChieu,LUONG.DiemThamChieu,LUONG.HeSoThuong,LUONG.DiemPTMax,LUONG.DiemThuong,LUONG.[Level],LUONG.Class,NhanSu_BoPhan.MaBP,NHANSU.ChucVu as ChucVuSX"
        '  sql &= "    LUONG.LoiNhuanGop,LUONG.LuongThamChieu,LUONG.DiemThamChieu,LUONG.HeSoThuong,LUONG.DiemPTMax,LUONG.DiemThuong"
        sql &= " INTO #tbtmp"
        sql &= " FROM NHANSU INNER JOIN LUONG ON NHANSU.ID=LUONG.IDNhanVien "
        sql &= " 	AND Convert(int,LEFT(LUONG.[Month],2))=" & cbThang.EditValue & "  AND Convert(int,RIGHT(LUONG.[Month],4))=" & tbNam.EditValue.ToString
        sql &= " LEFT JOIN NhanSu_BoPhan ON NhanSU_BoPhan.Ma=LUONG.IDBoPhan"
        sql &= " WHERE NHANSU.NoiCtac=74"

        sql &= " SELECT * FROM ("
        sql &= " SELECT * FROM #tbtmp"
        sql &= " UNION ALL"
        sql &= " SELECT NHANSU.Ten AS NhanVien,DateDiff(Month,NgayVaoCty,Convert(Datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103))ThamNien,"
        sql &= " 	LUONG.ID,NHANSU.ID AS IDNhanVien,ISNULL(LUONG.LuongCB,0)LuongCB,ISNULL(LUONG.LuongThuong,0)LuongThuong,ISNULL(ThuongDotXuat,0)ThuongDotXuat,ISNULL(LUONG.LuongBH,0)LuongBH,ISNULL(LUONG.PCXang,0)PCXang,ISNULL(LUONG.PCAn,0)PCAn,ISNULL(LUONG.PCDienThoai,0)PCDienThoai,ISNULL(LUONG.PCTrachNhiem,0)PCTrachNhiem,ISNULL(LUONG.MaThuong,0)MaThuong,"
        sql &= " 	ISNULL(LUONG.PC_DVKT_Luong,0)PC_DVKT_Luong,ISNULL(LUONG.PC_DVKT_Xang,0)PC_DVKT_Xang,ISNULL(LUONG.PC_DVKT_TrachNhiem,0)PC_DVKT_TrachNhiem,ISNULL(LUONG.HSVanHoa,0)HSVanHoa,ISNULL(LUONG.HSNangLuc,0)HSNangLuc,ISNULL(LUONG.HSHoatDong,0)HSHoatDong,ISNULL(LUONG.HSSangTao,0)HSSangTao,ISNULL(LUONG.HSThamNien,0)HSThamNien,"
        sql &= " 	LUONG.[Month],ISNULL(LUONG.HS_BH_CongTy,0)HS_BH_CongTy,ISNULL(LUONG.HS_BH_NhanVien,0)HS_BH_NhanVien,NHANSU.IDDepatment,ISNULL(LUONG.HSThuong,0)HSThuong,ISNULL(LUONG.DMThuong,0)DMThuong,LUONG.ChucVu,LUONG.NhomKN,"
        sql &= " convert(bit,0)Modify,ISNULL(LUONG.CTKhongHT,0)CTKhongHT,ISNULL(LUONG.PhatChiTieu,10)PhatChiTieu,IDDinhMucTinhDiem,Convert(float,0)Tong,"
        sql &= "    ISNULL(LUONG.PTLuong,0)PTLuong,isnull(LUONG.DiemDo,50)DiemDo,isnull(LUONG.DiemVang,75)DiemVang,isnull(LUONG.DiemXanh,100)DiemXanh,LUONG.IDBoPhan,"
        sql &= "    LUONG.LoiNhuanGop,LUONG.LuongThamChieu,LUONG.DiemThamChieu,LUONG.HeSoThuong,LUONG.DiemPTMax,LUONG.DiemThuong,LUONG.[Level],LUONG.Class,NhanSu_BoPhan.MaBP,NHANSU.ChucVu as ChucVuSX"
        sql &= " FROM NHANSU LEFT JOIN LUONG ON NHANSU.ID=LUONG.IDNhanVien AND LUONG.ID=-1"
        sql &= " LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=NHanSu.IDBoPhan"
        sql &= " WHERE NHANSU.NoiCtac=74 AND NHANSU.ID NOT IN (SELECT IDNhanVien FROM #tbtmp) "
        sql &= " 	AND  ((month(NgayVaoCty)=" & cbThang.EditValue & " AND Year(NgayVaoCty)=" & tbNam.EditValue.ToString & ") OR Convert(datetime,NgayVaoCty,103)<Convert(Datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103))"
        sql &= " 	AND  ((month(NgayRoiCty)=" & cbThang.EditValue & " AND Year(NgayRoiCty)=" & tbNam.EditValue.ToString & ") OR Convert(datetime,ISNULL(NgayRoiCty,Convert(Datetime,'01/12/2060',103)),103)>Convert(Datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103))"
        sql &= " )tbl"
        sql &= " ORDER BY IDDepatment,MaBP,ChucVuSX,IDNhanVien"
        sql &= " DROP table #tbtmp"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            LuongThangKT = cbThang.EditValue & "/" & tbNam.EditValue
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        On Error Resume Next

        If Not e.Column.FieldName = "Modify" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        Else
            If _exit = True Then Exit Sub
            Dim tong As Double = 0
            'LUONG.PC_DVKT_TrachNhiem + LUONG.LuongBH*ISNULL(LUONG.HS_BH_NhanVien,0))Tong"
            tong = gdvCT.GetRowCellValue(e.RowHandle, "LuongCB") + gdvCT.GetRowCellValue(e.RowHandle, "LuongThuong") + gdvCT.GetRowCellValue(e.RowHandle, "ThuongDotXuat")
            tong += gdvCT.GetRowCellValue(e.RowHandle, "PCXang") + gdvCT.GetRowCellValue(e.RowHandle, "PCAn") + gdvCT.GetRowCellValue(e.RowHandle, "PCDienThoai")
            tong += gdvCT.GetRowCellValue(e.RowHandle, "PCTrachNhiem") + gdvCT.GetRowCellValue(e.RowHandle, "PC_DVKT_Luong") + gdvCT.GetRowCellValue(e.RowHandle, "PC_DVKT_Xang")
            tong += gdvCT.GetRowCellValue(e.RowHandle, "PC_DVKT_TrachNhiem") + gdvCT.GetRowCellValue(e.RowHandle, "LuongBH") * gdvCT.GetRowCellValue(e.RowHandle, "HS_BH_NhanVien")
            _exit = True
            gdvCT.SetRowCellValue(e.RowHandle, "Tong", tong)
            _exit = False
        End If
    End Sub

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick
        If LuongThangKT <> cbThang.EditValue & "/" & tbNam.EditValue Then
            ShowCanhBao("Thông tin tháng lương không khớp nhau, bạn cần thực hiện thao tác tải lại trước khi sửa đổi thông tin !")
            Exit Sub
        End If
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        gdvCT.BeginUpdate()
        Try
            For i As Integer = 0 To gdvCT.RowCount - 1
                AddParameter("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                AddParameter("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
                AddParameter("@ThamNien", gdvCT.GetRowCellValue(i, "ThamNien"))
                AddParameter("@LuongCB", gdvCT.GetRowCellValue(i, "LuongCB"))
                AddParameter("@LuongThuong", gdvCT.GetRowCellValue(i, "LuongThuong"))
                AddParameter("@ThuongDotXuat", gdvCT.GetRowCellValue(i, "ThuongDotXuat"))
                AddParameter("@LuongBH", gdvCT.GetRowCellValue(i, "LuongBH"))
                AddParameter("@PCXang", gdvCT.GetRowCellValue(i, "PCXang"))
                AddParameter("@PCAn", gdvCT.GetRowCellValue(i, "PCAn"))
                AddParameter("@PCDienThoai", gdvCT.GetRowCellValue(i, "PCDienThoai"))
                AddParameter("@PCTrachNhiem", gdvCT.GetRowCellValue(i, "PCTrachNhiem"))
                AddParameter("@PC_DVKT_Luong", gdvCT.GetRowCellValue(i, "PC_DVKT_Luong"))
                AddParameter("@PC_DVKT_Xang", gdvCT.GetRowCellValue(i, "PC_DVKT_Xang"))
                AddParameter("@PC_DVKT_TrachNhiem", gdvCT.GetRowCellValue(i, "PC_DVKT_TrachNhiem"))
                AddParameter("@HSVanHoa", gdvCT.GetRowCellValue(i, "HSVanHoa"))
                AddParameter("@HSNangLuc", gdvCT.GetRowCellValue(i, "HSNangLuc"))
                AddParameter("@HSHoatDong", gdvCT.GetRowCellValue(i, "HSHoatDong"))
                AddParameter("@HSSangTao", gdvCT.GetRowCellValue(i, "HSSangTao"))
                AddParameter("@HSThamNien", gdvCT.GetRowCellValue(i, "HSThamNien"))
                AddParameter("@HS_BH_CongTy", gdvCT.GetRowCellValue(i, "HS_BH_CongTy"))
                AddParameter("@HS_BH_NhanVien", gdvCT.GetRowCellValue(i, "HS_BH_NhanVien"))

                'AddParameter("@HSThuong", gdvCT.GetRowCellValue(i, "HSThuong"))
                'AddParameter("@DMThuong", gdvCT.GetRowCellValue(i, "DMThuong"))

                AddParameter("@ChucVu", gdvCT.GetRowCellValue(i, "ChucVu"))
                AddParameter("@NhomKN", gdvCT.GetRowCellValue(i, "NhomKN"))
                AddParameter("@MaThuong", gdvCT.GetRowCellValue(i, "MaThuong"))
                AddParameter("@IDDinhMucTinhDiem", gdvCT.GetRowCellValue(i, "IDDinhMucTinhDiem"))
                AddParameter("@IDDepatment", gdvCT.GetRowCellValue(i, "IDDepatment"))
                If IsNumeric(gdvCT.GetRowCellValue(i, "CTKhongHT")) Then
                    AddParameter("@CTKhongHT", gdvCT.GetRowCellValue(i, "CTKhongHT"))
                End If

                If IsNumeric(gdvCT.GetRowCellValue(i, "PhatChiTieu")) Then
                    AddParameter("@PhatChiTieu", gdvCT.GetRowCellValue(i, "PhatChiTieu"))
                End If

                If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                    AddParameter("@PTLuong", gdvCT.GetRowCellValue(i, "PTLuong"))
                    AddParameter("@DiemDo", gdvCT.GetRowCellValue(i, "DiemDo"))
                    AddParameter("@DiemVang", gdvCT.GetRowCellValue(i, "DiemVang"))
                    AddParameter("@DiemXanh", gdvCT.GetRowCellValue(i, "DiemXanh"))
                    AddParameter("@IDBoPhan", gdvCT.GetRowCellValue(i, "IDBoPhan"))

                    AddParameter("@LoiNhuanGop", gdvCT.GetRowCellValue(i, "LoiNhuanGop"))
                    AddParameter("@LuongThamChieu", gdvCT.GetRowCellValue(i, "LuongThamChieu"))
                    AddParameter("@DiemThamChieu", gdvCT.GetRowCellValue(i, "DiemThamChieu"))
                    AddParameter("@HeSoThuong", gdvCT.GetRowCellValue(i, "HeSoThuong"))
                    AddParameter("@DiemPTMax", gdvCT.GetRowCellValue(i, "DiemPTMax"))
                    AddParameter("@DiemThuong", gdvCT.GetRowCellValue(i, "DiemThuong"))
                    AddParameter("@Level", gdvCT.GetRowCellValue(i, "Level"))
                    AddParameter("@Class", gdvCT.GetRowCellValue(i, "Class"))
                End If

                If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Then
                    Dim oID As Integer = doInsert("LUONG")
                    If oID = Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvCT.SetRowCellValue(i, "ID", oID)
                Else
                    AddParameterWhere("@ID", gdvCT.GetRowCellValue(i, "ID"))
                    If doUpdate("LUONG", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvCT.SetRowCellValue(i, "Modify", False)
                End If
            Next
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT COUNT([Month]) FROM tblDuyetLuong WHERE [Month]='" & cbThang.EditValue & "/" & tbNam.EditValue & "'")
            If Not tb Is Nothing Then
                If Convert.ToInt32(tb.Rows(0)(0)) = 0 Then
                    AddParameter("@Month", cbThang.EditValue & "/" & tbNam.EditValue)
                    AddParameter("@Duyet", False)
                    If doInsert("tblDuyetLuong") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                End If
            End If

            gdvCT.EndUpdate()
            ShowAlert("Đã lưu !")
        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
        End Try

    End Sub

    Private Sub btThangTruoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThangTruoc.ItemClick
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"
        sql &= " SELECT LUONG.ID, NHANSU.Ten AS NhanVien,LUONG.IDNhanVien, DateDiff(Month,NgayVaoCty,Convert(Datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103))ThamNien,LUONG.IDDepatment, "
        sql &= " ISNULL(LUONG.HSThuong,0)HSThuong,ISNULL(LUONG.DMThuong,0)DMThuong,Convert(float,0)ThuongDotXuat,ISNULL(LUONG.MaThuong,0)MaThuong,LUONG.ChucVu,LUONG.NhomKN,"
        sql &= " convert(bit,0)Modify,Convert(tinyint,0) AS CTKhongHT, LUONG.PhatChiTieu,LUONG.PTLuong,LUONG.DiemDo,LUONG.DiemVang,LUONG.DiemXanh,LUONG.IDBoPhan,"
        sql &= " (SELECT ID FROM DiemSo_DinhMuc"
        sql &= " WHERE IDBoPhan=(SELECT IDBoPhan FROM DiemSo_DinhMuc WHERE ID=LUONG.IDDinhMucTinhDiem)"
        sql &= " AND Level=LUONG.Level"
        sql &= " AND Class=LUONG.Class"
        sql &= " AND Thang= '" & cbThang.EditValue & "/" & tbNam.EditValue.ToString & "' ) as IDDinhMucTinhDiem,"
        sql &= " LUONG.LoiNhuanGop,LUONG.LuongThamChieu,LUONG.DiemThamChieu,LUONG.HeSoThuong,LUONG.DiemPTMax,LUONG.DiemThuong,LUONG.Level,LUONG.Class INTO #tbtmp2"
        sql &= " FROM NHANSU INNER JOIN LUONG ON NHANSU.ID=LUONG.IDNhanVien "
        sql &= " 	AND Convert(int,LEFT(LUONG.[Month],2))= " & cbThang.EditValue & "   AND Convert(int,RIGHT(LUONG.[Month],4))=" & tbNam.EditValue.ToString
        sql &= " WHERE NHANSU.NoiCtac=74"
        sql &= " SELECT * INTO #tbtmp"
        sql &= " FROM #tbtmp2"
        sql &= " UNION ALL"
        sql &= " SELECT LUONG.ID,NHANSU.Ten AS NhanVien,NHANSU.ID AS IDNhanVien,DateDiff(Month,NgayVaoCty,Convert(Datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103))ThamNien,NHANSU.IDDepatment, "
        sql &= " ISNULL(LUONG.HSThuong,0)HSThuong,ISNULL(LUONG.DMThuong,0)DMThuong,Convert(float,0)ThuongDotXuat,ISNULL(LUONG.MaThuong,0)MaThuong,LUONG.ChucVu,LUONG.NhomKN,"
        sql &= " convert(bit,0)Modify,Convert(tinyint,0) AS CTKhongHT, LUONG.PhatChiTieu,LUONG.PTLuong,LUONG.DiemDo,LUONG.DiemVang,LUONG.DiemXanh,LUONG.IDBoPhan,"
        sql &= " (SELECT ID FROM DiemSo_DinhMuc"
        sql &= " WHERE IDBoPhan=(SELECT IDBoPhan FROM DiemSo_DinhMuc WHERE ID=LUONG.IDDinhMucTinhDiem)"
        sql &= " AND Level=LUONG.Level"
        sql &= " AND Class=LUONG.Class"
        sql &= " AND Thang= '" & cbThang.EditValue & "/" & tbNam.EditValue.ToString & "' ) as IDDinhMucTinhDiem,"
        sql &= " LUONG.LoiNhuanGop,LUONG.LuongThamChieu,LUONG.DiemThamChieu,LUONG.HeSoThuong,LUONG.DiemPTMax,LUONG.DiemThuong,LUONG.Level,LUONG.Class"
        sql &= " FROM NHANSU LEFT JOIN LUONG ON NHANSU.ID=LUONG.IDNhanVien AND LUONG.ID=-1"
        sql &= " WHERE NHANSU.NoiCtac=74 AND NHANSU.ID NOT IN (SELECT IDNhanVien FROM #tbtmp2) "
        sql &= " 	AND  ((month(NgayVaoCty)= " & cbThang.EditValue & "  AND Year(NgayVaoCty)=" & tbNam.EditValue.ToString & ") OR Convert(datetime,NgayVaoCty,103)<Convert(Datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103))"
        sql &= " 	AND  ((month(NgayRoiCty)= " & cbThang.EditValue & "  AND Year(NgayRoiCty)=" & tbNam.EditValue.ToString & ") OR Convert(datetime,ISNULL(NgayRoiCty,Convert(Datetime,'01/12/2060',103)),103)>Convert(Datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103))"

        sql &= " SELECT #tbtmp.ID,#tbtmp.NhanVien,#tbtmp.ThamNien,#tbtmp.IDNhanVien,#tbtmp.IDDepatment, "
        sql &= " 	ISNULL(tb2.LuongCB,0)LuongCB,ISNULL(tb2.LuongThuong,0)LuongThuong,ISNULL(tb2.LuongBH,0)LuongBH,ISNULL(tb2.PCXang,0)PCXang,ISNULL(tb2.PCAn,0)PCAn,ISNULL(tb2.PCDienThoai,0)PCDienThoai,ISNULL(tb2.PCTrachNhiem,0)PCTrachNhiem,"
        sql &= " 	ISNULL(tb2.PC_DVKT_Luong,0)PC_DVKT_Luong,ISNULL(tb2.PC_DVKT_Xang,0)PC_DVKT_Xang,ISNULL(tb2.PC_DVKT_TrachNhiem,0)PC_DVKT_TrachNhiem,ISNULL(tb2.HSVanHoa,0)HSVanHoa,ISNULL(tb2.HSNangLuc,0)HSNangLuc,ISNULL(tb2.HSHoatDong,0)HSHoatDong,ISNULL(tb2.HSSangTao,0)HSSangTao,ISNULL(tb2.HSThamNien,0)HSThamNien,"
        sql &= " 	tb2.[Month],ISNULL(tb2.HS_BH_CongTy,0)HS_BH_CongTy,ISNULL(tb2.HS_BH_NhanVien,0)HS_BH_NhanVien, "
        sql &= " ISNULL(tb2.HSThuong,0)HSThuong,ISNULL(tb2.DMThuong,0)DMThuong,ISNULL(tb2.ThuongDotXuat,0)ThuongDotXuat,ISNULL(tb2.MaThuong,0)MaThuong,tb2.ChucVu,tb2.NhomKN,tb2.Modify,0 as CTKhongHT,tb2.PhatChiTieu,tb2.PTLuong,tb2.DiemDo,tb2.DiemVang,tb2.DiemXanh,tb2.IDBoPhan,tb2.IDDinhMucTinhDiem,tb2.LoiNhuanGop,tb2.LuongThamChieu,tb2.DiemThamChieu,tb2.HeSoThuong,tb2.DiemPTMax,tb2.DiemThuong,tb2.Level,tb2.Class"
        sql &= " FROM #tbtmp"

        sql &= " LEFT JOIN (SELECT NHANSU.Ten AS NhanVien, DateDiff(Month,NgayVaoCty,Convert(Datetime,'" & DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, cbThang.EditValue + 1, 0)) & "',103))ThamNien, "
        sql &= " 	LUONG.ID,LUONG.IDNhanVien,LUONG.LuongCB,LUONG.LuongThuong,LUONG.LuongBH,LUONG.PCXang,LUONG.PCAn,LUONG.PCDienThoai,LUONG.PCTrachNhiem,"
        sql &= " 	LUONG.PC_DVKT_Luong,LUONG.PC_DVKT_Xang,LUONG.PC_DVKT_TrachNhiem,LUONG.HSVanHoa,LUONG.HSNangLuc,LUONG.HSHoatDong,LUONG.HSSangTao,LUONG.HSThamNien,"
        sql &= " 	LUONG.[Month],LUONG.HS_BH_CongTy,LUONG.HS_BH_NhanVien,LUONG.IDDepatment,"
        sql &= " LUONG.HSThuong,LUONG.DMThuong, Convert(float,0)ThuongDotXuat,LUONG.MaThuong,LUONG.ChucVu,LUONG.NhomKN,"
        sql &= " convert(bit,0)Modify,Convert(tinyint,0) AS CTKhongHT, LUONG.PhatChiTieu,LUONG.PTLuong,LUONG.DiemDo,LUONG.DiemVang,LUONG.DiemXanh,LUONG.IDBoPhan,"
        sql &= " (SELECT ID FROM DiemSo_DinhMuc"
        sql &= " WHERE IDBoPhan=(SELECT IDBoPhan FROM DiemSo_DinhMuc WHERE ID=LUONG.IDDinhMucTinhDiem)"
        sql &= " AND Level=LUONG.Level"
        sql &= " AND Class=LUONG.Class"
        sql &= " AND Thang= '" & cbThang.EditValue & "/" & tbNam.EditValue.ToString & "' ) as IDDinhMucTinhDiem,"
        sql &= " LUONG.LoiNhuanGop,LUONG.LuongThamChieu,LUONG.DiemThamChieu,LUONG.HeSoThuong,LUONG.DiemPTMax,LUONG.DiemThuong,LUONG.Level,LUONG.Class "
        sql &= " FROM NHANSU INNER JOIN LUONG ON NHANSU.ID=LUONG.IDNhanVien "
        If Convert.ToInt16(cbThang.EditValue) - 1 = 0 Then
            sql &= " AND Convert(int,LEFT(LUONG.[Month],2))=12  AND Convert(int,RIGHT(LUONG.[Month],4))=" & (Convert.ToInt16(tbNam.EditValue) - 1).ToString
        Else
            sql &= " AND Convert(int,LEFT(LUONG.[Month],2))=  " & (Convert.ToInt16(cbThang.EditValue) - 1).ToString & "  AND Convert(int,RIGHT(LUONG.[Month],4))=" & tbNam.EditValue.ToString
        End If
        sql &= " WHERE NHANSU.NoiCtac=74 AND NHANSU.ID IN (SELECT IDNhanVien FROM #tbtmp2))tb2 ON #tbtmp.IDNhanVien=tb2.IDNhanVien"

        sql &= " ORDER BY #tbtmp.ThamNien DESC,#tbtmp.IDNhanVien"
        sql &= " DROP table #tbtmp2"
        sql &= " DROP table #tbtmp"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub



    Private Sub rcbChucVu_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbChucVu.ButtonClick
        If e.Button.Index = 1 Then
            gdvCT.SetFocusedRowCellValue("ChucVu", DBNull.Value)
        End If
    End Sub

    Private Sub rcbNhomKN_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhomKN.ButtonClick
        If e.Button.Index = 1 Then
            gdvCT.SetFocusedRowCellValue("NhomKN", DBNull.Value)
        End If
    End Sub


    Private Sub btCNDMThuongPhong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim f As New frmDinhMucThuongPhong
        f.ShowDialog()
    End Sub

    Private Sub rgdvDinhMucTinhDiem_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rgdvDinhMucTinhDiem.ButtonClick
        If e.Button.Index = 1 Then
            gdvCT.SetFocusedRowCellValue("IDDinhMucTinhDiem", DBNull.Value)
        End If
    End Sub

    Private Sub rgdvDinhMucTinhDiem_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles rgdvDinhMucTinhDiem.EditValueChanged
        On Error Resume Next
        Dim dr As DataRowView = CType(sender, DevExpress.XtraEditors.GridLookUpEdit).GetSelectedDataRow
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        gdvCT.BeginUpdate()
        gdvCT.SetFocusedRowCellValue("Class", dr("Class"))
        gdvCT.SetFocusedRowCellValue("Level", dr("Level"))
        gdvCT.SetFocusedRowCellValue("LoiNhuanGop", dr("LoiNhuanGop"))
        gdvCT.SetFocusedRowCellValue("LuongThamChieu", dr("LuongThamChieu"))
        gdvCT.SetFocusedRowCellValue("DiemThamChieu", dr("DiemThamChieu"))
        gdvCT.SetFocusedRowCellValue("HeSoThuong", dr("HeSoThuong"))
        gdvCT.SetFocusedRowCellValue("DiemPTMax", dr("DiemPTMax"))
        gdvCT.SetFocusedRowCellValue("DiemThuong", dr("DiemThuong"))
        gdvCT.EndUpdate()
        ' ShowAlert("Level " + dr("Level").ToString())
    End Sub
End Class