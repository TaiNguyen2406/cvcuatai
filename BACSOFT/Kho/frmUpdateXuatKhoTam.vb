Imports BACSOFT.Db.SqlHelper

Public Class frmUpdateXuatKhoTam

    Public IdPhieu As Object



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For i As Integer = 0 To gdvVTCT.Columns.Count - 1
            gdvVTCT.Columns(i).OptionsColumn.ReadOnly = True
        Next

        gdvVTCT.Columns("SlXuatKho").OptionsColumn.ReadOnly = False

    End Sub

    Private Sub frmUpdateYcXuatTam_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim sql As String = "SELECT SoCG,SoPhieu,SoYC,GhiChuXK,IdNguoiXuat,ThoiGianXuat, "
        sql &= "(select ten from nhansu where id = phieuxuatkhotam.IdNguoiXuat)TenNguoiXuat  "
        sql &= "FROM PHIEUXUATKHOTAM WHERE ID = @ID "
        AddParameter("@ID", IdPhieu)

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Me.Close()
        End If

        txtSoPhieuYC.Text = dt.Rows(0)("SoYC")
        SoCG = dt.Rows(0)("SoCG")

        If TrangThai.isAddNew Then
            txtNguoiLap.Tag = TaiKhoan
            txtNguoiLap.EditValue = NguoiDung
            txtNgayLap.EditValue = GetServerTime()
        Else
            txtNguoiLap.Tag = dt.Rows(0)("IdNguoiXuat")
            txtNguoiLap.EditValue = dt.Rows(0)("TenNguoiXuat")
            txtSoPhieuXuatTam.EditValue = dt.Rows(0)("SoPhieu")
            txtGhiChu.EditValue = dt.Rows(0)("GhiChuXK")
            txtNgayLap.EditValue = dt.Rows(0)("ThoiGianXuat")
        End If

        LoadDsVatTu()

        If TrangThai.isAddNew Or TrangThai.isCopy Then
            btnInPhieu.Enabled = False
        Else
            btnInPhieu.Enabled = True
        End If


        If gdvVTCT.RowCount = 0 Then
            btnCapNhat.Enabled = False
            btnInPhieu.Enabled = False
        End If

    End Sub

    Private SoCG As String = ""

    Private Sub LoadDsVatTu()


        Dim sql As String = ""


        sql &= " SELECT XUATKHOTAM.ID as IdXuatKhoTam, XUATKHOTAM.IDVatTu, "
        sql &= " TENVATTU.Ten AS TenVT,  "
        sql &= " TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.Thongso AS ThongSo, "
        sql &= " TENDONVITINH.Ten AS TenDVT, "
        sql &= " XUATKHOTAM.SlYeuCau,ChaoGia.SoLuong as SlCG, "
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon, "
        sql &= " (select isnull(SUM(sLuong),0) from V_Dangve where IDVattu= CHAOGIA.IDVattu) AS DangVe, "

        sql &= " isnull(XUATKHOTAM.SlXuatKho,XUATKHOTAM.SlYeuCau) SlXuatKho, "

        sql &= " ISNULL(CHAOGIA.AZ,0)AZ "
        sql &= " FROM XUATKHOTAM  "
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=XUATKHOTAM.SoCG "
        sql &= " LEFT OUTER JOIN VATTU ON XUATKHOTAM.IdVatTu=VATTU.ID "
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID "
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID "
        sql &= " LEFT OUTER JOIN CHAOGIA ON CHAOGIA.SoPhieu=XUATKHOTAM.SoCG AND CHAOGIA.IdVatTu = XUATKHOTAM.IdVatTu "
        sql &= " where id_phieu = @Id_Phieu "
        sql &= " ORDER BY AZ "

        AddParameter("@Id_Phieu", IdPhieu)


        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If

        gdvVT.DataSource = dt


    End Sub



    Private Sub btnCapNhat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCapNhat.ItemClick

        Try

            gdvVTCT.CloseEditor()
            gdvVTCT.UpdateCurrentRow()

            For i As Integer = 0 To gdvVTCT.RowCount - 1
                If gdvVTCT.GetRowCellValue(i, "SlXuatKho") Is DBNull.Value Then
                    ShowCanhBao(gdvVTCT.GetRowCellValue(i, "TenVT") & " chưa nhập số lượng xuất kho tạm!")
                    Exit Sub
                End If
                If gdvVTCT.GetRowCellValue(i, "SlXuatKho") < 0 Then
                    ShowCanhBao(gdvVTCT.GetRowCellValue(i, "TenVT") & " không thể nhập số âm!")
                    Exit Sub
                End If
                'If gdvVTCT.GetRowCellValue(i, "SlXuatKho") > gdvVTCT.GetRowCellValue(i, "SlYeuCau") Then
                '    ShowCanhBao(gdvVTCT.GetRowCellValue(i, "TenVT") & " không thể nhập số xuất lớn hơn yêu cầu!")
                '    Exit Sub
                'End If
            Next

            If TrangThai.isAddNew Or TrangThai.isCopy Then
                txtSoPhieuXuatTam.EditValue = LaySoPhieuXuatTam()
            End If

            TrangThai.isUpdate = True

            AddParameter("@SoPhieu", txtSoPhieuXuatTam.Text)
            AddParameter("@ThoiGianXuat", txtNgayLap.EditValue)
            AddParameter("@IdNguoiXuat", txtNguoiLap.Tag)
            AddParameter("@GhiChuXK", txtGhiChu.Text)

            AddParameterWhere("@Id", IdPhieu)
            If doUpdate("PHIEUXUATKHOTAM", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)

            For i As Integer = 0 To gdvVTCT.RowCount - 1
                AddParameter("@SlXuatKho", gdvVTCT.GetRowCellValue(i, "SlXuatKho"))
                AddParameter("@SlYeuCau", gdvVTCT.GetRowCellValue(i, "SlXuatKho"))
                AddParameterWhere("@Id", gdvVTCT.GetRowCellValue(i, "IdXuatKhoTam"))
                If doUpdate("XUATKHOTAM", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next

            ShowAlert("Cập nhật dữ liệu thành công!")

            btnInPhieu.Enabled = True

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

    Public Function LaySoPhieuXuatTam() As Object
        Dim sql As String = ""
        sql &= " DECLARE @DoDai AS NVARCHAR(5) "
        sql &= " SET @DoDai = N'00000'"
        sql &= " DECLARE @SP AS INT"
        sql &= " DECLARE @SoPhieu AS CHAR(9)"
        sql &= " DECLARE @Thang AS NVARCHAR(2)"
        sql &= " DECLARE @Nam AS NVARCHAR(2)"

        sql &= " SET @Thang = LEFT('00',2-LEN(CONVERT(NVARCHAR,DATEPART(mm,getdate())))) +  CONVERT(NVARCHAR,DATEPART(mm,getdate()))"
        sql &= " SET @Nam = LEFT('00',2-LEN(RIGHT( CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2))) + RIGHT(CONVERT(NVARCHAR,DATEPART(yyyy,getdate())),2)"
        sql &= " SET @SP = (SELECT ISNULL(MAX(CONVERT(INT,ISNULL(RIGHT(LEFT(SoPhieu,9),5),0))),0)+1  "
        sql &= "            FROM PHIEUXUATKHOTAM "
        sql &= "            WHERE"
        sql &= " 	       LEFT(SoPhieu,2)=@Nam AND "
        sql &= " 	       SUBSTRING(SoPhieu,3,2)=@Thang AND LEN(SoPhieu)>=9 "
        sql &= "            )"

        sql &= " SET @SoPhieu = @Nam +  @Thang  + CONVERT(NVARCHAR,LEFT(@DoDai,LEN(@DoDai)-LEN(@SP))) + CONVERT(NVARCHAR,@SP)"
        sql &= " SELECT @SoPhieu"

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
        InPhieuXuaTam(IdPhieu)
    End Sub



    Public Shared Sub InPhieuXuaTam(id As Object)
        Try


            Dim f As New frmIn("In phiếu xuất kho tạm")
            Dim rpt As New rptPhieuXuatKhoTam
            rpt.pLogo.Image = My.Resources.Logo3


            Dim sql As String = ""
            sql &= "select SoPhieu,SoCG,ThoiGianCan as NgayXuat,GhiChuXK as GhiChu, "
            sql &= "(select ttcMa from khachhang where id = (SELECT IDKhachHang from bangchaogia where sophieu = phieuxuatkhotam.socg))MaKH  "
            sql &= "from phieuxuatkhotam where id = " & id & " ; "

            sql &= "select IdVatTu,SlXuatKho as SoLuong,TENVATTU.Ten as TenVT, "
            sql &= "TENHANGSANXUAT.Ten as TenHang, VATTU.Model,VATTU.Thongso AS ThongSo, "
            sql &= "TENDONVITINH.Ten AS TenDVT "
            sql &= "from xuatkhotam "
            sql &= "LEFT OUTER JOIN VATTU ON XUATKHOTAM.IdVatTu=VATTU.ID "
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

            rpt.lbTieuDe.Text = "PHIẾU XUẤT KHO TẠM"

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