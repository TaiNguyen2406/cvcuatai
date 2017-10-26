Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmCNThu2

    Public PhieuThu As String
    Public PhieuThuCT As String
    Public ThuNH As Boolean
    Public _Exit As Boolean = False
    Public arrPhieu As List(Of DanhSachPhieu) = Nothing
    Public _TrangThai As New Utils.TrangThai
    Public idChungTuThue As Object = Nothing

    Private Sub frmCNThu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("ID", Type.GetType("System.Int64")))
        dt.Columns.Add(New DataColumn("SoPhieu", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("PhieuCG", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("PhieuXK", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("Tamung", Type.GetType("System.Boolean")))
        dt.Columns.Add(New DataColumn("SoTien", Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("NoiDung", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("IdCT", Type.GetType("System.Int64")))
        dt.Columns.Add(New DataColumn("SoCT", Type.GetType("System.String")))
        gdv.DataSource = dt


        btIn.Enabled = False

        If ThuNH Then
            tbChungTuGoc.Enabled = False
        End If

        LoadDataCb()

        If ThuNH Then
            tbTKGui.Enabled = True
            tbNoiMoTkGui.Enabled = True
            btIn.Visible = False
        Else
            tbTKGui.Enabled = False
            tbNoiMoTkGui.Enabled = False
        End If

        If _TrangThai.isAddNew Then
            Dim _ToDay As DateTime = GetServerTime.Date
            tbNgayCT.EditValue = _ToDay.Date
            tbNgayVS.EditValue = _ToDay.Date
            If CType(cbMucDich.Properties.DataSource, DataTable).Rows.Count > 0 Then
                cbMucDich.EditValue = 100
            End If
            tbChungTuGoc.EditValue = "0 CT"
            tbNguoiLap.EditValue = NguoiDung

            If Not arrPhieu Is Nothing Then
                gdvMaKH.EditValue = arrPhieu(0).IDkhachHang
                tbDienGiai.Text = "Bán hàng XK" & arrPhieu(0).SoPhieu
                For i As Integer = 0 To arrPhieu.Count - 1
                    gdvData.AddNewRow()
                    gdvData.SetFocusedRowCellValue("PhieuCG", "000000000")
                    gdvData.SetFocusedRowCellValue("PhieuXK", arrPhieu(i).SoPhieu)
                    gdvData.SetFocusedRowCellValue("SoTien", arrPhieu(i).SoTien)
                    gdvData.SetFocusedRowCellValue("Tamung", False)
                    gdvData.SetFocusedRowCellValue("NoiDung", "Bán hàng XK" & arrPhieu(i).SoPhieu)
                    gdvData.CloseEditor()
                    gdvData.UpdateCurrentRow()
                Next
                TinhTongTien()
            End If

        Else

            btIn.Enabled = True

            Me.Text = "Thông tin phiếu thu " & PhieuThu
            Dim sql As String = ""
            If ThuNH Then
                sql = "SELECT MucDich FROM THUNH WHERE SoPhieuT=@SoPhieu"
            Else
                sql = "SELECT MucDich FROM THU WHERE SoPhieuT=@SoPhieu"
            End If
            AddParameterWhere("@SoPhieu", PhieuThu)

            Dim tb1 As DataTable = ExecuteSQLDataTable(sql)
            If Not tb1 Is Nothing Then
                If tb1.Rows.Count > 0 Then
                    cbMucDich.EditValue = Convert.ToInt32(tb1.Rows(0)("MucDich"))
                End If
            End If

            sql = ""
            If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                If ThuNH Then
                    sql = "SELECT ID,SoPhieuT,Diengiaichung,SoPhieu,NgayThangCT,NgayThangVS,TaiKhoanDi,NganHangDi,TaiKhoanDen,NganHangDen,IDKH,DienGiai,SoTien,TienTe,MucDich,IDUser,PhieuTC0 ,PhieuTC1,TamUng,(SELECT Ten FROM NHANSU WHERE ID=THUNH.IDUser)NguoiLap,"
                    sql &= "IdChungTu,(select soct from chungtu where id=THUNH.IdChungTu)SoCT,(select diengiai from chungtu where id=THUNH.IdChungTu)DienGiaiThue FROM THUNH WHERE SoPhieuT=@SoPhieu"
                Else
                    sql = "SELECT ID,SoPhieuT,Diengiaichung,SoPhieu,NgayThangCT,NgayThangVS,NguoiNop,IDKH,DienGiai,SoTien,TienTe,ChungTuGoc,MucDich,IDUser,PhieuTC0,PhieuTC1,MaTK,TamUng,(SELECT Ten FROM NHANSU WHERE ID=THU.IDUser)NguoiLap,"
                    sql &= "IdChungTu,(select soct from chungtu where id=THU.IdChungTu)SoCT,(select diengiai from chungtu where id=THU.IdChungTu)DienGiaiThue FROM THU WHERE SoPhieuT=@SoPhieu"
                End If
                AddParameterWhere("@SoPhieu", PhieuThu)
            Else
                If ThuNH Then
                    sql = "SELECT ID,SoPhieuT,Diengiaichung,SoPhieu,NgayThangCT,NgayThangVS,TaiKhoanDi,NganHangDi,TaiKhoanDen,NganHangDen,IDKH,DienGiai,SoTien,TienTe,MucDich,IDUser,PhieuTC0 ,PhieuTC1,TamUng,(SELECT Ten FROM NHANSU WHERE ID=THUNH.IDUser)NguoiLap,"
                    sql &= "IdChungTu,(select soct from chungtu where id=THUNH.IdChungTu)SoCT,(select diengiai from chungtu where id=THUNH.IdChungTu)DienGiaiThue FROm THUNH WHERE SoPhieu=@SoPhieu"
                Else
                    sql = "SELECT ID,SoPhieuT,Diengiaichung,SoPhieu,NgayThangCT,NgayThangVS,NguoiNop,IDKH,DienGiai,SoTien,TienTe,ChungTuGoc,MucDich,IDUser,PhieuTC0,PhieuTC1,MaTK,TamUng,(SELECT Ten FROM NHANSU WHERE ID=THU.IDUser)NguoiLap,"
                    sql &= "IdChungTu,(select soct from chungtu where id=THU.IdChungTu)SoCT,(select diengiai from chungtu where id=THU.IdChungTu)DienGiaiThue FROM THU WHERE SoPhieu=@SoPhieu"
                End If
                AddParameterWhere("@SoPhieu", PhieuThuCT.Trim.Substring(3, 9))
            End If


            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tbSoPhieu.EditValue = tb.Rows(0)("SoPhieuT")
                tbNgayCT.EditValue = tb.Rows(0)("NgayThangCT")
                tbNgayVS.EditValue = tb.Rows(0)("NgayThangVS")
                chkTamUng.EditValue = tb.Rows(0)("TamUng")
                gdvMaKH.EditValue = tb.Rows(0)("IDKH")
                'tbSoTien.EditValue = tb.Rows(0)("SoTien")
                cbTienTe.EditValue = tb.Rows(0)("TienTe")
                cbMucDich.EditValue = Convert.ToInt32(tb.Rows(0)("MucDich"))
                'cbMucDich.EditValue = tb.Rows(0)("MucDich")

                If ThuNH Then
                    cbTKDoiUng.EditValue = tb.Rows(0)("TaiKhoanDen")
                    tbNoiMoTKDU.EditValue = tb.Rows(0)("NganHangDen")
                    tbTKGui.EditValue = tb.Rows(0)("TaiKhoanDi")
                    tbNoiMoTkGui.EditValue = tb.Rows(0)("NganHangDi")
                Else
                    cbTKDoiUng.EditValue = tb.Rows(0)("MaTK")
                    cbNguoiNop.EditValue = tb.Rows(0)("NguoiNop")
                    tbChungTuGoc.EditValue = tb.Rows(0)("ChungTuGoc")
                End If

                tbNguoiLap.EditValue = tb.Rows(0)("NguoiLap")

                If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        gdvData.AddNewRow()
                        gdvData.SetFocusedRowCellValue("ID", tb.Rows(i)("ID"))
                        gdvData.SetFocusedRowCellValue("SoPhieu", tb.Rows(i)("SoPhieu"))
                        gdvData.SetFocusedRowCellValue("PhieuCG", tb.Rows(i)("PhieuTC0"))
                        gdvData.SetFocusedRowCellValue("PhieuXK", tb.Rows(i)("PhieuTC1"))
                        gdvData.SetFocusedRowCellValue("SoTien", tb.Rows(i)("SoTien"))
                        gdvData.SetFocusedRowCellValue("NoiDung", tb.Rows(i)("DienGiai"))
                        gdvData.SetFocusedRowCellValue("Tamung", tb.Rows(i)("Tamung"))
                        gdvData.SetFocusedRowCellValue("IdChungTu", tb.Rows(i)("IdChungTu"))
                        gdvData.SetFocusedRowCellValue("SoCT", tb.Rows(i)("SoCT"))
                        gdvData.CloseEditor()
                        gdvData.UpdateCurrentRow()
                    Next
                    TinhTongTien()
                Else
                    tbSoTien.Value = tb.Rows(0)("SoTien")
                    idPhieuThu = tb.Rows(0)("ID")
                    chkTamUng.Checked = tb.Rows(0)("Tamung")
                End If
                tbDienGiai.EditValue = tb.Rows(0)("Diengiaichung")

                If Not tb.Rows(0)("IdChungTu") Is DBNull.Value Then
                    chkLapPhieuThu.Enabled = False
                    txtSoPhieuCT.Text = tb.Rows(0)("SoCT").ToString
                    txtSoPhieuCT.Enabled = True
                    txtNoiDungPhieuThu.EditValue = tb.Rows(0)("DienGiaiThue")
                    chkLapPhieuThu.Properties.Appearance.ForeColor = Color.Blue

                Else
                    chkLapPhieuThu.Properties.Appearance.ForeColor = Color.Red
                    txtSoPhieuCT.Enabled = False
                End If

            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            '  gdvData.Columns(colPhieuCG.FieldName).re = True
            colPhieuCG.OptionsColumn.ReadOnly = False
            colPhieuXK.OptionsColumn.ReadOnly = False

        End If
        'gdvMaKH.Focus()
        'SendKeys.Send("{Enter}")
    End Sub

    Public Sub LoadDataCb()
        Dim sql As String = ""
        sql &= " SELECT convert(bit,0) as Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang FROM BANGCHAOGIA ORDER BY SoPhieu DESC "
        sql &= " SELECT convert(bit,0) as Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang FROM PHIEUXUATKHO ORDER BY SoPhieu DESC "
        sql &= " SELECT ltrim(rtrim(MaSo))MaSo,Ten FROM TAIKHOAN "
        sql &= " SELECT ID,Ten FROm MUCDICHTHUCHI WHERE left(ID,1)=1 ORDER BY Ten"
        sql &= " SELECT ID,Ten,TyGia FROM tblTienTe"
        sql &= " SELECT ID,ttcMa,Ten,IDTakecare,rtrim(ltrim(ttcTaiKhoan))AS ttcTaiKhoan,ttcNoiMo FROM KHACHHANG ORDER BY ttcMa "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvPhieuTC0.Properties.DataSource = ds.Tables(0)
            gdvPhieuTC1.Properties.DataSource = ds.Tables(1)
            cbTKDoiUng.Properties.DataSource = ds.Tables(2)
            cbMucDich.Properties.DataSource = ds.Tables(3)
            cbTienTe.Properties.DataSource = ds.Tables(4)
            If ds.Tables(4).Rows.Count > 0 Then
                cbTienTe.EditValue = ds.Tables(4).Rows(0)(0)
            End If
            gdvMaKH.Properties.DataSource = ds.Tables(5)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadNguoiNop()
        cbNguoiNop.Properties.Items.Clear()
        If Not gdvMaKH.EditValue Is Nothing Then
            Dim sql As String = ""
            sql &= " SELECT Ten FROM NHANSU WHERE NoiCtac =" & gdvMaKH.EditValue
            sql &= " UNION ALL"
            sql &= " SELECT Ten FROM NHANSU WHERE ID=(SELECT IDTakeCare FROM KHACHHANG WHERE ID=" & gdvMaKH.EditValue & ")"
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    cbNguoiNop.Properties.Items.Add(dt.Rows(i)(0))
                Next
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If

    End Sub


    Public Sub LoadCGXK()
        Dim sql As String = ""
        sql &= " SELECT top 1000 convert(bit,0) As Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang,ISNULL(tb.DaThu,0)DaThu FROM BANGCHAOGIA "
        If Not gdvMaKH.EditValue Is Nothing Then

            sql &= " LEFT JOIN ( SELECT SUM (SoTien2) AS DaThu,PhieuTC0 FROM (SELECT SoTien,SoTien AS SoTien2,(N'TT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THU "
            sql &= " WHERE THU.IDKH = " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,SoTien as SoTien2,(N'CK ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THUNH"
            sql &= " WHERE THUNH.IDKH = " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI WHERE MucDich=230"
            sql &= " AND CHI.IDKH = " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC WHERE MucDich=230"
            sql &= " AND UNC.IDKH = " & gdvMaKH.EditValue
            sql &= " )tbThu GROUP BY PhieuTC0"
            sql &= " )tb ON BANGCHAOGIA.SoPhieu=tb.PhieuTC0"
            sql &= " WHERE IDKhachhang = " & gdvMaKH.EditValue
        End If
        sql &= " ORDER BY SoPhieu DESC "
        If Not gdvMaKH.EditValue Is Nothing Then
            sql &= " SELECT Chon,SoPhieu,TienTruocThue,TienThue,TongTien,TienChietKhau,IDKhachHang,ISNULL(SUM(SoTien2),0)DaThu FROM "
            sql &= " (SELECT convert(bit,0) as Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang,SoTien2 FROM PHIEUXUATKHO "

            sql &= " LEFT JOIN (SELECT SoTien,SoTien AS SoTien2,NgayThangVS,PhieuTC0,PhieuTC1 FROM THU "
            sql &= " WHERE THU.IDKH = " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,SoTien as SoTien2,NgayThangVS,PhieuTC0,PhieuTC1 FROM THUNH"
            sql &= " WHERE THUNH.IDKH = " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,(0-SoTien)SoTien2,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI WHERE MucDich=230"
            sql &= " AND CHI.IDKH = " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,(0-SoTien)SoTien2,NgayThang,PhieuTC0,PhieuTC1 FROM UNC WHERE MucDich=230"
            sql &= " AND UNC.IDKH = " & gdvMaKH.EditValue
            sql &= " )tbThu ON PHIEUXUATKHO.SoPhieu=tbThu.PhieuTC1 OR PHIEUXUATKHO.SoPhieuCG=tbThu.PhieuTC0"
            sql &= " WHERE IDKhachhang = " & gdvMaKH.EditValue
            sql &= " )tb GROUP BY Chon,SoPhieu,TienTruocThue,TienThue,TongTien,TienChietKhau,IDKhachHang"
        Else
            sql &= " SELECT  top 1000 convert(bit,0) as Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang FROM PHIEUXUATKHO "
        End If
        sql &= " ORDER BY SoPhieu DESC "

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvPhieuTC0.Properties.DataSource = ds.Tables(0)
            gdvPhieuTC1.Properties.DataSource = ds.Tables(1)
            _SoLuong1 = 0
            _SoLuong2 = 0
            _SoTien1 = 0
            _SoTien2 = 0
            'While gdvSoPhieuTC0.ro
            gdvSoPhieuTC0.RefreshData()
            gdvSoPhieuTC1.RefreshData()
            If gdvPhieuTC0.Text.ToString = "" Then
                gdvPhieuTC0.EditValue = Nothing
                tbSoTienTC0.Value = 0

            End If
            If gdvPhieuTC1.Text.ToString = "" Then
                gdvPhieuTC1.EditValue = Nothing
                tbSoTienTC1.Value = 0
            End If

            If _TrangThai.isAddNew And gdvPhieuTC1.Text.ToString = "000000000" And gdvPhieuTC0.Text.ToString = "000000000" Then tbSoTien.EditValue = 0
            TinhTongTien()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvMaKH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gdvMaKH.KeyPress
        If gdvMaKH.IsPopupOpen Then
            Exit Sub
        Else
            gdvMaKH.ShowPopup()
        End If
    End Sub

    Private Sub cbTKDoiUng_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTKDoiUng.EditValueChanged
        On Error Resume Next
        Dim edit As LookUpEdit = CType(sender, LookUpEdit)
        Dim dr As DataRowView = edit.GetSelectedDataRow
        tbNoiMoTKDU.EditValue = dr("Ten")
    End Sub

    Private idPhieuThu As Object
    Private arrIdPhieuThu As List(Of Object)
    Public Function GhiLai() As Boolean

        'If cbNguoiNop.Text.Trim = "" Then
        '    ShowCanhBao("Chưa có thông tin người nộp")
        '    ActiveControl = cbNguoiNop
        '    Return False
        'End If

        If tbSoTien.EditValue = 0 And _TrangThai.isAddNew Then
            ShowCanhBao("Chưa có thông tin số tiền")
            Return False
        End If
        If tbDienGiai.EditValue = "" Then
            ShowCanhBao("Chưa có thông tin diễn giải")
            Return False
        End If
        If cbMucDich.EditValue = Nothing Then
            ShowCanhBao("Chưa có mục đích thu chi")
            Return False
        End If

        If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled And gdvData.DataRowCount = 0 Then
            ShowCanhBao("Chưa có thông tin phiếu CG hoặc XK")
            ActiveControl = gdvPhieuTC1
            Return False
        End If

        Dim _TenBang As String = ""

        If ThuNH Then
            _TenBang = "THUNH"
        Else
            _TenBang = "THU"
        End If

        If _TrangThai.isAddNew Then
            tbSoPhieu.Text = LaySoPhieuT(_TenBang)
        End If

        Try

            Dim sophieuCtBegin As Integer = Convert.ToInt32(LaySoPhieu(_TenBang))

            ' BeginTransaction()
            Dim countLoop As Integer = 1
            If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                countLoop = gdvData.DataRowCount
            End If

            Dim sophieuCtBegin2 As String = ""
            arrIdPhieuThu = New List(Of Object)

            For i As Integer = 0 To countLoop - 1

                If gdvData.GetRowCellValue(i, "SoPhieu") Is Nothing Or IsDBNull(gdvData.GetRowCellValue(i, "SoPhieu")) Then
                    sophieuCtBegin2 = LaySoPhieu(_TenBang)
                Else
                    sophieuCtBegin2 = gdvData.GetRowCellValue(i, "SoPhieu")
                End If

                AddParameter("@NgaythangCT", tbNgayCT.EditValue)
                AddParameter("@NgaythangVS", tbNgayVS.EditValue)
                AddParameter("@IDkh", gdvMaKH.EditValue)
                AddParameter("@TienTe", cbTienTe.EditValue)
                AddParameter("@Mucdich", cbMucDich.EditValue)
                AddParameter("@IDUser", TaiKhoan)

                If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                    AddParameter("@PhieuTC0", gdvData.GetRowCellValue(i, "PhieuCG"))
                    AddParameter("@PhieuTC1", gdvData.GetRowCellValue(i, "PhieuXK"))
                    AddParameter("@Diengiai", gdvData.GetRowCellValue(i, "NoiDung"))
                    AddParameter("@Sotien", gdvData.GetRowCellValue(i, "SoTien"))
                    AddParameter("@Tamung", gdvData.GetRowCellValue(i, "Tamung"))
                Else
                    AddParameter("@PhieuTC0", "000000000")
                    AddParameter("@PhieuTC1", "000000000")
                    AddParameter("@Diengiai", tbDienGiai.Text)
                    AddParameter("@Sotien", tbSoTien.Value)
                    AddParameter("@Tamung", chkTamUng.Checked)
                End If

                AddParameter("@Diengiaichung", tbDienGiai.Text)

                If ThuNH Then
                    AddParameter("@TaiKhoanDi", tbTKGui.EditValue)
                    AddParameter("@NganHangDi", tbNoiMoTkGui.EditValue)
                    AddParameter("@TaiKhoanDen", cbTKDoiUng.EditValue)
                    AddParameter("@NganHangDen", tbNoiMoTKDU.EditValue)
                Else
                    AddParameter("@Nguoinop", cbNguoiNop.EditValue)
                    AddParameter("@Chungtugoc", tbChungTuGoc.EditValue)
                    If cbTKDoiUng.EditValue Is Nothing Then
                        AddParameter("@MaTK", 0)
                    Else
                        AddParameter("@MaTK", cbTKDoiUng.EditValue)
                    End If
                End If

                'If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then

                'Else

                'End If

                If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                    If gdvData.GetRowCellValue(i, "SoPhieu") Is Nothing Or IsDBNull(gdvData.GetRowCellValue(i, "SoPhieu")) Or _TrangThai.isAddNew = True Then
                        AddParameter("@SophieuT", tbSoPhieu.Text)
                        AddParameter("@Sophieu", sophieuCtBegin2)
                        If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                            gdvData.SetRowCellValue(i, "SoPhieu", sophieuCtBegin2.ToString)
                        End If
                        '  sophieuCtBegin += 1
                        idPhieuThu = doInsert(_TenBang)
                        If idPhieuThu Is Nothing Then
                            tbSoPhieu.EditValue = Nothing
                            Throw New Exception(LoiNgoaiLe)
                        End If
                        If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                            gdvData.SetRowCellValue(i, "ID", idPhieuThu)
                        End If
                    Else


                        If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                            idPhieuThu = gdvData.GetRowCellValue(i, "ID")
                            AddParameterWhere("@ID", gdvData.GetRowCellValue(i, "ID"))
                        Else
                            AddParameterWhere("@ID", idPhieuThu)
                        End If
                        If doUpdate(_TenBang, "ID=@ID") Is Nothing Then
                            Throw New Exception(LoiNgoaiLe)
                        End If
                    End If
                Else
                    If _TrangThai.isAddNew = True Then
                        AddParameter("@SophieuT", tbSoPhieu.Text)
                        AddParameter("@Sophieu", sophieuCtBegin2)

                        idPhieuThu = doInsert(_TenBang)
                        If idPhieuThu Is Nothing Then
                            tbSoPhieu.EditValue = Nothing
                            Throw New Exception(LoiNgoaiLe)
                        End If

                    Else

                        AddParameterWhere("@ID", idPhieuThu)

                        If doUpdate(_TenBang, "ID=@ID") Is Nothing Then
                            Throw New Exception(LoiNgoaiLe)
                        End If
                    End If
                End If


                arrIdPhieuThu.Add(idPhieuThu)

            Next



            '***********************************************************************************************
            'Đưa sang bên thuế
            '***********************************************************************************************


            If chkLapPhieuThu.Checked And chkLapPhieuThu.Enabled = True Then

                If idChungTuThue Is Nothing Then
                    If ThuNH Then
                        txtSoPhieuCT.Text = ChungTu.LaySoPhieu(ChungTu.LoaiChungTu.NopTienNganHang)
                    Else
                        txtSoPhieuCT.Text = ChungTu.LaySoPhieu(ChungTu.LoaiChungTu.PhieuThuTienMat)
                    End If
                    AddParameter("@NgayCT", tbNgayCT.EditValue)
                    AddParameter("@SoCT", txtSoPhieuCT.Text)
                End If

                If ThuNH Then
                    AddParameter("@LoaiCT", ChungTu.LoaiChungTu.NopTienNganHang)
                Else
                    AddParameter("@LoaiCT", ChungTu.LoaiChungTu.PhieuThuTienMat)
                End If
                AddParameter("@IdKH", gdvMaKH.EditValue)
                AddParameter("@TenKH", CType(gdvMaKH.Properties.DataSource, DataTable).Select("ID=" & gdvMaKH.EditValue)(0)("Ten"))
                AddParameter("@NguoiLienHe", cbNguoiNop.EditValue)
                AddParameter("@TienTe", cbTienTe.EditValue)
                AddParameter("@DienGiai", txtNoiDungPhieuThu.EditValue)
                AddParameter("@ThanhTien", tbSoTien.EditValue)
                AddParameter("@TongTien", tbSoTien.EditValue)
                If idChungTuThue Is Nothing Then
                    AddParameter("@NguoiLap", TaiKhoan)
                End If
                Dim isAddNew As Boolean = False
                If idChungTuThue Is Nothing Then
                    isAddNew = True
                    idChungTuThue = doInsert("CHUNGTU")
                Else
                    AddParameterWhere("@Id", idChungTuThue)
                    doUpdate("CHUNGTU", "Id=@Id")
                End If
                '--Ben chi tiet--
                AddParameter("@Id_CT", idChungTuThue)
                AddParameter("@DienGiai", txtNoiDungPhieuThu.EditValue)
                AddParameter("@ThanhTien", tbSoTien.EditValue)
                If ThuNH Then
                    AddParameter("@TaiKhoanNo", "112")
                    AddParameter("@TaiKhoanCo", "131")
                Else
                    AddParameter("@TaiKhoanNo", "111")
                    AddParameter("@TaiKhoanCo", "131")
                End If
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
                AddParameter("@GhiChuKhac", tbSoPhieu.EditValue)
                If isAddNew Then
                    doInsert("CHUNGTUCHITIET")
                Else
                    AddParameterWhere("@Id_CT", idChungTuThue)
                    doUpdate("CHUNGTUCHITIET", "Id_CT=@Id_CT")
                End If
                For Each objId As Object In arrIdPhieuThu
                    AddParameter("@IdChungTu", idChungTuThue)
                    AddParameterWhere("@Id", objId)
                    doUpdate(_TenBang, "Id=@Id")
                Next

                txtSoPhieuCT.Enabled = True
            End If

            If _TrangThai.isAddNew Then
                ShowAlert("Đã thêm phiếu thu!")
                _TrangThai.isUpdate = True
                Me.Text = "Cập nhật phiếu thu"
            Else
                ShowAlert("Đã cập nhật phiếu thu!")

            End If


            '  ComitTransaction()
            UpdateTrangThaiThu()
            btIn.Enabled = True

            isCapNhatOK = True
            Return True

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            ' RollBackTransaction()
            Return False
        End Try

    End Function


    Private isCapNhatOK As Boolean = False

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Public Sub UpdateTrangThaiThu()

        Application.DoEvents()

        ShowWaiting("Đang update các khoản thu ...")

        For i As Integer = 0 To gdvData.RowCount - 1
            Dim sql As String = ""
            If gdvData.GetRowCellValue(i, "PhieuCG") <> "000000000" And gdvData.GetRowCellValue(i, "PhieuXK") = "000000000" Then
                sql = " UPDATE tblCongNo SET TrangThai=0 WHERE Loai=0 AND SoPhieu1='" & gdvData.GetRowCellValue(i, "PhieuCG") & "' AND SoTien= " & Math.Round(gdvData.GetRowCellValue(i, "SoTien"), 0, MidpointRounding.AwayFromZero)

            ElseIf gdvData.GetRowCellValue(i, "PhieuCG") = "000000000" And gdvData.GetRowCellValue(i, "PhieuXK") <> "000000000" Then
                'sql &= " DECLARE @SPCG nvarchar(10)"
                'sql &= " DECLARE @SPXK nvarchar(10)"
                'sql &= " SET @SPCG=(SELECT SoPhieuCG FROM PHIEUXUATKHO WHERE SoPhieu='" & gdvData.GetRowCellValue(i, "PhieuXK") & "')"
                'sql &= " SET @SPXK='" & gdvData.GetRowCellValue(i, "PhieuXK") & "'"
                'sql &= " Update tblCongNo SET "
                'sql &= "         TrangThai = 0"
                'sql &= "         WHERE"
                'sql &= " Loai=0 AND SoPhieu2 IN ("
                'sql &= " Select SoPhieuXK"
                'sql &= " FROM("

                'sql &= " SELECT SoPhieuXK,PhaiThu,SUM(SoTien2)DaThu"
                'sql &= " FROM("
                'sql &= " SELECT   PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
                'sql &= "   (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia AS PhaiThu, ISNULL(tbThu.Sotien, 0) AS TienThu,ISNULL(SoTien2,0)SoTien2"
                'sql &= " FROM PHIEUXUATKHO "
                'sql &= " INNER JOIN (SELECT SoTien,SoTien AS SoTien2,(N'TT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THU "
                'sql &= " WHERE THU.PhieuTC1 =@SPXK OR THU.PhieuTC0=@SPCG "
                'sql &= " UNION ALL "
                'sql &= " SELECT SoTien,SoTien as SoTien2,(N'CK ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THUNH"
                'sql &= " WHERE THUNH.PhieuTC1 =@SPXK OR THUNH.PhieuTC0=@SPCG "
                'sql &= " UNION ALL "
                'sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI WHERE MucDich=230"
                'sql &= " AND (CHI.PhieuTC1 =@SPXK OR CHI.PhieuTC0=@SPCG) "
                'sql &= " UNION ALL "
                'sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC WHERE MucDich=230"
                'sql &= " AND (UNC.PhieuTC1 =@SPXK OR UNC.PhieuTC0=@SPCG) "
                'sql &= " )tbThu ON PHIEUXUATKHO.Sophieu = tbThu.PhieuTC1 OR PHIEUXUATKHO.SophieuCG = tbThu.PhieuTC0  "
                'sql &= " )tb"
                'sql &= " GROUP BY SoPhieuXK,PhaiThu"

                'sql &= " )tb2 WHERE (PhaiThu >50000 AND (PhaiThu-DaThu) <=50000) OR (PhaiThu <=50000 AND (PhaiThu-DaThu) <=2000)"
                'sql &= " )"

                sql &= " SET DATEFORMAT DMY"
                sql &= " DECLARE @SPCG nvarchar(10)"
                sql &= " DECLARE @SPXK nvarchar(10)"
                sql &= " DECLARE @tbDaThu table"
                sql &= " ("
                sql &= " 	SoPhieuXK nvarchar(10),"
                sql &= " 	PhaiThu float,"
                sql &= " 	DaThu float"
                sql &= " )"

                sql &= " SET @SPCG=(SELECT DISTINCT SoPhieuCG FROM PHIEUXUATKHO WHERE SoPhieu='" & gdvData.GetRowCellValue(i, "PhieuXK") & "')"
                sql &= " SET @SPXK='" & gdvData.GetRowCellValue(i, "PhieuXK") & "'"

                sql &= " INSERT INTO @tbDaThu(SoPhieuXK,PhaiThu,DaThu)"
                sql &= " SELECT SoPhieuXK,PhaiThu,DaThu FROM("
                sql &= " SELECT SoPhieuXK,PhaiThu,SUM(SoTien2)DaThu"
                sql &= " FROM("
                sql &= " SELECT   PHIEUXUATKHO.Sophieu AS SoPhieuXK, "
                sql &= "   (PHIEUXUATKHO.Tientruocthue + PHIEUXUATKHO.Tienthue) * PHIEUXUATKHO.Tygia AS PhaiThu, ISNULL(tbThu.Sotien, 0) AS TienThu,ISNULL(SoTien2,0)SoTien2"
                sql &= " FROM PHIEUXUATKHO "
                sql &= " INNER JOIN (SELECT SoTien,SoTien AS SoTien2,(N'TT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THU "
                sql &= " WHERE THU.PhieuTC1 =@SPXK OR THU.PhieuTC0=@SPCG "
                sql &= " UNION ALL "
                sql &= " SELECT SoTien,SoTien as SoTien2,(N'CK ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM THUNH"
                sql &= " WHERE THUNH.PhieuTC1 =@SPXK OR THUNH.PhieuTC0=@SPCG "
                sql &= " UNION ALL "
                sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI WHERE MucDich=230"
                sql &= " AND (CHI.PhieuTC1 =@SPXK OR CHI.PhieuTC0=@SPCG) "
                sql &= " UNION ALL "
                sql &= " SELECT SoTien,(0-SoTien)SoTien2,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC WHERE MucDich=230"
                sql &= " AND (UNC.PhieuTC1 =@SPXK OR UNC.PhieuTC0=@SPCG) "
                sql &= " )tbThu ON PHIEUXUATKHO.Sophieu = tbThu.PhieuTC1 OR PHIEUXUATKHO.SophieuCG = tbThu.PhieuTC0  "
                sql &= " )tb"
                sql &= " GROUP BY SoPhieuXK,PhaiThu"
                sql &= " )tb2 WHERE (PhaiThu >50000 AND (PhaiThu-DaThu) <=50000) OR (PhaiThu <=50000 AND (PhaiThu-DaThu) <=2000)"

                sql &= " Update tblCongNo SET "
                sql &= "         TrangThai = 0"
                sql &= "         WHERE"
                sql &= " Loai=0 AND SoPhieu2 IN ("
                sql &= " Select [@tbDaThu].SoPhieuXK"
                sql &= " FROM @tbDaThu"
                sql &= " )"

                sql &= " DECLARE @DaThu float"
                sql &= " DECLARE @DuKien float"

                sql &= " SET @DaThu=(Select [@tbDaThu].DaThu FROM @tbDaThu)"
                sql &= " SET @DuKien = (SELECT SUM(SoTien) FROM tblCongNo WHERE Loai=0 AND SoPhieu1=@SPCG)"

                sql &= " IF (@DuKien >50000 AND (@DuKien-@DaThu) <=50000) OR (@DuKien <=50000 AND (@DuKien-@DaThu) <=2000)"
                sql &= " BEGIN"
                sql &= "    Update tblCongNo SET "
                sql &= "         TrangThai = 0"
                sql &= "         WHERE"
                sql &= "    Loai=0 AND SoPhieu1 = @SPCG "
                sql &= " End"

            End If

            If sql <> "" Then
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If

        Next

        CloseWaiting()

    End Sub

    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click
        If GhiLai() Then
            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = deskTop.mThuTienMat.Name Then
                    Dim index As Integer = CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuTienMat).gdvThuCT.FocusedRowHandle
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuTienMat).LoadThu()

                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuTienMat).gdvThuCT.ClearSelection()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuTienMat).gdvThuCT.SelectRow(index)
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuTienMat).gdvThuCT.FocusedRowHandle = index
                ElseIf deskTop.tabMain.TabPages(i).Tag = deskTop.mThuNganHang.Name Then
                    Dim index As Integer = CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuNganHang).gdvThuNHCT.FocusedRowHandle
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuNganHang).LoadThuNH()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuNganHang).gdvThuNHCT.ClearSelection()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuNganHang).gdvThuNHCT.SelectRow(index)
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuNganHang).gdvThuNHCT.FocusedRowHandle = index

                End If
            Next
        End If
    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        If GhiLai() Then

            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = deskTop.mThuTienMat.Name Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuTienMat).LoadThu()
                ElseIf deskTop.tabMain.TabPages(i).Tag = deskTop.mThuNganHang.Name Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmThuNganHang).LoadThuNH()
                End If
            Next
            tbNguoiLap.EditValue = NguoiDung
            tbSoPhieu.EditValue = Nothing
            chkLapPhieuThu.Enabled = True
            _TrangThai.isAddNew = True
            tbDienGiai.Text = ""
            While gdvData.DataRowCount > 0
                gdvData.DeleteRow(0)
            End While
            btIn.Enabled = False
            tbSoTien.EditValue = 0

            'Trang thai so phieu vao
            chkLapPhieuThu.Enabled = True
            chkLapPhieuThu.Checked = False
            txtSoPhieuCT.Text = ""
            txtSoPhieuCT.Enabled = False
            txtNoiDungPhieuThu.Text = ""
            idChungTuThue = Nothing

        End If
    End Sub

    Private Sub TinhTongTien()
        gdvData.CloseEditor()
        gdvData.UpdateCurrentRow()
        Dim tongTien As Double = 0
        For i As Integer = 0 To gdvData.DataRowCount - 1
            tongTien += gdvData.GetRowCellValue(i, "SoTien")
        Next
        tbSoTien.Value = tongTien


    End Sub

    Public _IsLoadData As Boolean = False

    Private Sub gdvMaKH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvMaKH.EditValueChanged
        '_IsLoadData = True
        If Not gdvMaKH.IsPopupOpen Then
            LoadCGXK()
            LoadNguoiNop()
            '   _IsLoadData = False
        End If

        chkLapPhieuThu_CheckedChanged(chkLapPhieuThu, New System.EventArgs())
    End Sub


    Private Sub gdvMaKH_QueryCloseUp(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles gdvMaKH.QueryCloseUp
        'If _IsLoadData Then
        '    LoadCGXK()
        '    LoadNguoiNop()
        '    _IsLoadData = False
        'End If

    End Sub

    Private Sub gdvPhieuTC0_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gdvPhieuTC0.ButtonClick
        If e.Button.Index = 1 Then
            gdvPhieuTC0.EditValue = Nothing
        End If
    End Sub

    Private Sub gdvPhieuTC1_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gdvPhieuTC1.ButtonClick
        If e.Button.Index = 1 Then
            gdvPhieuTC1.EditValue = Nothing
        End If
    End Sub

    Private Sub cbMucDich_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbMucDich.EditValueChanged
        If cbMucDich.EditValue <> 100 And cbMucDich.EditValue <> 105 Then
            gdvPhieuTC0.EditValue = Nothing
            gdvPhieuTC1.EditValue = Nothing
            gdvPhieuTC0.Enabled = False
            gdvPhieuTC1.Enabled = False

            gdv.Enabled = False
            tbSoTienTC0.Enabled = False
            tbSoTienTC1.Enabled = False
            btnAddPhieuCG.Enabled = False
            btnAddPhieuXK.Enabled = False
            chkTamUng.Enabled = True
            ' cbTienTe.Enabled = True
            tbSoTien.Enabled = True

            tbSoTienTC0.Value = 0
            tbSoTienTC1.Value = 0
            If _TrangThai.isAddNew Then tbSoTien.EditValue = 0
        Else
            gdvPhieuTC0.Enabled = True
            gdvPhieuTC1.Enabled = True

            gdv.Enabled = True
            tbSoTienTC0.Enabled = True
            tbSoTienTC1.Enabled = True
            btnAddPhieuCG.Enabled = True
            btnAddPhieuXK.Enabled = True
            chkTamUng.Enabled = False
            ' cbTienTe.Enabled = False
            tbSoTien.Enabled = False

            If gdvPhieuTC0.Text.ToString <> "" And gdvPhieuTC0.Text <> "000000000" Then
                tbDienGiai.EditValue = cbMucDich.Text & " CG" & gdvPhieuTC0.Text
            ElseIf gdvPhieuTC1.Text.ToString <> "" And gdvPhieuTC1.Text <> "000000000" Then
                tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
            Else
                tbDienGiai.EditValue = ""
            End If

        End If

        If cbMucDich.EditValue <> 101 And ThuNH = False Then
            cbTKDoiUng.Enabled = False
            tbNoiMoTKDU.Enabled = False
            cbTKDoiUng.EditValue = Nothing
            tbNoiMoTKDU.EditValue = ""
        Else
            cbTKDoiUng.Enabled = True
            tbNoiMoTKDU.Enabled = True
            If Not gdvMaKH.EditValue Is Nothing And _TrangThai.isAddNew Then
                Dim dr As DataRowView = gdvMaKH.GetSelectedDataRow
                cbTKDoiUng.EditValue = dr("ttcTaiKhoan")
            End If
        End If

    End Sub

    Private Sub gdvPhieuTC0_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvPhieuTC0.EditValueChanged
        On Error Resume Next
        If gdvPhieuTC0.EditValue <> "000000000" And Not gdvPhieuTC0.EditValue Is Nothing Then
            gdvPhieuTC1.EditValue = Nothing
            tbSoTienTC1.Value = 0
            '   tbDienGiai.EditValue = cbMucDich.Text & " CG" & gdvPhieuTC0.Text
            Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
            Dim dr As DataRowView = edit.GetSelectedDataRow
            tbSoTienTC0.Value = dr("TongTien")
            'If TrangThai.isAddNew Then tbSoTien.EditValue = dr("TongTien")
            gdvMaKH.EditValue = dr("IDKhachHang")
        Else
            'If gdvPhieuTC1.EditValue = "0000000" Or gdvPhieuTC1.EditValue Is Nothing Then
            '    tbDienGiai.EditValue = ""
            'End If
            tbSoTienTC0.Value = 0
        End If
    End Sub

    Private Sub gdvPhieuTC1_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvPhieuTC1.EditValueChanged
        If gdvPhieuTC1.EditValue <> "000000000" And Not gdvPhieuTC1.EditValue Is Nothing Then
            gdvPhieuTC0.EditValue = Nothing
            ' tbSoTienTC0.Value = 0
            'tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
            Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
            Dim dr As DataRowView = edit.GetSelectedDataRow
            tbSoTienTC1.Value = dr("TongTien")
            'If TrangThai.isAddNew Then tbSoTien.EditValue = dr("TongTien")
            gdvMaKH.EditValue = dr("IDKhachHang")
        Else
            'If gdvPhieuTC0.EditValue = "0000000" Or gdvPhieuTC0.EditValue Is Nothing Then
            '    tbDienGiai.EditValue = ""
            'End If
            tbSoTienTC1.Value = 0
        End If
    End Sub

    Private Sub gdvMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gdvMaKH.ButtonClick
        If e.Button.Index = 1 Then
            gdvMaKH.EditValue = Nothing

        End If
    End Sub

    Private Sub btIn_Click(sender As System.Object, e As System.EventArgs) Handles btIn.Click
        If tbSoPhieu.EditValue Is Nothing Then
            ShowCanhBao("Chưa tồn tại số phiếu !")
            Exit Sub
        End If
        Dim sql As String = ""
        sql = "SELECT SoPhieuT as SoPhieu,NgayThangCT,NguoiNop,DienGiaiChung as DienGiai,SUM(SoTien) as SoTien,ChungTuGoc, "
        sql &= "(SELECT Ten FROM KHACHHANG WHERE ID=THU.IDKh)DiaChi, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID=THU.IDUser)NguoiLap "
        sql &= "FROM THU WHERE SoPhieuT = @SoPhieu "
        sql &= "GROUP BY SoPhieuT,NgayThangCT,NguoiNop,DienGiaiChung,ChungTuGoc,IDKh,IDUser "
        AddParameterWhere("@SoPhieu", tbSoPhieu.EditValue)
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

    Private Sub gdvData_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvData.CellValueChanged
        If e.Column.FieldName = "SoTien" Then
            TinhTongTien()
        End If
    End Sub

    Private Sub btnAddPhieuCG_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPhieuCG.Click
        If _SoLuong1 > 0 Then
            For i As Integer = 0 To gdvSoPhieuTC0.RowCount - 1
                If gdvSoPhieuTC0.GetRowCellValue(i, "Chon") Then
                    Dim _e As Boolean = False
                    For j As Integer = 0 To gdvData.DataRowCount - 1
                        If gdvData.GetRowCellValue(j, "PhieuCG").ToString = gdvSoPhieuTC0.GetRowCellValue(i, "SoPhieu") Then
                            _e = True
                            Exit For
                        End If

                    Next
                    If _e = True Then Continue For
                    gdvData.AddNewRow()
                    gdvData.SetFocusedRowCellValue("PhieuCG", gdvSoPhieuTC0.GetRowCellValue(i, "SoPhieu"))
                    gdvData.SetFocusedRowCellValue("PhieuXK", "000000000")
                    gdvData.SetFocusedRowCellValue("SoTien", gdvSoPhieuTC0.GetRowCellValue(i, "TongTien"))
                    gdvData.SetFocusedRowCellValue("Tamung", False)
                    If cbMucDich.EditValue <> 100 And cbMucDich.EditValue <> 105 Then
                        gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text)
                    Else
                        gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " CG" & gdvSoPhieuTC0.GetRowCellValue(i, "SoPhieu"))
                    End If

                    TinhTongTien()
                    If cbMucDich.EditValue = 100 Or cbMucDich.EditValue = 105 Then
                        tbDienGiai.EditValue = cbMucDich.Text
                        For k As Integer = 0 To gdvData.DataRowCount - 1
                            If gdvData.GetRowCellValue(k, "PhieuCG").ToString <> "000000000" Then
                                tbDienGiai.EditValue &= " CG" & gdvData.GetRowCellValue(k, "PhieuCG").ToString
                            Else
                                tbDienGiai.EditValue &= " XK" & gdvData.GetRowCellValue(k, "PhieuXK").ToString
                            End If
                            If k < gdvData.DataRowCount - 1 Then
                                tbDienGiai.EditValue &= ","
                            End If
                        Next
                    End If

                    ' End If


                End If
            Next
        Else
            If gdvPhieuTC0.EditValue Is Nothing Then Exit Sub
            For i As Integer = 0 To gdvData.DataRowCount - 1
                If gdvData.GetRowCellValue(i, "PhieuCG").ToString = gdvPhieuTC0.EditValue.ToString Then Exit Sub
            Next
            gdvData.AddNewRow()
            gdvData.SetFocusedRowCellValue("PhieuCG", gdvPhieuTC0.EditValue)
            gdvData.SetFocusedRowCellValue("PhieuXK", "000000000")
            gdvData.SetFocusedRowCellValue("SoTien", tbSoTienTC0.EditValue)
            gdvData.SetFocusedRowCellValue("Tamung", False)
            If cbMucDich.EditValue <> 100 And cbMucDich.EditValue <> 105 Then
                gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text)
            Else
                gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " CG" & gdvPhieuTC0.EditValue)
            End If

            TinhTongTien()
            If cbMucDich.EditValue = 100 Or cbMucDich.EditValue = 105 Then
                tbDienGiai.EditValue = cbMucDich.Text
                For i As Integer = 0 To gdvData.DataRowCount - 1
                    If gdvData.GetRowCellValue(i, "PhieuCG").ToString <> "000000000" Then
                        tbDienGiai.EditValue &= " CG" & gdvData.GetRowCellValue(i, "PhieuCG").ToString
                    Else
                        tbDienGiai.EditValue &= " XK" & gdvData.GetRowCellValue(i, "PhieuXK").ToString
                    End If
                    If i < gdvData.DataRowCount - 1 Then
                        tbDienGiai.EditValue &= ","
                    End If
                Next
            End If
        End If



    End Sub


    Private Sub btnAddPhieuXK_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPhieuXK.Click
        If _SoLuong2 > 0 Then
            For i As Integer = 0 To gdvSoPhieuTC1.RowCount - 1
                If gdvSoPhieuTC1.GetRowCellValue(i, "Chon") Then
                    Dim _e As Boolean = False
                    For j As Integer = 0 To gdvData.DataRowCount - 1
                        If gdvData.GetRowCellValue(j, "PhieuXK").ToString = gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu") Then

                            Exit For
                        End If
                    Next
                    If _e = True Then Continue For
                    gdvData.AddNewRow()
                    gdvData.SetFocusedRowCellValue("PhieuCG", "000000000")
                    gdvData.SetFocusedRowCellValue("PhieuXK", gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                    gdvData.SetFocusedRowCellValue("SoTien", gdvSoPhieuTC1.GetRowCellValue(i, "TongTien"))
                    gdvData.SetFocusedRowCellValue("Tamung", False)
                    If cbMucDich.EditValue <> 100 And cbMucDich.EditValue <> 105 Then
                        gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text)
                    Else
                        gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " XK" & gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                    End If

                    TinhTongTien()
                    If cbMucDich.EditValue = 100 Or cbMucDich.EditValue = 105 Then
                        tbDienGiai.EditValue = cbMucDich.Text
                        For k As Integer = 0 To gdvData.DataRowCount - 1
                            If gdvData.GetRowCellValue(k, "PhieuCG").ToString <> "000000000" Then
                                tbDienGiai.EditValue &= " CG" & gdvData.GetRowCellValue(k, "PhieuCG").ToString
                            Else
                                tbDienGiai.EditValue &= " XK" & gdvData.GetRowCellValue(k, "PhieuXK").ToString
                            End If
                            If k < gdvData.DataRowCount - 1 Then
                                tbDienGiai.EditValue &= ","
                            End If
                        Next
                    End If
                    '  End If

                End If
            Next

        Else
            If gdvPhieuTC1.EditValue Is Nothing Then Exit Sub
            For i As Integer = 0 To gdvData.DataRowCount - 1
                If gdvData.GetRowCellValue(i, "PhieuXK").ToString = gdvPhieuTC1.EditValue.ToString Then Exit Sub
            Next
            gdvData.AddNewRow()
            gdvData.SetFocusedRowCellValue("PhieuCG", "000000000")
            gdvData.SetFocusedRowCellValue("PhieuXK", gdvPhieuTC1.EditValue)
            gdvData.SetFocusedRowCellValue("SoTien", tbSoTienTC1.EditValue)
            gdvData.SetFocusedRowCellValue("Tamung", False)
            If cbMucDich.EditValue <> 100 And cbMucDich.EditValue <> 105 Then
                gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text)
            Else
                gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " XK" & gdvPhieuTC1.EditValue)
            End If

            TinhTongTien()
            If cbMucDich.EditValue = 100 Or cbMucDich.EditValue = 105 Then
                tbDienGiai.EditValue = cbMucDich.Text
                For i As Integer = 0 To gdvData.DataRowCount - 1
                    If gdvData.GetRowCellValue(i, "PhieuCG").ToString <> "000000000" Then
                        tbDienGiai.EditValue &= " CG" & gdvData.GetRowCellValue(i, "PhieuCG").ToString
                    Else
                        tbDienGiai.EditValue &= " XK" & gdvData.GetRowCellValue(i, "PhieuXK").ToString
                    End If
                    If i < gdvData.DataRowCount - 1 Then
                        tbDienGiai.EditValue &= ","
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub gdvData_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvData.RowCellClick
        'If e.Button = Windows.Forms.MouseButtons.Right And gdvData.FocusedRowHandle >= 0 Then
        '    pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        '    If gdvData.GetFocusedRowCellValue("SoPhieu") Is DBNull.Value Then
        '        mnuInPhieu.Enabled = False
        '        ' mnuXoaDong.Enabled = True
        '    Else
        '        mnuInPhieu.Enabled = Not ThuNH
        '        mnuXoaDong.Enabled = False
        '    End If
        'End If
    End Sub

    Private Sub mnuXoaDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaDong.ItemClick
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xóa nội dung vừa chọn ?") Then
            Dim _TenBang As String = ""
            If ThuNH Then
                _TenBang = "THUNH"
            Else
                _TenBang = "THU"
            End If
            If Not IsDBNull(gdvData.GetRowCellValue(gdvData.FocusedRowHandle, "ID")) And Not gdvData.GetRowCellValue(gdvData.FocusedRowHandle, "ID") Is Nothing Then
                'If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
                '    ShowCanhBao("Bạn cần có quyền admin để thực hiện thao tác này !")
                '    Exit Sub
                'End If
                'AddParameterWhere("@IDD", gdvData.GetRowCellValue(gdvData.FocusedRowHandle, "ID"))
                'If doDelete(_TenBang, "ID=@IDD") Is Nothing Then
                '    ShowBaoLoi(LoiNgoaiLe)
                '    Exit Sub
                'Else
                '    gdvData.DeleteRow(gdvData.FocusedRowHandle)
                'End If
            Else
                gdvData.DeleteRow(gdvData.FocusedRowHandle)
            End If


            TinhTongTien()
            If cbMucDich.EditValue = 100 Or cbMucDich.EditValue = 105 Then
                tbDienGiai.EditValue = cbMucDich.Text
                For i As Integer = 0 To gdvData.DataRowCount - 1
                    If gdvData.GetRowCellValue(i, "PhieuCG").ToString <> "000000000" Then
                        tbDienGiai.EditValue &= " CG" & gdvData.GetRowCellValue(i, "PhieuCG").ToString
                    Else
                        tbDienGiai.EditValue &= " XK" & gdvData.GetRowCellValue(i, "PhieuXK").ToString
                    End If
                    If i < gdvData.DataRowCount - 1 Then
                        tbDienGiai.EditValue &= ","
                    End If
                Next
            End If
        End If
    End Sub


    Private Sub mnuInPhieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInPhieu.ItemClick
        If gdvData.GetFocusedRowCellValue("SoPhieu") Is DBNull.Value Then
            ShowCanhBao("Chưa tồn tại số phiếu !")
            Exit Sub
        End If
        Dim sql As String = ""
        sql = "SELECT SoPhieu,NgayThangCT,NguoiNop,DienGiai,SoTien,ChungTuGoc, "
        sql &= "(SELECT Ten FROM KHACHHANG WHERE ID=THU.IDKh)DiaChi, "
        sql &= "(SELECT Ten FROM NHANSU WHERE ID=THU.IDUser)NguoiLap "
        sql &= "FROM THU WHERE SoPhieu = @SoPhieu "
        AddParameterWhere("@SoPhieu", gdvData.GetFocusedRowCellValue("SoPhieu"))
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

    Private Sub frmCNThu_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If isCapNhatOK Then Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub


    Public _SoLuong1 As Integer = 0
    Public _SoTien1 As Integer = 0

    Private Sub gdvSoPhieuTC0_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvSoPhieuTC0.KeyDown
        If e.KeyCode = Keys.Space Then
            Dim _Check As Boolean = Not gdvSoPhieuTC0.GetFocusedRowCellValue("Chon")
            gdvSoPhieuTC0.SetFocusedRowCellValue("Chon", _Check)
            gdvSoPhieuTC0.CloseEditor()
            gdvSoPhieuTC0.UpdateCurrentRow()

            If _Check Then
                _SoTien1 += gdvSoPhieuTC0.GetFocusedRowCellValue("TongTien")
                _SoLuong1 += 1
            Else
                If _SoTien1 > 0 Then
                    _SoTien1 -= gdvSoPhieuTC0.GetFocusedRowCellValue("TongTien")
                End If
                _SoLuong1 -= 1
            End If



            ' For i As Integer = 0 to gdvSoPhieuTC0
            ' gdvSoPhieuTC0.UpdateTotalSummary()
        End If

    End Sub

    Public _SoLuong2 As Integer = 0
    Public _SoTien2 As Integer = 0

    Private Sub gdvSoPhieuTC1_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvSoPhieuTC1.KeyDown
        If e.KeyCode = Keys.Space Then
            Dim _Check As Boolean = Not gdvSoPhieuTC1.GetFocusedRowCellValue("Chon")
            gdvSoPhieuTC1.SetFocusedRowCellValue("Chon", _Check)
            gdvSoPhieuTC1.CloseEditor()
            gdvSoPhieuTC1.UpdateCurrentRow()

            If _Check Then
                _SoTien2 += gdvSoPhieuTC1.GetFocusedRowCellValue("TongTien")
                _SoLuong2 += 1
            Else
                If _SoTien2 > 0 Then
                    _SoTien2 -= gdvSoPhieuTC1.GetFocusedRowCellValue("TongTien")
                End If
                _SoLuong2 -= 1
            End If



            ' For i As Integer = 0 to gdvSoPhieuTC0
            ' gdvSoPhieuTC0.UpdateTotalSummary()
        End If

    End Sub

    Private Sub gdvSoPhieuTC0_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvSoPhieuTC0.CustomSummaryCalculate
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "TongTien" Then
                e.TotalValue = _SoTien1
            End If
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "SoPhieu" Then
                e.TotalValue = _SoLuong1
            End If
        End If
    End Sub

    Private Sub gdvSoPhieuTC1_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvSoPhieuTC1.CustomSummaryCalculate
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "TongTien" Then
                e.TotalValue = _SoTien2
            End If
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "SoPhieu" Then
                e.TotalValue = _SoLuong2
            End If
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



    Private Function getTenCongTy()
        Try
            Return CType(gdvMaKH.Properties.DataSource, DataTable).Select("ID=" & gdvMaKH.EditValue)(0)("Ten")
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Sub chkLapPhieuThu_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkLapPhieuThu.CheckedChanged
        txtNoiDungPhieuThu.Properties.ReadOnly = Not chkLapPhieuThu.Checked
        If chkLapPhieuThu.Checked Then
            If ThuNH Then
                txtNoiDungPhieuThu.EditValue = getTenCongTy() & " trả tiền hàng"
            Else
                txtNoiDungPhieuThu.EditValue = getTenCongTy() & " trả tiền hàng"
            End If
        Else
            txtNoiDungPhieuThu.EditValue = ""
        End If
    End Sub

    Private Sub chkLapPhieuThu_EnabledChanged(sender As System.Object, e As System.EventArgs) Handles chkLapPhieuThu.EnabledChanged
        'If chkLapPhieuThu.Enabled Then
        '    chkLapPhieuThu.Properties.Appearance.ForeColor = Color.Red
        '    txtNoiDungPhieuThu.Enabled = True
        'Else
        '    chkLapPhieuThu.Properties.Appearance.ForeColor = Color.Blue
        '    txtNoiDungPhieuThu.Enabled = False
        'End If
    End Sub

    Private Sub txtSoPhieuCT_Click(sender As System.Object, e As System.EventArgs) Handles txtSoPhieuCT.Click

        TrangThai.isUpdate = True

        Dim f As New frmUpdateThuChiThue

        If ThuNH Then
            f.LoaiCT = ChungTu.LoaiChungTu.NopTienNganHang
        Else
            f.LoaiCT = ChungTu.LoaiChungTu.PhieuThuTienMat
        End If

        f.soPhieuT = txtSoPhieuCT.Text

        f.ShowDialog(Me)

    End Sub


End Class