Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports System.IO
Imports System.Runtime.Serialization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Columns

Public Class frmKhauHaoTaiSan
    Private Shared query As String
    Private Shared dt As DataTable

    Private Sub loadGV()
        Dim time As DateTime = barDeDenNgay.EditValue
        Dim time2 As DateTime = barDeTuNgay.EditValue
        query = meSql.Text
        query = query.Replace("2013/8/31", time.ToString("yyyy/MM/dd"))
        query = query.Replace("2013/7/31", time2.ToString("yyyy/MM/dd"))
        If chkXemHet.Checked Then
            query &= " where SoNgayKH<datediff(day, NgayThang,@time) or (SoLuong-SoLuongHongDauKy-SoLuongHongTrongKy)=0"
        Else
            query &= " where SoNgayKH>=datediff(day, NgayThang,@time)"
        End If
        query &= " order by NgayThang desc, Id desc "
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If Not dt Is Nothing Then
            gc.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
        '  End If

    End Sub

    Private Sub loadData()
     
        Dim hientai As DateTime = GetServerTime.Date()
        barDeDenNgay.EditValue = hientai
        barDeTuNgay.EditValue = New DateTime(hientai.Year, hientai.Month, 1)
        loadGV()
    End Sub


    Private Sub frmKhauHaoTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub barCbbThang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barCbbThang.EditValueChanged
        loadGV()
    End Sub

    Private Sub barSenam_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barSeNam.EditValueChanged
        loadGV()
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        loadGV()
    End Sub

    Private Sub barLueNhomVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueNhomVT.EditValueChanged
        loadGV()
    End Sub

    Private Sub barLueHang_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueHang.EditValueChanged
        loadGV()
    End Sub

    Private Sub barTxtMaVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barTxtMaVT.EditValueChanged
        loadGV()
    End Sub

    Private Sub barLueTenVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles barLueTenVT.EditValueChanged
        loadGV()
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim gvexprort As DevExpress.XtraGrid.Views.Grid.GridView = gv

        Dim saveDialog As SaveFileDialog = New SaveFileDialog()
        saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html"

        'For i = 0 To gvKhauHaoTs.Columns.Count - 1
        '    If gvKhauHaoTs.Columns(i).Caption <> "Ngày trực nhật" And gvKhauHaoTs.Columns(i).Caption <> "Người trực" Then
        '        gvKhauHaoTs.Columns(i).Visible = False
        '        'i -= 1
        '    End If
        'Next i
        If saveDialog.ShowDialog() = DialogResult.OK Then
            Dim exportFilePath As String = saveDialog.FileName
            Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
            Dim str As String

            Select Case fileExtenstion
                Case ".xls"
                    gv.ExportToXls(exportFilePath)
                Case (".xlsx")
                    gv.ExportToXlsx(exportFilePath)
                Case ".rtf"
                    gv.ExportToRtf(exportFilePath)
                Case ".pdf"
                    gv.ExportToPdf(exportFilePath)
            End Select

            System.Diagnostics.Process.Start(exportFilePath)

            If File.Exists(exportFilePath) Then
                Try
                    System.Diagnostics.Process.Start(exportFilePath)
                Catch ex As Exception
                    str = "Không thể mở file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                    ShowBaoLoi(str)
                End Try
            Else
                str = "Không thể lưu file này." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath
                ShowBaoLoi(str)
            End If
        End If

    End Sub

    Private Sub riSeNam_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles riSeNam.EditValueChanged
        Bar1.Manager.ActiveEditItemLink.PostEditor()
    End Sub

    Private Sub barCiLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barCiLoc.CheckedChanged
        If barCiLoc.Checked = True Then
            gv.OptionsView.ShowAutoFilterRow = True
        Else
            gv.OptionsView.ShowAutoFilterRow = False
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiLai.ItemClick
        loadGV()
    End Sub

    Private Sub riCbbThang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riCbbThang.ButtonClick
        If e.Button.Index = 1 Then
            barCbbThang.EditValue = Today.Month
        End If
    End Sub

    Private Sub riSeNam_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riSeNam.ButtonClick
        If e.Button.Index = 1 Then
            barSeNam.EditValue = Today.Year
        End If
    End Sub
    Private Sub riLueNhomVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueNhomVT.ButtonClick
        If e.Button.Index = 1 Then
            barLueNhomVT.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueHang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueHang.ButtonClick
        If e.Button.Index = 1 Then
            barLueHang.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueTenVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles riLueTenVT.ButtonClick
        If e.Button.Index = 1 Then
            barLueTenVT.EditValue = Nothing
        End If
    End Sub

    Private Sub gvKhauHaoTs_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gv.HiddenEditor
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub chkXemHet_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkXemHet.CheckedChanged
        If chkXemHet.Checked Then
            chkXemHet.Glyph = My.Resources.Checked
        Else
            chkXemHet.Glyph = My.Resources.UnCheck
        End If
    End Sub
    Private Sub gvPBCongCuDungCu_CustomDrawCell(sender As Object, e As Base.RowCellCustomDrawEventArgs) Handles gv.CustomDrawCell
        If e.Column.FieldName = "thoigiankh" Then
            e.Appearance.ForeColor = Color.Blue
        End If
        'If e.Column.FieldName = "khthang" Then
        '    e.Appearance.BackColor = Color.MistyRose
        'End If
        'If e.Column.FieldName = "khluyke" Then
        '    e.Appearance.BackColor = Color.Salmon
        'End If
        'If e.Column.FieldName = "saukh" Then
        '    e.Appearance.BackColor = Color.Tomato
        'End If
    End Sub

    Private Sub barDeDenNgay_EditValueChanged(sender As Object, e As EventArgs) Handles barDeDenNgay.EditValueChanged
        Dim time As DateTime = barDeDenNgay.EditValue
        barDeTuNgay.EditValue = New DateTime(time.Year, time.Month, 1)
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Alt Or Keys.O) Then
            ' colHongDauKy.Visible = Not colHongDauKy.Visible
            'colHongTrongKy.Visible = Not colHongTrongKy.Visible
            colSoNgayPB.Visible = Not colSoNgayPB.Visible
            If meSql.Visible = True Then
                meSql.Visible = False
            Else
                meSql.Visible = True
            End If
            Return True
        End If
        Return False
    End Function
End Class