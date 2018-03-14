Imports System
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Configuration
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports BACSOFT.Db.SqlHelper
Public Class TAI
    Public Shared isHienThiT As Boolean = False
#Region "#Xe"
    Public Shared Function tableMucDo() As DataTable
        Dim mucdo As DataTable = New DataTable()
        mucdo.Columns.Add("id", GetType(Integer))
        mucdo.Columns.Add("tenmucdo", GetType(String))
        mucdo.Rows.Add(1, "Không quan trọng")
        mucdo.Rows.Add(2, "Bình thường")
        mucdo.Rows.Add(3, "Quan trọng")
        mucdo.Rows.Add(4, "Khẩn cấp")
        Return mucdo
    End Function
    Public Shared Function tableTrangthai() As DataTable
        Dim trangthaixe As New DataTable()
        trangthaixe.Columns.Add("id", GetType(Integer))
        trangthaixe.Columns.Add("tentrangthaixe", GetType(String))
        trangthaixe.Rows.Add(1, "Chưa lấy xe")
        trangthaixe.Rows.Add(2, "Đã lấy xe")
        trangthaixe.Rows.Add(3, "Đã trả xe")
        trangthaixe.Rows.Add(4, "Hủy")
        Return trangthaixe
    End Function
    Public Shared Function tableTinhTrang() As DataTable
        Dim tinhtrangxe As DataTable = New DataTable()
        tinhtrangxe.Columns.Add("id", GetType(Integer))
        tinhtrangxe.Columns.Add("tentinhtrang", GetType(String))
        tinhtrangxe.Rows.Add(1, "Bình thường")
        tinhtrangxe.Rows.Add(2, "Sửa chữa")
        tinhtrangxe.Rows.Add(3, "Bảo dưỡng")
        Return tinhtrangxe
    End Function
    Public Shared Function tableLoaiGiayTo() As DataTable
        Dim giaytoxe As DataTable = New DataTable
        giaytoxe.Columns.Add("id", GetType(Integer))
        giaytoxe.Columns.Add("tenloaigiayto", GetType(String))
        giaytoxe.Rows.Add(1, "Bảo hiểm TNDS")
        giaytoxe.Rows.Add(2, "Đăng kiểm")
        giaytoxe.Rows.Add(3, "Xác nhận của NH")
        Return giaytoxe
    End Function
#End Region
#Region "#trực nhật"
    Public Shared Function tableTrangthaiTN() As DataTable
        Dim TrangThaiTN As New DataTable()
        TrangThaiTN.Columns.Add("id", GetType(Integer))
        TrangThaiTN.Columns.Add("tentrangthai", GetType(String))
        TrangThaiTN.Rows.Add(1, "Chưa trực nhật")
        TrangThaiTN.Rows.Add(2, "Đã trực nhật")
        TrangThaiTN.Rows.Add(3, "Bỏ trực nhật")
        Return TrangThaiTN
    End Function
#End Region
#Region "#Tài sản"
    Public Shared Function tableTinhTrangTS() As DataTable
        Dim TinhTrangTS As DataTable = New DataTable()
        TinhTrangTS.Columns.Add("id", GetType(Integer))
        TinhTrangTS.Columns.Add("tentinhtrang", GetType(String))
        TinhTrangTS.Rows.Add(1, "Đang sử dụng")
        TinhTrangTS.Rows.Add(2, "Chưa sử dụng")
        TinhTrangTS.Rows.Add(3, "Thanh lý")
        'TinhTrangTS.Rows.Add(4, "Thanh lý")
        Return TinhTrangTS
    End Function
    Public Shared Function tableLoaiTS() As DataTable
        Dim LoaiTS As DataTable = New DataTable()
        LoaiTS.Columns.Add("id", GetType(Integer))
        LoaiTS.Columns.Add("tenloaitaisan", GetType(String))
        LoaiTS.Rows.Add(1, "Tài sản chung")
        LoaiTS.Rows.Add(2, "Tài sản cá nhân")
        Return LoaiTS
    End Function
    Public Shared Function tableLoaiCCDC() As DataTable
        Dim LoaiCCDC As DataTable = New DataTable()
        LoaiCCDC.Columns.Add("id", GetType(Integer))
        LoaiCCDC.Columns.Add("tenloaiccdc", GetType(String))
        LoaiCCDC.Rows.Add(1, "Tiêu hao")
        LoaiCCDC.Rows.Add(2, "Không tiêu hao")
        Return LoaiCCDC
    End Function

    Public Shared Function tableTSorCCDC() As DataTable
        Dim TSorCCDC As DataTable = New DataTable()
        TSorCCDC.Columns.Add("id", GetType(Integer))
        TSorCCDC.Columns.Add("ten", GetType(String))
        TSorCCDC.Rows.Add(1, "Tài sản")
        TSorCCDC.Rows.Add(2, "Công cụ, dụng cụ")
        Return TSorCCDC
    End Function
  
#End Region
#Region "#Hải quan"
    Public Shared hienthipopup As Boolean = False
    Public Shared Function tableTinhTrangHaiQuan(ByVal check As Integer) As DataTable
        Dim TinhTrangTS As DataTable = New DataTable()
        TinhTrangTS.Columns.Add("id", GetType(Integer))
        TinhTrangTS.Columns.Add("ten", GetType(String))
        If check = 1 Then
            TinhTrangTS.Rows.Add(0, "Nháp")
            TinhTrangTS.Rows.Add(4, "Đang soạn thảo")
        End If
        TinhTrangTS.Rows.Add(1, "Chờ xử lý")
        TinhTrangTS.Rows.Add(2, "Đang xử lý")
        TinhTrangTS.Rows.Add(3, "Đã xử lý")

        Return TinhTrangTS
    End Function
    Public Shared Function tableLuongHaiQuan() As DataTable
        Dim TinhTrangTS As DataTable = New DataTable()
        TinhTrangTS.Columns.Add("id", GetType(Integer))
        TinhTrangTS.Columns.Add("ten", GetType(String))
        TinhTrangTS.Rows.Add(1, "Xanh")
        TinhTrangTS.Rows.Add(2, "Đỏ")
        TinhTrangTS.Rows.Add(3, "Vàng")
        Return TinhTrangTS
    End Function
#End Region
#Region "#Công nợ"
    Public Shared Function tableNhomHinhThucTT() As DataTable
        Dim NhomHinhThuc As DataTable = New DataTable()
        NhomHinhThuc.Columns.Add("ID", GetType(Integer))
        NhomHinhThuc.Columns.Add("TenNhom", GetType(String))

        NhomHinhThuc.Rows.Add(1, "Thanh toán trước")
        NhomHinhThuc.Rows.Add(2, "Thanh toán trước và sau")
        NhomHinhThuc.Rows.Add(3, "Thanh toán sau khi giao nhận xong đơn hàng")
        NhomHinhThuc.Rows.Add(4, "Thanh toán sau kể từ ngày cuối tháng")
        NhomHinhThuc.Rows.Add(5, "Thanh toán theo lịch cố định")
        Return NhomHinhThuc
    End Function
    Public Shared Function tableHinhThucCT() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("Id", Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("Ten", Type.GetType("System.String")))
        Dim r As DataRow

        r = dt.NewRow
        r("Id") = 1
        r("Ten") = "Không lấy VAT"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("Id") = 2
        r("Ten") = "Lấy VAT"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("Id") = 3
        r("Ten") = "Hải Quan"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        Return dt
    End Function
    Public Shared Sub TinhNgayCongNo(_chaogia As Integer, _xuatkho As Object, _htct As Integer, _ngay As DateTime, Optional _tien As Object = Nothing)
        Dim ngaycongno As New DateTime
        Dim tienno As New Integer
        Dim ngaycongno2 As New DateTime
        Dim tienno2 As New Integer
        AddParameter("@Sophieu", _chaogia)
        Dim dt = ExecuteSQLDataTable("select DM_HINH_THUC_TT.* from DM_HINH_THUC_TT inner join BANGCHAOGIA on IDHinhThucTT2 =DM_HINH_THUC_TT .ID where SoPhieu=@Sophieu")
        If dt Is Nothing Or dt.Rows.Count < 1 Then
            ShowBaoLoi("Chào giá chưa có hình thức thanh toán")
            Exit Sub
        End If
        Dim row = dt.Rows(0)
        If row("Nhom") = 1 Then
            ngaycongno = _ngay
            tienno = _tien * row("TraTruoc2")
        End If
        If row("Nhom") = 2 Then
            ngaycongno = _ngay.AddDays(row("SoNgayHT"))
            tienno = _tien * row("TraSau1")
            If Not IsDBNull(row("SoNgayHT2")) And row("SoNgayHT2") IsNot Nothing Then
                ngaycongno2 = _ngay.AddDays(row("SoNgayHT2"))
                tienno2 = _tien * row("TraSau2")
            End If
        End If
        If row("Nhom") = 3 Then
            ngaycongno = _ngay.AddDays(row("SoNgayHT"))
            tienno = _tien * row("TraSau1")
        End If
        If row("Nhom") = 4 Then
            ngaycongno = New DateTime(_ngay.Year, _ngay.Month + 1, row("SoNgayHT"))
            tienno = _tien * row("TraSau1")
        End If
        If row("Nhom") = 5 Then
            If row("SoNgayHT") = 2 Then
                If _ngay.DayOfWeek > 2 Then
                    ngaycongno = _ngay.AddDays(7 - (row("SoNgayHT") - 2))
                Else
                    ngaycongno = _ngay
                End If
            End If

            If _ngay.Day < row("SoNgayHT") Then
                ngaycongno = New DateTime(_ngay.Year, _ngay.Month, row("SoNgayHT"))
            Else
                ngaycongno = New DateTime(_ngay.Year, _ngay.Month + 1, row("SoNgayHT"))
            End If
            tienno = _tien * row("TraSau1")
        End If

        If Not IsDBNull(_xuatkho) And Not _xuatkho Is Nothing Then
            AddParameterWhere("@SoPhieuXK", _xuatkho)
            Dim sd = ExecuteSQLDataTable("select HTCT from NGAY_CONG_NO where SoPhieuXK=@SoPhieuXK")

            If _tien IsNot Nothing Then
                AddParameter("@TienNo", tienno / 100)
            End If

            If sd.Rows.Count > 0 Then
                If sd.Rows(0).Item("HTCT") < _htct Or (sd.Rows(0).Item("HTCT") = _htct And _htct <> 1) Then
                    AddParameter("@NgayCongNo", ngaycongno)
                    AddParameter("@HTCT", _htct)
                Else
                    If _tien Is Nothing Then
                        Exit Sub
                    End If
                End If
                AddParameterWhere("@Lan", 1)
                AddParameterWhere("@SoPhieuXK", _xuatkho)
                If doUpdate("NGAY_CONG_NO", "SoPhieuXK=@SoPhieuXK and Lan=@Lan") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Else
                AddParameter("@NgayCongNo", ngaycongno)
                AddParameter("@HTCT", _htct)
                AddParameter("@Lan", 1)
                AddParameter("@SoPhieuXK", _xuatkho)
                If doInsert("NGAY_CONG_NO") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If
            If Not IsDBNull(ngaycongno2) And ngaycongno2 <> Nothing Then
                If _tien IsNot Nothing Then
                    AddParameter("@TienNo", tienno2 / 100)
                End If
                If sd.Rows.Count > 0 Then
                    If sd.Rows(0).Item("HTCT") < _htct Or (sd.Rows(0).Item("HTCT") = _htct And _htct <> 1) Then
                        AddParameter("@NgayCongNo", ngaycongno)
                        AddParameter("@HTCT", _htct)
                    Else
                        If _tien Is Nothing Then
                            Exit Sub
                        End If
                    End If
                    AddParameterWhere("@Lan", 2)
                    AddParameterWhere("@SoPhieuXK", _xuatkho)
                    If doUpdate("NGAY_CONG_NO", "SoPhieuXK=@SoPhieuXK and Lan=@Lan") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                Else
                    AddParameter("@NgayCongNo", ngaycongno)
                    AddParameter("@HTCT", _htct)
                    AddParameter("@Lan", 2)
                    AddParameter("@SoPhieuXK", _xuatkho)
                    If doInsert("NGAY_CONG_NO") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            End If
        End If
    End Sub
#End Region
#Region "Công trình"
    Public Shared Function tableCongTrinh() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("Id", Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("Ten", Type.GetType("System.String")))
        Dim r As DataRow

        r = dt.NewRow
        r("Id") = 0
        r("Ten") = "Thương mại"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("Id") = 1
        r("Ten") = "Công trình"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("Id") = 2
        r("Ten") = "Phần mềm"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        Return dt
    End Function
#End Region
#Region "Đặt tồn"
    Public Shared Function tableDatTon() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("ID", Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("Ten", Type.GetType("System.String")))
        Dim r As DataRow

        r = dt.NewRow
        r("Id") = 0
        r("Ten") = "Chờ duyệt"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("Id") = 1
        r("Ten") = "Đã duyệt"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        r = dt.NewRow
        r("Id") = 2
        r("Ten") = "Hủy"
        dt.Rows.InsertAt(r, dt.Rows.Count)

        Return dt
    End Function
#End Region
End Class
