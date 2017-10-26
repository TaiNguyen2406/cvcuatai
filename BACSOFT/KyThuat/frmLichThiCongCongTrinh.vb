Imports BACSOFT.Db.SqlHelper

Public Class frmLichThiCongCongTrinh

    Private Sub frmLichThiCongCongTrinh_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loadDSPhongBan()
        LoadDSNhanVien()
        SchLichThiCong.Start = DateAdd(DateInterval.Hour, 6, GetServerTime().Date)
        TaiLich()
        Dim index As Integer = 0
        For i As Integer = 0 To TreeNV.ItemCount - 1
            If CType(TreeNV.Items(i).Value, DevExpress.XtraScheduler.UI.ResourceCheckedListBoxItem).Resource.Id = TaiKhoan Then
                index = i
                Exit For
            End If
        Next
        SchLichThiCong.Views.TimelineView.FirstVisibleResourceIndex = index
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
    End Sub

    Private Sub loadDSPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
            If tb.Rows.Count > 0 Then
                cbPhongBan.EditValue = 2
            End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNhanVien()
        Dim sql As String = "SELECT ID,Ten FROM NHANSU WHERE Noictac=74 AND Trangthai=1 "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbNhanVien.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub TaiLich()
        Dim sql As String = ""
        sql &= " SELECT tblBaoCaoLichThiCong.ID,BANGYEUCAU.NoiDung AS TieuDe,KHACHHANG.ttcMa AS DiaDiem,(0)DoQuanTrong,(0)DoKhanCap,"
        sql &= "   (tblBaoCaoLichThiCong.Ngay + tblBaoCaoLichThiCong.BatDau) BatDau,(tblBaoCaoLichThiCong.Ngay + tblBaoCaoLichThiCong.KetThuc) KetThuc, (Case WHEN tblBaoCaoLichThiCong.SoCG is null THEN tblTuDien.NoiDung ELSE (tblTuDien.NoiDung + ' CG:' + ISNULL(tblBaoCaoLichThiCong.SoCG,'')) END )NoiDung, "
        sql &= " convert(int, tblBaoCaoLichThiCong.IDNgThucHien) AS UserID"
        sql &= " FROM tblBaoCaoLichThiCong "
        sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=tblBaoCaoLichThiCong.SoYC "
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=BANGYEUCAU.IDKhachhang "
        sql &= " LEFT JOIN tblTuDien ON tblBaoCaoLichThiCong.IDNoiDung=tblTuDien.ID AND tblTuDien.Loai=6 "
        sql &= " WHERE GiaoViec=0"

        If cbThoiGian.EditValue = "Từ đầu tháng" Then
            sql &= " AND (Convert(datetime,Convert(nvarchar,(tblBaoCaoLichThiCong.Ngay + tblBaoCaoLichThiCong.BatDau),103),103)>=@NgayDauThang OR Convert(datetime,Convert(nvarchar,(tblBaoCaoLichThiCong.Ngay + tblBaoCaoLichThiCong.KetThuc),103),103)>=@NgayDauThang) "
            AddParameterWhere("@NgayDauThang", New DateTime(Today.Year, Today.Month, 1))
        ElseIf cbThoiGian.EditValue = "Tùy chỉnh" Then
            sql &= " AND( (Convert(datetime,Convert(nvarchar,(tblBaoCaoLichThiCong.Ngay + tblBaoCaoLichThiCong.BatDau),103),103) BETWEEN @TuNgay AND @DenNgay) OR (Convert(datetime,Convert(nvarchar,(tblBaoCaoLichThiCong.Ngay + tblBaoCaoLichThiCong.KetThuc),103),103) BETWEEN @TuNgay AND @DenNgay)) "
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If

        If Not cbPhongBan.EditValue Is Nothing Then
            sql &= " SELECT NHANSU.ID,NHANSU.Ten FROM NHANSU LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=NhanSu.IDBoPhan WHERE NHANSU.Noictac=74 AND NHANSU.TrangThai=1 AND NHANSU.IDDepatment = " & cbPhongBan.EditValue & " ORDER BY NHANSU.IDDepatment,NhanSu_BoPhan.MaBP,NhanSu.ChucVu,NHANSU.ID "
        Else
            sql &= " SELECT NHANSU.ID,NHANSU.Ten FROM NHANSU LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=NhanSu.IDBoPhan WHERE NHANSU.Noictac=74 AND NHANSU.TrangThai=1 ORDER BY NHANSU.IDDepatment,NhanSu_BoPhan.MaBP,NhanSu.ChucVu,NHANSU.ID "
        End If

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            SchLichThiCong.Storage.Resources.DataSource = ds.Tables(1)
            SchLichThiCong.Storage.Appointments.DataSource = ds.Tables(0)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        'If SchLichThiCong.SelectedAppointments(0).ResourceId <> TaiKhoan Then
        '    Exit Sub
        'End If
    End Sub


    Private Sub SchLichThiCong_PopupMenuShowing(sender As System.Object, e As DevExpress.XtraScheduler.PopupMenuShowingEventArgs) Handles SchLichThiCong.PopupMenuShowing
        If e.Menu.Id = DevExpress.XtraScheduler.SchedulerMenuItemId.AppointmentMenu Then
            e.Menu.Items(0).Caption = "Xem"
            e.Menu.Items(1).Visible = False
            e.Menu.Items(2).Visible = False
            e.Menu.Items(3).Visible = False
            e.Menu.Items(4).Visible = False
            e.Menu.Items(5).Caption = "Xóa"
        Else
            Select Case SchLichThiCong.ActiveViewType
                Case DevExpress.XtraScheduler.SchedulerViewType.Day
                    e.Menu.Items(0).Caption = "Thêm báo cáo công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Ngày hôm nay"
                    e.Menu.Items(3).Caption = "Chuyển đến ngày"
                    e.Menu.Items(4).Caption = "Chế độ xem"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
                Case DevExpress.XtraScheduler.SchedulerViewType.Month
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Chuyển tới ngày này"
                    e.Menu.Items(3).Caption = "Ngày hôm nay"
                    e.Menu.Items(4).Caption = "Chuyển đến ngày"
                    e.Menu.Items(5).Caption = "Chế độ xem"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
                Case DevExpress.XtraScheduler.SchedulerViewType.Timeline
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Ngày hôm nay"
                    e.Menu.Items(3).Caption = "Chuyển đến ngày"
                    e.Menu.Items(4).Caption = "Tuỳ chọn hiển thị"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Năm"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Quý"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Tháng"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Ngày"
                    ' CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(5).Caption = "Giờ"
                    e.Menu.Items(5).Visible = False
                    e.Menu.Items(6).Caption = "Chế độ xem"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(6), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
                Case DevExpress.XtraScheduler.SchedulerViewType.Week
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Chuyển tới ngày này"
                    e.Menu.Items(3).Caption = "Ngày hôm nay"
                    e.Menu.Items(4).Caption = "Chuyển đến ngày"
                    e.Menu.Items(5).Caption = "Chế độ xem"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(5), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
                Case DevExpress.XtraScheduler.SchedulerViewType.WorkWeek
                    e.Menu.Items(0).Caption = "Thêm công việc mới"
                    e.Menu.Items(1).Visible = False
                    e.Menu.Items(2).Caption = "Ngày hôm nay"
                    e.Menu.Items(3).Caption = "Chuyển đến ngày"
                    e.Menu.Items(4).Caption = "Chế độ xem"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(0).Caption = "Theo ngày"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(1).Caption = "Theo ngày làm việc trong tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(2).Caption = "Theo tuần"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(3).Caption = "Theo tháng"
                    CType(e.Menu.Items(4), DevExpress.Utils.Menu.DXPopupMenu).Items(4).Caption = "Theo dòng thời gian"
            End Select
        End If
    End Sub

    Private Sub SchLichThiCong_GotoDateFormShowing(sender As System.Object, e As DevExpress.XtraScheduler.GotoDateFormEventArgs) Handles SchLichThiCong.GotoDateFormShowing
        e.Handled = True
        Dim f As New frmDenNgay
        f.Tag = "BC"
        f.ShowDialog()
    End Sub

    Private Sub SchLichLamViec_EditAppointmentFormShowing(sender As System.Object, e As DevExpress.XtraScheduler.AppointmentFormEventArgs) Handles SchLichThiCong.EditAppointmentFormShowing
        e.Handled = True
        If SchLichThiCong.SelectedAppointments.Count > 0 Then
            TrangThai.isUpdate = True
            objID = SchLichThiCong.SelectedAppointments(0).CustomFields("ID")
            If SchLichThiCong.SelectedAppointments(0).ResourceId <> TaiKhoan Then
                Dim str As String = ""
                str &= "- Bắt đầu:  " & SchLichThiCong.SelectedAppointments(0).Start.ToString("HH:mm dd/MM/yyyy")
                str &= vbCrLf & "- Kết thúc: " & SchLichThiCong.SelectedAppointments(0).End.ToString("HH:mm dd/MM/yyyy")
                str &= vbCrLf & "- Công trình,Khách hàng: " & SchLichThiCong.SelectedAppointments(0).Description & " - " & SchLichThiCong.SelectedAppointments(0).Location
                str &= vbCrLf & "- Nội dung công việc: " & SchLichThiCong.SelectedAppointments(0).Subject
                str &= vbCrLf & "- Thực hiện: "
                Dim tb2 As DataTable = DataSourceDSFile(SchLichThiCong.SelectedAppointments(0).ResourceId, , ",")
                'tb.Rows(i)("IDNgThucHien") = ""

                    AddParameterWhere("@ID", SchLichThiCong.SelectedAppointments(0).ResourceId)
                    Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                    If Not tb3 Is Nothing Then
                    str &= tb3.Rows(0)(0).ToString
                    End If


                tbChiTiet.EditValue = str
                pChiTiet.Visible = True
                Exit Sub
            End If
        Else
            TrangThai.isAddNew = True
        End If

        Dim f As New frmCNLichThiCong
        f.Tag = "Lich"
        If TrangThai.isUpdate Then
            'f.CongTrinh = SchLichThiCong.SelectedAppointments(0).CustomFields("CongTrinh")
            ' f.IDUser = Convert.ToInt32(SchLichThiCong.SelectedAppointments(0).CustomFields("IDUser"))
        Else
            f.tbTuNgay.EditValue = SchLichThiCong.SelectedInterval.Start.Date
            f.tbBatDauTu.EditValue = SchLichThiCong.SelectedInterval.Start.TimeOfDay
            f.tbKetThucTu.EditValue = SchLichThiCong.SelectedInterval.End.TimeOfDay
        End If

        f.ShowDialog()
    End Sub


    Private Sub mChonHet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChonHet.ItemClick
        Dim check As CheckState
        If TreeNV.Items(0).CheckState = CheckState.Checked Then
            check = CheckState.Unchecked
        Else
            check = CheckState.Checked
        End If

        For i As Integer = 0 To TreeNV.ItemCount - 1
            TreeNV.Items(i).CheckState = check
        Next
    End Sub

    Private Sub btTaiLichLamViec_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLichLamViec.ItemClick
        TaiLich()
    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhongBan.EditValue = Nothing
        End If
    End Sub

    Private Sub btAn_Click(sender As System.Object, e As System.EventArgs) Handles btAn.Click
        pChiTiet.Visible = False
    End Sub

    Private Sub SchedulerStorage1_AppointmentDeleting(sender As System.Object, e As DevExpress.XtraScheduler.PersistentObjectCancelEventArgs) Handles SchedulerStorage1.AppointmentDeleting

        If SchLichThiCong.SelectedAppointments(0).ResourceId <> TaiKhoan Then
            ShowBaoLoi("Bạn không có quyền xóa báo cáo của người khác !")
            e.Cancel = True
        Else
            If ShowCauHoi("Xóa nội dung này ?") Then
                AddParameterWhere("@ID", SchLichThiCong.SelectedAppointments(0).CustomFields("ID"))
                If doDelete("tblBaoCaoLichThiCong", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    e.Cancel = True
                Else
                    ShowAlert("Đã xóa !")
                End If
            Else
                e.Cancel = True
            End If
        End If

    End Sub

    Private Sub cbThoiGian_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbThoiGian.EditValueChanged
        If cbThoiGian.EditValue <> "Tùy chỉnh" Then
            tbTuNgay.Enabled = False
            tbDenNgay.Enabled = False
        Else
            tbTuNgay.Enabled = True
            tbDenNgay.Enabled = True
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = tbDenNgay.EditValue
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub
End Class
