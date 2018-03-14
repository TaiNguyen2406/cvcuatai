Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI


Public Class frmCNChi2
    Public PhieuChi As String
    Public UNC As Boolean
    Public arrPhieu As List(Of DanhSachPhieu) = Nothing
    Public _TrangThai As New Utils.TrangThai

    Public idChungTuThue As Object = Nothing

    Private Sub frmCNChi_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("ID", Type.GetType("System.Int64")))
        dt.Columns.Add(New DataColumn("SoPhieu", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("PhieuDH", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("PhieuNK", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("Tamung", Type.GetType("System.Boolean")))
        dt.Columns.Add(New DataColumn("SoTien", Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("NoiDung", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("IdCT", Type.GetType("System.Int64")))
        dt.Columns.Add(New DataColumn("SoCT", Type.GetType("System.String")))
        gdv.DataSource = dt

        btIn.Enabled = False

        If UNC Then
            tbChungTuGoc.Enabled = False
            cbNguoiNhan.Enabled = False
        End If

        LoadDataCb()
        If UNC Then
            tbTKNhan.Enabled = True
            tbNoiMoTkNhan.Enabled = True

        Else
            tbDienGiaiNH.Enabled = False
            tbTKNhan.Enabled = False
            tbNoiMoTkNhan.Enabled = False
            cbTKDoiUng.Enabled = False
            tbNoiMoTKDU.Enabled = False

        End If
        If CType(cbTKDoiUng.Properties.DataSource, DataTable).Rows.Count > 0 And UNC Then
            cbTKDoiUng.EditValue = CType(cbTKDoiUng.Properties.DataSource, DataTable).Rows(0)(0)
        End If


        If _TrangThai.isAddNew Then
            Dim _ToDay As DateTime = GetServerTime.Date
            tbNgayCT.EditValue = _ToDay.Date
            tbNgayVS.EditValue = _ToDay.Date
            If CType(cbMucDich.Properties.DataSource, DataTable).Rows.Count > 0 Then
                cbMucDich.EditValue = 210
            End If
            tbChungTuGoc.EditValue = "0 CT"
            tbNguoiLap.EditValue = NguoiDung



            If Not arrPhieu Is Nothing Then
                gdvMaKH.EditValue = arrPhieu(0).IDkhachHang
                tbDienGiai.Text = "Mua hàng NK" & arrPhieu(0).SoPhieu
                For i As Integer = 0 To arrPhieu.Count - 1
                    gdvData.AddNewRow()
                    gdvData.SetFocusedRowCellValue("PhieuDH", "000000000")
                    gdvData.SetFocusedRowCellValue("PhieuNK", arrPhieu(i).SoPhieu)
                    gdvData.SetFocusedRowCellValue("SoTien", arrPhieu(i).SoTien)
                    gdvData.SetFocusedRowCellValue("Tamung", False)
                    gdvData.SetFocusedRowCellValue("NoiDung", "Mua hàng NK" & arrPhieu(i).SoPhieu)
                    gdvData.CloseEditor()
                    gdvData.UpdateCurrentRow()
                Next
                TinhTongTien()
            End If



        Else

            btIn.Enabled = True


            Me.Text = "Thông tin phiếu chi " & PhieuChi
            Dim sql As String = ""
            If UNC Then
                sql = "SELECT ID,SoPhieuT,Diengiaichung,SoPhieu,NgayThang AS NgayThangCT,NgayThang AS NgayThangVS,rtrim(TaiKhoanDi)TaiKhoanDi,NganHangDi,rtrim(TaiKhoanDen)TaiKhoanDen,NganHangDen,IDKH,DienGiai,SoTien,TienTe,MucDich,IDUser,PhieuTC0 ,PhieuTC1,TamUng,(SELECT Ten FROM NHANSU WHERE ID=UNC.IDUser)NguoiLap,DienGiaiNH,"
                sql &= "IdChungTu,(select soct from chungtu where id=UNC.IdChungTu)SoCT,(select diengiai from chungtu where id=UNC.IdChungTu)DienGiaiThue,UNC.TyGia,UNC.ChiPhiNhap  FROm UNC WHERE SoPhieuT=@SoPhieu"
            Else
                sql = "SELECT ID,SoPhieuT,Diengiaichung,SoPhieu,NgayThangCT,NgayThangVS,NguoiNhan,IDKH,DienGiai,SoTien,TienTe,ChungTuGoc,MucDich,IDUser,PhieuTC0,PhieuTC1,TamUng,rtrim(MaTK)MaTK,(SELECT Ten FROM NHANSU WHERE ID=CHI.IDUser)NguoiLap,"
                sql &= "IdChungTu,(select soct from chungtu where id=CHI.IdChungTu)SoCT,(select diengiai from chungtu where id=CHI.IdChungTu)DienGiaiThue,Chi.TyGia,Chi.ChiPhiNhap  FROM CHI WHERE SoPhieuT=@SoPhieu"
            End If
            AddParameterWhere("@SoPhieu", PhieuChi)
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tbSoPhieu.EditValue = tb.Rows(0)("SoPhieuT")
                tbNgayCT.EditValue = tb.Rows(0)("NgayThangCT")
                tbNgayVS.EditValue = tb.Rows(0)("NgayThangVS")
                gdvMaKH.EditValue = tb.Rows(0)("IDKH")
                'tbSoTien.EditValue = tb.Rows(0)("SoTien")
                cbTienTe.EditValue = tb.Rows(0)("TienTe")
                txtTyGia.EditValue = tb.Rows(0)("TyGia")
                cbMucDich.EditValue = Convert.ToInt32(tb.Rows(0)("MucDich"))
                'cbMucDich.EditValue = tb.Rows(0)("MucDich")
                'tbDienGiai.EditValue = tb.Rows(0)("DienGiai")
                chkChiPhiNhap.Checked = Convert.ToBoolean(tb.Rows(0)("ChiPhiNhap"))
                If UNC Then
                    cbTKDoiUng.EditValue = tb.Rows(0)("TaiKhoanDi")
                    tbNoiMoTKDU.EditValue = tb.Rows(0)("NganHangDi")
                    tbTKNhan.EditValue = tb.Rows(0)("TaiKhoanDen")
                    tbNoiMoTkNhan.EditValue = tb.Rows(0)("NganHangDen")
                    tbDienGiaiNH.EditValue = tb.Rows(0)("DienGiaiNH")
                Else
                    cbTKDoiUng.EditValue = tb.Rows(0)("MaTK")
                    cbNguoiNhan.EditValue = tb.Rows(0)("NguoiNhan")
                    tbChungTuGoc.EditValue = tb.Rows(0)("ChungTuGoc")
                End If
                tbNguoiLap.EditValue = tb.Rows(0)("NguoiLap")

                If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        gdvData.AddNewRow()
                        gdvData.SetFocusedRowCellValue("ID", tb.Rows(i)("ID"))
                        gdvData.SetFocusedRowCellValue("SoPhieu", tb.Rows(i)("SoPhieu"))
                        gdvData.SetFocusedRowCellValue("PhieuDH", tb.Rows(i)("PhieuTC0"))
                        gdvData.SetFocusedRowCellValue("PhieuNK", tb.Rows(i)("PhieuTC1"))
                        gdvData.SetFocusedRowCellValue("SoTien", tb.Rows(i)("SoTien"))
                        gdvData.SetFocusedRowCellValue("NoiDung", tb.Rows(i)("DienGiai"))
                        gdvData.SetFocusedRowCellValue("Tamung", tb.Rows(i)("Tamung"))
                        gdvData.CloseEditor()
                        gdvData.UpdateCurrentRow()
                    Next
                    TinhTongTien()
                Else
                    tbSoTien.Value = tb.Rows(0)("SoTien")
                    chkTamUng.Checked = tb.Rows(0)("Tamung")
                    idPhieuChi = tb.Rows(0)("ID")
                End If
                tbDienGiai.EditValue = tb.Rows(0)("Diengiaichung")


                If Not tb.Rows(0)("IdChungTu") Is DBNull.Value Then
                    chkLapPhieuThu.Enabled = False
                    txtSoPhieuCT.Text = tb.Rows(0)("SoCT")
                    txtSoPhieuCT.Enabled = True
                    txtNoiDungPhieuThu.EditValue = tb.Rows(0)("DienGiaiThue")
                    chkLapPhieuThu.Properties.Appearance.ForeColor = Color.Blue

                Else
                    chkLapPhieuThu.Properties.Appearance.ForeColor = Color.Red
                    txtSoPhieuCT.Enabled = False
                End If

                btnHoaDon.Enabled = True

                'Xem có hóa đơn của phiếu này chưa
                sql = "SELECT a.SoHD FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT WHERE a.LoaiCT = @LoaiCT AND a.LoaiCT2 = @LoaiCT2 AND b.GhiChuKhac = @SoPhieuT AND b.ButToan = @ButToan"
                AddParameter("@LoaiCT", ChungTu.LoaiChungTu.HoaDonDauVao)
                AddParameter("@LoaiCT2", ChungTu.LoaiCT2.MuaDichVu)
                AddParameter("@SoPhieuT", tb.Rows(0)("SoPhieuT"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
                Dim dtHD As DataTable = ExecuteSQLDataTable(sql)
                If Not dtHD Is Nothing AndAlso dtHD.Rows.Count > 0 Then
                    btnHoaDon.Enabled = False
                    btnHoaDon.Text = "HĐ: " & dtHD.Rows(0)(0).ToString
                    btnHoaDon.Appearance.Font = New Font(Me.Font.Name, Me.Font.Size, FontStyle.Bold)
                    btnHoaDon.Appearance.ForeColor = Color.Red
                End If

            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If

        If KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) Then
            '  gdvData.Columns(colPhieuCG.FieldName).re = True
            colPhieuDH.OptionsColumn.ReadOnly = False
            colPhieuNK.OptionsColumn.ReadOnly = False

        End If


    End Sub

    Private idPhieuChi As Object

    Public Sub LoadDataCb()
        Dim sql As String = ""
        sql &= " SELECT rtrim(ltrim(MaSo))MaSo,Ten FROM TAIKHOAN "
        sql &= " SELECT ID,Ten FROm MUCDICHTHUCHI WHERE left(ID,1)=2 ORDER BY Ten"
        sql &= " SELECT ID,Ten,TyGia FROM tblTienTe"
        sql &= " SELECT ID,ttcMa,Ten,IDTakecare,ttcTaiKhoan,ttcNoiMo FROM KHACHHANG ORDER BY ttcMa "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbTKDoiUng.Properties.DataSource = ds.Tables(0)

            cbMucDich.Properties.DataSource = ds.Tables(1)
            cbTienTe.Properties.DataSource = ds.Tables(2)
            If ds.Tables(2).Rows.Count > 0 Then
                cbTienTe.EditValue = ds.Tables(2).Rows(0)(0)
            End If
            gdvMaKH.Properties.DataSource = ds.Tables(3)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadCGXK()
        Dim sql As String = ""
        If Not gdvMaKH.EditValue Is Nothing Then
            'sql &= " SELECT convert(bit,0)Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang,Isnull(tbChi.DaChi,0)DaChi "
            'sql &= ", DaTamUng, ((TienTruocThue+TienThue)*TyGia-DaTamUng) ConLai "
            sql &= " SELECT convert(bit,0)Chon, SoPhieu,TienTruocThue AS TienTruocThue, TienThue AS TienThue, (TienTruocThue+TienThue) AS TongTien,TienChietKhau AS TienChietKhau,IDKhachHang,Isnull(tbChi.DaChi,0)/TyGia DaChi "
            sql &= ", DaTamUng/TyGia DaTamUng, ((TienTruocThue+TienThue)-DaTamUng/TyGia) ConLai "
            sql &= "FROM BANGCHAOGIA "
            sql &= " LEFT JOIN "
            sql &= " (SELECT SUM(SoTien)DaChi,PhieuTC0 FROM (SELECT SoTien,PhieuTC0,PhieuTC1 FROM CHI"
            sql &= " WHERE MucDich IN (200, 224, 244, 235, 205, 230) AND CHI.IDKH= " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,PhieuTC0,PhieuTC1 FROM UNC "
            sql &= " WHERE MucDich IN (200, 224, 244, 235, 205, 230) AND UNC.IDKH= " & gdvMaKH.EditValue
            sql &= " )tb GROUP BY PhieuTC0"
            sql &= " )tbChi  ON BANGCHAOGIA.Sophieu = tbChi.PhieuTC0 "
            sql &= " WHERE IDKhachhang = " & gdvMaKH.EditValue
        Else
            sql &= " SELECT TOP 1000 convert(bit,0)Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang FROM BANGCHAOGIA "

        End If
        sql &= " ORDER BY SoPhieu DESC "


        If Not gdvMaKH.EditValue Is Nothing Then
            sql &= " SELECT Chon,SoPhieu,TienTruocThue,TienThue,TongTien,TienChietKhau,IDKhachHang,ISNULL(SUM(tb.SoTien),0) As DaChi,PhanBoTamUng PhanBo,TongTien-PhanBoTamUng as ConLai  FROM"
            sql &= " (SELECT   convert(bit,0)Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang,tbChi.SoTien,PhanBoTamUng FROM PHIEUXUATKHO "
            sql &= " LEFT JOIN "
            sql &= " (SELECT SoTien,PhieuTC0,PhieuTC1 FROM CHI"
            sql &= " WHERE MucDich IN (200, 224, 244, 235, 205, 230) AND CHI.IDKH= " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,PhieuTC0,PhieuTC1 FROM UNC "
            sql &= " WHERE MucDich IN (200, 224, 244, 235, 205, 230) AND UNC.IDKH= " & gdvMaKH.EditValue
            sql &= " )tbChi  ON PHIEUXUATKHO.Sophieu = tbChi.PhieuTC1 OR PHIEUXUATKHO.SoPhieuCG=tbChi.PhieuTC0 "
            sql &= " WHERE IDKhachhang = " & gdvMaKH.EditValue
            sql &= " )tb GROUP BY Chon,SoPhieu,TienTruocThue,TienThue,TongTien,TienChietKhau,IDKhachHang,PhanBoTamUng "
            '   sql &= " WHERE IDKhachhang = " & gdvMaKH.EditValue
        Else
            sql &= " SELECT TOP 1000  convert(bit,0)Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,TienChietKhau*TyGia AS TienChietKhau,IDKhachHang FROM PHIEUXUATKHO "

        End If
        sql &= " ORDER BY SoPhieu DESC "

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvPhieuTC0.Properties.DataSource = ds.Tables(0)
            gdvPhieuTC1.Properties.DataSource = ds.Tables(1)
            If gdvPhieuTC0.Text.ToString = "" Then
                gdvPhieuTC0.EditValue = Nothing
                tbSoTienTC0.Value = 0

            End If
            If gdvPhieuTC1.Text.ToString = "" Then
                gdvPhieuTC1.EditValue = Nothing
                tbSoTienTC1.Value = 0
            End If
            _SoLuong1 = 0
            _SoLuong2 = 0
            _SoTien1 = 0
            _SoTien2 = 0
            gdvSoPhieuTC0.RefreshData()
            gdvSoPhieuTC1.RefreshData()


            If _TrangThai.isAddNew And gdvPhieuTC1.Text.ToString = "000000000" And gdvPhieuTC0.Text.ToString = "000000000" Then tbSoTien.EditValue = 0
            TinhTongTien()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadNguoiNhan()

        cbNguoiNhan.Properties.Items.Clear()
        If Not gdvMaKH.EditValue Is Nothing Then
            Dim sql As String = ""
            sql &= " SELECT Ten FROM NHANSU WHERE NoiCtac =" & gdvMaKH.EditValue
            sql &= " UNION ALL"
            sql &= " SELECT Ten FROM NHANSU WHERE ID=(SELECT IDTakeCare FROM KHACHHANG WHERE ID=" & gdvMaKH.EditValue & ")"

            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    cbNguoiNhan.Properties.Items.Add(dt.Rows(i)(0))
                Next
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If

    End Sub

    Public Sub LoadDHNK()
        Dim sql As String = ""
        If Not gdvMaKH.EditValue Is Nothing Then
            'sql &= " SELECT convert(bit,0)Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,IDKhachHang,ISNULL(tbChi.DaChi,0)DaChi "
            'sql &= ", DaTamUng, ((TienTruocThue+TienThue)*TyGia-DaTamUng) ConLai "
            sql &= " SELECT convert(bit,0)Chon, SoPhieu,TienTruocThue AS TienTruocThue, TienThue AS TienThue, (TienTruocThue+TienThue) AS TongTien,IDKhachHang,Isnull(tbChi.DaChi,0) DaChi "
            sql &= ", DaTamUngDonTe DaTamUng, ((TienTruocThue+TienThue)-isnull(DaTamUngDonTe,0)) ConLai "
            sql &= " ,(Select Ten from tblTienTe where ID=PHIEUDATHANG.Tiente) TienTe, PHIEUDATHANG.Tiente IDTienTe"
            sql &= " FROM PHIEUDATHANG "

            sql &= " LEFT JOIN "
            sql &= " (SELECT SUM(SoTien)DaChi,PhieuTC0 FROM (SELECT SoTien,PhieuTC0,PhieuTC1 FROM CHI"
            'sql &= " WHERE MucDich IN (210, 228, 205) AND CHI.IDKH= " & gdvMaKH.EditValue
            sql &= " WHERE MucDich IN (210) AND CHI.IDKH= " & gdvMaKH.EditValue
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,PhieuTC0,PhieuTC1 FROM UNC "
            'sql &= " WHERE MucDich IN (210, 228, 205) AND UNC.IDKH= " & gdvMaKH.EditValue
            sql &= " WHERE MucDich IN (210) AND UNC.IDKH= " & gdvMaKH.EditValue
            sql &= " )tb GROUP BY PhieuTC0"
            sql &= " )tbChi  ON PHIEUDATHANG.Sophieu = tbChi.PhieuTC0 "
            sql &= " WHERE IDKhachhang = " & gdvMaKH.EditValue
        Else
            '   sql &= " SELECT TOP 1000 convert(bit,0)Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,IDKhachHang FROM PHIEUDATHANG "
            sql &= " SELECT TOP 1000 convert(bit,0)Chon, SoPhieu,TienTruocThue AS TienTruocThue, TienThue AS TienThue, (TienTruocThue+TienThue) AS TongTien,IDKhachHang,(Select Ten from tblTienTe where ID=PHIEUDATHANG.Tiente) TienTe, PHIEUDATHANG.Tiente IDTienTe FROM PHIEUDATHANG "

        End If
        sql &= " ORDER BY SoPhieu DESC "
        If Not gdvMaKH.EditValue Is Nothing Then
            sql &= " SELECT Chon,SoPhieu,TienTruocThue,TienThue,TongTien,IDKhachHang,ISNULL(SUM(tb.SoTien),0) As DaChi,PhanBoTamUngDonTe PhanBo,TongTien-PhanBoTamUngDonTe as ConLai,TienTe, IDTienTe FROM"
            'sql &= " (SELECT   convert(bit,0)Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,IDKhachHang,tbChi.SoTien,PhanBoTamUng  FROM PHIEUNHAPKHO "
            sql &= " (SELECT   convert(bit,0)Chon, SoPhieu,TienTruocThue AS TienTruocThue, TienThue AS TienThue, (TienTruocThue+TienThue) AS TongTien,IDKhachHang,tbChi.SoTien,isnull(PhanBoTamUngDonTe,0)PhanBoTamUngDonTe ,(Select Ten from tblTienTe where ID=PHIEUNHAPKHO.Tiente) TienTe,PHIEUNHAPKHO.Tiente IDTienTe  FROM PHIEUNHAPKHO "
            sql &= " LEFT JOIN "
            sql &= " (SELECT SoTien,PhieuTC0,PhieuTC1 FROM CHI"
            'sql &= " WHERE MucDich IN (210, 228, 205) AND CHI.IDKH= " & gdvMaKH.EditValue
            sql &= " WHERE CHI.IDKH= " & gdvMaKH.EditValue & " AND replace(PhieuTC1,'0','') <> '' "
            sql &= " UNION ALL "
            sql &= " SELECT SoTien,PhieuTC0,PhieuTC1 FROM UNC "
            'sql &= " WHERE MucDich IN (210, 228, 205) AND UNC.IDKH= " & gdvMaKH.EditValue
            sql &= " WHERE UNC.IDKH= " & gdvMaKH.EditValue & " AND replace(PhieuTC1,'0','') <> '' "
            sql &= " )tbChi  ON PHIEUNHAPKHO.Sophieu = tbChi.PhieuTC1 OR PHIEUNHAPKHO.SoPhieuDH=tbChi.PhieuTC0 "
            sql &= " WHERE IDKhachhang = " & gdvMaKH.EditValue
            sql &= " )tb GROUP BY Chon,SoPhieu,TienTruocThue,TienThue,TongTien,IDKhachHang,PhanBoTamUngDonTe, Tiente,IDTienTe "
        Else
            'sql &= " SELECT TOP 1000  convert(bit,0)Chon, SoPhieu,TienTruocThue*TyGia AS TienTruocThue, TienThue*TyGia AS TienThue, (TienTruocThue+TienThue)*TyGia AS TongTien,IDKhachHang FROM PHIEUNHAPKHO "
            sql &= " SELECT TOP 1000  convert(bit,0)Chon, SoPhieu,TienTruocThue AS TienTruocThue, TienThue AS TienThue, (TienTruocThue+TienThue) AS TongTien,IDKhachHang,(Select Ten from tblTienTe where ID=PHIEUNHAPKHO.Tiente) TienTe,PHIEUNHAPKHO.Tiente IDTienTe FROM PHIEUNHAPKHO "

        End If
        sql &= " ORDER BY SoPhieu DESC "

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvPhieuTC0.Properties.DataSource = ds.Tables(0)
            gdvPhieuTC1.Properties.DataSource = ds.Tables(1)
            If gdvPhieuTC0.Text.ToString = "" Then
                gdvPhieuTC0.EditValue = Nothing
                tbSoTienTC0.Value = 0

            End If
            If gdvPhieuTC1.Text.ToString = "" Then
                gdvPhieuTC1.EditValue = Nothing
                tbSoTienTC1.Value = 0
            End If
            _SoLuong1 = 0
            _SoLuong2 = 0
            _SoTien1 = 0
            _SoTien2 = 0
            gdvSoPhieuTC0.RefreshData()
            gdvSoPhieuTC1.RefreshData()

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

    Private Sub gdvMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gdvMaKH.ButtonClick
        If e.Button.Index = 1 Then
            gdvMaKH.EditValue = Nothing
        End If
    End Sub

    Public Sub CapNhatDaChi()
        Application.DoEvents()
        ShowWaiting("Đang update các khoản chi ...")
        For i As Integer = 0 To gdvData.RowCount - 1
            Dim sql As String = ""
            If gdvData.GetRowCellValue(i, "PhieuDH") <> "000000000" And gdvData.GetRowCellValue(i, "PhieuNK") = "000000000" Then
                sql = " UPDATE tblCongNo SET TrangThai=0 WHERE Loai=1 AND SoPhieu1='" & gdvData.GetRowCellValue(i, "PhieuDH") & "' AND SoTien= " & gdvData.GetRowCellValue(i, "SoTien")
            Else

                sql &= " DECLARE @SP0 nvarchar(10)"
                sql &= " DECLARE @SP1 nvarchar(10)"
                sql &= " SET @SP1='" & gdvData.GetRowCellValue(i, "PhieuNK") & "'"
                sql &= " SET @SP0= (SELECT DISTINCT SoPhieuDH FROM PHIEUNHAPKHO WHERE SoPhieu=@SP1 )"
                sql &= " Update tblCongNo SET "
                sql &= "         TrangThai = 0"
                sql &= "         WHERE"
                sql &= " Loai=1 AND SoPhieu2 IN ("
                sql &= " Select SoPhieuNK"
                sql &= " FROM("
                sql &= " SELECT SoPhieuNK,PhaiChi,Sum(TienChi) AS TienChi FROM ("
                sql &= " SELECT PHIEUNHAPKHO.Sophieu AS SoPhieuNK,"
                sql &= "   (PHIEUNHAPKHO.Tientruocthue + PHIEUNHAPKHO.Tienthue) * PHIEUNHAPKHO.Tygia AS PhaiChi, ISNULL(tbChi.Sotien, 0) AS TienChi"
                sql &= " FROM PHIEUNHAPKHO "
                sql &= " INNER JOIN (SELECT SoTien,(N'CT ' + SoPhieu)SoPhieu,NgayThangVS,PhieuTC0,PhieuTC1 FROM CHI"
                sql &= " WHERE PHIEUTC0=@SP0 OR PhieuTC1=@SP1"
                sql &= " UNION ALL "
                sql &= " SELECT SoTien,(N'UNC ' + SoPhieu)SoPhieu,NgayThang,PhieuTC0,PhieuTC1 FROM UNC "
                sql &= " WHERE PHIEUTC0=@SP0 OR PhieuTC1=@SP1"
                sql &= " )tbChi ON PHIEUNHAPKHO.Sophieu = tbChi.PhieuTC1 OR PHIEUNHAPKHO.SophieuDH = tbChi.PhieuTC0 "
                sql &= " WHERE PHIEUNHAPKHO.SoPhieu=@SP1"
                sql &= " )tb GROUP BY SoPhieuNK,PhaiChi )tb2 WHERE (PhaiChi >50000 AND (PhaiChi-TienChi) <=50000) OR (PhaiChi <=50000 AND (PhaiChi-TienChi) <=2000))"
                Application.DoEvents()
                ' ShowWaiting("Đang update các khoản chi ...")
                If ExecuteSQLNonQuery(sql) Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If

            End If
        Next
        CloseWaiting()
    End Sub


    Private _idPhieuChi As Object
    Private _arrIdPhieuChi As List(Of Object)
    Public Function GhiLai() As Boolean
        'If cbNguoiNhan.Text.Trim = "" Then
        '    ShowCanhBao("Chưa có thông tin người nhận")
        '    ActiveControl = cbNguoiNhan
        '    Return False
        'End If
        If tbSoTien.EditValue = 0 And _TrangThai.isAddNew Then
            ShowCanhBao("Chưa có thông tin số tiền")
            Return False
            Exit Function
        End If
        If tbDienGiai.EditValue = "" Then
            ShowCanhBao("Chưa có thông tin diễn giải")
            Return False
            Exit Function
        End If
        If cbMucDich.EditValue = Nothing Then
            ShowCanhBao("Chưa có mục đích thu chi")
            Return False
            Exit Function
        End If

        If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled And gdvData.DataRowCount = 0 Then
            ShowCanhBao("Chưa có thông tin phiếu ĐH hoặc NK")
            ActiveControl = gdvPhieuTC1
            Return False
        End If

        Dim _TenBang As String = ""
        If UNC Then
            _TenBang = "UNC"
        Else
            _TenBang = "CHI"
        End If
        If _TrangThai.isAddNew Then
            tbSoPhieu.EditValue = LaySoPhieuT(_TenBang)
        End If


        Try
            Dim sophieuCtBegin As Integer = Convert.ToInt32(LaySoPhieu(_TenBang))
            ' BeginTransaction()
            Dim countLoop As Integer = 1
            If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                countLoop = gdvData.DataRowCount
            End If

            Dim sophieuCtBegin2 As String = ""
            _arrIdPhieuChi = New List(Of Object)

            For i As Integer = 0 To countLoop - 1
                If gdvData.GetRowCellValue(i, "SoPhieu") Is Nothing Or IsDBNull(gdvData.GetRowCellValue(i, "SoPhieu")) Then
                    sophieuCtBegin2 = LaySoPhieu(_TenBang)
                Else
                    sophieuCtBegin2 = gdvData.GetRowCellValue(i, "SoPhieu")
                End If

                AddParameter("@IDkh", gdvMaKH.EditValue)
                AddParameter("@TienTe", cbTienTe.EditValue)
                AddParameter("@TyGia", txtTyGia.EditValue)
                AddParameter("@Mucdich", cbMucDich.EditValue)
                AddParameter("@IDUser", TaiKhoan)
                AddParameter("@ChiPhiNhap", chkChiPhiNhap.Checked)

                If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                    AddParameter("@PhieuTC0", gdvData.GetRowCellValue(i, "PhieuDH"))
                    AddParameter("@PhieuTC1", gdvData.GetRowCellValue(i, "PhieuNK"))
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
                If UNC Then
                    AddParameter("@NgayThang", tbNgayCT.EditValue)
                    AddParameter("@TaiKhoanDi", cbTKDoiUng.EditValue)
                    AddParameter("@NganHangDi", tbNoiMoTKDU.EditValue)
                    AddParameter("@TaiKhoanDen", tbTKNhan.EditValue)
                    AddParameter("@NganHangDen", tbNoiMoTkNhan.EditValue)
                    AddParameter("@DienGiaiNH", tbDienGiaiNH.EditValue)
                Else
                    AddParameter("@NgaythangCT", tbNgayCT.EditValue)
                    AddParameter("@NgaythangVS", tbNgayVS.EditValue)
                    AddParameter("@Chungtugoc", tbChungTuGoc.EditValue)
                    AddParameter("@Nguoinhan", cbNguoiNhan.EditValue)
                    If cbTKDoiUng.EditValue Is Nothing Then
                        AddParameter("@MaTK", 0)
                    Else
                        AddParameter("@MaTK", cbTKDoiUng.EditValue)
                    End If
                End If


                If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                    If gdvData.GetRowCellValue(i, "SoPhieu") Is Nothing Or IsDBNull(gdvData.GetRowCellValue(i, "SoPhieu")) Or _TrangThai.isAddNew = True Then
                        AddParameter("@SophieuT", tbSoPhieu.Text)
                        AddParameter("@Sophieu", sophieuCtBegin2)
                        If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                            gdvData.SetRowCellValue(i, "SoPhieu", sophieuCtBegin2.ToString)
                        End If
                        '  sophieuCtBegin += 1
                        idPhieuChi = doInsert(_TenBang)
                        If idPhieuChi Is Nothing Then
                            tbSoPhieu.EditValue = Nothing
                            Throw New Exception(LoiNgoaiLe)
                        End If
                        If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                            gdvData.SetRowCellValue(i, "ID", idPhieuChi)
                        End If
                    Else


                        If gdvPhieuTC0.Enabled And gdvPhieuTC1.Enabled Then
                            idPhieuChi = gdvData.GetRowCellValue(i, "ID")
                            AddParameterWhere("@ID", gdvData.GetRowCellValue(i, "ID"))
                        Else
                            AddParameterWhere("@ID", idPhieuChi)
                        End If
                        If doUpdate(_TenBang, "ID=@ID") Is Nothing Then
                            Throw New Exception(LoiNgoaiLe)
                        End If
                    End If
                    If UNC Then
                        Dim _sql As String
                        Dim LoaiDH As Integer
                        Dim TyGiaTB As Double = 0

                        If gdvData.GetRowCellValue(i, "PhieuDH") <> 0 Then

                            AddParameter("@PhieuTC0", gdvData.GetRowCellValue(i, "PhieuDH"))
                            _sql = "select LoaiDH from PHIEUDATHANG where SoPhieu=@PhieuTC0"
                            LoaiDH = ExecuteSQLScalar(_sql)
                            If LoaiDH = 1 Then
                                AddParameter("@PhieuTC0", gdvData.GetRowCellValue(i, "PhieuDH"))
                                AddParameter("@SoPhieuDH", gdvData.GetRowCellValue(i, "PhieuDH"))
                                _sql = "select isnull(sum(TyGia*Sotien)/sum(SoTien),1)  from UNC where PhieuTC0=@PhieuTC0 or PhieuTC1 in(Select SoPhieu from PHIEUNHAPKHO where SoPhieuDH=@SoPhieuDH)"
                                TyGiaTB = ExecuteSQLScalar(_sql)
                                AddParameter("@TyGia", TyGiaTB)
                                AddParameterWhere("@PhieuTC0", gdvData.GetRowCellValue(i, "PhieuDH"))
                                If doUpdate("PHIEUDATHANG", "SoPhieu=@PhieuTC0") Is Nothing Then
                                    ShowBaoLoi(LoiNgoaiLe)
                                Else
                                    AddParameter("@TyGia", TyGiaTB)
                                    AddParameterWhere("@PhieuTC0", gdvData.GetRowCellValue(i, "PhieuDH"))
                                    If doUpdate("PHIEUNHAPKHO", "SoPhieuDH=@PhieuTC0") Is Nothing Then
                                        ShowBaoLoi(LoiNgoaiLe)
                                    End If
                                End If
                            End If

                        ElseIf gdvData.GetRowCellValue(i, "PhieuNK") <> 0 Then
                            AddParameter("@PhieuTC1", gdvData.GetRowCellValue(i, "PhieuNK"))
                            Dim SoPhieuDH As String = ExecuteSQLScalar("Select SoPhieuDH from PHIEUNHAPKHO Where SoPhieuDH=@PhieuTC1")
                            AddParameter("@PhieuTC0", SoPhieuDH)
                            _sql = "select LoaiDH from PHIEUDATHANG where SoPhieu=@PhieuTC0"
                            LoaiDH = ExecuteSQLScalar(_sql)
                            If LoaiDH = 1 Then
                                AddParameter("@PhieuTC0", SoPhieuDH)
                                AddParameter("@SoPhieuDH", SoPhieuDH)
                                _sql = "select isnull(sum(TyGia*Sotien)/sum(SoTien),1)  from UNC where PhieuTC0=@PhieuTC0 or PhieuTC1 in(Select SoPhieu from PHIEUNHAPKHO where SoPhieuDH=@SoPhieuDH)"
                                TyGiaTB = ExecuteSQLScalar(_sql)
                                AddParameter("@TyGia", TyGiaTB)
                                AddParameterWhere("@PhieuTC0", SoPhieuDH)
                                If doUpdate("PHIEUDATHANG", "SoPhieu=@PhieuTC0") Is Nothing Then
                                    ShowBaoLoi(LoiNgoaiLe)
                                Else
                                    AddParameter("@TyGia", TyGiaTB)
                                    AddParameterWhere("@PhieuTC0", SoPhieuDH)
                                    If doUpdate("PHIEUNHAPKHO", "SoPhieuDH=@PhieuTC0") Is Nothing Then
                                        ShowBaoLoi(LoiNgoaiLe)
                                    End If
                                End If
                            End If
                        End If
                    End If

                Else
                    If _TrangThai.isAddNew = True Then
                        AddParameter("@SophieuT", tbSoPhieu.Text)
                        AddParameter("@Sophieu", sophieuCtBegin2)

                        idPhieuChi = doInsert(_TenBang)
                        If idPhieuChi Is Nothing Then
                            tbSoPhieu.EditValue = Nothing
                            Throw New Exception(LoiNgoaiLe)
                        End If

                    Else

                        AddParameterWhere("@ID", idPhieuChi)

                        If doUpdate(_TenBang, "ID=@ID") Is Nothing Then
                            Throw New Exception(LoiNgoaiLe)
                        End If
                    End If
                End If
                _arrIdPhieuChi.Add(idPhieuChi)

            Next


            '***********************************************************************************************
            'Đưa sang bên thuế
            '***********************************************************************************************
            If chkLapPhieuThu.Checked And chkLapPhieuThu.Enabled = True Then
                If _TrangThai.isAddNew Then
                    If idChungTuThue Is Nothing Then
                        If UNC Then
                            txtSoPhieuCT.Text = ChungTu.LaySoPhieu(ChungTu.LoaiChungTu.UyNhiemChi)
                        Else
                            txtSoPhieuCT.Text = ChungTu.LaySoPhieu(ChungTu.LoaiChungTu.PhieuChiTienMat)
                        End If
                        AddParameter("@NgayCT", tbNgayCT.EditValue)
                        AddParameter("@SoCT", txtSoPhieuCT.Text)
                    End If


                    If UNC Then
                        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.UyNhiemChi)
                    Else
                        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.PhieuChiTienMat)
                    End If

                    AddParameter("@IdKH", gdvMaKH.EditValue)
                    AddParameter("@TenKH", CType(gdvMaKH.Properties.DataSource, DataTable).Select("ID=" & gdvMaKH.EditValue)(0)("Ten"))
                    AddParameter("@NguoiLienHe", cbNguoiNhan.EditValue)
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

                    If UNC Then
                        AddParameter("@TaiKhoanNo", "131")
                        AddParameter("@TaiKhoanCo", "1121")
                    Else
                        AddParameter("@TaiKhoanNo", "131")
                        AddParameter("@TaiKhoanCo", "1111")
                    End If

                    AddParameter("@ButToan", ChungTu.LoaiButToan.HangTien)
                    AddParameter("@GhiChuKhac", tbSoPhieu.EditValue)

                    If isAddNew Then
                        doInsert("CHUNGTUCHITIET")
                    Else
                        AddParameterWhere("@Id_CT", idChungTuThue)
                        doUpdate("CHUNGTUCHITIET", "Id_CT=@Id_CT")
                    End If

                    For Each objId As Object In _arrIdPhieuChi
                        AddParameter("@IdChungTu", idChungTuThue)
                        AddParameterWhere("@Id", objId)
                        doUpdate(_TenBang, "Id=@Id")
                    Next

                    txtSoPhieuCT.Enabled = True

                End If

            End If



            '************* PHAN BO TAM UNG ************************'
            For i As Integer = 0 To gdvData.RowCount - 1
                If gdvData.GetRowCellValue(i, "PhieuDH").ToString.Replace("0", "").Trim = "" Then Continue For
                frmCNNhapKho.CapNhatPhanBoTamUng(gdvData.GetRowCellValue(i, "PhieuDH"))
            Next

            If _TrangThai.isAddNew Then
                ShowAlert("Đã thêm phiếu chi!")
                _TrangThai.isUpdate = True
                Me.Text = "Cập nhật phiếu chi"
            Else
                ShowAlert("Đã cập nhật phiếu chi!")
            End If








            ' ComitTransaction()
            CapNhatDaChi()
            btIn.Enabled = True
            isCapNhatOK = True

            If btnHoaDon.Text = "Nhập hóa đơn" Then
                btnHoaDon.Enabled = True
            End If

            If cbMucDich.EditValue = 205 And chkChiPhiNhap.Checked Then
                For i As Integer = 0 To gdvData.RowCount - 1
                    If gdvData.GetRowCellValue(i, "PhieuNK") <> "000000000" Then
                        PhanBoChiPhiNhap(gdvData.GetRowCellValue(i, "PhieuNK"))
                    End If
                Next
            End If


            Return True

        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            '  RollBackTransaction()
            Return False
        End Try

    End Function


    Private isCapNhatOK As Boolean = False
    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click
        If GhiLai() Then
            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = deskTop.mChiTienMat.Name Then
                    Dim index As Integer = CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiTienMat).gdvChiCT.FocusedRowHandle
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiTienMat).LoadChi()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiTienMat).gdvChiCT.ClearSelection()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiTienMat).gdvChiCT.SelectRow(index)
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiTienMat).gdvChiCT.FocusedRowHandle = index
                ElseIf deskTop.tabMain.TabPages(i).Tag = deskTop.mUNC.Name Then
                    Dim index As Integer = CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiNganHang).gdvUNCCT.FocusedRowHandle
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiNganHang).LoadUNC()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiNganHang).gdvUNCCT.ClearSelection()
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiNganHang).gdvUNCCT.SelectRow(index)
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiNganHang).gdvUNCCT.FocusedRowHandle = index
                End If
            Next
        End If
    End Sub


    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click

        If GhiLai() Then

            For i As Integer = 0 To deskTop.tabMain.TabPages.Count - 1
                If deskTop.tabMain.TabPages(i).Tag = deskTop.mChiTienMat.Name Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiTienMat).LoadChi()
                ElseIf deskTop.tabMain.TabPages(i).Tag = deskTop.mUNC.Name Then
                    CType(deskTop.tabMain.TabPages(i).Controls(0), frmChiNganHang).LoadUNC()
                End If
            Next

            tbNguoiLap.EditValue = NguoiDung
            tbSoPhieu.EditValue = Nothing
            _TrangThai.isAddNew = True
            tbDienGiai.Text = ""
            tbDienGiaiNH.Text = ""
            While gdvData.RowCount > 0
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


    Public _IsLoadData As Boolean = False

    Private Sub gdvMaKH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvMaKH.EditValueChanged
        ' _IsLoadData = True
        If Not gdvMaKH.IsPopupOpen Then
            LoadNguoiNhan()
            Select Case cbMucDich.EditValue
                Case 210, 228
                    LoadDHNK()
                Case 200, 224, 244, 235, 230
                    LoadCGXK()
            End Select
            If gdvMaKH.EditValue Is Nothing Then
                cbTKDoiUng.EditValue = Nothing
                tbNoiMoTKDU.EditValue = Nothing
            Else
                On Error Resume Next
                Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
                Dim dr As DataRowView = edit.GetSelectedDataRow

                If UNC Then
                    tbTKNhan.EditValue = dr("ttcTaiKhoan")
                    tbNoiMoTkNhan.EditValue = dr("ttcNoiMo")
                Else
                    If cbMucDich.EditValue = 211 Then
                        cbTKDoiUng.EditValue = dr("ttcTaiKhoan")
                    End If
                End If

            End If
        End If

        chkLapPhieuThu_CheckedChanged(chkLapPhieuThu, New System.EventArgs())

    End Sub

    Private Sub gdvMaKH_QueryCloseUp(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles gdvMaKH.QueryCloseUp
        'If _IsLoadData Then
        '    LoadNguoiNhan()
        '    Select Case cbMucDich.EditValue
        '        Case 210, 228, 205
        '            LoadDHNK()
        '        Case 200, 224, 244, 235, 205, 230
        '            LoadCGXK()
        '    End Select
        '    If gdvMaKH.EditValue Is Nothing Then
        '        cbTKDoiUng.EditValue = Nothing
        '        tbNoiMoTKDU.EditValue = Nothing
        '    Else
        '        On Error Resume Next
        '        Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
        '        Dim dr As DataRowView = edit.GetSelectedDataRow

        '        If UNC Then
        '            tbTKNhan.EditValue = dr("ttcTaiKhoan")
        '            tbNoiMoTkNhan.EditValue = dr("ttcNoiMo")
        '        Else
        '            If cbMucDich.EditValue = 211 Then
        '                cbTKDoiUng.EditValue = dr("ttcTaiKhoan")
        '            End If
        '        End If

        '    End If
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



    Private Sub TinhTongTien()
        gdvData.CloseEditor()
        gdvData.UpdateCurrentRow()
        Dim tongTien As Double = 0
        For i As Integer = 0 To gdvData.DataRowCount - 1
            tongTien += gdvData.GetRowCellValue(i, "SoTien")
        Next
        tbSoTien.Value = tongTien
    End Sub


    Private Sub cbMucDich_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbMucDich.EditValueChanged
        Select Case cbMucDich.EditValue
            Case 210, 228
                chkChiPhiNhap.Enabled = False
                LoadDHNK()
                gdvPhieuTC0.Enabled = True
                gdvPhieuTC1.Enabled = True
                gdv.Enabled = True
                tbSoTienTC0.Enabled = True
                tbSoTienTC1.Enabled = True
                btnAddPhieuDH.Enabled = True
                btnAddPhieuNK.Enabled = True
                chkTamUng.Enabled = False
                'cbTienTe.Enabled = False
                tbSoTien.Enabled = False
                lbTC0.Text = "Phiếu ĐH"
                colPhieuDH.Caption = "Phiếu ĐH"
                lbTC1.Text = "Phiếu NK"
                colPhieuNK.Caption = "Phiếu NK"
                If _TrangThai.isAddNew Then
                    If gdvPhieuTC0.Text.ToString <> "" And gdvPhieuTC0.Text <> "000000000" Then
                        tbDienGiai.EditValue = cbMucDich.Text & " DH" & gdvPhieuTC0.Text
                    ElseIf gdvPhieuTC1.Text.ToString <> "" And gdvPhieuTC1.Text <> "000000000" Then
                        tbDienGiai.EditValue = cbMucDich.Text & " NK" & gdvPhieuTC1.Text
                    Else
                        tbDienGiai.EditValue = ""
                    End If
                End If

            Case 200, 224, 244, 235, 230
                chkChiPhiNhap.Enabled = False
                LoadCGXK()
                lbTC0.Text = "Phiếu CG"
                lbTC1.Text = "Phiếu XK"
                colPhieuDH.Caption = "Phiếu CG"
                colPhieuNK.Caption = "Phiếu XK"
                gdvPhieuTC0.Enabled = True
                gdvPhieuTC1.Enabled = True
                tbSoTienTC0.Enabled = True
                tbSoTienTC1.Enabled = True
                btnAddPhieuDH.Enabled = True
                btnAddPhieuNK.Enabled = True
                gdv.Enabled = True
                chkTamUng.Enabled = False
                'cbTienTe.Enabled = False
                tbSoTien.Enabled = False
                If _TrangThai.isAddNew Then
                    If gdvPhieuTC0.Text.ToString <> "" And gdvPhieuTC0.Text <> "000000000" Then
                        tbDienGiai.EditValue = cbMucDich.Text & " CG" & gdvPhieuTC0.Text
                    ElseIf gdvPhieuTC1.Text.ToString <> "" And gdvPhieuTC1.Text <> "000000000" Then
                        tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
                    Else
                        tbDienGiai.EditValue = ""
                    End If
                End If
            Case 205
                chkChiPhiNhap.Enabled = True
                If chkChiPhiNhap.Checked Then
                    LoadDHNK()
                    gdvPhieuTC0.Enabled = True
                    gdvPhieuTC1.Enabled = True
                    gdv.Enabled = True
                    tbSoTienTC0.Enabled = True
                    tbSoTienTC1.Enabled = True
                    btnAddPhieuDH.Enabled = True
                    btnAddPhieuNK.Enabled = True
                    chkTamUng.Enabled = False
                    ' cbTienTe.Enabled = False
                    tbSoTien.Enabled = False
                    lbTC0.Text = "Phiếu ĐH"
                    lbTC1.Text = "Phiếu NK"
                    colPhieuDH.Caption = "Phiếu DH"
                    colPhieuNK.Caption = "Phiếu NK"
                    If _TrangThai.isAddNew Then
                        If gdvPhieuTC0.Text.ToString <> "" And gdvPhieuTC0.Text <> "000000000" Then
                            tbDienGiai.EditValue = cbMucDich.Text & " DH" & gdvPhieuTC0.Text
                        ElseIf gdvPhieuTC1.Text.ToString <> "" And gdvPhieuTC1.Text <> "000000000" Then
                            tbDienGiai.EditValue = cbMucDich.Text & " NK" & gdvPhieuTC1.Text
                        Else
                            tbDienGiai.EditValue = ""
                        End If
                    End If
                Else
                    LoadCGXK()
                    lbTC0.Text = "Phiếu CG"
                    lbTC1.Text = "Phiếu XK"
                    colPhieuDH.Caption = "Phiếu CG"
                    colPhieuNK.Caption = "Phiếu XK"
                    gdvPhieuTC0.Enabled = True
                    gdvPhieuTC1.Enabled = True
                    tbSoTienTC0.Enabled = True
                    tbSoTienTC1.Enabled = True
                    btnAddPhieuDH.Enabled = True
                    btnAddPhieuNK.Enabled = True
                    gdv.Enabled = True
                    chkTamUng.Enabled = False
                    ' cbTienTe.Enabled = False
                    tbSoTien.Enabled = False
                    If _TrangThai.isAddNew Then
                        If gdvPhieuTC0.Text.ToString <> "" And gdvPhieuTC0.Text <> "000000000" Then
                            tbDienGiai.EditValue = cbMucDich.Text & " CG" & gdvPhieuTC0.Text
                        ElseIf gdvPhieuTC1.Text.ToString <> "" And gdvPhieuTC1.Text <> "000000000" Then
                            tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
                        Else
                            tbDienGiai.EditValue = ""
                        End If
                    End If
                End If

            Case Else
                chkChiPhiNhap.Enabled = False
                gdvPhieuTC0.EditValue = Nothing
                gdvPhieuTC1.EditValue = Nothing
                gdvPhieuTC0.Enabled = False
                gdvPhieuTC1.Enabled = False
                gdv.Enabled = False
                tbSoTienTC0.Enabled = False
                tbSoTienTC1.Enabled = False
                btnAddPhieuDH.Enabled = False
                btnAddPhieuNK.Enabled = False
                chkTamUng.Enabled = True
                'cbTienTe.Enabled = True
                tbSoTien.Enabled = True
                tbSoTienTC0.Value = 0
                tbSoTienTC1.Value = 0
                If _TrangThai.isAddNew Then
                    tbSoTien.Value = 0
                    tbDienGiai.EditValue = ""
                End If

        End Select

        If Not UNC Then

            If cbMucDich.EditValue = 211 Then
                cbTKDoiUng.Enabled = True
                tbNoiMoTKDU.Enabled = True

            Else
                cbTKDoiUng.Enabled = False
                tbNoiMoTKDU.Enabled = False
            End If
            If Not gdvMaKH.EditValue Is Nothing And _TrangThai.isAddNew And cbMucDich.EditValue = 211 Then
                Dim dr As DataRowView = gdvMaKH.GetSelectedDataRow
                cbTKDoiUng.EditValue = dr("ttcTaiKhoan")

            Else
                If _TrangThai.isAddNew Then
                    cbTKDoiUng.EditValue = Nothing
                    tbNoiMoTKDU.EditValue = Nothing
                End If
            End If
        End If
        'MsgBox(cbMucDich.EditValue)
    End Sub

    Private Sub gdvPhieuTC1_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvPhieuTC1.EditValueChanged
        If gdvPhieuTC1.EditValue <> "000000000" And Not gdvPhieuTC1.EditValue Is Nothing Then
            gdvPhieuTC0.EditValue = Nothing
            tbSoTienTC0.Value = 0
            Select Case cbMucDich.EditValue
                Case 210, 228
                    '   tbDienGiai.EditValue = cbMucDich.Text & " NK" & gdvPhieuTC1.Text
                Case 200, 224, 244, 235, 230
                    '    tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
            End Select
            Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
            Dim dr As DataRowView = edit.GetSelectedDataRow
            If cbMucDich.EditValue = 200 Then
                tbSoTienTC1.Value = dr("TienChietKhau")
                If _TrangThai.isAddNew Then tbSoTien.EditValue = dr("TienChietKhau")
            Else
                'tbSoTienTC1.Value = dr("TongTien")
                tbSoTienTC1.Value = dr("ConLai")
                'If _TrangThai.isAddNew Then tbSoTien.EditValue = dr("TongTien")
            End If

            gdvMaKH.EditValue = dr("IDKhachHang")
            cbTienTe.EditValue = dr("IDTienTe")
        Else
            If gdvPhieuTC0.EditValue = "000000000" Or gdvPhieuTC0.EditValue Is Nothing Then
                '   tbDienGiai.EditValue = ""
            End If
            tbSoTienTC1.Value = 0
        End If
    End Sub

    Private Sub btIn_Click(sender As System.Object, e As System.EventArgs) Handles btIn.Click
        If tbSoPhieu.EditValue Is Nothing Then
            ShowCanhBao("Chưa tồn tại số phiếu!")
            Exit Sub
        End If
        If UNC Then
            Dim sql As String = ""
            sql &= " SELECT NgayThang,SoPhieuT as SoPhieu,TaiKhoanDi,NganHangDi,(SELECT Ten FROm KHACHHANG WHERE ID=74)DonViTra, "
            sql &= "TaiKhoanDen,NganHangDen,(SELECT Ten FROm KHACHHANG WHERE ID=UNC.IDKh)DonViNhan,DienGiaiNH AS DienGiai,SUM(SoTien) as SoTien,N'' AS BangChu "
            sql &= "FROM UNC "
            sql &= "WHERE SoPhieuT=@SoPhieu "
            sql &= "GROUP BY NgayThang,SoPhieuT,TaiKhoanDi,NganHangDi,TaiKhoanDen,NganHangDen,IDKh,DienGiaiNH "
            AddParameterWhere("@SoPhieu", tbSoPhieu.EditValue)
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
            AddParameterWhere("@SoPhieu", tbSoPhieu.EditValue)
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

    Private Sub cbTKDoiUng_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbTKDoiUng.ButtonClick
        If e.Button.Index = 1 Then
            cbTKDoiUng.EditValue = Nothing
            tbNoiMoTKDU.EditValue = Nothing
        End If
    End Sub

    Private Sub gdvData_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvData.CellValueChanged
        If e.Column.FieldName = "SoTien" Then
            TinhTongTien()
        End If
    End Sub

    Private Sub gdvData_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvData.RowCellClick
        If e.Button = Windows.Forms.MouseButtons.Right And gdvData.FocusedRowHandle >= 0 Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
            If gdvData.GetFocusedRowCellValue("SoPhieu") Is DBNull.Value Then
                mnuIn.Enabled = False
                '  mnuXoaDong.Enabled = True
            Else
                mnuIn.Enabled = True
                '  mnuXoaDong.Enabled = False
            End If
        End If
    End Sub

    Private Sub mnuXoaDong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaDong.ItemClick

        Exit Sub
        If gdvData.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xóa nội dung vừa chọn ?") Then
            Dim _TenBang As String = ""
            If UNC Then
                _TenBang = "UNC"
            Else
                _TenBang = "CHI"
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
            tbDienGiai.EditValue = cbMucDich.Text
            For k As Integer = 0 To gdvData.DataRowCount - 1
                If gdvData.GetRowCellValue(k, "PhieuDH").ToString <> "000000000" Then
                    tbDienGiai.EditValue &= " DH" & gdvData.GetRowCellValue(k, "PhieuDH").ToString
                Else
                    tbDienGiai.EditValue &= " NK" & gdvData.GetRowCellValue(k, "PhieuNK").ToString
                End If
                If k < gdvData.DataRowCount - 1 Then
                    tbDienGiai.EditValue &= ","
                End If
            Next
        End If
    End Sub




    Private Sub btnAddPhieuDH_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPhieuDH.Click
        If _SoLuong1 > 0 Then
            For i As Integer = 0 To gdvSoPhieuTC0.RowCount - 1
                If gdvSoPhieuTC0.GetRowCellValue(i, "Chon") Then
                    Dim _e As Boolean = False
                    For j As Integer = 0 To gdvData.DataRowCount - 1
                        If gdvData.GetRowCellValue(j, "PhieuDH").ToString = gdvSoPhieuTC0.GetRowCellValue(i, "SoPhieu") Then
                            _e = True
                            Exit For
                        End If
                    Next
                    If _e = True Then Continue For
                    gdvData.AddNewRow()
                    gdvData.SetFocusedRowCellValue("PhieuDH", gdvSoPhieuTC0.GetRowCellValue(i, "SoPhieu"))
                    gdvData.SetFocusedRowCellValue("PhieuNK", "000000000")
                    If cbMucDich.EditValue = 200 Then
                        gdvData.SetFocusedRowCellValue("SoTien", gdvSoPhieuTC0.GetRowCellValue(i, "TienChietKhau"))
                    Else
                        gdvData.SetFocusedRowCellValue("SoTien", gdvSoPhieuTC0.GetRowCellValue(i, "TongTien"))
                    End If

                    gdvData.SetFocusedRowCellValue("Tamung", False)
                    Select Case cbMucDich.EditValue
                        Case 210, 228, 205
                            gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " ĐH" & gdvSoPhieuTC0.GetRowCellValue(i, "SoPhieu"))
                            ' tbDienGiai.EditValue = cbMucDich.Text & " ĐH" & gdvPhieuTC0.Text
                        Case 200, 224, 244, 235, 205, 230
                            gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " CG" & gdvSoPhieuTC0.GetRowCellValue(i, "SoPhieu"))
                            '  tbDienGiai.EditValue = cbMucDich.Text & " CG" & gdvPhieuTC0.Text
                    End Select

                    TinhTongTien()
                    tbDienGiai.EditValue = cbMucDich.Text
                    For k As Integer = 0 To gdvData.DataRowCount - 1
                        If gdvData.GetRowCellValue(k, "PhieuDH").ToString <> "000000000" Then
                            tbDienGiai.EditValue &= " DH" & gdvData.GetRowCellValue(k, "PhieuDH").ToString
                        Else
                            tbDienGiai.EditValue &= " NK" & gdvData.GetRowCellValue(k, "PhieuNK").ToString
                        End If
                        If k < gdvData.DataRowCount - 1 Then
                            tbDienGiai.EditValue &= ","
                        End If
                    Next
                    'End If
                End If
            Next
        Else
            If gdvPhieuTC0.EditValue Is Nothing Then Exit Sub
            For i As Integer = 0 To gdvData.DataRowCount - 1
                If gdvData.GetRowCellValue(i, "PhieuDH").ToString = gdvPhieuTC0.EditValue.ToString Then Exit Sub
            Next
            gdvData.AddNewRow()
            gdvData.SetFocusedRowCellValue("PhieuDH", gdvPhieuTC0.EditValue)
            gdvData.SetFocusedRowCellValue("PhieuNK", "000000000")
            gdvData.SetFocusedRowCellValue("SoTien", tbSoTienTC0.EditValue)
            gdvData.SetFocusedRowCellValue("Tamung", False)
            Select Case cbMucDich.EditValue
                Case 210, 228, 205
                    gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " ĐH" & gdvPhieuTC0.Text)
                    ' tbDienGiai.EditValue = cbMucDich.Text & " ĐH" & gdvPhieuTC0.Text
                Case 200, 224, 244, 235, 205, 230
                    gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " CG" & gdvPhieuTC0.Text)
                    '  tbDienGiai.EditValue = cbMucDich.Text & " CG" & gdvPhieuTC0.Text
            End Select

            TinhTongTien()
            tbDienGiai.EditValue = cbMucDich.Text
            For i As Integer = 0 To gdvData.DataRowCount - 1
                If gdvData.GetRowCellValue(i, "PhieuDH").ToString <> "000000000" Then
                    tbDienGiai.EditValue &= " DH" & gdvData.GetRowCellValue(i, "PhieuDH").ToString
                Else
                    tbDienGiai.EditValue &= " NK" & gdvData.GetRowCellValue(i, "PhieuNK").ToString
                End If
                If i < gdvData.DataRowCount - 1 Then
                    tbDienGiai.EditValue &= ","
                End If
            Next
        End If


    End Sub

    Private Sub btnAddPhieuNK_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPhieuNK.Click
        If _SoLuong2 > 0 Then
            For i As Integer = 0 To gdvSoPhieuTC1.RowCount - 1
                If gdvSoPhieuTC1.GetRowCellValue(i, "Chon") Then
                    Dim _e As Boolean = False
                    For j As Integer = 0 To gdvData.DataRowCount - 1
                        If gdvData.GetRowCellValue(j, "PhieuNK").ToString = gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu") Then
                            _e = True
                            Exit For
                        End If

                    Next
                    If _e Then Continue For
                    gdvData.AddNewRow()
                    gdvData.SetFocusedRowCellValue("PhieuDH", "000000000")
                    gdvData.SetFocusedRowCellValue("PhieuNK", gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                    Select Case cbMucDich.EditValue
                        Case 200
                            gdvData.SetFocusedRowCellValue("SoTien", gdvSoPhieuTC1.GetRowCellValue(i, "TienChietKhau"))
                        Case Else
                            gdvData.SetFocusedRowCellValue("SoTien", gdvSoPhieuTC1.GetRowCellValue(i, "TongTien"))
                    End Select

                    gdvData.SetFocusedRowCellValue("Tamung", False)
                    Select Case cbMucDich.EditValue
                        Case 210, 228
                            gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " NK" & gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                            ' tbDienGiai.EditValue = cbMucDich.Text & " NK" & gdvPhieuTC1.Text
                        Case 200, 224, 244, 235, 230

                            gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " XK" & gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                            ' tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
                    End Select

                    TinhTongTien()

                    tbDienGiai.EditValue = cbMucDich.Text
                    For k As Integer = 0 To gdvData.DataRowCount - 1
                        If gdvData.GetRowCellValue(k, "PhieuDH").ToString <> "000000000" Then
                            Select Case cbMucDich.EditValue
                                Case 210, 228
                                    tbDienGiai.EditValue &= " DH" & gdvData.GetRowCellValue(k, "PhieuDH").ToString
                                    '  gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " NK" & gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                                    ' tbDienGiai.EditValue = cbMucDich.Text & " NK" & gdvPhieuTC1.Text
                                Case 200, 224, 244, 235, 230
                                    tbDienGiai.EditValue &= " CG" & gdvData.GetRowCellValue(k, "PhieuDH").ToString
                                    'gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " XK" & gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                                    ' tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
                            End Select

                        Else
                            Select Case cbMucDich.EditValue
                                Case 210, 228
                                    tbDienGiai.EditValue &= " NK" & gdvData.GetRowCellValue(k, "PhieuNK").ToString
                                    ' tbDienGiai.EditValue &= " DH" & gdvData.GetRowCellValue(k, "PhieuDH").ToString
                                    '  gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " NK" & gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                                    ' tbDienGiai.EditValue = cbMucDich.Text & " NK" & gdvPhieuTC1.Text
                                Case 200, 224, 244, 235, 230
                                    tbDienGiai.EditValue &= " XK" & gdvData.GetRowCellValue(k, "PhieuNK").ToString
                                    'gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " XK" & gdvSoPhieuTC1.GetRowCellValue(i, "SoPhieu"))
                                    ' tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
                            End Select

                        End If
                        If k < gdvData.DataRowCount - 1 Then
                            tbDienGiai.EditValue &= ","
                        End If
                    Next
                End If
            Next
        Else
            If gdvPhieuTC1.EditValue Is Nothing Then Exit Sub
            For i As Integer = 0 To gdvData.DataRowCount - 1
                If gdvData.GetRowCellValue(i, "PhieuNK").ToString = gdvPhieuTC1.EditValue.ToString Then Exit Sub
            Next
            gdvData.AddNewRow()
            gdvData.SetFocusedRowCellValue("PhieuDH", "000000000")
            gdvData.SetFocusedRowCellValue("PhieuNK", gdvPhieuTC1.EditValue)
            gdvData.SetFocusedRowCellValue("SoTien", tbSoTienTC1.EditValue)
            gdvData.SetFocusedRowCellValue("Tamung", False)
            Select Case cbMucDich.EditValue
                Case 210, 228, 205
                    gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " NK" & gdvPhieuTC1.Text)
                    ' tbDienGiai.EditValue = cbMucDich.Text & " NK" & gdvPhieuTC1.Text
                Case 200, 224, 244, 235, 205, 230
                    gdvData.SetFocusedRowCellValue("NoiDung", cbMucDich.Text & " XK" & gdvPhieuTC1.Text)
                    ' tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
            End Select

            TinhTongTien()

            tbDienGiai.EditValue = cbMucDich.Text
            For i As Integer = 0 To gdvData.DataRowCount - 1
                If gdvData.GetRowCellValue(i, "PhieuDH").ToString <> "000000000" Then
                    Select Case cbMucDich.EditValue
                        Case 210, 228, 205
                            tbDienGiai.EditValue &= " NK" & gdvData.GetRowCellValue(i, "PhieuDH").ToString

                        Case 200, 224, 244, 235, 205, 230
                            tbDienGiai.EditValue &= " XK" & gdvData.GetRowCellValue(i, "PhieuDH").ToString

                    End Select

                Else
                    Select Case cbMucDich.EditValue
                        Case 210, 228, 205
                            tbDienGiai.EditValue &= " NK" & gdvData.GetRowCellValue(i, "PhieuNK").ToString

                        Case 200, 224, 244, 235, 205, 230
                            tbDienGiai.EditValue &= " XK" & gdvData.GetRowCellValue(i, "PhieuNK").ToString

                    End Select
                    'tbDienGiai.EditValue &= " NK" & gdvData.GetRowCellValue(i, "PhieuNK").ToString
                End If
                If i < gdvData.DataRowCount - 1 Then
                    tbDienGiai.EditValue &= ","
                End If
            Next
        End If

    End Sub

    Private Sub mnuIn_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuIn.ItemClick
        If gdvData.GetFocusedRowCellValue("SoPhieu") Is DBNull.Value Then
            ShowCanhBao("Chưa tồn tại số phiếu !")
            Exit Sub
        End If


        If UNC Then
            Dim sql As String = ""
            sql &= " SELECT NgayThang,SoPhieu,TaiKhoanDi,NganHangDi,(SELECT Ten FROm KHACHHANG WHERE ID=74)DonViTra, "
            sql &= "TaiKhoanDen,NganHangDen,(SELECT Ten FROm KHACHHANG WHERE ID=UNC.IDKh)DonViNhan,DienGiaiNH AS DienGiai,SoTien,N'' AS BangChu "
            sql &= "FROM UNC "
            sql &= "WHERE SoPhieu=@SoPhieu "
            AddParameterWhere("@SoPhieu", gdvData.GetFocusedRowCellValue("SoPhieu"))
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tb.Rows(0)("BangChu") = Utils.StringHelper.VIE2String(tb.Rows(0)("SoTien"), False, "đồng", "lẻ", "phẩy", 2)
                Dim f As New frmIn("In ủy nhiệm chi")
                Dim rpt As New rptUNC
                rpt.pLogo.Image = My.Resources.Logo3
                rpt.pAnhLien2.Image = My.Resources.Logo3
                rpt.DataSource = tb
                rpt.CreateDocument()
                f.printControl.PrintingSystem = rpt.PrintingSystem
                f.ShowDialog()
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        Else
            Dim sql As String = ""
            sql = "SELECT SoPhieu,NgayThangCT,NguoiNhan,DienGiai,SoTien,ChungTuGoc, "
            sql &= "(SELECT Ten FROM KHACHHANG WHERE ID=CHI.IDKh)DiaChi, "
            sql &= "(SELECT Ten FROM NHANSU WHERE ID=CHI.IDUser)NguoiLap FROM CHI WHERE SoPhieu=@SoPhieu "
            AddParameterWhere("@SoPhieu", gdvData.GetFocusedRowCellValue("SoPhieu"))
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

    Private Sub frmCNChi_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If isCapNhatOK Then Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public _SoLuong1 As Integer = 0
    Public _SoTien1 As Integer = 0

    Private Sub gdvSoPhieuTC0_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvSoPhieuTC0.KeyDown
        On Error Resume Next
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
        On Error Resume Next
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

    Private Sub gdvPhieuTC0_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles gdvPhieuTC0.EditValueChanged
        If gdvPhieuTC0.EditValue <> "000000000" And Not gdvPhieuTC0.EditValue Is Nothing Then
            gdvPhieuTC1.EditValue = Nothing
            tbSoTienTC1.Value = 0
            Select Case cbMucDich.EditValue
                Case 210, 228
                    '    tbDienGiai.EditValue = cbMucDich.Text & " ĐH" & gdvPhieuTC0.Text
                Case 200, 224, 244, 235, 230
                    '      tbDienGiai.EditValue = cbMucDich.Text & " CG" & gdvPhieuTC0.Text
            End Select

            Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
            Dim dr As DataRowView = edit.GetSelectedDataRow
            If cbMucDich.EditValue = 200 Then
                tbSoTienTC0.Value = dr("TienChietKhau")
                If _TrangThai.isAddNew Then tbSoTien.EditValue = dr("TienChietKhau")
            Else
                'tbSoTienTC0.Value = dr("TongTien")
                tbSoTienTC0.Value = dr("ConLai")
                ' If _TrangThai.isAddNew Then tbSoTien.EditValue = dr("TongTien")
            End If
            gdvMaKH.EditValue = dr("IDKhachHang")
            cbTienTe.EditValue = dr("IDTienTe")
        Else
            If gdvPhieuTC1.EditValue = "000000000" Or gdvPhieuTC1.EditValue Is Nothing Then
                '      tbDienGiai.EditValue = ""
            End If
            tbSoTienTC0.Value = 0S
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
            If UNC Then
                txtNoiDungPhieuThu.EditValue = "Trả tiền hàng " & getTenCongTy()
            Else
                txtNoiDungPhieuThu.EditValue = "Trả tiền hàng " & getTenCongTy()
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

    Private Sub btnHoaDon_Click(sender As System.Object, e As System.EventArgs) Handles btnHoaDon.Click

        TrangThai.isAddNew = True
        Dim f As New frmUpdateHdDauVao
        f.LoaiCT2 = ChungTu.LoaiCT2.MuaDichVu

        f.Text = "Nhập hóa đơn mới (" & NguoiDung & ")"
        f.txtNguoiLienHe.Focus()


        'Bin du lieu chung len hoa don
        f.cmbDoiTuong.EditValue = gdvMaKH.EditValue
        f.txtDienGiaiChung.EditValue = "Trả tiền nhà cung cấp"

        Dim ref As String = ChungTu.getRef
        Dim sotien As Double = tbSoTien.EditValue
        Dim tienhang As Double = Math.Round(tbSoTien.EditValue / 1.1F, 0, MidpointRounding.AwayFromZero)
        Dim tienthue As Double = sotien - tienhang

        'Thêm dòng cho hàng tiền
        With f.gdvHangTienCT
            .AddNewRow()
            .SetFocusedRowCellValue("ref", ref)
            .SetFocusedRowCellValue("DienGiai", f.txtDienGiaiChung.EditValue)
            .SetFocusedRowCellValue("ThanhTien", tienhang)
            If UNC Then
                .SetFocusedRowCellValue("TaiKhoanNo", "112")
            Else
                .SetFocusedRowCellValue("TaiKhoanNo", "111")
            End If
            .SetFocusedRowCellValue("TaiKhoanCo", "331")
            .SetFocusedRowCellValue("GhiChuKhac", tbSoPhieu.EditValue)
        End With

        'Thêm dòng cho thuế
        With f.gdvThueCT
            .AddNewRow()
            .SetFocusedRowCellValue("ref", ref)
            .SetFocusedRowCellValue("DienGiai", f.txtDienGiaiChung.EditValue)
            .SetFocusedRowCellValue("ThanhTien", tienthue)
            If UNC Then
                .SetFocusedRowCellValue("TaiKhoanNo", "112")
            Else
                .SetFocusedRowCellValue("TaiKhoanNo", "111")
            End If
            .SetFocusedRowCellValue("TaiKhoanCo", "331")
            .SetFocusedRowCellValue("GhiChuKhac", tbSoPhieu.EditValue)
        End With

        f.ShowDialog(Me)

    End Sub

    Private Sub txtSoPhieuCT_Click(sender As System.Object, e As System.EventArgs) Handles txtSoPhieuCT.Click
        TrangThai.isUpdate = True

        Dim f As New frmUpdateThuChiThue

        If UNC Then
            f.LoaiCT = ChungTu.LoaiChungTu.UyNhiemChi
        Else
            f.LoaiCT = ChungTu.LoaiChungTu.PhieuChiTienMat
        End If

        f.soPhieuT = txtSoPhieuCT.Text

        f.ShowDialog(Me)
    End Sub

    Private Sub chkChiPhiNhap_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkChiPhiNhap.CheckedChanged
        If chkChiPhiNhap.Checked Then
            LoadDHNK()
            gdvPhieuTC0.Enabled = True
            gdvPhieuTC1.Enabled = True
            gdv.Enabled = True
            tbSoTienTC0.Enabled = True
            tbSoTienTC1.Enabled = True
            btnAddPhieuDH.Enabled = True
            btnAddPhieuNK.Enabled = True
            chkTamUng.Enabled = False
            ' cbTienTe.Enabled = False
            tbSoTien.Enabled = False
            lbTC0.Text = "Phiếu ĐH"
            lbTC1.Text = "Phiếu NK"
            colPhieuDH.Caption = "Phiếu DH"
            colPhieuNK.Caption = "Phiếu NK"
            If _TrangThai.isAddNew Then
                If gdvPhieuTC0.Text.ToString <> "" And gdvPhieuTC0.Text <> "000000000" Then
                    tbDienGiai.EditValue = cbMucDich.Text & " DH" & gdvPhieuTC0.Text
                ElseIf gdvPhieuTC1.Text.ToString <> "" And gdvPhieuTC1.Text <> "000000000" Then
                    tbDienGiai.EditValue = cbMucDich.Text & " NK" & gdvPhieuTC1.Text
                Else
                    tbDienGiai.EditValue = ""
                End If
            End If
        Else
            LoadCGXK()
            lbTC0.Text = "Phiếu CG"
            lbTC1.Text = "Phiếu XK"
            colPhieuDH.Caption = "Phiếu CG"
            colPhieuNK.Caption = "Phiếu XK"
            gdvPhieuTC0.Enabled = True
            gdvPhieuTC1.Enabled = True
            tbSoTienTC0.Enabled = True
            tbSoTienTC1.Enabled = True
            btnAddPhieuDH.Enabled = True
            btnAddPhieuNK.Enabled = True
            gdv.Enabled = True
            chkTamUng.Enabled = False
            ' cbTienTe.Enabled = False
            tbSoTien.Enabled = False
            If _TrangThai.isAddNew Then
                If gdvPhieuTC0.Text.ToString <> "" And gdvPhieuTC0.Text <> "000000000" Then
                    tbDienGiai.EditValue = cbMucDich.Text & " CG" & gdvPhieuTC0.Text
                ElseIf gdvPhieuTC1.Text.ToString <> "" And gdvPhieuTC1.Text <> "000000000" Then
                    tbDienGiai.EditValue = cbMucDich.Text & " XK" & gdvPhieuTC1.Text
                Else
                    tbDienGiai.EditValue = ""
                End If
            End If
        End If
    End Sub

    'Public Sub PhanBoChiPhiNhap(ByVal _SoPhieu As String)
    '    Dim sql As String = ""
    '    sql &= " DECLARE @TongChiPhi float"
    '    sql &= " DECLARE @TongTienHang float"
    '    sql &= " DECLARE @SoPhieuDH nvarchar(15)"
    '    sql &= " SET @SoPhieuDH = (SELECT SoPhieuDH FROM PHIEUNHAPKHO WHERE SoPhieu=@SoPhieu)"
    '    sql &= " SET @TongChiPhi=((SELECT Sum(SoTien) FROM CHI WHERE MucDich=205 AND ChiPhiNhap=1 AND (PhieuTC0=@SoPhieuDH OR PhieuTC1=@SoPhieu))"
    '    sql &= " 	+ (SELECT Sum(SoTien) FROM UNC WHERE MucDich=205 AND ChiPhiNhap=1 AND (PhieuTC0=@SoPhieuDH OR PhieuTC1=@SoPhieu))"
    '    sql &= " )"
    '    sql &= " SET @TongTienHang = (SELECT SUM(SoLuong*DonGia) FROM NHAPKHO WHERE SoPhieu=@SoPhieu)"
    '    sql &= "         Update(NHAPKHO)"
    '    sql &= " SET ChiPhi=round((DonGia/@TongTienHang)* @TongChiPhi,0)"
    '    sql &= " WHERE SoPhieu=@SoPhieu"

    '    AddParameterWhere("@SoPhieu", _SoPhieu)
    '    If ExecuteSQLNonQuery(sql) Is Nothing Then
    '        ShowBaoLoi(LoiNgoaiLe)
    '    End If
    'End Sub

    Private Sub cbTienTe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTienTe.EditValueChanged
        Try
            AddParameter("@Id", cbTienTe.EditValue)
            Dim sql As String = "select TyGia from tblTienTe where ID = @Id"
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            txtTyGia.EditValue = dt.Rows(0)(0)
        Catch ex As Exception
            txtTyGia.EditValue = DBNull.Value
        End Try
    End Sub
End Class