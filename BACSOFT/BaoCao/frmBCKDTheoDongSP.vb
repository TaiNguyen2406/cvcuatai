Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraTreeList.Nodes
Imports System.Threading

Public Class frmBCKDTheoDongSP

    Public _DaChon As Integer = 0

    Private Sub frmBCKDTheoDongSP_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        txtNam.EditValue = tg.Year
        cmbHangSX.EditValue = "[Tùy chọn]"
        Dim t As New Threading.Thread(AddressOf LoadDanhSachTreeHangSX)
        t.IsBackground = True
        t.Start()
        'LoadDanhSachTreeHangSX()
    End Sub

#Region "-- Danh sách tree hãng sản xuất --"
    Private Sub LoadDanhSachTreeHangSX()
        CheckForIllegalCrossThreadCalls = False
        ' treeHang.Nodes.Clear()
        Dim sql As String = "SELECT ID,Convert(bit,0)Chon,TEN FROM TENHANGSANXUAT ORDER BY TEN; "
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        'Dim nodeTatCa As TreeListNode = treeHang.AppendNode(New Object() {-1, "[Tất cả]"}, 0)
        'For Each rowHang As DataRow In dt.Rows
        '    Dim nodeHang As TreeListNode = treeHang.AppendNode(New Object() {rowHang("ID"), rowHang("TEN")}, nodeTatCa)
        '    Select Case rowHang("ID")
        '        Case 1, 15, 70, 141, 265, 378
        '            nodeHang.Checked = True
        '    End Select
        'Next
        'treeHang.ExpandAll()


        gdvHangSX.DataSource = dt

        gdvHangSXCT.BeginUpdate()
        For i As Integer = 0 To gdvHangSXCT.RowCount - 1
            Select Case gdvHangSXCT.GetRowCellValue(i, "ID")
                Case 378, 15, 70, 265, 389, 141, 154, 3, 1
                    gdvHangSXCT.SetRowCellValue(i, "Chon", True)
            End Select
        Next
        gdvHangSXCT.EndUpdate()

    End Sub

    Private Sub treeNhanVien_AfterCheckNode(sender As System.Object, e As DevExpress.XtraTreeList.NodeEventArgs) Handles treeHang.AfterCheckNode
        If e.Node.Level = 0 Then
            For Each n As TreeListNode In e.Node.Nodes
                n.Checked = e.Node.Checked
            Next
        End If
        Dim kt As Boolean = True
        For Each nod As TreeListNode In treeHang.Nodes(0).Nodes
            If Not nod.Checked Then
                kt = False
                Exit For
            End If
        Next
        If kt Then
            cmbHangSX.EditValue = "[Tất cả]"
            treeHang.Nodes(0).Checked = True
        Else
            cmbHangSX.EditValue = "[Tùy chọn]"
            treeHang.Nodes(0).Checked = False
        End If
    End Sub

    Private Function LayIdDanhSachHangSX() As String
        Dim str As String = ""
        'For Each nod As TreeListNode In treeHang.Nodes(0).Nodes
        '    'For Each n As TreeListNode In nod.Nodes
        '    If nod.Checked Then
        '        str &= nod.GetValue(colID).ToString & ","
        '    End If
        '    'Next
        'Next

        gdvHangSXCT.CloseEditor()
        gdvHangSXCT.UpdateCurrentRow()
        For i As Integer = 0 To gdvHangSXCT.DataRowCount - 1
            If gdvHangSXCT.GetRowCellValue(i, "Chon") Then
                str &= gdvHangSXCT.GetRowCellValue(i, "ID") & ","
            End If
        Next

        If str = "" Then Return Nothing
        Return str.Substring(0, str.Length - 1)
    End Function
#End Region

#Region " -- Convert -- "
    Private Function tryToDouble(str As Object) As Double
        Try
            Return Convert.ToDouble(str)
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Function tryToDoubleX(str As Object, Optional isMauSo As Boolean = False) As Double
        Try
            If Not isMauSo Then
                Return Convert.ToInt32(str.Substring(0, str.IndexOf("/")))
            Else
                Return Convert.ToInt32(str.Substring(str.IndexOf("/") + 1))
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region

    Private Sub gdvData_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs)
        'If gdvData.GetRowCellValue(e.RowHandle, "cNhanVien") = -1 Then
        '    e.Appearance.Font = New Font(gdv.Font, FontStyle.Bold)
        '    e.Appearance.ForeColor = Color.DarkMagenta
        'End If
    End Sub



    Private Sub btDuyetBC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDuyetBC.ItemClick
        If LayIdDanhSachHangSX() Is Nothing Then
            ShowCanhBao("Chưa chọn hãng sản xuất nào!")
            Exit Sub
        End If
        ShowWaiting("Đang tải báo cáo ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " DECLARE @Nam as int"
        sql &= " SET @Nam=" & txtNam.EditValue

        sql &= " DECLARE @tbTieuChi as table"
        sql &= " ("
        sql &= " 	ID int,"
        sql &= " 	NoiDung Nvarchar(100)"
        sql &= " )"

        sql &= " INSERT INTO @tbTieuChi"
        sql &= " SELECT 1,N'Đặt hàng'"
        sql &= " UNION ALL"
        sql &= " SELECT 2,N'Nhập kho'"
        sql &= " UNION ALL"
        sql &= " SELECT 3,N'XN nhập'"
        sql &= " UNION ALL"
        sql &= " SELECT 4,N'XN bán'"
        sql &= " UNION ALL"
        sql &= " SELECT 5,N'XK nhập'"
        sql &= " UNION ALL"
        sql &= " SELECT 6,N'XK bán'"
        sql &= " UNION ALL"
        sql &= " SELECT 7,N'Tồn kho'"

        sql &= " DECLARE @tbtmp AS Table"
        sql &= " ("
        sql &= " 	IDHangSanXuat int,"
        sql &= " 	TenHang	nvarchar(250),"
        sql &= " 	IDTieuChi int,"
        sql &= " 	NoiDung	nvarchar(100),"
        sql &= " 	SoTien	float,"
        sql &= " 	Thang int"
        sql &= " )"

        sql &= "  DECLARE @tbNgayCuoiThang AS table"
        sql &= " ("
        sql &= "         Ngay DateTime"
        sql &= " )"

        sql &= " INSERT INTO @tbNgayCuoiThang"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/01/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/02/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/03/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/04/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/05/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/06/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/07/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/08/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/09/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/10/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/11/'+Convert(nvarchar,@Nam),103))+1,0))"
        sql &= " UNION ALL"
        sql &= " SELECT DATEADD(d,-1,DATEADD(mm, DATEDIFF(m,0,Convert(datetime,'01/12/'+Convert(nvarchar,@Nam),103))+1,0))"
        ' truy vấn chính
        sql &= " INSERT INTO @tbTmp"
        sql &= " SELECT TENHANGSANXUAT.ID,TENHANGSANXUAT.Ten,[@tbTieuChi].ID AS IDTieuChi,[@tbTieuChi].NoiDung,"
        sql &= " (CASE [@tbTieuChi].ID"
        sql &= " 	WHEN 1 THEN"
        sql &= " 	tbDatHang.SoTien/1000000"
        sql &= " 	WHEN 2 THEN"
        sql &= " 	tbNhapKho.SoTien/1000000"
        sql &= " 	WHEN 3 THEN"
        sql &= " 	tbChaoGiaNhap.SoTien/1000000"
        sql &= " 	WHEN 4 THEN"
        sql &= " 	tbChaoGiaBan.SoTien/1000000"
        sql &= " 	WHEN 5 THEN"
        sql &= " 	tbXuatKhoNhap.SoTien/1000000"
        sql &= " 	WHEN 6 THEN"
        sql &= " 	tbXuatKhoBan.SoTien/1000000"
        sql &= " 	WHEN 7 THEN"
        sql &= " 	tbTon.SoTien/1000000"
        sql &= " END)SoTien,"
        sql &= " (CASE [@tbTieuChi].ID"
        sql &= " 	WHEN 1 THEN"
        sql &= " 	tbDatHang.Thang"
        sql &= " 	WHEN 2 THEN"
        sql &= " 	tbNhapKho.Thang"
        sql &= " 	WHEN 3 THEN"
        sql &= " 	tbChaoGiaNhap.Thang"
        sql &= " 	WHEN 4 THEN"
        sql &= " 	tbChaoGiaBan.Thang"
        sql &= " 	WHEN 5 THEN"
        sql &= " 	tbXuatKhoNhap.Thang"
        sql &= " 	WHEN 6 THEN"
        sql &= " 	tbXuatKhoBan.Thang"
        sql &= " 	WHEN 7 THEN"
        sql &= " 	tbTon.Thang"
        sql &= " END)Thang"
        sql &= " FROM @tbTieuChi"
        sql &= " INNER JOIN TENHANGSANXUAT ON TENHANGSANXUAT.ID IN (" & LayIdDanhSachHangSX() & ")"
        ' Đặt hàng
        sql &= " LEFT JOIN "
        sql &= " (SELECT 1 AS IDTieuChi, SUM(DonGia*SoLuong*PHIEUDATHANG.TyGia)SoTien,VATTU.IDHangSanXuat,month(NGAYDAT) AS Thang FROM DATHANG"
        sql &= " INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieu AND PHIEUDATHANG.PheDuyet=1"
        sql &= " INNER JOIN VATTU ON DATHANG.IDVatTu=VATTU.ID"
        sql &= " WHERE Year(PHIEUDATHANG.NgayDat)=@Nam"
        sql &= " GROUP BY IDHangSanXuat,month(NGAYDAT))tbDatHang ON [@tbTieuChi].ID=tbDatHang.IDTieuChi AND TENHANGSANXUAT.ID=tbDatHang.IDHangSanXuat"
        ' Nhập kho
        sql &= " LEFT JOIN"
        sql &= " (SELECT 2 AS IDTieuChi, SUM(DonGia*SoLuong*PHIEUNHAPKHO.TyGia)SoTien,VATTU.IDHangSanXuat,Month(NgayThang) Thang FROM NHAPKHO"
        sql &= " INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu"
        sql &= " INNER JOIN VATTU ON NHAPKHO.IDVatTu=VATTU.ID"
        sql &= " WHERE Year(PHIEUNHAPKHO.NgayThang)=@Nam"
        sql &= " GROUP BY IDHangSanXuat,month(NgayThang))tbNhapKho ON [@tbTieuChi].ID=tbNhapKho.IDTieuChi AND TENHANGSANXUAT.ID=tbNhapKho.IDHangSanXuat"
        ' Chào giá xác nhận theo giá nhập
        sql &= " LEFT JOIN "
        sql &= " ("
        sql &= " SELECT 3 AS IDTieuChi, SUM(SoTIen)SoTien,IDHangSanXuat,Thang"
        sql &= " FROM("
        sql &= " SELECT "
        sql &= " ISNULL(ISNULL("
        sql &= " (SELECT     TOP (1) Gianhap"
        sql &= "     FROM V_GiaNhap "
        sql &= "     WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103)"
        sql &= "     ORDER BY Ngaythang DESC),"
        sql &= " (SELECT     TOP (1) Gianhap"
        sql &= "     FROM V_GiaNhap"
        sql &= "     WHERE IDvattu = CHAOGIA.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,BANGCHAOGIA.NgayNhan,103),103)"
        sql &= "          ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)) * CHAOGIA.SoLuong AS SoTien,"
        sql &= " VATTU.IDHangSanXuat,month(NgayNhan) AS Thang FROM CHAOGIA"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=CHAOGIA.SoPhieu AND BANGCHAOGIA.TrangThai=2"
        sql &= " INNER JOIN VATTU ON CHAOGIA.IDVatTu=VATTU.ID"
        sql &= " WHERE Year(BANGCHAOGIA.NgayNhan)=@Nam)tb"
        sql &= " GROUP BY IDHangSanXuat,Thang)tbChaoGiaNhap ON [@tbTieuChi].ID=tbChaoGiaNhap.IDTieuChi AND TENHANGSANXUAT.ID=tbChaoGiaNhap.IDHangSanXuat"
        ' Chào giá xác nhận theo giá bán
        sql &= " LEFT JOIN "
        sql &= " ("
        sql &= " SELECT 4 AS IDTieuChi, SUM(CHAOGIA.DonGia * CHAOGIA.SoLuong * BANGCHAOGIA.TyGia) AS SoTien,"
        sql &= " VATTU.IDHangSanXuat,month(NgayNhan)AS Thang "
        sql &= " FROM CHAOGIA"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=CHAOGIA.SoPhieu AND BANGCHAOGIA.TrangThai=2"
        sql &= " INNER JOIN VATTU ON CHAOGIA.IDVatTu=VATTU.ID"
        sql &= " WHERE Year(BANGCHAOGIA.NgayNhan)=@Nam"
        sql &= " GROUP BY IDHangSanXuat,month(NgayNhan))tbChaoGiaBan ON [@tbTieuChi].ID=tbChaoGiaBan.IDTieuChi AND TENHANGSANXUAT.ID=tbChaoGiaBan.IDHangSanXuat"
        ' Xuất kho theo giá nhập
        sql &= " LEFT JOIN"
        sql &= " ("
        sql &= " SELECT 5 AS IDTieuChi, SUM(SoTIen)SoTien,IDHangSanXuat,Thang"
        sql &= " FROM("
        sql &= " SELECT "
        sql &= " ISNULL(ISNULL("
        sql &= " (SELECT     TOP (1) Gianhap"
        sql &= "     FROM V_GiaNhap "
        sql &= "     WHERE IDvattu = XUATKHO.IDvattu AND Ngaythang <= Convert(datetime,Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103)"
        sql &= "     ORDER BY Ngaythang DESC),"
        sql &= " (SELECT     TOP (1) Gianhap"
        sql &= "     FROM V_GiaNhap"
        sql &= "     WHERE IDvattu = XUATKHO.IDvattu AND Ngaythang > Convert(datetime,Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103)"
        sql &= "          ORDER BY Ngaythang)),VATTU.DonGia1*(VATTU.GiaNhap1/100)) * XUATKHO.SoLuong AS SoTien,"
        sql &= " VATTU.IDHangSanXuat,month(NgayThang)AS Thang FROM XUATKHO"
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu "
        sql &= " INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID"
        sql &= " WHERE Year(PHIEUXUATKHO.NgayThang)=@Nam)tb"
        sql &= " GROUP BY IDHangSanXuat,Thang)tbXuatKhoNhap ON [@tbTieuChi].ID=tbXuatKhoNhap.IDTieuChi AND TENHANGSANXUAT.ID=tbXuatKhoNhap.IDHangSanXuat"
        ' Xuất kho theo giá bán"
        sql &= " LEFT JOIN"
        sql &= " ("
        sql &= " SELECT 6 AS IDTieuChi, SUM(XUATKHO.DonGia * XUATKHO.SoLuong * PHIEUXUATKHO.TyGia) AS SoTien,"
        sql &= " VATTU.IDHangSanXuat,month(NgayThang)AS Thang "
        sql &= " FROM XUATKHO"
        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
        sql &= " INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID"
        sql &= " WHERE Year(PHIEUXUATKHO.NgayThang)=@Nam"
        sql &= " GROUP BY IDHangSanXuat,month(NgayThang))tbXuatKhoBan ON [@tbTieuChi].ID=tbXuatKhoBan.IDTieuChi AND TENHANGSANXUAT.ID=tbXuatKhoBan.IDHangSanXuat"
        ' Tồn kho
        sql &= " Left Join"
        sql &= " ("
        sql &= " SELECT 7 AS IDTieuChi, SUM(GiaNhap*SL)SoTien,IDHangSanXuat,Month(Ngay)Thang"
        sql &= " FROM"
        sql &= " ("
        sql &= " SELECT  IDVatTu, (SELECT IDHangSanXuat FROM VATTU WHERE VATTU.ID=tb.IDVatTu)IDHangSanXuat, SLNhap AS SL,Ngay,"
        sql &= " (SELECT TOP 1 (DonGia * PHIEUNHAPKHO.TyGia) FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu WHERE NHAPKHO.IDVatTu=tb.IDVatTu ORDER BY PHIEUNHAPKHO.NgayThang DESC)GiaNhap"
        sql &= " FROM"
        sql &= " (SELECT NHAPKHO.IDVatTu,SUM(SoLuong)SLNhap, [@tbNgayCuoiThang].Ngay"
        sql &= " FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu "
        sql &= " CROSS JOIN @tbNgayCuoiThang"
        sql &= " WHERE  Convert(Datetime,Convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103)<=[@tbNgayCuoiThang].Ngay"
        sql &= " GROUP BY NHAPKHO.IDVatTu, [@tbNgayCuoiThang].Ngay)tb"
        sql &= " UNION ALL"
        sql &= " SELECT  IDVatTu, (SELECT IDHangSanXuat FROM VATTU WHERE VATTU.ID=tb1.IDVatTu)IDHangSanXuat,(0- SLXuat)SL,Ngay,"
        sql &= " (SELECT TOP 1 (DonGia*PHIEUNHAPKHO.TyGia) FROM NHAPKHO INNER JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieu=NHAPKHO.SoPhieu WHERE NHAPKHO.IDVatTu=tb1.IDVatTu ORDER BY PHIEUNHAPKHO.NgayThang DESC)GiaNhap"
        sql &= " FROM"
        sql &= " (SELECT XUATKHO.IDVatTu,SUM(SoLuong)SLXuat, [@tbNgayCuoiThang].Ngay"
        sql &= " FROM XUATKHO INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu "
        sql &= " CROSS JOIN @tbNgayCuoiThang"
        sql &= " WHERE  Convert(Datetime,Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),103)<=[@tbNgayCuoiThang].Ngay"
        sql &= " GROUP BY XUATKHO.IDVatTu, [@tbNgayCuoiThang].Ngay)tb1)tb3"
        sql &= " GROUP BY IDHangSanXuat,Month(Ngay))tbTon ON [@tbTieuChi].ID=tbTon.IDTieuChi AND TENHANGSANXUAT.ID=tbTon.IDHangSanXuat"
        '
        sql &= " ORDER BY ID,IDTieuChi,Thang"
        sql &= " SELECT IDHangSanXuat,TenHang,IDTieuChi,NoiDung, "
        sql &= " 	[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],"
        sql &= " (CASE IDTieuChi WHEN 7 THEN "
        sql &= " 	(ISNULL([1],0)+ISNULL([2],0)+ISNULL([3],0)+ISNULL([4],0)+ISNULL([5],0)+ISNULL([6],0)+ISNULL([7],0)+ISNULL([8],0)+ISNULL([9],0)+ISNULL([10],0)+ISNULL([11],0)+ISNULL([12],0))/12 "
        sql &= " 	ELSE (ISNULL([1],0)+ISNULL([2],0)+ISNULL([3],0)+ISNULL([4],0)+ISNULL([5],0)+ISNULL([6],0)+ISNULL([7],0)+ISNULL([8],0)+ISNULL([9],0)+ISNULL([10],0)+ISNULL([11],0)+ISNULL([12],0)) END) CaNam,"
        sql &= " (CASE IDTieuChi WHEN 7 THEN "
        sql &= " 	(ISNULL([1],0)+ISNULL([2],0)+ISNULL([3],0))/3 "
        sql &= " 	ELSE (ISNULL([1],0)+ISNULL([2],0)+ISNULL([3],0)) END) AS Quy1,"
        sql &= " (CASE IDTieuChi WHEN 7 THEN "
        sql &= " 	(ISNULL([4],0)+ISNULL([5],0)+ISNULL([6],0))/3 "
        sql &= " 	ELSE (ISNULL([4],0)+ISNULL([5],0)+ISNULL([6],0)) END) AS Quy2,"
        sql &= " (CASE IDTieuChi WHEN 7 THEN "
        sql &= " 	(ISNULL([7],0)+ISNULL([8],0)+ISNULL([9],0))/3 "
        sql &= " 	ELSE (ISNULL([7],0)+ISNULL([8],0)+ISNULL([9],0)) END) AS Quy3,"
        sql &= " (CASE IDTieuChi WHEN 7 THEN "
        sql &= " 	(ISNULL([10],0)+ISNULL([11],0)+ISNULL([12],0))/3 "
        sql &= " 	ELSE (ISNULL([10],0)+ISNULL([11],0)+ISNULL([12],0)) END) AS Quy4"
        sql &= " FROM @tbTmp"
        sql &= " PIVOT"
        sql &= " ("
        sql &= " SUM (SoTien)"
        sql &= " FOR Thang IN"
        sql &= " ( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] )"
        sql &= " ) AS pvt"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)

        End If
    End Sub

    Private Sub gdvHangSXCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvHangSXCT.RowCellClick
        If e.RowHandle < 0 Then Exit Sub
        If e.Column.FieldName = "Chon" Then
            gdvHangSXCT.SetRowCellValue(e.RowHandle, "Chon", Not e.CellValue)
        End If
        gdvHangSXCT.CloseEditor()
        gdvHangSXCT.UpdateCurrentRow()
    End Sub

    Private Sub chkChonHet_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkChonHet.CheckedChanged
        gdvHangSXCT.BeginUpdate()
        For i As Integer = 0 To gdvHangSXCT.DataRowCount - 1
            gdvHangSXCT.SetRowCellValue(i, "Chon", chkChonHet.Checked)
        Next
        gdvHangSXCT.EndUpdate()
    End Sub
End Class