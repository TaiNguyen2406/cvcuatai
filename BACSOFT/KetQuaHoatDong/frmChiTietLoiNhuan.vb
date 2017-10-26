Imports BACSOFT.Db.SqlHelper
Public Class frmChiTietLoiNhuan
    Public _Thang As String = ""
    Public _Nam As String = ""

    Private Sub frmChiTietLoiNhuan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            colDuyet.OptionsColumn.ReadOnly = True
            mDuyetDiem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub SplitContainerControl1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles SplitContainerControl1.MouseDoubleClick
        SplitContainerControl1.Horizontal = Not SplitContainerControl1.Horizontal
    End Sub

    Private Sub gdvBCThuTienCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvBCThuTienCT.RowCellClick
        If e.Column.FieldName = "Duyet" Then
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                gdvBCThuTienCT.CloseEditor()
                gdvBCThuTienCT.UpdateCurrentRow()
                gdvBCThuTienCT.SetRowCellValue(e.RowHandle, "Duyet", Not gdvBCThuTienCT.GetRowCellValue(e.RowHandle, "Duyet"))
                gdvBCThuTienCT.CloseEditor()
                gdvBCThuTienCT.UpdateCurrentRow()
            End If

        End If
    End Sub

    Private Sub mDuyetDiem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuyetDiem.ItemClick
        gdvBCThuTienCT.CloseEditor()
        gdvBCThuTienCT.UpdateCurrentRow()
        gdvBCThuTienCT.BeginUpdate()
        With gdvBCThuTienCT
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
        gdvBCThuTienCT.EndUpdate()
    End Sub

    Private Sub gdvBCThuTienCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvBCThuTienCT.CellValueChanged
        On Error Resume Next
        If e.Column.FieldName <> "Modify" Then
            gdvBCThuTienCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub btTaiLai_Click(sender As System.Object, e As System.EventArgs) Handles btTaiLai.Click
        ShowWaiting("Đang tải nội dung...")
        Dim sql As String = ""
        sql &= " SELECT *,(LN*HSLNKT) AS LoiNhuanKT,(LN*HSLNKD) AS LoiNhuanKD FROM "
        sql &= " (SELECT    KHACHHANG.ttcMa, tbThu.NgaythangCT AS Ngay,Datediff(day,PHIEUXUATKHO.NgayThang,tbThu.NgayThangCT)SoNgay,dbo.PHIEUXUATKHO.Sophieu AS SoPhieuXK, dbo.PHIEUXUATKHO.SoPhieuCG,  "
        sql &= "     dbo.BANGCHAOGIA.MaSoDatHang,PHIEUXUATKHO.CongTrinh,tbThu.Sotien AS TienThu, "
        sql &= "     dbo.PHIEUXUATKHO.Tienthue * dbo.PHIEUXUATKHO.Tygia AS TienThue, tbThu.Sotien, ISNULL(tb.GiaNhap, 0) AS GiaNhap,"
        sql &= "     dbo.PHIEUXUATKHO.tienchietkhau * dbo.PHIEUXUATKHO.Tygia AS TienChietKhau, "
        'sql &= "     (CASE PHIEUXUATKHO.TienTruocThue WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * dbo.PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap,0) "
        'sql &= " 		- ISNULL(dbo.V_XuatkhoChiphiTM.Chitienmat, 0) - ISNULL(dbo.V_XuatkhoChiphiUnc.ChiUNC, 0) "
        'sql &= " 		- (ISNULL(dbo.PHIEUXUATKHO.tienchietkhau, 0) * ISNULL(dbo.PHIEUXUATKHO.Tygia, 1) / (1 - ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15)))) "
        'sql &= "      / ((dbo.PHIEUXUATKHO.Tientruocthue + dbo.PHIEUXUATKHO.Tienthue) * dbo.PHIEUXUATKHO.Tygia) END )AS TySuatLN,"
        sql &= " 	 (CASE PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= "      - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN BANGCHAOGIA.KhauTru is null THEN 0.15 ELSE BANGCHAOGIA.KhauTru/100 END) END)) ) /((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.Tygia) END) AS TySuatLN, "

        sql &= " 	 tbThu.Sotien * (CASE PHIEUXUATKHO.Tientruocthue  * PHIEUXUATKHO.Tygia WHEN 0 THEN 0 ELSE (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(tb.GiaNhap, 0) - ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) "
        sql &= "      - ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0) - ISNULL(PHIEUXUATKHO.tienchietkhau, 0) / (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15) ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END)) ) /((PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.TienThue) * PHIEUXUATKHO.Tygia) END) AS LN, "


        sql &= "    (CASE WHEN BANGCHAOGIA.CongTrinh = 1 THEN (CASE WHEN ISNULL(BANGCHAOGIA.NhanKS, 1) = 1 "
        sql &= "    THEN KTPhanBoLoiNhuanCT.PhuTrachHopDong / 100 ELSE (KTPhanBoLoiNhuanCT.PhuTrachHopDong + KTPhanBoLoiNhuanCT.PhuTrachChaoGia) / 100 END) "
        sql &= "    ELSE 1 END) As HSLNKD,"
        sql &= "    (CASE WHEN BANGCHAOGIA.CongTrinh = 1 THEN (CASE WHEN ISNULL(BANGCHAOGIA.NhanKS, 1) = 1 "
        sql &= "    THEN (100 - ISNULL(KTPhanBoLoiNhuanCT.PhuTrachHopDong, 0)) / 100 ELSE (100 - ISNULL(KTPhanBoLoiNhuanCT.PhuTrachHopDong, 0) "
        sql &= "    - ISNULL(KTPhanBoLoiNhuanCT.PhuTrachChaoGia, 0)) / 100 END) ELSE 0 END) AS HSLNKT,"
        sql &= " 	PHIEUXUATKHO.IDTakeCare,NHANSU.Ten AS TakeCare"
        sql &= " FROM  (SELECT     NgaythangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "        FROM  THU"
        sql &= "        UNION ALL"
        sql &= "        SELECT     ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "        FROM  THUNH) AS tbThu "
        sql &= " 	INNER JOIN PHIEUXUATKHO ON tbThu.PhieuTC1 = dbo.PHIEUXUATKHO.Sophieu OR tbThu.PhieuTC0 = dbo.PHIEUXUATKHO.SophieuCG "
        sql &= "    LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUXUATKHO.IDKhachhang "
        sql &= " 	LEFT JOIN NHANSU ON NHANSU.ID= PHIEUXUATKHO.IDTakeCare"
        sql &= " 	LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(dbo.tblQuyDoi.ThangNam, 2)) = MONTH(tbThu.NgaythangCT) AND CONVERT(int, RIGHT(dbo.tblQuyDoi.ThangNam, 4)) = YEAR(tbThu.NgaythangCT) "
        sql &= " 	INNER JOIN BANGCHAOGIA ON dbo.BANGCHAOGIA.Sophieu = dbo.PHIEUXUATKHO.SophieuCG "
        sql &= " 	INNER JOIN BANGYEUCAU ON dbo.BANGYEUCAU.Sophieu = dbo.BANGCHAOGIA.Masodathang "
        sql &= " 	LEFT OUTER JOIN dbo.KTPhanBoLoiNhuanCT ON dbo.BANGYEUCAU.IDLoaiYeuCau = dbo.KTPhanBoLoiNhuanCT.ID "
        sql &= " 	LEFT OUTER JOIN (SELECT     Sophieu, SUM(ISNULL(gianhap, giaban) * Soluong) AS GiaNhap"
        sql &= "                    FROM dbo.View_XuatKhoGiaNhapTB"
        sql &= "                    GROUP BY Sophieu) AS tb ON tb.Sophieu = dbo.PHIEUXUATKHO.Sophieu "
        sql &= " 	LEFT OUTER JOIN V_XuatkhoChiphiTM ON dbo.V_XuatkhoChiphiTM.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " 	LEFT OUTER JOIN V_XuatkhoChiphiUnc ON dbo.V_XuatkhoChiphiUnc.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " 	LEFT OUTER JOIN V_XuatkhoChietkhauTM ON dbo.V_XuatkhoChietkhauTM.Sophieu = PHIEUXUATKHO.Sophieu "
        sql &= " 	LEFT OUTER JOIN V_XuatkhoChietkhauUNC ON dbo.V_XuatkhoChietkhauUNC.Sophieu = PHIEUXUATKHO.Sophieu"
        sql &= " WHERE (month(tbThu.NgayThangCT)=" & _Thang & " AND Year(tbThu.NgayThangCT)= " & _Nam & "))tb ORDER BY SoPhieuXK"

        sql &= " SELECT * FROM ("
        sql &= " SELECT ROW_NUMBER() OVER (PARTITION BY HDNhanVIen.ID ORDER BY right(HDNhanVien.ChiTiet,9)) AS STT, HDNhanVIen.ID,NgayBaoCao,NHANSU.Ten AS NhanVien,Datediff(day,PHIEUXUATKHO.NgayThang,tbThu.NgayThangCT)SoNgay, PHIEUXUATKHO.SoPhieu, NgayNhapLieu,IdDanhSach,HDTen.Ten AS NoiDungBC,ChiTiet,SoLuong,KhoiLuong,HDNhanVien.Diem,BaoCao,HDNhanVien.PhanHoi,Duyet,IDNhanVien,HDNhanVien.IDUser,YearMonth,Convert(bit,0)Modify"
        sql &= " FROM HDNhanVien"
        sql &= " INNER JOIN NHANSU ON HDNhanVien.IDNhanVien=NHANSU.ID"
        sql &= " INNER JOIN HDDanhSach ON HDDanhSach.ID=HDNhanVien.IDDanhSach"
        sql &= " INNER JOIN HDTen ON HDDanhSach.IDTen=HDTen.ID"
        sql &= " LEFT JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=right(HDNhanVien.ChiTiet,9)"
        sql &= " LEFT JOIN  (SELECT     NgaythangCT, Sophieu, IDkh, Sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "        FROM  THU"
        ' sql &= "        WHERE month(NgayThangCT)=" & _Thang & " AND Year(NgayThangCT)= " & _Nam
        sql &= "        UNION ALL"
        sql &= "        SELECT     ngaythangCT, sophieu, IDKh, sotien, Mucdich, IDUser, PhieuTC0, PhieuTC1"
        sql &= "        FROM  THUNH"
        '  sql &= "        WHERE month(NgayThangCT)=" & _Thang & " AND Year(NgayThangCT)= " & _Nam
        sql &= "        ) AS tbThu ON (tbThu.PhieuTC1 = PHIEUXUATKHO.SoPhieu OR tbThu.PhieuTC0=PHIEUXUATKHO.SoPhieuCG) AND HDNhanVien.KhoiLuong=tbThu.SoTien "
        sql &= " WHERE IDDanhSach IN (35,36,37,38,39,40)"
        sql &= " AND month(HDNhanVien.NgayBaoCao)=" & _Thang & " AND Year(HDNhanVien.NgayBaoCao)= " & _Nam
        sql &= " )tbl"
        sql &= " WHERE STT =1"
        sql &= " ORDER BY SoPhieu"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdv.DataSource = ds.Tables(0)
            gdvBCThuTien.DataSource = ds.Tables(1)
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
End Class