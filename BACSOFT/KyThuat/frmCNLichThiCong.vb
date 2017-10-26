Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Popup
Imports DevExpress.Utils.Win

Public Class frmCNLichThiCong
    Public NVThamGia As New ArrayList

    Private Sub frmCNLichThiCong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadDSNoiDungThiCong()
        LoadDSSoYC()
        'LoadcbNhanVien()
        If TrangThai.isAddNew Then
            Me.Text = "Thêm lịch thi công công trình"
            tbTuNgay.EditValue = Today.Date
            tbDenNgay.EditValue = Today.Date
            tbBatDauTu.EditValue = "08:00"
            tbBatDauDen.EditValue = "13:00"
            tbKetThucTu.EditValue = "12:00"
            tbKetThucDen.EditValue = "17:00"
            NVThamGia.Clear()
            NVThamGia.AddRange(TaiKhoan.ToString.Split(","))
            NVThamGia.Clear()
            NVThamGia.AddRange(TaiKhoan.ToString.Split(","))
        Else
            Me.Text = "Cập nhật lịch thi công công trình"
            tbDenNgay.Enabled = False
            chkChieu.Enabled = False
            chkDenNgay.Enabled = False
            Dim sql As String = ""
            AddParameterWhere("@ID", objID)
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ngay,BatDau,KetThuc,SoYC,SoCG,IDNoiDung,IDNgThucHien,IDNgThongBao FROM tblBaoCaoLichThiCong WHERE ID=@ID")
            If Not tb Is Nothing Then
                tbTuNgay.EditValue = tb.Rows(0)("Ngay")
                tbDenNgay.EditValue = tb.Rows(0)("Ngay")
                tbBatDauTu.EditValue = tb.Rows(0)("BatDau")
                tbKetThucTu.EditValue = tb.Rows(0)("KetThuc")
                tbBatDauDen.EditValue = tb.Rows(0)("BatDau")
                tbKetThucDen.EditValue = tb.Rows(0)("KetThuc")
                cbSoYeuCau.EditValue = tb.Rows(0)("SoYC")
                cbNoiDungThiCong.EditValue = tb.Rows(0)("IDNoiDung")
                tbSoCG.EditValue = tb.Rows(0)("SoCG")
                NVThamGia.AddRange(tb.Rows(0)("IDNgThucHien").ToString.Split(","))
            End If
            treeNV.Enabled = Enabled = False
        End If

        LoadDSNhanVien()
    End Sub

    'Private Sub LoadcbNhanVien()
    '    Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74")
    '    If Not tb Is Nothing Then
    '        cbThucHien.Properties.DataSource = tb
    '        If tb.Rows.Count > 0 Then
    '            cbThucHien.EditValue = Convert.ToInt32(TaiKhoan)
    '        End If
    '    Else
    '        ShowBaoLoi(LoiNgoaiLe)
    '    End If
    'End Sub

    Private Sub LoadDSNhanVien()
        Dim sql As String = ""
        sql &= " SELECT 'PB'+Convert(nvarchar, ID)ID,Ten FROM DEPATMENT "
        sql &= " SELECT NHANSU.ID,NHANSU.Ten,'PB'+Convert(nvarchar, NHANSU.IDDepatment)IDDepatment"
        sql &= " FROM NHANSU "
        sql &= " LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=NhanSu.IDBoPhan"
        sql &= " WHERE NHANSU.Noictac=74 AND NHANSU.Trangthai=1 "
        sql &= " ORDER BY NHANSU.IDDepatment,NhanSu_BoPhan.MaBP,NHANSU.ChucVu,NhanSu.ID"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        If Not ds Is Nothing Then

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim item As New Utils.ItemObject(ds.Tables(0).Rows(i)("ID"), ds.Tables(0).Rows(i)("Ten"))
                Dim nodeCha As TreeListNode = Nothing
                Dim node As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item.Value, item.Name)}, nodeCha)
                If NVThamGia.Contains(item.Value) Then node.Checked = True
                'If deskTop.BarMenu.ItemLinks.Item(i).ToString = "DevExpress.XtraBars.BarSubItemLink" Then
                For j As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    If ds.Tables(1).Rows(j)("IDDepatment") = ds.Tables(0).Rows(i)("ID") Then
                        Dim item2 As New Utils.ItemObject(ds.Tables(1).Rows(j)("ID"), ds.Tables(1).Rows(j)("Ten"))
                        Dim nodecon As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item2.Value, item2.Name)}, node)
                        If NVThamGia.Contains(item2.Value.ToString) Then nodecon.Checked = True
                    End If
                Next
                'End If
            Next

            'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            '    Dim item As New Utils.ItemObject(ds.Tables(0).Rows(i)("ID"), ds.Tables(0).Rows(i)("Ten"))
            '    Dim nodeCha As TreeListNode = Nothing
            '    Dim node As TreeListNode = treeNVThongBao.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item.Value, item.Name)}, nodeCha)
            '    If NVTh.Contains(item.Value) Then node.Checked = True
            '    'If deskTop.BarMenu.ItemLinks.Item(i).ToString = "DevExpress.XtraBars.BarSubItemLink" Then
            '    For j As Integer = 0 To ds.Tables(1).Rows.Count - 1
            '        If ds.Tables(1).Rows(j)("IDDepatment") = ds.Tables(0).Rows(i)("ID") Then
            '            Dim item2 As New Utils.ItemObject(ds.Tables(1).Rows(j)("ID"), ds.Tables(1).Rows(j)("Ten"))
            '            Dim nodecon As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item2.Value, item2.Name)}, node)
            '            If NVThamGia.Contains(item2.Value.ToString) Then nodecon.Checked = True
            '        End If
            '    Next
            '    'End If
            'Next

            treeNV.ExpandAll()
        End If
    End Sub

    Public Sub LoadDSSoYC()
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT BANGYEUCAU.Sophieu,KHACHHANG.ttcMa,BANGYEUCAU.NoiDung,BANGYEUCAU.IDLoaiYeuCau,tblTuDien.NoiDung as LoaiYeuCau,"
        sql &= " BANGCHAOGIA.SoPhieu AS SoCG,BANGCHAOGIA.TenDuAn "
        sql &= " FROM BANGYEUCAU "
        sql &= " INNER JOIN KHACHHANG ON BANGYEUCAU.IDKhachhang=KHACHHANG.ID "
        sql &= " LEFT JOIN BANGCHAOGIA ON BANGCHAOGIA.MaSoDatHang=BANGYEUCAU.SoPhieu"
        sql &= " LEFT JOIN tblTuDien ON tblTuDien.ID=BANGYEUCAU.IDLoaiYeuCau AND tblTuDien.Loai = 10 "
        sql &= " WHERE IDLoaiYeuCau Is not null "
        '    sql &= " AND  BANGCHAOGIA.TrangThai in (1,2)"
        sql &= " AND ( BANGCHAOGIA.TrangThai in (1,2) or  BANGCHAOGIA.MaSoDatHang is null) "
        sql &= " ORDER BY BANGYEUCAU.SoPhieu DESC"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbSoYeuCau.Properties.View.Columns.Clear()
            cbSoYeuCau.Properties.DataSource = tb
            With cbSoYeuCau.Properties.View.Columns
                Dim colSP = .AddField("Sophieu")
                colSP.Caption = "Số YC"
                colSP.VisibleIndex = 0
                colSP.Width = 60
                colSP.OptionsColumn.FixedWidth = True
                colSP.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like

                Dim colSPCG = .AddField("SoCG")
                colSPCG.Caption = "Số CG"
                colSPCG.VisibleIndex = 1
                colSPCG.Width = 60
                colSPCG.OptionsColumn.FixedWidth = True
                colSPCG.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like

                Dim colTenHang = .AddField("ttcMa")
                colTenHang.Caption = "Mã KH"
                colTenHang.VisibleIndex = 2
                colTenHang.Width = 120
                colTenHang.OptionsColumn.FixedWidth = True
                colTenHang.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like

                Dim colNoiDung = .AddField("NoiDung")
                colNoiDung.Caption = "Nội dung yêu cầu"
                colNoiDung.VisibleIndex = 3
                colNoiDung.Width = 250
                colNoiDung.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                colNoiDung.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                colNoiDung.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                colNoiDung.ColumnEdit = rMemoText

                Dim colTenCG = .AddField("TenDuAn")
                colTenCG.Caption = "Nội dung chào giá"
                colTenCG.VisibleIndex = 4
                colTenCG.Width = 250
                colTenCG.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                colTenCG.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                colTenCG.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                colTenCG.ColumnEdit = rMemoText

                Dim colLoaiYeuCau = .AddField("LoaiYeuCau")
                colLoaiYeuCau.Caption = "Loại yêu cầu"
                colLoaiYeuCau.VisibleIndex = 5
                colLoaiYeuCau.Width = 200
                colLoaiYeuCau.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                colLoaiYeuCau.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                colLoaiYeuCau.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                colLoaiYeuCau.ColumnEdit = rMemoText
                Dim colIDLoai = .AddField("IDLoaiYeuCau")
                colIDLoai.Caption = "Loại yêu cầu"
                colIDLoai.VisibleIndex = -1



            End With

        End If
    End Sub


    Public Sub loadDSNoiDungThiCong()
        Dim sql As String = " SELECT tblTuDien.ID,tblTuDien.NoiDung,tbTmp.NoiDung AS Nhom  "
        If Not IsDBNull(cbSoYeuCau.Tag) And Not cbSoYeuCau.Tag Is Nothing Then
            sql &= " ,(SELECT " & "C" & cbSoYeuCau.Tag & " FROM tblDinhMucDiem WHERE  IDNoiDungCV=tblTuDien.ID ) DiemSo "

        End If
        sql &= " FROM tblTuDien LEFT JOIN tblTuDien as tbTmp ON tblTuDien.IDP=tbTmp.ID and tbTmp.Loai=@loai2  "
        sql &= " WHERE tblTuDien.Loai=@Loai "

        If Not IsDBNull(cbSoYeuCau.Tag) And Not cbSoYeuCau.Tag Is Nothing Then
            '   sql &= " AND tblTuDien.ID IN (SELECT IDNoiDungCV FROM tblDinhMucDiem WHERE " & "C" & cbSoYeuCau.Tag & " >0 ) "
        End If
        sql &= " ORDER BY tbTmp.Ma,tblTuDien.Ma"
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungThiCong)
        AddParameterWhere("@Loai2", LoaiTuDien.NhomNoiDungThiCong)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cbNoiDungThiCong.Properties.DataSource = tb
            cbNoiDungThiCong.Properties.View.Columns.Clear()
            With cbNoiDungThiCong.Properties.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                Dim colNoiDung = .AddField("NoiDung")
                colNoiDung.Caption = "Nội dung"
                colNoiDung.VisibleIndex = 1
                colNoiDung.Width = 120
                colNoiDung.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colNhom = .AddField("Nhom")
                colNhom.Caption = "Nhóm"
                colNhom.VisibleIndex = 2
                colNhom.Width = 250
                colNhom.GroupIndex = 0
                'Dim colDiem = .AddField("DiemSo")
                'colDiem.Caption = "Điểm"
                'colDiem.VisibleIndex = 2
                'colDiem.Width = 50
            End With
         
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub cbSoYeuCau_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbSoYeuCau.KeyPress
        'If Not cbSoYeuCau.IsPopupOpen Then
        '    cbSoYeuCau.ShowPopup()
        'End If
    End Sub

    Private Sub cbNoiDungThiCong_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbNoiDungThiCong.KeyPress
        If Not cbNoiDungThiCong.IsPopupOpen Then
            cbNoiDungThiCong.ShowPopup()
        End If
    End Sub

    Private indexRow As Integer = -1
    Private Sub gdvSoYC_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvSoYC.RowCellClick
        cbSoYeuCau.EditValue = gdvSoYC.GetFocusedRowCellValue("Sophieu")
        cbSoYeuCau.Tag = gdvSoYC.GetFocusedRowCellValue("IDLoaiYeuCau")
        tbSoCG.EditValue = gdvSoYC.GetFocusedRowCellValue("SoCG")
        indexRow = gdvSoYC.FocusedRowHandle
        cbSoYeuCau.ClosePopup()
    End Sub


    Private Sub cbSoYeuCau_Popup(sender As System.Object, e As System.EventArgs) Handles cbSoYeuCau.Popup
        gdvSoYC.FocusedRowHandle = indexRow
    End Sub


    Private Sub gdvNoiDungThiCong_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvNoiDungThiCong.RowCellClick
        cbNoiDungThiCong.EditValue = gdvNoiDungThiCong.GetFocusedRowCellValue("ID")
        cbNoiDungThiCong.ClosePopup()
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Public Function Luu() As Boolean
        Dim strNVThamGia As String = ","
        Try
            If (Convert.ToDateTime(tbKetThucTu.EditValue) < Convert.ToDateTime(tbBatDauTu.EditValue) And tbKetThucTu.Enabled = True And tbBatDauTu.Enabled = True) Or (Convert.ToDateTime(tbKetThucDen.EditValue) < Convert.ToDateTime(tbBatDauDen.EditValue) And tbKetThucDen.Enabled = True And tbKetThucTu.Enabled = True) Then
                Throw New Exception("Thời gian kết thúc không được nhỏ hơn thời gian bắt đầu !")
            End If

            If cbSoYeuCau.EditValue.ToString = "" Then Throw New Exception("Chưa có số yêu cầu đến !")
            If cbNoiDungThiCong.EditValue.ToString = "" Then Throw New Exception("Chưa có nội dung công việc !")
            'If cbThucHien.EditValue.ToString = "" Then Throw New Exception("Chưa có nội dung công việc !")
            Dim count As Integer = 0
            For i As Integer = 0 To treeNV.Nodes.Count - 1
                Dim nod1 As TreeListNode = treeNV.Nodes(i)
                ' If nod1.Checked Then strNVThamGia &= CType(nod1(0), Utils.ItemObject).Value & ","

                For j As Integer = 0 To nod1.Nodes.Count - 1
                    Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                    If nod2.Checked Then count += 1
                Next
            Next
            If count = 0 Then Throw New Exception("Chưa có người thực hiện công việc !")
            Dim tg As DateTime = GetServerTime()

            If TrangThai.isAddNew Then
                For i As Integer = 0 To treeNV.Nodes.Count - 1
                    Dim nod1 As TreeListNode = treeNV.Nodes(i)
                    ' If nod1.Checked Then strNVThamGia &= CType(nod1(0), Utils.ItemObject).Value & ","
                    For j As Integer = 0 To nod1.Nodes.Count - 1
                        Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                        If nod2.Checked Then
                            If chkSang.Checked Then
                                AddParameter("@Ngay", tbTuNgay.EditValue)
                                AddParameter("@BatDau", tbBatDauTu.Text)
                                AddParameter("@KetThuc", tbKetThucTu.Text)
                                AddParameter("@IDNoiDung", cbNoiDungThiCong.EditValue)
                                AddParameter("@IDNgThucHien", CType(nod2(0), Utils.ItemObject).Value)
                                AddParameter("@SoYC", cbSoYeuCau.EditValue)

                                If tbSoCG.EditValue.ToString = "" Then
                                    AddParameter("@SoCG", DBNull.Value)
                                Else
                                    AddParameter("@SoCG", tbSoCG.EditValue)
                                End If
                                AddParameter("@IDNgNhap", TaiKhoan)
                                AddParameter("@NgayNhap", tg)
                                If doInsert("tblBaoCaoLichThiCong") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                            End If
                            
                            If chkChieu.Checked Then
                                AddParameter("@Ngay", tbTuNgay.EditValue)
                                AddParameter("@BatDau", tbBatDauDen.Text)
                                AddParameter("@KetThuc", tbKetThucDen.Text)
                                AddParameter("@IDNoiDung", cbNoiDungThiCong.EditValue)
                                AddParameter("@IDNgThucHien", CType(nod2(0), Utils.ItemObject).Value)
                                AddParameter("@SoYC", cbSoYeuCau.EditValue)
                                If tbSoCG.EditValue.ToString = "" Then
                                    AddParameter("@SoCG", DBNull.Value)
                                Else
                                    AddParameter("@SoCG", tbSoCG.EditValue)
                                End If
                                AddParameter("@IDNgNhap", TaiKhoan)
                                AddParameter("@NgayNhap", tg)
                                If doInsert("tblBaoCaoLichThiCong") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                            End If
                            If Convert.ToDateTime(tbDenNgay.EditValue) > Convert.ToDateTime(tbTuNgay.EditValue) And chkDenNgay.CheckState = CheckState.Checked Then
                                Dim d As Integer = DateDiff(DateInterval.Day, tbTuNgay.EditValue, tbDenNgay.EditValue)
                                For k As Integer = 1 To d
                                    If chkSang.Checked Then
                                        AddParameter("@Ngay", DateAdd(DateInterval.Day, k, tbTuNgay.EditValue))
                                        AddParameter("@BatDau", tbBatDauTu.Text)
                                        AddParameter("@KetThuc", tbKetThucTu.Text)
                                        AddParameter("@IDNoiDung", cbNoiDungThiCong.EditValue)
                                        AddParameter("@IDNgThucHien", CType(nod2(0), Utils.ItemObject).Value)
                                        AddParameter("@SoYC", cbSoYeuCau.EditValue)
                                        If tbSoCG.EditValue.ToString = "" Then
                                            AddParameter("@SoCG", DBNull.Value)
                                        Else
                                            AddParameter("@SoCG", tbSoCG.EditValue)
                                        End If
                                        AddParameter("@IDNgNhap", TaiKhoan)
                                        AddParameter("@NgayNhap", tg)
                                        If doInsert("tblBaoCaoLichThiCong") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                                    End If
                                  
                                    If chkChieu.Checked Then
                                        AddParameter("@Ngay", DateAdd(DateInterval.Day, k, tbTuNgay.EditValue))
                                        AddParameter("@BatDau", tbBatDauDen.Text)
                                        AddParameter("@KetThuc", tbKetThucDen.Text)
                                        AddParameter("@IDNoiDung", cbNoiDungThiCong.EditValue)
                                        AddParameter("@IDNgThucHien", CType(nod2(0), Utils.ItemObject).Value)
                                        AddParameter("@SoYC", cbSoYeuCau.EditValue)
                                        If tbSoCG.EditValue.ToString = "" Then
                                            AddParameter("@SoCG", DBNull.Value)
                                        Else
                                            AddParameter("@SoCG", tbSoCG.EditValue)
                                        End If
                                        AddParameter("@IDNgNhap", TaiKhoan)
                                        AddParameter("@NgayNhap", tg)
                                        If doInsert("tblBaoCaoLichThiCong") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                                    End If
                                Next
                            End If
                        End If

                    Next
                Next

            Else
                For i As Integer = 0 To treeNV.Nodes.Count - 1
                    Dim nod1 As TreeListNode = treeNV.Nodes(i)
                    ' If nod1.Checked Then strNVThamGia &= CType(nod1(0), Utils.ItemObject).Value & ","
                    For j As Integer = 0 To nod1.Nodes.Count - 1
                        Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                        If nod2.Checked Then
                            AddParameter("@Ngay", tbTuNgay.EditValue)
                            AddParameter("@BatDau", tbBatDauTu.Text)
                            AddParameter("@KetThuc", tbKetThucTu.Text)
                            AddParameter("@IDNoiDung", cbNoiDungThiCong.EditValue)
                            AddParameter("@IDNgThucHien", CType(nod2(0), Utils.ItemObject).Value)
                            AddParameter("@SoYC", cbSoYeuCau.EditValue)
                            If tbSoCG.EditValue.ToString = "" Then
                                AddParameter("@SoCG", DBNull.Value)
                            Else
                                AddParameter("@SoCG", tbSoCG.EditValue)
                            End If
                            AddParameterWhere("@ID", objID)
                            If doUpdate("tblBaoCaoLichThiCong", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        End If
                    Next
                Next
            End If

            ShowAlert(Me.Text & " thành công !")
            If Me.Tag = "Lich" Then
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmLichThiCongCongTrinh).btTaiLichLamViec.PerformClick()
            Else
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmBaoCaoLichThiCong).btTaiBaoCao.PerformClick()
            End If
            Return True
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            Return False
        End Try

    End Function


    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        If Luu() Then
            Me.Close()
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Luu()
    End Sub

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        TrangThai.isAddNew = True
        cbNoiDungThiCong.EditValue = Nothing
    End Sub
    Private Sub cbSoYeuCau_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbSoYeuCau.EditValueChanged

        loadDSNoiDungThiCong()

    End Sub
    'Private Sub cbSoYeuCau_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbSoYeuCau.EditValueChanged
    '    On Error Resume Next

    '    Dim edit As GridLookUpEdit = CType(sender, GridLookUpEdit)
    '    Dim dr As DataRowView = edit.GetSelectedDataRow
    '    cbSoYeuCau.Tag = dr("IDLoaiYeuCau")
    '    'tbSoCG.EditValue = dr("SoCG")
    '    'cbSoYeuCau.Tag = gdvSoYC.GetFocusedRowCellValue("IDLoaiYeuCau")
    '    'tbSoCG.EditValue = gdvSoYC.GetFocusedRowCellValue("SoCG")
    '    loadDSNoiDungThiCong()

    'End Sub

    'Private Sub cbThucHien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    If e.Button.Index = 1 Then
    '        cbThucHien.EditValue = Nothing
    '    End If
    'End Sub
    Private Sub chkSang_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkSang.CheckedChanged
        tbBatDauTu.Enabled = chkSang.Checked
        tbKetThucTu.Enabled = chkSang.Checked
    End Sub
    Private Sub chkChieu_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkChieu.CheckedChanged
        tbBatDauDen.Enabled = chkChieu.Checked
        tbKetThucDen.Enabled = chkChieu.Checked
    End Sub

    Private Sub treeNV_AfterCheckNode(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.NodeEventArgs) Handles treeNV.AfterCheckNode
        If e.Node.Nodes.Count > 0 Then
            For i As Integer = 0 To e.Node.Nodes.Count - 1
                e.Node.Nodes(i).Checked = e.Node.Checked
            Next
        Else
            Dim _count As Integer = 0
            If e.Node.ParentNode Is Nothing Then Exit Sub
            For i As Integer = 0 To e.Node.ParentNode.Nodes.Count - 1
                If e.Node.ParentNode.Nodes(i).Checked Then _count += 1
            Next
            If _count > 0 Then
                e.Node.ParentNode.CheckState = CheckState.Checked
            Else
                e.Node.ParentNode.CheckState = CheckState.Indeterminate
            End If
        End If
    End Sub

    Private Sub chkDenNgay_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkDenNgay.CheckedChanged
        tbDenNgay.Enabled = chkDenNgay.CheckState
    End Sub
 
    Private Sub gdvNoiDungThiCong_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gdvNoiDungThiCong.RowStyle
        Dim view = cbNoiDungThiCong.Properties.View
        If view.GetRowCellValue(e.RowHandle, "DiemSo") = 0 And Not view.IsGroupRow(e.RowHandle) And Not view.IsFilterRow(e.RowHandle) Then
            e.Appearance.ForeColor = Color.Gray
        Else
            If view.GetRowCellValue(e.RowHandle, "DiemSo") <> 0 And Not view.IsGroupRow(e.RowHandle) And Not view.IsFilterRow(e.RowHandle) Then
                e.Appearance.Font = New Font(Me.Font.FontFamily, 8, FontStyle.Bold)
            End If
        End If
    End Sub
    Private Sub tbBatDauTu_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles tbBatDauTu.Validating
        Dim time As DateTime = tbBatDauTu.EditValue
        Dim chot As TimeSpan = New TimeSpan(0, 0, 0)
        If time.TimeOfDay < chot Then
            tbBatDauTu.EditValue = "00:00"
        End If
    End Sub
    Private Sub tbKetThucTu_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles tbKetThucTu.Validating
        Dim time As DateTime = tbKetThucTu.EditValue
        Dim chot As TimeSpan = New TimeSpan(12, 0, 0)
        If time.TimeOfDay > chot Then
            tbKetThucTu.EditValue = "12:00"
        End If
    End Sub
    Private Sub tbBatDauDen_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles tbBatDauDen.Validating
        Dim time As DateTime = tbBatDauDen.EditValue
        Dim chot As TimeSpan = New TimeSpan(12, 0, 0)
        If time.TimeOfDay < chot Then
            tbBatDauDen.EditValue = "12:00"
        End If
    End Sub
    Private Sub tbKetThucDen_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles tbKetThucDen.Validating
        Dim time As DateTime = tbKetThucDen.EditValue
        Dim chot As TimeSpan = New TimeSpan(23, 59, 59)
        If time.TimeOfDay > chot Then
            tbKetThucDen.EditValue = "23:59"
        End If
    End Sub
End Class