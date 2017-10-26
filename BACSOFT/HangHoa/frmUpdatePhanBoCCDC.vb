Imports BACSOFT.Db.SqlHelper
Imports System.Linq


Public Class frmUpdatePhanBoCCDC

    Public idChungTu As Object

    Private Sub frmUpdatePhanBoCCDC_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        txtNgayVaoSo.EditValue = GetServerTime()

        LoadDsTaiKhoan()


        Dim sql As String = "SELECT convert(int,0)STT, Id,IdVatTu,DienGiai,TaiKhoanNo,TaiKhoanCo,ThanhTien,"
        sql &= "convert(int,0)KyPhanBo,convert(int,0)KyDaPhanBo,convert(float,0)GiaTriBanDau,IdChiTiet,convert(nvarchar,'')SoCT, "
        sql &= "(SELECT MaPhongBan + ' - ' + TenPhongBan FROM PhongBanThue WHERE Id = (SELECT IdPhongBan FROM CHUNGTU WHERE Id = CHUNGTUCHITIET.Id_CT))PhongBan "
        sql &= "FROM CHUNGTUCHITIET WHERE Id = -1"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        gdv.DataSource = dt

        txtDienGiaiChung.Focus()

    End Sub


    Private Sub btnChungTuKhac_Click(sender As System.Object, e As System.EventArgs) Handles btnChungTuKhac.Click

        If Not ShowCauHoi("Tính giá trị chi phí từ ngày " & txtNgayVaoSo.Text & " về trước!") Then Exit Sub

        Dim sql As String = "SELECT COUNT(Id) FROM CHUNGTU WHERE GhiSo = 0 AND LoaiCT = " & ChungTu.LoaiChungTu.GhiTangCCDC & " "
        sql &= "AND Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) <= @NgayCT "
        Dim ngayvs As DateTime = CType(txtNgayVaoSo.EditValue, DateTime)
        AddParameter("@NgayCT", New DateTime(ngayvs.Year, ngayvs.Month, ngayvs.Day))
        Dim dt As DataTable = Nothing
        dt = ExecuteSQLDataTable(sql)
        If dt.Rows(0)(0) > 0 Then
            ShowCanhBao("Vẫn còn " & dt.Rows.Count & " chứng từ ghi tăng TSCD chưa ghi sổ!")
            Exit Sub
        End If

        Dim tg As DateTime = GetServerTime()

        'Lấy bên tồn đầu kỳ trước.
        sql = "SELECT Id,IdVatTu, SoKyPhanBo, (SoKyPhanBo-SoKyConLai+SoKyPhanBoTrongNam)SoKyDaPhanBo, round((GiaTriConLai/SoKyConLai),0)TienPhanBo, "
        sql &= "(select tenhoadon from vattu where id = TONKHOTHUECCDC.IdVatTu)DienGiai, "
        sql &= "(SELECT MaPhongBan + ' - ' + TenPhongBan FROM PhongBanThue WHERE Id = tonkhothueccdc.IdPhongBan)PhongBan "
        sql &= "FROM TONKHOTHUECCDC WHERE SoKyPhanBo > (SoKyPhanBo-SoKyConLai+SoKyPhanBoTrongNam) AND Nam = " & tg.Year
        dt = ExecuteSQLDataTable(sql)

        Dim drx As DataTable = CType(gdv.DataSource, DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim r As DataRow = drx.NewRow
            r("STT") = i + 1
            r("SoCT") = "Đầu kỳ"
            r("IdVatTu") = dt.Rows(i)("IdVatTu")
            r("DienGiai") = dt.Rows(i)("DienGiai")
            r("ThanhTien") = dt.Rows(i)("TienPhanBo")
            r("KyPhanBo") = dt.Rows(i)("SoKyPhanBo")
            r("KyDaPhanBo") = dt.Rows(i)("SoKyDaPhanBo")
            r("GiaTriBanDau") = Math.Round(dt.Rows(i)("SoKyPhanBo") * dt.Rows(i)("TienPhanBo"), 0, MidpointRounding.AwayFromZero)
            r("IdChiTiet") = dt.Rows(i)("Id")
            r("TaiKhoanNo") = "242"
            r("TaiKhoanCo") = "64213"
            r("PhongBan") = dt.Rows(i)("PhongBan")
            drx.Rows.InsertAt(r, drx.Rows.Count)
        Next

        'Lấy bên chứng từ ghi tăng CCDC

        sql = "SELECT a.SoCT,b.IdVatTu,b.DienGiai,b.ThanhTien,b.GiaTriKhac,b.GiaTriKhac2,b.Id, "
        sql &= "(SELECT MaPhongBan + ' - ' + TenPhongBan FROM PhongBanThue WHERE Id = a.IdPhongBan)PhongBan "

        sql &= "FROM CHUNGTUCHITIET b RIGHT OUTER JOIN CHUNGTU a ON b.Id_CT = a.Id "

        sql &= "WHERE a.LoaiCT = @LoaiCT AND b.ButToan = @ButToan AND a.GhiSo = 1 AND Convert(datetime,CONVERT(nvarchar,a.NgayCT,103),103) <= @NgayCT "
        sql &= "AND b.GiaTriKhac > b.GiaTriKhac2 "
        AddParameter("@LoaiCT", ChungTu.LoaiChungTu.GhiTangCCDC)
        AddParameter("@NgayCT", New DateTime(ngayvs.Year, ngayvs.Month, ngayvs.Day))
        AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)
        dt = ExecuteSQLDataTable(sql)
        Dim index As Integer = gdvData.RowCount
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim r As DataRow = drx.NewRow
            r("STT") = i + 1 + index
            r("SoCT") = dt.Rows(i)("SoCT")
            r("IdVatTu") = dt.Rows(i)("IdVatTu")
            r("DienGiai") = dt.Rows(i)("DienGiai")
            r("ThanhTien") = Math.Round(dt.Rows(i)("ThanhTien") / dt.Rows(i)("GiaTriKhac"), 0, MidpointRounding.AwayFromZero)
            r("KyPhanBo") = dt.Rows(i)("GiaTriKhac")
            r("KyDaPhanBo") = dt.Rows(i)("GiaTriKhac2")
            r("GiaTriBanDau") = dt.Rows(i)("ThanhTien")
            r("IdChiTiet") = dt.Rows(i)("Id")
            r("TaiKhoanNo") = "242"
            r("TaiKhoanCo") = "64213"
            r("PhongBan") = dt.Rows(i)("PhongBan")
            drx.Rows.InsertAt(r, drx.Rows.Count)
        Next


    End Sub


    Private Sub LoadDsTaiKhoan()
        Dim sql As String = "SELECT TaiKhoan,TaiKhoanCha,TenGoi FROM TAIKHOANTHUE ORDER BY TaiKhoan "
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        Dim tb2 As DataTable = tb.Copy
        tb2.Rows.Clear()
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = tb.Rows(i)("TenGoi")
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), 1, tb2)
            End If
        Next
        rcmbTaiKhoan.DataSource = tb2
    End Sub

    Private Sub deQuy(ByVal tb As DataTable, ByVal idCha As Object, ByVal level As Object, ByVal tb2 As DataTable)
        For i As Integer = 0 To tb.Rows.Count - 1
            If tb.Rows(i)("TaiKhoanCha") Is DBNull.Value Then Continue For
            If tb.Rows(i)("TaiKhoanCha") = idCha Then
                Dim strTen As String = ""
                For j As Integer = 0 To level - 1
                    strTen &= "-- "
                Next
                strTen = " " & strTen & tb.Rows(i)("TenGoi")
                Dim r As DataRow = tb2.NewRow
                r("TaiKhoan") = tb.Rows(i)("TaiKhoan")
                r("TenGoi") = strTen
                r("TaiKhoanCha") = tb.Rows(i)("TaiKhoanCha")
                tb2.Rows.Add(r)
                deQuy(tb, tb.Rows(i)("TaiKhoan"), level + 1, tb2)
            End If
        Next
    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub

    Private Sub txtNgayVaoSo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtNgayVaoSo.EditValueChanged
        txtDienGiaiChung.Text = "Phân bổ công cụ dụng cụ tháng " & CType(txtNgayVaoSo.EditValue, DateTime).Month
    End Sub


    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click

        If gdvData.RowCount <= 0 Then
            ShowCanhBao("Chưa có nội dung phân bổ!")
            Exit Sub
        End If

        If chkGhiSo.Checked Then
            For i As Integer = 0 To gdvData.RowCount - 1
                If gdvData.GetRowCellValue(i, "TaiKhoanNo") Is DBNull.Value Or _
                    gdvData.GetRowCellValue(i, "TaiKhoanCo") Is DBNull.Value Then
                    ShowCanhBao("Chưa đầy đủ bút toán nên không thể ghi sổ chứng từ này được!")
                    Exit Sub
                End If
            Next
        End If

        If Not ShowCauHoi("Bạn có chắc chắn với thao tác này không?") Then Exit Sub

        Try
            BeginTransaction()


            AddParameter("@NgayCT", txtNgayVaoSo.EditValue)
            AddParameter("@DienGiai", txtDienGiaiChung.EditValue)
            AddParameter("@GhiSo", Convert.ToByte(chkGhiSo.Checked))
            AddParameter("@LoaiCT", ChungTu.LoaiChungTu.PhanBoCCDC)
            Dim tongthanhtien As Double = 0
            For i As Integer = 0 To gdvData.RowCount - 1
                tongthanhtien += gdvData.GetRowCellValue(i, "ThanhTien")
            Next
            AddParameter("@ThanhTien", tongthanhtien)
            AddParameter("@NguoiLap", TaiKhoan)

            idChungTu = doInsert("CHUNGTU")
            If idChungTu Is Nothing Then Throw New Exception(LoiNgoaiLe)

            For i As Integer = 0 To gdvData.RowCount - 1
                AddParameter("@Id_CT", idChungTu)
                AddParameter("@IdVatTu", gdvData.GetRowCellValue(i, "IdVatTu"))
                AddParameter("@DienGiai", gdvData.GetRowCellValue(i, "DienGiai"))
                AddParameter("@ThanhTien", gdvData.GetRowCellValue(i, "ThanhTien"))
                AddParameter("@TaiKhoanNo", gdvData.GetRowCellValue(i, "TaiKhoanNo"))
                AddParameter("@TaiKhoanCo", gdvData.GetRowCellValue(i, "TaiKhoanCo"))

                If gdvData.GetRowCellValue(i, "SoCT").ToString = "Đầu kỳ" Then
                    AddParameter("@GiaTriKhac", DBNull.Value) 'Nếu = null thì idChiTiet là đầu kỳ
                Else
                    AddParameter("@GiaTriKhac", 0) 'Nếu = 0 thì idChiTiet là idChungTu
                End If

                AddParameter("@IdChiTiet", gdvData.GetRowCellValue(i, "IdChiTiet"))
                AddParameter("@ButToan", ChungTu.LoaiButToan.Khac)

                Dim idchitiet As Object = doInsert("CHUNGTUCHITIET")
                If idchitiet Is Nothing Then Throw New Exception(LoiNgoaiLe)

                'Cập nhật lại số lần phân bổ
                If gdvData.GetRowCellValue(i, "SoCT").ToString = "Đầu kỳ" Then
                    AddParameter("@Id", gdvData.GetRowCellValue(i, "IdChiTiet"))
                    If ExecuteSQLNonQuery("UPDATE TONKHOTHUECCDC SET SoKyPhanBoTrongNam = SoKyPhanBoTrongNam + 1 WHERE ID = @Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Else
                    AddParameter("@Id", gdvData.GetRowCellValue(i, "IdChiTiet"))
                    If ExecuteSQLNonQuery("UPDATE CHUNGTUCHITIET SET GiaTriKhac2 = GiaTriKhac2 + 1 WHERE ID = @Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
                
            Next
            ComitTransaction()
            ShowAlert("Lập chứng từ phân bổ chi phí CCDC thành công!")
            Me.Close()
        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub


End Class