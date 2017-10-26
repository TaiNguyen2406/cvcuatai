Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraGrid.Views.BandedGrid

Public Class frmChiTieu
    Private _exit As Boolean = False
    Private tbNhanVien As DataTable
    Private tbPhong As DataTable
    Private tbNV As DataTable

    Private isFormLoading As Boolean = True

    Private Sub frmChiTieu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        tbNam.EditValue = Today.Year
        LoadDsPhong()
        LoadDsNhanVien()
        LoadDsNhomChiTieu()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btCapNhatCT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btLuuKetQua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            gdvCT.OptionsBehavior.ReadOnly = True
        End If
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cbPhong.Enabled = False
            cbNhanVien.Enabled = False
            cbLoaiChiTieu.Enabled = False
        End If

        cbLoaiChiTieu.EditValue = "Nhân viên"
        cbPhong.EditValue = MaPhongBan
        cbNhanVien.EditValue = TaiKhoan


        isFormLoading = False
        btXem.PerformClick()

        'LoadDSDinhMuc()

    End Sub

    Public Sub LoadDsPhong()
        Dim sql As String = "SELECT ID,Ten FROM DEPATMENT"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
            tbPhong = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDsNhanVien()
        Dim sql As String = ""
        If cbPhong.EditValue Is Nothing Then
            sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER By ID"
            sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER By MaTruyCap,ID"
        Else
            sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment=@IDPhong ORDER By ID"
            sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment=@IDPhong ORDER By MaTruyCap,ID"
            AddParameterWhere("@IDPhong", cbPhong.EditValue)
        End If

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbNhanVien.DataSource = ds.Tables(0)
            tbNhanVien = ds.Tables(0)
            tbNV = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Public Sub LoadDsNhomChiTieu()
        AddParameterWhere("@Loai", LoaiTuDien.NhomChiTieu)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@Loai")
        If Not tb Is Nothing Then
            rcbNhomChiTieu.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub KiemTraChiTieuMoi()
        If cbLoaiChiTieu.EditValue <> "Phòng" And cbPhong.EditValue Is Nothing Then
            ShowCanhBao("Cần chọn phòng ban trước khi thực hiện thao tác này !")
            Exit Sub
        End If
        ShowWaiting("Đang kiểm tra bản chỉ tiêu mới ...")
        Dim sql As String = ""
        sql &= " DECLARE @tbDMChiTieu table"
        sql &= " ("
        sql &= " 	IDChiTieu int,"
        sql &= " 	IDNhom int,"
        sql &= " 	NoiDung nvarchar(500),"
        sql &= " 	IDThucHien int,"
        sql &= " 	Thang nvarchar(7),"
        sql &= "    ChiTieu float"
        sql &= " )"

        sql &= " DECLARE @tbThang table"
        sql &= " ("
        sql &= "    Thang nvarchar(7)"
        sql &= " )"
        For i As Integer = 1 To 12
            sql &= " INSERT INTO @tbThang"
            If i < 10 Then
                sql &= " VALUES('0" & i.ToString & "/" & tbNam.EditValue.ToString & "')"
            Else
                sql &= " VALUES('" & i.ToString & "/" & tbNam.EditValue.ToString & "')"
            End If
        Next

        sql &= " INSERT INTO @tbDMChiTieu"
        sql &= " SELECT tblTuDien.ID As IDChiTieu,tblTuDien.IDP AS IDNhom,tblTuDien.NoiDung, "
        sql &= " 	tbThucHien.ID AS IDThucHien,[@tbThang].Thang,Convert(float,0)ChiTieu"
        sql &= " FROM tblTuDien "
        If cbLoaiChiTieu.EditValue = "Phòng" Then
            sql &= " CROSS JOIN (SELECT ID,Ten FROM DEPATMENT)tbThucHien"
        Else
            sql &= " CROSS JOIN (SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment=@IDPhong)tbThucHien"
            AddParameterWhere("@IDPhong", cbPhong.EditValue)
        End If

        sql &= " CROSS JOIN @tbThang"
        sql &= " WHERE tblTuDien.Loai=@ChiTieu "

        sql &= " SELECT [@tbDMChiTieu].IDChiTieu,[@tbDMChiTieu].IDThucHien,[@tbDMChiTieu].Thang,@Loai As Loai, tblChiTieu.ID FROM @tbDMChiTieu"
        sql &= " LEFT JOIN tblChiTieu ON tblChiTieu.Loai=@Loai AND right(tblChiTieu.Thang,4)=@Nam AND tblChiTieu.IDChiTieu = [@tbDMChiTieu].IDChiTieu AND tblChiTieu.IdThucHien = [@tbDMChiTieu].IDThucHien"

        If cbLoaiChiTieu.EditValue = "Phòng" Then
            sql &= " INNER JOIN tblPhanBoChiTieu ON [@tbDMChiTieu].IDChiTieu = tblPhanBoChiTieu.IDChiTieu AND [@tbDMChiTieu].IDThucHien=tblPhanBoChiTieu.IDPhong"
        Else
            sql &= " INNER JOIN tblPhanBoChiTieu ON [@tbDMChiTieu].IDChiTieu = tblPhanBoChiTieu.IDChiTieu AND tblPhanBoChiTieu.IDPhong = @IDPhong2"
            AddParameterWhere("@IDPhong2", cbPhong.EditValue)
        End If
        sql &= " WHERE tblChiTieu.ID IS NULL"

        AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
        AddParameterWhere("@Nam", tbNam.EditValue)
        If cbLoaiChiTieu.EditValue = "Phòng" Then
            AddParameterWhere("@Loai", 0)
        Else
            AddParameterWhere("@Loai", 1)
        End If

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        CloseWaiting()
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If ShowCauHoi("Có chỉ tiêu mới, bạn có muốn cập nhật không ? " & tb.Rows.Count) Then
                    ShowWaiting("Đang cập nhật chỉ tiêu mới ...")
                    sql = ""
                    sql &= " DECLARE @tbDMChiTieu table"
                    sql &= " ("
                    sql &= " 	IDChiTieu int,"
                    sql &= " 	IDNhom int,"
                    sql &= " 	NoiDung nvarchar(500),"
                    sql &= " 	IDThucHien int,"
                    sql &= " 	Thang nvarchar(7),"
                    sql &= "    ChiTieu float"
                    sql &= " )"

                    sql &= " DECLARE @tbThang table"
                    sql &= " ("
                    sql &= "    Thang nvarchar(7)"
                    sql &= " )"

                    For i As Integer = 1 To 12
                        sql &= " INSERT INTO @tbThang"
                        If i < 10 Then
                            sql &= " VALUES('0" & i.ToString & "/" & tbNam.EditValue.ToString & "')"
                        Else
                            sql &= " VALUES('" & i.ToString & "/" & tbNam.EditValue.ToString & "')"
                        End If
                    Next

                    sql &= " INSERT INTO @tbDMChiTieu"
                    sql &= " SELECT tblTuDien.ID As IDChiTieu,tblTuDien.IDP AS IDNhom,tblTuDien.NoiDung, "
                    sql &= " 	tbThucHien.ID AS IDThucHien,[@tbThang].Thang,Convert(float,0)ChiTieu"
                    sql &= " FROM tblTuDien "
                    If cbLoaiChiTieu.EditValue = "Phòng" Then
                        sql &= " CROSS JOIN (SELECT ID,Ten FROM DEPATMENT)tbThucHien"
                    Else
                        sql &= " CROSS JOIN (SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 AND IDDepatment=@IDPhong)tbThucHien"
                        AddParameterWhere("@IDPhong", cbPhong.EditValue)
                    End If
                    sql &= " CROSS JOIN @tbThang"
                    sql &= " WHERE tblTuDien.Loai=@ChiTieu"

                    sql &= " INSERT INTO tblChiTieu (IDChiTieu,IDThucHien,Thang,Loai)"
                    sql &= " SELECT [@tbDMChiTieu].IDChiTieu,[@tbDMChiTieu].IDThucHien,[@tbDMChiTieu].Thang,@Loai As Loai FROM @tbDMChiTieu"
                    sql &= " LEFT JOIN tblChiTieu ON tblChiTieu.Loai=@Loai AND right(tblChiTieu.Thang,4)=@Nam AND tblChiTieu.IDChiTieu = [@tbDMChiTieu].IDChiTieu AND tblChiTieu.IdThucHien = [@tbDMChiTieu].IDThucHien"
                    If cbLoaiChiTieu.EditValue = "Phòng" Then
                        sql &= " INNER JOIN tblPhanBoChiTieu ON [@tbDMChiTieu].IDChiTieu = tblPhanBoChiTieu.IDChiTieu AND [@tbDMChiTieu].IDThucHien=tblPhanBoChiTieu.IDPhong"
                    Else
                        sql &= " INNER JOIN tblPhanBoChiTieu ON [@tbDMChiTieu].IDChiTieu = tblPhanBoChiTieu.IDChiTieu AND tblPhanBoChiTieu.IDPhong = @IDPhong2"
                        AddParameterWhere("@IDPhong2", cbPhong.EditValue)
                    End If
                    sql &= " WHERE tblChiTieu.ID IS NULL"

                    AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
                    AddParameterWhere("@Nam", tbNam.EditValue)
                    If cbLoaiChiTieu.EditValue = "Phòng" Then
                        AddParameterWhere("@Loai", 0)
                    Else
                        AddParameterWhere("@Loai", 1)
                    End If
                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        CloseWaiting()
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        ShowAlert("Đã cập nhật!")
                        CloseWaiting()
                    End If
                End If
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub

    Public Sub LoadDSChiTieuPhong()

        Dim sql As String = ""
        sql &= " SELECT * FROM"
        sql &= " ("
        sql &= " SELECT DISTINCT  IDChiTieu, tblTuDien.IDP AS IDNhom, tblTuDien.NoiDung, "

        For i As Integer = 1 To 12
            If i < 10 Then
                sql &= " Convert(float,0) AS [0" & i.ToString & "/" & tbNam.EditValue.ToString & "],"
            Else
                sql &= " Convert(float,0) AS [" & i.ToString & "/" & tbNam.EditValue.ToString & "],"
            End If
        Next

        sql &= " Convert(float,0) AS Tong,	tblTuDien.Ma"
        sql &= " FROM tblChiTieu"
        sql &= " INNER JOIN tblTuDien ON  tblTuDien.Loai=@ChiTieu AND tblChiTieu.IDChiTieu=tblTuDien.ID"
        sql &= " WHERE IDThucHien=@Phong AND tblChiTieu.Loai=0"
        sql &= " )tb"
        sql &= " ORDER BY IDNhom,Ma"

        sql &= " SELECT IDChiTieu,Thang,ChiTieu FROM tblChiTieu WHERE right(Thang,4)=@Nam AND IDThucHien=@Phong "

        AddParameterWhere("@Phong", cbPhong.EditValue)
        AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
        AddParameterWhere("@Nam", tbNam.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvCT.Columns.Clear()
            gdvCT.Bands.Clear()

            gdv.DataSource = ds.Tables(0)
            gdvCT.BeginUpdate()
            Dim bandMain = createBand("MainB", "", False)


            gdvCT.Columns("NoiDung").OptionsColumn.ReadOnly = True
            gdvCT.Columns("NoiDung").VisibleIndex = 0
            gdvCT.Columns("NoiDung").Width = 200
            gdvCT.Columns("NoiDung").Caption = "Chỉ tiêu"
            gdvCT.Columns("NoiDung").OwnerBand = bandMain

            For i As Integer = 3 To gdvCT.Columns.Count - 1
                gdvCT.Columns(i).OwnerBand = bandMain
                gdvCT.Columns(i).ColumnEdit = tbN0
                gdvCT.Columns(i).VisibleIndex = i
            Next

            gdvCT.Columns("IDChiTieu").Visible = False
            gdvCT.Columns("IDChiTieu").OwnerBand = bandMain
            gdvCT.Columns("Ma").Visible = False
            gdvCT.Columns("Ma").OwnerBand = bandMain
            gdvCT.Columns("IDNhom").GroupIndex = 0
            'gdvCT.Columns("IDNhom").Width = 200
            'gdvCT.Columns("IDNhom").VisibleIndex = 1
            gdvCT.Columns("IDNhom").Caption = "Nhóm"
            gdvCT.Columns("IDNhom").ColumnEdit = rcbNhomChiTieu
            gdvCT.Columns("IDNhom").OwnerBand = bandMain



            gdvCT.Columns("Tong").Caption = "Tổng"
            gdvCT.Columns("Tong").OptionsColumn.ReadOnly = True
            gdvCT.Columns("Tong").OwnerBand = bandMain
            ' gdvCT.Columns("TOng").Width = 75



            _exit = True

            For i As Integer = 0 To gdvCT.DataRowCount - 1
                Dim _Tong As Double = 0
                For j As Integer = 0 To gdvCT.Columns.Count - 1
                    Dim r() As DataRow = ds.Tables(1).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.Columns(j).FieldName.ToString & "'")
                    If r.Length > 0 Then
                        gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, r(0)("ChiTieu"))
                        _Tong += r(0)("ChiTieu")
                    End If
                Next
                gdvCT.SetRowCellValue(i, "Tong", _Tong)

            Next
            gdvCT.EndUpdate()
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
            _exit = False


        Else

            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Function createBand(_Ten As String, _TieuDe As String, Optional _HienTieuDe As Boolean = True) As GridBand
        Dim b As New GridBand
        b.Caption = _TieuDe
        b.Name = _Ten
        b.OptionsBand.ShowCaption = _HienTieuDe
        gdvCT.Bands.Add(b)
        Return b
    End Function

    Public Sub loadDSChiTieuMotNhanVien()

        Dim sql As String = ""
        sql &= " SELECT * FROM"
        sql &= " ("
        sql &= " SELECT DISTINCT  IDChiTieu, tblTuDien.IDP AS IDNhom, tblTuDien.NoiDung,tblTuDien.Diem AS MaCT, "

        For i As Integer = 1 To 12
            If i < 10 Then
                sql &= " Convert(float,0) AS [0" & i.ToString & "/" & tbNam.EditValue.ToString & "],"
                sql &= " Convert(float,NULL) AS [0" & i.ToString & "/" & tbNam.EditValue.ToString & "_HT],"
                sql &= " Convert(float,NULL) AS [0" & i.ToString & "/" & tbNam.EditValue.ToString & "_TT],"
            Else
                sql &= " Convert(float,0) AS [" & i.ToString & "/" & tbNam.EditValue.ToString & "],"
                sql &= " Convert(float,NULL) AS [" & i.ToString & "/" & tbNam.EditValue.ToString & "_HT],"
                sql &= " Convert(float,NULL) AS [" & i.ToString & "/" & tbNam.EditValue.ToString & "_TT],"
            End If
        Next

        sql &= " Convert(float,0) AS Tong,Convert(float,0) AS HoanThanh,tblTuDien.Ma,convert(bit,0)Modify"
        sql &= " FROM tblChiTieu"
        sql &= " INNER JOIN tblTuDien ON  tblTuDien.Loai=@ChiTieu AND tblChiTieu.IDChiTieu=tblTuDien.ID"
        sql &= " WHERE IDThucHien=@NhanVien AND tblChiTieu.Loai=1 and right(tblChiTieu.Thang,4)=@Nam"
        sql &= " )tb"
        sql &= " ORDER BY IDNhom,Ma"

        sql &= " SELECT IDChiTieu,Thang,ChiTieu,KetQua FROM tblChiTieu WHERE right(Thang,4)=@Nam AND IDThucHien=@NhanVien AND tblChiTieu.Loai=1 "

        AddParameterWhere("@NhanVien", cbNhanVien.EditValue)
        AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
        AddParameterWhere("@Nam", tbNam.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        'Dim objGetData As New DbWebBaoAn
        'objGetData.AddParameterWhere("@Nam", tbNam.EditValue)
        'objGetData.AddParameterWhere("@IDNV", cbNhanVien.EditValue)
        'Dim dsKQ1 As DataTable = objGetData.ExecutePrcDataTable("prc_get_BaoCaoChiTieu2")
        Dim dsKQ As New DataTable
        'If dsKQ1 Is Nothing Then
        '    ShowBaoLoi(objGetData.LoiNgoaiLe)
        'Else
        '    dsKQ.Merge(dsKQ1)
        'End If

        AddParameterWhere("@Nam", tbNam.EditValue)
        AddParameterWhere("@Phong", cbPhong.EditValue)
        AddParameterWhere("@IDNV", cbNhanVien.EditValue)
        Dim dsKQ2 As DataTable = ExecutePrcDataTable("prc_get_BaoCaoChiTieu")

        If dsKQ2 Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            dsKQ.Merge(dsKQ2)
        End If

        If Not ds Is Nothing Then
            gdvCT.Columns.Clear()
            gdvCT.Bands.Clear()
            gdvCT.ColumnPanelRowHeight = 35
            gdv.DataSource = ds.Tables(0)
            gdvCT.BeginUpdate()

            Dim bandMain = createBand("MainB", "", False)
            bandMain.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("NoiDung").OptionsColumn.ReadOnly = True
            gdvCT.Columns("NoiDung").VisibleIndex = 0
            gdvCT.Columns("NoiDung").Width = 200
            gdvCT.Columns("NoiDung").Caption = "Chỉ tiêu"
            gdvCT.Columns("NoiDung").OwnerBand = bandMain

            For i As Integer = 3 To gdvCT.Columns.Count - 1
                If gdvCT.Columns(i).FieldName.Length = 7 Then
                    Dim band = createBand(gdvCT.Columns(i).FieldName, gdvCT.Columns(i).FieldName, True)
                    gdvCT.Columns(i).ColumnEdit = tbN0
                    gdvCT.Columns(i).Caption = "Chỉ tiêu"
                    gdvCT.Columns(i).VisibleIndex = 1
                    gdvCT.Columns(i).Width = 50
                    gdvCT.Columns(i).OwnerBand = band

                    gdvCT.Columns(i + 1).ColumnEdit = tbN0
                    gdvCT.Columns(i + 1).Caption = "Hoàn thành"
                    gdvCT.Columns(i + 1).VisibleIndex = 2
                    gdvCT.Columns(i + 1).Width = 50
                    gdvCT.Columns(i + 1).OwnerBand = band
                    Try
                        gdvCT.Columns(i + 2).ColumnEdit = tbPT
                        gdvCT.Columns(i + 2).Caption = "Tiến độ"
                        gdvCT.Columns(i + 2).VisibleIndex = 3
                        gdvCT.Columns(i + 2).Width = 60
                        gdvCT.Columns(i + 2).OwnerBand = band
                        gdvCT.Columns(i + 2).Tag = "c"
                    Catch ex As Exception

                    End Try

                End If

            Next

            gdvCT.Columns("Tong").Caption = "Tổng"
            gdvCT.Columns("Tong").OptionsColumn.ReadOnly = True
            gdvCT.Columns("Tong").VisibleIndex = 1
            gdvCT.Columns("Tong").Width = 50
            gdvCT.Columns("Tong").OwnerBand = bandMain

            gdvCT.Columns("HoanThanh").Caption = "Hoàn thành"
            gdvCT.Columns("HoanThanh").OptionsColumn.ReadOnly = True
            gdvCT.Columns("HoanThanh").VisibleIndex = 2
            gdvCT.Columns("HoanThanh").Width = 50
            gdvCT.Columns("HoanThanh").OwnerBand = bandMain

            gdvCT.Columns("IDChiTieu").Visible = False
            gdvCT.Columns("IDChiTieu").OwnerBand = bandMain
            gdvCT.Columns("Modify").Visible = False
            gdvCT.Columns("Modify").OwnerBand = bandMain
            gdvCT.Columns("Ma").Visible = False
            gdvCT.Columns("Ma").OwnerBand = bandMain
            gdvCT.Columns("MaCT").Visible = False
            gdvCT.Columns("MaCT").OwnerBand = bandMain
            gdvCT.Columns("IDNhom").GroupIndex = 0
            gdvCT.Columns("IDNhom").Caption = "Nhóm"
            gdvCT.Columns("IDNhom").ColumnEdit = rcbNhomChiTieu
            gdvCT.Columns("IDNhom").OwnerBand = bandMain


            _exit = True

            For i As Integer = 0 To gdvCT.DataRowCount - 1
                Dim _Tong As Double = 0
                Dim _TongHT As Double = 0
                For j As Integer = 0 To gdvCT.Columns.Count - 1

                    Dim r() As DataRow = ds.Tables(1).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.Columns(j).FieldName.ToString & "'")
                    If r.Length > 0 Then
                        gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, r(0)("ChiTieu"))
                        _Tong += r(0)("ChiTieu")
                        If r(0)("KetQua") > 0 Then
                            gdvCT.SetRowCellValue(i, gdvCT.Columns(j + 1).FieldName, r(0)("KetQua"))
                            gdvCT.UpdateCurrentRow()
                            _TongHT += r(0)("KetQua")
                        End If
                    End If

                    If Not IsDBNull(gdvCT.GetRowCellValue(i, "MaCT")) Then
                        If IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName)) Then
                            Dim r3() As DataRow = dsKQ.Select("MaCT=" & gdvCT.GetRowCellValue(i, "MaCT") & " AND Thang='" & gdvCT.Columns(j).FieldName & "'")
                            If r3.Length > 0 Then
                                gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, r3(0)("SoLuong"))
                                _TongHT += r3(0)("SoLuong")
                            Else
                                If gdvCT.Columns(j).ColumnEdit Is tbPT Then
                                    Try
                                        If gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) > 0 Then
                                            gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, 0)
                                        End If
                                    Catch ex As Exception

                                    End Try
                                End If
                            End If
                        End If
                    End If

                    If gdvCT.Columns(j).FieldName.ToString.Length > 2 Then
                        If gdvCT.Columns(j).FieldName.ToString.Substring(gdvCT.Columns(j).FieldName.ToString.Length - 2, 2) = "TT" Then
                            If Not IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 1).FieldName)) Then
                                If Convert.ToInt32(gdvCT.Columns(j).FieldName.ToString.Substring(0, 2)) < Today.Month Then
                                    If gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) = 0 Then
                                        gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, 100)
                                    Else
                                        gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, Math.Round((gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 1).FieldName) / gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName)) * 100, 2))
                                    End If
                                Else
                                    If Not IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName)) Then
                                        Dim _valC As Double = gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 1).FieldName) / Today.Day
                                        If gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) = 0 Then
                                            gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, 100)
                                        Else
                                            Dim _val As Double = gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) / DateTime.DaysInMonth(Today.Year, Today.Month)

                                            gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, Math.Round((_valC / _val) * 100, 2))
                                        End If

                                    End If

                                End If
                            End If

                        End If
                    End If

                Next
                gdvCT.SetRowCellValue(i, "Tong", _Tong)
                gdvCT.SetRowCellValue(i, "HoanThanh", _TongHT)
            Next
            gdvCT.EndUpdate()
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
            _exit = False


        Else

            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSChiTieuTatCaNhanVien()

        Dim sql As String = ""
        sql &= " SELECT * FROM ( SELECT DISTINCT tblChiTieu.IDChiTieu,tblChiTieu.Thang,DSCHITIEU.NoiDung,DSCHITIEU.Diem AS MaCT,(SELECT NoiDung FROM tblTuDien WHERE tblTuDien.ID=DSCHITIEU.IDP) AS NhomCT," & vbCrLf
        For i As Integer = 0 To tbNV.Rows.Count - 1
            sql &= " Convert(float,0) AS [" & tbNV.Rows(i)("ID").ToString & "]," & vbCrLf
            sql &= " Convert(float,NULL) AS [" & tbNV.Rows(i)("ID").ToString & "_HT]," & vbCrLf
            sql &= " Convert(float,NULL) AS [" & tbNV.Rows(i)("ID").ToString & "_TT]," & vbCrLf
        Next

        sql &= " Convert(float,0) AS Tong, Convert(float,0) AS ChiTieuPhong, Convert(float,NULL) AS ChiTieuPhong_HT, Convert(float,NULL) AS ChiTieuPhong_TT,Convert(bit,0)Modify" & vbCrLf
        sql &= " FROM tblChiTieu" & vbCrLf
        sql &= " INNER JOIN tblTuDien As DSCHITIEU ON DSCHITIEU.ID=tblChiTieu.IDChiTieu" & vbCrLf
        sql &= " WHERE right(tblChiTieu.Thang,4)=@Nam AND tblChiTieu.Loai=1 AND tblChiTieu.IDThucHien IN (SELECT ID FROM NHANSU WHERE Noictac=74 AND iddepatment=@IDPhong) )tb" & vbCrLf
        sql &= " ORDER BY NhomCT,MaCT" & vbCrLf

        sql &= " SELECT IDChiTieu,Thang,ChiTieu,KetQua,IDThucHien FROM tblChiTieu WHERE right(Thang,4)=@Nam AND IDThucHien IN (SELECT ID FROM NHANSU WHERE Noictac=74 AND iddepatment=@IDPhong) AND tblChiTieu.Loai=1 " & vbCrLf
        sql &= " SELECT IDChiTieu,Thang,ChiTieu,IDThucHien FROM tblChiTieu WHERE right(Thang,4)=@Nam AND IDThucHien =@IDPhong AND tblChiTieu.Loai=0 " & vbCrLf

        AddParameterWhere("@IDPhong", cbPhong.EditValue)
        AddParameterWhere("@Nam", tbNam.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        Dim objGetData As New DbWebBaoAn
        objGetData.AddParameterWhere("@Nam", tbNam.EditValue)
        objGetData.AddParameterWhere("@IDNV", -1)
        Dim dsKQ1 As DataTable = objGetData.ExecutePrcDataTable("prc_get_BaoCaoChiTieu2")
        Dim dsKQ As New DataTable
        If dsKQ1 Is Nothing Then
            ShowBaoLoi(objGetData.LoiNgoaiLe)
        Else
            dsKQ.Merge(dsKQ1)
        End If

        AddParameterWhere("@Nam", tbNam.EditValue)
        AddParameterWhere("@Phong", cbPhong.EditValue)
        ' If cbNhanVien.EditValue Is Nothing Then
        AddParameterWhere("@IDNV", -1)
        ' Else
        ' AddParameterWhere("@IDNV", cbNhanVien.EditValue)
        ' End If

        Dim dsKQ2 As DataTable = ExecutePrcDataTable("prc_get_BaoCaoChiTieu")

        If dsKQ2 Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            dsKQ.Merge(dsKQ2)
        End If

        If Not ds Is Nothing Then
            gdvCT.Columns.Clear()
            gdvCT.Bands.Clear()
            gdv.DataSource = ds.Tables(0)
            gdvCT.BeginUpdate()
            Dim bandMain = createBand("MainB", "", False)
            bandMain.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("IDChiTieu").Visible = False
            gdvCT.Columns("IDChiTieu").OwnerBand = bandMain
            gdvCT.Columns("Modify").Visible = False
            gdvCT.Columns("Modify").OwnerBand = bandMain
            gdvCT.Columns("MaCT").Visible = False
            gdvCT.Columns("MaCT").OwnerBand = bandMain
            gdvCT.Columns("Thang").Caption = "Tháng"
            gdvCT.Columns("Thang").GroupIndex = 0
            gdvCT.Columns("Thang").OwnerBand = bandMain

            gdvCT.Columns("NhomCT").Caption = "Nhóm"
            gdvCT.Columns("NhomCT").GroupIndex = 1
            gdvCT.Columns("NhomCT").OwnerBand = bandMain

            gdvCT.Columns("NoiDung").OptionsColumn.ReadOnly = True
            gdvCT.Columns("NoiDung").VisibleIndex = 0
            gdvCT.Columns("NoiDung").Width = 200
            gdvCT.Columns("NoiDung").Caption = "Chỉ tiêu"
            gdvCT.Columns("NoiDung").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("NoiDung").OwnerBand = bandMain

            gdvCT.Columns("ChiTieuPhong").Caption = "Chỉ tiêu phòng"
            gdvCT.Columns("ChiTieuPhong").OptionsColumn.ReadOnly = True
            gdvCT.Columns("ChiTieuPhong").Width = 50
            gdvCT.Columns("ChiTieuPhong").ColumnEdit = tbN0
            gdvCT.Columns("ChiTieuPhong").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("ChiTieuPhong").OwnerBand = bandMain

            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
                gdvCT.Columns("ChiTieuPhong").VisibleIndex = -1
                gdvCT.Columns("ChiTieuPhong").Visible = False
            Else
                gdvCT.Columns("ChiTieuPhong").VisibleIndex = 1
            End If

            gdvCT.Columns("Tong").Caption = "Tổng"
            gdvCT.Columns("Tong").OptionsColumn.ReadOnly = True
            gdvCT.Columns("Tong").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("Tong").VisibleIndex = 2
            gdvCT.Columns("Tong").Width = 50
            gdvCT.Columns("Tong").ColumnEdit = tbN0
            gdvCT.Columns("Tong").OwnerBand = bandMain

            gdvCT.Columns("ChiTieuPhong_HT").Caption = "Hoàn thành"
            gdvCT.Columns("ChiTieuPhong_HT").OptionsColumn.ReadOnly = True
            gdvCT.Columns("ChiTieuPhong_HT").VisibleIndex = 3
            gdvCT.Columns("ChiTieuPhong_HT").Width = 50
            gdvCT.Columns("ChiTieuPhong_HT").ColumnEdit = tbN0
            gdvCT.Columns("ChiTieuPhong_HT").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("ChiTieuPhong_HT").OwnerBand = bandMain

            gdvCT.Columns("ChiTieuPhong_TT").Caption = "Tiến độ"
            gdvCT.Columns("ChiTieuPhong_TT").OptionsColumn.ReadOnly = True
            gdvCT.Columns("ChiTieuPhong_TT").VisibleIndex = 4
            gdvCT.Columns("ChiTieuPhong_TT").Width = 50
            gdvCT.Columns("ChiTieuPhong_TT").ColumnEdit = tbN0
            gdvCT.Columns("ChiTieuPhong_TT").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("ChiTieuPhong_TT").OwnerBand = bandMain

            For i As Integer = 0 To gdvCT.Columns.Count - 1
                If IsNumeric(gdvCT.Columns(i).FieldName) Then
                    Dim r() As DataRow = tbNV.Select("ID=" & gdvCT.Columns(i).FieldName)
                    If r.Length > 0 Then
                        Dim b = createBand(gdvCT.Columns(i).FieldName, r(0)("Ten"))
                        gdvCT.Columns(i).Caption = "Chỉ tiêu"
                        gdvCT.Columns(i).OwnerBand = b
                        gdvCT.Columns(i).VisibleIndex = 1
                        gdvCT.Columns(i).Width = 50
                        gdvCT.Columns(i).ColumnEdit = tbN0

                        gdvCT.Columns(i + 1).Caption = "Hoàn thành"
                        gdvCT.Columns(i + 1).OwnerBand = b
                        gdvCT.Columns(i + 1).VisibleIndex = 2
                        gdvCT.Columns(i + 1).Width = 50
                        gdvCT.Columns(i + 1).ColumnEdit = tbN0

                        gdvCT.Columns(i + 2).Caption = "Tiến độ"
                        gdvCT.Columns(i + 2).OwnerBand = b
                        gdvCT.Columns(i + 2).VisibleIndex = 3
                        gdvCT.Columns(i + 2).Width = 60
                        gdvCT.Columns(i + 2).ColumnEdit = tbPT
                        gdvCT.Columns(i + 2).Tag = "c"
                    End If
                    gdvCT.Columns(i).ColumnEdit = tbN0
                End If

            Next

            _exit = True

            Dim _DayOfMonth As Integer = DateTime.DaysInMonth(Today.Year, Today.Month)

            For i As Integer = 0 To gdvCT.DataRowCount - 1
                Dim _Tong As Double = 0
                Dim _TongHT As Double = 0
                Dim _Modify As Boolean = False

                If Not IsDBNull(gdvCT.GetRowCellValue(i, "MaCT")) Then
                    Select Case CType(gdvCT.GetRowCellValue(i, "MaCT"), Integer)
                        Case 12, 13, 14, 15
                            Dim r0() As DataRow = dsKQ.Select("MaCT=" & gdvCT.GetRowCellValue(i, "MaCT") & " AND Thang='" & gdvCT.GetRowCellValue(i, "Thang") & "'" & " AND NguoiDang='0_HT'")
                            If r0.Length > 0 Then
                                gdvCT.SetRowCellValue(i, "ChiTieuPhong_HT", r0(0)("SoLuong"))
                                'Continue For
                            End If
                    End Select
                End If


                For j As Integer = 0 To gdvCT.Columns.Count - 1
                    If IsNumeric(gdvCT.Columns(j).FieldName) Then
                        Dim r() As DataRow = ds.Tables(1).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.GetRowCellValue(i, "Thang") & "'" & " AND IDThucHien=" & gdvCT.Columns(j).FieldName)
                        If r.Length > 0 Then
                            gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, r(0)("ChiTieu"))
                            _Tong += r(0)("ChiTieu")
                            If r(0)("KetQua") <> 0 Then
                                gdvCT.SetRowCellValue(i, gdvCT.Columns(j + 1).FieldName, r(0)("KetQua"))
                                _TongHT += r(0)("KetQua")
                            End If
                        End If
                    Else
                        If IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName)) Then
                            If Not IsDBNull(gdvCT.GetRowCellValue(i, "MaCT")) Then
                                Dim r3() As DataRow = dsKQ.Select("MaCT=" & gdvCT.GetRowCellValue(i, "MaCT") & " AND Thang='" & gdvCT.GetRowCellValue(i, "Thang") & "'" & " AND NguoiDang='" & gdvCT.Columns(j).FieldName & "'")
                                If r3.Length > 0 Then
                                    gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, r3(0)("SoLuong"))
                                    _TongHT += r3(0)("SoLuong")
                                    _Modify = True
                                Else
                                    If gdvCT.Columns(j).ColumnEdit Is tbPT Then
                                        Try
                                            If gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) > 0 Then
                                                gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, 0)
                                            End If
                                        Catch ex As Exception

                                        End Try
                                    End If


                                End If
                            End If

                        End If

                    End If

                    If gdvCT.Columns(j).FieldName.ToString.Length > 2 Then
                        If gdvCT.Columns(j).FieldName.ToString.Substring(gdvCT.Columns(j).FieldName.ToString.Length - 2, 2) = "TT" Then
                            If Not IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 1).FieldName)) Then
                                If tbNam.EditValue < Today.Year Then
                                    If gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) = 0 Then
                                        gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, 100)
                                    Else
                                        gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, Math.Round((gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 1).FieldName) / gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName)) * 100, 2))
                                    End If

                                Else
                                    If Convert.ToInt32(gdvCT.GetRowCellValue(i, "Thang").ToString.Substring(0, 2)) < Today.Month Then
                                        If gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) = 0 Then
                                            gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, 100)
                                        Else
                                            gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, Math.Round((gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 1).FieldName) / gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName)) * 100, 2))
                                        End If
                                    Else
                                        If Not IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName)) Then
                                            Dim _valC As Double = gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 1).FieldName) / Today.Day
                                            If gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) = 0 Then
                                                gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, 100)
                                            Else
                                                Dim _val As Double = gdvCT.GetRowCellValue(i, gdvCT.Columns(j - 2).FieldName) / _DayOfMonth
                                                gdvCT.SetRowCellValue(i, gdvCT.Columns(j).FieldName, Math.Round((_valC / _val) * 100, 2))
                                            End If
                                        End If
                                    End If
                                End If

                            End If
                        End If
                    End If
                Next



                If _Modify = True Then
                    gdvCT.SetRowCellValue(i, "Modify", True)
                End If
                gdvCT.SetRowCellValue(i, "Tong", _Tong)
                gdvCT.SetRowCellValue(i, "ChiTieuPhong_HT", _TongHT)

                Dim r1() As DataRow = ds.Tables(2).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.GetRowCellValue(i, "Thang") & "'")
                If r1.Length > 0 Then
                    gdvCT.SetRowCellValue(i, "ChiTieuPhong", r1(0)("ChiTieu"))
                End If

                If Not IsDBNull(gdvCT.GetRowCellValue(i, "MaCT")) Then
                    Select Case CType(gdvCT.GetRowCellValue(i, "MaCT"), Integer)
                        Case 12, 13, 14, 15, 16, 17, 18
                            Dim r0() As DataRow = dsKQ.Select("MaCT=" & gdvCT.GetRowCellValue(i, "MaCT") & " AND Thang='" & gdvCT.GetRowCellValue(i, "Thang") & "'" & " AND NguoiDang='0_HT'")
                            If r0.Length > 0 Then
                                gdvCT.SetRowCellValue(i, "ChiTieuPhong_HT", r0(0)("SoLuong"))
                                'Continue For
                            End If
                    End Select
                End If

            Next

            'For i As Integer = 0 To gdvCT.DataRowCount - 1
            '    Dim r() As DataRow = ds.Tables(2).Select("IDChiTieu=" & gdvCT.GetRowCellValue(i, "IDChiTieu") & " AND Thang='" & gdvCT.GetRowCellValue(i, "Thang") & "'")
            '    If r.Length > 0 Then
            '        gdvCT.SetRowCellValue(i, "ChiTieuPhong", r(0)("ChiTieu"))
            '    End If
            'Next

            gdvCT.EndUpdate()
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
            _exit = False
            gdvCT.CollapseAllGroups()

            Dim Handle As Integer = -1
            Do Until gdvCT.GetRow(Handle) Is Nothing
                If gdvCT.GetGroupRowValue(Handle) = Today.ToString("MM/yyyy") Then
                    gdvCT.ExpandGroupRow(Handle, True)
                End If
                Handle -= 1
            Loop

        Else

            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        On Error Resume Next
        If _exit Then
            Exit Sub
        End If
        If e.Column.FieldName = "NoiDung" Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If cbLoaiChiTieu.EditValue = "Nhân viên" Then
            If cbNhanVien.EditValue Is Nothing Then
                If IsNumeric(e.Column.FieldName) Then
                    AddParameter("@ChiTieu", e.Value)
                    AddParameterWhere("@IDChiTieu", gdvCT.GetRowCellValue(e.RowHandle, "IDChiTieu"))
                    AddParameterWhere("@Thang", gdvCT.GetRowCellValue(e.RowHandle, "Thang"))
                    AddParameterWhere("@IDThucHien", e.Column.FieldName)

                    If doUpdate("tblChiTieu", "IDChiTieu=@IDChiTieu AND Thang=@Thang AND IDThucHien=@IDThucHien") Is Nothing Then
                        ShowCanhBao(LoiNgoaiLe)
                    Else
                        Dim _Tong As Double = 0
                        For i As Integer = 1 To gdvCT.Columns.Count - 1
                            If IsNumeric(gdvCT.Columns(i).FieldName) Then
                                ' <> "Tong" And gdvCT.Columns(i).FieldName <> "IDNhom" And gdvCT.Columns(i).FieldName <> "IDChiTieu" And gdvCT.Columns(i).FieldName <> "NoiDung" And gdvCT.Columns(i).FieldName <> "Ma" Then
                                _Tong += gdvCT.GetRowCellValue(e.RowHandle, gdvCT.Columns(i).FieldName)
                            End If
                        Next
                        _exit = True
                        gdvCT.SetRowCellValue(e.RowHandle, "Tong", _Tong)
                        _exit = False
                    End If
                Else
                    _exit = True
                    gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
                    gdvCT.UpdateCurrentRow()
                    _exit = False
                End If
            End If


        Else
            If Not cbPhong.EditValue Is Nothing Then
                'If IsNumeric(e.Column.FieldName) Then
                AddParameter("@ChiTieu", e.Value)
                AddParameterWhere("@IDChiTieu", gdvCT.GetRowCellValue(e.RowHandle, "IDChiTieu"))
                AddParameterWhere("@Thang", e.Column.FieldName)
                If cbLoaiChiTieu.EditValue = "Phòng" Then
                    AddParameterWhere("@IDThucHien", cbPhong.EditValue)
                Else
                    AddParameterWhere("@IDThucHien", cbNhanVien.EditValue)
                End If

                If doUpdate("tblChiTieu", "IDChiTieu=@IDChiTieu AND Thang=@Thang AND IDThucHien=@IDThucHien") Is Nothing Then
                    ShowCanhBao(LoiNgoaiLe)
                Else
                    Dim _Tong As Double = 0
                    For i As Integer = 1 To gdvCT.Columns.Count - 1
                        If gdvCT.Columns(i).FieldName <> "Tong" And gdvCT.Columns(i).FieldName <> "IDNhom" And gdvCT.Columns(i).FieldName <> "IDChiTieu" And gdvCT.Columns(i).FieldName <> "NoiDung" And gdvCT.Columns(i).FieldName <> "Ma" Then
                            _Tong += gdvCT.GetRowCellValue(e.RowHandle, gdvCT.Columns(i).FieldName)
                        End If
                    Next
                    _exit = True
                    gdvCT.SetRowCellValue(e.RowHandle, "Tong", _Tong)
                    _exit = False
                End If
                'Else
                '    _exit = True
                '    gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
                '    gdvCT.UpdateCurrentRow()
                '    _exit = False
                'End If

            Else

            End If
        End If

    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Chi tieu.xls"
        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCapNhatCT.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        'fCNChiTieu = New frmCNChiTieu
        'fCNChiTieu.Tag = Me.Parent.Tag
        'fCNChiTieu.ShowDialog()
        tabCT.SelectedTabPage = tabCapNhatChiTieu
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick

        If cbPhong.EditValue Is Nothing And cbLoaiChiTieu.EditValue = "Phòng" Then
            gdv.DataSource = Nothing
            Exit Sub
        End If
        Try
            ShowWaiting("Đang tải dữ liệu...")
            If cbLoaiChiTieu.EditValue = "Phòng" Then
                gdvCT.ColumnPanelRowHeight = -1
                LoadDSChiTieuPhong()
            Else
                If cbPhong.EditValue Is Nothing Then

                Else
                    If cbNhanVien.EditValue Is Nothing Then
                        gdvCT.ColumnPanelRowHeight = 50
                        loadDSChiTieuTatCaNhanVien()
                    Else
                        gdvCT.ColumnPanelRowHeight = -1
                        loadDSChiTieuMotNhanVien()
                    End If

                End If
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        Finally
            CloseWaiting()
        End Try

    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        If cbLoaiChiTieu.EditValue = "Phòng" Then
            If Not isFormLoading Then btXem.PerformClick()
        Else
            LoadDsNhanVien()
        End If
    End Sub

    Private Sub cbLoaiChiTieu_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbLoaiChiTieu.EditValueChanged
        If cbLoaiChiTieu.EditValue = "Phòng" Then
            cbNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            rcbPhong.Buttons(1).Visible = True
            gdv.DataSource = Nothing
        Else
            cbNhanVien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            rcbPhong.Buttons(1).Visible = False
            If cbPhong.EditValue Is Nothing Then
                cbPhong.EditValue = 1
            End If
            LoadDsNhanVien()
            gdv.DataSource = Nothing
        End If
    End Sub

    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub btApDungChiTieuMoi_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            KiemTraChiTieuMoi()
        End If
    End Sub

    Private Sub cbNhanVien_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbNhanVien.EditValueChanged
        If Not isFormLoading Then btXem.PerformClick()
    End Sub

    Private Sub tabCT_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabCT.SelectedPageChanged
        If e.Page Is tabCapNhatChiTieu Then
            LoadDSChiTieuApDungChoPhong()
        End If
    End Sub

    Public Sub LoadDSChiTieuApDungChoPhong()
        Dim sql As String = ""
        AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
        sql &= " SELECT (SELECT NoiDung FROM tblTuDien WHERE tblTuDien.ID=IDNhom)NhomCT,IDChiTieu,NoiDung,"
        For i As Integer = 0 To tbPhong.Rows.Count - 1
            sql &= " CAST([" & tbPhong.Rows(i)("ID").ToString & "] AS bit)[" & tbPhong.Rows(i)("ID").ToString & "],"
        Next
        sql = sql.Substring(0, sql.Length - 1)

        sql &= " FROM"
        sql &= " ("
        sql &= " SELECT tblTuDien.ID AS IDChiTieu,tblTuDien.IDP AS IDNhom,tblTuDien.NoiDung,DEPATMENT.ID AS IDPhong,"
        sql &= " 	(CASE WHEN tblPhanBoChiTieu.ID IS NULL THEN 0 ELSE 1 END ) GiaTri"
        sql &= " FROM tblTuDien"
        sql &= " CROSS JOIN DEPATMENT "
        sql &= " LEFT JOIN tblPhanBoChiTieu ON tblTuDien.ID=tblPhanBoChiTieu.IDChiTieu AND tblPhanBoChiTieu.IDPhong=DEPATMENT.ID"
        sql &= " WHERE tblTuDien.Loai=@ChiTieu"
        sql &= " )tbl"
        sql &= "  PIVOT"
        sql &= "  ("
        sql &= "   SUM( GiaTri)"
        sql &= "  FOR IDPhong IN"
        sql &= "  ( "
        For i As Integer = 0 To tbPhong.Rows.Count - 1
            sql &= " [" & tbPhong.Rows(i)("ID").ToString & "],"
        Next
        sql = sql.Substring(0, sql.Length - 1)

        sql &= "  )) AS pvt"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvChiTieu.DataSource = tb
            gdvChiTieuCT.ColumnPanelRowHeight = 60
            gdvChiTieuCT.Columns("IDChiTieu").Visible = False
            gdvChiTieuCT.Columns("NhomCT").GroupIndex = 0
            gdvChiTieuCT.Columns("NhomCT").Caption = "Nhóm"
            gdvChiTieuCT.Columns("NoiDung").OptionsColumn.ReadOnly = True
            gdvChiTieuCT.Columns("NoiDung").VisibleIndex = 0
            gdvChiTieuCT.Columns("NoiDung").Width = 250
            gdvChiTieuCT.Columns("NoiDung").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            gdvChiTieuCT.Columns("NoiDung").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvChiTieuCT.Columns("NoiDung").Caption = "Chỉ tiêu"

            Dim tbCv As DataTable = rcbPhong.DataSource
            If Not tbCv Is Nothing Then
                For i As Integer = 0 To tbCv.Rows.Count - 1
                    For j As Integer = 3 To gdvChiTieuCT.Columns.Count - 1
                        If gdvChiTieuCT.Columns(j).FieldName = tbCv.Rows(i)("ID") Then
                            gdvChiTieuCT.Columns(j).Caption = tbCv.Rows(i)("Ten").ToString
                            gdvChiTieuCT.Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                            gdvChiTieuCT.Columns(j).VisibleIndex = j + 3
                        End If
                    Next
                Next
            Else
                ShowCanhBao(LoiNgoaiLe)
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvChiTieuCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvChiTieuCT.CellValueChanged
        Select Case e.Column.FieldName
            Case "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"
                If CType(e.Value, Boolean) = True Then
                    AddParameter("@IDPhong", e.Column.FieldName)
                    AddParameter("@IDChiTieu", gdvChiTieuCT.GetRowCellValue(e.RowHandle, "IDChiTieu"))
                    If doInsert("tblPhanBoChiTieu") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If

                Else
                    AddParameterWhere("@IDPhong", e.Column.FieldName)
                    AddParameterWhere("@IDChiTieu", gdvChiTieuCT.GetRowCellValue(e.RowHandle, "IDChiTieu"))
                    If doDelete("tblPhanBoChiTieu", "IDPhong=@IDPhong AND IDChiTieu=@IDChiTieu") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                End If
        End Select
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        If ShowCauHoi("Bạn có muốn cập nhật chỉ tiêu năm " & tbNam.EditValue.ToString & " cho các phòng ban và nhân viên hay không ?") Then
            ShowWaiting("Cập nhật chỉ tiêu cho các phòng ...")
            Dim sql As String = ""
            sql &= " DECLARE @tbDMChiTieu table"
            sql &= " ("
            sql &= " 	IDChiTieu int,"
            sql &= " 	IDNhom int,"
            sql &= " 	NoiDung nvarchar(500),"
            sql &= " 	IDThucHien int,"
            sql &= " 	Thang nvarchar(7),"
            sql &= "    ChiTieu float"
            sql &= " )"

            sql &= " DECLARE @tbThang table"
            sql &= " ("
            sql &= "    Thang nvarchar(7)"
            sql &= " )"

            For i As Integer = 1 To 12
                sql &= " INSERT INTO @tbThang"
                If i < 10 Then
                    sql &= " VALUES('0" & i.ToString & "/" & tbNam.EditValue.ToString & "')"
                Else
                    sql &= " VALUES('" & i.ToString & "/" & tbNam.EditValue.ToString & "')"
                End If
            Next

            sql &= " INSERT INTO @tbDMChiTieu"
            sql &= " SELECT tblTuDien.ID As IDChiTieu,tblTuDien.IDP AS IDNhom,tblTuDien.NoiDung, "
            sql &= " 	tbThucHien.ID AS IDThucHien,[@tbThang].Thang,Convert(float,0)ChiTieu"
            sql &= " FROM tblTuDien "
            sql &= " CROSS JOIN (SELECT ID,Ten FROM DEPATMENT)tbThucHien"
            sql &= " CROSS JOIN @tbThang"
            sql &= " WHERE tblTuDien.Loai=@ChiTieu"

            sql &= " INSERT INTO tblChiTieu (IDChiTieu,IDThucHien,Thang,Loai)"
            sql &= " SELECT [@tbDMChiTieu].IDChiTieu,[@tbDMChiTieu].IDThucHien,[@tbDMChiTieu].Thang,@Loai As Loai FROM @tbDMChiTieu"
            sql &= " LEFT JOIN tblChiTieu ON tblChiTieu.Loai=@Loai AND tblChiTieu.Thang =[@tbDMChiTieu].Thang  AND tblChiTieu.IDChiTieu = [@tbDMChiTieu].IDChiTieu AND tblChiTieu.IdThucHien = [@tbDMChiTieu].IDThucHien"
            sql &= " INNER JOIN tblPhanBoChiTieu ON [@tbDMChiTieu].IDChiTieu = tblPhanBoChiTieu.IDChiTieu AND [@tbDMChiTieu].IDThucHien=tblPhanBoChiTieu.IDPhong"
            sql &= " WHERE tblChiTieu.ID IS NULL"

            AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
            AddParameterWhere("@Nam", tbNam.EditValue)
            AddParameterWhere("@Loai", 0)

            If ExecuteSQLNonQuery(sql) Is Nothing Then
                CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật chỉ tiêu cho các phòng ban!")
                CloseWaiting()
            End If
            Threading.Thread.Sleep(1000)

            For i As Integer = 0 To tbPhong.Rows.Count - 1
                ShowWaiting("CN cho nv phòng " & tbPhong.Rows(i)("Ten").ToString)
                Threading.Thread.Sleep(1000)
                sql = ""
                sql &= " DECLARE @tbDMChiTieu table"
                sql &= " ("
                sql &= " 	IDChiTieu int,"
                sql &= " 	IDNhom int,"
                sql &= " 	NoiDung nvarchar(500),"
                sql &= " 	IDThucHien int,"
                sql &= " 	Thang nvarchar(7),"
                sql &= "    ChiTieu float,"
                sql &= "    IDPhong float"
                sql &= " )"

                sql &= " DECLARE @tbThang table"
                sql &= " ("
                sql &= "    Thang nvarchar(7)"
                sql &= " )"

                For j As Integer = 1 To 12
                    sql &= " INSERT INTO @tbThang"
                    If j < 10 Then
                        sql &= " VALUES('0" & j.ToString & "/" & tbNam.EditValue.ToString & "')"
                    Else
                        sql &= " VALUES('" & j.ToString & "/" & tbNam.EditValue.ToString & "')"
                    End If
                Next

                sql &= " INSERT INTO @tbDMChiTieu"
                sql &= " SELECT tblTuDien.ID As IDChiTieu,tblTuDien.IDP AS IDNhom,tblTuDien.NoiDung, "
                sql &= " 	tbThucHien.ID AS IDThucHien,[@tbThang].Thang,Convert(float,0)ChiTieu,IDDepatment"
                sql &= " FROM tblTuDien "
                sql &= " CROSS JOIN (SELECT ID,Ten,IDDepatment FROM NHANSU WHERE Noictac=74 AND trangThai=1 AND IDDepatment=@IDPB)tbThucHien"
                sql &= " CROSS JOIN @tbThang"
                sql &= " WHERE tblTuDien.Loai=@ChiTieu"

                sql &= " INSERT INTO tblChiTieu (IDChiTieu,IDThucHien,Thang,Loai)"
                sql &= " SELECT [@tbDMChiTieu].IDChiTieu,[@tbDMChiTieu].IDThucHien,[@tbDMChiTieu].Thang,@Loai As Loai FROM @tbDMChiTieu"
                sql &= " LEFT JOIN tblChiTieu ON tblChiTieu.Loai=@Loai AND tblChiTieu.Thang =[@tbDMChiTieu].Thang  AND tblChiTieu.IDChiTieu = [@tbDMChiTieu].IDChiTieu AND tblChiTieu.IdThucHien = [@tbDMChiTieu].IDThucHien"
                sql &= " INNER JOIN tblPhanBoChiTieu ON [@tbDMChiTieu].IDChiTieu = tblPhanBoChiTieu.IDChiTieu AND [@tbDMChiTieu].IDPhong=tblPhanBoChiTieu.IDPhong"
                sql &= " WHERE tblChiTieu.ID IS NULL"

                AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
                AddParameterWhere("@Nam", tbNam.EditValue)
                AddParameterWhere("@Loai", 1)
                AddParameterWhere("@IDPB", tbPhong.Rows(i)("ID"))

                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    CloseWaiting()
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã cập nhật chỉ tiêu cho nv phòng " & tbPhong.Rows(i)("Ten") & "!")
                    CloseWaiting()
                End If
            Next

            'ShowWaiting("Cập nhật chỉ tiêu cho nhân viên ...")
            'Threading.Thread.Sleep(1000)
            'sql = ""
            'sql &= " DECLARE @tbDMChiTieu table"
            'sql &= " ("
            'sql &= " 	IDChiTieu int,"
            'sql &= " 	IDNhom int,"
            'sql &= " 	NoiDung nvarchar(500),"
            'sql &= " 	IDThucHien int,"
            'sql &= " 	Thang nvarchar(7),"
            'sql &= "    ChiTieu float,"
            'sql &= "    IDPhong float"
            'sql &= " )"

            'sql &= " DECLARE @tbThang table"
            'sql &= " ("
            'sql &= "    Thang nvarchar(7)"
            'sql &= " )"

            'For i As Integer = 1 To 12
            '    sql &= " INSERT INTO @tbThang"
            '    If i < 10 Then
            '        sql &= " VALUES('0" & i.ToString & "/" & tbNam.EditValue.ToString & "')"
            '    Else
            '        sql &= " VALUES('" & i.ToString & "/" & tbNam.EditValue.ToString & "')"
            '    End If
            'Next

            'sql &= " INSERT INTO @tbDMChiTieu"
            'sql &= " SELECT tblTuDien.ID As IDChiTieu,tblTuDien.IDP AS IDNhom,tblTuDien.NoiDung, "
            'sql &= " 	tbThucHien.ID AS IDThucHien,[@tbThang].Thang,Convert(float,0)ChiTieu,IDDepatment"
            'sql &= " FROM tblTuDien "
            'sql &= " CROSS JOIN (SELECT ID,Ten,IDDepatment FROM NHANSU WHERE Noictac=74 AND trangThai=1)tbThucHien"
            'sql &= " CROSS JOIN @tbThang"
            'sql &= " WHERE tblTuDien.Loai=@ChiTieu"

            'sql &= " INSERT INTO tblChiTieu (IDChiTieu,IDThucHien,Thang,Loai)"
            'sql &= " SELECT [@tbDMChiTieu].IDChiTieu,[@tbDMChiTieu].IDThucHien,[@tbDMChiTieu].Thang,@Loai As Loai FROM @tbDMChiTieu"
            'sql &= " LEFT JOIN tblChiTieu ON tblChiTieu.Loai=@Loai AND tblChiTieu.Thang =[@tbDMChiTieu].Thang  AND tblChiTieu.IDChiTieu = [@tbDMChiTieu].IDChiTieu AND tblChiTieu.IdThucHien = [@tbDMChiTieu].IDThucHien"
            'sql &= " INNER JOIN tblPhanBoChiTieu ON [@tbDMChiTieu].IDChiTieu = tblPhanBoChiTieu.IDChiTieu AND [@tbDMChiTieu].IDPhong=tblPhanBoChiTieu.IDPhong"
            'sql &= " WHERE tblChiTieu.ID IS NULL"

            'AddParameterWhere("@ChiTieu", LoaiTuDien.ChiTieu)
            'AddParameterWhere("@Nam", tbNam.EditValue)
            'AddParameterWhere("@Loai", 1)

            'If ExecuteSQLNonQuery(sql) Is Nothing Then
            '    CloseWaiting()
            '    ShowBaoLoi(LoiNgoaiLe)
            'Else
            '    ShowAlert("Đã cập nhật chỉ tiêu cho nhân viên!")
            '    CloseWaiting()
            'End If

        End If
        tabCT.SelectedTabPage = tabGiaoChiTieu
    End Sub

    Private Sub btThemChiTieu_Click(sender As System.Object, e As System.EventArgs) Handles btThemChiTieu.Click
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        fCNChiTieu = New frmCNChiTieu
        fCNChiTieu.Tag = Me.Parent.Tag
        fCNChiTieu.ShowDialog()
    End Sub

    Private Sub btCapNhatBangGiaoChiTieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCapNhatBangGiaoChiTieu.ItemClick
        KiemTraChiTieuMoi()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim objGetData As New DbWebBaoAn
        objGetData.AddParameterWhere("@Nam", tbNam.EditValue)
        Dim ds As DataSet = objGetData.ExecutePrcDataSet("prc_get_BaoCaoChiTieu")
        If ds Is Nothing Then
            ShowBaoLoi(objGetData.LoiNgoaiLe)
        Else
            ShowCanhBao(ds.Tables.Count)
        End If
    End Sub

    Private Sub btLuuKetQua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuKetQua.ItemClick
        If Not ShowCauHoi("Lưu lại kết quả hoàn thành ?") Then
            Exit Sub
        End If
        Dim _err As Integer = 0
        Dim _errLine As Integer = 0
        Dim _success As Integer = 0
        Dim _saveCurrentMonth As Boolean = False
        If ShowCauHoi("Bạn có muốn lưu lại kết quả hoàn thành của tháng hiện tại không ?") Then
            _saveCurrentMonth = True
        End If

        If cbLoaiChiTieu.EditValue = "Nhân viên" And cbNhanVien.EditValue Is Nothing Then
            For i As Integer = 0 To gdvCT.RowCount - 1
                _errLine = 0
                If gdvCT.GetRowCellValue(i, "Modify") Then
                    For j As Integer = 0 To gdvCT.Columns.Count - 1
                        If IsNumeric(gdvCT.Columns(j).FieldName) Then
                            If Not _saveCurrentMonth Then
                                If CType(gdvCT.GetRowCellValue(i, "Thang").Substring(0, 2), Integer) >= Today.Month Then
                                    Exit For
                                End If
                            Else
                                If CType(gdvCT.GetRowCellValue(i, "Thang").Substring(0, 2), Integer) > Today.Month Then
                                    Exit For
                                End If
                            End If
                            If Not IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j + 1).FieldName.ToString)) Then
                                'If gdvCT.GetRowCellValue(i, gdvCT.Columns(j + 1).FieldName.ToString)
                                AddParameter("@KetQua", gdvCT.GetRowCellValue(i, gdvCT.Columns(j + 1).FieldName.ToString))
                                AddParameterWhere("@IDChiTieu", gdvCT.GetRowCellValue(i, "IDChiTieu"))
                                AddParameterWhere("@Thang", gdvCT.GetRowCellValue(i, "Thang"))
                                AddParameterWhere("@IDThucHien", gdvCT.Columns(j).FieldName)

                                If doUpdate("tblChiTieu", "IDChiTieu=@IDChiTieu AND Thang=@Thang AND IDThucHien=@IDThucHien") Is Nothing Then
                                    _err += 1
                                    _errLine += 1
                                    ShowCanhBao(LoiNgoaiLe)
                                Else
                                    _success += 1
                                    'ShowBaoLoi(LoiNgoaiLe)
                                End If
                            End If

                        End If

                    Next
                End If
                If _errLine = 0 Then
                    _exit = True
                    gdvCT.SetRowCellValue(i, "Modify", False)
                    gdvCT.UpdateCurrentRow()
                    _exit = False
                End If
            Next
            'ElseIf cbLoaiChiTieu.EditValue = "Nhân viên" And Not cbNhanVien.EditValue Is Nothing Then
            '    For i As Integer = 0 To gdvCT.RowCount - 1
            '        _errLine = 0
            '        If gdvCT.GetRowCellValue(i, "Modify") Then
            '            For j As Integer = 0 To gdvCT.Columns.Count - 1
            '                If IsNumeric(gdvCT.Columns(j).FieldName) Then
            '                    If Not _saveCurrentMonth Then
            '                        If CType(gdvCT.GetRowCellValue(i, "Thang").Substring(0, 2), Integer) >= Today.Month Then
            '                            Exit For
            '                        End If
            '                    Else
            '                        If CType(gdvCT.GetRowCellValue(i, "Thang").Substring(0, 2), Integer) > Today.Month Then
            '                            Exit For
            '                        End If
            '                    End If
            '                    If Not IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j + 1).FieldName.ToString)) Then
            '                        'If gdvCT.GetRowCellValue(i, gdvCT.Columns(j + 1).FieldName.ToString)
            '                        AddParameter("@KetQua", gdvCT.GetRowCellValue(i, gdvCT.Columns(j + 1).FieldName.ToString))
            '                        AddParameterWhere("@IDChiTieu", gdvCT.GetRowCellValue(i, "IDChiTieu"))
            '                        AddParameterWhere("@Thang", gdvCT.GetRowCellValue(i, "Thang"))
            '                        AddParameterWhere("@IDThucHien", gdvCT.Columns(j).FieldName)

            '                        If doUpdate("tblChiTieu", "IDChiTieu=@IDChiTieu AND Thang=@Thang AND IDThucHien=@IDThucHien") Is Nothing Then
            '                            _err += 1
            '                            _errLine += 1
            '                            ShowCanhBao(LoiNgoaiLe)
            '                        Else
            '                            _success += 1
            '                            'ShowBaoLoi(LoiNgoaiLe)
            '                        End If
            '                    End If

            '                End If

            '            Next
            '        End If
            '        If _errLine = 0 Then
            '            _exit = True
            '            gdvCT.SetRowCellValue(i, "Modify", False)
            '            gdvCT.UpdateCurrentRow()
            '            _exit = False
            '        End If
            '    Next
        End If

        ShowThongBao("Lỗi: " & _err & "; Thành công: " & _success)

    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        On Error Resume Next

        If e.Column.Tag = "c" Then
            Select Case gdvCT.GetRowCellValue(e.RowHandle, "MaCT")
                Case 17, 18, 19
                    If Not IsDBNull(e.CellValue) Then
                        If e.CellValue < 60 Then
                            e.Appearance.BackColor = Color.Chartreuse
                        ElseIf e.CellValue >= 60 And e.CellValue < 75 Then
                            e.Appearance.BackColor = Color.Yellow
                        Else
                            e.Appearance.BackColor = Color.Red
                        End If
                    End If
                Case Else
                    If Not IsDBNull(e.CellValue) Then
                        If e.CellValue < 60 Then
                            e.Appearance.BackColor = Color.Red
                        ElseIf e.CellValue >= 60 And e.CellValue < 75 Then
                            e.Appearance.BackColor = Color.Yellow
                        Else
                            e.Appearance.BackColor = Color.Chartreuse
                        End If
                    End If
            End Select


        End If
    End Sub

End Class