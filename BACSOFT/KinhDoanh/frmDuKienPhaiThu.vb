Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors.Repository
Imports SpreadsheetGear
Imports DevExpress.XtraEditors
Imports BACSOFT.Utils

Public Class frmDuKienPhaiThu
    Public _exit As Boolean = False
    Public _FileCGKinhDoanh As String = ""

    Private Sub frmDuKienPhaiThu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btNhanVien.Enabled = False
        End If
        LoadrCbNhanVien()
        LoadrCbKH()
        LoadDS()
    End Sub

#Region "Load DS đặt hàng cần nhập"
    ' 
    Public Sub LoadDS()
        ShowWaiting("Đang tải dữ liệu...")

        Dim sql As String = ""
        sql &= " SELECT ROW_NUMBER() OVER(ORDER BY tblCongNo.NgayCongNo) AS STT,tblCongNo.ID,"
        sql &= " tblCongNo.NgayCongNo,tblCongNo.SoTien,tblCongNo.SoPhieu1,tblCongNo.SoPhieu2,tblCongNo.GhiChu,"
        sql &= " PT.Ten AS PhuTrach,KHACHHANG.ttcMa,datediff(day,getdate(),tblCongNo.NgayCongNo)SoNgay"
        sql &= " FROM tblCongNo"
        sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=tblCongNo.SoPhieu1"
        If Not cbKH.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDKhachHang = " & cbKH.EditValue
        End If
        If Not btNhanVien.EditValue Is Nothing Then
            sql &= " AND BANGCHAOGIA.IDTakeCare = " & btNhanVien.EditValue
        End If
        sql &= " INNER JOIN NHANSU AS PT ON PT.ID=BANGCHAOGIA.IDTakeCare"
        sql &= " INNER JOIN KHACHHANG ON KHACHHANG.ID=BANGCHAOGIA.IDKhachHang"
        sql &= " WHERE tblCongNo.Loai=0 AND tblCongNo.TrangThai <>0"
        sql &= " ORDER BY TblCongNo.NgayCongNo"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)

        If Not dt Is Nothing Then
            gdvPhaiThu.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        CloseWaiting()

    End Sub

    Private Sub LoadrCbNhanVien()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 And TrangThai=1")
        If Not dt Is Nothing Then
            rCbNhanVien.DataSource = dt
            btNhanVien.EditValue = TaiKhoan
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadrCbKH()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,ttcMa,Ten FROM KHACHHANG order by ttcMa")
        If Not dt Is Nothing Then
            rcbKH.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region


    Private Sub btXem_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        LoadDS()
    End Sub


    Private Sub rCbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rCbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            btNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbKH.ButtonClick
        If e.Button.Index = 1 Then
            cbKH.EditValue = Nothing
        End If
    End Sub

    Private Sub gdvPhaiThuCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvPhaiThuCT.RowCellStyle
        If e.Column.FieldName = "STT" Then
            If e.RowHandle < 0 Then Exit Sub
            If gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "SoNgay") <= 0 Then
                e.Appearance.BackColor = Color.Red
            ElseIf gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "SoNgay") > 0 And gdvPhaiThuCT.GetRowCellValue(e.RowHandle, "SoNgay") <= 3 Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub

    Private Sub gdvPhaiThuCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvPhaiThuCT.MouseDown
        Dim HitInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo
        HitInfo = gdvPhaiThuCT.CalcHitInfo(gdvPhaiThu.PointToClient(Cursor.Position))
        If HitInfo.InColumnPanel Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            PopupMenu1.ShowPopup(gdvPhaiThu.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mXacNhanDaTHu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXacNhanDaTHu.ItemClick
        If ShowCauHoi("Xác nhận đã thu ?") Then
            Dim c As Integer = 0
            For i As Integer = 0 To gdvPhaiThuCT.SelectedRowsCount - 1
                AddParameter("@TrangThai", 0)
                AddParameterWhere("@IDD", gdvPhaiThuCT.GetRowCellValue(gdvPhaiThuCT.GetSelectedRows(i), "ID"))
                If doUpdate("tblCongNo", "ID=@IDD") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    c += 1
                End If
            Next
            If c > 0 Then
                ShowAlert("Đã cập nhật !")
            End If
            btXem.PerformClick()
        End If
    End Sub
End Class
