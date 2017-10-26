Imports BACSOFT.Db.SqlHelper

Public Class frmThongTinHaiQuan2
    Public PhieuDatHang As Object

    Private Sub frmThongTinHaiQuan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDataCb()
        loadThongTinChinh(PhieuDatHang)
        LoadChiPhi()
    End Sub

    Public Sub LoadDataCb()
        Dim sql As String = ""
        sql &= " SELECT ID,ttcMa,Ten FROM KHACHHANG ORDER BY ttcMa"
        sql &= " SELECT ID,Ten,TyGia FROM tblTienTe ORDER BY ID"
        sql &= " SELECT ID,Ten FROM MucDichThuChi WHERE ID IN(203,205,210,228) "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvCuaKhauHQ.Properties.DataSource = ds.Tables(0)
            cbTienTeHH.Properties.DataSource = ds.Tables(1)
            cbTienTeHH.EditValue = ds.Tables(1).Rows(1)(0)

            rcbKH.DataSource = ds.Tables(0)
            rcbTienTe.DataSource = ds.Tables(1)
            rcbMucDich.DataSource = ds.Tables(2)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadChiPhi()
        AddParameterWhere("@SP", PhieuDatHang)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Row_number() over(order by ID) as STT, *,convert(bit,0)Modify FROM CHIPHI WHERE Loai=0 AND PhieuCGDH=@SP")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadThongTinChinh(ByVal SoPhieuDH As Object)
        Dim sql As String = ""
        sql &= " SELECT SoPhieuNhapKhau,SoPhieuDatHang,SoToKhai,CuaKhauHQ,SoKienHang,KhoiLuongHang,TheTichHang,TyGiaHQHH,TyGiaTTVC,TyGiaHQVC,"
        sql &= " 	ISNULL(TienTeHH,1)TienTeHH,TyGiaNhapKho,TyGiaThanhToan,DonViVCQuocTe,ISNULL(TienTeVC,1)TienTeVC,TienVCQT,"
        sql &= " 	PhiCO,PhiHaiQuan,PhiLuuKho,PhiVCMatDat,PhiPSPhanBoChung,PhiPSPhanBoRieng,DonViVCNoiDia,"
        sql &= " 	TienVCNoiDia,PhiDiLaiThongQuan,PhiTTTienHangDuKien,ChiPhiKhac,GhiChu"
        sql &= " FROM DATHANGQT"
        sql &= " WHERE SoPhieuDatHang='" & SoPhieuDH & "'"
        sql &= " SELECT SUM(VATTU.KhoiLuong1 *DATHANG.SoLuong)/1000 FROM DATHANG"
        sql &= " INNER JOIN VATTU ON DATHANG.IDVatTu=VATTU.ID"
        sql &= " WHERE DATHANG.SoPhieu='" & SoPhieuDH & "'"
        sql &= " SELECT SUM(SoLuong*FOB) FROM DATHANG "
        sql &= " WHERE DATHANG.SoPhieu='" & SoPhieuDH & "'"

        sql &= " DECLARE @TongFOB as float"
        sql &= " DECLARE @SP as nvarchar(15)"
        sql &= " DECLARE @TienVCQT as float"
        sql &= " SET @SP='" & SoPhieuDH & "'"
        sql &= " SET @TienVCQT = (SELECT IsNULL(SoTien,0) FROM CHIPHI WHERE PHIEUCGDH=@SP AND Loai=0 AND TienTE <>0)"
        sql &= " SET @TongFOB = (SELECT SUM(FOB*SoLuong) FROM DATHANG WHERE SoPhieu=@SP)"
        sql &= " IF @TongFOB=0 "
        sql &= "         BEGIN "
        sql &= " 	SET @TongFOB = 1"
        sql &= "         End"
        sql &= " IF @TienVCQT is null"
        sql &= "    BEGIN"
        sql &= "        SET @TienVCQT = 0"
        sql &= "    END"
        sql &= " SELECT ISNULL( SUM(ThueNK),0) FROM"
        sql &= " ("
        sql &= " SElECT ID,IDVatTu,FOB,(FOB*SoLuong)ThanhTienFOB,TNK,"
        sql &= " 	(@TienVCQT*(DATHANGQT.TyGiaHQVC"
        sql &= " 							/(CASE DATHANGQT.TyGiaNhapKho WHEN 0 THEN 0 "
        sql &= " 								ELSE DATHANGQT.TyGiaNhapKho END))/@TongFOB)*(FOB*SoLuong) AS TienVC,"
        sql &= " (((FOB*SoLuong) + "
        sql &= " 	(@TienVCQT*(DATHANGQT.TyGiaHQVC"
        sql &= " 						/(CASE DATHANGQT.TyGiaNhapKho WHEN 0 THEN 1 "
        sql &= " 						ELSE DATHANGQT.TyGiaNhapKho END) )/@TongFOB)*(FOB*SoLuong)) * TNK/100)*TyGiaHQHH AS ThueNK"
        sql &= " FROM DATHANG "
        sql &= " INNER JOIN DATHANGQT ON DATHANG.SoPhieu=DATHANGQT.SoPhieuDatHang"
        sql &= " WHERE SoPhieu=@SP)tb"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            If ds.Tables(0).Rows.Count > 0 Then
                tbPhieuNK.EditValue = ds.Tables(0).Rows(0)("SoPhieuNhapKhau")
                tbPhieuDH.EditValue = ds.Tables(0).Rows(0)("SoPhieuDatHang")
                tbSoToKhai.EditValue = ds.Tables(0).Rows(0)("SoToKhai")
                gdvCuaKhauHQ.EditValue = ds.Tables(0).Rows(0)("CuaKhauHQ")
                tbSoKienHang.EditValue = ds.Tables(0).Rows(0)("SoKienHang")
                tbKhoiLuongHang.EditValue = ds.Tables(0).Rows(0)("KhoiLuongHang")
                tbTheTichHang.EditValue = ds.Tables(0).Rows(0)("TheTichHang")
                cbTienTeHH.EditValue = Convert.ToByte(ds.Tables(0).Rows(0)("TienTeHH"))
                tbTyGiaHQ.EditValue = ds.Tables(0).Rows(0)("TyGiaHQHH")
                tbTyGiaHQVC.EditValue = ds.Tables(0).Rows(0)("TyGiaHQVC")
                tbTyGiaNK.EditValue = ds.Tables(0).Rows(0)("TyGiaNhapKho")
                tbTyGiaThanhToan.EditValue = ds.Tables(0).Rows(0)("TyGiaThanhToan")

                tbTienHangDuKien.EditValue = ds.Tables(0).Rows(0)("PhiTTTienHangDuKien")

                tbGhiChu.EditValue = ds.Tables(0).Rows(0)("GhiChu")


            Else
                tbPhieuDH.EditValue = PhieuDatHang
            End If
            tbKhoiLuongDuKien.EditValue = ds.Tables(1).Rows(0)(0)
            Try
                tbTongFOB.EditValue = ds.Tables(2).Rows(0)(0)
            Catch ex As Exception
                tbTongFOB.EditValue = 0
            End Try

            Try
                tbThueNK.EditValue = Math.Round(ds.Tables(3).Rows(0)(0), 0)
            Catch ex As Exception
                tbThueNK.EditValue = 0
            End Try

        Else
            ShowBaoLoi(LoiNgoaiLe)

        End If
    End Sub

    Public Sub TinhTNK()
        Dim sql As String = ""
        sql &= " DECLARE @TongFOB as float"
        sql &= " DECLARE @SP as nvarchar(15)"
        sql &= " DECLARE @TienVCQT as float"
        sql &= " SET @SP='" & tbPhieuDH.EditValue & "'"
        sql &= " SET @TienVCQT = (SELECT IsNULL(SoTien,0) FROM CHIPHI WHERE PHIEUCGDH=@SP AND Loai=0 AND TienTE <>0)"
        sql &= " SET @TongFOB = (SELECT SUM(FOB*SoLuong) FROM DATHANG WHERE SoPhieu=@SP)"
        sql &= " IF @TongFOB=0 "
        sql &= "    BEGIN"
        sql &= " 	    SET @TongFOB = 1"
        sql &= "    End"
        sql &= " IF @TienVCQT is null"
        sql &= "    BEGIN"
        sql &= "        SET @TienVCQT = 0"
        sql &= "    END"
        sql &= " SELECT ISNULL( SUM(ThueNK),0) FROM"
        sql &= " ("
        sql &= " SElECT ID,IDVatTu,FOB,(FOB*SoLuong)ThanhTienFOB,TNK,"
        sql &= " 	(@TienVCQT*(DATHANGQT.TyGiaHQVC"
        sql &= " 							/(CASE DATHANGQT.TyGiaNhapKho WHEN 0 THEN 0 "
        sql &= " 								ELSE DATHANGQT.TyGiaNhapKho END))/@TongFOB)*(FOB*SoLuong) AS TienVC,"
        sql &= " (((FOB*SoLuong) + "
        sql &= " 	(@TienVCQT*(DATHANGQT.TyGiaHQVC"
        sql &= " 						/(CASE DATHANGQT.TyGiaNhapKho WHEN 0 THEN 1 "
        sql &= " 						ELSE DATHANGQT.TyGiaNhapKho END) )/@TongFOB)*(FOB*SoLuong)) * TNK/100)*TyGiaHQHH AS ThueNK"
        sql &= " FROM DATHANG "
        sql &= " INNER JOIN DATHANGQT ON DATHANG.SoPhieu=DATHANGQT.SoPhieuDatHang"
        sql &= " WHERE SoPhieu=@SP)tb"


        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            tbThueNK.EditValue = Math.Round(tb.Rows(0)(0), 0)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvNhaCCDatHang_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvCuaKhauHQ.KeyPress
        If gdvCuaKhauHQ.IsPopupOpen Then
            Exit Sub
        Else
            gdvCuaKhauHQ.ShowPopup()
        End If
    End Sub


    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub btLuuLai_Click(sender As System.Object, e As System.EventArgs) Handles btLuuLai.Click
        Dim SPNK As New Object
        If tbPhieuNK.EditValue Is Nothing Then
            SPNK = LaySoPhieuNhapKhau("DATHANGQT")
        End If
        AddParameter("@SoPhieuDatHang", tbPhieuDH.EditValue)
        AddParameter("@SoToKhai", tbSoToKhai.EditValue)
        AddParameter("@CuaKhauHQ", gdvCuaKhauHQ.EditValue)
        AddParameter("@SoKienHang", tbSoKienHang.EditValue)
        AddParameter("@KhoiLuongHang", tbKhoiLuongHang.EditValue)
        AddParameter("@TheTichHang", tbTheTichHang.EditValue)
        AddParameter("@TienTeHH", cbTienTeHH.EditValue)
        AddParameter("@TyGiaHQHH", tbTyGiaHQ.EditValue)
        AddParameter("@TyGiaHQVC", tbTyGiaHQVC.EditValue)
        AddParameter("@TyGiaNhapKho", tbTyGiaNK.EditValue)
        AddParameter("@TyGiaThanhToan", tbTyGiaThanhToan.EditValue)
        AddParameter("@PhiTTTienHangDuKien", tbTienHangDuKien.EditValue)
        AddParameter("@GhiChu", tbGhiChu.EditValue)

        If tbPhieuNK.EditValue Is Nothing Then
            AddParameter("@SoPhieuNhapKhau", SPNK)
            If doInsert("DATHANGQT") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            Else
                tbPhieuNK.EditValue = SPNK
            End If
        Else
            AddParameterWhere("@SoPhieuNhapKhau", tbPhieuNK.EditValue)
            If doUpdate("DATHANGQT", "SoPhieuNhapKhau=@SoPhieuNhapKhau") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
        End If

        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        For i As Integer = 0 To gdvCT.DataRowCount - 1
            If gdvCT.GetRowCellValue(i, "Modify") Then
                AddParameter("@IDDVVC", gdvCT.GetRowCellValue(i, "IDDVVC"))
                AddParameter("@SoBill", gdvCT.GetRowCellValue(i, "SoBill").ToString)
                AddParameter("@CanNang", tbKhoiLuongHang.EditValue)
                AddParameter("@ThoiGian", gdvCT.GetRowCellValue(i, "ThoiGian"))
                AddParameter("@SoTien", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@SoTienTC", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@TienTe", gdvCT.GetRowCellValue(i, "TienTe"))
                AddParameter("@TyGia", gdvCT.GetRowCellValue(i, "TyGia"))
                AddParameter("@GhiChu", gdvCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@PhieuCGDH", tbPhieuDH.EditValue)
                AddParameter("@MucDich", gdvCT.GetRowCellValue(i, "MucDich"))
                AddParameter("@Loai", False)
                AddParameter("@IDUser", CType(TaiKhoan, Int32))
                AddParameter("@SoHD", gdvCT.GetRowCellValue(i, "SoHD"))
                AddParameter("@NgayHD", gdvCT.GetRowCellValue(i, "NgayHD"))
                AddParameter("@KyHieuHD", gdvCT.GetRowCellValue(i, "KyHieuHD"))
                AddParameter("@PTVAT", gdvCT.GetRowCellValue(i, "PTVAT"))
                AddParameter("@TienVAT", gdvCT.GetRowCellValue(i, "TienVAT"))
                If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Then
                    Dim _objID As Object = doInsert("CHIPHI")
                    If _objID Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        gdvCT.SetRowCellValue(i, "ID", _objID)
                    End If
                Else
                    AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "ID"))
                    If doUpdate("CHIPHI", "ID=@IDD") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                End If

            End If
        Next
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()

        TinhTNK()
        ShowAlert("Đã cập nhật !")

    End Sub

    'Private Sub tbTienVCQuocTe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTongFOB.EditValueChanged
    '    If tbTongFOB.EditValue = 0 Then
    '        tbPTVCVaFOB.EditValue = tbTienVCQuocTe.EditValue
    '        tbPTTongCPVaFOB.EditValue = 0
    '    Else
    '        If tbTyGiaNK.EditValue = 0 Then
    '            tbPTVCVaFOB.EditValue = 0
    '            tbPTTongCPVaFOB.EditValue = 0
    '        Else
    '            tbPTVCVaFOB.EditValue = Math.Round((tbTienVCQuocTe.EditValue / tbTongFOB.EditValue) * (tbTyGiaTTVC.EditValue / tbTyGiaNK.EditValue) * 100, 2, MidpointRounding.AwayFromZero)
    '            tbPTTongCPVaFOB.EditValue = Math.Round(((tbTienVCQuocTe.EditValue * tbTyGiaTTVC.EditValue) + tbPhiDiLai.EditValue + tbPhiHaiQuan.EditValue + tbPhiVCMatDat.EditValue + tbPhiLuuKho.EditValue + tbPhiVCNoiDia.EditValue + tbPhiPSChung.EditValue + tbPhiCO.EditValue + tbChiPhiKhac.EditValue + tbPhiPSRieng.EditValue) / (tbTongFOB.EditValue * tbTyGiaNK.EditValue) * 100, 2, MidpointRounding.AwayFromZero)
    '        End If
    '    End If
    'End Sub

    'Private Sub tbPhiDiLai_EditValueChanged(sender As System.Object, e As System.EventArgs)
    '    If tbTongFOB.EditValue = 0 Then
    '        tbPTTongCPVaFOB.EditValue = 0
    '    Else
    '        If tbTyGiaNK.EditValue = 0 Then
    '            tbPTTongCPVaFOB.EditValue = 0
    '        Else
    '            tbPTTongCPVaFOB.EditValue = Math.Round(((tbTienVCQuocTe.EditValue * tbTyGiaTTVC.EditValue) + tbPhiDiLai.EditValue + tbPhiHaiQuan.EditValue + tbPhiVCMatDat.EditValue + tbPhiLuuKho.EditValue + tbPhiVCNoiDia.EditValue + tbPhiPSChung.EditValue + tbPhiCO.EditValue + tbChiPhiKhac.EditValue + tbPhiPSRieng.EditValue) / (tbTongFOB.EditValue * tbTyGiaNK.EditValue) * 100, 2, MidpointRounding.AwayFromZero)
    '        End If
    '    End If
    '    tbChiPhiNoiDia.EditValue = tbPhiDiLai.EditValue + tbPhiHaiQuan.EditValue + tbPhiVCMatDat.EditValue + tbPhiLuuKho.EditValue + tbPhiVCNoiDia.EditValue + tbPhiPSChung.EditValue + tbPhiCO.EditValue + tbChiPhiKhac.EditValue + tbPhiPSRieng.EditValue
    'End Sub


    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        gdvCT.SetFocusedRowCellValue("STT", gdvCT.DataRowCount + 1)

    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        On Error Resume Next
        If e.Column.FieldName = "TienTe" Then
            gdvCT.SetRowCellValue(e.RowHandle, "TyGia", CType(rcbTienTe.DataSource, DataTable).Select("ID=" & e.Value)(0)("TyGia"))
            gdvCT.CloseEditor()
            gdvCT.UpdateCurrentRow()
        ElseIf e.Column.FieldName = "IDDVVC" Then
            gdvCT.SetFocusedRowCellValue("MucDich", 205)
            gdvCT.SetFocusedRowCellValue("ThoiGian", Now)
            gdvCT.SetFocusedRowCellValue("TienTe", 0)
        End If
        If e.Column.FieldName <> "Modify" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub


    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If gdvCT.FocusedRowHandle < 0 Then
            If e.Control AndAlso e.KeyCode = Keys.Delete Then
                If ShowCauHoi("Xóa dòng được chọn ?") Then
                    If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Then
                        gdvCT.DeleteSelectedRows()
                    Else
                        If KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                            AddParameterWhere("@IDD", gdvCT.GetFocusedRowCellValue("ID"))
                            If doDelete("CHIPHI", "ID=@IDD") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            Else
                                gdvCT.DeleteSelectedRows()
                            End If
                        End If
                    End If
                End If
                
            End If
        End If
    End Sub

    Private Function tryNoThing(obj As Object) As String
        If obj Is Nothing Then Return ""
        Return obj.ToString.Trim
    End Function

    Private Sub btnDuaSangHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btnDuaSangHoaDon.Click

        If Not ShowCauHoi("Đưa hết các chi phí có số hóa đơn sang bên thuế?") Then Exit Sub

        For i As Integer = 0 To gdvCT.RowCount - 1
            If tryNoThing(gdvCT.GetRowCellValue(i, "NgayHD")) = "" Or _
                tryNoThing(gdvCT.GetRowCellValue(i, "SoHD")) = "" Then Continue For

            'Tong hop
            Dim dtKH As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachiTs FROM KHACHHANG WHERE ID = " & gdvCT.GetRowCellValue(i, "IDDVVC"))

            AddParameter("@NgayCT", gdvCT.GetRowCellValue(i, "NgayHD"))
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
            AddParameter("@LoaiCT2", ChungTu.LoaiCT2.MuaDichVu)
            AddParameter("@IdKH", gdvCT.GetRowCellValue(i, "IDDVVC"))

            If Not dtKH Is Nothing AndAlso dtKH.Rows.Count > 0 Then
                AddParameter("@TenKH", dtKH.Rows(0)("Ten"))
                AddParameter("@DiaChi", dtKH.Rows(0)("ttcDiachiTs"))
                AddParameter("@MaSoThue", dtKH.Rows(0)("ttcMasothue"))
            End If

            AddParameter("@NguoiLienHe", "")
            AddParameter("@HtThanhToan", HoaDonGTGT.HinhThucThanhToan.TrangThai.TMCK)
            AddParameter("@TienTe", 0)
            AddParameter("@TyGia", 1)

            If tryNoThing(gdvCT.GetRowCellValue(i, "PTVAT")) = "" Then
                AddParameter("@Thue", DBNull.Value)
            Else
                AddParameter("@Thue", gdvCT.GetRowCellValue(i, "PTVAT"))
            End If

            AddParameter("@KemBangKe", 0)
            AddParameter("@DienGiai", gdvCT.GetRowCellValue(i, "GhiChu"))
            AddParameter("@NgayHD", gdvCT.GetRowCellValue(i, "NgayHD"))
            AddParameter("@SoHD", gdvCT.GetRowCellValue(i, "SoHD"))
            AddParameter("@KyHieuHD", gdvCT.GetRowCellValue(i, "KyHieuHD"))
            AddParameter("@ThanhTien", gdvCT.GetRowCellValue(i, "SoTien") - gdvCT.GetRowCellValue(i, "TienVAT"))
            AddParameter("@TienThue", gdvCT.GetRowCellValue(i, "TienVAT"))
            AddParameter("@TongTien", gdvCT.GetRowCellValue(i, "SoTien"))
            AddParameter("@GhiSo", 1)
            AddParameter("@NguoiLap", TaiKhoan)

            Dim idHoaDon As Object = DBNull.Value
            If gdvCT.GetRowCellValue(i, "IdChungTu") Is DBNull.Value Then
                idHoaDon = doInsert("CHUNGTU")
                If idHoaDon Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                idHoaDon = gdvCT.GetRowCellValue(i, "IdChungTu")
                AddParameterWhere("@dk_Id", idHoaDon)
                If doUpdate("CHUNGTU", "Id=@dk_Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            'Update bên chi phí
            AddParameter("@IdChungTu", idHoaDon)
            AddParameterWhere("@dk_IdChiPhi", gdvCT.GetRowCellValue(i, "ID"))
            If doUpdate("ChiPhi", "ID=@dk_IdChiPhi") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            Dim __ref As String = ChungTu.getRef

            'Hang tien
            AddParameter("@Id_CT", idHoaDon)
            AddParameter("@ref", __ref)
            AddParameter("@IdVatTu", DBNull.Value)
            AddParameter("@DienGiai", gdvCT.GetRowCellValue(i, "GhiChu"))
            AddParameter("@ThanhTien", gdvCT.GetRowCellValue(i, "SoTien") - gdvCT.GetRowCellValue(i, "TienVAT"))
            '-- AddParameter("@TaiKhoanNo", "")
            AddParameter("@TaiKhoanCo", "331")
            AddParameter("@GhiChuKhac", tbPhieuDH.EditValue)
            If gdvCT.GetRowCellValue(i, "IdChungTu") Is DBNull.Value Then
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
                If doInsert("CHUNGTUCHITIET") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@dk_Id_CT", idHoaDon)
                If doUpdate("CHUNGTUCHITIET", "Id_CT=@dk_Id_CT AND ButToan = 1 ") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            'Thue
            AddParameter("@Id_CT", idHoaDon)
            AddParameter("@ref", __ref)
            AddParameter("@DienGiai", gdvCT.GetRowCellValue(i, "GhiChu"))
            AddParameter("@ThanhTien", gdvCT.GetRowCellValue(i, "TienVAT"))
            AddParameter("@TaiKhoanNo", "1331")
            AddParameter("@TaiKhoanCo", "331")
            AddParameter("@GhiChuKhac", tbPhieuDH.EditValue)
            If gdvCT.GetRowCellValue(i, "IdChungTu") Is DBNull.Value Then
                AddParameter("@ButToan", ChungTu.LoaiButToan.ThueGTGT)
                If doInsert("CHUNGTUCHITIET") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameterWhere("@dk_Id_CT", idHoaDon)
                If doUpdate("CHUNGTUCHITIET", "Id_CT=@dk_Id_CT AND ButToan = 2 ") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            'Dat lai idhoadon cho danh sach chi phi
            gdvCT.SetRowCellValue(i, "IdChungTu", idHoaDon)

        Next

        ShowAlert("Đã đưa các chi phí có số hóa đơn vào bên thuế!")

    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        If e.RowHandle >= 0 AndAlso gdvCT.GetRowCellValue(e.RowHandle, "IdChungTu").ToString.Trim <> "" Then
            e.Appearance.BackColor = Color.LightPink
        End If
    End Sub

    Private Sub gdvCT_InvalidValueException(sender As System.Object, e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles gdvCT.InvalidValueException
        If gdvCT.FocusedRowHandle >= 0 And gdvCT.FocusedColumn.FieldName = "PTVAT" Then
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore
            gdvCT.SetFocusedRowCellValue("PTVAT", DBNull.Value)
        End If
    End Sub



End Class