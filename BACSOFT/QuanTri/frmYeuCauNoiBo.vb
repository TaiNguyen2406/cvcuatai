Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmYeuCauNoiBo

    Private Sub frmYeuCauNoiBo_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
        tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        LoadDSNhanSu()
        LoadDSNhom()
        cbThucHien.EditValue = CType(TaiKhoan, Int32)
        LoadDS()
    End Sub



    Private Sub LoadDSNhanSu()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 AND TrangThai=1")
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            rcbNguoiLap.DataSource = tb
            rcbThucHien.DataSource = tb
        End If
    End Sub

    Private Sub LoadDSNhom()
        AddParameterWhere("@Loai", LoaiTuDien.NhomYCNoiBo)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY NoiDung")
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            rcbNhom.DataSource = tb
        End If
    End Sub

    Public Sub LoadDS()
        ShowWaiting("Đang tải yêu cầu ...")
        Dim sql As String = ""
        sql &= " DECLARE @tb table"
        sql &= " ("
        sql &= " 	ID int,"
        sql &= " 	SoPhieu nvarchar(9),"
        sql &= " 	ThoiGianLap Datetime,"
        sql &= " 	ThoiGianDuKien Datetime,"
        sql &= " 	IDNhom int,"
        sql &= " 	NoiDung Nvarchar(MAX),"
        sql &= " 	IDNguoiLap int,"
        sql &= " 	IDThucHien Nvarchar(1000),"
        sql &= " 	ThucHien Nvarchar(500),"
        sql &= " 	NguoiLap Nvarchar(250),"
        sql &= " 	FileDinhKem Nvarchar(MAX),"
        sql &= " 	TrangThai Nvarchar(100)"
        sql &= " )"

        sql &= " INSERT INTO @tb"
        sql &= " SELECT tblYeuCauNoiBo.ID,SoPhieu,ThoiGianLap,ThoiGianDuKien,IDNhom,NoiDung,IDNguoiLap,IDThucHien,N'' AS ThucHien,"
        sql &= "    NHANSU.Ten AS NguoiLap,tblYeuCauNoiBo.FileDinhKem,"
        sql &= "    (CASE tblYeuCauNoiBo.TrangThai WHEN 0 THEN N'Chờ xử lý' WHEN 1 THEN N'Đang xử lý' ELSE N'Đã xử lý' END)TrangThai"
        sql &= " FROM tblYeuCauNoiBo"
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=tblYeuCauNoiBo.IDNguoiLap AND NHANSU.NoiCtac=74"
        sql &= " WHERE 1=1 "
        If cbXemTheo.EditValue = "Nội dung mới" Then
            sql &= " AND tblYeuCauNoiBo.TrangThai <> 2"
        ElseIf cbXemTheo.EditValue = "Theo thời gian" Then
            sql &= " AND Convert(datetime,convert(nvarchar, tblYeuCauNoiBo.ThoiGianLap,103),103) BETWEEN @TuNgay AND @DenNgay"
            AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
            AddParameterWhere("@DenNgay", tbDenNgay.EditValue)
        End If

        If Not cbNguoiLap.EditValue Is Nothing Then
            sql &= " AND tblYeuCauNoiBo.IDNguoiLap = @IDNgLap"
            AddParameterWhere("@IDNgLap", cbNguoiLap.EditValue)
        End If

        If Not cbThucHien.EditValue Is Nothing Then
            sql &= " AND tblYeuCauNoiBo.IDThucHien like '%," & cbThucHien.EditValue & ",%'"
            'AddParameterWhere("@ThucHien", cbThucHien.EditValue)
        End If

        If Not cbNhom.EditValue Is Nothing Then
            sql &= " AND tblYeuCauNoiBo.IDNhom=@Nhom"
            AddParameterWhere("@Nhom", cbNhom.EditValue)
        End If

        sql &= " SELECT * FROM @tb "
        sql &= " ORDER BY SoPhieu DESC "

        sql &= " SELECT ID,Ten FROM NHANSU WHERE Noictac=74"

        sql &= " SELECT  tblYeuCauNoiBoXL.ID,SoPhieu,ThoiGian,NoiDung,IDThucHien,tblYeuCauNoiBoXL.FileDinhKem,NHANSU.Ten AS ThucHien"
        sql &= " FROM tblYeuCauNoiBoXL"
        sql &= " INNER JOIN NHANSU ON NHANSU.ID = tblYeuCauNoiBoXL.IDThucHien"
        sql &= " WHERE tblYeuCauNoiBoXL.SoPhieu IN (SELECT SoPhieu FROM @tb)"
        sql &= " ORDER BY ThoiGian"

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim tb2 As DataTable = DataSourceDSFile(ds.Tables(0).Rows(i)("IDThucHien").ToString, "ID", ",")
                For j As Integer = 0 To tb2.Rows.Count - 1
                    If IsNumeric(tb2.Rows(j)("ID")) Then
                        Dim collectRowNV() As DataRow = ds.Tables(1).Select("ID=" & tb2.Rows(j)("ID"))
                        If collectRowNV.Length > 0 Then
                            ds.Tables(0).Rows(i)("ThucHien") &= "- " & collectRowNV(0)("Ten").ToString & vbCrLf
                        End If
                    End If

                Next
                ds.Tables(0).Rows(i)("ThucHien") = ds.Tables(0).Rows(i)("ThucHien").ToString.Trim
            Next
            ds.Relations.Add(ds.Tables(0).Columns("SoPhieu"), ds.Tables(2).Columns("SoPhieu"))
            ds.Relations.Item(0).RelationName = "Xử lý"

            gdv.DataSource = ds.Tables(0)
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub gdv_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdv.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub gdvXuLyYC_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvXuLyYC.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            pMenuXL.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub pMenu_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenu.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
    End Sub

    Private Sub pMenuXL_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles pMenuXL.BeforePopup
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvXuLyYC.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub
    End Sub

    Private Sub cbXemTheo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbXemTheo.EditValueChanged
        If cbXemTheo.EditValue = "Theo thời gian" Then
            tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    Private Sub rcbNguoiLap_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNguoiLap.ButtonClick
        If e.Button.Index = 1 Then
            cbNguoiLap.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbThucHien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbThucHien.ButtonClick
        If e.Button.Index = 1 Then
            cbThucHien.EditValue = Nothing
        End If
    End Sub

    Private Sub btLapYeuCau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLapYeuCau.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNYeuCauNoiBo
        f.ShowDialog()
    End Sub

    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        LoadDS()
    End Sub

    Private Sub btThemQuaTrinhXL_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThemQuaTrinhXL.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        TrangThai.isAddNew = True
        Dim f As New frmCNYeuCauNoiBoXL
        f._SP = gdvCT.GetFocusedRowCellValue("SoPhieu")
        f._IDNgYC = gdvCT.GetFocusedRowCellValue("IDNguoiLap")
        f.ShowDialog()
    End Sub

    Private Sub gdvCT_CustomDrawCell(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles gdvCT.CustomDrawCell
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If e.Column.VisibleIndex = 0 And view.IsMasterRowEmptyEx(e.RowHandle, 0) And view.IsMasterRowEmptyEx(e.RowHandle, 1) Then
            CType(e.Cell, DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).CellButtonRect = Rectangle.Empty
        End If
    End Sub

    Private Sub mSuaQuaTrinhXL_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mSuaQuaTrinhXL.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If Not gdvCT.GetMasterRowExpanded(gdvCT.FocusedRowHandle) Then Exit Sub
        If CType(TaiKhoan, Int32) <> CType(CType(gdvCT.GetDetailView(gdvCT.FocusedRowHandle, gdvCT.GetRelationIndex(gdvCT.FocusedRowHandle, "Xử lý")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("IDThucHien"), Int32) Then
            ShowCanhBao("Bạn không có quyền sửa yêu cầu của người khác !")
            Exit Sub
        End If
        TrangThai.isUpdate = True
        Dim Index As Integer = gdvCT.FocusedRowHandle
        Dim indexXL As Integer = CType(gdvCT.GetDetailView(gdvCT.FocusedRowHandle, gdvCT.GetRelationIndex(gdvCT.FocusedRowHandle, "Xử lý")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle
        objID = CType(gdvCT.GetDetailView(gdvCT.FocusedRowHandle, gdvCT.GetRelationIndex(gdvCT.FocusedRowHandle, "Xử lý")), DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")
        Dim f As New frmCNYeuCauNoiBoXL
        f.Tag = Me.Parent.Tag
        f._IDNgYC = gdvCT.GetFocusedRowCellValue("IDNguoiLap")
        f._SP = gdvCT.GetFocusedRowCellValue("SoPhieu")
        f.ShowDialog()
        gdvCT.FocusedRowHandle = Index
        gdvCT.SelectRow(Index)
        gdvCT.ExpandMasterRow(Index, "Xử lý")
        CType(gdvCT.GetDetailView(gdvCT.FocusedRowHandle, gdvCT.GetRelationIndex(gdvCT.FocusedRowHandle, "Xử lý")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle = indexXL

    End Sub

    Private Sub btSuaYeuCau_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSuaYeuCau.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If CType(TaiKhoan, Int32) <> CType(gdvCT.GetFocusedRowCellValue("IDNguoiLap"), Int32) Then
            ShowCanhBao("Bạn không có quyền sửa yêu cầu của người khác !")
            Exit Sub
        End If
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT TrangThai FROM tblYeuCauNoiBo WHERE SoPhieu=N'" & gdvCT.GetFocusedRowCellValue("SoPhieu") & "'")
        If Not tb Is Nothing Then
            Dim sql As String = ""
            If tb.Rows(0)("TrangThai") = 1 Then
                sql = "Yêu cầu đang được xử lý, bạn không thể sửa nội dung !"
            ElseIf tb.Rows(0)("TrangThai") = 2 Then
                sql = "Yêu cầu đã hoàn tất xử lý, bạn không thể sửa nội dung !"
            End If
            If sql <> "" Then
                ShowCanhBao(sql)
                Exit Sub
            End If

        End If

        TrangThai.isUpdate = True
        Dim index As Integer = gdvCT.FocusedRowHandle
        Dim f As New frmCNYeuCauNoiBo
        f.Tag = Me.Parent.Tag
        f._SP = gdvCT.GetFocusedRowCellValue("SoPhieu")
        f.ShowDialog()
        gdvCT.FocusedRowHandle = index
        gdvCT.SelectRow(index)
    End Sub

    Private Sub rcbNhom_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhom.ButtonClick
        If e.Button.Index = 1 Then
            cbNhom.EditValue = Nothing
        End If
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If e.Column.FieldName = "FileDinhKem" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub gdvXuLyYC_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvXuLyYC.RowCellClick
        If e.Column.FieldName = "FileDinhKem" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub rpFile_Popup(sender As System.Object, e As System.EventArgs) Handles rpFile.Popup
        gListFileCT.Text = "Danh sách file đính kèm"
        'If _exit = True Then Exit Sub
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
            OpenFileOnLocal(RootUrl & UrlYeuCauNoiBo & Convert.ToDateTime(gdvCT.GetFocusedRowCellValue("ThoiGianLap")).ToString("yyyyMM") & "\" & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub gdvCT_CalcRowHeight(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs) Handles gdvCT.CalcRowHeight
        If e.RowHandle < 0 Then Exit Sub
        If e.RowHeight > 210 Then
            e.RowHeight = 210
        End If
    End Sub
End Class
