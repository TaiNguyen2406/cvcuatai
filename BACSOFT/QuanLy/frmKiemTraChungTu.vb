Imports BACSOFT.Db.SqlHelper

Public Class frmKiemTraChungTu
    Private i = 0
    Private timestick As Integer = 24 * 60 * 60
    Private Sub frmKiemTraChungTu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim tg = GetServerTime()
        Dim _tgtruoc = tg.AddMonths(-1)
        dtTuNgay.EditValue = New Date(2000, _tgtruoc.Month, _tgtruoc.Day)
        dtDenNgay.EditValue = tg.Date
        Timer1.Start()
        Timer1.Interval = 1000
        loaddulieu()
    End Sub

    Public Sub loaddulieu()
        Dim str As String
        Dim dt As New DataSet
        str = " SELECT ID, Ten FROM NHANSU "
        str &= " SET DATEFORMAT DMY DECLARE @tblNhapKho AS TABLE(IDNguoiDat INT, Sophieu nvarchar(15), IDHinhThucCT int, CoCT int) "

        str &= " INSERT INTO @tblNhapKho (IDNguoiDat, Sophieu, IDHinhThucCT ) "
        str &= " Select IDNguoiDat, Sophieu, (SELECT IDHinhThucCT FROM PHIEUDATHANG WHERE Sophieu = PHIEUNHAPKHO.SophieuDH)"
        str &= " From PHIEUNHAPKHO Where Convert(nvarchar(10), Ngaythang, 103) BETWEEN @TuNgay And @DenNgay"
        str &= " DELETE from @tblNhapKho where isnull(IDHinhThucCT, 0) Not in (2,3)"

        ' xu ly voi nhung thang htct = 2

        str &= " Update tbl set CoCT = (select count(Id_CT) from NHAPKHO where Sophieu = tbl.Sophieu) from @tblNhapKho tbl where IDHinhThucCT = 2"
        str &= " Update tbl set CoCT = (select count(idlamhaiquan) from HaiQuan_ChiTietLamHaiQuan ct "
         str &= " inner Join HaiQuan_LamHaiQuan on idlamhaiquan =HaiQuan_LamHaiQuan.id right Join  NHAPKHO  on ct.idchaogia =NHAPKHO .IDDathang "
        str &= " where  NHAPKHO.Sophieu = tbl.Sophieu And NgayThongQuan Is Not null) from @tblNhapKho tbl where IDHinhThucCT = 3"

        str &= " delete from @tblNhapKho where CoCT <> 0 "

        str &= " Select ROW_NUMBER() over (order by sophieu) As STT, * FROM @tblNhapKho"

        ' str &= " Select distinct  IDNguoiDat, (SELECT email FROM NHANSU WHERE ID = IDNguoiDat)Email, count(Sophieu) As SOCT from @tblNhapKho group by IDNguoiDat"

        AddParameterWhere("@TuNgay", dtTuNgay.EditValue)
        AddParameterWhere("@DenNgay", dtDenNgay.EditValue)
        dt = ExecuteSQLDataSet(str)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        gdv.DataSource = dt.Tables(1)
        rcbNhanVien.DataSource = dt.Tables(0)
    End Sub

    Private Sub btnTaiDS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDS.ItemClick
        loaddulieu()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim Now = GetServerTime.ToString("h-mm-ss-tt")
        Dim words As String() = Now.Split("-")
        i = i + 1
        btnTaiDS.Caption = "Tải danh sách (" & timestick - i & ")"
        If words(0) = "7" And words(1) = "30" And words(2) = "00" Then
            If words(3) = "CH" Then
                btnTaiDS.PerformClick()
                Dim str As String
                Dim dt As New DataTable
                str = " SET DATEFORMAT DMY DECLARE @tblNhapKho AS TABLE(IDNguoiDat INT, Sophieu nvarchar(15), IDHinhThucCT int, CoCT int) "

                str &= " INSERT INTO @tblNhapKho (IDNguoiDat, Sophieu, IDHinhThucCT ) "
                str &= " Select IDNguoiDat, Sophieu, (SELECT IDHinhThucCT FROM PHIEUDATHANG WHERE Sophieu = PHIEUNHAPKHO.SophieuDH)"
                str &= " From PHIEUNHAPKHO Where Convert(nvarchar(10), Ngaythang, 103) BETWEEN @TuNgay And @DenNgay"
                str &= " DELETE from @tblNhapKho where isnull(IDHinhThucCT, 0) Not in (2,3)"

                ' xu ly voi nhung thang htct = 2

                str &= " Update tbl set CoCT = (select count(Id_CT) from NHAPKHO where Sophieu = tbl.Sophieu) from @tblNhapKho tbl where IDHinhThucCT = 2"
                str &= " Update tbl set CoCT = (select count(idlamhaiquan) from HaiQuan_ChiTietLamHaiQuan ct "
                str &= " inner Join HaiQuan_LamHaiQuan on idlamhaiquan =HaiQuan_LamHaiQuan.id right Join  NHAPKHO  on ct.idchaogia =NHAPKHO .IDDathang "
                str &= " where  NHAPKHO.Sophieu = tbl.Sophieu And NgayThongQuan Is Not null) from @tblNhapKho tbl where IDHinhThucCT = 3"

                str &= " delete from @tblNhapKho where CoCT <> 0 "

                str &= " Select distinct  IDNguoiDat, (SELECT email FROM NHANSU WHERE ID = IDNguoiDat)Email, count(Sophieu) As SOCT from @tblNhapKho group by IDNguoiDat"

                AddParameterWhere("@TuNgay", dtTuNgay.EditValue)
                AddParameterWhere("@DenNgay", dtDenNgay.EditValue)
                dt = ExecuteSQLDataTable(str)
                If dt Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                    Exit Sub
                End If

                Dim _noidung As String = ""
                For j = 0 To dt.Rows.Count - 1
                    If Not dt.Rows(j)("Email") Is DBNull.Value And Not dt.Rows(j)("Email") Is Nothing Then
                        Try
                            _noidung = "Bạn có " & dt.Rows(j)("SOCT") & " nhập kho chưa đủ chứng từ. "
                            Utils.Email.Send(dt.Rows(j)("Email"), "Hoàn tất thủ tục chứng từ nhập kho", _noidung)
                        Catch ex As Exception
                            ShowBaoLoi("Không thể gửi mail")
                        End Try
                    End If
                Next
            End If
        End If
    End Sub

End Class
