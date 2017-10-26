Imports BACSOFT.Db.SqlHelper

Public Class frmLichSuNhapXuatKhoThue

    Public idVatTu As Object
    Public Nam As Object
    Public idChungTu As Object

    Private Sub frmLichSuNhapXuatKhoThue_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If idVatTu Is DBNull.Value Then Exit Sub

        Dim sql As String = txtSQL.EditValue
        sql = sql.Replace("{@IdVatTu}", idVatTu)
        sql = sql.Replace("{@Nam}", Nam)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        Dim dt As DataTable = ds.Tables(0)

        dt.Columns.Add(New DataColumn("Ton", Type.GetType("System.Int32")))

        If ds.Tables(1).Rows.Count > 0 Then
            txtSoLuongDauKy.EditValue = ds.Tables(1).Rows(0)("DauKy")
        End If
        Dim toncuoiky As Double = txtSoLuongDauKy.EditValue

        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            dt.Rows(i)("Ton") = toncuoiky + dt.Rows(i)("Nhap") - dt.Rows(i)("Xuat")
            toncuoiky = dt.Rows(i)("Ton")
        Next

        gdv.DataSource = dt
        txtSoLuongCuoiKy.EditValue = toncuoiky



        'Kho thuc
        AddParameterWhere("@IDVatTu", idVatTu)
        sql = " SET DATEFORMAT DMY "
        sql &= " SELECT KHACHHANG.ttcMa,PHIEUNHAPKHO.NgayThang,NHAPKHO.SoPhieu,VATTU.Model,SoLuong,"
        sql &= " 		(Dongia * PHIEUNHAPKHO.Tygia)DonGia,(Dongia * PHIEUNHAPKHO.Tygia * SoLuong)ThanhTien,"
        sql &= " 		NGUOIDAT.Ten AS TakeCare,NGUOINHAP.Ten AS NguoiNhapKho"
        sql &= " FROM NHAPKHO "
        sql &= " 	INNER JOIN PHIEUNHAPKHO ON NHAPKHO.Sophieu=PHIEUNHAPKHO.Sophieu"
        sql &= " 	LEFT JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=PHIEUNHAPKHO.SoPhieuDH"
        sql &= "     INNER JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachhang=KHACHHANG.ID"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID=NHAPKHO.IDVatTu"
        sql &= " 	LEFT JOIN NHANSU AS NGUOIDAT ON NGUOIDAT.ID=PHIEUDATHANG.IDTakeCare"
        sql &= " 	LEFT JOIN NHANSU AS NGUOINHAP ON NGUOINHAP.ID=PHIEUNHAPKHO.IDUSer"
        sql &= " WHERE NHAPKHO.IDVattu=@IDVatTu AND YEAR(PHIEUNHAPKHO.NgayThang) = " & Nam & " "
        sql &= " ORDER BY PHIEUNHAPKHO.Ngaythang DESC"

        sql &= " SELECT KHACHHANG.ttcMa,PHIEUXUATKHO.NgayThang,PHIEUXUATKHO.CongTrinh,XUATKHO.SoPhieu,VATTU.Model,SoLuong,"
        sql &= " 		(Dongia * PHIEUXUATKHO.Tygia)DonGia,(Dongia * PHIEUXUATKHO.Tygia * SoLuong)ThanhTien,"
        sql &= " 		TAKECARE.Ten AS TakeCare,NGUOIXUAT.Ten AS NguoiXuatKho"
        sql &= " FROM XUATKHO "
        sql &= " 	INNER JOIN PHIEUXUATKHO ON XUATKHO.Sophieu=PHIEUXUATKHO.Sophieu"
        sql &= "     INNER JOIN KHACHHANG ON PHIEUXUATKHO.IDKhachhang=KHACHHANG.ID"
        sql &= " 	INNER JOIN VATTU ON VATTU.ID=XUATKHO.IDVatTu"
        sql &= " 	LEFT JOIN NHANSU AS TAKECARE ON TAKECARE.ID=PHIEUXUATKHO.IDTakeCare"
        sql &= " 	LEFT JOIN NHANSU AS NGUOIXUAT ON NGUOIXUAT.ID=PHIEUXUATKHO.IDUSer"
        sql &= " WHERE XUATKHO.IDVattu=@IDVatTu AND YEAR(PHIEUXUATKHO.NgayThang) = " & Nam & " "
        sql &= " ORDER BY PHIEUXUATKHO.Ngaythang DESC"



        sql &= ""
        ds = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            Dim f As New frmLichSuNhapXuat
            f.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            f.Dock = DockStyle.Fill
            f.TopLevel = False
            f.Show()
            f.gdvNhap.DataSource = ds.Tables(0)
            f.gdvXuat.DataSource = ds.Tables(1)
            'f.tbTonKho.EditValue = gdvDataVT.GetFocusedRowCellValue("Ton")
            'f.tbTienTonKho.EditValue = Math.Round(gdvDataVT.GetFocusedRowCellValue("Ton") * gdvDataVT.GetFocusedRowCellValue("DonGiaCuoiNamTruoc"), 0, MidpointRounding.AwayFromZero)

            tabHeThongThuc.Controls.Add(f)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub gdvData_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvData.CustomDrawCell
        If IsNumeric(e.CellValue) AndAlso e.CellValue = 0 Then e.DisplayText = ""
        If e.Column.FieldName = "LoaiCT2" Then
            Select Case e.CellValue
                Case ChungTu.LoaiCT2.BanHangHoa
                    e.DisplayText = "Bán hàng"
                Case ChungTu.LoaiCT2.MuaHangTrongNuoc
                    e.DisplayText = "Mua hàng trong nước"
                Case ChungTu.LoaiCT2.MuaCongCuDungCu
                    e.DisplayText = "Mua công cụ dụng cụ"
                Case ChungTu.LoaiCT2.XuLyCongTrinh
                    e.DisplayText = "Xử lý công trình"
            End Select
        End If
        If e.RowHandle >= 0 Then
            If gdvData.GetRowCellValue(e.RowHandle, "ID") = idChungTu Then
                e.Appearance.BackColor = Color.LightGoldenrodYellow
            End If
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Me.Close()
    End Sub


    Private Sub tabMain_SelectedPageChanged(sender As System.Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tabMain.SelectedPageChanged
        If tabMain.SelectedTabPageIndex = 1 Then
            txtSoLuongCuoiKy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            txtSoLuongDauKy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Bar3.Visible = False
        Else
            txtSoLuongCuoiKy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            txtSoLuongDauKy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Bar3.Visible = True
        End If
    End Sub
End Class