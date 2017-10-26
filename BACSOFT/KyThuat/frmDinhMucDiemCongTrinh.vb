Imports BACSOFT.Db.SqlHelper

Public Class frmDinhMucDiemCongTrinh

    Private Sub frmDinhMucDiemCongTrinh_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSDinhMuc()
    End Sub

    Private Sub btNoiDungThiCong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btNoiDungThiCong.ItemClick
        fNoiDungThiCong = New frmNoiDungThiCong
        fNoiDungThiCong.Tag = Me.Parent.Tag
        fNoiDungThiCong.ShowDialog()
    End Sub

    Private Sub btCongViec_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btCongViec.ItemClick
        fCongViec = New frmCongViec
        fCongViec.Tag = Me.Parent.Tag
        fCongViec.ShowDialog()
    End Sub

    Public Sub LoadDSDinhMuc()
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungThiCong)
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT tblDinhMucDiem.*,tblTuDien.NoiDung,tblTuDien.Ma AS STT FROM tblDinhMucDiem LEFT JOIN tblTuDien ON tblDinhMucDiem.IDNoiDungCV=tblTuDien.ID AND tblTuDien.Loai=@Loai ORDER BY STT ")
        If Not dt Is Nothing Then
            gdv.DataSource = dt
            gdvCT.ColumnPanelRowHeight = 60
            gdvCT.Columns("IDNoiDungCV").Visible = False
            gdvCT.Columns("ID").Visible = False
            gdvCT.Columns("STT").OptionsColumn.ReadOnly = True
            gdvCT.Columns("STT").VisibleIndex = 0
            gdvCT.Columns("STT").Width = 40
            gdvCT.Columns("STT").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gdvCT.Columns("STT").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("NoiDung").Caption = "Nội dung thi công"
            gdvCT.Columns("NoiDung").Width = 250
            gdvCT.Columns("NoiDung").VisibleIndex = 1
            gdvCT.Columns("NoiDung").OptionsColumn.ReadOnly = True
            gdvCT.Columns("NoiDung").AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            gdvCT.Columns("NoiDung").ColumnEdit = rMemoText
            gdvCT.Columns("NoiDung").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            gdvCT.Columns("NoiDung").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("HeSo").Caption = "Hệ số"
            gdvCT.Columns("HeSo").Width = 70
            AddParameterWhere("@Loai", LoaiTuDien.NoiDungCongViec)
            Dim tbCv As DataTable = ExecuteSQLDataTable("SELECT ID,NoiDung,isnull(Diem,0)HeSo FROM tblTuDien WHERE Loai=@Loai ORDER BY Ma")
            If Not tbCv Is Nothing Then
                For i As Integer = 0 To tbCv.Rows.Count - 1
                    For j As Integer = 0 To gdvCT.Columns.Count - 1
                        If gdvCT.Columns(j).FieldName = "C" & tbCv.Rows(i)("ID") Then
                            gdvCT.Columns(j).Caption = tbCv.Rows(i)("NoiDung").ToString & vbCrLf & "(" & tbCv.Rows(i)("HeSo").ToString & "%)"
                            gdvCT.Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                            gdvCT.Columns(j).VisibleIndex = j + 3
                        End If
                    Next
                Next
            Else
                ShowCanhBao(LoiNgoaiLe)
            End If
            
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvCT.CellValueChanged
        If e.Column.FieldName = "NoiDung" Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Parent.Tag, DanhMucQuyen.QuyenSua) Then Exit Sub
        AddParameter("@" & e.Column.FieldName, e.Value)
        AddParameterWhere("@ID", gdvCT.GetRowCellValue(e.RowHandle, "ID"))
        If doUpdate("tblDinhMucDiem", "ID=@ID") Is Nothing Then
            ShowCanhBao(LoiNgoaiLe)
        End If
    End Sub

    Private Sub gdvCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            gdvCT.OptionsView.ShowAutoFilterRow = Not gdvCT.OptionsView.ShowAutoFilterRow
        End If
    End Sub

    Private Sub btKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "Dinh Muc Diem.xls"
        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT)
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

    Private Sub mPhanBoLoiNhuan_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mPhanBoLoiNhuan.ItemClick
        Dim f As New frmPhanBoLoiNhuanCT
        f.Tag = Me.Parent.Tag
        f.ShowDialog()
    End Sub

End Class
