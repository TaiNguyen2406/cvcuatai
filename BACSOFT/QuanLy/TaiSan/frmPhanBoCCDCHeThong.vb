Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports System.IO
Imports System.Runtime.Serialization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Columns

Public Class frmPhanBoCCDCHeThong
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
    End Sub

    Private Sub loadData()
         Dim hientai As DateTime = GetServerTime.Date()
        barDeDenNgay.EditValue = hientai
        barDeTuNgay.EditValue = New DateTime(hientai.Year, hientai.Month, 1)
        riLueLoaiCCDC.DataSource = TAI.tableLoaiCCDC()
        loadGV()
    End Sub

    Private Sub frmThongTinCongCuDungCu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub barCbbThang_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
    End Sub

    Private Sub barSeNam_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
    End Sub

    Private Sub barTxtMaVT_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
    End Sub

    Private Sub barLueTenVT_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
    End Sub

    Private Sub barLueNhomVT_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
    End Sub

    Private Sub barLueHang_EditValueChanged(sender As System.Object, e As System.EventArgs)
        loadGV()
    End Sub
    Private Sub PopupMenu1_BeforePopup(sender As System.Object, e As System.ComponentModel.CancelEventArgs)
        If gv.RowCount < 1 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub riSeNam_EditValueChanged(sender As System.Object, e As System.EventArgs)
        Bar1.Manager.ActiveEditItemLink.PostEditor()
    End Sub
    Private Sub barCiLoc_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles barCiLoc.CheckedChanged
        If barCiLoc.Checked = True Then
            gv.OptionsView.ShowAutoFilterRow = True
        Else
            gv.OptionsView.ShowAutoFilterRow = False
        End If
    End Sub
    Private Sub BarButtonItem4_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiLai.ItemClick
        loadGV()
    End Sub

    Private Sub riCbbThang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Index = 1 Then
            barCbbThang.EditValue = Today.Month
        End If
    End Sub
    Private Sub riSeNam_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Index = 1 Then
            barSeNam.EditValue = Today.Year
        End If
    End Sub
    Private Sub riLueNhomVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Index = 1 Then
            barLueNhomVT.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueHang_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Index = 1 Then
            barLueHang.EditValue = Nothing
        End If
    End Sub
    Private Sub riLueTenVT_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Index = 1 Then
            barLueTenVT.EditValue = Nothing
        End If
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
        'If e.Column.FieldName = "pbthang" Then
        '    e.Appearance.BackColor = Color.MistyRose
        'End If
        'If e.Column.FieldName = "pbluyke" Then
        '    e.Appearance.BackColor = Color.Salmon
        'End If
        'If e.Column.FieldName = "saupb" Then
        '    e.Appearance.BackColor = Color.Tomato
        'End If
    End Sub

    Private Sub barDeDenNgay_EditValueChanged(sender As Object, e As EventArgs) Handles barDeDenNgay.EditValueChanged
        Dim time As DateTime = barDeDenNgay.EditValue
        barDeTuNgay.EditValue = New DateTime(time.Year, time.Month, 1)
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Alt Or Keys.O) Then
            '   colHongDauKy.Visible = Not colHongDauKy.Visible
            ' colHongTrongKy.Visible = Not colHongTrongKy.Visible
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