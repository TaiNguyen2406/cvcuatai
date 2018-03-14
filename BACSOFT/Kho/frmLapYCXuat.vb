Imports BACSOFT.Db.SqlHelper

Public Class frmLapYCXuat
    Public arraySophieu As New ArrayList
    Public arrayIDVattu As String
    Public maKh As String
    Public takecare As String
    Public idtakecare As String
    Public idkhachhang As String
    Private _hanmuc As Double = 0
    Public _updateFlag As Boolean = false
    Public noidungyc As String
    '  Public formDanhsachYeucauXuat As frmDanhsachYeucauXuat
    Public thoigianYC As string
    Private idtoDelete = "" ' Chuỗi id để xoá khi bấm nút xoá

    Private Sub frmLapYCXuat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtMakh.EditValue = maKh
        txtTGLap.EditValue = GetServerTime()
        txtNDYC.Text = noidungyc
        If String.IsNullOrEmpty(thoigianYC) = False Then
            txtTGYCX.EditValue = Date.Parse(thoigianYC)
        End If

        ShowWaiting("Đang tải dữ liệu ...")
        If arrayIDVattu <> "" Then
            arrayIDVattu = arrayIDVattu & "0" ' Để khắc phục chuỗi "a;"
        End If
        LoadData(arrayIDVattu)
        DefaultThongtin()
        CloseWaiting()
    End Sub

     Private Sub CalData()
        Dim sql As String = txtSQL_PhaiThu.Text
        sql = sql.Replace("31/12/2017", GetServerTime().ToString("dd/MM/yyyy"))
        
        Dim _strDK As String = ""
        If String.IsNullOrEmpty(txtMakh.Text) = false Then
            _strDK &= " AND phieuxuatkho.IDkhachhang=" & txtMakh.Text & " "
        End If

        sql = sql.Replace(" AND 1 = 1 ", _strDK)

        ExecuteSQLNonQuery(sql)
    End Sub

    ' Hiển thị thông tin takecare và tổng nợ
    Private sub DefaultThongtin()
        txtTakecare.Text = takecare

        Dim sqlDefault = "Select distinct top 1 convert(datetime,Thoigianlap, 103), convert(datetime,thoigianYC,103), NoidungYC from tblLapYCX where MaKH = " & idkhachhang
        Dim tbDefault As DataTable = ExecuteSQLDataTable(sqlDefault)
        If tbDefault IsNot Nothing Then
            If tbDefault.Rows.Count > 0 Then
                If tbDefault.Rows(0)(0) IsNot DBNull.Value Then
                     txtTGLap.EditValue = Date.Parse(tbDefault.Rows(0)(0).ToString())
                End If
                If tbDefault.Rows(0)(1) IsNot DBNull.Value Then
                     txtTGYCX.EditValue = Date.Parse(tbDefault.Rows(0)(1).ToString())
                End If
                If tbDefault.Rows(0)(2) IsNot DBNull.Value Then
                    txtNDYC.Text = tbDefault.Rows(0)(2).ToString()
                End If
            End If
        End If

        CalData()
        Dim sqlNoquahan = "Select isnull(sum(ConNo),0) from tblTempKQ where NgayQH > 0 and IdKH = " & idkhachhang
        Dim sqlNo = "Select isnull(sum(ConNo),0) from tblTempKQ where IdKH = " & idkhachhang

        Dim tbNoquahan As DataTable = ExecuteSQLDataTable(sqlNoquahan)
        Dim tbNo As DataTable = ExecuteSQLDataTable(sqlNo)
        Dim noqh = 0, no = 0, hanmuc = 0, tongtien = 0
        If tbNoquahan IsNot Nothing Then
            If tbNoquahan.Rows.Count > 0 Then
                If tbNoquahan.Rows(0)(0) IsNot DBNull.Value Then
                    noqh = Double.Parse(tbNoquahan.Rows(0)(0))
                End If
            End If
        End If
        If tbNo IsNot Nothing Then
            If tbNo.Rows.Count > 0 Then
                If tbNo.Rows(0)(0) IsNot DBNull.Value Then
                    no = Double.Parse(tbNo.Rows(0)(0))
                End If
            End If
        End If

        txtNoqh.Text = noqh.ToString()
        txtTongno.Text = no.ToString()

        ' Tính hạn mức
        Dim sqlqh = "select top 1 isnull(Hanmuc,0) from tblQuanlyHanmuc where IdKhachhang = " & idkhachhang & " and "
        sqlqh = sqlqh & "convert(datetime,convert(varchar(15), Ngaycapnhat, 103),103) <= convert(datetime, convert(varchar(15),getdate(),103), 103) order by convert(datetime,convert(varchar(15), Ngaycapnhat, 103),103) desc"
        Dim tbqh As DataTable = ExecuteSQLDataTable(sqlqh)
        If tbqh IsNot Nothing Then
            If tbqh.Rows.Count > 0 Then
                If tbqh.Rows(0)(0) IsNot DBNull.Value Then
                    _hanmuc = Double.Parse(tbqh.Rows(0)(0))
                End If
            End If
        End If

    End sub

    Private Sub LoadData(Idvattu As String)
        Dim sql = ""
        sql = "SELECT CHAOGIA.Sophieu, CHAOGIA.Sophieu Sophieu_Hide, 0 as Stt, "
        sql = sql & " CHAOGIA.IDvattu AS ID,"
        sql = sql & " VATTU.Model,"
        sql = sql & " TENVATTU.Ten AS TenVT, "
        sql = sql & " TENHANGSANXUAT.Ten AS TenHang,"
        sql = sql & " ISNULL(CHAOGIA.Dongia,0) DonGia,"
        sql = sql & " Soluong, "
        sql = sql & " CanXuat AS SLCanXuat, "
        sql = sql & " (Soluong - CanXuat) AS SLDaXuat, "
        sql = sql & " isnull((Select top 1 SLYCXuat from tblLapYCX where IdVattu = CHAOGIA.IDvattu),0) as SLYCXuat,"

        sql = sql & " isnull((select SUM(SlXuatKho) from xuatkhotam where IdVatTu = CHAOGIA.IDVatTu),0) - isnull((select SUM(SlNhapKho) from nhapkhotam where IdVatTu = CHAOGIA.IDVatTu),0) - isnull((select SUM(SoLuong) from XUATKHO  where IdVatTu = CHAOGIA.IDVatTu AND (select SophieuCG from PHIEUXUATKHO where PHIEUXUATKHO.Sophieu=XUATKHO.Sophieu) in (SELECT distinct SoCG FROM xuatkhotam where IdVatTu = CHAOGIA.IDVatTu and SlXuatKho > 0)),0) as XuatTam,"
        
        ' sql = sql & " ((select isnull(SUM(Soluong),0) from NHAPKHO where IDVattu=CHAOGIA.IDVattu)-(select isnull(SUM(Soluong),0) from XUATKHO where IDVattu=CHAOGIA.IDVattu)) AS slTon,"
        sql = sql & " (Select top 1 isnull(round(V_TonkhoTonghop.Ton,2),0) from V_TonkhoTonghop where IdVatTu = CHAOGIA.IDVatTu) AS slTon,"

        sql = sql & " cast ((CHAOGIA.Dongia * 1) as float) AS ThanhTien_Yeucauxuat,"
        sql = sql & " 0 As Tongtien,"
        sql = sql & " (Select top 1 Phanhoi from tblLapYCX where IdVattu = CHAOGIA.IDvattu) as Phanhoi,"
        sql = sql & " (Select top 1 Duyet from tblLapYCX where IdVattu = CHAOGIA.IDvattu) as Duyet,"
        sql = sql & " (Select top 1 DungXK from tblLapYCX where IdVattu = CHAOGIA.IDvattu) as DungXK,"

        sql = sql & " (Select top 1 Ghichu from tblLapYCX where IdVattu = CHAOGIA.IDvattu) as Ghichu"
        sql = sql & " FROM CHAOGIA LEFT OUTER JOIN VATTU ON CHAOGIA.IDvattu=VATTU.ID "
        sql = sql & " LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID "
        sql = sql & " LEFT OUTER JOIN TENHANGSANXUAT ON VATTU.IDHangSanxuat=TENHANGSANXUAT.ID"
        sql = sql & " Where CHAOGIA.IDvattu in (" & Idvattu & ")"
        
        Dim tb As DataTable = ExecuteSQLDataTable(sql)

        Dim tblTemp = tb.Clone()
        Dim tempRow As DataRow
        Dim stt = 1, tongtien = 0
        Dim ghichu = ""
        Dim sophieu_hide = ""
        For Each item As String In arraySophieu
            tempRow = tblTemp.NewRow()
            tempRow("Sophieu") = item
            Dim tblRows0() As DataRow
            tblRows0 = tb.Select("Sophieu = '" & item & "'")
            For Each itemSub As DataRow In tblRows0
                If itemSub("ThanhTien_Yeucauxuat") IsNot DBNull.Value Then
                    tongtien = tongtien + Double.Parse(itemSub("ThanhTien_Yeucauxuat"))
                End If
                If itemSub("Ghichu") IsNot DBNull.Value Then
                    ghichu = itemSub("Ghichu")
                End If
                If itemSub("Sophieu_Hide") IsNot DBNull.Value Then
                    sophieu_hide = itemSub("Sophieu_Hide")
                End If
            Next
            tempRow("Ghichu") = ghichu
            tempRow("Tongtien") = tongtien
            tempRow("Sophieu_Hide") = sophieu_hide
            tblTemp.Rows.Add(tempRow)

            ' Thêm dòng mới
            Dim tblRows() As DataRow
            tblRows = tb.Select("Sophieu = '" & item & "'")
            For Each itemSub As DataRow In tblRows
                tempRow = tblTemp.NewRow()
                itemSub("Stt") = stt
                itemSub("Sophieu") = ""
                itemSub("Ghichu") = ""
                itemSub("Tongtien") = DBNull.Value
                tempRow = itemSub
                tblTemp.ImportRow(tempRow)
                stt = stt + 1
            Next
            stt = 1
            tongtien = 0
        Next

        grdLapYCX.DataSource = tblTemp
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        If grdLapYCXView.FocusedRowHandle < 0 Then Exit Sub
        grdLapYCXView.DeleteSelectedRows()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Close()
    End Sub

    Private Sub btnGhilai_Click(sender As Object, e As EventArgs) Handles btnGhilai.Click
        Dim tongtien_Chaogia_truoc = 0
        Dim noqh = 0, no = 0, hanmuc = 0, tongtien = 0

        hanmuc = _hanmuc

        If String.IsNullOrEmpty(txtNoqh.Text) = False Then
            noqh = Double.Parse(txtNoqh.Text)
        End If
        If String.IsNullOrEmpty(txtTongno.Text) = False Then
            no = Double.Parse(txtTongno.Text)
        End If

        Dim runner = 0
        Dim arrTongtien As New ArrayList

        ' Lưu trước giá trị tổng tiền
        For i = 0 To grdLapYCXView.DataRowCount - 1
            If grdLapYCXView.GetRowCellValue(i, "Stt") Is DBNull.Value Then
                arrTongtien.Add(grdLapYCXView.GetRowCellValue(i, "Tongtien"))

                ' Xoá trước khi thêm
               
                If String.IsNullOrEmpty(idtoDelete) = False Then
                    idtoDelete = idtoDelete & "0"
                    ExecuteSQLNonQuery("Delete from tblLapYCX where SoCG = '" & grdLapYCXView.GetRowCellValue(i, "Sophieu_Hide") & "' and IdVattu in (" & idtoDelete & ")")
                End If
            End If
        Next

        Dim tongNoTruocdo = 0
        tongNoTruocdo = no

        ' Bắt đầu phiên
        BeginTransaction()

        Dim tempRunner = 0
        Dim ghichu = ""

        For i = 0 To grdLapYCXView.DataRowCount - 1
            If grdLapYCXView.GetRowCellValue(runner, "Stt") Is DBNull.Value Then
                 runner = runner + 1
            End If

            If grdLapYCXView.GetRowCellValue(i, "Stt") Is DBNull.Value Then
                 If grdLapYCXView.GetRowCellValue(i, "Ghichu") IsNot DBNull.Value And grdLapYCXView.GetRowCellValue(i, "Ghichu") IsNot Nothing Then
                    ghichu = grdLapYCXView.GetRowCellValue(i, "Ghichu")
                 Else
                    ghichu = ""
                 End If
            End If
            
            If grdLapYCXView.GetRowCellValue(i, "Stt") IsNot DBNull.Value Then

                ' Xoá trước khi thêm
                ExecuteSQLNonQuery("Delete from tblLapYCX where SoCG = '" & grdLapYCXView.GetRowCellValue(i, "Sophieu_Hide") & "' and IdVattu = " & grdLapYCXView.GetRowCellValue(i, "ID"))

                ' Thêm bản ghi
                AddParameter("@MaKH", idkhachhang)
                AddParameter("@Thoigianlap", txtTGLap.EditValue)
                AddParameter("@ThoigianYC", txtTGYCX.EditValue)
                AddParameter("@NoidungYC", txtNDYC.EditValue)
                AddParameter("@SoCG", grdLapYCXView.GetRowCellValue(i, "Sophieu_Hide"))
                AddParameter("@IdVattu", grdLapYCXView.GetRowCellValue(i, "ID"))
                AddParameter("@SLYCXuat", grdLapYCXView.GetRowCellValue(i, "SLYCXuat"))
                AddParameter("@Phanhoi", grdLapYCXView.GetRowCellValue(i, "Phanhoi"))
                AddParameter("@Duyet", grdLapYCXView.GetRowCellValue(i, "Duyet"))

                If _updateFlag Then
                    AddParameter("@DungXK", grdLapYCXView.GetRowCellValue(i, "DungXK"))
                End If

                AddParameter("@Takecare", txtTakecare.EditValue)
                AddParameter("@IdTakecare", idtakecare)
                'AddParameter("@Tongtien", grdLapYCXView.GetRowCellValue(i, "Tongtien"))
                AddParameter("@Ghichu", ghichu)

                ' Mỗi lần Lưu xong thì kiểm tra
                ' Nếu tổng nợ quá hạn > 50k thì toàn bộ bản ghi sẽ có trạng thái dừng xuất kho
                ' Nếu tổng nợ > hạn mức thì toàn bộ bản ghi sẽ có trạng thái dừng xuất kho
                ' Nếu tổng nợ + tổng tiền từng chào giá <= hạn mức thì chào giá được xuất
                ' Nếu chào giá 1 được xuất thì tính tổng nợ + tổng tiền chào giá 2 > hạn mức thì chào giá 2 không được xuất

                If runner > 0 Then
                    tempRunner = runner - 1
                End If

                If _updateFlag = False Then
                    If noqh > 50000 Or no > hanmuc Then
                        AddParameter("@DungXK", 1)
                    Else
                        If no > 0 Then
                            If (tongNoTruocdo + Double.Parse(arrTongtien(tempRunner))) > hanmuc Then
                                AddParameter("@DungXK", 1)
                            Else
                                tongNoTruocdo = tongNoTruocdo + Double.Parse(arrTongtien(tempRunner))
                            End If
                        End If
                    End If
                End If
                
                'If noqh > 50000 Then
                '    AddParameter("@DungXK", 1)
                'End If
                
                'If _updateFlag = False Then
                '    If (no + Double.Parse(arrTongtien(tempRunner))) > hanmuc Or noqh > 50000 Or no > hanmuc Then
                '        AddParameter("@DungXK", 1)
                '    End If
                'End If
                
                If doInsert("tblLapYCX") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit For
                    Exit Sub
                End If
            End If
        Next
       
        ComitTransaction()
        ShowAlert("Đã cập nhật thành công!")

        If _updateFlag Then
            ' formDanhsachYeucauXuat.RefreshDS2()
        End If
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        If grdLapYCXView.RowCount = 0 Then Exit Sub
        If grdLapYCXView.FocusedRowHandle < 0 Then Exit Sub
        If grdLapYCXView.GetFocusedRowCellValue("Stt") IsNot DBNull.Value Then
            
            If ShowCauHoi("Bạn có chắc chắn xoá VT không?") Then
                ' Trừ tiền
                For i = 0 To grdLapYCXView.DataRowCount - 1
                    If grdLapYCXView.GetRowCellValue(i, "Sophieu") = grdLapYCXView.GetFocusedRowCellValue("Sophieu_Hide") Then
                        grdLapYCXView.SetRowCellValue(i,"Tongtien", Double.Parse(grdLapYCXView.GetRowCellValue(i,"Tongtien")) - Double.Parse(grdLapYCXView.GetFocusedRowCellValue("ThanhTien_Yeucauxuat")))
                    End If
                Next
                idtoDelete = idtoDelete & grdLapYCXView.GetFocusedRowCellValue("ID") & ","
                grdLapYCXView.DeleteSelectedRows()

                grdLapYCXView.PostEditor()
            End If
        Else
            ShowCanhBao("Chọn vật tư/hàng hoá.")
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If _updateFlag Then
            Close()
                    deskTop.OpenTab("Hàng cần xuất", "frmCanXuat", New frmCanXuat, True, Nothing, deskTop.mHangCanXuat.Name)
        Else
                    Close()
        End If
    End Sub

    Private Sub grdLapYCXView_CellValueChanging_1(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grdLapYCXView.CellValueChanging
        Dim strDongia As Double = 0
        Dim strSLYCX As Double = 0
        Dim thanhtien As Double = 0
        Dim slcg = 0, sldx = 0
        Dim ton = 0, xuattam = 0

        If e.Column.FieldName = "SLYCXuat" Then
            If Not grdLapYCXView.GetFocusedRowCellValue("Soluong") Is Nothing And grdLapYCXView.GetFocusedRowCellValue("Soluong") IsNot DBNull.Value Then
                slcg = grdLapYCXView.GetFocusedRowCellValue("Soluong")
            End If
            If Not grdLapYCXView.GetFocusedRowCellValue("SLDaXuat") Is Nothing And grdLapYCXView.GetFocusedRowCellValue("SLDaXuat") IsNot DBNull.Value Then
                sldx = grdLapYCXView.GetFocusedRowCellValue("SLDaXuat")
            End If
            If Not grdLapYCXView.GetFocusedRowCellValue("XuatTam") Is Nothing And grdLapYCXView.GetFocusedRowCellValue("XuatTam") IsNot DBNull.Value Then
                xuattam = grdLapYCXView.GetFocusedRowCellValue("XuatTam")
            End If
            If Not grdLapYCXView.GetFocusedRowCellValue("slTon") Is Nothing And grdLapYCXView.GetFocusedRowCellValue("slTon") IsNot DBNull.Value Then
                ton = grdLapYCXView.GetFocusedRowCellValue("slTon")
            End If

            If IsNumeric(e.Value) = False Then
                grdLapYCXView.SetRowCellValue(e.RowHandle, "SLYCXuat", 0)
                Exit Sub
            End If

            If e.Value <= (slcg - sldx) And e.Value <= (ton - xuattam) Then
                grdLapYCXView.SetRowCellValue(e.RowHandle, "SLYCXuat", e.Value)

                If Not grdLapYCXView.GetFocusedRowCellValue("DonGia") Is Nothing And grdLapYCXView.GetFocusedRowCellValue("DonGia") IsNot DBNull.Value Then
                    strDongia = grdLapYCXView.GetFocusedRowCellValue("DonGia")
                End If

                If Not grdLapYCXView.GetFocusedRowCellValue("SLYCXuat") Is Nothing Then
                    strSLYCX = grdLapYCXView.GetFocusedRowCellValue("SLYCXuat")
                End If
                thanhtien = strDongia * strSLYCX
                grdLapYCXView.SetRowCellValue(e.RowHandle, "ThanhTien_Yeucauxuat", thanhtien)
                grdLapYCXView.UpdateCurrentRow()
            Else
                ShowCanhBao("SLYCX không hợp lệ.")
                grdLapYCXView.SetRowCellValue(e.RowHandle, "SLYCXuat", 0)
            End If
        End If
    End Sub

    Private Sub grdLapYCXView_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles grdLapYCXView.CustomDrawCell
        If grdLapYCXView.GetRowCellValue(e.RowHandle, "Stt") IsNot Nothing And grdLapYCXView.GetRowCellValue(e.RowHandle, "Stt") IsNot DBNull.Value Then
            If e.Column.FieldName = "Ghichu" Then
                e.Appearance.BackColor = Color.FromArgb(242, 242, 242)
            End If
        End If
        ' else
        If e.Column.FieldName = "SLYCXuat" Then
            e.Appearance.BackColor = Color.FromArgb(170, 252, 252)
        End If
         ' end if
    End Sub

    Private Sub grdLapYCXView_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles grdLapYCXView.ShowingEditor
        If grdLapYCXView.GetRowCellValue(grdLapYCXView.FocusedRowHandle, "Stt") IsNot Nothing And grdLapYCXView.GetRowCellValue(grdLapYCXView.FocusedRowHandle, "Stt") IsNot DBNull.Value Then
            if grdLapYCXView.FocusedColumn.FieldName = "Ghichu" Then
                e.Cancel = true
            End If
        Else
            if grdLapYCXView.FocusedColumn.FieldName = "SLYCXuat" Then
                e.Cancel = true
            End If
        End If
    End Sub
End Class