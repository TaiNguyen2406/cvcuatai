Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data.Filtering
Imports System.Security.AccessControl
Imports DevExpress
Imports DevExpress.XtraPrinting

Public Class frmSoTienMat
    Public _Exit As Boolean = False
    Private _ToDay As DateTime = GetServerTime.Date
    Public _LoadStyle As Boolean = False
    Public _ConLai As Double = 0

    Private Sub frmSoTienMat_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        btfilterTuNgay.EditValue = New DateTime(_ToDay.Year, _ToDay.Month, 1)
        btfilterDenNgay.EditValue = _ToDay.Date
        LoadTuDien()
        btTaiLai.PerformClick()
    End Sub

#Region "Load dữ liệu phụ"

    Public Sub LoadTuDien()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM NHANSU WHERE NoiCtac=74 AND TrangThai=1 "
        sql &= " SELECT ID,ttcMa FROM KHACHHANG ORDER BY ttcMa "
        sql &= " SELECT ltrim(rtrim(MaSo))MaSo,Ten FROM TAIKHOAN "
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            rcbTakecare.DataSource = ds.Tables(0)
            rcbMaKH.DataSource = ds.Tables(1)
            rcbSoTK.DataSource = ds.Tables(2)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub


    Private Sub rcbMaKH_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbMaKH.ButtonClick
        If e.Button.Index = 1 Then
            btfilterMaKH.EditValue = Nothing
        End If
    End Sub


#End Region

    Public Sub LoadSoTM()

        ShowWaiting("Đang tải dữ liệu ...")
        Dim sql As String = " SET DATEFORMAT DMY "
        sql &= " SELECT (ISNULL((SELECT SUM(SoTien) FROM THU WHERE THU.NgayThangCT<@TuNgay),0) - ISNULL((SELECT SUM(SoTien) FROM Chi WHERE CHI.NgayThangCT<@TuNgay),0))AS DauKy"
        sql &= "   SELECT STT, tb.ID,NgayThangVS,SoPhieu,NgayThangCT,IDKh,DienGiai,"
        sql &= " 	(CASE THUCHI WHEN 0 THEN tb.SoTien ELSE NULL END)TienThu,(CASE THUCHI WHEN 1 THEN tb.SoTien ELSE NULL END)TienChi,0.0 AS ConLai,"
        sql &= " 	tblTienTe.Ten AS TienTe,NguoiGiaoDich,THUCHI,KHACHHANG.ttcMa,MUCDICHTHUCHI.Ten AS MucDich"

        sql &= " FROM "
        sql &= " (SELECT 0 AS STT, THU.ID,NgayThangVS,(N'TT ' + THU.SoPhieu) AS SoPhieu,NgayThangCT,THU.IDKh,THU.DienGiai,"
        sql &= " 	THU.SoTien,TienTe,MucDich,NguoiNop AS NguoiGiaoDich,Convert(bit,0) AS THUCHI,MaTK"
        sql &= " FROM THU"
        sql &= " UNION ALL "
        sql &= " SELECT 0 AS STT, CHI.ID,NgayThangVS,(N'CT ' + CHI.SoPhieu) AS SoPhieu,NgayThangCT,CHI.IDKh,CHI.DienGiai,"
        sql &= " 	CHI.SoTien,TienTe,MucDich,NguoiNhan AS NguoiGiaoDich,Convert(bit,1) AS THUCHI,MaTK"
        sql &= " FROM CHI)tb"
        sql &= " LEFT JOIN KHACHHANG ON KHACHHANG.ID=tb.IDKh"
        sql &= " INNER JOIN MUCDICHTHUCHI ON MUCDICHTHUCHI.ID=tb.MucDich"
        sql &= " INNER JOIN tblTienTe ON tblTienTe.ID = tb.TienTe"
        sql &= " WHERE CONVERT(datetime,CONVERT(nvarchar,tb.NgayThangCT ,103),103)  BETWEEN @TuNgay AND @DenNgay"
        If Not cbSoTK.EditValue Is Nothing Then
            sql &= " AND ltrim(rtrim(tb.MaTK))='" & cbSoTK.EditValue & "' "
        End If
        sql &= " ORDER BY NgayThangCT,SoPhieu"

        AddParameter("@TuNgay", btfilterTuNgay.EditValue)
        AddParameter("@DenNgay", btfilterDenNgay.EditValue)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        If Not ds Is Nothing Then
            gridBandSoTien.Caption = String.Format("Số tiền đầu kỳ:   {0:N0}", ds.Tables(0).Rows(0)(0))
            If ds.Tables(1).Rows.Count > 0 Then

                With ds.Tables(1)
                    .Rows(0)("STT") = 1
                    Dim _TienThu As Double = 0
                    Dim _TienChi As Double = 0
                    If IsDBNull(.Rows(0)("TienThu")) Then
                        _TienThu = 0
                    Else
                        _TienThu = .Rows(0)("TienThu")
                    End If
                    If IsDBNull(.Rows(0)("TienChi")) Then
                        _TienChi = 0
                    Else
                        _TienChi = .Rows(0)("TienChi")
                    End If
                    .Rows(0)("ConLai") = ds.Tables(0).Rows(0)(0) + _TienThu - _TienChi


                    For i As Integer = 1 To .Rows.Count - 1
                        .Rows(i)("STT") = i + 1
                        If IsDBNull(.Rows(i)("TienThu")) Then
                            _TienThu = 0
                        Else
                            _TienThu = .Rows(i)("TienThu")
                        End If
                        If IsDBNull(.Rows(i)("TienChi")) Then
                            _TienChi = 0
                        Else
                            _TienChi = .Rows(i)("TienChi")
                        End If

                        .Rows(i)("ConLai") = .Rows(i - 1)("ConLai") + _TienThu - _TienChi
                    Next
                    _ConLai = ds.Tables(1).Rows(ds.Tables(1).Rows.Count - 1)("ConLai")
                End With
            Else
                _ConLai = 0
            End If

            gdvSoTienMat.DataSource = ds.Tables(1)

            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private Sub btTaiLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiLai.ItemClick

        LoadSoTM()

    End Sub



    'Private Sub chkRutGon_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles chkRutGon.CheckedChanged
    '    If chkRutGon.Checked = True Then
    '        chkRutGon.Glyph = My.Resources.Checked
    '    Else
    '        chkRutGon.Glyph = My.Resources.UnCheck
    '    End If
    'End Sub

    'Private Sub mXemChaoGia_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemChaoGia.ItemClick
    '    If isShowing Then
    '        ShowCanhBao("Có chào giá đang được mở, phải đóng lại trước khi sử dụng tính năng này")
    '        Exit Sub
    '    End If

    '    If gdvDaXuatCT.FocusedRowHandle < 0 Then Exit Sub

    '    TrangThai.isUpdate = True
    '    fCNChaoGia = New frmCNChaoGia
    '    'fCNChaoGia.chkCongTrinh.Checked = gdvCT.GetFocusedRowCellValue("CongTrinh")
    '    fCNChaoGia.SPChaoGia = gdvDaXuatCT.GetFocusedRowCellValue("SoPhieuCG")
    '    fCNChaoGia.Tag = Me.Parent.Tag
    '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
    '        fCNChaoGia.gdvVTCT.OptionsBehavior.ReadOnly = True
    '        fCNChaoGia.gdvCongTrinhCT.OptionsBehavior.ReadOnly = True
    '        fCNChaoGia.btGhi.Enabled = False
    '        fCNChaoGia.tabChuyenMa.PageVisible = False
    '        fCNChaoGia.btTinhToan.Enabled = False
    '    End If

    '    fCNChaoGia.Show()
    'End Sub

    'Private Sub mXemXuatKho_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXemXuatKho.ItemClick
    '    If gdvDaXuatCT.FocusedRowHandle < 0 Then Exit Sub

    '    TrangThai.isUpdate = True
    '    fCNXuatKho = New frmCNXuatKho
    '    fCNXuatKho.PhieuXK = gdvDaXuatCT.GetFocusedRowCellValue("SoPhieu2")
    '    fCNXuatKho.Tag = Me.Parent.Tag
    '    If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
    '        fCNXuatKho.btCal.Enabled = False
    '        fCNXuatKho.btGhi.Enabled = False
    '        fCNXuatKho.btChuyenXK.Enabled = False
    '        fCNXuatKho.mXuatKho.Enabled = False
    '    End If
    '    fCNXuatKho.ShowDialog()
    'End Sub



    Private Sub gdvSoTienMatCT_CustomSummaryCalculate(sender As System.Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles gdvSoTienMatCT.CustomSummaryCalculate
        If e.IsTotalSummary Then
            If CType(e.Item, DevExpress.XtraGrid.GridColumnSummaryItem).FieldName = "ConLai" Then
                e.TotalValue = _ConLai
            End If
        End If

    End Sub



    Private Sub rcbSoTK_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbSoTK.ButtonClick
        If e.Button.Index = 1 Then
            cbSoTK.EditValue = Nothing
        End If
    End Sub

    Private Sub btfilterDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btfilterDenNgay.EditValueChanged
        btfilterTuNgay.EditValue = New DateTime(Convert.ToDateTime(btfilterDenNgay.EditValue).Year, Convert.ToDateTime(btfilterDenNgay.EditValue).Month, 1)
    End Sub


    Private Sub btnXuatPdf_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnXuatPdf.ItemClick

        'Dim ps As New PrintingSystem()
        'Dim Link As New PrintableComponentLink(ps)

        Try

            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Title = "Export to PDF"
            saveFileDialog1.Filter = "PDF File|*.pdf"
            saveFileDialog1.FileName = "SoQuyTienMat_" & Convert.ToDateTime(btfilterTuNgay.EditValue).ToString("ddMMyyyy") & "_" & Convert.ToDateTime(btfilterDenNgay.EditValue).ToString("ddMMyyyy")

            gdvSoTienMatCT.ViewCaption = "Sổ Quỹ Tiền Mặt"


            saveFileDialog1.ShowDialog()
            If saveFileDialog1.FileName.Trim = "" Then Exit Sub

            Dim ps As New DevExpress.XtraPrinting.PrintingSystemBase()
            Dim link = New DevExpress.XtraPrintingLinks.PrintableComponentLinkBase(ps)
            AddHandler link.CreateMarginalFooterArea, AddressOf printableComponentLink1_CreateMarginalHeaderArea
            AddHandler link.CreateReportHeaderArea, AddressOf printableComponentLink1_CreateReportHeaderArea
            link.Component = gdvSoTienMat
            link.Landscape = True
            link.PaperKind = Printing.PaperKind.A4
            link.Margins.Bottom = 45
            link.Margins.Top = 0
            link.Margins.Left = 0
            link.Margins.Right = 0
            link.CreateDocument()
            link.PrintingSystemBase.ExportToPdf(saveFileDialog1.FileName)
      


            If Not ShowCauHoi("Bạn có muốn mở file vừa kết xuất không?") Then Exit Sub
            Process.Start(saveFileDialog1.FileName)

        Catch ex As Exception

        End Try




    End Sub

    Public Sub printableComponentLink1_CreateMarginalHeaderArea(sender As Object, e As CreateAreaEventArgs)
        Dim pib = New PageInfoBrick()
        pib.PageInfo = PageInfo.NumberOfTotal
        pib.Rect = New System.Drawing.RectangleF(0, 0, 300, 25)
        pib.BorderColor = Color.White
        pib.Format = "Trang số {0} / {1}."
        e.Graph.DrawBrick(pib)
    End Sub

    Public Sub printableComponentLink1_CreateReportHeaderArea(sender As Object, e As CreateAreaEventArgs)
        Dim pib = New PageInfoBrick()
        pib.PageInfo = PageInfo.NumberOfTotal
        pib.Rect = New System.Drawing.RectangleF(0, 0, 500, 45)
        pib.BorderColor = Color.White
        pib.Font = New Font(Me.Font.FontFamily, 14, FontStyle.Bold)
        pib.Format = "Sổ Quỹ Tiền Mặt (" & btfilterTuNgay.EditValue & " - " & btfilterDenNgay.EditValue & ")"
        e.Graph.DrawBrick(pib)
    End Sub



End Class
