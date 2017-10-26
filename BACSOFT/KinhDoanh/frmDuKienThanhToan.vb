Imports BACSOFT.Db.SqlHelper

Public Class frmDuKienThanhToan
    Public _SoPhieuCGDH As String = ""
    Public _SoPhieuXNK As String = ""
    Public _Buoc1 As Boolean = True
    Public _PhaiTra As Boolean = False
    Public _exit As Boolean = False

    Private Sub frmDuKienThanhToan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Loaddata()
    End Sub

    Public Sub Loaddata()
        Dim sql As String = ""
        sql = "SELECT ID, ROW_NUMBER() OVER(ORDER BY NgayCongNo) AS STT,NgaySua,"
        If Not _PhaiTra Then
            sql &= " (Case (SELECT TienTruocThue + TienThue FROM BANGCHAOGIA WHERE SoPhieu='" & _SoPhieuCGDH & "') WHEN 0 then 0 ELSE "
            If _Buoc1 Then
                sql &= " round((SoTien/(SELECT TienTruocThue + TienThue FROM BANGCHAOGIA WHERE SoPhieu='" & _SoPhieuCGDH & "'))*100,2) END) as PTTT,SoTien,NgayCongNo,SoPhieu1, SoPhieu2,GhiChu,Convert(bit,0) Modify FROM tblCongNo WHERE "
            Else
                sql &= " round((SoTien/(SELECT TienTruocThue + TienThue FROM PHIEUXUATKHO WHERE SoPhieu='" & _SoPhieuCGDH & "'))*100,2) END) as PTTT,SoTien,NgayCongNo,SoPhieu1, SoPhieu2,GhiChu,Convert(bit,0) Modify FROM tblCongNo WHERE "
            End If
            sql &= " Loai=0 "
        Else
            sql &= " (Case (SELECT TienTruocThue + TienThue FROM PHIEUDATHANG WHERE SoPhieu='" & _SoPhieuCGDH & "') WHEN 0 then 0 ELSE "
            sql &= " round((SoTien/(SELECT TienTruocThue + TienThue FROM PHIEUDATHANG WHERE SoPhieu='" & _SoPhieuCGDH & "'))*100,2) END) as PTTT,SoTien,NgayCongNo,SoPhieu1, SoPhieu2,GhiChu,Convert(bit,0) Modify FROM tblCongNo WHERE "
            sql &= " Loai=1 "
        End If

        If _Buoc1 Then
            sql &= " AND SoPhieu1='" & _SoPhieuCGDH & "'"
        Else
            sql &= " AND SoPhieu2='" & _SoPhieuXNK & "' OR (SoPhieu2 is null AND SoPhieu1='" & _SoPhieuCGDH & "' )"
        End If

        sql &= " ORDER BY NgayCongNo"

        If Not _PhaiTra Then
            sql &= " SELECT NgayNhan,(TienTruocThue+TienThue)Tien FROM BANGCHAOGIA WHERE SoPhieu='" & _SoPhieuCGDH & "'"
        Else
            sql &= " SELECT NgayNhan,(TienTruocThue+TienThue)Tien FROM PHIEUDATHANG WHERE SoPhieu='" & _SoPhieuCGDH & "'"
        End If

        If Not _Buoc1 Then
            If Not _PhaiTra Then
                sql &= " SELECT NgayThang,(TienTruocThue+TienThue)Tien FROM PHIEUXUATKHO WHERE SoPhieu='" & _SoPhieuXNK & "'"
            Else
                sql &= " SELECT NgayThang,(TienTruocThue+TienThue)Tien FROM PHIEUNHAPKHO WHERE SoPhieu='" & _SoPhieuXNK & "'"
            End If
        End If



        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            gdv.DataSource = ds.Tables(0)
            If ds.Tables(1).Rows.Count = 1 Then
                lbNgayCG.Caption = String.Format(lbNgayCG.Caption, ds.Tables(1).Rows(0)("NgayNhan"))
                If _PhaiTra Then
                    lbTienCG.Caption = String.Format(lbTienCG.Caption, "ĐH", ds.Tables(1).Rows(0)("Tien"))
                Else
                    lbTienCG.Caption = String.Format(lbTienCG.Caption, "CG", ds.Tables(1).Rows(0)("Tien"))
                End If

                lbTienCG.Tag = ds.Tables(1).Rows(0)("Tien")

                If Not _Buoc1 Then
                    lbNgayNX.Caption = String.Format(lbNgayNX.Caption, ds.Tables(2).Rows(0)("NgayThang"))
                    If _PhaiTra Then
                        lbTienXK.Caption = String.Format(lbTienXK.Caption, "NK", ds.Tables(2).Rows(0)("Tien"))
                    Else
                        lbTienXK.Caption = String.Format(lbTienXK.Caption, "XK", ds.Tables(2).Rows(0)("Tien"))
                    End If
                    lbTienXK.Tag = ds.Tables(2).Rows(0)("Tien")
                Else
                    lbTienXK.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    lbNgayNX.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            Else
                If Not _Buoc1 Then
                    lbNgayNX.Caption = String.Format(lbNgayNX.Caption, 0)
                    If _PhaiTra Then
                        lbTienXK.Caption = String.Format(lbTienXK.Caption, "NK", 0)
                    Else
                        lbTienXK.Caption = String.Format(lbTienXK.Caption, "XK", 0)
                    End If
                Else
                    lbTienXK.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    lbNgayNX.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
                lbNgayCG.Caption = String.Format(lbNgayCG.Caption, 0)
                If _PhaiTra Then
                    lbTienCG.Caption = String.Format(lbTienCG.Caption, "ĐH", 0)
                Else
                    lbTienCG.Caption = String.Format(lbTienCG.Caption, "CG", 0)
                End If

                lbTienCG.Tag = 0
                lbTienXK.Tag = 0
            End If



        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvCT.CalcHitInfo(gdv.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            pMenu.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub btThem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btThem.ItemClick, mThem.ItemClick
        gdvCT.AddNewRow()
    End Sub

    Private Sub gdvCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvCT.InitNewRow
        '_exit = True
        gdvCT.SetRowCellValue(e.RowHandle, "STT", gdvCT.RowCount)
        gdvCT.SetRowCellValue(e.RowHandle, "NgaySua", Now)
        gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        gdvCT.SetRowCellValue(e.RowHandle, "SoPhieu1", _SoPhieuCGDH)
        If Not _Buoc1 Then
            gdvCT.SetRowCellValue(e.RowHandle, "SoPhieu2", _SoPhieuXNK)
        End If
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "PTTT" Then
            If _exit Then Exit Sub
            _exit = True
            If _Buoc1 Then
                gdvCT.SetRowCellValue(e.RowHandle, "SoTien", Math.Round(CType(lbTienCG.Tag, Double) * e.Value / 100, 0))
            Else
                gdvCT.SetRowCellValue(e.RowHandle, "SoTien", Math.Round(CType(lbTienXK.Tag, Double) * e.Value / 100, 0))
            End If

            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
            _exit = False
        ElseIf e.Column.FieldName = "SoTien" Then
            If _exit Then Exit Sub
            _exit = True
            If _Buoc1 Then
                gdvCT.SetRowCellValue(e.RowHandle, "PTTT", Math.Round((e.Value / CType(lbTienCG.Tag, Double)) * 100, 2))
            Else
                gdvCT.SetRowCellValue(e.RowHandle, "PTTT", Math.Round((e.Value / CType(lbTienXK.Tag, Double)) * 100, 2))
            End If

            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
            _exit = False
        End If
        'If _exit = True Then Exit Sub
        If e.Column.FieldName <> "Modify" Then
            gdvCT.SetRowCellValue(e.RowHandle, "Modify", True)
        End If


    End Sub

    Private Sub btLuu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuu.ItemClick, mLuu.ItemClick
        gdvCT.CloseEditor()
        gdvCT.UpdateCurrentRow()
        Dim tg As DateTime = GetServerTime()

        For i As Integer = 0 To gdvCT.RowCount - 1
            If gdvCT.GetRowCellValue(i, "Modify") Then
                AddParameter("@NgaySua", tg)
                AddParameter("@SoTien", gdvCT.GetRowCellValue(i, "SoTien"))
                AddParameter("@SoPhieu1", gdvCT.GetRowCellValue(i, "SoPhieu1"))
                AddParameter("@SoPhieu2", gdvCT.GetRowCellValue(i, "SoPhieu2"))
                AddParameter("@NgayCongNo", gdvCT.GetRowCellValue(i, "NgayCongNo"))
                AddParameter("@GhiChu", gdvCT.GetRowCellValue(i, "GhiChu"))
                AddParameter("@Loai", _PhaiTra)
                If IsDBNull(gdvCT.GetRowCellValue(i, "ID")) Or gdvCT.GetRowCellValue(i, "ID") Is Nothing Then
                    AddParameter("@NgayLap", tg)
                    Dim _id As Object = doInsert("tblCongNo")
                    If _id Is Nothing Then
                        ShowBaoLoi(i.ToString & ": " & LoiNgoaiLe)
                    Else
                        _exit = True
                        gdvCT.SetRowCellValue(i, "ID", _id)
                        gdvCT.SetRowCellValue(i, "Modify", False)
                        _exit = False
                    End If
                Else
                    AddParameterWhere("@IDD", gdvCT.GetRowCellValue(i, "ID"))
                    If doUpdate("tblCongNo", "ID=@IDD") Is Nothing Then
                        ShowBaoLoi(i.ToString & ": " & LoiNgoaiLe)
                    End If

                End If
            End If
        Next
        ShowAlert("Đã lưu !")
    End Sub

    Private Sub btXoa_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXoa.ItemClick, mXoa.ItemClick
        If ShowCauHoi("Xóa nội dung đang chọn ?") Then
            If IsDBNull(gdvCT.GetFocusedRowCellValue("ID")) Or gdvCT.GetFocusedRowCellValue("ID") Is Nothing Then
                gdvCT.DeleteSelectedRows()
            Else
                AddParameterWhere("@IDD", gdvCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblCongNo", "ID=@IDD") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvCT.DeleteSelectedRows()
                    ShowAlert("Đã xóa!")
                End If
            End If
        End If
    End Sub
End Class