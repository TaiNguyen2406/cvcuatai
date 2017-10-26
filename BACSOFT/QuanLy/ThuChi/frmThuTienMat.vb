Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress

Public Class frmThuTienMat
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0

    Private Sub frmThuTienMat_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadTuDien()
        btTaiLai.PerformClick()
    End Sub

#Region "Load dữ liệu phụ"

    Public Sub LoadTuDien()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 "
        sql &= " SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa "
        sql &= " SELECT ltrim(rtrim(MaSo))MaSo,Ten FROM TAIKHOAN "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
            rcbSoTK.DataSource = ds.Tables(2)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub rcbMaKH_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btfilterMaKH.EditValue = Nothing
        End If
    End Sub


#End Region

    Public Sub LoadThu()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT THU.SoPhieuT, 0 AS STT, THU.ID,NgayThangVS,(N'TT ' + THU.SoPhieu) AS SoPhieu,NgayThangCT,KHACHHANG.ttcMa,THU.DienGiai,"
        sql &= " 	THU.SoTien,tblTienTe.Ten AS TienTe,MUCDICHTHUCHI.Ten AS MucDich,NguoiNop,"
        sql &= " (CASE PhieuTC0 WHEN N'000000000' THEN N'' ELSE N'CG ' + PhieuTC0 END) PhieuTC0,"
        sql &= " (CASE PhieuTC1 WHEN N'000000000' THEN N'' ELSE N'XK ' + PhieuTC1 END) PhieuTC1,"
        sql &= " (SELECT SoCT FROM CHUNGTU WHERE ID = THU.IdChungTu)SoPhieuThu,NHANSU.Ten as NguoiLap "
        sql &= " FROM THU"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=THU.IDKh"
        sql &= " LEFT JOIN tblTienTe ON tblTienTe.ID=THU.TienTe"
        sql &= " LEFT JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=THU.MucDich"
        sql &= " LEFT JOIN NHANSU ON NHANSU.ID=THU.IDUser"
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,THU.NgayThangCT,103),103) BETWEEN @TuNgay AND @DenNgay"
        If Not btfilterMaKH.EditValue Is Nothing Then
            sql &= " AND THU.IDKh=" & btfilterMaKH.EditValue
        End If
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND ltrim(rtrim(THU.MaTK))='" & cbSoTK.EditValue & "' "
        End If
        sql &= " ORDER BY NgayThangCT,SoPhieu"

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then

            For i As Integer = 0 To tb.Rows.Count - 1
                tb.Rows(i)("STT") = i + 1
            Next

            gdvThu.DataSource = tb

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadThu()

    End Sub



    'Private Sub chkRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkRutGon.CheckedChanged
    '    If chkRutGon.Checked = True Then
    '        chkRutGon.Glyph = My.Resources.Checked
    '    Else
    '        chkRutGon.Glyph = My.Resources.UnCheck
    '    End If
    'End Sub

    'Private Sub mXemChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGia.ItemClick
    '    If isShowing Then
    '        ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
    '        Exit Sub
    '    End If

    '    If gdvDaXuatCT.FocusedRowHandle < 0 Then Exit Sub

    '    TrangThai.isUpdate = True
    '    fCNChaoGia = New frmCNChaoGia
    '    'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
    '    fCNChaoGia.SPChaoGia = gdvDaXuatCT.GetFocusedRowCellValue("SoPhieuCG")
    '    fCNChaoGia.Tag = Me.Parent.Tag
    '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
    '        fCNChaoGia.gdvVTCT.OptionsBehavior.ReadOnly = True
    '        fCNChaoGia.gdvCongTrinhCT.OptionsBehavior.ReadOnly = True
    '        fCNChaoGia.btGhi.Enabled = False
    '        fCNChaoGia.tabChuyenMa.PageVisible = False
    '        fCNChaoGia.btTinhToan.Enabled = False
    '    End If

    '    fCNChaoGia.Show()
    'End Sub

    'Private Sub mXemXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemXuatKho.ItemClick
    '    If gdvDaXuatCT.FocusedRowHandle < 0 Then Exit Sub

    '    TrangThai.isUpdate = True
    '    fCNXuatKho = New frmCNXuatKho
    '    fCNXuatKho.PhieuXK = gdvDaXuatCT.GetFocusedRowCellValue("SoPhieu2")
    '    fCNXuatKho.Tag = Me.Parent.Tag
    '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
    '        fCNXuatKho.btCal.Enabled = False
    '        fCNXuatKho.btGhi.Enabled = False
    '        fCNXuatKho.btChuyenXK.Enabled = False
    '        fCNXuatKho.mXuatKho.Enabled = False
    '    End If
    '    fCNXuatKho.ShowDialog()
    'End Sub

   
    Private Sub mThemPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThemPhieuThu.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        ' TrangThai.isAddNew = True
        Dim f As New frmCNThu2
        f._TrangThai.isAddNew = True
        f.Tag = Me.Parent.Tag
        f.Text = "Thêm phiếu thu"
        f.Show()
    End Sub

    Private Sub mSuaPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaPhieuThu.ItemClick

        If gdvThuCT.FocusedRowHandle < 0 Then Exit Sub
        'TrangThai.isUpdate = True
        Dim _Index As Integer = gdvThuCT.FocusedRowHandle
        Dim f As New frmCNThu2
        f.Tag = Me.Parent.Tag
        f._TrangThai.isUpdate = True
        f.Text = "Cập nhật phiếu thu " & gdvThuCT.GetFocusedRowCellValue("SoPhieuT") '.ToString.Substring(3, 7)
        f.PhieuThu = gdvThuCT.GetFocusedRowCellValue("SoPhieuT").ToString '.Substring(3, 7)
        f.PhieuThuCT = gdvThuCT.GetFocusedRowCellValue("SoPhieu").ToString
        f.ThuNH = False
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then
            f.btGhi.Enabled = False
            f.btThem.Enabled = False
        End If
        f.Show()
        gdvThuCT.FocusedRowHandle = _Index

    End Sub

   

    Private Sub btInPhieuThu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btInPhieuThu.ItemClick
        If gdvThuCT.FocusedRowHandle < 0 Then Exit Sub

        Dim sql As String = ""
        sql = "SELECT SoPhieu,NgayThangCT,NguoiNop,DienGiai,SoTien,ChungTuGoc,(SELECT Ten FROM KHACHHANG WHERE ID=THU.IDKh)DiaChi,(SELECT Ten FROM NHANSU WHERE ID=THU.IDUser)NguoiLap FROM THU WHERE SoPhieu=@SoPhieu"
        AddParameterWhere("@SoPhieu", gdvThuCT.GetFocusedRowCellValue("SoPhieu").ToString.Substring(3, 9))
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            Dim f As New frmIn("In phiếu thu")
            Dim rpt As New rptPhieuThuChi
            rpt.pLogo.Image = My.Resources.Logo3
            rpt.lbTenPhieu.Text = "PHIẾU THU"
            rpt.lbNgay.Text = "Ngày: " & Convert.ToDateTime(tb.Rows(0)("NgayThangCT")).ToString("dd/MM/yyyy")
            rpt.lbSoPhieu.Text = "Số: " & tb.Rows(0)("SoPhieu")
            rpt.lbHoTen.Text = "Người nộp tiền: "
            rpt.lbHoTenV.Text = tb.Rows(0)("NguoiNop")
            rpt.lbDiaChiV.Text = tb.Rows(0)("DiaChi")
            rpt.lbLyDo.Text = "Lý do nộp: "
            rpt.lbLyDoV.Text = tb.Rows(0)("DienGiai")
            rpt.lbSoTienV.Text = String.Format("{0:N2}", tb.Rows(0)("SoTien"))
            rpt.lbBangChuV.Text = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
            rpt.lbKemTheoV.Text = tb.Rows(0)("ChungTuGoc")
            rpt.lbNguoiGd.Text = "Người nộp tiền"
            rpt.lbKyTenNgNhan.Text = tb.Rows(0)("NguoiNop")
            rpt.lbKTNguoiLap.Text = tb.Rows(0)("NguoiLap")
            rpt.CreateDocument()
            f.printControl.PrintingSystem = rpt.PrintingSystem
            f.ShowDialog()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub rcbSoTK_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbSoTK.ButtonClick
        If e.Button.Index = 1 Then
            cbSoTK.EditValue = Nothing
        End If
    End Sub

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub




    Private Sub btNhapChiPhiTuExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNhapChiPhiTuExcel.ItemClick
        Dim f As New frmNhapChiPhi
        f.ShowDialog()
        'f.btChonFileExcel.PerformClick()
    End Sub


    Private Sub pMenuThu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuThu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvThuCT.CalcHitInfo(gdvThu.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gdvThuCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvThuCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuThu.ShowPopup(gdvThu.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mXemPhieuTC0_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemPhieuTC0.ItemClick
        If gdvThuCT.FocusedRowHandle < 0 Then Exit Sub
        If gdvThuCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Length > 0 Then
            If gdvThuCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(0, 2) = "CG" Then
                'If gdvThuCT.GetFocusedRowCellValue("TrangThai") = TrangThaiChaoGia.DaXacNhan Or gdvThuCT.GetFocusedRowCellValue("IDTakeCare") <> TaiKhoan Then
                '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mChaoGia.Name, DanhMucQuyen.TPKinhDoanh) Then
                '        ShowCanhBao("Bạn cần có quyền TP Kinh doanh hoặc Admin để sửa chào giá đã xác nhận hoặc chào giá của nv khác!")
                '        Exit Sub
                '    End If
                'End If

                TrangThai.isUpdate = True
                fCNChaoGia = New frmCNChaoGia
                fCNChaoGia.TrangThaiCG.isUpdate = True
                'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
                fCNChaoGia.SPChaoGia = gdvThuCT.GetFocusedRowCellValue("PhieuTC0").ToString.Trim.Substring(3, 9)
                fCNChaoGia.Tag = deskTop.mChaoGia.Name
                fCNChaoGia.btGhi.Enabled = False
                fCNChaoGia.Show()
            End If
        End If
    End Sub

    Private Sub mXemPhieuTC1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemPhieuTC1.ItemClick

        If gdvThuCT.FocusedRowHandle < 0 Then Exit Sub

        If gdvThuCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Length > 0 Then
            If gdvThuCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(0, 2) = "XK" Then
                TrangThai.isUpdate = True
                fCNXuatKho = New frmCNXuatKho
                fCNXuatKho.PhieuXK = gdvThuCT.GetFocusedRowCellValue("PhieuTC1").ToString.Trim.Substring(3, 9)
                fCNXuatKho.Tag = deskTop.mXuatKho.Name
                If Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", deskTop.mXuatKho.Name, DanhMucQuyen.QuyenSua) Then
                    fCNXuatKho.btGhi.Enabled = False
                    fCNXuatKho.btChuyenXK.Enabled = False
                    fCNXuatKho.btCal.Enabled = False
                    fCNXuatKho.btTichThue.Enabled = False
                    fCNXuatKho.mChonBoChon.Enabled = False
                End If
                fCNXuatKho.ShowDialog()
            End If
            
        End If
        
    End Sub
End Class
