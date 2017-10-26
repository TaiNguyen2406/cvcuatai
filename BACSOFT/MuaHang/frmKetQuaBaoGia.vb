Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Linq

Public Class frmKetQuaBaoGia
    Private tbtmp As DataTable
    Private _exit As Boolean = False

    Private Sub frmKetQuaBaoGia_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Application.DoEvents()
        LoadCbPhuTrach()

        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date

        'Threading.Thread.Sleep(2000)
        Application.DoEvents()
        ' btTaiDS.PerformClick()



    End Sub


    Public Sub LoadCbPhuTrach()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 ORDER BY ID ")
        If Not tb Is Nothing Then
            rcbPhuTrach.DataSource = tb
            rcbNguoiNhanXuLy.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub rcbPhuTrach_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhuTrach.ButtonClick
        If e.Button.Index = 1 Then
            cbPhuTrach.EditValue = Nothing
        End If
    End Sub


    Private Sub btTaiDS_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiDS.ItemClick
        Try
            ShowWaiting("Đang tải dữ liệu ...")
            gdv.DataSource = SqlSelect.Select_KetQuaBaoGia(tbTuNgay.EditValue, tbDenNgay.EditValue, chkHienDayDu.Checked)
        Catch ex As Exception
        Finally
            CloseWaiting()
        End Try
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        Dim tg As DateTime = Convert.ToDateTime(tbDenNgay.EditValue)
        tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)
    End Sub

    Private Sub gdvCT_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvCT.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            PopupMenu1.ShowPopup(gdv.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim f As New frmTinhTrangVT
        f.Tag = Me.Parent.Tag
        f._IDVatTu = gdvCT.GetFocusedRowCellValue("IDVatTu")
        f._HienThongTinNX = True
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.TPKinhDoanh) Then
                f._HienNCC = False
                f._HienCGXK = False
            Else
                f._HienNCC = True
                f._HienCGXK = True
            End If
        Else
            f._HienCGXK = True
            f._HienNCC = True
        End If
        f.ShowDialog()
    End Sub

    Private Sub chkHienDayDu_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkHienDayDu.CheckedChanged
        If chkHienDayDu.Checked Then
            chkHienDayDu.Glyph = My.Resources.Checked
        Else
            chkHienDayDu.Glyph = My.Resources.UnCheck
        End If
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "Ket qua bao gia tu " & Convert.ToDateTime(tbTuNgay.EditValue).ToString("dd-MM-yyyy") & " den " & Convert.ToDateTime(tbDenNgay.EditValue).ToString("dd-MM-yyyy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub
End Class
