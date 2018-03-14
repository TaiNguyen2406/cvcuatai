Imports BACSOFT.Db.SqlHelper
Imports System.IO
Imports DevExpress.XtraPrinting
Imports System.DateTime
Public Class frmChiTietLoiNhuan2
    Public _Thang As String = ""
    Public _Nam As String = ""

    Private Sub frmChiTietLoiNhuan_Load(sender As System.Object, e As System.EventArgs) Handles Me.Load
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            colDuyet.OptionsColumn.ReadOnly = True
            mDuyetDiem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        For i = 1 To 12
            riCbbThang.Items.Add(i)
        Next
        Dim hientai As DateTime = GetServerTime()
        For i = 2000 To hientai.Year() + 1
            riCbbNam.Items.Add(i)
        Next
        cbbThang.EditValue = _Thang
        cbbNam.EditValue = _Nam
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 and TrangThai=1")
        If Not dt Is Nothing Then
            riLueTakeCare.DataSource = dt
            lueTakeCare.EditValue = Convert.ToInt32(TaiKhoan)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        '   btnTaiLai.PerformClick()
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

    Private Sub btnTaiLai_ItemClick(sender As System.Object, e As System.EventArgs) Handles btnTaiLai.ItemClick
        _Thang = cbbThang.EditValue
        _Nam = cbbNam.EditValue
        '   ShowWaiting("Đang tải nội dung...")
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
        sql &= " 	INNER JOIN PHIEUXUATKHO ON tbThu.PhieuTC1 = dbo.PHIEUXUATKHO.Sophieu   OR tbThu.PhieuTC0 = dbo.PHIEUXUATKHO.SophieuCG "
        sql &= "    LEFT JOIN KHACHHANG ON KHACHHANG.ID=PHIEUXUATKHO.IDKhachhang "
        sql &= " 	LEFT JOIN NHANSU ON NHANSU.ID= PHIEUXUATKHO.IDTakeCare"
        sql &= " 	LEFT OUTER JOIN tblQuyDoi ON CONVERT(int, LEFT(dbo.tblQuyDoi.ThangNam, 2)) = MONTH(tbThu.NgaythangCT) AND CONVERT(int, RIGHT(dbo.tblQuyDoi.ThangNam, 4)) = YEAR(tbThu.NgaythangCT) "
        sql &= " 	inner JOIN BANGCHAOGIA ON dbo.BANGCHAOGIA.Sophieu = dbo.PHIEUXUATKHO.SophieuCG "
        sql &= " 	inner JOIN BANGYEUCAU ON dbo.BANGYEUCAU.Sophieu = dbo.BANGCHAOGIA.Masodathang "
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
        If chkMoi.Checked = True Then
            Dim ds As New DataSet
            sql = meSql.Text
            btnTaiLai.Enabled = False
            btnTaiLai.Caption = "Đang tải...."
            AddParameter("@Thang", cbbThang.EditValue)
            AddParameter("@Nam", cbbNam.EditValue)
            If lueTakeCare.EditValue Is Nothing Then
                AddParameter("@IDTakeCare", "")
            Else
                AddParameter("@IDTakeCare", lueTakeCare.EditValue)
            End If

            ds = ExecuteSQLDataSet(sql)
            If Not ds Is Nothing Then
                Dim tb As DataTable = ds.Tables(0)
                For i = 0 To tb.Rows.Count - 1
                    If i < tb.Rows.Count Then
                        Dim SoCG = tb.Rows(i)("SophieuCG")
                        For j = i To tb.Rows.Count - 1
                            If j < tb.Rows.Count Then
                                Dim tienthu = tb.Rows(j)("Thu")
                                If tb.Rows(j)("SophieuCG") = SoCG And tb.Rows(j)("PhieuTC0") <> "000000000" And tb.Rows(j)("GiaTriXK") <> 0 Then
                                    Dim SoPhieuXK = tb.Rows(j)("SoPhieuXK")
                                    Dim SoPhieuThu = tb.Rows(j)("SoPhieu")
                                    If tienthu >= tb.Rows(j)("GiaTriXK") Then
                                        tienthu = tienthu - tb.Rows(j)("GiaTriXK")
                                        tb.Rows(j)("Thu") = tb.Rows(j)("GiaTriXK")
                                        tb.Rows(j)("GiaTriXK") = 0
                                        'tb.Rows(j)("LN") = tb.Rows(j)("Thu") * tb.Rows(j)("TyLe")
                                        'tb.Rows(j)("LoiNhuanKT") = tb.Rows(j)("LN") * tb.Rows(j)("HSLNKT")
                                        'tb.Rows(j)("LoiNhuanKD") = tb.Rows(j)("LN") * tb.Rows(j)("HSLNKD")
                                        If j < tb.Rows.Count Then
                                            For k = j + 1 To tb.Rows.Count - 1
                                                If tb.Rows(k)("SoPhieu") = SoPhieuThu Then
                                                    tb.Rows(k)("Thu") = tienthu

                                                End If
                                                If tb.Rows(k)("SoPhieuXK") = SoPhieuXK Then
                                                    tb.Rows(k)("TienTruocThue") = 0
                                                    tb.Rows(k)("GiaTriXK") = 0
                                                    tb.Rows(k).Delete()
                                                End If

                                            Next
                                            tb.AcceptChanges()
                                        End If
                                    Else
                                        Dim GiaTriXK = tb.Rows(j)("GiaTriXK") - tienthu
                                        tb.Rows(j)("Thu") = tienthu
                                        tb.Rows(j)("GiaTriXK") = 0
                                        'tb.Rows(j)("LN") = tb.Rows(j)("Thu") * tb.Rows(j)("TyLe")
                                        'tb.Rows(j)("LoiNhuanKT") = tb.Rows(j)("LN") * tb.Rows(j)("HSLNKT")
                                        'tb.Rows(j)("LoiNhuanKD") = tb.Rows(j)("LN") * tb.Rows(j)("HSLNKD")
                                        tienthu = 0
                                        For k = j + 1 To tb.Rows.Count - 1
                                            If tb.Rows(k)("SoPhieuXK") = SoPhieuXK Then
                                                '  tb.Rows(k)("TienTruocThue") = GiaTriXK
                                                tb.Rows(k)("GiaTriXK") = GiaTriXK
                                            End If
                                            If tb.Rows(k)("SoPhieu") = SoPhieuThu Then
                                                tb.Rows(k)("Thu") = tienthu
                                                tb.Rows(k).Delete()
                                            End If
                                        Next
                                        tb.AcceptChanges()
                                    End If
                                End If
                            End If

                        Next
                        tb.AcceptChanges()
                    End If
                   
                Next
                tb.AcceptChanges()
                Dim d = tb.Rows.Count - 1
                For i = 0 To tb.Rows.Count - 1
                    Dim ngaythu As DateTime = tb.Rows(i)("Ngay")
                    Dim ngayxk As DateTime = tb.Rows(i)("NgayXK")
                    Dim ngayxem As DateTime = New DateTime(cbbNam.EditValue, cbbThang.EditValue, DaysInMonth(cbbNam.EditValue, cbbThang.EditValue))

                    If Not ((ngaythu.Month = cbbThang.EditValue And ngaythu.Year = cbbNam.EditValue And ngayxk.Month <= cbbThang.EditValue And ngayxk.Year <= cbbNam.EditValue) Or (ngaythu.Month <= cbbThang.EditValue And ngaythu.Year <= cbbNam.EditValue And ngayxk.Month = cbbThang.EditValue And ngayxk.Year = cbbNam.EditValue) Or (ngaythu.Month >= cbbThang.EditValue And ngaythu.Year < cbbNam.EditValue And ngayxk.Month = cbbThang.EditValue And ngayxk.Year = cbbNam.EditValue) Or (ngaythu.Month = cbbThang.EditValue And ngaythu.Year = cbbNam.EditValue And ngayxk.Month >= cbbThang.EditValue And ngayxk.Year < cbbNam.EditValue)) Then
                        '    
                        '  tb.Rows(i)("Thu") = 0
                        'tb.Rows(i)("PhieuTC0") = "000000001"
                        tb.Rows(i).Delete()
                      
                    Else
                       tb.Rows(i)("LN") = tb.Rows(i)("Thu") * tb.Rows(i)("TyLe")
                        tb.Rows(i)("LoiNhuanKT") = tb.Rows(i)("LN") * tb.Rows(i)("HSLNKT")
                        tb.Rows(i)("LoiNhuanKD") = tb.Rows(i)("LN") * tb.Rows(i)("HSLNKD")
                    End If
                    

                Next
                tb.AcceptChanges()
                gdv.DataSource = tb
                gdvBCThuTien.DataSource = ds.Tables(1)
                btnTaiLai.Enabled = True
                btnTaiLai.Caption = "Tải lại"
                '    CloseWaiting()
            Else
                '   CloseWaiting()
                ShowBaoLoi(LoiNgoaiLe)
            End If

            'AddParameter("@Thang", cbbThang.EditValue)
            'AddParameter("@Nam", cbbNam.EditValue)
            'Dim ds As DataSet = ExecuteSQLDataSet(sql)
           
        Else
            Dim th As New Threading.Thread(
            Sub()
                CheckForIllegalCrossThreadCalls = False
                sql = meSqlCu.Text
                btnTaiLai.Enabled = False
                btnTaiLai.Caption = "Đang tải...."
                AddParameter("@Thang", cbbThang.EditValue)
                AddParameter("@Nam", cbbNam.EditValue)
                Dim ds As DataSet = ExecuteSQLDataSet(sql)
                gdv.DataSource = ds.Tables(0)
                gdvBCThuTien.DataSource = ds.Tables(1)
                btnTaiLai.Enabled = True
                btnTaiLai.Caption = "Tải lại"
            End Sub
           )
            th.Start()
        End If
      
      
       
        
       
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Alt Or Keys.O) Then
            If meSql.Visible = True Then
                meSql.Visible = False

            Else
                meSql.Visible = True

            End If
            meSqlCu.Visible = Not meSqlCu.Visible
            Return True
        End If
        Return False
    End Function
    Private Sub ketxuat(ByVal gvexprort As DevExpress.XtraGrid.Views.Grid.GridView, ByVal _FileName As String)
        Dim saveDialog As SaveFileDialog = New SaveFileDialog() 'tgchuyentuchaogia
        Try
            saveDialog.Filter = "Excel (2010) (.xlsx)|*.xlsx"
            saveDialog.FileName = _FileName
            If saveDialog.ShowDialog() = DialogResult.OK Then
                ShowWaiting("Đang kết xuất ..." + saveDialog.FileName)
                Dim exportFilePath As String = saveDialog.FileName
                Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
                Dim tuychonx As XlsxExportOptions = New XlsxExportOptions
                tuychonx.ShowGridLines() = True
                Try
                    gvexprort.ExportToXlsx(exportFilePath, tuychonx)
                Catch ex As Exception
                    ShowBaoLoi(LoiNgoaiLe)
                End Try
            End If
            CloseWaiting()
        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
        End Try
        CloseWaiting()
    End Sub
    Private Sub btnKetXuat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKetXuat.ItemClick
        If gdvCT.RowCount > 0 Then
            ketxuat(gdvCT, "Chi tiết phiếu thu trên hệ thống " & cbbThang.EditValue.ToString() & "-" & cbbNam.EditValue.ToString())
        End If
        If gdvBCThuTienCT.RowCount > 0 Then
            ketxuat(gdvBCThuTienCT, "Chi tiết phiếu thu tiền của nhân viên " & cbbThang.EditValue.ToString() & "-" & cbbNam.EditValue.ToString())
        End If
    End Sub


    Private Sub riLueTakeCare_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueTakeCare.ButtonClick
        If e.Button.Index = 1 Then
            lueTakeCare.EditValue = Nothing
        End If
    End Sub
End Class