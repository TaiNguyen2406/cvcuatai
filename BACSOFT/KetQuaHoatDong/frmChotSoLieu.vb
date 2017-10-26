Imports BACSOFT.Db.SqlHelper

Public Class frmChotSoLieu

    Private Sub frmChotSoLieu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tbThang.EditValue = Today.Date
    End Sub

    Private Sub tbThang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbThang.ButtonClick
        If e.Button.Index = 1 Then
            tbThang.EditValue = Nothing
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        If ShowCauHoi("Cập nhật số liệu ?") Then
            Dim sql As String = ""
            If chkCNGiaNhapTBTrongXK.Checked Then
                ShowWaiting("Cập nhật giá nhập trung bình trên xuất kho ...")
                sql &= " UPDATE XUATKHO SET GiaNhap="
                sql &= " ISNULL((SELECT DonGia FROM tblTonDauKy WHERE tblTonDauKy.IDVatTu=XuatKho.IDVatTu"
                sql &= " AND ThoiGian=(Select right(Convert(nvarchar,NgayThang,103),7) FROM PHIEUXUATKHO"
                sql &= " WHERE PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
                If Not tbThang.EditValue Is Nothing Then
                    sql &= " AND Right(Convert(nvarchar,NgayThang,103),7)=@Thang "
                End If
                sql &= " )),0)"

                If Not tbThang.EditValue Is Nothing Then
                    AddParameterWhere("@Thang", Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy"))
                    sql &= " WHERE ISNULL( XUATKHO.isGiaDacBiet,0)=0 AND XUATKHO.SoPhieu IN (SELECT SoPhieu FROM PHIEUXUATKHO WHERE Right(Convert(nvarchar,NgayThang,103),7)=@Thang )"
                End If

                sql &= " UPDATE PHIEUXUATKHO SET TienGoc=tb.TongGiaNhap"
                sql &= " FROM PHIEUXUATKHO,"
                sql &= " (SELECT SUM(SoLuong*ISNULL(GiaNhap,0))TongGiaNhap,SoPhieu FROM XUATKHO"
                sql &= " GROUP BY SoPhieu)tb"
                sql &= " WHERE PHIEUXUATKHO.SoPhieu=tb.SoPhieu"
                If Not tbThang.EditValue Is Nothing Then
                    sql &= " AND Right(Convert(nvarchar,NgayThang,103),7)=@Thang "
                End If
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
                CloseWaiting()
            End If

            Application.DoEvents()

            If chkCNChiPhiXK.Checked Then
                ShowWaiting("Cập nhật chi phí trên xuất kho ...")
                sql = ""
                sql &= " UPDATE PHIEUXUATKHO SET ChiPhi=ISNULL(tb2.SoTien,0)"
                sql &= " FROM PHIEUXUATKHO,"
                sql &= " (SELECT SUM(SoTien) as SoTien,SoPhieu"
                sql &= " FROM("
                sql &= " SELECT SoPhieu,ChiTienMat as SoTien  FROM V_XuatkhoChiphiTM"
                sql &= " UNION ALL "
                sql &= " SELECT SoPhieu,ChiUNC as SoTien FROM V_XuatkhoChiphiUNC)tb GROUP BY SoPhieu)tb2"
                sql &= " WHERE PHIEUXUATKHO.SoPhieu=tb2.SoPhieu"
                If Not tbThang.EditValue Is Nothing Then
                    AddParameterWhere("@Thang", Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy"))
                    sql &= " AND Right(Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),7)=@Thang "
                End If

                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
                CloseWaiting()

            End If
            Application.DoEvents()
            If chkCNLoiNhuanXK.Checked Then
                ShowWaiting("Cập nhật lợi nhuận trên xuất kho ...")
                sql = ""
                sql &= " UPDATE PHIEUXUATKHO SET LoiNhuanChot="
                sql &= " (PHIEUXUATKHO.Tientruocthue * PHIEUXUATKHO.Tygia - ISNULL(TienGoc,0) - ISNULL(ChiPhi,0) - "
                sql &= " ISNULL(PHIEUXUATKHO.tienchietkhau, 0) /  "
                sql &= " (1 - (CASE WHEN PHIEUXUATKHO.CongTrinh=0 THEN "
                sql &= " ISNULL(tblQuyDoi.HSThuCK, 0.15)"
                sql &= " ELSE (CASE WHEN KhauTru is null THEN 0.15 ELSE KhauTru/100 END) END))) "
                sql &= " FROM PHIEUXUATKHO,BANGCHAOGIA,tblQuyDoi"
                sql &= " WHERE tblQuyDoi.ThangNam = right(Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),7) AND PHIEUXUATKHO.SoPhieuCG=BANGCHAOGIA.SoPhieu"
                If Not tbThang.EditValue Is Nothing Then
                    AddParameterWhere("@Thang", Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy"))
                    sql &= " AND Right(Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),7)=@Thang "
                End If

                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
                CloseWaiting()

            End If

            'If chkLNNVPhatTrienSP.Checked Then
            '    If tbThang.EditValue IsNot Nothing Then
            '        ShowWaiting("Cập nhật lợi nhuận nhân viên phát triển...")
            '        sql = " DELETE FROM PhatTrienSanPham_LoiNhuan WHERE left(SoPhieu,4) = '" & Convert.ToDateTime(tbThang.EditValue).ToString("yyMM") & "'"
            '        sql &= " INSERT INTO PhatTrienSanPham_LoiNhuan(SoPhieu,IDVatTu,SoLuong,GiaBan,GiaNhap,LoiNhuan,IDNhanVien)"
            '        sql &= " SELECT XUATKHO.SoPhieu,XUATKHO.IDVatTu,XUATKHO.SoLuong,XUATKHO.DonGia,XUATKHO.GiaNhap,"
            '        sql &= " (CASE PHIEUXUATKHO.CongTrinh WHEN 0 "
            '        sql &= " THEN (XUATKHO.DonGia-ISNULL(XUATKHO.GiaNhap,0) - "
            '        sql &= " ((ISNULL(V_XuatkhoChiphiTM.Chitienmat, 0) + ISNULL(V_XuatkhoChiphiUnc.ChiUNC, 0))/PHIEUXUATKHO.TienTruocThue)"
            '        sql &= " *XUATKHO.DonGia - ISNULL(CHAOGIA.ChietKhau,0)/(1-ISNULL(dbo.tblQuyDoi.HSThuCK, 0.15)))* PhatTrienSanPham.PTLoiNhuanTM/100"
            '        sql &= " ELSE  XUATKHO.GiaNhap*PTLoiNhuanCT/100 END)LoiNhuan,"
            '        sql &= " PhatTrienSanPham.IDPhuTrach"
            '        sql &= " FROM XUATKHO"
            '        sql &= " LEFT JOIN CHAOGIA ON CHAOGIA.ID=XUATKHO.IDChaoGia"
            '        sql &= " INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu"
            '        sql &= " AND Right(Convert(nvarchar,PHIEUXUATKHO.NgayThang,103),7)=@Thang "
            '        sql &= " LEFT JOIN tblQuyDoi ON CONVERT(int, LEFT(tblQuyDoi.ThangNam, 2)) = MONTH(PHIEUXUATKHO.NgayThang) AND CONVERT(int, RIGHT(tblQuyDoi.ThangNam, 4))= YEAR(PHIEUXUATKHO.Ngaythang) "
            '        sql &= " LEFT JOIN V_XuatkhoChiphiTM ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiTM.Sophieu "
            '        sql &= " LEFT JOIN V_XuatkhoChiphiUnc ON PHIEUXUATKHO.Sophieu = V_XuatkhoChiphiUnc.Sophieu "
            '        sql &= " INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu "
            '        sql &= " INNER JOIN PhatTrienSanPham ON VATTU.IDHangSanXuat=PhatTrienSanPham.IDHangSX"
            '        sql &= " AND PhatTrienSanPham.Thang=@Thang "
            '        AddParameterWhere("@Thang", Convert.ToDateTime(tbThang.EditValue).ToString("MM/yyyy"))
            '        If ExecuteSQLNonQuery(sql) Is Nothing Then
            '            ShowBaoLoi(LoiNgoaiLe)
            '        End If
            '        CloseWaiting()
            '    End If
            'End If
        End If
    End Sub

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub
End Class