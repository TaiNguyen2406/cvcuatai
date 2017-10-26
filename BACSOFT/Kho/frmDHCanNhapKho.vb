Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraEditors
Imports BACSOFT.Utils

Public Class frmDHCanNhapKho
    Public _exit As Boolean = False
    Public _FileCGKinhDoanh As String = ""

    Private Sub frmDHCanNhapKho_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Top 5000"
        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
    End Sub

#Region "Load DS đặt hàng cần nhập"

    Public Sub LoadDS()
        ShowWaiting("Đang tải dữ liệu...")

        Dim sql As String = ""
        If cbTieuChi.EditValue = "Top 5000" Then
            sql = " SELECT TOP 5000 PHIEUDATHANG.ID,PHIEUDATHANG.Sophieu,PHIEUDATHANG.NgayDat,PHIEUDATHANG.NgayNhan,PHIEUDATHANG.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenNCC, "
        Else
            sql = " SELECT PHIEUDATHANG.ID,PHIEUDATHANG.Sophieu,PHIEUDATHANG.NgayDat,PHIEUDATHANG.NgayNhan,PHIEUDATHANG.IDKhachhang,KHACHHANG.ttcMa AS MaKH,KHACHHANG.Ten AS TenNCC, "
        End If
        sql &= "	Null as CanhBao,datediff(day,getdate(),PHIEUDATHANG.NgayNhan)SoNgay,"
        sql &= "	PHIEUDATHANG.TienTruocthue,PHIEUDATHANG.Tienthue,PHIEUDATHANG.IDUser,  "
        sql &= "	NHANSU_1.Ten AS TenNgd,PHIEUDATHANG.IDTakeCare,TAKECARE.Ten AS TakeCare,tblTienTe.Ten AS TienTe"
        sql &= " FROM PHIEUDATHANG LEFT OUTER JOIN KHACHHANG ON PHIEUDATHANG.IDKhachhang=KHACHHANG.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS NHANSU_1 ON PHIEUDATHANG.IDNgd=NHANSU_1.ID"
        sql &= "        LEFT OUTER JOIN NHANSU AS TAKECARE ON PHIEUDATHANG.IDTakeCare=TAKECARE.ID"
        sql &= "        LEFT OUTER JOIN tblTienTe ON PHIEUDATHANG.Tiente=tblTienTe.ID"
        sql &= "        INNER JOIN (SELECT DISTINCT SoPhieu FROM DATHANG WHERE CanNhap <>0)tbCN ON tbCN.SoPhieu=PHIEUDATHANG.SoPhieu "
        sql &= " WHERE PHIEUDATHANG.PheDuyet=1 "


        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
            sql &= " AND Convert(datetime,convert(nvarchar,PHIEUDATHANG.Ngaythang,103),103) BETWEEN @TuNgay AND @DenNgay "
        End If

        If Not btNhanVien.EditValue Is Nothing Then
            sql &= " AND PHIEUDATHANG.IDUser= " & btNhanVien.EditValue
        End If

        If Not cbKH.EditValue Is Nothing Then
            sql &= " AND PHIEUDATHANG.IDKhachhang= " & cbKH.EditValue
        End If

        'If cbTieuChi.EditValue = "Top 100" Then
        sql &= " ORDER BY PHIEUDATHANG.NgayNhan "
        ' End If

        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If Not dt Is Nothing Then
            gdv.DataSource = dt
            If Not gdvCT.FocusedRowHandle < 0 Then
                If Not gdvCT.GetFocusedRowCellValue("Sophieu") Is Nothing Then
                    loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))
                End If

            Else
                gdvDH.DataSource = Nothing
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        CloseWaiting()

    End Sub

    Public Sub loadDSYCChiTiet(ByVal SoPhieu As Object)
        If SoPhieu Is Nothing Then
            gdvDH.DataSource = Nothing
            Exit Sub
        End If
        Dim sql As String = ""

        sql &= " SELECT DATHANG.SoPhieu,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.ThongSo,"
        sql &= " TENDONVITINH.Ten AS DVT,SoLuong,(Soluong-CanNhap)AS SLDaNhap,CanNhap AS SLCanNhap,ISNULL(DATHANG.Dongia,0)DonGia,(DATHANG.Dongia * DATHANG.Soluong) AS ThanhTien,DATHANG.NhapThue,DATHANG.MucThue,"
        sql &= " 0 AS AZ"
        sql &= " FROM DATHANG LEFT OUTER JOIN VATTU ON DATHANG.IDvattu=VATTU.ID"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE DATHANG.Sophieu=" & SoPhieu & ""
        sql &= " ORDER BY DATHANG.ID "

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            With dt
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                Next
            End With

            gdvDH.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub LoadrCbNhanVien()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not dt Is Nothing Then
            rCbNhanVien.DataSource = dt
            ' rcbNhanVienXuLy.DataSource = dt
            rcbNV.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadrCbKH()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG order by ttcMa")
        If Not dt Is Nothing Then
            rcbKH.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region


    Private Sub btXem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        gdvCT.ClearColumnsFilter()
        gdvCT.ClearSorting()
        LoadDS()
    End Sub

    Private Sub gdvCT_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        loadDSYCChiTiet(gdvCT.GetFocusedRowCellValue("Sophieu"))
    End Sub

    Private Sub rCbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            btNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbKH.ButtonClick
        If e.Button.Index = 1 Then
            cbKH.EditValue = Nothing
        End If
    End Sub

    Private Sub cbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChi.EditValueChanged
        If cbTieuChi.EditValue = "Tuỳ chỉnh" Then
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
        Else
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
        End If
    End Sub

    Private Sub gdvCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvCT.RowCellStyle
        If e.Column.FieldName = "CanhBao" Then
            If e.RowHandle < 0 Then Exit Sub
            If gdvCT.GetRowCellValue(e.RowHandle, "SoNgay") <= 0 Then
                e.Appearance.BackColor = Color.Red
            ElseIf gdvCT.GetRowCellValue(e.RowHandle, "SoNgay") > 0 And gdvCT.GetRowCellValue(e.RowHandle, "SoNgay") <= 3 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub btXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNhapKho.ItemClick, mLapPhieuNhap.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub

        Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu,NgayThang FROM PHIEUNHAPKHO WHERE SoPhieuDH='" & gdvCT.GetFocusedRowCellValue("Sophieu") & "' ORDER BY SoPhieu DESC")
        If Not tb Is Nothing Then
            If tb.Rows.Count > 0 Then
                If ShowCauHoi("Đã có nhập kho cho đơn hàng này, bạn có muốn mở phiếu nhập kho đã có không ?") Then
                    If tb.Rows.Count = 1 Then
                        TrangThai.isUpdate = True
                        fCNNhapKho = New frmCNNhapKho
                        fCNNhapKho.PhieuNK = tb.Rows(0)(0)
                        fCNNhapKho.Tag = Me.Parent.Tag
                        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                            fCNNhapKho.btCal.Enabled = False
                            fCNNhapKho.btGhi.Enabled = False
                            fCNNhapKho.btChuyenXK.Enabled = False
                            fCNNhapKho.mNhapKho.Enabled = False
                        End If
                        fCNNhapKho.ShowDialog()
                    Else
                        Dim f As New frmChonSoPhieu
                        f.gdv.DataSource = tb
                        f.Tag = Me.Parent.Tag
                        f.ShowDialog()
                    End If

                    Exit Sub
                End If

            End If
        End If


        TrangThai.isAddNew = True
        fCNNhapKho = New frmCNNhapKho
        fCNNhapKho.Tag = Me.Parent.Tag
        fCNNhapKho.gdvMaKH.EditValue = gdvCT.GetFocusedRowCellValue("IDKhachhang")
        fCNNhapKho.gdvPhieuDH.EditValue = gdvCT.GetFocusedRowCellValue("Sophieu")
        fCNNhapKho.ShowDialog()

    End Sub
End Class
