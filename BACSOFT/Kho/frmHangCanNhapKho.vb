Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraEditors
Imports BACSOFT.Utils

Public Class frmHangCanNhapKho
    Public _exit As Boolean = False
    Public _FileCGKinhDoanh As String = ""

    Private Sub frmDHCanNhapKho_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbTuNgay.Enabled = False
        tbDenNgay.EditValue = Today.Date
        tbDenNgay.Enabled = False
        cbTieuChi.EditValue = "Tất cả"
        Application.DoEvents()
        LoadrCbNhanVien()
        Application.DoEvents()
        LoadrCbKH()
        Application.DoEvents()
        LoadDS()
    End Sub

#Region "Load DS hàng cần nhập kho"

    Public Sub LoadDS()
        ShowWaiting("Đang tải dữ liệu...")
        Application.DoEvents()
        Dim sql As String = ""
        sql &= " SELECT convert(bit,0)isChecked, PHIEUDATHANG.NgayDat, DATHANG.SoPhieu,NCC.ttcMa as TenNCC,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS TenHang,VATTU.Model,VATTU.ThongSo,"
        sql &= " TENDONVITINH.Ten AS DVT,SoLuong,(Soluong-CanNhap)AS SLDaNhap,CanNhap AS SLCanNhap,ISNULL(DATHANG.Dongia,0)DonGia,(DATHANG.Dongia * DATHANG.Soluong) AS ThanhTien,DATHANG.NhapThue,DATHANG.MucThue,"
        sql &= " 0 AS AZ,NGUOIDAT.Ten as NguoiDat,PHIEUDATHANG.IDKhachHang,DATHANG.IDVatTu,DATHANG.ID"
        sql &= " FROM DATHANG LEFT OUTER JOIN VATTU ON DATHANG.IDvattu=VATTU.ID"
        sql &= " INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=DATHANG.SoPhieu"
        If btNhanVien.EditValue IsNot Nothing Then
            sql &= " AND PHIEUDATHANG.IDTakeCare=@IDNV"
            AddParameterWhere("@IDNV", btNhanVien.EditValue)
        End If

        If cbKH.EditValue IsNot Nothing Then
            sql &= " AND PHIEUDATHANG.IDKhachHang=@IDKH"
            AddParameterWhere("@IDKH", cbKH.EditValue)
        End If

        If cbTieuChi.EditValue <> "Tất cả" Then
            sql &= " AND Convert(datetime,Convert(nvarchar,PHIEUDATHANG.NgayDat,103),103) BETWEEN @TuNgay AND @DenNgay "
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If

        sql &= " LEFT JOIN NHANSU AS NGUOIDAT ON PHIEUDATHANG.IDTakeCare=NGUOIDAT.ID"
        sql &= " LEFT JOIN KHACHHANG AS NCC ON NCC.ID=PHIEUDATHANG.IDKhachHang"
        sql &= " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID"
        sql &= " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql &= " LEFT OUTER JOIN TENDONVITINH ON VATTU.IDDonvitinh=TENDONVITINH.ID"
        sql &= " WHERE CanNhap <> 0 "

        sql &= " ORDER BY DATHANG.ID "
        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If Not dt Is Nothing Then
            gdvDH.DataSource = dt
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
            '   rcbNV.DataSource = dt
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
        LoadDS()

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

    Private Sub gdvDHCT_CalcRowHeight(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs) Handles gdvDHCT.CalcRowHeight
        If e.RowHandle < 0 Then Exit Sub
        If e.RowHeight > 150 Then
            e.RowHeight = 150
        End If
    End Sub

    Private Sub gdvDHCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvDHCT.RowCellClick
        If e.Column.FieldName = "isChecked" Then
            '  e.CellValue = Not e.CellValue
            gdvDHCT.SetRowCellValue(e.RowHandle, e.Column.FieldName, Not e.CellValue)
            gdvDHCT.CloseEditor()
            gdvDHCT.UpdateCurrentRow()
        End If
    End Sub

    Private Sub btNhapKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNhapKho.ItemClick, mLapPhieuNhap.ItemClick
        If gdvDHCT.FocusedRowHandle < 0 Then Exit Sub
        Dim _Type As Integer = 0
        Dim dr() As DataRow = CType(gdvDH.DataSource, DataTable).Select("isChecked=1")
        If dr.Length = 0 Then
            If ShowCauHoi("Lập nhập kho cho tất cả mặt hàng cần nhập của ĐH: " & gdvDHCT.GetFocusedRowCellValue("SoPhieu") & " ?") Then
                ' _Type = 0
                Dim _SP As String = gdvDHCT.GetFocusedRowCellValue("SoPhieu")
                gdvDHCT.BeginUpdate()
                For i As Integer = 0 To gdvDHCT.RowCount - 1
                    If gdvDHCT.GetRowCellValue(i, "SoPhieu") = gdvDHCT.GetFocusedRowCellValue("SoPhieu") Then
                        gdvDHCT.SetRowCellValue(i, "isChecked", True)
                    End If
                Next
                gdvDHCT.CloseEditor()
                gdvDHCT.UpdateCurrentRow()
                gdvDHCT.EndUpdate()
            End If
        Else
            For i As Integer = 1 To dr.Length - 1
                If dr(i)("SoPhieu") <> dr(i - 1)("SoPhieu") Then
                    ShowCanhBao("Tính năng này chỉ áp dụng với một đặt hàng !")
                    Exit Sub
                End If
            Next
            ' _Type = 3
        End If

        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub

        ' dr = CType(gdvDH.DataSource, DataTable).Select("isChecked=1")
        TrangThai.isAddNew = True
        fCNNhapKho = New frmCNNhapKho
        fCNNhapKho.Tag = Me.Parent.Tag
        fCNNhapKho.gdvMaKH.EditValue = gdvDHCT.GetFocusedRowCellValue("IDKhachHang")
        fCNNhapKho.gdvPhieuDH.EditValue = gdvDHCT.GetFocusedRowCellValue("SoPhieu")
        Dim str As String = ""
        gdvDHCT.BeginUpdate()
        For i As Integer = 0 To gdvDHCT.RowCount - 1
            If gdvDHCT.GetRowCellValue(i, "isChecked") Then
                str &= gdvDHCT.GetRowCellValue(i, "ID").ToString() & ","
            End If
        Next
        If str.Length > 0 Then str = str.TrimEnd(",")
        gdvDHCT.EndUpdate()
        fCNNhapKho.strID = str
        fCNNhapKho.ShowDialog()
    End Sub

End Class
