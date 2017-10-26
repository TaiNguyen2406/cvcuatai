Imports BACSOFT.Db.SqlHelper

Public Class frmTHChamCong
    Private ChamCongThang As String = ""

    Private Sub frmTHChamCong_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbThang.EditValue = Now.ToString("MM")
        tbNam.EditValue = Today.Year
        LoadDSPhong()
        LoadDSBP()
        chkSoSanh.PerformClick()

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) And Not (KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy)) Then
            colDiem1.OptionsColumn.ReadOnly = True
            colDiem2.OptionsColumn.ReadOnly = True
            colDiemKN.OptionsColumn.ReadOnly = True
            colDiemNLDV.OptionsColumn.ReadOnly = True
            colDiemVH.OptionsColumn.ReadOnly = True
            colDoanhThu1.OptionsColumn.ReadOnly = True
            colDoanhThu2.OptionsColumn.ReadOnly = True
            colLoiNhuan1.OptionsColumn.ReadOnly = True
            colLoiNhuan2.OptionsColumn.ReadOnly = True

        ElseIf Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) And Not (KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy)) Then
            btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            gdvCT.OptionsBehavior.ReadOnly = True
            chkSoSanh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            colDoanhThu1.Visible = False
            colDoanhThu2.Visible = False
            colLoiNhuan1.Visible = False
            colLoiNhuan2.Visible = False
            colLNTamUng.Visible = False
            colPTDiemTU.Visible = False
            colHSDanhGia.Visible = False
        End If



        'LoadDS()
    End Sub

    Public Sub LoadDSPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSBP()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,Ten FROM NhanSu_BoPhan ORDER BY STT")
        If Not tb Is Nothing Then
            rcbBoPhan.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        If KiemTraDuyetLuong(cbThang.EditValue & "/" & tbNam.EditValue.ToString) Then
            lbDuyet.Caption = "Tháng lương đã được duyệt"
            lbDuyet.Glyph = My.Resources.Checked
            If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                gdvCT.OptionsBehavior.ReadOnly = False
                btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                gdvCT.OptionsBehavior.ReadOnly = True
                btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If
        Else
            lbDuyet.Caption = "Tháng lương chưa được duyệt"
            lbDuyet.Glyph = My.Resources.UnCheck
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
                btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                gdvCT.OptionsBehavior.ReadOnly = True
            Else
                btLuuLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                gdvCT.OptionsBehavior.ReadOnly = False
            End If
        End If
        Dim sql As String = ""
        sql &= " SELECT NHANSU.ID AS IDNhanVien,NHANSU.Ten AS NhanVien,LUONG.IDDepatment,tblTHChamCong.ID,tblTHChamCong.IDNhanVien,"
        sql &= " ISNULL(tblTHChamCong.CongThuong,26)CongThuong,ISNULL(tblTHChamCong.CongPhep,0)CongPhep,ISNULL(tblTHChamCong.CongNghiLe,0)CongNghiLe,"
        sql &= " ISNULL(tblTHChamCong.CNLe1,0)CNLe1,ISNULL(tblTHChamCong.CNLe2,0)CNLe2,ISNULL(tblTHChamCong.ThemGio1,0)ThemGio1,"
        sql &= " ISNULL(tblTHChamCong.ThemGio2,0)ThemGio2,ISNULL(tblTHChamCong.ThemGio3,0)ThemGio3,ISNULL(tblTHChamCong.PC_CT_Xang,0)PC_CT_Xang,ISNULL(tblTHChamCong.KT_DVKT_An,0)KT_DVKT_An,"
        sql &= " ISNULL(tblTHChamCong.PCAnCa,0)PCAnCa,ISNULL(tblTHChamCong.DiemVH,0)DiemVH,ISNULL(tblTHChamCong.DiemKN,0)DiemKN,ISNULL(tblTHChamCong.DiemNLDV,0)DiemNLDV,ISNULL(tblTHChamCong.Diem,0)Diem,ISNULL(tblTHChamCong.Diem1,0)Diem1,ISNULL(tblTHChamCong.DiemBC,0)DiemBC,"
        sql &= " ISNULL(tblTHChamCong.DoanhThu,0)DoanhThu,ISNULL(tblTHChamCong.LoiNhuan,0)LoiNhuan,ISNULL(tblTHChamCong.DoanhThu1,0)DoanhThu1,ISNULL(tblTHChamCong.LoiNhuan1,0)LoiNhuan1,ISNULL(tblTHChamCong.LoiNhuanTU,0)LoiNhuanTU,ISNULL(tblTHChamCong.PTDiemTamUng,0)PTDiemTamUng,tblTHChamCong.HSDanhGia,convert(bit,0)Modify"
        sql &= " FROM LUONG INNER JOIN NHANSU ON NHANSU.ID=LUONG.IDNhanVien AND NHANSU.NoiCtac=74 "
        sql &= " LEFT JOIN NhanSu_BoPhan ON LUONG.IDBoPhan=NhanSu_BoPhan.Ma"
        sql &= " LEFT JOIN tblTHChamCong ON LUONG.IDNhanVien = tblTHChamCong.IDNhanVien AND Convert(int,LEFT(tblTHChamCong.[Month],2))=" & cbThang.EditValue & " AND Convert(int,RIGHT(tblTHChamCong.[Month],4))=" & tbNam.EditValue.ToString
        sql &= " WHERE Convert(int,LEFT(LUONG.[Month],2))=" & cbThang.EditValue & " AND Convert(int,RIGHT(LUONG.[Month],4))=" & tbNam.EditValue.ToString
        sql &= " ORDER BY LUONG.IDDepatment,NhanSu_BoPhan.MaBP,NHANSU.ChucVu,LUONG.ID"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
            ChamCongThang = cbThang.EditValue & "/" & tbNam.EditValue
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If Not e.Column.FieldName = "Modify" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If
    End Sub

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick
        If ChamCongThang <> cbThang.EditValue & "/" & tbNam.EditValue Then
            ShowCanhBao("Dữ liệu chấm công không khớp, cần tải lại dữ liệu trước khi sửa đổi !")
            Exit Sub
        End If
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        Try
            For i As Integer = 0 To gdvCT.RowCount - 1

                If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Then
                    AddParameter("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                    AddParameter("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
                    AddParameter("@CongThuong", gdvCT.GetRowCellValue(i, "CongThuong"))
                    AddParameter("@CongPhep", gdvCT.GetRowCellValue(i, "CongPhep"))
                    AddParameter("@CongNghiLe", gdvCT.GetRowCellValue(i, "CongNghiLe"))
                    AddParameter("@CNLe1", gdvCT.GetRowCellValue(i, "CNLe1"))
                    AddParameter("@CNLe2", gdvCT.GetRowCellValue(i, "CNLe2"))
                    AddParameter("@ThemGio1", gdvCT.GetRowCellValue(i, "ThemGio1"))
                    AddParameter("@ThemGio2", gdvCT.GetRowCellValue(i, "ThemGio2"))
                    AddParameter("@ThemGio3", gdvCT.GetRowCellValue(i, "ThemGio3"))
                    AddParameter("@PC_CT_Xang", gdvCT.GetRowCellValue(i, "PC_CT_Xang"))
                    AddParameter("@KT_DVKT_An", gdvCT.GetRowCellValue(i, "KT_DVKT_An"))
                    AddParameter("@PCAnCa", gdvCT.GetRowCellValue(i, "PCAnCa"))
                    If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then
                        AddParameter("@DiemVH", gdvCT.GetRowCellValue(i, "DiemVH"))
                        AddParameter("@DiemKN", gdvCT.GetRowCellValue(i, "DiemKN"))
                        AddParameter("@DiemNLDV", gdvCT.GetRowCellValue(i, "DiemNLDV"))
                        AddParameter("@Diem", gdvCT.GetRowCellValue(i, "Diem"))
                        AddParameter("@Diem1", gdvCT.GetRowCellValue(i, "Diem1"))
                        AddParameter("@DiemBC", gdvCT.GetRowCellValue(i, "DiemBC"))
                        AddParameter("@DoanhThu", gdvCT.GetRowCellValue(i, "DoanhThu"))
                        AddParameter("@LoiNhuan", gdvCT.GetRowCellValue(i, "LoiNhuan"))
                        AddParameter("@DoanhThu1", gdvCT.GetRowCellValue(i, "DoanhThu1"))
                        AddParameter("@LoiNhuan1", gdvCT.GetRowCellValue(i, "LoiNhuan1"))
                        AddParameter("@HSDanhGia", gdvCT.GetRowCellValue(i, "HSDanhGia"))
                        AddParameter("@IDBoPhan", gdvCT.GetRowCellValue(i, "IDBoPhan"))
                    End If

                    Dim oID As Integer = doInsert("tblTHChamCong")
                    If oID = Nothing Then Throw New Exception(LoiNgoaiLe)
                    gdvCT.SetRowCellValue(i, "ID", oID)
                Else
                    If gdvCT.GetRowCellValue(i, "Modify") Then
                        AddParameter("@IDNhanVien", gdvCT.GetRowCellValue(i, "IDNhanVien"))
                        AddParameter("@Month", cbThang.EditValue.ToString & "/" & tbNam.EditValue.ToString)
                        AddParameter("@CongThuong", gdvCT.GetRowCellValue(i, "CongThuong"))
                        AddParameter("@CongPhep", gdvCT.GetRowCellValue(i, "CongPhep"))
                        AddParameter("@CongNghiLe", gdvCT.GetRowCellValue(i, "CongNghiLe"))
                        AddParameter("@CNLe1", gdvCT.GetRowCellValue(i, "CNLe1"))
                        AddParameter("@CNLe2", gdvCT.GetRowCellValue(i, "CNLe2"))
                        AddParameter("@ThemGio1", gdvCT.GetRowCellValue(i, "ThemGio1"))
                        AddParameter("@ThemGio2", gdvCT.GetRowCellValue(i, "ThemGio2"))
                        AddParameter("@ThemGio3", gdvCT.GetRowCellValue(i, "ThemGio3"))
                        AddParameter("@PC_CT_Xang", gdvCT.GetRowCellValue(i, "PC_CT_Xang"))
                        AddParameter("@KT_DVKT_An", gdvCT.GetRowCellValue(i, "KT_DVKT_An"))
                        AddParameter("@PCAnCa", gdvCT.GetRowCellValue(i, "PCAnCa"))
                        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Or KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPQuanLy) Then
                            AddParameter("@DiemVH", gdvCT.GetRowCellValue(i, "DiemVH"))
                            AddParameter("@DiemKN", gdvCT.GetRowCellValue(i, "DiemKN"))
                            AddParameter("@DiemNLDV", gdvCT.GetRowCellValue(i, "DiemNLDV"))
                            AddParameter("@Diem", gdvCT.GetRowCellValue(i, "Diem"))
                            AddParameter("@Diem1", gdvCT.GetRowCellValue(i, "Diem1"))
                            AddParameter("@DiemBC", gdvCT.GetRowCellValue(i, "DiemBC"))
                            AddParameter("@DoanhThu", gdvCT.GetRowCellValue(i, "DoanhThu"))
                            AddParameter("@LoiNhuan", gdvCT.GetRowCellValue(i, "LoiNhuan"))
                            AddParameter("@DoanhThu1", gdvCT.GetRowCellValue(i, "DoanhThu1"))
                            AddParameter("@LoiNhuan1", gdvCT.GetRowCellValue(i, "LoiNhuan1"))
                            AddParameter("@HSDanhGia", gdvCT.GetRowCellValue(i, "HSDanhGia"))
                            '  AddParameter("@IDBoPhan", gdvCT.GetRowCellValue(i, "IDBoPhan"))
                        End If
                        AddParameterWhere("@ID", gdvCT.GetRowCellValue(i, "ID"))
                        If doUpdate("tblTHChamCong", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        gdvCT.SetRowCellValue(i, "Modify", False)
                    End If
                End If

            Next
            ShowAlert("Đã lưu !")
        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
        End Try

    End Sub

    Private Sub chkSoSanh_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkSoSanh.CheckedChanged
        If chkSoSanh.Checked Then
            chkSoSanh.Glyph = My.Resources.Checked
            colDiem1.VisibleIndex = 15
            colDiem2.VisibleIndex = 16
            colDiem1.Caption = "Điểm 1"
            colDiem2.Caption = "Điểm 2"
            colDoanhThu1.VisibleIndex = 17
            colLoiNhuan1.VisibleIndex = 18
            colDoanhThu1.Caption = "Doanh thu 1"
            colLoiNhuan1.Caption = "Lợi nhuận 1"
            colDoanhThu2.VisibleIndex = 19
            colLoiNhuan2.VisibleIndex = 20
            colDoanhThu2.Caption = "Doanh thu 2"
            colLoiNhuan2.Caption = "Lợi nhuận 2"
        Else
            chkSoSanh.Glyph = My.Resources.UnCheck
            colDiem1.Visible = False
            colDiem2.Caption = "Điểm"
            colDoanhThu1.Visible = False
            colLoiNhuan1.Visible = False
            colDoanhThu2.Caption = "Doanh thu"
            colLoiNhuan2.Caption = "Lợi nhuận"
        End If
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub

        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "TH_CHAMCONG_" & cbThang.EditValue & "_" & tbNam.EditValue.ToString & ".xls"
        saveFile.OverwritePrompt = False
        If saveFile.ShowDialog = DialogResult.OK Then

            Try
                ShowWaiting("Đang kết xuất ...")
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            Finally
                CloseWaiting()
            End Try

        End If
    End Sub
End Class