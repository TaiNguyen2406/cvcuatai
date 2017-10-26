Imports BACSOFT.Db.SqlHelper

Public Class frmDiemThiKyNang

    Private Sub frmDiemThiKyNang_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loadDSPhong()
        LoadDS()
    End Sub

    Public Sub loadDSPhong()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDS()
        Dim sql As String = ""
        sql &= " SELECT NHANSU.IDDepatment, tblDiemThiKyNang.ID,IDNhanVien,NHANSU.Ten AS NhanVien,IDKyNang, NLTen.Ten AS KyNang,"
        sql &= " 	ThoiGian,tblDiemThiKyNang.Diem,NLDanhSach.Diem AS DiemChuan,NgayThi,tblDiemThiKyNang.FileDinhKem,tblDiemThiKyNang.isNew "
        sql &= " FROM tblDiemThiKyNang "
        sql &= " 	LEFT JOIN NHANSU ON NHANSU.ID=tblDiemThiKyNang.IDNhanVien "
        sql &= " 	LEFT JOIN NLDanhSach ON NLDanhSach.ID=tblDiemThiKyNang.IDKyNang"
        sql &= " 	LEFT JOIN NLTen ON NLDanhSach.IDTen=NLTen.ID"
        sql &= " ORDER BY KyNang,NhanVien,NgayThi DESC"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThem.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenThem) Then Exit Sub
        Dim f As New frmCNDiemThiKyNang
        TrangThai.isAddNew = True
        f.ShowDialog()
    End Sub

    Private Sub btSua_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btSua.ItemClick, mSua.ItemClick
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        If gdvCT.FocusedRowHandle < 0 Then Exit Sub
        Dim index As Integer = gdvCT.FocusedRowHandle
        AddParameterWhere("@IDKyNang", gdvCT.GetFocusedRowCellValue("IDKyNang"))
        AddParameterWhere("@NgayThi", gdvCT.GetFocusedRowCellValue("NgayThi"))
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT Id FROM tblDiemThiKyNang WHERE IDKyNang = @IDKyNang AND NgayThi > @NgayThi ")
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        If dt.Rows.Count > 0 Then
            ShowCanhBao("Chỉ được cập nhật bài thi gần nhất !")
            Exit Sub
        End If
        TrangThai.isUpdate = True
        objID = gdvCT.GetFocusedRowCellValue("ID")
        Dim f As New frmCNDiemThiKyNang
        f.ShowDialog()
        gdvCT.FocusedRowHandle = index
    End Sub


    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup

        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))

        If HitInfo.InColumnPanel Then
            e.Cancel = True
        ElseIf HitInfo.RowHandle < 0 Then
            mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        ElseIf HitInfo.RowHandle >= 0 Then
            mSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick
        LoadDS()
    End Sub
End Class
