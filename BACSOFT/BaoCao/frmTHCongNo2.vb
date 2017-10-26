Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmTHCongNo2
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public ThuPSNo As Double = 0
    Public ThuPSCo As Double = 0
    Public ChiPSNo As Double = 0
    Public ChiPSCo As Double = 0
    Public ThuLuyKe As Double = 0
    Public ChiLuyKe As Double = 0

    Private Sub frmTHCongNo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        btNgayChotCuoiKyXem.EditValue = DateAdd(DateInterval.Day, 0, DateSerial(_ToDay.Year, Today.Month, 0))
        btNgayChotNoDauKy.EditValue = DateSerial(2012, 1, 1)
        LoadTuDien()
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

#End Region

    Private Sub LoadPhaiThu()
        gdvPhaiThuCT.ClearColumnsFilter()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY ttcMa) AS STT,* FROM ("
        sql &= " Select ID,ttcMa ,ISNULL(tbNoDauKy.NoDauKy,0) AS NDK,"
        sql &= " (ISNULL(tbNoDauKy.UngDauKy,0) + ISNULL(tbThuTUDK.SoTien,0) + ISNULL(tbThuNHTUDK.SoTien,0) - ISNULL(tbChiHUDK.SoTien,0) - ISNULL(tbUNCHUDK.SoTien,0)) AS UDK,"
        sql &= " (ISNULL(tbThuTUDK.SoTien,0)+ ISNULL(tbThuNHTUDK.SoTien,0)) AS UngTK0,"
        sql &= " (ISNULL(tbNoDauKy.NoDauKy,0) + ISNULL(tbPhaiThuDK.PhaiThu,0)+ ISNULL(tbXLNoPhaiThuDK.SoTien,0) - ISNULL(tbDaThuDK.SoTien,0) - ISNULL(tbDaThuNHDK.SoTien,0)) AS ConPhaiThuDK,"
        sql &= " (ISNULL(tbChiHUDK.SoTien,0) + ISNULL(tbUNCHUDK.SoTien,0)) AS HoanungTK0,"
        sql &= " (ISNULL(tbThuTUTK.SoTien,0) + ISNULL(tbThuNHTUTK.SoTien,0)) AS UngTK,"
        sql &= " (ISNULL(tbPhaiThuTK.PhaiThu,0) + ISNULL(tbXLNoPhaiThuTK.SoTien,0)) AS PhaithuTK,"
        sql &= " (ISNULL(tbDaThuTK.SoTien,0) + ISNULL(tbDaThuNHTK.SoTien,0)) AS DathuTK,"
        sql &= " (ISNULL(tbChiHUTK.SoTien,0) + ISNULL(tbUNCHUTK.SoTien,0)) AS HoanungTK,"
        sql &= " (ISNULL(tbNoDauKy.NoDauKy,0)+ISNULL(tbThuTUDK.SoTien,0)+ ISNULL(tbThuNHTUDK.SoTien,0)+ISNULL(tbThuTUTK.SoTien,0) + ISNULL(tbThuNHTUTK.SoTien,0)-ISNULL(tbChiHUDK.SoTien,0) - ISNULL(tbUNCHUDK.SoTien,0)-ISNULL(tbChiHUTK.SoTien,0) - ISNULL(tbUNCHUTK.SoTien,0)) AS UngCK,"
        sql &= " (ISNULL(tbNoDauKy.NoDauKy,0) + ISNULL(tbPhaiThuDK.PhaiThu,0)+ ISNULL(tbXLNoPhaiThuDK.SoTien,0) + ISNULL(tbPhaiThuTK.PhaiThu,0) + ISNULL(tbXLNoPhaiThuTK.SoTien,0) - ISNULL(tbDaThuDK.SoTien,0) - ISNULL(tbDaThuNHDK.SoTien,0) - ISNULL(tbDaThuTK.SoTien,0) - ISNULL(tbDaThuNHTK.SoTien,0)) AS ConPhaiThu"
        sql &= " FROM KHACHHANG "
        sql &= " LEFT JOIN (SELECT isnull(SUM(NoDauKy),0)NoDauKy,ISNULL(SUM(Ungdauky),0)Ungdauky,IDKh FROM NOPHAITHU WHERE [month] = '" & Convert.ToDateTime(btNgayChotNoDauKy.EditValue).ToString("MM/yyyy") & "' GROUP BY IDKh)tbNoDauKy ON KHACHHANG.ID=tbNoDauKy.IDKh "
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THU where Mucdich in (100,105) and Tamung =1  and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbThuTUDK ON tbThuTUDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THUNH where Mucdich in (100,105) and Tamung =1  and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbThuNHTUDK ON tbThuNHTUDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum((tientruocthue + tienthue)*tygia),0)PhaiThu,IDKhachHang from PHIEUXUATKHO where  ngaythang between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKhachHang)tbPhaiThuDK ON tbPhaiThuDK.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from CHI where Mucdich = 246 and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbXLNoPhaiThuDK ON tbXLNoPhaiThuDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THU where Mucdich in (100,105) and Tamung =0  and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbDaThuDK ON tbDaThuDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THUNH where Mucdich in (100,105) and Tamung =0  and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbDaThuNHDK ON tbDaThuNHDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from CHI where Mucdich = 230 and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh )tbChiHUDK ON tbChiHUDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from UNC where Mucdich = 230 and Ngaythang between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh )tbUNCHUDK ON tbUNCHUDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THU where Mucdich in (100,105) and Tamung =1  and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbThuTUTK ON tbThuTUTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THUNH where Mucdich in (100,105) and Tamung = 1 and NgaythangVS  between @TuNgay and @DenNgay GROUP BY IDKh)tbThuNHTUTK ON tbThuNHTUTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum((tientruocthue + tienthue)*tygia),0)PhaiThu,IDKhachHang from PHIEUXUATKHO where ngaythang between @TuNgay and @DenNgay GROUP BY IDKhachHang)tbPhaiThuTK ON tbPhaiThuTK.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from CHI where Mucdich = 246 and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbXLNoPhaiThuTK ON tbXLNoPhaiThuTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THU where Mucdich in (100,105) and Tamung =0  and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbDaThuTK ON tbDaThuTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THUNH where Mucdich in (100,105) and Tamung =0  and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbDaThuNHTK ON tbDaThuNHTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from CHI where Mucdich = 230 and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbChiHUTK ON tbChiHUTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from UNC where Mucdich = 230 and Ngaythang between @TuNgay and @DenNgay GROUP BY IDKh)tbUNCHUTK ON tbUNCHUTK.IDKh=KHACHHANG.ID )tb"
        sql &= " WHERE NDK<>0 OR UDK<>0 OR ConPhaiThuDK<>0 OR UngTK<>0 OR PhaiThuTK<>0 OR HoanUngTK<>0 OR UngCK<>0 OR ConPhaiThu<>0"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND tb.ID=" & btfilterMaKH.EditValue
        End If
        sql &= " order by ttcMa"
        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        AddParameter("@NgayChotCuoiKyXem", btNgayChotCuoiKyXem.EditValue)
        AddParameter("@NgayChotNoDauKy", btNgayChotNoDauKy.EditValue)

        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then


            gdvPhaiThu.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadPhaiTra()

        ' gdvPhaiTraCT.ClearColumnsFilter()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "

        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY ttcMa) AS STT,* FROM"
        sql &= " (Select ID,ttcMa ,NDK = ISNULL(tbNoDauKy.NoDauKy,0)"
        sql &= "  ,UDK = ISNULL(tbNoDauKy.Ungdauky,0) + ISNULL(tbChiTUDK.SoTien,0) + ISNULL(tbUNCTUDK.SoTien,0) - ISNULL(tbThuHUDK.SoTien,0) - ISNULL(tbThuNHHUDK.SoTien,0)"
        sql &= " ,UngTK0 =(ISNULL(tbChiTUDK.SoTien,0) + ISNULL(tbUNCTUDK.SoTien,0))"
        sql &= " ,PhaitraTK0 =(ISNULL(tbPhaiTraDK.PhaiTra,0)+ISNULL(tbTienVayDK.SoTien,0) +ISNULL(tbTienVayNHDK.SoTien,0) )"
        sql &= " ,DatraTK0 = (ISNULL(tbChiDK.SoTien,0)+ISNULL(tbUNCDK.SoTien,0))"
        sql &= " ,HoanungTK0 = (ISNULL(tbThuHUDK.SoTien,0) + ISNULL(tbThuNHHUDK.SoTien,0))"
        sql &= " ,ConPhaiTraDK =ISNULL(tbNoDauKy.NoDauKy,0) + ISNULL(tbPhaiTraDK.PhaiTra,0)+ISNULL(tbTienVayDK.SoTien,0) +ISNULL(tbTienVayNHDK.SoTien,0) - ISNULL(tbChiDK.SoTien,0)-ISNULL(tbUNCDK.SoTien,0)"
        sql &= " ,UngTK =(ISNULL(tbChiTUTK.SoTien,0) + ISNULL(tbUNCTUTK.SoTien,0))"
        sql &= " ,PhaitraTK =(ISNULL(tbPhaiTraTK.PhaiTra,0) + ISNULL(tbDaTraTK.SoTien,0) + ISNULL(tbDaTraNHTK.SoTien,0))"
        sql &= " ,DatraTK = (ISNULL(tbChiTK.SoTien,0) + ISNULL(tbUNCTK.SoTien,0))"
        sql &= " ,HoanungTK = (ISNULL(tbThuHUTK.SoTien,0) + ISNULL(tbThuNHHUTK.SoTien,0))"
        sql &= " ,UngCK = (ISNULL(tbNoDauKy.Ungdauky,0) + ISNULL(tbChiTUDK.SoTien,0) + ISNULL(tbUNCTUDK.SoTien,0) + ISNULL(tbChiTUTK.SoTien,0) + ISNULL(tbUNCTUTK.SoTien,0) -ISNULL(tbThuHUDK.SoTien,0) - ISNULL(tbThuNHHUDK.SoTien,0)"
        sql &= "  - ISNULL(tbThuHUTK.SoTien,0) - ISNULL(tbThuNHHUTK.SoTien,0))"
        sql &= " ,ConPhaiTra= (ISNULL(tbNoDauKy.NoDauKy,0)+ISNULL(tbPhaiTraDK.PhaiTra,0)+ISNULL(tbTienVayDK.SoTien,0) +ISNULL(tbTienVayNHDK.SoTien,0)"
        sql &= " +ISNULL(tbPhaiTraTK.PhaiTra,0) + ISNULL(tbDaTraTK.SoTien,0) + ISNULL(tbDaTraNHTK.SoTien,0) "
        sql &= " -ISNULL(tbChiDK.SoTien,0)-ISNULL(tbUNCDK.SoTien,0) -ISNULL(tbChiTK.SoTien,0) - ISNULL(tbUNCTK.SoTien,0))"
        sql &= "  from KHACHHANG "
        sql &= " LEFT JOIN (SELECT isnull(SUM(NoDauKy),0)NoDauKy,ISNULL(SUM(Ungdauky),0)Ungdauky,IDKh FROM NOPHAITRA WHERE [month] = '01/2012' GROUP BY IDKh)tbNoDauKy ON KHACHHANG.ID=tbNoDauKy.IDKh "
        'Tạm ứng đầu kỳ"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from CHI where Mucdich in (210,239,229) and Tamung =1  and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbChiTUDK ON tbChiTUDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from UNC where Mucdich in (210,239,229) and Tamung =1  and Ngaythang between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbUNCTUDK ON tbUNCTUDK.IDKh=KHACHHANG.ID"
        'Phải trả đầu kỳ"
        sql &= " LEFT JOIN (select isnull(sum((tientruocthue + tienthue)*tygia),0)PhaiTra,IDKhachHang from PHIEUNHAPKHO where  ngaythang between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKhachHang)tbPhaiTraDK ON tbPhaiTraDK.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THU where Mucdich IN(106,113) and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbTienVayDK ON tbTienVayDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THUNH where Mucdich =106 and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbTienVayNHDK ON tbTienVayNHDK.IDKh=KHACHHANG.ID"
        'Đã trả đầu kỳ"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from CHI where Mucdich in (210,239,229) and Tamung =0 and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbChiDK ON tbChiDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from UNC where Mucdich in (210,239,229) and Tamung =0 AND Ngaythang between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh )tbUNCDK ON tbUNCDK.IDKh=KHACHHANG.ID"
        ' Hoàn ứng đk"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THU where Mucdich = 109 and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh )tbThuHUDK ON tbThuHUDK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THUNH where Mucdich =109  and NgaythangVS between @NgayChotNoDauKy and @NgayChotCuoiKyXem GROUP BY IDKh)tbThuNHHUDK ON tbThuNHHUDK.IDKh=KHACHHANG.ID"
        ' Ứng trong kỳ"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from Chi where Mucdich in (210,239,229) and Tamung = 1 and NgaythangVS  between @TuNgay and @DenNgay GROUP BY IDKh)tbChiTUTK ON tbChiTUTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from UNC where Mucdich in (210,239,229) and Tamung = 1 and Ngaythang between @TuNgay and @DenNgay GROUP BY IDKh)tbUNCTUTK ON tbUNCTUTK.IDKh=KHACHHANG.ID"
        'Phải trả TK"
        sql &= " LEFT JOIN (select isnull(sum((tientruocthue + tienthue)*tygia),0)PhaiTra,IDKhachHang from PHIEUNHAPKHO where ngaythang between @TuNgay and @DenNgay GROUP BY IDKhachHang)tbPhaiTraTK ON tbPhaiTraTK.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THU where Mucdich in (106,113) and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbDaTraTK ON tbDaTraTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THUNH where Mucdich = 106  and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbDaTraNHTK ON tbDaTraNHTK.IDKh=KHACHHANG.ID"
        ' Đã trả TK"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from CHI where Mucdich in (210,239,229)  and Tamung = 0 AND NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbChiTK ON tbChiTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from UNC where Mucdich in (210,239,229) and Tamung = 0 and Ngaythang between @TuNgay and @DenNgay GROUP BY IDKh)tbUNCTK ON tbUNCTK.IDKh=KHACHHANG.ID"
        ' Hoàn ứng TK"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THU where Mucdich = 109 and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh )tbThuHUTK ON tbThuHUTK.IDKh=KHACHHANG.ID"
        sql &= " LEFT JOIN (select isnull(sum(sotien),0)SoTien,IDKh from THUNH where Mucdich =109  and NgaythangVS between @TuNgay and @DenNgay GROUP BY IDKh)tbThuNHHUTK ON tbThuNHHUTK.IDKh=KHACHHANG.ID"
        sql &= " )tb "
        sql &= " WHERE NDK<>0 OR UDK<>0 OR ConPhaiTraDK<>0 OR UngTK<>0 OR PhaiTraTK<>0 OR HoanUngTK<>0 OR UngCK<>0 OR ConPhaiTra<>0"

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND tb.ID=" & btfilterMaKH.EditValue
        End If
        sql &= " order by ttcMA"
        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        AddParameter("@NgayChotCuoiKyXem", btNgayChotCuoiKyXem.EditValue)
        AddParameter("@NgayChotNoDauKy", btNgayChotNoDauKy.EditValue)

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then


            gdvPhaiTra.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadChiTietPhaiThu()

        ' gdvPhaiTraCT.ClearColumnsFilter()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "

        sql &= " SELECT 1 AS STT, NULL AS SP,NULL AS TongTien,NULL as NgayThangVS,N'Đầu kỳ: Ứng trước/ phải thu' AS Ten,"
        sql &= " NULL AS Model,NULL AS DVT,NULL AS SoLuong,NULL AS DonGia,NULL AS TienHang,NULL AS TienThue,"
        sql &= " (UDK+UngTruoc) As PSNo,(NDK+PhaiThu) AS PSCo,(NDK+PhaiThu-UDK-UngTruoc)LuyKe,TyGia,IDs"
        sql &= " FROM ("
        sql &= " Select NDK = (Select isnull(sum(nodauky),0) from NOPHAITHU where IDKh = @IDKH and month = '" & Convert.ToDateTime(btNgayChotNoDauKy.EditValue).ToString("MM/yyyy") & "')"
        sql &= " ,UDK = (Select isnull(sum(Ungdauky),0) from NOPHAITHU where IDKh = @IDKH and month = '" & Convert.ToDateTime(btNgayChotNoDauKy.EditValue).ToString("MM/yyyy") & "')"
        sql &= " ,UngTruoc =((select isnull(sum(sotien),0) from THU where IDKh = @IDKH and Mucdich in (100,105) and Tamung =1  and convert(datetime,Convert(nvarchar, NgaythangVS,103),103) between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  + (select isnull(sum(sotien),0) from THUNH where IDKh = @IDKH and Mucdich in (100,105) and Tamung = 1 and convert(datetime,Convert(nvarchar, NgaythangVS,103),103)  between @NgayChotNoDauKy and @NgayChotCuoiKyXem))"
        sql &= " ,Phaithu =((select isnull(sum((tientruocthue + tienthue)*tygia),0) from PHIEUXUATKHO where IDKhachhang = @IDKH and convert(datetime,Convert(nvarchar, Ngaythang,103),103)  between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  - (select isnull(sum(sotien),0) from THU where IDKh = @IDKH and Mucdich in (100,105) and Tamung =0  and NgaythangVS  between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  - (select isnull(sum(sotien),0) from THUNH where IDKh = @IDKH and Mucdich in (100,105) and Tamung = 0 and NgaythangVS  between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  + (select isnull(sum(sotien),0) from CHI where IDKh = @IDKH and Mucdich in (230,246) and NgaythangVS  between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  + (select isnull(sum(sotien),0) from UNC where IDKh = @IDKH and Mucdich = 230 and Ngaythang  between @NgayChotNoDauKy and @NgayChotCuoiKyXem))"
        sql &= " ,TyGia=1,IDs=0"
        sql &= " )tb"
        sql &= " UNION ALL"
        sql &= " Select NULL AS STT,  SP = ('XK' + PHIEUXUATKHO.Sophieu),((PHIEUXUATKHO.TienTruocThue+PHIEUXUATKHO.TienThue)*PHIEUXUATKHO.TyGia)TongTien, NgaythangVS = ngaythang, TENVATTU.Ten, VATTU.Model, TENDONVITINH.Ten AS DVT, XUATKHO.Soluong"
        sql &= " , Dongia = Dongia * tygia, Tienhang = (Soluong * Dongia * Tygia),   Tienthue = (Soluong * Dongia * Tygia* Mucthue /100 * Xuatthue)"
        sql &= " , PSNo = 0, PSco = 0,NULL AS LyKe, tygia, IDs=XUATKHO.ID "
        sql &= " from PHIEUXUATKHO inner join XUATKHO on PHIEUXUATKHO.Sophieu = XUATKHO.Sophieu inner join VATTU on VATTU.ID = IDvattu inner join TENVATTU on VATTU.IDTenvattu = TENVATTU.ID"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonvitinh "
        sql &= " where convert(datetime,convert(nvarchar, Ngaythang,103),103) between @TuNgay and @DenNgay and IDKhachHang =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select NULL AS STT,  SP = ('XK' + PHIEUXUATKHO.Sophieu),((PHIEUXUATKHO.TienTruocThue+PHIEUXUATKHO.TienThue)*PHIEUXUATKHO.TyGia)TongTien, NgaythangVS = ngaythang, Noidung, Model='0', TENDONVITINH.Ten, Soluong"
        sql &= " , Dongia = Dongia * tygia, Tienhang = (Soluong * Dongia * Tygia),   Tienthue = (Soluong * Dongia * Tygia* Mucthue /100 * Xuatthue)"
        sql &= " , PSNo = 0, PSco = 0,NULL AS LyKe,tygia, IDs=XUATKHOAUX.ID "
        sql &= " from PHIEUXUATKHO inner join XUATKHOAUX on PHIEUXUATKHO.Sophieu = XUATKHOAUX.Sophieu"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=XUATKHOAUX.DonVi"
        sql &= "  where convert(datetime,convert(nvarchar, Ngaythang,103),103) between @TuNgay and @DenNgay and IDKhachHang =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select  NULL AS STT, SP = ('CK' + Sophieu),NULL AS TongTien, NgaythangVS , Diengiai, Model='0',Donvi=NULL, soluong=0,Dongia =0, Tienhang = 0, Tienthue=0"
        sql &= "  ,PSNo = 0, PSCo=Sotien,NULL AS LyKe, tygia =1,IDs= ID From THUNH"
        sql &= "  where Ngaythangvs between @TuNgay and @DenNgay AND mucdich in(100,105) and IDKh =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select NULL AS STT, SP = ('THU' + Sophieu),NULL AS TongTien, NgaythangVS, Diengiai, Model='0',Donvi=NULL, soluong=0,Dongia =0, Tienhang = 0, Tienthue=0"
        sql &= "  ,PSNo = 0, PSCo=Sotien,NULL AS LyKe, tygia =1,IDs= ID From THU"
        sql &= "  where NgaythangVS between @TuNgay and @DenNgay AND mucdich in(100,105) and IDKh =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select NULL AS STT, SP = ('UNC' + Sophieu),NULL AS TongTien, NgaythangVS=Ngaythang, Diengiai, Model='0',Donvi=NULL, soluong=0,Dongia =0, Tienhang = 0, Tienthue=0"
        sql &= "  ,PSNo = Sotien, PSCo=0,NULL AS LyKe, tygia =1,IDs= ID From UNC"
        sql &= "  where Ngaythang between @TuNgay and @DenNgay AND mucdich =230 and IDKh =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select NULL AS STT, SP = ('CHI' + Sophieu),NULL AS TongTien, NgaythangVS, Diengiai, Model='0',Donvi=NULL, soluong=0,Dongia =0, Tienhang = 0, Tienthue=0"
        sql &= "  ,PSNo = Sotien, PSCo=0,NULL AS LyKe, tygia =1,IDs= ID From CHI"
        sql &= "  where NgaythangVS between @TuNgay and @DenNgay AND mucdich in (230,246) and IDKh =  @IDKH"
        sql &= "  order by ngaythangvs, sp, ids"

        AddParameter("@IDKH", btfilterMaKH.EditValue)
        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        AddParameter("@NgayChotCuoiKyXem", btNgayChotCuoiKyXem.EditValue)
        AddParameter("@NgayChotNoDauKy", btNgayChotNoDauKy.EditValue)

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim stt As Integer = 1
            Dim stt2 As Integer = 1
            ThuLuyKe = 0
            ThuPSNo = 0
            ThuPSCo = 0
            tb.Rows(0)("STT") = stt
            Dim tb2 As DataTable = tb.Clone
            Dim r As DataRow = tb2.NewRow

            r("STT") = tb.Rows(0)("STT")
            r("SP") = tb.Rows(0)("SP")
            r("TongTien") = tb.Rows(0)("TongTien")
            r("NgayThangVS") = tb.Rows(0)("NgayThangVS")
            r("Ten") = tb.Rows(0)("Ten")
            r("Model") = tb.Rows(0)("Model")
            r("DVT") = tb.Rows(0)("DVT")
            r("SoLuong") = tb.Rows(0)("SoLuong")
            r("DonGia") = tb.Rows(0)("DonGia")
            r("TienHang") = tb.Rows(0)("TienHang")
            r("TienThue") = tb.Rows(0)("TienThue")
            r("PSNo") = tb.Rows(0)("PSNo")
            r("PSCo") = tb.Rows(0)("PSCo")
            r("LuyKe") = tb.Rows(0)("LuyKe")
            ThuLuyKe = tb.Rows(0)("LuyKe")
            r("TyGia") = tb.Rows(0)("TyGia")
            r("IDs") = tb.Rows(0)("IDs")
            If Not IsDBNull(tb.Rows(0)("PSNo")) Then ThuPSNo += tb.Rows(0)("PSNo")
            If Not IsDBNull(tb.Rows(0)("PSCo")) Then ThuPSCo += tb.Rows(0)("PSCo")
            tb2.Rows.Add(r)
            Dim _SP As String = ""
            For i As Integer = 1 To tb.Rows.Count - 1
                If tb.Rows(i)("SP").ToString.Substring(0, 2) = "XK" Then
                    If tb.Rows(i)("SP").ToString <> tb.Rows(i - 1)("SP").ToString Then
                        Dim r2 As DataRow = tb2.NewRow
                        stt += 1
                        stt2 = 1
                        r2("STT") = stt
                        r2("SP") = tb.Rows(i)("SP")
                        r2("NgayThangVS") = tb.Rows(i)("NgayThangVS")
                        r2("PSNo") = tb.Rows(i)("TongTien")
                        ThuLuyKe += tb.Rows(i)("TongTien")
                        r2("LuyKe") = ThuLuyKe
                        r2("IDs") = tb.Rows(i)("IDs")
                        tb2.Rows.Add(r2)
                        If Not IsDBNull(tb.Rows(i)("PSNo")) Then ThuPSNo += tb.Rows(i)("PSNo")

                        Dim r3 As DataRow = tb2.NewRow

                        r3("SP") = stt2
                        stt2 += 1
                        r3("Ten") = tb.Rows(i)("Ten")
                        r3("Model") = tb.Rows(i)("Model")
                        r3("DVT") = tb.Rows(i)("DVT")
                        r3("SoLuong") = tb.Rows(i)("SoLuong")
                        r3("DonGia") = tb.Rows(i)("DonGia")
                        r3("TienHang") = tb.Rows(i)("TienHang")
                        r3("TienThue") = tb.Rows(i)("TienThue")

                        r3("IDs") = tb.Rows(i)("IDs")
                        tb2.Rows.Add(r3)
                    Else
                        Dim r2 As DataRow = tb2.NewRow

                        r2("SP") = stt2
                        stt2 += 1
                        r2("Ten") = tb.Rows(i)("Ten")
                        r2("Model") = tb.Rows(i)("Model")
                        r2("DVT") = tb.Rows(i)("DVT")
                        r2("SoLuong") = tb.Rows(i)("SoLuong")
                        r2("DonGia") = tb.Rows(i)("DonGia")
                        r2("TienHang") = tb.Rows(i)("TienHang")
                        r2("TienThue") = tb.Rows(i)("TienThue")

                        r2("IDs") = tb.Rows(i)("IDs")
                        tb2.Rows.Add(r2)
                    End If

                Else
                    Dim r2 As DataRow = tb2.NewRow
                    stt += 1
                    r2("STT") = stt
                    r2("SP") = tb.Rows(i)("SP")
                    r2("NgayThangVS") = tb.Rows(i)("NgayThangVS")
                    r2("Ten") = tb.Rows(i)("Ten")
                    If tb.Rows(i)("PSNo") <> 0 Then r2("PSNo") = tb.Rows(i)("PSNo")
                    If tb.Rows(i)("PSCo") <> 0 Then r2("PSCo") = tb.Rows(i)("PSCo")
                    ThuLuyKe += tb.Rows(i)("PSNo") - tb.Rows(i)("PSCo")
                    r2("LuyKe") = ThuLuyKe
                    r2("IDs") = tb.Rows(i)("IDs")
                    If Not IsDBNull(tb.Rows(i)("PSNo")) Then ThuPSNo += tb.Rows(i)("PSNo")
                    If Not IsDBNull(tb.Rows(i)("PSCo")) Then ThuPSCo += tb.Rows(i)("PSCo")
                    tb2.Rows.Add(r2)
                End If




                'If Not IsDBNull(tb.Rows(i)("PSNo")) And Not IsDBNull(tb.Rows(i)("PSCo")) And tb.Rows(i)("PSNo") <> 0 And tb.Rows(i)("PSCo") <> 0 Then
                '    tb.Rows(i)("LuyKe") = tb.Rows(i - 1)("LuyKe") + tb.Rows(i)("PSNo") - tb.Rows(i)("PSCo")
                'End If
            Next

            gdvCTNoPhaiThu.DataSource = tb2

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadChiTietPhaiTra()

        ' gdvPhaiTraCT.ClearColumnsFilter()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "

        sql &= " SELECT 1 AS STT, NULL AS SP,NULL AS TongTien,NULL as NgayThangVS,N'Đầu kỳ: Ứng trước/ phải thu' AS Ten,"
        sql &= " NULL AS Model,NULL AS DVT,NULL AS SoLuong,NULL AS DonGia,NULL AS TienHang,NULL AS TienThue,"
        sql &= " (UDK+UngTruoc) As PSNo,(NDK+PhaiThu) AS PSCo,(NDK+PhaiThu-UDK-UngTruoc)LuyKe,TyGia,IDs"
        sql &= " FROM ("
        sql &= " Select NDK = (Select isnull(sum(nodauky),0) from NOPHAITRA where IDKh = @IDKH and month = '" & Convert.ToDateTime(btNgayChotNoDauKy.EditValue).ToString("MM/yyyy") & "')"
        sql &= " ,UDK = (Select isnull(sum(Ungdauky),0) from NOPHAITRA where IDKh = @IDKH and month = '" & Convert.ToDateTime(btNgayChotNoDauKy.EditValue).ToString("MM/yyyy") & "')"
        sql &= " ,UngTruoc =((select isnull(sum(sotien),0) from CHI where IDKh = @IDKH and Mucdich in (210,229,239) and Tamung =1  and convert(datetime,Convert(nvarchar, NgaythangVS,103),103) between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  + (select isnull(sum(sotien),0) from UNC where IDKh = @IDKH and Mucdich in (210,229,239) and Tamung = 1 and convert(datetime,Convert(nvarchar, Ngaythang,103),103)  between @NgayChotNoDauKy and @NgayChotCuoiKyXem))"
        sql &= " ,Phaithu =((select isnull(sum((tientruocthue + tienthue)*tygia),0) from PHIEUNHAPKHO where IDKhachhang = @IDKH and convert(datetime,Convert(nvarchar, Ngaythang,103),103)  between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  - (select isnull(sum(sotien),0) from CHI where IDKh = @IDKH and Mucdich in (210,229,239) and Tamung =0  and NgaythangVS  between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  - (select isnull(sum(sotien),0) from UNC where IDKh = @IDKH and Mucdich in (210,229,239) and Tamung = 0 and Ngaythang  between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  + (select isnull(sum(sotien),0) from THU where IDKh = @IDKH and Mucdich in (106,109,113) and NgaythangVS  between @NgayChotNoDauKy and @NgayChotCuoiKyXem)"
        sql &= "  + (select isnull(sum(sotien),0) from THUNH where IDKh = @IDKH and Mucdich in(106,109) and NgaythangVS  between @NgayChotNoDauKy and @NgayChotCuoiKyXem))"
        sql &= " ,TyGia=1,IDs=0"
        sql &= " )tb"
        sql &= " UNION ALL"
        sql &= " Select NULL AS STT,  SP = ('NK' + PHIEUNHAPKHO.Sophieu),((PHIEUNHAPKHO.TienTruocThue+PHIEUNHAPKHO.TienThue)*PHIEUNHAPKHO.TyGia)TongTien, NgaythangVS = ngaythang, TENVATTU.Ten, VATTU.Model, TENDONVITINH.Ten AS DVT, NHAPKHO.Soluong"
        sql &= " , Dongia = Dongia * tygia, Tienhang = (Soluong * Dongia * Tygia),   Tienthue = (Soluong * Dongia * Tygia* Mucthue /100 * NhapThue)"
        sql &= " , PSNo = 0, PSco = 0,NULL AS LyKe, tygia, IDs=NHAPKHO.ID "
        sql &= " from PHIEUNHAPKHO inner join NHAPKHO on PHIEUNHAPKHO.Sophieu = NHAPKHO.Sophieu inner join VATTU on VATTU.ID = IDvattu inner join TENVATTU on VATTU.IDTenvattu = TENVATTU.ID"
        sql &= " LEFT JOIN TENDONVITINH ON TENDONVITINH.ID=VATTU.IDDonvitinh "
        sql &= " where convert(datetime,convert(nvarchar, Ngaythang,103),103) between @TuNgay and @DenNgay and IDKhachHang =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select  NULL AS STT, SP = ('UNC' + Sophieu),NULL AS TongTien, Ngaythang AS NgayThangVS , Diengiai, Model='0',Donvi=NULL, soluong=0,Dongia =0, Tienhang = 0, Tienthue=0"
        sql &= "  ,PSNo = SoTien, PSCo=0,NULL AS LyKe, tygia =1,IDs= ID From UNC"
        sql &= "  where Ngaythang between @TuNgay and @DenNgay AND mucdich in(210,229,239) and IDKh =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select NULL AS STT, SP = ('CHI' + Sophieu),NULL AS TongTien, NgaythangVS, Diengiai, Model='0',Donvi=NULL, soluong=0,Dongia =0, Tienhang = 0, Tienthue=0"
        sql &= "  ,PSNo = SoTien, PSCo=0,NULL AS LyKe, tygia =1,IDs= ID From CHI"
        sql &= "  where NgaythangVS between @TuNgay and @DenNgay AND mucdich in(210,229,239) and IDKh =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select NULL AS STT, SP = ('CK' + Sophieu),NULL AS TongTien, NgaythangVS, Diengiai, Model='0',Donvi=NULL, soluong=0,Dongia =0, Tienhang = 0, Tienthue=0"
        sql &= "  ,PSNo = 0, PSCo=SoTien,NULL AS LyKe, tygia =1,IDs= ID From THUNH"
        sql &= "  where NgaythangVS between @TuNgay and @DenNgay AND mucdich in(106,109) and IDKh =  @IDKH"
        sql &= "  Union All"
        sql &= "  Select NULL AS STT, SP = ('THU' + Sophieu),NULL AS TongTien, NgaythangVS, Diengiai, Model='0',Donvi=NULL, soluong=0,Dongia =0, Tienhang = 0, Tienthue=0"
        sql &= "  ,PSNo = 0, PSCo=SoTien,NULL AS LyKe, tygia =1,IDs= ID From THU"
        sql &= "  where NgaythangVS between @TuNgay and @DenNgay AND mucdich in(106,109,113) and IDKh =  @IDKH"
        sql &= "  order by ngaythangvs, sp, ids"

        AddParameter("@IDKH", btfilterMaKH.EditValue)
        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        AddParameter("@NgayChotCuoiKyXem", btNgayChotCuoiKyXem.EditValue)
        AddParameter("@NgayChotNoDauKy", btNgayChotNoDauKy.EditValue)

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim stt As Integer = 1
            Dim stt2 As Integer = 1
            ChiLuyKe = 0
            ChiPSNo = 0
            ChiPSCo = 0
            tb.Rows(0)("STT") = stt
            Dim tb2 As DataTable = tb.Clone
            Dim r As DataRow = tb2.NewRow

            r("STT") = tb.Rows(0)("STT")
            r("SP") = tb.Rows(0)("SP")
            r("TongTien") = tb.Rows(0)("TongTien")
            r("NgayThangVS") = tb.Rows(0)("NgayThangVS")
            r("Ten") = tb.Rows(0)("Ten")
            r("Model") = tb.Rows(0)("Model")
            r("DVT") = tb.Rows(0)("DVT")
            r("SoLuong") = tb.Rows(0)("SoLuong")
            r("DonGia") = tb.Rows(0)("DonGia")
            r("TienHang") = tb.Rows(0)("TienHang")
            r("TienThue") = tb.Rows(0)("TienThue")
            r("PSNo") = tb.Rows(0)("PSNo")
            r("PSCo") = tb.Rows(0)("PSCo")
            r("LuyKe") = tb.Rows(0)("LuyKe")
            ChiLuyKe = tb.Rows(0)("LuyKe")
            r("TyGia") = tb.Rows(0)("TyGia")
            r("IDs") = tb.Rows(0)("IDs")
            If Not IsDBNull(tb.Rows(0)("PSNo")) Then ChiPSNo += tb.Rows(0)("PSNo")
            If Not IsDBNull(tb.Rows(0)("PSCo")) Then ChiPSCo += tb.Rows(0)("PSCo")
            tb2.Rows.Add(r)
            Dim _SP As String = ""
            For i As Integer = 1 To tb.Rows.Count - 1
                If tb.Rows(i)("SP").ToString.Substring(0, 2) = "NK" Then
                    If tb.Rows(i)("SP").ToString <> tb.Rows(i - 1)("SP").ToString Then
                        Dim r2 As DataRow = tb2.NewRow
                        stt += 1
                        stt2 = 1
                        r2("STT") = stt
                        r2("SP") = tb.Rows(i)("SP")
                        r2("NgayThangVS") = tb.Rows(i)("NgayThangVS")
                        r2("PSCo") = tb.Rows(i)("TongTien")
                        ChiLuyKe += tb.Rows(i)("TongTien")
                        r2("LuyKe") = ChiLuyKe
                        r2("IDs") = tb.Rows(i)("IDs")
                        tb2.Rows.Add(r2)
                        If Not IsDBNull(tb.Rows(i)("PSCo")) Then ChiPSCo += tb.Rows(i)("PSCo")

                        Dim r3 As DataRow = tb2.NewRow

                        r3("SP") = stt2
                        stt2 += 1
                        r3("Ten") = tb.Rows(i)("Ten")
                        r3("Model") = tb.Rows(i)("Model")
                        r3("DVT") = tb.Rows(i)("DVT")
                        r3("SoLuong") = tb.Rows(i)("SoLuong")
                        r3("DonGia") = tb.Rows(i)("DonGia")
                        r3("TienHang") = tb.Rows(i)("TienHang")
                        r3("TienThue") = tb.Rows(i)("TienThue")

                        r3("IDs") = tb.Rows(i)("IDs")
                        tb2.Rows.Add(r3)
                    Else
                        Dim r2 As DataRow = tb2.NewRow

                        r2("SP") = stt2
                        stt2 += 1
                        r2("Ten") = tb.Rows(i)("Ten")
                        r2("Model") = tb.Rows(i)("Model")
                        r2("DVT") = tb.Rows(i)("DVT")
                        r2("SoLuong") = tb.Rows(i)("SoLuong")
                        r2("DonGia") = tb.Rows(i)("DonGia")
                        r2("TienHang") = tb.Rows(i)("TienHang")
                        r2("TienThue") = tb.Rows(i)("TienThue")

                        r2("IDs") = tb.Rows(i)("IDs")
                        tb2.Rows.Add(r2)
                    End If

                Else
                    Dim r2 As DataRow = tb2.NewRow
                    stt += 1
                    r2("STT") = stt
                    r2("SP") = tb.Rows(i)("SP")
                    r2("NgayThangVS") = tb.Rows(i)("NgayThangVS")
                    r2("Ten") = tb.Rows(i)("Ten")
                    If tb.Rows(i)("PSNo") <> 0 Then r2("PSNo") = tb.Rows(i)("PSNo")
                    If tb.Rows(i)("PSCo") <> 0 Then r2("PSCo") = tb.Rows(i)("PSCo")
                    ChiLuyKe += tb.Rows(i)("PSCo") - tb.Rows(i)("PSNo")
                    r2("LuyKe") = ChiLuyKe
                    r2("IDs") = tb.Rows(i)("IDs")
                    If Not IsDBNull(tb.Rows(i)("PSNo")) Then ChiPSNo += tb.Rows(i)("PSNo")
                    If Not IsDBNull(tb.Rows(i)("PSCo")) Then ChiPSCo += tb.Rows(i)("PSCo")
                    tb2.Rows.Add(r2)
                End If




                'If Not IsDBNull(tb.Rows(i)("PSNo")) And Not IsDBNull(tb.Rows(i)("PSCo")) And tb.Rows(i)("PSNo") <> 0 And tb.Rows(i)("PSCo") <> 0 Then
                '    tb.Rows(i)("LuyKe") = tb.Rows(i - 1)("LuyKe") + tb.Rows(i)("PSNo") - tb.Rows(i)("PSCo")
                'End If
            Next

            gdvCTNoPhaiTra.DataSource = tb2

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        If tabCongNo.SelectedTabPage Is tabPhaiTra Then
            LoadPhaiTra()
        ElseIf tabCongNo.SelectedTabPage Is tabPhaiThu Then
            LoadPhaiThu()
        ElseIf tabCongNo.SelectedTabPage Is tabChiTietPhaiThu Then
            LoadChiTietPhaiThu()
        Else
            LoadChiTietPhaiTra()
        End If

    End Sub


    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            saveFile.FileName = "No Phai Thu " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"
        Else
            saveFile.FileName = "No Phai Tra " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"
        End If

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                If tabCongNo.SelectedTabPage Is tabPhaiThu Then
                    Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvPhaiThuCT, False)
                Else
                    '         Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvPhaiTraCT, False)
                End If

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

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub

    'Private Sub gdvPhaiThuCT_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvPhaiThuCT.RowStyle
    '    If gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") > 0 Then
    '        e.Appearance.BackColor = Color.Red
    '    End If
    'End Sub

    Private Sub gdvPhaiThuCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiThuCT.RowCellStyle
        On Error Resume Next
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            If e.Column.FieldName = "ConNo" Then
                If gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") > 0 Then
                    e.Appearance.BackColor = Color.Red
                ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") < 0 Then
                    e.Appearance.BackColor = Color.Cyan
                ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") = 0 Then
                    e.Appearance.BackColor = Color.Yellow
                End If
            End If
        Else
            If e.Column.FieldName = "ConNo" Then
                'If gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "NgayQH") > 0 Then
                '    e.Appearance.BackColor = Color.Red
                'ElseIf gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") < 0 Then
                '    e.Appearance.BackColor = Color.Cyan
                'ElseIf gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiTraCT.GetRowCellValue(e.RowHandle, "NgayQH") = 0 Then
                '    e.Appearance.BackColor = Color.Yellow
                'End If
            End If
        End If


    End Sub

    Private Sub btLocDo_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocDo.ItemClick
        On Error Resume Next
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            gdvPhaiThuCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As New ColumnFilterInfoCollection
            Dim BinaryFilter As New CriteriaOperatorCollection
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Greater)))
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("NgayQH", 0, BinaryOperatorType.Greater)))

            FilterString = BinaryFilter.ToString()
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(0).ToString, "Còn nợ >0"))
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(1).ToString, "Đã quá hạn"))
            gdvPhaiThuCT.Columns("ConNo").FilterInfo = FilterConNo(0)
            gdvPhaiThuCT.Columns("NgayQH").FilterInfo = FilterConNo(1)
        Else
            'gdvPhaiTraCT.ClearColumnsFilter()
            'Dim FilterString As String
            'Dim FilterConNo As New ColumnFilterInfoCollection
            'Dim BinaryFilter As New CriteriaOperatorCollection
            'BinaryFilter.Add(New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Greater)))
            'BinaryFilter.Add(New GroupOperator(New BinaryOperator("NgayQH", 0, BinaryOperatorType.Greater)))

            'FilterString = BinaryFilter.ToString()
            'FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(0).ToString, "Còn nợ >0"))
            'FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(1).ToString, "Đã quá hạn"))
            'gdvPhaiTraCT.Columns("ConNo").FilterInfo = FilterConNo(0)
            'gdvPhaiTraCT.Columns("NgayQH").FilterInfo = FilterConNo(1)
        End If


    End Sub

    Private Sub btLocVang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocVang.ItemClick
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            gdvPhaiThuCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As New ColumnFilterInfoCollection
            Dim BinaryFilter As New CriteriaOperatorCollection
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Greater)))
            BinaryFilter.Add(New GroupOperator(New BinaryOperator("NgayQH", 0, BinaryOperatorType.Equal)))

            FilterString = BinaryFilter.ToString()
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(0).ToString, "Còn nợ >0"))
            FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(1).ToString, "Đến hạn"))
            gdvPhaiThuCT.Columns("ConNo").FilterInfo = FilterConNo(0)
            gdvPhaiThuCT.Columns("NgayQH").FilterInfo = FilterConNo(1)
        Else
            'gdvPhaiTraCT.ClearColumnsFilter()
            'Dim FilterString As String
            'Dim FilterConNo As New ColumnFilterInfoCollection
            'Dim BinaryFilter As New CriteriaOperatorCollection
            'BinaryFilter.Add(New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Greater)))
            'BinaryFilter.Add(New GroupOperator(New BinaryOperator("NgayQH", 0, BinaryOperatorType.Equal)))

            'FilterString = BinaryFilter.ToString()
            'FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(0).ToString, "Còn nợ >0"))
            'FilterConNo.Add(New ColumnFilterInfo(BinaryFilter(1).ToString, "Đến hạn"))
            'gdvPhaiTraCT.Columns("ConNo").FilterInfo = FilterConNo(0)
            'gdvPhaiTraCT.Columns("NgayQH").FilterInfo = FilterConNo(1)
        End If

    End Sub

    Private Sub btLocXanh_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLocXanh.ItemClick
        If tabCongNo.SelectedTabPage Is tabPhaiThu Then
            gdvPhaiThuCT.ClearColumnsFilter()
            Dim FilterString As String
            Dim FilterConNo As ColumnFilterInfo
            Dim BinaryFilter As CriteriaOperator
            BinaryFilter = New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Less))

            FilterString = BinaryFilter.ToString()
            FilterConNo = New ColumnFilterInfo(FilterString, "Còn nợ <0")

            gdvPhaiThuCT.Columns("ConNo").FilterInfo = FilterConNo
        Else
            'gdvPhaiTraCT.ClearColumnsFilter()
            'Dim FilterString As String
            'Dim FilterConNo As ColumnFilterInfo
            'Dim BinaryFilter As CriteriaOperator
            'BinaryFilter = New GroupOperator(New BinaryOperator("ConNo", 0, BinaryOperatorType.Less))

            'FilterString = BinaryFilter.ToString()
            'FilterConNo = New ColumnFilterInfo(FilterString, "Còn nợ <0")

            'gdvPhaiTraCT.Columns("ConNo").FilterInfo = FilterConNo
        End If


    End Sub

    'Private Sub gdvPhaiTraCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiTraCT.RowCellStyle
    '    If e.Column.FieldName = "ConNo" Then
    '        If gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") > 0 Then
    '            e.Appearance.BackColor = Color.Red
    '        ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") < 0 Then
    '            e.Appearance.BackColor = Color.Cyan
    '        ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "ConNo") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "NgayQH") = 0 Then
    '            e.Appearance.BackColor = Color.Yellow
    '        End If
    '    End If
    'End Sub

    'Private Sub tabCongNo_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabCongNo.SelectedPageChanged
    '    Select Case e.Page.Name
    '        Case tabPhaiThu.Name
    '            Bar2.Visible = True
    '            btfilterMaKH.Visibility = XtraBars.BarItemVisibility.Never
    '            btNgayChotCuoiKyXem.Visibility = XtraBars.BarItemVisibility.Always
    '            btNgayChotNoDauKy.Visibility = XtraBars.BarItemVisibility.Always
    '        Case tabPhaiTra.Name
    '            Bar2.Visible = True
    '            btfilterMaKH.Visibility = XtraBars.BarItemVisibility.Never
    '            btNgayChotCuoiKyXem.Visibility = XtraBars.BarItemVisibility.Always
    '            btNgayChotNoDauKy.Visibility = XtraBars.BarItemVisibility.Always
    '        Case tabChiTietPhaiThu.Name
    '            Bar2.Visible = False
    '            btfilterMaKH.Visibility = XtraBars.BarItemVisibility.Always
    '            btNgayChotCuoiKyXem.Visibility = XtraBars.BarItemVisibility.Never
    '            btNgayChotNoDauKy.Visibility = XtraBars.BarItemVisibility.Never
    '        Case tabChiTietPhaiTra.Name
    '            Bar2.Visible = False
    '            btfilterMaKH.Visibility = XtraBars.BarItemVisibility.Always
    '            btNgayChotCuoiKyXem.Visibility = XtraBars.BarItemVisibility.Never
    '            btNgayChotNoDauKy.Visibility = XtraBars.BarItemVisibility.Never
    '    End Select
    'End Sub

    Private Sub btfilterTuNgay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btfilterTuNgay.ItemClick
        btNgayChotCuoiKyXem.EditValue = DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(btfilterTuNgay.EditValue).Year, Convert.ToDateTime(btfilterTuNgay.EditValue).Month, 0))
    End Sub

    Private Sub gdvCTNoPhaiThuCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvCTNoPhaiThuCT.CustomSummaryCalculate
        If e.IsTotalSummary Then
            Select Case CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName
                Case "PSNo"
                    e.TotalValue = ThuPSNo
                Case "PSCo"
                    e.TotalValue = ThuPSCo
                Case "LuyKe"
                    e.TotalValue = ThuLuyKe
            End Select

        End If
    End Sub

    Private Sub gdvCTNoPhaiTraCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvCTNoPhaiTraCT.CustomSummaryCalculate
        If e.IsTotalSummary Then
            Select Case CType(e.Item, XtraGrid.GridColumnSummaryItem).FieldName
                Case "PSNo"
                    e.TotalValue = ChiPSNo
                Case "PSCo"
                    e.TotalValue = ChiPSCo
                Case "LuyKe"
                    e.TotalValue = ChiLuyKe
            End Select

        End If
    End Sub
End Class
