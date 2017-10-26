Imports BACSOFT.Db.SqlHelper

Public Class frmPhanBoChiPhiNhap

    Private Sub frmPhanBoChiPhiNhap_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = Convert.ToDateTime(tbDenNgay.EditValue)
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub


    Private Sub btTaiDS_Click(sender As System.Object, e As System.EventArgs) Handles btTaiDS.Click
        ShowWaiting("Đang tải danh sách ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        If chkPhieuChi.Checked Then
            sql &= " SELECT ROW_NUMBER() OVER(ORDER BY SoPhieuNK) AS STT,SoPhieuNK,TrangThai,NhapKhau,GhiChu "
            sql &= " FROM ( "
            sql &= " SELECT DISTINCT "
            sql &= "	(CASE WHEN PhieuTC1 <> '000000000' THEN PhieuTC1 ELSE"
            sql &= " 	(CASE WHEN PhieuTC0 <> '000000000' THEN SoPhieu ELSE '' END) END) SoPhieuNK,N'' as TrangThai,Convert(bit,0) as NhapKhau,N'' as GhiChu"
            sql &= " FROM("
            sql &= " 	SELECT PhieuTC0,PhieuTC1,PHIEUNHAPKHO.SoPhieu FROM"
            sql &= " 	("
            sql &= " 	SELECT PhieuTC0,PhieuTC1 FROM CHI"
            sql &= " 	WHERE MucDIch=205 AND ChiPhiNhap=1 AND (PhieuTC0<>'000000000' OR PhieuTC1 <>'000000000')"
            sql &= "    AND Convert(datetime,Convert(nvarchar,NgayThangCT,103),103) between @TuNgay AND @DenNgay"
            sql &= " 	UNION ALL"
            sql &= " 	SELECT PhieuTC0,PhieuTC1 FROM UNC"
            sql &= " 	WHERE MucDIch=205 AND ChiPhiNhap=1 AND (PhieuTC0<>'000000000' OR PhieuTC1 <>'000000000')"
            sql &= "    AND Convert(datetime,Convert(nvarchar,NgayThang,103),103) between @TuNgay AND @DenNgay"
            sql &= " 	)tbChi"
            sql &= " 	LEFT JOIN PHIEUNHAPKHO ON PHIEUNHAPKHO.SoPhieuDH=tbChi.PhieuTC0"
            sql &= "  )tbSoPhieuNK"
            sql &= "  UNION ALL"
            sql &= "  SELECT PHIEUNHAPKHO.SoPhieu as SoPhieuNKN,'' as TrangThai,(CASE WHEN PHIEUDATHANG.LoaiDH =2 THEN CONVERT(bit,1) ELSE Convert(bit,0) END)NhapKhau, N'' as GhiChu"
            sql &= "	FROM PHIEUNHAPKHO "
            sql &= "	INNER JOIN PHIEUDATHANG ON PHIEUNHAPKHO.SoPhieuDH=PHIEUDATHANG.SoPhieu AND PHIEUDATHANG.LoaiDH=2"
            sql &= "	WHERE Convert(datetime,Convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) between @TuNgay AND @DenNgay"
            sql &= "  )tbl "
            sql &= "  ORDER BY SoPhieuNK "
        Else
            sql &= " SELECT ROW_NUMBER() OVER(ORDER BY SoPhieu) AS STT, SoPhieu as SoPhieuNK,NhapKhau,N'' as TrangThai,N'' as GhiChu"
            sql &= " FROM (SELECT DISTINCT SoPhieu,Convert(bit,0) as NhapKhau FROM("
            sql &= " SELECT PHIEUNHAPKHO.SoPhieu,tbChi.SoTien "
            sql &= " FROM PHIEUNHAPKHO"
            sql &= " LEFT JOIN "
            sql &= " (SELECT SoTien,PhieuTC0,PhieuTC1,MucDich,ChiPhiNhap FROM CHI"
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,PhieuTC0,PhieuTC1,MucDich,ChiPhiNhap FROM UNC)tbChi"
            sql &= "  ON (PHIEUNHAPKHO.SoPhieu=tbChi.PhieuTC1 OR PHIEUNHAPKHO.SoPhieuDH=tbChi.PhieuTC0) AND tbChi.MucDich IN (205,208) AND tbChi.ChiPhiNhap=1"
            sql &= " WHERE Convert(datetime,Convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) between @TuNgay AND @DenNgay"
            sql &= " )tb WHERE SoTien is not null "
            sql &= "   UNION ALL"
            sql &= "   SELECT PHIEUNHAPKHO.SoPhieu as SoPhieuNK,(CASE WHEN PHIEUDATHANG.LoaiDH =2 THEN CONVERT(bit,1) ELSE Convert(bit,0) END)NhapKhau"
            sql &= " 	FROM PHIEUNHAPKHO "
            sql &= " 	INNER JOIN PHIEUDATHANG ON PHIEUNHAPKHO.SoPhieuDH=PHIEUDATHANG.SoPhieu AND PHIEUDATHANG.LoaiDH=2"
            sql &= " 	WHERE Convert(datetime,Convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) between @TuNgay AND @DenNgay"
            sql &= " )tb2 ORDER BY SoPhieu"
        End If
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If tb Is Nothing Then
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdv.DataSource = tb
            CloseWaiting()
        End If

    End Sub

    Private Sub btPhanBoChiPhi_Click(sender As System.Object, e As System.EventArgs) Handles btPhanBoChiPhi.Click
        If ShowCauHoi("Phân bổ chi phí ?") Then
            For i As Integer = 0 To gdvCT.RowCount - 1
                gdvCT.FocusedRowHandle = i
                Application.DoEvents()
                If gdvCT.GetRowCellValue(i, "NhapKhau") = False Then
                    Dim str As String = PhanBoChiPhiNhapR(gdvCT.GetRowCellValue(i, "SoPhieuNK"))
                    If str = "" Then
                        gdvCT.SetRowCellValue(i, "TrangThai", "Đã phân bổ")
                    Else
                        gdvCT.SetRowCellValue(i, "TrangThai", "Lỗi")
                        gdvCT.SetRowCellValue(i, "GhiChu", str)
                    End If
                Else
                    Dim sql As String = ""
                    sql &= " UPDATE NHAPKHO SET ChiPhi=(SELECT CHiPhi FROM DATHANG WHERE DATHANG.ID=NHAPKHO.IDDatHang)"
                    sql &= " WHERE SoPhieu='" & gdvCT.GetRowCellValue(i, "SoPhieuNK") & "'"
                    If ExecuteSQLNonQuery(sql) Is Nothing Then
                        gdvCT.SetRowCellValue(i, "TrangThai", "Lỗi")
                        gdvCT.SetRowCellValue(i, "GhiChu", LoiNgoaiLe)
                    Else
                        gdvCT.SetRowCellValue(i, "TrangThai", "Đã phân bổ")
                    End If
                End If
                
            Next
            ShowAlert("Đã hoàn thành!")
        End If
    End Sub
End Class