Imports BACSOFT.Db.SqlHelper

Public Class frmCNThongTinPhuKH

    Public _exit = False

    Private Sub frmCNThongTinPhuKH_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadDS()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.QuyenThem) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.QuyenSua) Then
            gdvTinhThanhCT.OptionsBehavior.ReadOnly = True
            gdvKhuCNCT.OptionsBehavior.ReadOnly = True
            gdvLinhVucSXCT.OptionsBehavior.ReadOnly = True
            gdvLoaiHinhDNCT.OptionsBehavior.ReadOnly = True
            gdvThongTinChuSHCT.OptionsBehavior.ReadOnly = True
            gdvTinhTrangCT.OptionsBehavior.ReadOnly = True
        End If
    End Sub

    Public Sub loadDS()
        AddParameterWhere("@TinhThanh", LoaiTuDien.TinhThanh)
        AddParameterWhere("@KhuCN", LoaiTuDien.KhuCN)
        AddParameterWhere("@LinhVuc", LoaiTuDien.LinhVucSX)
        AddParameterWhere("@LoaiDN", LoaiTuDien.LoaiHinhDN)
        AddParameterWhere("@LoaiHinhCSH", LoaiTuDien.LoaiHinhChuSoHuu)
        AddParameterWhere("@TinhTrang", LoaiTuDien.TinhTrangKH)
        Dim sql As String = ""
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@TinhThanh ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@KhuCN ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LinhVuc ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LoaiDN ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@LoaiHinhCSH ORDER BY ID "
        sql &= " SELECT ID,NoiDung FROM tblTuDien WHERE Loai=@TinhTrang ORDER BY ID "

        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdvTinhThanh.DataSource = ds.Tables(0)
            gdvKhuCN.DataSource = ds.Tables(1)
            gdvLinhVucSX.DataSource = ds.Tables(2)
            gdvLoaiHinhDN.DataSource = ds.Tables(3)
            gdvThongTinChuSH.DataSource = ds.Tables(4)
            gdvTinhTrang.DataSource = ds.Tables(5)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvChiTiet_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvTinhThanhCT.RowUpdated, gdvKhuCNCT.RowUpdated, gdvLinhVucSXCT.RowUpdated, gdvLoaiHinhDNCT.RowUpdated, gdvThongTinChuSHCT.RowUpdated, gdvTinhTrangCT.RowUpdated
        If _exit = True Then Exit Sub

        Dim _TenBang As String = ""
        Select Case CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).Name
            Case "gdvTinhThanhCT"
                _TenBang = "tblTuDien"
                AddParameter("@NoiDung", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("NoiDung"))
                AddParameter("@Loai", LoaiTuDien.TinhThanh)
            Case "gdvKhuCNCT"
                _TenBang = "tblTuDien"
                AddParameter("@NoiDung", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("NoiDung"))
                AddParameter("@Loai", LoaiTuDien.KhuCN)
            Case "gdvLinhVucSXCT"
                _TenBang = "tblTuDien"
                AddParameter("@NoiDung", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("NoiDung"))
                AddParameter("@Loai", LoaiTuDien.LinhVucSX)
            Case "gdvLoaiHinhDNCT"
                _TenBang = "tblTuDien"
                AddParameter("@NoiDung", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("NoiDung"))
                AddParameter("@Loai", LoaiTuDien.LoaiHinhDN)
            Case "gdvThongTinChuSHCT"
                _TenBang = "tblTuDien"
                AddParameter("@NoiDung", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("NoiDung"))
                AddParameter("@Loai", LoaiTuDien.LoaiHinhChuSoHuu)
            Case "gdvTinhTrangCT"
                _TenBang = "tblTuDien"
                AddParameter("@NoiDung", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("NoiDung"))
                AddParameter("@Loai", LoaiTuDien.TinhTrangKH)
        End Select


        If IsDBNull(CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID")) Or CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID") Is Nothing Then
            objID = doInsert(_TenBang)
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).DeleteSelectedRows()
            Else
                CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).SetFocusedRowCellValue("ID", objID)
            End If
        Else
            AddParameterWhere("@ID", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
            If doUpdate(_TenBang, "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    Private Sub gdvChiTiet_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvTinhThanhCT.KeyDown, gdvKhuCNCT.KeyDown, gdvLinhVucSXCT.KeyDown, gdvLoaiHinhDNCT.KeyDown, gdvThongTinChuSHCT.KeyDown, gdvTinhTrangCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow = Not CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).OptionsView.ShowAutoFilterRow
        ElseIf e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xóa mục được chọn ?") Then
                AddParameterWhere("@ID", CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("ID"))
                If doDelete("tblTuDien", "ID=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).DeleteSelectedRows()
                End If
            End If
            
        End If
    End Sub

    Private Sub frmCNThongTinPhuKH_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        fCNKhachHang.loadTuDien()
    End Sub
End Class