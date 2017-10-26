Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmTHDiemThiNangLuc

    Private Sub frmTHDiemThiNangLuc_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' tbThoiGian.EditValue = Today.Date
        loadDSPhong()
        loadDSNangLuc()
        LoadDS()
    End Sub

    Public Sub loadDSPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
            rcbPhongCT.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSNangLuc()
        Dim sql As String = ""

        sql &= " SELECT tblNangLuc.ID,Ten,DiemChuan,tblTuDien.NoiDung AS Nhom"
        sql &= " FROM tblNangLuc"
        sql &= " INNER JOIN tblTuDien ON tblNangLuc.IDNhom=tblTuDien.Ma AND tblTuDien.Loai=" & LoaiTuDien.NhomNangLuc

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvNL.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDS()
        Dim sql As String = ""
        sql &= " SELECT tblDiemThiNangLuc.ID,NgayThi,IDNangLuc,GhiChu,FileDinhKem,tblNangLuc.Ten,tblTuDien.NoiDung As Nhom"
        sql &= " FROM tblDiemThiNangLuc"
        sql &= " INNER JOIN tblNangLuc ON tblDiemThiNangLuc.IDNangLuc = tblNangLuc.ID"
        sql &= " INNER JOIN tblTuDien ON tblNangLuc.IDNhom=tblTuDien.Ma AND tblTuDien.Loai=" & LoaiTuDien.NhomNangLuc
        sql &= " ORDER BY NgayThi DESC"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNVThi(ByVal _IDLanThi As Object)
        Dim sql As String = ""
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY Diem DESC) AS STT,tblDiemThiNangLucCT.ID,IDNhanVien,Diem,NHANSU.Ten AS NhanVien"
        sql &= " FROM tblDiemThiNangLucCT"
        sql &= " INNER JOIN NHANSU ON tblDiemThiNangLucCT.IDNhanVien=NHANSU.ID"
        sql &= " WHERE IDLanThi=@LanThi"
        sql &= " ORDER BY Diem DESC "
        AddParameterWhere("@LanThi", _IDLanThi)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdvDiemThi.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        fCNDiemThiNangLuc = New frmCNDiemThiNangLuc
        TrangThai.isAddNew = True
        fCNDiemThiNangLuc.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim index As Integer = gdvCT.FocusedRowHandle
        TrangThai.isUpdate = True
        objID = gdvCT.GetFocusedRowCellValue("ID")
        fCNDiemThiNangLuc = New frmCNDiemThiNangLuc
        fCNDiemThiNangLuc.ShowDialog()
        gdvCT.ClearSelection()
        gdvCT.FocusedRowHandle = index
        gdvCT.SelectRow(index)
    End Sub

    Private Sub btXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xoá mục được chọn ?") Then
            AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
            If doDelete("tblDiemThiNangLuc", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã xoá !")
                LoadDS()
            End If
        End If
    End Sub


    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub

    Private Sub gdv_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdv.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
    End Sub

    Private Sub gdvNL_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvNL.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuNL.ShowPopup(gdvNL.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub pMenuNL_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuNL.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvNLCT.CalcHitInfo(gdvNL.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
    End Sub

    Private Sub btThemNL_Click(sender As System.Object, e As System.EventArgs) Handles btThemNL.Click, mThemNL.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        fCNNangLuc = New frmCNNangLuc
        fCNNangLuc.Tag = "CNNL"
        fCNNangLuc.ShowDialog()

    End Sub

    Private Sub btSuaNL_Click(sender As System.Object, e As System.EventArgs) Handles btSuaNL.Click, mSuaNL.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvNLCT.FocusedRowHandle < 0 Then Exit Sub
        Dim _index As Integer = gdvNLCT.FocusedRowHandle
        TrangThai.isUpdate = True
        objID = gdvNLCT.GetFocusedRowCellValue("ID")
        fCNNangLuc = New frmCNNangLuc
        fCNNangLuc.Tag = "CNNL"
        fCNNangLuc.ShowDialog()

        gdvNLCT.ClearSelection()
        gdvNLCT.FocusedRowHandle = _index
        gdvNLCT.SelectRow(_index)

    End Sub

    Private Sub btXoa_Click(sender As System.Object, e As System.EventArgs) Handles btXoa.Click, mXoaNL.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If gdvNLCT.FocusedRowHandle < 0 Then Exit Sub
        AddParameterWhere("@IDNL", gdvNLCT.GetFocusedRowCellValue("ID"))
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT count(ID) FROM tblDiemThiNangLuc WHERE IDNangLuc=@IDNL")
        If Not tb Is Nothing Then
            If tb.Rows(0)(0) > 0 Then
                ShowCanhBao("Đã có điểm thi cho năng lực này, bạn không thể xóa !")
                Exit Sub
            End If
            If ShowCauHoi("Bạn có muốn xóa năng lực này không ?") Then
                AddParameterWhere("@IDNL", gdvNLCT.GetFocusedRowCellValue("ID"))
                If Not doDelete("tblNangLuc", "ID=@IDNL") Is Nothing Then
                    ShowAlert("Đã xóa !")
                    loadDSNangLuc()
                Else
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If

        End If
    End Sub

    Private Sub gdvCT_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvCT.FocusedRowChanged
        LoadDSNVThi(gdvCT.GetRowCellValue(e.FocusedRowHandle, "ID"))
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.FieldName = "FileDinhKem" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub rPopupFileCT_Popup(sender As System.Object, e As System.EventArgs) Handles rpopupFile.Popup
        gListFileCT.Text = "Danh sách file đính kèm"
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

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            OpenFileOnLocal(UrlThiNangLuc & e.CellValue, e.CellValue, True)
        End If
    End Sub
End Class
