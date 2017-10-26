Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper
Imports DevExpress.Utils.HorzAlignment

Public Class frmBoPhanTrongSo
    Dim _exit As Boolean = False
    Private Sub frmBoPhanTrongSo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbThang.EditValue = Today.ToString("MM")
        tbNam.EditValue = Today.Year
        loadDS()
    End Sub

    'Private Sub BarManager1_HighlightedLinkChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.HighlightedLinkChangedEventArgs) Handles BarManager1.HighlightedLinkChanged
    '    If Not e.Link Is Nothing AndAlso TypeOf e.Link.Item Is BarButtonItem Then
    '        Cursor = Cursors.Hand
    '    Else
    '        Cursor = Cursors.Default
    '    End If
    'End Sub

    Public Sub loadDS()

        AddParameterWhere("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
        Dim sql As String = " SELECT NhanSu_BoPhan_TrongSo.Ma as ID, NhanSu_BoPhan.Ma,NhanSu_BoPhan.MaBP,NhanSu_BoPhan.Ten, NhanSu_BoPhan_TrongSo.TrongSo,"
        sql &= " (CASE  IDNhom WHEN 1 THEN N'Tạo ra lợi nhuận' WHEN 2 then N'Hỗ trợ' END) TenNhom"
        sql &= " FROM NhanSu_BoPhan LEFT JOIN NhanSu_BoPhan_TrongSo ON NhanSu_BoPhan.Ma= NhanSu_BoPhan_TrongSo.Ma AND NhanSu_BoPhan_TrongSo.Thang=@Thang ORDER BY STT"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Dim _CurentR As Integer = -1

    Private Sub gdvChiTiet_RowUpdated(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gdvChiTiet.RowUpdated
        ' If _exit Then Exit Sub

        If IsDBNull(gdvChiTiet.GetRowCellValue(e.RowHandle, "ID")) Or gdvChiTiet.GetRowCellValue(e.RowHandle, "ID") Is Nothing Then
            AddParameter("@TrongSo", gdvChiTiet.GetRowCellValue(e.RowHandle, "TrongSo"))
            ' AddParameter("@MaBP", gdvChiTiet.GetRowCellValue(e.RowHandle, "MaBP"))
            AddParameter("@Ma", gdvChiTiet.GetRowCellValue(e.RowHandle, "Ma"))
            AddParameter("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
            If doInsert("NhanSu_BoPhan_TrongSo") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                _exit = True
                _CurentR = e.RowHandle
                gdvChiTiet.SetRowCellValue(e.RowHandle, "ID", gdvChiTiet.GetRowCellValue(e.RowHandle, "Ma"))
                'gdvChiTiet.CloseEditor()
                'gdvChiTiet.UpdateCurrentRow()
                '  _exit = False
                ShowAlert("Đã cập nhật")
            End If
            ' Dim objID As Object =

        Else
            If _exit And _CurentR = e.RowHandle Then
                _exit = False
                Exit Sub
            Else
                _exit = False
            End If
            AddParameter("@TrongSo", gdvChiTiet.GetRowCellValue(e.RowHandle, "TrongSo"))
            AddParameterWhere("@Ma", gdvChiTiet.GetRowCellValue(e.RowHandle, "Ma"))
            AddParameterWhere("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
            If doUpdate("NhanSu_BoPhan_TrongSo", "Ma=@Ma AND Thang=@Thang") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật")
            End If
        End If
    End Sub


    Private Sub btCapNhatBoPhan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCapNhatBoPhan.ItemClick
        Dim f As New frmCNBoPhan
        f.ShowDialog()
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        loadDS()
    End Sub

    Private Sub btLayThangTruoc_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLayThangTruoc.ItemClick
        AddParameterWhere("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
        AddParameterWhere("@ThangTruoc", DateAdd(DateInterval.Month, -1, New DateTime(tbNam.EditValue, Convert.ToInt32(tbThang.EditValue), 1)).ToString("MM/yyyy"))
        Dim sql As String = " SELECT TSThangNay.Ma as ID, NhanSu_BoPhan.Ma,NhanSu_BoPhan.MaBP,NhanSu_BoPhan.Ten, TSThangTruoc.TrongSo,"
        sql &= " (CASE  IDNhom WHEN 1 THEN N'Tạo ra lợi nhuận' WHEN 2 then N'Hỗ trợ' END) TenNhom"
        sql &= " FROM NhanSu_BoPhan "
        sql &= " LEFT JOIN NhanSu_BoPhan_TrongSo as TSThangTruoc ON NhanSu_BoPhan.Ma= TSThangTruoc.Ma AND TSThangTruoc.Thang=@ThangTruoc "
        sql &= " LEFT JOIN NhanSu_BoPhan_TrongSo as TSThangNay ON NhanSu_BoPhan.Ma= TSThangNay.Ma AND TSThangNay.Thang=@Thang "
        sql &= " ORDER BY STT"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdv.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub btLuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuLai.ItemClick
        For i As Integer = 0 To gdvChiTiet.RowCount - 1
            If IsDBNull(gdvChiTiet.GetRowCellValue(i, "ID")) Or gdvChiTiet.GetRowCellValue(i, "ID") Is Nothing Then
                AddParameter("@TrongSo", gdvChiTiet.GetRowCellValue(i, "TrongSo"))
                ' AddParameter("@MaBP", gdvChiTiet.GetRowCellValue(e.RowHandle, "MaBP"))
                AddParameter("@Ma", gdvChiTiet.GetRowCellValue(i, "Ma"))
                AddParameter("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
                If doInsert("NhanSu_BoPhan_TrongSo") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    _exit = True
                    _CurentR = i
                    gdvChiTiet.SetRowCellValue(i, "ID", gdvChiTiet.GetRowCellValue(i, "Ma"))
                    'gdvChiTiet.CloseEditor()
                    'gdvChiTiet.UpdateCurrentRow()
                    '  _exit = False

                End If
                ' Dim objID As Object =

            Else
                'If _exit And _CurentR = i Then
                '    _exit = False
                '    Exit Sub
                'Else
                '    _exit = False
                'End If
                AddParameter("@TrongSo", gdvChiTiet.GetRowCellValue(i, "TrongSo"))
                AddParameterWhere("@Ma", gdvChiTiet.GetRowCellValue(i, "Ma"))
                AddParameterWhere("@Thang", tbThang.EditValue.ToString + "/" + tbNam.EditValue.ToString)
                If doUpdate("NhanSu_BoPhan_TrongSo", "Ma=@Ma AND Thang=@Thang") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ' ShowAlert("Đã cập nhật")
                End If
            End If
        Next
        ShowAlert("Đã cập nhật")
    End Sub
End Class
