Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmDatHangCanThanhToan
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False

    Private Sub frmVatTuDaDatHang_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadTuDien()

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
        '    btKetXuat.Visibility = XtraBars.BarItemVisibility.Never
        'End If

        'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
        '    btfilterTakecare.Enabled = False
        'End If
        'btfilterTakecare.EditValue = Convert.ToInt32(TaiKhoan)
    End Sub

#Region "Lọc vật tư"

    Public Sub LoadTuDien()
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa")
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

   

    Private Sub rcbTakecare_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakecare.ButtonClick
        If e.Button.Index = 1 Then
            btfilterTakecare.EditValue = Nothing
        End If
    End Sub



#End Region

    Private Sub LoadDaDatHang()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT  PHIEUNHAPKHO.Sophieu AS SoPhieuNK, PHIEUNHAPKHO.Ngaythang AS NgayNK,  ISNULL(tbChi.Sotien, 0) AS TienChi"
        sql &= " INTO #tbPhaiChi"
        sql &= " FROM PHIEUNHAPKHO "
        sql &= " INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=PHIEUNHAPKHO.SoPhieuDH"

        sql &= " LEFT JOIN (SELECT SoTien,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI"
        sql &= " UNION ALL "
        sql &= " SELECT SoTien,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC)tbChi ON PHIEUNHAPKHO.Sophieu = tbChi.PhieuTC1 OR PHIEUNHAPKHO.SophieuDH = tbChi.PhieuTC0 "
        sql &= " WHERE convert(datetime, Convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay"
        AddParameterWhere("@TuNgay", btfilterTuNgay.EditValue)
        AddParameterWhere("@DenNgay", btfilterDenNgay.EditValue)

        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND PHIEUNHAPKHO.IDKhachHang=@IDKH"
            AddParameterWhere("@IDKH", btfilterMaKH.EditValue)
        End If



        sql &= " SELECT KHACHHANG.ttcMa, PHIEUNHAPKHO.SoPhieu, PHIEUNHAPKHO.Ngaythang AS NgayNK,PHIEUNHAPKHO.SoPhieuDH, "
        sql &= "   (PHIEUNHAPKHO.Tientruocthue + PHIEUNHAPKHO.Tienthue) * PHIEUNHAPKHO.Tygia AS PhaiChi, ISNULL( tbDaChi.DaChi,0)DaChi, NHANSU.Ten AS PhuTrach"
        sql &= " FROM PHIEUNHAPKHO "
        'sql &= " INNER JOIN PHIEUDATHANG ON PHIEUDATHANG.SoPhieu=PHIEUNHAPKHO.SoPhieuDH"
        'If Not btfilterTakecare.EditValue Is Nothing Then
        '    sql &= " AND PHIEUDATHANG.IDTakeCare=@IDTK"
        '    AddParameterWhere("@IDTK", btfilterMaKH.EditValue)
        'End If
        sql &= " LEFT JOIN (SELECT SUM(TienChi)DaChi,SoPhieuNK FROM #tbPhaiChi GROUP BY SoPhieuNK)tbDaChi ON PHIEUNHAPKHO.SoPhieu = tbDaChi.SoPhieuNK"
        sql &= " LEFT JOIN KHACHHANG ON PHIEUNHAPKHO.IDKhachHang=KHACHHANG.ID"
        sql &= " LEFT JOIN PHIEUDATHANG ON PHIEUNHAPKHO.SoPhieuDH=PHIEUDATHANG.SoPhieu"
        sql &= " LEFT JOIN NHANSU ON PHIEUDATHANG.IDTakeCare=NHANSU.ID"
        sql &= " WHERE convert(datetime, Convert(nvarchar,PHIEUNHAPKHO.NgayThang,103),103) BETWEEN @TuNgay AND @DenNgay AND ( (PHIEUNHAPKHO.Tientruocthue + PHIEUNHAPKHO.Tienthue) * PHIEUNHAPKHO.Tygia <> tbDaChi.DaChi)"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND PHIEUNHAPKHO.IDKhachHang=@IDKH"

        End If



        sql &= " DROP table #tbPhaiChi "

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then

            gdv.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        LoadDaDatHang()
    End Sub



   
    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "DH can thanh toan " & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("dd-MM-yy") & "  " & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("dd-MM-yy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try

                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)

                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub


    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            menu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mDuKienThanhToan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDuKienThanhToan.ItemClick
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim f As New frmDuKienThanhToan
        f._SoPhieuCGDH = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieuDH")
        f._SoPhieuXNK = gdvCT.GetRowCellValue(gdvCT.FocusedRowHandle, "SoPhieu")
        f._PhaiTra = True
        f._Buoc1 = False
        f.ShowDialog()
    End Sub
End Class
