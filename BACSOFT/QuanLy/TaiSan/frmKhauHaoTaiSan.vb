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
        '   query = "select TaiSan_TaiSan.*, PHIEUXUATKHO.NgayThang, SoLuong, DonGia, SoLuong*DonGia as tongtien,TENVATTU.Ten AS TenVT, thoigiankh "
        If barSeNam.EditValue IsNot Nothing And barCbbThang.EditValue IsNot Nothing Then
            Dim thoigian As String = barSeNam.EditValue.ToString() & "/" & barCbbThang.EditValue.ToString()
            Dim time As String = ""
            Dim time2 As String = ""
            Dim thang = barCbbThang.EditValue.ToString()
            Dim nam = barSeNam.EditValue.ToString()
            Dim ngay = System.DateTime.DaysInMonth(nam, thang)
            Dim thang2 = thang - 1
            Dim nam2 = nam
            If thang = 1 Then
                thang2 = 12
                nam2 = nam - 1
            End If
            Dim ngay2 = System.DateTime.DaysInMonth(nam2, thang2)
            time = nam.ToString() + "/" + thang.ToString + "/" + ngay.ToString()
            time2 = nam.ToString() + "/" + thang.ToString + "/" + "1" ' ngay2.ToString()
            query = meKhauHaoTS.EditValue
            query = query.Replace("2013/8/31", time)
            query = query.Replace("2013/7/31", time2)
            If chkXemHet.Checked Then
                query &= " where SoNgayKH<datediff(day, NgayThang,@time)"
            Else
                query &= " where SoNgayKH>=datediff(day, NgayThang,@time)"
            End If
            'query &= ", SoLuong-(select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id) as SoLuongThuc , (SoLuong-(select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id))*DonGia/(thoigiankh*365) as muckh , datediff(day, NgayThang,@time) as thoigiansudung, (SoLuong-(select COUNT(id) from Taisan_ChiTietTaiSan where  ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id))*DonGia/(thoigiankh*365)*  datediff(day, NgayThang,@time)+ ((select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id))*DonGia as khluyke"
            'query &= ", case when datediff(day, NgayThang,@time2)>0 then (SoLuong-(select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id))*DonGia/(thoigiankh*365)*  datediff(day, @time2,@time) else (SoLuong-(select COUNT(id) from Taisan_ChiTietTaiSan where  ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id))*DonGia/(thoigiankh*365)*  datediff(day, NgayThang,@time) end +((select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly >=@time2 and ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id))*DonGia/(thoigiankh*365)* (thoigiankh*365- datediff(day, @time2,@time))  as khthang"
            'query &= ", SoLuong*DonGia- ((SoLuong-(select COUNT(id) from Taisan_ChiTietTaiSan where ngaythanhly <=@time and idtaisan =TaiSan_TaiSan .id))*DonGia/(thoigiankh*365)*  datediff(day, NgayThang,@time))as saukh"
            'query &= ", CONVERT(varchar(50), thoigiankh*365- datediff(day, NgayThang,@time))+N' ngày =' +CONVERT(varchar(50),(thoigiankh*365- datediff(day, NgayThang,@time) )/30 )+N' tháng '+ case when (thoigiankh*365- datediff(day, NgayThang,@time) )%30>0 then CONVERT(varchar(50),(thoigiankh*365- datediff(day, NgayThang,@time) )%30 )+N' ngày' else '' end as thoigianconkh  "

            'query &= "from TaiSan_TaiSan inner join XUATKHO on TaiSan_TaiSan.idxuatkho=XUATKHO.ID INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID ,Taisan_DinhMuc where 1=1  and  DonGia>=mucdau and DonGia<=muccuoi and ngayapdung=(select top 1 ngayapdung from Taisan_DinhMuc  where Dongia>=mucdau and Dongia<=muccuoi and ngayapdung<=@time  and TSorCCDC=1  order by ngayapdung desc) and datediff(day, NgayThang,@time)<=thoigiankh *365 and datediff(day, NgayThang,@time)>=0 and TSorCCDC=1"
            'If barLueNhomVT.EditValue IsNot Nothing Then
            '    AddParameterWhere("@IDTennhom", barLueNhomVT.EditValue)
            '    query &= " and IDTennhom=@IDTennhom"
            'End If
            'If barLueHang.EditValue IsNot Nothing Then
            '    AddParameterWhere("@IDHangSanxuat", barLueHang.EditValue)
            '    query &= " and IDHangSanxuat=@IDHangSanxuat"
            'End If
            'If barLueTenVT.EditValue IsNot Nothing Then
            '    AddParameterWhere("@IDTenvattu", barLueTenVT.EditValue)
            '    query &= " and IDTenvattu=@IDTenvattu"
            'End If
            'If barTxtMaVT.EditValue IsNot Nothing Then
            '    query &= " and Model Like N'%" & barTxtMaVT.EditValue.ToString & "%' "
            'End If
            'query &= " order by NgayThang desc"
            'AddParameter("@time", time)
            'AddParameter("@time2", time2)

            Dim dt As DataTable = ExecuteSQLDataTable(query)
            If Not dt Is Nothing Then
                gcKhauHaoTs.DataSource = dt
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
       
    End Sub

    Private Sub loadData()
        'riLueNhomVT.DataSource = ExecuteSQLDataTable("select * from TENNHOM ORDER BY Ten ASC")
        'riLueHang.DataSource = ExecuteSQLDataTable("select * from TENHANGSANXUAT ORDER BY Ten ASC")
        'riLueTenVT.DataSource = ExecuteSQLDataTable("select * from TENVATTU ORDER BY Ten ASC")
        barSeNam.EditValue = BAC.GetServerTime().Year
        riCbbThang.Items.Clear()
        For i = 1 To 12
            If i < 10 Then
                riCbbThang.Items.Add("0" + i.ToString())
            Else
                riCbbThang.Items.Add(i.ToString())
            End If

        Next i
        barCbbThang.EditValue = Today.Month
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
        Dim gvexprort As DevExpress.XtraGrid.Views.Grid.GridView = gvKhauHaoTs

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
                    gvKhauHaoTs.ExportToXls(exportFilePath)
                Case (".xlsx")
                    gvKhauHaoTs.ExportToXlsx(exportFilePath)
                Case ".rtf"
                    gvKhauHaoTs.ExportToRtf(exportFilePath)
                Case ".pdf"
                    gvKhauHaoTs.ExportToPdf(exportFilePath)
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
            gvKhauHaoTs.OptionsView.ShowAutoFilterRow = True
        Else
            gvKhauHaoTs.OptionsView.ShowAutoFilterRow = False
        End If
    End Sub

    Private Sub BarButtonItem4_ItemClick_1(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
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

    Private Sub gvKhauHaoTs_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gvKhauHaoTs.HiddenEditor
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub chkXemHet_CheckedChanged(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkXemHet.CheckedChanged
        If chkXemHet.Checked Then
            chkXemHet.Glyph = My.Resources.Checked
        Else
            chkXemHet.Glyph = My.Resources.UnCheck
        End If
    End Sub
    Private Sub gvPBCongCuDungCu_CustomDrawCell(sender As Object, e As Base.RowCellCustomDrawEventArgs) Handles gvKhauHaoTs.CustomDrawCell
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
End Class