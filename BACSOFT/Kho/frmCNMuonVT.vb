Imports BACSOFT.Db.SqlHelper

Public Class frmCNMuonVT

    Private Sub frmCNMuonVT_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'LoadDS()
        LoadDSNguoiMuon()
        If TrangThai.isAddNew Then

        Else
            AddParameterWhere("@ID", objID)
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT *,(CASE tblXuatMuon.TrangThai WHEN 0 THEN N'Đang mượn' WHEN 1 THEN N'Đã trả' WHEN 2 THEN N'Thất lạc' END) AS TrangThai2 FROM tblXuatMuon WHERE ID=@ID")

            If Not tb Is Nothing Then
                cbNguoiMuon.EditValue = CType(tb.Rows(0)("IDNguoiMuon"), Int32)
                tbThoiGianMuon.EditValue = tb.Rows(0)("ThoiGianMuon")
                tbThoiGianTra.EditValue = tb.Rows(0)("ThoiGianTra")
                LoadDSVT(, tb.Rows(0)("IDVatTu"))
                gdvVatTu.EditValue = CType(tb.Rows(0)("IDVatTu"), Int32)
                tbSoLuong.EditValue = tb.Rows(0)("SoLuong")
                tbTinhTrangMuon.EditValue = tb.Rows(0)("TinhTrangMuon")
                tbTinhTrangTra.EditValue = tb.Rows(0)("TinhTrangTra")
                cbTrangThai.EditValue = tb.Rows(0)("TrangThai2")
                tbGhiChu.EditValue = tb.Rows(0)("GhiChu")

            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    Public Sub LoadDSNguoiMuon()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        If Not tb Is Nothing Then
            cbNguoiMuon.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSVT(Optional ByVal _TenVT As String = "", Optional ByVal _IDVatTu As Integer = 0)
        Dim sql As String = ""
        'sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        sql &= " SELECT VATTU.ID,Model,TENVATTU.Ten AS TenVT,TENHANGSANXUAT.Ten AS HangSX, "
        sql &= " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=VATTU.ID)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=VATTU.ID)) AS slTon"
        sql &= " FROM VATTU "
        If Not tbSoChaoGia.EditValue Is Nothing Then
            sql &= " INNER JOIN CHAOGIA ON CHAOGIA.IDVatTu=VATTU.ID AND CHAOGIA.SoPhieu=@SP "
            AddParameterWhere("@SP", tbSoChaoGia.EditValue)
        End If

        sql &= " LEFT JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql &= " LEFT JOIN TENHANGSANXUAT ON VATTU.IDHangsanxuat=TENHANGSANXUAT.ID "
        If _IDVatTu <> 0 Then
            sql &= " WHERE VATTU.ID =@IDVT"
            AddParameterWhere("@IDVT", _IDVatTu)
        Else
            sql &= " WHERE VATTU.Model like '%" & _TenVT & "%'"
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            ' cbNguoiMuon.Properties.DataSource = ds.Tables(0)
            gdvVatTu.Properties.DataSource = tb
            gdvVatTu.Properties.View.Columns.Clear()
            With gdvVatTu.Properties.View.Columns
                Dim colID = .AddField("ID")
                colID.VisibleIndex = -1
                Dim colModel = .AddField("Model")
                colModel.Caption = "Model"
                colModel.VisibleIndex = 0
                colModel.Width = 150
                colModel.OptionsColumn.FixedWidth = True
                colModel.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like
                Dim colTen = .AddField("TenVT")
                colTen.Caption = "Tên vật tư"
                colTen.VisibleIndex = 1
                colTen.Width = 150
                colTen.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colHang = .AddField("HangSX")
                colHang.Caption = "Hãng SX"
                colHang.VisibleIndex = 2
                colHang.Width = 120
                colHang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colTon = .AddField("slTon")
                colTon.Caption = "Tồn"
                colTon.VisibleIndex = 3
                colTon.Width = 50
            End With
        End If
    End Sub

    Private Sub btTimVatTu_Click(sender As System.Object, e As System.EventArgs) Handles btTimVatTu.Click
        ShowWaiting("Đang tải vật tư ...")
        LoadDSVT(tbModel.EditValue, 0)
        CloseWaiting()
        gdvVatTu.Focus()
        SendKeys.Send("{F4}")
    End Sub

    Private Sub btLuuLai_Click(sender As System.Object, e As System.EventArgs) Handles btLuuLai.Click
        Dim tg As DateTime = GetServerTime()
        If tbThoiGianMuon.EditValue Is Nothing Then
            tbThoiGianMuon.EditValue = tg
        End If
        If cbNguoiMuon.EditValue Is Nothing Then
            ShowCanhBao("Chưa có người mượn !")
            Exit Sub
        End If
        If gdvVatTu.EditValue Is Nothing Then
            ShowCanhBao("Chưa có vật tư !")
            Exit Sub
        End If

        AddParameter("@ThoiGianMuon", tbThoiGianMuon.EditValue)
        If cbTrangThai.EditValue = "Đã trả" Then
            If tbThoiGianTra.Text = "" Then
                tbThoiGianTra.EditValue = tg
            End If
            AddParameter("@ThoiGianTra", tbThoiGianTra.EditValue)
        Else
            AddParameter("@ThoiGianTra", DBNull.Value)
        End If
        AddParameter("@SoPhieuCG", tbSoChaoGia.EditValue)
        AddParameter("@IDNguoiXuat", TaiKhoan)
        AddParameter("@IDNguoiMuon", cbNguoiMuon.EditValue)
        AddParameter("@IDVatTu", gdvVatTu.EditValue)
        AddParameter("@SoLuong", tbSoLuong.EditValue)
        AddParameter("@TinhTrangMuon", tbTinhTrangMuon.EditValue)
        AddParameter("@TinhTrangTra", tbTinhTrangTra.EditValue)
        AddParameter("@GhiChu", tbGhiChu.EditValue)
        AddParameter("@GhiChuKD", tbGhiChuKD.EditValue)
        Select Case cbTrangThai.EditValue
            Case "Đang mượn"
                AddParameter("@TrangThai", 0)
            Case "Đã trả"
                AddParameter("@TrangThai", 1)
            Case "Thất lạc"
                AddParameter("@TrangThai", 2)
        End Select

        If TrangThai.isAddNew Then
            objID = doInsert("tblXuatMuon")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                TrangThai.isUpdate = True
                ShowAlert("Đã thêm vật tư mượn !")
                'Dim index As Integer = CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuatMuon).gdvCT.FocusedRowHandle
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuatMuon).btTaiDS.PerformClick()

            End If
        Else
            AddParameterWhere("@ID", objID)
            If doUpdate("tblXuatMuon", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thông tin mượn vật tư !")
                Dim index As Integer = CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuatMuon).gdvCT.FocusedRowHandle
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuatMuon).btTaiDS.PerformClick()
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuatMuon).gdvCT.FocusedRowHandle = index
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuatMuon).gdvCT.SelectRow(index)
            End If
        End If

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub btThemMoi_Click(sender As System.Object, e As System.EventArgs) Handles btThemMoi.Click
        tbThoiGianMuon.Enabled = True
        tbTinhTrangMuon.Enabled = True
        tbModel.Enabled = True
        gdvVatTu.Enabled = True
        btTimVatTu.Enabled = True
        TrangThai.isAddNew = True
        gdvVatTu.EditValue = DBNull.Value
        tbThoiGianMuon.EditValue = Nothing
        tbTinhTrangTra.EditValue = Nothing
        tbSoLuong.EditValue = 1
        tbTinhTrangMuon.EditValue = ""
        tbTinhTrangTra.EditValue = ""
        cbTrangThai.EditValue = "Đang mượn"
    End Sub

    Private Sub gdvVatTu_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles gdvVatTu.ButtonClick
        If e.Button.Index = 1 Then
            gdvVatTu.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub cbNguoiMuon_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNguoiMuon.ButtonClick
        If e.Button.Index = 1 Then
            cbNguoiMuon.EditValue = Nothing
        End If
    End Sub

    Private Sub tbSoChaoGia_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbSoChaoGia.ButtonClick
        tbSoChaoGia.EditValue = Nothing
    End Sub
End Class