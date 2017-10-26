Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraReports.UI

Namespace printFile
    Public Class CongTrinh
        Public Shared Sub BangKeVatTu(ByVal SoChaoGia As Object)
            Try
                ShowWaiting("Đang tải nội dung ...")
                Dim Sql As String = ""
                Sql &= " SELECT ('YC'+ ISNULL( BANGCHAOGIA.Masodathang,'') + ' CG'+ ISNULL(BANGCHAOGIA.Sophieu,''))SoCT,KHACHHANG.ttcMa,Congtrinh,TenDuAn,ISNULL(NS1.Ten,NS.Ten) AS NgXuLy, NS2.Ten AS NgDuyet"
                Sql &= " FROM BANGCHAOGIA INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
                Sql &= " LEFT JOIN NHANSU AS NS1 ON NS1.ID = BANGCHAOGIA.IDNgXuLy "
                Sql &= " LEFT JOIN NHANSU AS NS ON NS.ID = BANGCHAOGIA.IDUser "
                Sql &= " LEFT JOIN NHANSU AS NS2 ON NS2.ID = BANGCHAOGIA.IDNgDuyet "
                Sql &= " WHERE BANGCHAOGIA.Sophieu=@SoChaoGia"

                Sql &= " SELECT (SELECT 0)STT, TENVATTU.Ten As TenVT,TENDONVITINH.Ten AS TenDVT,VATTU.Model,TENHANGSANXUAT.Ten AS HangSX,SoLuong,NgayCan, "
                Sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon"
                Sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
                Sql &= "     LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
                Sql &= "     LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
                Sql &= "     LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
                Sql &= " WHERE CHAOGIA.Sophieu=@SoChaoGia"
                Sql &= " ORDER BY CHAOGIA.AZ"
                AddParameterWhere("@SoChaoGia", SoChaoGia)
                Dim ds As DataSet = ExecuteSQLDataSet(Sql)
                If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    ds.Tables(1).Rows(i)("STT") = i + 1
                Next

                Dim f As New frmIn("Bảng kê vật tư thi công")
                Dim rpt As New rptBangKeVatTu
                rpt.pLoGo.ImageUrl = Application.StartupPath & "\Logo.png"
                rpt.DataSource = ds
                rpt.RequestParameters = False
                rpt.CreateDocument()

                f.printControl.PrintingSystem = rpt.PrintingSystem
                CloseWaiting()
                f.ShowDialog()

            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End Sub


        Public Shared Sub KeHoachThiCong(ByVal SoChaoGia As Object, ByVal SoYeuCau As Object)
            Try
                ShowWaiting("Đang tải nội dung ...")
                Dim Sql As String = ""
                Sql &= " SELECT ('YC'+ ISNULL( BANGCHAOGIA.Masodathang,'') + ' CG'+ ISNULL(BANGCHAOGIA.Sophieu,''))SoCT,KHACHHANG.ttcMa,TenDuAn,NS1.Ten AS NgXuLy, NS2.Ten AS NgDuyet"
                Sql &= " FROM BANGCHAOGIA INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
                Sql &= " LEFT JOIN NHANSU AS NS1 ON NS1.ID = BANGCHAOGIA.IDNgXuLy "
                Sql &= " LEFT JOIN NHANSU AS NS2 ON NS2.ID = BANGCHAOGIA.IDNgDuyet "
                Sql &= " WHERE BANGCHAOGIA.Sophieu=@SoChaoGia"

                Sql &= " SELECT ('')NVThucHien,tblBaoCaoLichThiCong.*,tblTuDien.NoiDung FROM tblBaoCaoLichThiCong "
                Sql &= " LEFT OUTER JOIN tblTuDien ON tblTuDien.ID=tblBaoCaoLichThiCong.IDNoiDung AND tblTuDien.Loai= " & LoaiTuDien.NoiDungThiCong
                Sql &= " WHERE SoYC=@SoYC"
                Sql &= " ORDER BY AZ "
                AddParameterWhere("@SoChaoGia", SoChaoGia)
                AddParameterWhere("@SoYC", SoYeuCau)
                Dim ds As DataSet = ExecuteSQLDataSet(Sql)
                If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
                With ds.Tables(1)
                    For i As Integer = 0 To .Rows.Count - 1
                        .Rows(i)("AZ") = i + 1
                        Dim tb2 As DataTable = DataSourceDSFile(.Rows(i)("IDNgThucHien").ToString, , ",")
                        .Rows(i)("NVThucHien") = ""
                        For j As Integer = 0 To tb2.Rows.Count - 1
                            AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                            Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                            If Not tb3 Is Nothing Then
                                .Rows(i)("NVThucHien") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
                            End If
                        Next
                        .Rows(i)("NVThucHien") = .Rows(i)("NVThucHien").ToString.Trim
                    Next
                End With


                Dim f As New frmIn("Kế hoạch thi công")
                Dim rpt As New rptKeHoachThiCong
                rpt.pLoGo.ImageUrl = Application.StartupPath & "\Logo.png"
                rpt.DataSource = ds
                rpt.RequestParameters = False
                rpt.CreateDocument()

                f.printControl.PrintingSystem = rpt.PrintingSystem
                CloseWaiting()
                f.ShowDialog()

            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End Sub

        Public Shared Sub BangKeVatTuVaKeHoachThiCong(ByVal SoChaoGia As Object, ByVal SoYeuCau As Object)
            Try
                ShowWaiting("Đang tải nội dung ...")
                Dim mainRpt As New XtraReport
                mainRpt.CreateDocument()
                Dim Sql As String = ""
                Sql &= " SELECT ('YC'+ ISNULL( BANGCHAOGIA.Masodathang,'') + ' CG'+ ISNULL(BANGCHAOGIA.Sophieu,''))SoCT,KHACHHANG.ttcMa,TenDuAn,NS1.Ten AS NgXuLy, NS2.Ten AS NgDuyet"
                Sql &= " FROM BANGCHAOGIA INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
                Sql &= " LEFT JOIN NHANSU AS NS1 ON NS1.ID = BANGCHAOGIA.IDNgXuLy "
                Sql &= " LEFT JOIN NHANSU AS NS2 ON NS2.ID = BANGCHAOGIA.IDNgDuyet "
                Sql &= " WHERE BANGCHAOGIA.Sophieu=@SoChaoGia"

                Sql &= " SELECT (SELECT 0)STT, TENVATTU.Ten As TenVT,TENDONVITINH.Ten AS TenDVT,VATTU.Model,TENHANGSANXUAT.Ten AS HangSX,SoLuong,NgayCan, "
                Sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon"
                Sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
                Sql &= "     LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
                Sql &= "     LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
                Sql &= "     LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
                Sql &= " WHERE CHAOGIA.Sophieu=@SoChaoGia"
                Sql &= " ORDER BY CHAOGIA.AZ"
                AddParameterWhere("@SoChaoGia", SoChaoGia)
                Dim ds As DataSet = ExecuteSQLDataSet(Sql)
                If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)
                For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    ds.Tables(1).Rows(i)("STT") = i + 1
                Next

                Dim rpt As New rptBangKeVatTu
                rpt.pLoGo.ImageUrl = Application.StartupPath & "\Logo.png"
                rpt.DataSource = ds
                rpt.RequestParameters = False
                rpt.CreateDocument()
                mainRpt.Pages.AddRange(rpt.Pages)


                Sql = ""
                Sql &= " SELECT ('YC'+ ISNULL( BANGCHAOGIA.Masodathang,'') + ' CG'+ ISNULL(BANGCHAOGIA.Sophieu,''))SoCT,KHACHHANG.ttcMa,TenDuAn,NS1.Ten AS NgXuLy, NS2.Ten AS NgDuyet"
                Sql &= " FROM BANGCHAOGIA INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
                Sql &= " LEFT JOIN NHANSU AS NS1 ON NS1.ID = BANGCHAOGIA.IDNgXuLy "
                Sql &= " LEFT JOIN NHANSU AS NS2 ON NS2.ID = BANGCHAOGIA.IDNgDuyet "
                Sql &= " WHERE BANGCHAOGIA.Sophieu=@SoChaoGia"

                Sql &= " SELECT ('')NVThucHien,tblBaoCaoLichThiCong.*,tblTuDien.NoiDung FROM tblBaoCaoLichThiCong "
                Sql &= " LEFT OUTER JOIN tblTuDien ON tblTuDien.ID=tblBaoCaoLichThiCong.IDNoiDung AND tblTuDien.Loai= " & LoaiTuDien.NoiDungThiCong
                Sql &= " WHERE SoYC=@SoYC"
                Sql &= " ORDER BY AZ "
                AddParameterWhere("@SoChaoGia", SoChaoGia)
                AddParameterWhere("@SoYC", SoYeuCau)
                Dim ds1 As DataSet = ExecuteSQLDataSet(Sql)
                If ds1 Is Nothing Then Throw New Exception(LoiNgoaiLe)
                With ds.Tables(1)
                    For i As Integer = 0 To .Rows.Count - 1
                        .Rows(i)("AZ") = i + 1
                        Dim tb2 As DataTable = DataSourceDSFile(.Rows(i)("IDNgThucHien").ToString, , ",")
                        .Rows(i)("NVThucHien") = ""
                        For j As Integer = 0 To tb2.Rows.Count - 1
                            AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                            Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                            If Not tb3 Is Nothing Then
                                .Rows(i)("NVThucHien") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
                            End If
                        Next
                        .Rows(i)("NVThucHien") = .Rows(i)("NVThucHien").ToString.Trim
                    Next
                End With

                Dim rpt1 As New rptKeHoachThiCong
                rpt1.pLoGo.ImageUrl = Application.StartupPath & "\Logo.png"
                rpt1.DataSource = ds1
                rpt1.RequestParameters = False
                rpt1.CreateDocument()
                mainRpt.Pages.AddRange(rpt1.Pages)

                Dim f As New frmIn("Bảng kê vật tư và kế hoạch thi công")
                f.printControl.PrintingSystem = mainRpt.PrintingSystem
                CloseWaiting()
                f.ShowDialog()

            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End Sub

    End Class
End Namespace