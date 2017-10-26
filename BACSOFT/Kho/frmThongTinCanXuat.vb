Imports BACSOFT.Db.SqlHelper
Public Class frmThongTinCanXuat
    Public _IDVatTu As Object

    Private Sub frmThongTinCanXuat_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not _IDVatTu Is Nothing Then
            Dim sql As String = ""
            sql &= " SELECT BANGCHAOGIA.NgayNhan as NgayThang,KHACHHANG.ttcMa, CHAOGIA.SoLuong, NHANSU.Ten as PhuTrach"
            sql &= " FROM CHAOGIA "
            sql &= " INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.SoPhieu=CHAOGIA.SoPhieu"
            sql &= " INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachHang=KHACHHANG.ID"
            sql &= " INNER JOIN NHANSU ON BANGCHAOGIA.IDTakeCare = NHANSU.ID"
            sql &= " WHERE CHAOGIA.CanXuat <> 0 AND CHAOGIA.IDVatTu=@IDVT"
            sql &= " ORDER BY NgayThang"
            AddParameterWhere("@IDVT", _IDVatTu)
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                gdvChaoGia.DataSource = tb
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub
End Class