Imports BACSOFT.Db.SqlHelper

Public Class frmPhatTrienSanPham
    Private _exit As Boolean = False

    Private Sub frmPhatTrienSanPham_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tbThang.EditValue = Today.ToString("MM")
        tbNam.EditValue = Today.Year
        LoadDSHangSX()
        LoadcbPhuTrach()
        LoadDS()
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            gdvCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
            gdvCT.OptionsBehavior.ReadOnly = True
        End If
    End Sub

    Private Sub LoadDSHangSX()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM TenHangSanXuat ORDER BY Ten")
        If Not tb Is Nothing Then
            rcbHang.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadcbPhuTrach()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE NoiCTac=74 ORDER BY Ten")
        If Not tb Is Nothing Then
            rcbPhuTrach.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDS()

        AddParameterWhere("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM PhatTrienSanPham WHERE Thang=@Thang")
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvCT.RowUpdated
        If _exit Then Exit Sub
        Try

            AddParameter("@IDHangSX", gdvCT.GetFocusedRowCellValue("IDHangSX"))
            AddParameter("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
            AddParameter("@IDPhuTrach", gdvCT.GetFocusedRowCellValue("IDPhuTrach"))
            AddParameter("@PTLoiNhuanTM", gdvCT.GetFocusedRowCellValue("PTLoiNhuanTM"))
            AddParameter("@PTLoiNhuanCT", gdvCT.GetFocusedRowCellValue("PTLoiNhuanCT"))
   
            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
                Dim _id As Object = doInsert("PhatTrienSanPham")
                If _id Is Nothing Then Throw New Exception(LoiNgoaiLe)
                _exit = True
                gdvCT.SetFocusedRowCellValue("ID", _id)
                gdvCT.CloseEditor()
                gdvCT.UpdateCurrentRow()
                _exit = False
            Else
                AddParameterWhere("@ID", gdvCT.GetFocusedRowCellValue("ID"))
                If doUpdate("PhatTrienSanPham", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
            ShowAlert("Đã cập nhật")
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
            LoadDS()
        End Try
    End Sub

    Private Sub gdvCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvCT.RowCellClick
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
            Exit Sub
        End If
        If e.Column.FieldName = "IDHangSX" Or e.Column.FieldName = "IDPhuTrach" Then
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub

    Private Sub btLayDuLieuThangTruoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLayDuLieuThangTruoc.ItemClick
        If ShowCauHoi("Tạo dữ liệu mới cho " & tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString & " từ tháng " & DateAdd(DateInterval.Month, -1, New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1)).ToString("MM/yyyy") & " ?") Then
            Dim Sql As String = ""
            Sql &= " INSERT INTO PhatTrienSanPham(IDHangSX,Thang,IDPhuTrach,PTLoiNhuanTM,PTLoiNhuanCT)"
            Sql &= " SELECT IDHangSX,@Thang,IDPhuTrach,PTLoiNhuanTM,PTLoiNhuanCT FROM PhatTrienSanPham WHERE Thang=@ThangTruoc"
            AddParameterWhere("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
            AddParameterWhere("@ThangTruoc", DateAdd(DateInterval.Month, -1, New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1)).ToString("MM/yyyy"))
            If ExecuteSQLNonQuery(Sql) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật !")
                LoadDS()
            End If
        End If
    End Sub
End Class