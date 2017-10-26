Imports BACSOFT.Db.SqlHelper

Public Class frmThongTinHaiQuan
    Public PhieuDatHang As Object

    Private Sub frmThongTinHaiQuan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDataCb()
        loadThongTinChinh(PhieuDatHang)
    End Sub

    Public Sub LoadDataCb()
        Dim sql As String = ""
        sql &= " SELECT ID,ttcMa,Ten FROM KHACHHANG ORDER BY ttcMa"
        sql &= " SELECT ID,Ten FROM tblTienTe ORDER BY ID"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvCuaKhauHQ.Properties.DataSource = ds.Tables(0)
            gdvDonViVCNoiDia.Properties.DataSource = ds.Tables(0)
            gdvDoViVCQuocTe.Properties.DataSource = ds.Tables(0)
            cbTienTeVC.Properties.DataSource = ds.Tables(1)
            cbTienTeVC.EditValue = ds.Tables(1).Rows(1)(0)
            cbTienTeHH.Properties.DataSource = ds.Tables(1)
            cbTienTeHH.EditValue = ds.Tables(1).Rows(1)(0)
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
        sql &= " WHERE SoPhieuDatHang=" & SoPhieuDH
        sql &= " SELECT SUM(VATTU.KhoiLuong1 *DATHANG.SoLuong)/1000 FROM DATHANG"
        sql &= " INNER JOIN VATTU ON DATHANG.IDVatTu=VATTU.ID"
        sql &= " WHERE DATHANG.SoPhieu=" & SoPhieuDH
        sql &= " SELECT SUM(SoLuong*FOB) FROM DATHANG "
        sql &= " WHERE DATHANG.SoPhieu=" & SoPhieuDH
        sql &= " DECLARE @TongFOB as float"
        sql &= " SET @TongFOB = (SELECT SUM(FOB*SoLuong) FROM DATHANG WHERE SoPhieu=" & SoPhieuDH & ")"
        sql &= " IF @TongFOB=0 "
        sql &= "             BEGIN"
        sql &= " 	SET @TongFOB = 1"
        sql &= "             End"
        sql &= " SELECT SUM(ThueNK) FROM"
        sql &= " ("
        sql &= " SElECT ID,IDVatTu,FOB,(FOB*SoLuong)ThanhTienFOB,TNK,"
        sql &= " 	(DATHANGQT.TienVCQT*(DATHANGQT.TyGiaHQVC/(CASE DATHANGQT.TyGiaNhapKho WHEN 0 THEN 0 ELSE DATHANGQT.TyGiaNhapKho END))/@TongFOB)*(FOB*SoLuong) AS TienVC,"
        sql &= " (((FOB*SoLuong) + (DATHANGQT.TienVCQT*(DATHANGQT.TyGiaHQVC/(CASE DATHANGQT.TyGiaNhapKho WHEN 0 THEN 1 ELSE DATHANGQT.TyGiaNhapKho END) )/@TongFOB)*(FOB*SoLuong)) * TNK/100)*TyGiaHQHH AS ThueNK"
        sql &= " FROM DATHANG "
        sql &= " INNER JOIN DATHANGQT ON DATHANG.SoPhieu=DATHANGQT.SoPhieuDatHang"
        sql &= " WHERE SoPhieu=" & SoPhieuDH & ")tb"
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
                tbTyGiaNK.EditValue = ds.Tables(0).Rows(0)("TyGiaNhapKho")
                tbTyGiaThanhToan.EditValue = ds.Tables(0).Rows(0)("TyGiaThanhToan")
                gdvDoViVCQuocTe.EditValue = ds.Tables(0).Rows(0)("DonViVCQuocTe")
                cbTienTeVC.EditValue = Convert.ToByte(ds.Tables(0).Rows(0)("TienTeVC"))
                tbTienVCQuocTe.EditValue = ds.Tables(0).Rows(0)("TienVCQT")
                tbPhiCO.EditValue = ds.Tables(0).Rows(0)("PhiCO")
                tbPhiHaiQuan.EditValue = ds.Tables(0).Rows(0)("PhiHaiQuan")
                tbPhiLuuKho.EditValue = ds.Tables(0).Rows(0)("PhiLuuKho")
                tbPhiVCMatDat.EditValue = ds.Tables(0).Rows(0)("PhiVCMatDat")
                tbPhiPSChung.EditValue = ds.Tables(0).Rows(0)("PhiPSPhanBoChung")
                tbPhiPSRieng.EditValue = ds.Tables(0).Rows(0)("PhiPSPhanBoRieng")
                gdvDonViVCNoiDia.EditValue = ds.Tables(0).Rows(0)("DonViVCNoiDia")
                tbPhiVCNoiDia.EditValue = ds.Tables(0).Rows(0)("TienVCNoiDia")
                tbPhiDiLai.EditValue = ds.Tables(0).Rows(0)("PhiDiLaiThongQuan")
                tbTienHangDuKien.EditValue = ds.Tables(0).Rows(0)("PhiTTTienHangDuKien")
                tbChiPhiKhac.EditValue = ds.Tables(0).Rows(0)("ChiPhiKhac")
                tbGhiChu.EditValue = ds.Tables(0).Rows(0)("GhiChu")
                tbTyGiaTTVC.EditValue = ds.Tables(0).Rows(0)("TyGiaTTVC")
                tbTyGiaHQVC.EditValue = ds.Tables(0).Rows(0)("TyGiaHQVC")

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
        sql &= " SET @TongFOB = (SELECT SUM(FOB*SoLuong) FROM DATHANG WHERE SoPhieu=" & tbPhieuDH.EditValue & ")"
        sql &= " IF @TongFOB=0 "
        sql &= "             BEGIN"
        sql &= " 	SET @TongFOB = 1"
        sql &= "             End"
        Sql &= " SELECT SUM(ThueNK) FROM"
        Sql &= " ("
        Sql &= " SElECT ID,IDVatTu,FOB,(FOB*SoLuong)ThanhTienFOB,TNK,"
        Sql &= " 	(DATHANGQT.TienVCQT*(DATHANGQT.TyGiaHQVC/(CASE DATHANGQT.TyGiaNhapKho WHEN 0 THEN 0 ELSE DATHANGQT.TyGiaNhapKho END))/@TongFOB)*(FOB*SoLuong) AS TienVC,"
        Sql &= " (((FOB*SoLuong) + (DATHANGQT.TienVCQT*(DATHANGQT.TyGiaHQVC/(CASE DATHANGQT.TyGiaNhapKho WHEN 0 THEN 1 ELSE DATHANGQT.TyGiaNhapKho END) )/@TongFOB)*(FOB*SoLuong)) * TNK/100)*TyGiaHQHH AS ThueNK"
        Sql &= " FROM DATHANG "
        Sql &= " INNER JOIN DATHANGQT ON DATHANG.SoPhieu=DATHANGQT.SoPhieuDatHang"
        sql &= " WHERE SoPhieu=" & tbPhieuDH.EditValue & ")tb"
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

    Private Sub gdvDoViVCQuocTe_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvDoViVCQuocTe.KeyPress
        If gdvDoViVCQuocTe.IsPopupOpen Then
            Exit Sub
        Else
            gdvDoViVCQuocTe.ShowPopup()
        End If
    End Sub

    Private Sub gdvDonViVCNoiDia_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvDonViVCNoiDia.KeyPress
        If gdvDonViVCNoiDia.IsPopupOpen Then
            Exit Sub
        Else
            gdvDonViVCNoiDia.ShowPopup()
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
        AddParameter("@TyGiaTTVC", tbTyGiaTTVC.EditValue)
        AddParameter("@TyGiaHQVC", tbTyGiaHQVC.EditValue)
        AddParameter("@TyGiaHQHH", tbTyGiaHQ.EditValue)
        AddParameter("@TyGiaNhapKho", tbTyGiaNK.EditValue)
        AddParameter("@TyGiaThanhToan", tbTyGiaThanhToan.EditValue)
        AddParameter("@DonViVCQuocTe", gdvDoViVCQuocTe.EditValue)
        AddParameter("@TienTeVC", cbTienTeVC.EditValue)
        AddParameter("@TienVCQT", tbTienVCQuocTe.EditValue)
        AddParameter("@PhiCO", tbPhiCO.EditValue)
        AddParameter("@PhiHaiQuan", tbPhiHaiQuan.EditValue)
        AddParameter("@PhiLuuKho", tbPhiLuuKho.EditValue)
        AddParameter("@PhiVCMatDat", tbPhiVCMatDat.EditValue)
        AddParameter("@PhiPSPhanBoChung", tbPhiPSChung.EditValue)
        AddParameter("@PhiPSPhanBoRieng", tbPhiPSRieng.EditValue)
        AddParameter("@DonViVCNoiDia", gdvDonViVCNoiDia.EditValue)
        AddParameter("@TienVCNoiDia", tbPhiVCNoiDia.EditValue)
        AddParameter("@PhiDiLaiThongQuan", tbPhiDiLai.EditValue)
        AddParameter("@PhiTTTienHangDuKien", tbTienHangDuKien.EditValue)
        AddParameter("@ChiPhiKhac", tbChiPhiKhac.EditValue)
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
        TinhTNK()
        ShowAlert("Đã cập nhật !")

    End Sub

    Private Sub tbTienVCQuocTe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbTienVCQuocTe.EditValueChanged, tbTongFOB.EditValueChanged
        If tbTongFOB.EditValue = 0 Then
            tbPTVCVaFOB.EditValue = tbTienVCQuocTe.EditValue
            tbPTTongCPVaFOB.EditValue = 0
        Else
            If tbTyGiaNK.EditValue = 0 Then
                tbPTVCVaFOB.EditValue = 0
                tbPTTongCPVaFOB.EditValue = 0
            Else
                tbPTVCVaFOB.EditValue = Math.Round((tbTienVCQuocTe.EditValue / tbTongFOB.EditValue) * (tbTyGiaTTVC.EditValue / tbTyGiaNK.EditValue) * 100, 2, MidpointRounding.AwayFromZero)
                tbPTTongCPVaFOB.EditValue = Math.Round(((tbTienVCQuocTe.EditValue * tbTyGiaTTVC.EditValue) + tbPhiDiLai.EditValue + tbPhiHaiQuan.EditValue + tbPhiVCMatDat.EditValue + tbPhiLuuKho.EditValue + tbPhiVCNoiDia.EditValue + tbPhiPSChung.EditValue + tbPhiCO.EditValue + tbChiPhiKhac.EditValue + tbPhiPSRieng.EditValue) / (tbTongFOB.EditValue * tbTyGiaNK.EditValue) * 100, 2, MidpointRounding.AwayFromZero)
            End If
        End If
    End Sub

    Private Sub tbPhiDiLai_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbPhiDiLai.EditValueChanged, tbPhiHaiQuan.EditValueChanged, tbPhiVCMatDat.EditValueChanged, tbPhiLuuKho.EditValueChanged, tbPhiVCNoiDia.EditValueChanged, tbPhiPSChung.EditValueChanged, tbPhiCO.EditValueChanged, tbChiPhiKhac.EditValueChanged, tbPhiPSRieng.EditValueChanged
        If tbTongFOB.EditValue = 0 Then
            tbPTTongCPVaFOB.EditValue = 0
        Else
            If tbTyGiaNK.EditValue = 0 Then
                tbPTTongCPVaFOB.EditValue = 0
            Else
                tbPTTongCPVaFOB.EditValue = Math.Round(((tbTienVCQuocTe.EditValue * tbTyGiaTTVC.EditValue) + tbPhiDiLai.EditValue + tbPhiHaiQuan.EditValue + tbPhiVCMatDat.EditValue + tbPhiLuuKho.EditValue + tbPhiVCNoiDia.EditValue + tbPhiPSChung.EditValue + tbPhiCO.EditValue + tbChiPhiKhac.EditValue + tbPhiPSRieng.EditValue) / (tbTongFOB.EditValue * tbTyGiaNK.EditValue) * 100, 2, MidpointRounding.AwayFromZero)
            End If
        End If
        tbChiPhiNoiDia.EditValue = tbPhiDiLai.EditValue + tbPhiHaiQuan.EditValue + tbPhiVCMatDat.EditValue + tbPhiLuuKho.EditValue + tbPhiVCNoiDia.EditValue + tbPhiPSChung.EditValue + tbPhiCO.EditValue + tbChiPhiKhac.EditValue + tbPhiPSRieng.EditValue
    End Sub

   
End Class