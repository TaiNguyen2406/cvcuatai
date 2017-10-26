Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI

Public Class frmNhapChiPhi
    Public _isUpdate As Boolean

    Private Sub frmNhapChiPhi_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _isUpdate = False
        LoadDataCb()
        tbNgayCT.EditValue = Today
        tbNgayVS.EditValue = Today
    End Sub

    Private Sub frmNhapChiPhi_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        btChonFileExcel.PerformClick()
    End Sub

    Public Sub LoadDataCb()
        Dim sql As String = ""
        sql &= " SELECT rtrim(ltrim(MaSo))MaSo,Ten FROM TAIKHOAN "
        sql &= " SELECT ID,ttcMa,Ten FROM KHACHHANG WHERE ttcKhachHang=3 ORDER BY ttcMa "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbTKDoiUng.Properties.DataSource = ds.Tables(0)
            cbDVVC.Properties.DataSource = ds.Tables(1)
            rcbDVVC.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btChonFileExcel_Click(sender As System.Object, e As System.EventArgs) Handles btChonFileExcel.Click
        Dim openfile As New OpenFileDialog
        openfile.Filter = "Excel File|*.xls;*.xlsx"
        If openfile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang tải dữ liệu...")
            Try
                Dim workbook As SpreadsheetGear.IWorkbook = SpreadsheetGear.Factory.GetWorkbook(openfile.FileName)

                Dim range As SpreadsheetGear.IRange = workbook.Worksheets(0).UsedRange

                Dim tb As DataTable = Utils.exportXLS2DataTable.getDataTableFromXLSS_SpreadsheetGear(range, SpreadsheetGear.Data.GetDataFlags.None)
                If Not tb Is Nothing Then
                    tb.Columns.Add("IDDVVC", GetType(System.Int32))
                    tb.Columns.Add("IsCGDH", GetType(System.Boolean))
                    tb.Columns.Add("Phieu_Chi", GetType(System.String))
                    tb.Columns.Add("Phieu_Chi_T", GetType(System.String))
                    tb.Columns.Add("ID", GetType(System.Object))
                    tb.Columns.Add("Loi", GetType(System.Boolean))
                    For i As Integer = 0 To tb.Rows.Count - 1
                        tb.Rows(i)("IsCGDH") = False
                        If tb.Rows(i)("Thoi_Gian").ToString.Trim = "" Then
                            tb.Rows(i)("Thoi_Gian") = Today
                        End If
                        tb.Rows(i)("Loi") = False
                    Next

                    ' Dim f As New frmNhapChiPhi
                    gdv.DataSource = tb
                    'f.chkUNC.Checked = False
                    CloseWaiting()
                    ' f.ShowDialog()
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try




        End If
    End Sub


    Private Sub cbDVVC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbDVVC.ButtonClick
        If e.Button.Index = 1 Then
            cbDVVC.EditValue = Nothing

        End If
    End Sub

    Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvData.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mDungChungDVVC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDungChungDVVC.ItemClick
        If IsDBNull(gdvData.GetRowCellValue(gdvData.GetSelectedRows(0), "IDDVVC")) Then
            ShowCanhBao("Chưa có thông tin đơn vị vận chuyển !")
            Exit Sub
        End If
        For i As Integer = 1 To gdvData.SelectedRowsCount - 1
            gdvData.SetRowCellValue(gdvData.GetSelectedRows(i), "IDDVVC", gdvData.GetRowCellValue(gdvData.GetSelectedRows(0), "IDDVVC"))
        Next
    End Sub

    Private Sub rdChiPhiNhap_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdChiPhiNhap.CheckedChanged
        colIsDHCG.Caption = "ĐH"
    End Sub

    Private Sub rdChiPhiXuat_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rdChiPhiXuat.CheckedChanged
        colIsDHCG.Caption = "CG"
    End Sub

    Private Sub cbTKDoiUng_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTKDoiUng.EditValueChanged
        On Error Resume Next
        Dim edit As LookUpEdit = CType(sender, LookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        tbNoiMoTKDU.EditValue = dr("Ten")
    End Sub

    Private Sub cbTKDoiUng_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTKDoiUng.ButtonClick
        If e.Button.Index = 1 Then
            cbTKDoiUng.EditValue = Nothing
            tbNoiMoTKDU.EditValue = Nothing
        End If
    End Sub

    Private Sub chkUNC_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkUNC.CheckedChanged
        If chkUNC.Checked Then
            cbTKDoiUng.Enabled = True
            tbNoiMoTKDU.Enabled = True
            tbTKNhan.Enabled = True
            tbNoiMoTkNhan.Enabled = True
            tbNguoiNhan.Enabled = True
            tbDienGiaiNH.Enabled = True
        Else
            cbTKDoiUng.Enabled = False
            tbNoiMoTKDU.Enabled = False
            tbTKNhan.Enabled = False
            tbNoiMoTkNhan.Enabled = False
            tbNguoiNhan.Enabled = False
            tbDienGiaiNH.Enabled = False
        End If
    End Sub


    Private Sub cbDVVC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbDVVC.EditValueChanged
        If cbDVVC.EditValue Is Nothing Then
            colDVVC.VisibleIndex = 2
            colMoTaDVVC.VisibleIndex = 3
            tbNgayCT.Enabled = False
            tbNgayVS.Enabled = False
            tbNguoiNhan.Enabled = False
        Else
            colDVVC.VisibleIndex = -1
            colMoTaDVVC.VisibleIndex = -1
            tbNgayCT.Enabled = True
            tbNgayVS.Enabled = True
            tbNguoiNhan.Enabled = True
        End If
    End Sub


    Function CheckSoPhieu() As Boolean
        gdvData.CloseEditor()
        gdvData.UpdateCurrentRow()
        Dim _loi As Integer = 0
        For i As Integer = 0 To gdvData.RowCount - 1
            If rdChiPhiNhap.Checked Then
                Dim _TenBang As String = ""
                If gdvData.GetRowCellValue(i, "IsCGDH") Then
                    _TenBang = "PHIEUDATHANG"
                Else
                    _TenBang = "PHIEUNHAPKHO"
                End If
                AddParameterWhere("@SP", gdvData.GetRowCellValue(i, "Phieu_TC"))
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu FROM " & _TenBang & " WHERE SoPhieu=@SP")
                If Not tb Is Nothing Then
                    If tb.Rows.Count = 0 Then
                        _loi += 1
                        gdvData.SetRowCellValue(i, "Loi", True)
                    End If
                Else
                    _loi += 1
                    gdvData.SetRowCellValue(i, "Loi", True)
                End If
            Else
                Dim _TenBang As String = ""
                If gdvData.GetRowCellValue(i, "IsCGDH") Then
                    _TenBang = "BANGCHAOGIA"
                Else
                    _TenBang = "PHIEUXUATKHO"
                End If
                AddParameterWhere("@SP", gdvData.GetRowCellValue(i, "Phieu_TC"))
                Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu FROM " & _TenBang & " WHERE SoPhieu=@SP")
                If Not tb Is Nothing Then
                    If tb.Rows.Count = 0 Then
                        _loi += 1
                        gdvData.SetRowCellValue(i, "Loi", True)
                    End If
                Else
                    _loi += 1
                    gdvData.SetRowCellValue(i, "Loi", True)
                End If
            End If

        Next
        If _loi > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btXacNhan_Click(sender As System.Object, e As System.EventArgs) Handles btXacNhan.Click
        gdvData.CloseEditor()
        gdvData.UpdateCurrentRow()
        If gdvData.RowCount = 0 Then
            ShowCanhBao("Chưa có thông tin chi phí !")
            Exit Sub
        End If

        If chkUNC.Checked Then
            If cbTKDoiUng.EditValue Is Nothing Then
                ShowCanhBao("Chưa có thông tin tài khoản đối ứng !")
                Exit Sub
            End If
            If tbNoiMoTkNhan.EditValue Is Nothing Then
                ShowCanhBao("Chưa có thông tin tài khoản nhận!")
                Exit Sub
            End If
        Else
            If Not cbDVVC.EditValue Is Nothing Then
                If tbNguoiNhan.EditValue.ToString.Trim = "" Then
                    ShowCanhBao("Chưa có thông tin người nhận")
                    Exit Sub
                End If
            End If
        End If

        If Not CheckSoPhieu() Then
            ShowCanhBao("Số phiếu tham chiếu không chính xác")
            Exit Sub
        End If

        If ShowCauHoi("Lập phiếu chi ?") Then
            Try
                Dim _TenBang As String = ""
                If chkUNC.Checked Then
                    _TenBang = "UNC"
                Else
                    _TenBang = "CHI"
                End If
                Dim _SoPhieuT As Object = LaySoPhieuT(_TenBang)
                Dim _SoPhieu As Object = LaySoPhieu(_TenBang)
                'tbSoPhieu.EditValue = LaySoPhieuT(_TenBang)
                Dim _SPT As Integer = Convert.ToInt32(_SoPhieuT.ToString.Replace("T", ""))
                Dim _SP As Integer = Convert.ToInt32(_SoPhieu)
                BeginTransaction()
                For i As Integer = 0 To gdvData.RowCount - 1
                    If cbDVVC.EditValue Is Nothing Then
                        AddParameter("@IDkh", gdvData.GetRowCellValue(i, "IDDVVC"))
                    Else
                        AddParameter("@IDkh", cbDVVC.EditValue)
                    End If

                    AddParameter("@TienTe", 0)
                    AddParameter("@Mucdich", 205)
                    AddParameter("@IDUser", CType(TaiKhoan, Int32))

                    If gdvData.GetRowCellValue(i, "IsCGDH") Then
                        AddParameter("@PhieuTC0", gdvData.GetRowCellValue(i, "Phieu_TC").ToString)
                        AddParameter("@PhieuTC1", "000000000")
                    Else
                        AddParameter("@PhieuTC0", "000000000")
                        AddParameter("@PhieuTC1", gdvData.GetRowCellValue(i, "Phieu_TC").ToString)
                    End If
                    AddParameter("@Sotien", gdvData.GetRowCellValue(i, "So_Tien"))
                    If IsDBNull(gdvData.GetRowCellValue(i, "Ghi_Chu")) Then
                        AddParameter("@Diengiai", " ")
                    Else
                        AddParameter("@Diengiai", gdvData.GetRowCellValue(i, "Ghi_Chu"))
                    End If
                    AddParameter("@Diengiaichung", tbDienGiai.Text)
                    AddParameter("@ChiPhiNhap", rdChiPhiNhap.Checked)
                    If chkUNC.Checked Then
                        AddParameter("@NgayThang", tbNgayCT.EditValue)
                        AddParameter("@TaiKhoanDi", cbTKDoiUng.EditValue)
                        AddParameter("@NganHangDi", tbNoiMoTKDU.EditValue)
                        AddParameter("@TaiKhoanDen", tbTKNhan.EditValue)
                        AddParameter("@NganHangDen", tbNoiMoTkNhan.EditValue)
                        AddParameter("@DienGiaiNH", tbDienGiaiNH.EditValue)
                    Else
                        If cbTKDoiUng.EditValue Is Nothing Then
                            AddParameter("@MaTK", 0)

                        Else
                            AddParameter("@MaTK", cbTKDoiUng.EditValue)

                        End If

                        AddParameter("@Chungtugoc", " ")
                        If Not cbDVVC.EditValue Is Nothing Then
                            AddParameter("@Nguoinhan", tbNguoiNhan.EditValue)
                            AddParameter("@NgaythangCT", gdvData.GetRowCellValue(i, "Thoi_Gian"))
                            AddParameter("@NgaythangVS", gdvData.GetRowCellValue(i, "Thoi_Gian"))
                        Else

                            AddParameter("@Nguoinhan", gdvData.GetRowCellValue(i, "Nguoi_Nhan"))
                            AddParameter("@NgaythangCT", tbNgayCT.EditValue)
                            AddParameter("@NgaythangVS", tbNgayVS.EditValue)
                        End If


                    End If

                    If IsDBNull(gdvData.GetRowCellValue(i, "ID")) Then
                        AddParameter("@SophieuT", "T" & _SPT.ToString)
                        AddParameter("@Sophieu", _SP.ToString)
                        Dim _ID As Object = doInsert(_TenBang)
                        If _ID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        gdvData.SetRowCellValue(i, "ID", _ID)
                        gdvData.SetRowCellValue(i, "Phieu_Chi", _SP.ToString)
                        gdvData.SetRowCellValue(i, "Phieu_Chi_T", "T" & _SPT.ToString)
                        If cbDVVC.EditValue Is Nothing Then
                            _SPT += 1
                        End If
                        _SP += 1
                    Else
                        AddParameterWhere("@IDD", gdvData.GetRowCellValue(i, "ID"))
                        If doUpdate(_TenBang, "ID=@IDD") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If



                Next
                _isUpdate = True
                ComitTransaction()
                ShowAlert("Đã lập phiếu chi !")
                If rdChiPhiNhap.Checked Then
                    For i As Integer = 0 To gdvData.RowCount - 1
                        If Not gdvData.GetRowCellValue(i, "IsCGDH") Then
                            PhanBoChiPhiNhap(gdvData.GetRowCellValue(i, "Phieu_TC").ToString)
                        End If
                    Next
                End If

            Catch ex As Exception
                RollBackTransaction()
                If _isUpdate = False Then
                    For i As Integer = 0 To gdvData.RowCount - 1
                        gdvData.SetRowCellValue(i, "Phieu_Chi", DBNull.Value)
                        gdvData.SetRowCellValue(i, "Phieu_Chi_T", DBNull.Value)
                        gdvData.SetRowCellValue(i, "ID", DBNull.Value)
                    Next
                    gdvData.CloseEditor()
                    gdvData.UpdateCurrentRow()
                End If
                ShowBaoLoi(ex.Message)
            End Try
        End If




    End Sub

    Private Sub btIn_Click(sender As System.Object, e As System.EventArgs) Handles btIn.Click
        If Not cbDVVC.EditValue Is Nothing Then
            If IsDBNull(gdvData.GetRowCellValue(0, "Phieu_Chi_T")) Then
                ShowCanhBao("Chưa tồn tại số phiếu!")
                Exit Sub
            End If
        Else
            If IsDBNull(gdvData.GetFocusedRowCellValue("Phieu_Chi_T")) Then
                ShowCanhBao("Chưa tồn tại số phiếu!")
                Exit Sub
            End If
        End If


        If chkUNC.Checked Then
            Dim sql As String = ""
            sql &= " SELECT NgayThang,SoPhieuT as SoPhieu,TaiKhoanDi,NganHangDi,(SELECT Ten FROm KHACHHANG WHERE ID=74)DonViTra, "
            sql &= "TaiKhoanDen,NganHangDen,(SELECT Ten FROm KHACHHANG WHERE ID=UNC.IDKh)DonViNhan,DienGiaiNH AS DienGiai,SUM(SoTien) as SoTien,N'' AS BangChu "
            sql &= "FROM UNC "
            sql &= "WHERE SoPhieuT=@SoPhieu "
            sql &= "GROUP BY NgayThang,SoPhieuT,TaiKhoanDi,NganHangDi,TaiKhoanDen,NganHangDen,IDKh,DienGiaiNH "
            If Not cbDVVC.EditValue Is Nothing Then
                AddParameterWhere("@SoPhieu", gdvData.GetRowCellValue(0, "Phieu_Chi_T"))
            Else
                AddParameterWhere("@SoPhieu", gdvData.GetFocusedRowCellValue("Phieu_Chi_T"))
            End If

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tb.Rows(0)("BangChu") = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
                Dim f As New frmIn("In ủy nhiệm chi")
                If chkMBBank.Checked Then
                    Dim mainRpt As New XtraReport
                    mainRpt.CreateDocument()
                    Dim rpt As New rptUNCMB
                    rpt.lbLien.Text = "Liên 1"
                    rpt.DataSource = tb
                    rpt.CreateDocument()
                    mainRpt.Pages.AddRange(rpt.Pages)

                    Dim rpt2 As New rptUNCMB
                    rpt2.lbLien.Text = "Liên 2"
                    rpt2.DataSource = tb
                    rpt2.CreateDocument()
                    mainRpt.Pages.AddRange(rpt2.Pages)
                    f.printControl.PrintingSystem = mainRpt.PrintingSystem

                    f.ShowDialog()
                Else
                    Dim rpt As New rptUNC
                    rpt.pLogo.Image = My.Resources.Logo3
                    rpt.pAnhLien2.Image = My.Resources.Logo3
                    rpt.DataSource = tb
                    rpt.CreateDocument()
                    f.printControl.PrintingSystem = rpt.PrintingSystem
                    f.ShowDialog()
                End If

            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        Else
            Dim sql As String = ""
            sql = "SELECT SoPhieuT as SoPhieu,NgayThangCT,NguoiNhan,DienGiaiChung as DienGiai,SUM(SoTien) as SoTien,ChungTuGoc, "
            sql &= "(SELECT Ten FROM KHACHHANG WHERE ID=CHI.IDKh)DiaChi, "
            sql &= "(SELECT Ten FROM NHANSU WHERE ID=CHI.IDUser)NguoiLap FROM CHI WHERE SoPhieuT=@SoPhieu "
            sql &= "GROUP BY SoPhieuT,NgayThangCT,NguoiNhan,DienGiaiChung,ChungTuGoc,IDKh,IDUser "
            If Not cbDVVC.EditValue Is Nothing Then
                AddParameterWhere("@SoPhieu", gdvData.GetRowCellValue(0, "Phieu_Chi_T"))
            Else
                AddParameterWhere("@SoPhieu", gdvData.GetFocusedRowCellValue("Phieu_Chi_T"))
            End If
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                Dim f As New frmIn("In phiếu chi")
                Dim rpt As New rptPhieuThuChi
                rpt.pLogo.Image = My.Resources.Logo3
                rpt.lbTenPhieu.Text = "PHIẾU CHI"
                rpt.lbNgay.Text = "Ngày: " & Convert.ToDateTime(tb.Rows(0)("NgayThangCT")).ToString("dd/MM/yyyy")
                rpt.lbSoPhieu.Text = "Số: " & tb.Rows(0)("SoPhieu")
                rpt.lbHoTen.Text = "Người nhận tiền: "
                rpt.lbHoTenV.Text = tb.Rows(0)("NguoiNhan")
                rpt.lbDiaChiV.Text = tb.Rows(0)("DiaChi")
                rpt.lbLyDo.Text = "Lý do chi: "
                rpt.lbLyDoV.Text = tb.Rows(0)("DienGiai")
                rpt.lbSoTienV.Text = String.Format("{0:N2}", tb.Rows(0)("SoTien"))
                rpt.lbBangChuV.Text = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
                rpt.lbKemTheoV.Text = tb.Rows(0)("ChungTuGoc")
                rpt.lbNguoiGd.Text = "Người nhận tiền"
                rpt.lbKyTenNgNhan.Text = tb.Rows(0)("NguoiNhan")
                rpt.lbKTNguoiLap.Text = tb.Rows(0)("NguoiLap")
                rpt.CreateDocument()
                f.printControl.PrintingSystem = rpt.PrintingSystem
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    Private Sub btCapNhatChiPhiTheoSoBill_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhatChiPhiTheoSoBill.Click
        If ShowCauHoi("Cập nhật lại chi phí đang có ?") Then
            ShowWaiting("Đang tính toán ...")
            Dim sql As String = ""
            If Not IO.Directory.Exists(IO.Path.Combine(Application.StartupPath, "log")) Then
                IO.Directory.CreateDirectory(IO.Path.Combine(Application.StartupPath, "log"))
            End If
            Dim flogName As String = "CN_GiaVC_" & Now.ToString("yyyyMMddHHmmss") & ".txt"
            Dim sw As New IO.StreamWriter(IO.Path.Combine(Application.StartupPath, "log\" & flogName), False, System.Text.Encoding.UTF8)

            Dim _TongTien As Double = 0
            Dim _DaChi As Boolean = False
            For i As Integer = 0 To gdvData.RowCount - 1
                sql = ""
                AddParameterWhere("@bill", gdvData.GetRowCellValue(i, "So_Bill"))
                AddParameterWhere("@IDVC", cbDVVC.EditValue)

                If rdChiPhiNhap.Checked Then
                    sql &= " SELECT (PHIEUNHAPKHO.TienTruocThue * PHIEUNHAPKHO.TyGia)TienTruocThue, CHIPHI.ID,CHIPHI.SoBill,CHIPHI.SoTien,CHIPHI.SoTienTC,CHIPHI.IDDVVC,CHIPHI.PhieuTC FROM CHIPHI"
                    sql &= " INNER JOIN PHIEUNHAPKHO ON CHIPHI.PhieuTC = PHIEUNHAPKHO.SoPhieu "
                    sql &= " WHERE IDDVVC=@IDVC AND SoBill=@bill"
                    sql &= " AND CHIPHI.Loai=0"
                Else
                    sql &= " SELECT (PHIEUXUATKHO.TienTruocThue * PHIEUXUATKHO.TyGia)TienTruocThue, CHIPHI.ID,CHIPHI.SoBill,CHIPHI.SoTien,CHIPHI.SoTienTC,CHIPHI.IDDVVC,CHIPHI.PhieuTC FROM CHIPHI"
                    sql &= " INNER JOIN PHIEUXUATKHO ON CHIPHI.PhieuTC = PHIEUXUATKHO.SoPhieu "
                    sql &= " WHERE IDDVVC=@IDVC AND SoBill=@bill"
                    sql &= " AND CHIPHI.Loai=1"
                End If

                Dim tb As DataTable = ExecuteSQLDataTable(sql)
                If tb Is Nothing Then
                    sw.WriteLine(cbDVVC.Text & "  " & gdvData.GetRowCellValue(i, "So_Bill") & " " & LoiNgoaiLe)
                Else
                    If tb.Rows.Count = 0 Then
                        sw.WriteLine(cbDVVC.Text & "  " & gdvData.GetRowCellValue(i, "So_Bill") & " không tìm thấy!")
                    ElseIf tb.Rows.Count = 1 Then
                        AddParameter("@SoTien", gdvData.GetRowCellValue(i, "So_Tien"))
                        AddParameter("@SoTienTC", gdvData.GetRowCellValue(i, "So_Tien"))
                        AddParameterWhere("@IDD", tb.Rows(0)("ID"))
                        If doUpdate("CHIPHI", "ID=@IDD") Is Nothing Then
                            sw.WriteLine(cbDVVC.Text & "  " & gdvData.GetRowCellValue(i, "So_Bill") & " " & LoiNgoaiLe)
                        Else

                            If tb.Rows(0)("SoTienTC") <> 0 Then
                                sw.WriteLine(cbDVVC.Text & "  " & gdvData.GetRowCellValue(i, "So_Bill") & " Đã cập nhật (ID=" & tb.Rows(0)("ID") & ") và ghi đè số tiền cũ là: " & tb.Rows(0)("SoTienTC").ToString)
                            Else
                                sw.WriteLine(cbDVVC.Text & "  " & gdvData.GetRowCellValue(i, "So_Bill") & " Đã cập nhật (ID=" & tb.Rows(0)("ID") & ")")
                            End If
                        End If


                    ElseIf tb.Rows.Count > 1 Then
                        _TongTien = 0
                        For j As Integer = 0 To tb.Rows.Count - 1
                            _TongTien += tb.Rows(j)("TienTruocThue")
                        Next
                        For k As Integer = 0 To tb.Rows.Count - 1
                            AddParameter("@SoTien", gdvData.GetRowCellValue(i, "So_Tien") * tb.Rows(k)("TienTruocThue") / _TongTien)
                            AddParameter("@SoTienTC", gdvData.GetRowCellValue(i, "So_Tien"))
                            AddParameterWhere("@IDD", tb.Rows(k)("ID"))
                            If doUpdate("CHIPHI", "ID=@IDD") Is Nothing Then
                                sw.WriteLine(cbDVVC.Text & "  " & gdvData.GetRowCellValue(i, "So_Bill") & " " & LoiNgoaiLe)
                            Else
                                If tb.Rows(k)("SoTienTC") <> 0 Then
                                    sw.WriteLine(cbDVVC.Text & "  " & gdvData.GetRowCellValue(i, "So_Bill") & " Đã cập nhật (ID=" & tb.Rows(k)("ID") & ") và ghi đè số tiền là: " & tb.Rows(k)("SoTienTC").ToString)

                                Else
                                    sw.WriteLine(cbDVVC.Text & "  " & gdvData.GetRowCellValue(i, "So_Bill") & " Đã cập nhật (ID=" & tb.Rows(k)("ID") & ")")
                                End If
                            End If
                        Next
                    End If
                End If
            Next
            sw.Close()
            sw.Dispose()
            If ShowCauHoi("Xem file log ?") Then
                Dim p As New Process
                p.Start(IO.Path.Combine(Application.StartupPath, "log\" & flogName))
            End If
            CloseWaiting()
        End If
    End Sub
End Class