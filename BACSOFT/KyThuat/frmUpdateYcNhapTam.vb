Imports BACSOFT.Db.SqlHelper

Public Class frmUpdateYcNhapTam

    Public SoCG As String = ""


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For i As Integer = 0 To gdvVTCT.Columns.Count - 1
            gdvVTCT.Columns(i).OptionsColumn.ReadOnly = True
        Next

        gdvVTCT.Columns("SlYeuCau").OptionsColumn.ReadOnly = False

    End Sub

    Private Sub frmUpdateYcXuatTam_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        txtSoPhieuCG.Text = SoCG
        LoadDsVatTu()

        Dim tg = GetServerTime()
        If TrangThai.isAddNew Then
            txtNgayLap.EditValue = tg
            txtThoiGianCan.EditValue = tg
            txtNguoiLap.Tag = TaiKhoan
            txtNguoiLap.EditValue = NguoiDung
        Else
            btnInPhieu.Enabled = False
        End If

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            btnInPhieu.Enabled = False
        Else
            btnInPhieu.Enabled = True
        End If


        If gdvVTCT.RowCount = 0 Then
            btnCapNhat.Enabled = False
            btnInPhieu.Enabled = False
        End If

        If TrangThai.isUpdate Then
            Try
                Dim sql As String = "select SoYC,SoCG,ThoiGianLap,ThoiGianTra,GhiChu,IdNguoiLap, "
                sql &= "(select ten from nhansu where id = phieunhapkhotam.idnguoilap)TenNguoiLap "
                sql &= "from phieunhapkhotam where id = @Id "
                AddParameter("@Id", idPhieu)
                Dim dt As DataTable = ExecuteSQLDataTable(sql)
                txtSoPhieuCG.Text = dt.Rows(0)("SoCG").ToString
                txtSoPhieuXuatTam.Text = dt.Rows(0)("SoYC").ToString
                txtNgayLap.EditValue = dt.Rows(0)("ThoiGianLap")
                txtThoiGianCan.EditValue = dt.Rows(0)("ThoiGianTra")
                txtNguoiLap.Text = dt.Rows(0)("TenNguoiLap")
                txtNguoiLap.Tag = dt.Rows(0)("IdNguoiLap")
                txtGhiChu.Text = dt.Rows(0)("GhiChu").ToString
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try

        End If

    End Sub


    Private Sub LoadDsVatTu()

        Dim sql As String = ""
        sql &= " SELECT CHAOGIA.ID AS IDCG, CHAOGIA.IDvattu AS IDVatTu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo,"
        sql &= " TENDONVITINH.Ten AS TenDVT, "


        sql &= " (SELECT isnull(SUM(SlXuatKho),0) FROM XUATKHOTAM WHERE IdVatTu = CHAOGIA.IDvattu AND SoCG = @SP)SoLuong,  " 'xuất tạm

        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"




        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe,"
        sql &= " (select top 1 isnull(ngaythang,0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS NgayVe,"

        sql &= " convert(float,0)SlYeuCau, "


        If TrangThai.isUpdate Then
            sql &= " (SELECT isnull(SUM(SlYeuCau),0) FROM NHAPKHOTAM WHERE IdVatTu = CHAOGIA.IDvattu AND SoCG = @SP AND Id_Phieu <> @IdPhieu)SlDaYeuCau,  "
            AddParameter("@IdPhieu", idPhieu)
        Else
            sql &= " (SELECT isnull(SUM(SlYeuCau),0) FROM NHAPKHOTAM WHERE IdVatTu = CHAOGIA.IDvattu AND SoCG = @SP)SlDaYeuCau,  "
        End If

        'sql &= " (SELECT isnull(SUM(SlYeuCau),0) FROM XUATKHOTAM WHERE IdVatTu = CHAOGIA.IDvattu AND SoCG = @SP)SlCon,  "
        sql &= " CHAOGIA.SoLuong as SlCon,  " 'dự toán
 

        sql &= " ISNULL(CHAOGIA.AZ,0)AZ"
        sql &= " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=CHAOGIA.SoPhieu"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE CHAOGIA.Sophieu=@SP AND CHAOGIA.TrangThai = 2 "
        sql &= " ORDER BY AZ "

        AddParameter("@SP", SoCG)

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else

            dt.Columns.Add(New DataColumn("IdNhapKhoTam", Type.GetType("System.Int64")))

            If TrangThai.isUpdate Then
                sql = "select id,SoCG,IdVatTu,SlYeuCau from nhapkhotam where id_phieu = @Id"
                AddParameter("@Id", idPhieu)
                Dim dtX As DataTable = ExecuteSQLDataTable(sql)
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim arrRow = dtX.Select("IdVatTu=" & dt.Rows(i)("IDVatTu"))
                    If arrRow.Length > 0 Then
                        dt.Rows(i)("IdNhapKhoTam") = arrRow(0)("Id")
                        dt.Rows(i)("SlYeuCau") = arrRow(0)("SlYeuCau")
                    End If
                Next
                Dim rDelete = dt.Select("IdNhapKhoTam is null")
                While rDelete.Length > 0
                    dt.Rows.Remove(rDelete(0))
                    rDelete = dt.Select("IdNhapKhoTam is null")
                End While
            End If

            'For i As Integer = 0 To dt.Rows.Count - 1
            '    dt.Rows(i)("SlCon") = dt.Rows(i)("SoLuong") - dt.Rows(i)("SlDaYeuCau")
            'Next

        End If

        gdvVT.DataSource = dt

    End Sub


    Public idPhieu As Object

    Private Sub btnCapNhat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCapNhat.ItemClick

        gdvVTCT.CloseEditor()
        gdvVTCT.UpdateCurrentRow()

        'Try
        '    For i As Integer = 0 To gdvVTCT.RowCount - 1
        '        If gdvVTCT.GetRowCellValue(i, "SlYeuCau") > gdvVTCT.GetRowCellValue(i, "SlCon") Then
        '            ShowBaoLoi(gdvVTCT.GetRowCellValue(i, "TenVT") & " không thể yêu cầu số lượng lớn hơn lượng còn lại!")
        '            Exit Sub
        '        End If
        '    Next
        'Catch ex As Exception
        '    ShowBaoLoi(ex.Message)
        '    Exit Sub
        'End Try

        Try

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                txtSoPhieuXuatTam.EditValue = LaySoPhieuYeuCau()
            End If

            Dim tgNow As DateTime = GetServerTime()

            AddParameter("@SoYC", txtSoPhieuXuatTam.Text)
            AddParameter("@SoCG", txtSoPhieuCG.Text)
            AddParameter("@ThoiGianLap", txtNgayLap.EditValue)
            AddParameter("@IdNguoiLap", txtNguoiLap.Tag)
            AddParameter("@ThoiGianTra", txtThoiGianCan.EditValue)
            AddParameter("@GhiChu", txtGhiChu.Text)

            AddParameter("@IdNguoiSuaYC", TaiKhoan)
            AddParameter("@ThoiGianSuaYC", tgNow)

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                idPhieu = doInsert("PHIEUNHAPKHOTAM")
                If idPhieu Is Nothing Then Throw New Exception(LoiNgoaiLe)
                TrangThai.isUpdate = True
            Else
                AddParameterWhere("@Id", idPhieu)
                If doUpdate("PHIEUNHAPKHOTAM", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            For i As Integer = 0 To gdvVTCT.RowCount - 1
                If gdvVTCT.GetRowCellValue(i, "SlYeuCau") = 0 Then
                    If gdvVTCT.GetRowCellValue(i, "IdNhapKhoTam") Is DBNull.Value Then
                        Continue For
                    Else
                        AddParameterWhere("@Id", gdvVTCT.GetRowCellValue(i, "IdNhapKhoTam"))
                        If doDelete("NHAPKHOTAM", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                Else
                    AddParameter("@Id_Phieu", idPhieu)
                    AddParameter("@SoCG", txtSoPhieuCG.EditValue)
                    AddParameter("@IdVatTu", gdvVTCT.GetRowCellValue(i, "IDVatTu"))
                    AddParameter("@SlYeuCau", gdvVTCT.GetRowCellValue(i, "SlYeuCau"))
                    If gdvVTCT.GetRowCellValue(i, "IdNhapKhoTam") Is DBNull.Value Then
                        Dim idXKTam As Object = doInsert("NHAPKHOTAM")
                        If idXKTam Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        gdvVTCT.SetRowCellValue(i, "IdNhapKhoTam", idXKTam)
                    Else
                        AddParameterWhere("@Id", gdvVTCT.GetRowCellValue(i, "IdNhapKhoTam"))
                        If doUpdate("NHAPKHOTAM", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                End If
            Next

            ShowAlert("Cập nhật dữ liệu thành công!")

            btnInPhieu.Enabled = True
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

    Public Function LaySoPhieuYeuCau() As Object
        Dim sql As String = ""
        sql &= " DECLARE @DoDai AS NVARCHAR(5) "
        sql &= " SET @DoDai = N'00000'"
        sql &= " DECLARE @SP AS INT"
        sql &= " DECLARE @SoYC AS CHAR(9)"
        sql &= " DECLARE @Thang AS NVARCHAR(2)"
        sql &= " DECLARE @Nam AS NVARCHAR(2)"

        sql &= " SET @Thang = LEFT('00',2-LEN(CONVERT(NVARCHAR,DATEPART(mm,getdate())))) +  CONVERT(NVARCHAR,DATEPART(mm,getdate()))"
        sql &= " SET @Nam = LEFT('00',2-LEN(RIGHT( CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2))) + RIGHT(CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2)"
        sql &= " SET @SP = (SELECT ISNULL(MAX(CONVERT(INT,ISNULL(RIGHT(LEFT(SoYC,9),5),0))),0)+1  "
        sql &= "            FROM PHIEUNHAPKHOTAM "
        sql &= "            WHERE"
        sql &= " 	       LEFT(SoYC,2)=@Nam AND "
        sql &= " 	       SUBSTRING(SoYC,3,2)=@Thang AND LEN(SoYC)>=9 "
        sql &= "            )"

        sql &= " SET @SoYC = @Nam +  @Thang  + CONVERT(NVARCHAR,LEFT(@DoDai,LEN(@DoDai)-LEN(@SP))) + CONVERT(NVARCHAR,@SP)"
        sql &= " SELECT @SoYC"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            ShowBaoLoi("Không lấy được số phiếu !")
            Return Nothing
        End If
        Return tb.Rows(0)(0)
    End Function

    Private Sub btnDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDong.ItemClick
        Me.Close()
    End Sub

    Private Sub btnInPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnInPhieu.ItemClick
        InPhieuXuaTam(idPhieu)
    End Sub



    Public Shared Sub InPhieuXuaTam(id As Object)
        Try


            Dim f As New frmIn("In phiếu yêu cầu nhập kho tạm")
            Dim rpt As New rptPhieuXuatKhoTam
            rpt.pLogo.Image = My.Resources.Logo3


            Dim sql As String = ""
            sql &= "select SoYC as SoPhieu,SoCG,ThoiGianTra as NgayXuat,GhiChu, "
            sql &= "(select ttcMa from khachhang where id = (SELECT IDKhachHang from bangchaogia where sophieu = phieunhapkhotam.socg))MaKH  "
            sql &= "from phieunhapkhotam where id = " & id & " ; "

            sql &= "select IdVatTu,SlYeuCau as SoLuong,TENVATTU.Ten as TenVT, "
            sql &= "TENHANGSANXUAT.Ten as TenHang, VATTU.Model,VATTU.Thongso AS ThongSo, "
            sql &= "TENDONVITINH.Ten AS TenDVT "
            sql &= "from nhapkhotam "
            sql &= "LEFT OUTER JOIN VATTU ON NHAPKHOTAM.IdVatTu=VATTU.ID "
            sql &= "LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
            sql &= "LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID "
            sql &= "LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
            sql &= "where id_phieu =  " & id


            Dim ds As DataSet = ExecuteSQLDataSet(sql)

            If ds Is Nothing Then Throw New Exception(LoiNgoaiLe)


            ds.Tables(1).Columns.Add(New DataColumn("STT", Type.GetType("System.Int32")))
            For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                ds.Tables(1).Rows(i)("STT") = i + 1
            Next

            rpt.lbTieuDe.Text = "PHIẾU YÊU CẦU NHẬP KHO TẠM"

            If ds.Tables(0).Rows(0)("GhiChu").ToString.Trim <> "" Then
                ds.Tables(0).Rows(0)("GhiChu") = "(* Ghi chú: " & ds.Tables(0).Rows(0)("GhiChu") & ")"
            End If

            rpt.DataSource = ds


            rpt.RequestParameters = False
            rpt.CreateDocument()

            f.printControl.PrintingSystem = rpt.PrintingSystem

            f.Show()
        Catch ex As Exception

            ShowBaoLoi(ex.Message)
        End Try

    End Sub



End Class