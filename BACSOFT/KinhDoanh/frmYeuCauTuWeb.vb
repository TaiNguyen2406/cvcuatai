Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Public Class frmYeuCauTuWeb
    Private _exit As Boolean

    Private Sub frmYeuCauTuWeb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        dtTuNgay.Enabled = False
        dtDenNgay.EditValue = Today.Date
        dtDenNgay.Enabled = False
        btFilterTakeCarer.EditValue = Convert.ToInt32(TaiKhoan)
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btFilterTakeCarer.Enabled = True
            btFilterTakeCarer.EditValue = Nothing
        End If
        cbTieuChiLoc.EditValue = "Top 100"
        loaddulieu()
        LoadrcbTakeCare()
    End Sub

    Private Sub LoadrcbTakeCare()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
        If Not dt Is Nothing Then
            rcbTakeCare.DataSource = dt
            rcbNhanVien.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadTuDien()
        Dim sql As String = "SELECT ID,Ten FROM NHANSU WHERE Noictac <> 74"
        sql &= " SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=0 ORDER BY Ma"
        Dim dt As DataSet
        dt = ExecuteSQLDataSet(sql)
        If Not dt Is Nothing Then
            rcbNguoiGD.DataSource = dt.Tables(0)
            rcbKetQua.DataSource = dt.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
    Public Sub loaddulieu()
        Dim str As String = "SET DATEFORMAT DMY SELECT"
        If cbTieuChiLoc.EditValue = "Top 100" Then
            str &= " TOP 100 "
        End If
        str &= " yc.ID, yc.Email, yc.NguoiGD, yc.NguoiGiao,  yc.TenKH, yc.NoiDung, yc.SDT, yc.Sophieu, yc.TakeCare, yc.NguonYC, yc.ThoiGianNhan, yc.IdWeb, yc.FileDinhKem, "
        str &= " yc.SoYCDen , yc.PhanHoi, yc.MaKH, yc.DiaChi, yc.MaKH, (SELECT TOP 1 LS.LyDo FROM LichSuChuyenGiaoYCWeb LS WHERE IDYCWeb = YC.ID ORDER BY NgayChuyen DESC) LyDo, '' as ThoiGianChuyen1, "
        str &= " DATEDIFF(SECOND, ThoiGianNhan, NgayGiao) as ThoiGianChuyen, case yc.TrangThai  when 0 then N'Chờ giao XL'  when 1 then N'Đã giao xử lý'  when 2 then N'Đã XL'  when 3 then N'Hủy' end  as TrangThai, "
        str &= " ( case when SoYCDen IS  NULL then N'Chưa xử lý'"
        str &= " else case when (SELECT COUNT(ID)  FROM BANGCHAOGIA WHERE Masodathang = SoYCDen) = 0 THEN "
        str &= " (SELECT NoiDung From tblTuDien WHERE Loai = 0 and Ma = (Select TrangThai from BANGYEUCAU WHERE Sophieu = SoYCDen)) else"
        str &= " (SELECT NoiDung From tblTuDien WHERE Loai = 2 and Ma = (SELECT TOP 1 TrangThai FROM BANGCHAOGIA WHERE Masodathang = SoYCDen ORDER BY Ngaythang ASC )) End End )KetQua"
        str &= " FROM YEUCAUTUWEB yc"
        If cbTieuChiLoc.EditValue = "Tùy chỉnh" Then
            AddParameterWhere("@TuNgay", dtTuNgay.EditValue)
            AddParameterWhere("@DenNgay", dtDenNgay.EditValue)
            str &= " WHERE CONVERT(nvarchar,ThoiGianNhan,103) BETWEEN @TuNgay AND @DenNgay "
            If Not btFilterTakeCarer.EditValue Is Nothing Then
                str &= " and TakeCare = " & btFilterTakeCarer.EditValue & ""
            End If
        Else
            If Not btFilterTakeCarer.EditValue Is Nothing Then
                str &= " where TakeCare = " & btFilterTakeCarer.EditValue
            End If
        End If

        If cbTieuChiLoc.EditValue = "Top 100" Then
            str &= " ORDER BY ThoiGianNhan DESC"
        End If
        Dim dt As New DataTable
        dt = ExecuteSQLDataTable(str)
        If dt Is Nothing Then ShowBaoLoi(LoiNgoaiLe)
        gdvYCWeb.DataSource = dt
        For i = 0 To gdvYCWebCT.RowCount - 1
            If Not gdvYCWebCT.GetRowCellValue(i, "ThoiGianChuyen") Is Nothing And Not IsDBNull(gdvYCWebCT.GetRowCellValue(i, "ThoiGianChuyen")) Then
                Dim _t As New TimeSpan(0, 0, gdvYCWebCT.GetRowCellValue(i, "ThoiGianChuyen"))
                gdvYCWebCT.SetRowCellValue(i, "ThoiGianChuyen1", _t.Days * 24 + _t.Hours & "h" & _t.Minutes & "p")
            End If
        Next
        LoadTuDien()
    End Sub

    Private Sub btNhapYC_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNhapYC.ItemClick
        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            Dim frm As New frmCNYeuCauTuWeb
            frm.objTrangThai.isAddNew = True
            frm.ShowDialog()
            loaddulieu()
        Else
            ShowBaoLoi("Bạn không có quyền thực hiện chức năng này. ")
        End If
       
    End Sub

    Private Sub btnSua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnSua.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            ShowBaoLoi("Bạn không có quyền thực hiện chức năng này. ")
            Exit Sub
        End If
        If gdvYCWebCT.FocusedRowHandle < -1 Then
            Exit Sub
        End If
        If (gdvYCWebCT.GetFocusedRowCellValue("NguonYC") <> "Webmail") Then
            ShowThongBao("Không thể sửa yêu cầu từ website")
            Exit Sub
        End If
        If (gdvYCWebCT.GetFocusedRowCellValue("TrangThai") <> "Chờ giao XL") Then
            ShowThongBao("Đã giao xử lý. Không thể chỉnh sửa")
            Exit Sub
        End If
        Dim frm As New frmCNYeuCauTuWeb
        frm._id = gdvYCWebCT.GetFocusedRowCellValue("ID")
        frm.objTrangThai.isAddNew = False
        frm.objTrangThai.isUpdate = True
        frm.ShowDialog()
        loaddulieu()
    End Sub

    Private Sub btnTaiDS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDS.ItemClick
        loaddulieu()
    End Sub

    Private Sub rcbTakeCare_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbTakeCare.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            btFilterTakeCarer.EditValue = Nothing
        End If
    End Sub

    Private Sub cbTieuChiLoc_EditValueChanged(sender As Object, e As EventArgs) Handles cbTieuChiLoc.EditValueChanged
        If cbTieuChiLoc.EditValue = "Tùy chỉnh" Then
            dtTuNgay.Enabled = True
            dtDenNgay.Enabled = True
        Else
            dtTuNgay.Enabled = False
            dtDenNgay.Enabled = False
        End If
    End Sub

    Private Sub btnLapYCDen_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLapYCDen.ItemClick
        If gdvYCWebCT.FocusedRowHandle < -1 Then
            Exit Sub
        End If
        If Not gdvYCWebCT.GetFocusedRowCellValue("SoYCDen") Is Nothing And Not IsDBNull(gdvYCWebCT.GetFocusedRowCellValue("SoYCDen")) Then
            'Dim dt As New DataTable
            'dt = ExecuteSQLDataTable("SELECT TrangThai From BANGYEUCAU WHERE Sophieu = '" & gdvYCWebCT.GetFocusedRowCellValue("SoYCDen") & "'")
            'If dt Is Nothing Then Throw New Exception(LoiNgoaiLe)
            'If dt.Rows(0)(0) <> 7 Then
            If gdvYCWebCT.GetFocusedRowCellValue("KetQua") <> "Hủy" Then
                ShowCanhBao("Đã lập yêu cầu đến cho yêu cầu này! ")
                Exit Sub
            End If
            
            'End If
        End If

        If gdvYCWebCT.GetFocusedRowCellValue("TakeCare") Is Nothing Or IsDBNull(gdvYCWebCT.GetFocusedRowCellValue("TakeCare")) Then
            ShowCanhBao("Chưa giao việc. Không thể lập yêu cầu đến")
            Exit Sub
        End If

        If Convert.ToInt32(TaiKhoan) <> gdvYCWebCT.GetFocusedRowCellValue("TakeCare") Then
            ShowCanhBao("Bạn không thể lập yêu cầu đến cho đơn hàng này!")
            Exit Sub
        End If

        If gdvYCWebCT.GetFocusedRowCellValue("TakeCare") Is Nothing Or IsDBNull(gdvYCWebCT.GetFocusedRowCellValue("TakeCare")) Then
            ShowCanhBao("Chưa giao việc. Không thể lập yêu cầu đến")
            Exit Sub
        End If
        Dim frm As New frmThemYeuCau
        frm._YeuCauWeb = gdvYCWebCT.GetFocusedRowCellValue("ID")
        TrangThai.isAddNew = True
        MaTuDien = -1
        frm.Tag = Me.Tag
        frm._tag = Me.Parent.Tag
        frm.ShowDialog()
        loaddulieu()
    End Sub

    Private Sub btnGiaoXuLy_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGiaoXuLy.ItemClick
        If gdvYCWebCT.FocusedRowHandle < -1 Then
            Exit Sub
        End If
        If (gdvYCWebCT.GetFocusedRowCellValue("TrangThai") = "Hủy") Then
            ShowCanhBao("Yêu cầu đã hủy. Bạn không thể thực hiện chức năng này")
            Exit Sub
        End If
        If (gdvYCWebCT.GetFocusedRowCellValue("TrangThai") <> "Chờ giao XL") Then
            ShowThongBao("Đã giao việc. Bạn không thể thực hiện chức năng này")
            Exit Sub
        End If
        'If (gdvYCWebCT.GetFocusedRowCellValue("NguoiGD") Is Nothing Or IsDBNull(gdvYCWebCT.GetFocusedRowCellValue("NguoiGD"))) Then
        '    ShowCanhBao("Chưa có người giao dịch. Không thể giao việc")
        '    Exit Sub
        'End If
        Dim frm As New frmGiaoXuLy
        frm.Text = "Giao xử lý yêu cầu"
        frm._Chuyengiao = False
        frm._email = gdvYCWebCT.GetFocusedRowCellValue("Email")
        frm._sdt = gdvYCWebCT.GetFocusedRowCellValue("SDT")
        Dim str As String
        str = " TÊN KH: " & gdvYCWebCT.GetFocusedRowCellValue("TenKH") & vbCrLf & vbCrLf
        str &= " NGƯỜI GIAO DỊCH: " & gdvYCWebCT.GetFocusedRowCellValue("NguoiGD") & vbCrLf & vbCrLf
        str &= " EMAIL " & gdvYCWebCT.GetFocusedRowCellValue("Email") & ". SĐT: " & gdvYCWebCT.GetFocusedRowCellValue("SDT") & vbCrLf & vbCrLf
        str &= " NỘI DUNG YÊU CẦU: " & gdvYCWebCT.GetFocusedRowCellValue("NoiDung").ToString().Replace(Chr(10), vbCrLf)
        frm._NoiDung = str
        frm._SoYC = gdvYCWebCT.GetFocusedRowCellValue("Sophieu")
        frm._id = gdvYCWebCT.GetFocusedRowCellValue("ID")
        frm.ShowDialog()
        loaddulieu()
    End Sub

    Private Sub btnChuyenGiao_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnChuyenGiao.ItemClick
        If gdvYCWebCT.FocusedRowHandle < -1 Then
            Exit Sub
        End If
        If (gdvYCWebCT.GetFocusedRowCellValue("TakeCare") Is Nothing Or IsDBNull(gdvYCWebCT.GetFocusedRowCellValue("TakeCare"))) Then
            ShowCanhBao("Chưa giao việc. Không thể chuyển giao")
            Exit Sub
        End If
        If (gdvYCWebCT.GetFocusedRowCellValue("TrangThai") = "Hủy") Then
            ShowCanhBao("Yêu cầu đã hủy. Bạn không thể thực hiện chức năng này")
            Exit Sub
        End If
        If (gdvYCWebCT.GetFocusedRowCellValue("TakeCare") <> Convert.ToInt32(TaiKhoan)) Then
            ShowCanhBao("Bạn không có quyền thực hiện chức năng này")
            Exit Sub
        End If
      
        Dim frm As New frmGiaoXuLy
        frm._Chuyengiao = True
        frm.Text = "Chuyển giao xử lý yêu cầu"
        frm._email = gdvYCWebCT.GetFocusedRowCellValue("Email")
        frm._sdt = gdvYCWebCT.GetFocusedRowCellValue("SDT")
        Dim str As String
        str = " TÊN KH: " & gdvYCWebCT.GetFocusedRowCellValue("TenKH") & vbCrLf & vbCrLf
        str &= " NGƯỜI GIAO DỊCH: " & gdvYCWebCT.GetFocusedRowCellValue("NguoiGD") & vbCrLf & vbCrLf
        str &= " EMAIL: " & gdvYCWebCT.GetFocusedRowCellValue("Email") & ". SĐT: " & gdvYCWebCT.GetFocusedRowCellValue("SDT") & vbCrLf & vbCrLf
        str &= " NỘI DUNG YÊU CẦU: " & gdvYCWebCT.GetFocusedRowCellValue("NoiDung").ToString().Replace(Chr(10), vbCrLf)
        frm._NoiDung = str
        frm._SoYC = gdvYCWebCT.GetFocusedRowCellValue("Sophieu")
        frm._id = gdvYCWebCT.GetFocusedRowCellValue("ID")
        frm.ShowDialog()
        loaddulieu()

    End Sub


    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnHuy.ItemClick
        If gdvYCWebCT.FocusedRowHandle < -1 Then
            Exit Sub
        End If

        If gdvYCWebCT.GetFocusedRowCellValue("TrangThai") = "Đã XL" Then
            ShowCanhBao("Bạn không thể hủy yêu cầu này")
            Exit Sub
        End If
   
        If ShowCauHoi("Hủy yêu cầu sẽ không thể khôi phục được. Bạn vẫn tiếp tục? ") Then
            gdvYCWebCT.SetFocusedRowCellValue("TrangThai", "Hủy")
            AddParameter("@TrangThai", "3")
            AddParameterWhere("@ID", gdvYCWebCT.GetFocusedRowCellValue("ID"))
            If doUpdate("YeuCauTuWeb", "ID = @ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Hủy yêu cầu thành công")
            End If
        End If
    End Sub

    Private Sub gdvYCWebCT_MouseDown(sender As Object, e As MouseEventArgs) Handles gdvYCWebCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pmenu.ShowPopup(gdvYCWeb.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDongBo.ItemClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.KiemDuyet) Then
            Exit Sub
        End If
        Dim dlg As New DevExpress.Utils.WaitDialogForm
        dlg.AutoSize = True
        dlg.TopMost = True
        dlg.Show()

        Try

            Dim dbOnline As New DbWebBaoAn()

            '*********************************************************************
            '***** LAY DU LIEU LOCAL *********************************************
            '*********************************************************************
            dlg.Caption = "Đang so sánh dữ liệu liên hệ ..."
            Application.DoEvents()
            Dim str As String = "SET DATEFORMAT DMY SELECT "
            If cbTieuChiLoc.EditValue = "Top 100" Then
                str &= " TOP 100 IdWeb FROM YeuCauTuWeb ORDER BY ThoiGianNhan DESC "
            Else
                str &= " IdWeb FROM YeuCauTuWeb "
                str &= " WHERE Convert(datetime,CONVERT(nvarchar,ThoiGianNhan,103),103) BETWEEN @TuNgay AND @DenNgay "
                'str &= " ORDER BY ThoiGianNhan DESC "
                Dim tungay As DateTime = dtTuNgay.EditValue
                Dim denngay As DateTime = dtDenNgay.EditValue
                AddParameter("@TuNgay", tungay)
                AddParameter("@DenNgay", denngay)
            End If
            Dim dtLocal As DataTable = ExecuteSQLDataTable(str)


            '*********************************************************************
            '***** LAY DU LIEU LIEN HE *******************************************
            '*********************************************************************
            Application.DoEvents()
            'Đồng bộ dữ liệu từ bảng liên hệ trên website
            str = "SET DATEFORMAT DMY SELECT "
            If cbTieuChiLoc.EditValue = "Top 100" Then
                str &= " TOP 100 ID FROM THULIENHE ORDER BY NgayGui DESC "
            Else
                str &= " ID FROM THULIENHE "
                str &= " WHERE Convert(datetime,CONVERT(nvarchar,NgayGui,103),103) BETWEEN @TuNgay AND @DenNgay "
                'str &= " ORDER BY ThoiGianNhan DESC "
                Dim tungay As DateTime = dtTuNgay.EditValue
                Dim denngay As DateTime = dtDenNgay.EditValue
                dbOnline.AddParameter("@TuNgay", tungay)
                dbOnline.AddParameter("@DenNgay", denngay)
            End If
            Dim dtLienHe As DataTable = dbOnline.ExecuteSQLDataTable(str)
            Application.DoEvents()
            Dim index As Integer = 1
            For Each r As DataRow In dtLienHe.Rows
                Application.DoEvents()
                dlg.Caption = "Đồng bộ liên hệ " & index & " / " & dtLienHe.Rows.Count & " ..."
                If dtLocal.Select("IdWeb=" & r("ID")).Length > 0 Then
                    Continue For
                Else
                    dbOnline.AddParameter("@id", r("ID"))
                    Dim sql As String = "select Id,HoTen,NoiCongTac,SoDT,Email,TieuDe + ': ' + NoiDung as NoiDung, NgayGui from  THULIENHE where id = @id "
                    Dim rS As DataRow = dbOnline.ExecuteSQLDataTable(sql).Rows(0)
                    Dim sophieu As String = LaySoPhieu("YeuCauTuWeb")
                    AddParameter("@Sophieu", sophieu)
                    AddParameter("@TenKH", rS("NoiCongTac").ToString)
                    AddParameter("@SDT", rS("SoDT").ToString)
                    AddParameter("@Email", rS("Email").ToString)
                    AddParameter("@NoiDung", rS("NoiDung").ToString)
                    AddParameter("@NguoiGD", rS("HoTen").ToString)
                    AddParameter("@ThoiGianNhan", rS("NgayGui"))
                    AddParameter("@IdWeb", rS("Id"))
                    AddParameter("@NguonYC", "Web Liên Hệ")
                    doInsert("YeuCauTuWeb")
                End If
                index += 1
            Next


            '*********************************************************************
            '***** LAY DU LIEU DAT HANG ******************************************
            '*********************************************************************
            Application.DoEvents()
            dlg.Caption = "Đang so sánh dữ liệu đặt hàng ..."
            Application.DoEvents()
            'Đồng bộ dữ liệu từ bảng liên hệ trên website
            str = "SET DATEFORMAT DMY SELECT "
            If cbTieuChiLoc.EditValue = "Top 100" Then
                str &= " TOP 100 ID FROM DATHANG ORDER BY NgayDat DESC "
            Else
                str &= " ID FROM DATHANG "
                str &= " WHERE Convert(datetime,CONVERT(nvarchar,NgayDat,103),103) BETWEEN @TuNgay AND @DenNgay "
                'str &= " ORDER BY ThoiGianNhan DESC "
                Dim tungay As DateTime = dtTuNgay.EditValue
                Dim denngay As DateTime = dtDenNgay.EditValue
                dbOnline.AddParameter("@TuNgay", tungay)
                dbOnline.AddParameter("@DenNgay", denngay)
            End If
            Dim dtDatHang As DataTable = dbOnline.ExecuteSQLDataTable(str)
            index = 1
            For Each r As DataRow In dtDatHang.Rows
                Application.DoEvents()
                dlg.Caption = "Đồng bộ đặt hàng " & index & " / " & dtDatHang.Rows.Count & " ..."
                If dtLocal.Select("IdWeb=" & r("ID")).Length > 0 Then
                    Continue For
                Else
                    dbOnline.AddParameter("@id", r("ID"))
                    Dim sql As String = "select Id,HoTen,NoiCongTac,DienThoai,Email,NoiDung, NgayDat from  DATHANG where id = @id "
                    Dim rS As DataRow = dbOnline.ExecuteSQLDataTable(sql).Rows(0)
                    Dim sophieu As String = LaySoPhieu("YeuCauTuWeb")
                    AddParameter("@Sophieu", sophieu)
                    AddParameter("@TenKH", rS("NoiCongTac").ToString)
                    AddParameter("@SDT", rS("DienThoai").ToString)
                    AddParameter("@Email", rS("Email").ToString)
                    AddParameter("@NoiDung", rS("NoiDung").ToString)
                    AddParameter("@NguoiGD", rS("HoTen").ToString)
                    AddParameter("@ThoiGianNhan", rS("NgayDat"))
                    AddParameter("@IdWeb", rS("Id"))
                    AddParameter("@NguonYC", "Web Đặt Hàng")
                    Dim idYC As Object = doInsert("YeuCauTuWeb")
                    If idYC Is Nothing Then Continue For
                    'DONG BO CHI TIET YEU CAU --------------------------------------
                    dbOnline.AddParameter("@IdDonHang", r("ID"))
                    sql = "SELECT TenSP,TenModel,SoLuong FROM DATHANGCHITIET WHERE IdDonHang = @IdDonHang"
                    Dim dtDonHangCT As DataTable = dbOnline.ExecuteSQLDataTable(sql)
                    For Each rSx As DataRow In dtDonHangCT.Rows
                        Application.DoEvents()
                        AddParameter("@IdYc", idYC)
                        AddParameter("@TenSanPham", rSx("TenSP").ToString)
                        AddParameter("@Model", rSx("TenModel").ToString)
                        AddParameter("@SoLuong", rSx("SoLuong"))
                        doInsert("YeuCauTuWebCT")
                    Next
                    index += 1
                End If
            Next

            ShowAlert("Đã đồng bộ dữ liệu")
            loaddulieu()
        Catch ex As Exception
            dlg.Close()
            ShowBaoLoi(ex.Message)
        Finally
            dlg.Close()
        End Try
    End Sub

    Private Sub btnPhanHoi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPhanHoi.ItemClick
        If gdvYCWebCT.GetFocusedRowCellValue("Sophieu") Is Nothing Or IsDBNull(gdvYCWebCT.GetFocusedRowCellValue("Sophieu")) Then
            Exit Sub
        End If
        If gdvYCWebCT.GetFocusedRowCellValue("TakeCare") Is Nothing Or IsDBNull(gdvYCWebCT.GetFocusedRowCellValue("TakeCare")) Then
            ShowCanhBao("Yêu cầu chưa được xử lý. Bạn không thể phản hồi. ")
            Exit Sub
        End If
        If (gdvYCWebCT.GetFocusedRowCellValue("TakeCare") <> Convert.ToInt32(TaiKhoan)) Then
            ShowCanhBao("Bạn không có quyền thực hiện chức năng này")
            Exit Sub
        End If
        Dim frm As New frmCNPhanHoi
        frm._soyc = gdvYCWebCT.GetFocusedRowCellValue("Sophieu")
        frm._phanhoi = gdvYCWebCT.GetFocusedRowCellValue("PhanHoi")
        frm.ShowDialog()
        loaddulieu()
    End Sub

    Private Sub rbFile_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) 'Handles rbFile.ButtonClick
        'If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
        '    Dim openFile As New OpenFileDialog
        '    openFile.Multiselect = True
        '    Dim path As String = RootUrlOld & Convert.ToDateTime(gdvYCWebCT.GetFocusedRowCellValue("ThoiGianNhan")).Year.ToString & "\" & UrlKinhDoanh & "ONLINE"

        '    If openFile.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        If Not System.IO.Directory.Exists(path) Then
        '            Try
        '                System.IO.Directory.CreateDirectory(path)
        '                'path = RootUrlOld & Convert.ToDateTime(gdvYCWebCT.GetFocusedRowCellValue("ThoiGianNhan")).Year.ToString & "\" & UrlKinhDoanh & "YEU CAU TU WEB\"
        '            Catch ex As Exception
        '                ShowBaoLoi(ex.Message)
        '                Exit Sub
        '            End Try
        '        End If

        '        If System.IO.File.Exists(path & "\" & gdvYCWebCT.GetFocusedRowCellValue("FileDinhKem")) Then
        '            Try
        '                IO.File.Delete(path & "\" & gdvYCWebCT.GetFocusedRowCellValue("FileDinhKem"))
        '            Catch ex As Exception
        '                ShowBaoLoi(ex.Message)
        '                Exit Sub
        '            End Try
        '        End If
        '        For Each File In openFile.FileNames
        '            Try
        '                IO.File.Copy(File, path & "\" & gdvYCWebCT.GetFocusedRowCellValue("ID") & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(File), True)
        '                gdvYCWebCT.SetFocusedRowCellValue("FileDinhKem", gdvYCWebCT.GetFocusedRowCellValue("ID") & " " & TaiKhoan.ToString & " " & IO.Path.GetFileName(File))
        '                AddParameter("@FileDinhKem", gdvYCWebCT.GetFocusedRowCellValue("FileDinhKem"))
        '                AddParameterWhere("@idyc", gdvYCWebCT.GetFocusedRowCellValue("ID"))
        '                doUpdate("YeuCauTuWeb", "id = @idyc")
        '                ShowAlert("Upload File thành công")
        '            Catch ex As Exception
        '                ShowBaoLoi(ex.Message)
        '                Exit Sub
        '            End Try
        '        Next
        '        gdvYCWebCT.CloseEditor()
        '        gdvYCWebCT.UpdateCurrentRow()
        '    End If
        'End If
        'If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
        '    If System.IO.File.Exists(RootUrlOld & Convert.ToDateTime(gdvYCWebCT.GetFocusedRowCellValue("ThoiGianNhan")).Year.ToString & "\" & UrlKinhDoanh & "YEU CAU TU WEB\" & gdvYCWebCT.GetFocusedRowCellValue("FileDinhKem")) Then
        '        Try
        '            If ShowCauHoi("Bạn có chắc chắn muốn xóa file này không? ") Then
        '                IO.File.Delete(RootUrlOld & Convert.ToDateTime(gdvYCWebCT.GetFocusedRowCellValue("ThoiGianNhan")).Year.ToString & "\" & UrlKinhDoanh & "YEU CAU TU WEB\" & gdvYCWebCT.GetFocusedRowCellValue("FileDinhKem"))
        '                gdvYCWebCT.SetFocusedRowCellValue("FileDinhKem", "")
        '                AddParameter("@FileDinhKem", "")
        '                AddParameterWhere("@idyc", gdvYCWebCT.GetFocusedRowCellValue("ID"))
        '                doUpdate("YeuCauTuWeb", "id = @idyc")
        '                ShowAlert("Xóa file thành công")
        '            End If
        '        Catch ex As Exception
        '            ShowBaoLoi(ex.Message)
        '        End Try
        '    End If
        'End If
        'loaddulieu()
    End Sub

    Private Sub gdvYCWebCT_CalcRowHeight(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs) Handles gdvYCWebCT.CalcRowHeight
        If e.RowHeight > 80 Then
            e.RowHeight = 80
        End If
    End Sub

    'Private Sub gdvYCWebCT_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles gdvYCWebCT.ShowingEditor
    '    If gdvYCWebCT.FocusedColumn.FieldName <> "NoiDung" Then
    '        e.Cancel = True
    '    End If
    'End Sub

    Private Sub menuPhanHoi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles menuPhanHoi.ItemClick
        btnPhanHoi.PerformClick()
    End Sub

    Private Sub gdvYCWebCT_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) 'Handles gdvYCWebCT.RowCellClick
        If (e.RowHandle >= 0) Then
            If e.Column.FieldName = "FileDinhKem" Then
                If e.CellValue Is Nothing Then Exit Sub
                If e.CellValue.ToString = "" Then Exit Sub
                If Not ShowCauHoi("Bạn muốn xem file này? ") Then Exit Sub
                ShowWaiting("Đang mở file...")

                If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
                End If
                Application.DoEvents()
                Dim fileName As String = RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(gdvYCWebCT.GetFocusedRowCellValue("ThoiGianNhan")).Year.ToString & "\" & e.CellValue

                Try
                    System.IO.File.Copy(fileName, Application.StartupPath & "\tmp\" & e.CellValue, True)
                Catch ex As Exception
                    CloseWaiting()
                    ShowBaoLoi(ex.Message)
                    Exit Sub
                End Try

                CloseWaiting()

                Dim psi As New ProcessStartInfo()
                With psi
                    .FileName = Application.StartupPath & "\tmp\" & e.CellValue
                    .UseShellExecute = True
                End With
                Try
                    Process.Start(psi)
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
            End If

        End If
    End Sub

    Private Sub rPopupFile_Popup(sender As Object, e As EventArgs) Handles rPopupFile.Popup
        LoadDSFileDinhKem(CType(sender, PopupContainerEdit).EditValue)
    End Sub

    Private Sub LoadDSFileDinhKem(ByVal listFile As Object)
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
        If listFile Is Nothing Then Exit Sub
        Dim listUrl() As String = listFile.ToString.Split(New Char() {";"c})
        For Each _url In listUrl
            If _url <> "" Then
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("File", _url)
            End If
        Next
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
    End Sub

    Private Sub rPopupFile_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rPopupFile.Closed
        Dim _File As String = ""
        For i As Integer = 0 To gdvListFileCT.RowCount - 1
            _File &= gdvListFileCT.GetRowCellValue(i, "File")
            If i < gdvListFileCT.RowCount - 1 Then
                _File &= ";"
            End If
        Next
        AddParameter("@FileDinhKem", _File)
        AddParameterWhere("@ID", gdvYCWebCT.GetFocusedRowCellValue("ID"))
        If doUpdate("YeuCauTuWeb", "ID=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            CType(sender, PopupContainerEdit).EditValue = _File
        End If
    End Sub


    Private Sub gdvListFileCT_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(RootUrlOld & UrlKinhDoanh & "ONLINE\" & Convert.ToDateTime(gdvYCWebCT.GetFocusedRowCellValue("ThoiGianNhan")).Year.ToString & "\" & e.CellValue, e.CellValue, True)
        End If
    End Sub

    Private Sub gdvYCWebCT_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvYCWebCT.FocusedRowChanged
        If e.FocusedRowHandle > -1 Then
            Dim sql As String
            sql = "SELECT TakeCareGiao, TakeCareNhan, NgayChuyen, LyDo FROM LichSuChuyenGiaoYCWeb WHERE IDYCWeb = @IDWeb"
            sql &= " SELECT  TenSanPham, Model, SoLuong FROM YeuCauTuWebCT WHERE IdYc = @IDWeb"
            AddParameterWhere("@IDWeb", gdvYCWebCT.GetRowCellValue(e.FocusedRowHandle, "ID"))
            Dim dt As New DataSet
            dt = ExecuteSQLDataSet(sql)
            If dt Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            gdvLSChuyenGiao.DataSource = dt.Tables(0)
            rcbNV.DataSource = ExecuteSQLDataTable("SELECT ID, Ten From NHANSU")
            gdvYC.DataSource = dt.Tables(1)
        End If
    End Sub

    Private Sub gdvYCWebCT_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvYCWebCT.RowStyle
        If (e.RowHandle >= 0) Then
            If (gdvYCWebCT.GetRowCellDisplayText(e.RowHandle, "TrangThai") = "Đã XL") Then
                e.Appearance.BackColor = Color.Orange
                e.HighPriority = True
            End If
        End If
    End Sub

    Private Sub menuSua_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles menuSua.ItemClick
        btnSua.PerformClick()
    End Sub
End Class
