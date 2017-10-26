Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors

Public Class frmDiemChuyenMa
    Dim _exit As Boolean = False

    Private Sub frmDiemChuyenMa_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        '    _exit = True
        tbThang.EditValue = New DateTime(tg.Year, tg.Month, 1)
        '    _exit = False

        LoadrcbNV()

        btTaiBaoCao.PerformClick()

    End Sub


    Public Sub LoadrcbNV()
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT ID,Ten FROM NHANSU WHERE Noictac=74 and IDDepatment=2 and trangthai=1 ")
        If Not tb Is Nothing Then
            rcbNhanVien.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btTaiBaoCao_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btTaiBaoCao.ItemClick
        Dim sql As String = ""
        sql &= " SET DATEFORMAT DMY"
        sql &= " DECLARE @Thang int"
        sql &= " DECLARE @Nam int"
        sql &= " DECLARE @TyLeQD as float"
        sql &= " DECLARE @TyLeCM as Float"
        sql &= " SET @Thang= " & Convert.ToDateTime(tbThang.EditValue).Month
        sql &= " SET @Nam = " & Convert.ToDateTime(tbThang.EditValue).Year

        sql &= " SET @TyLeQD= (SELECT TyLeQuyDoiDiem FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"
        sql &= " SET @TyLeCM=(SELECT TyLeChuyenMa FROM tblQuyDoi WHERE Left(ThangNam,2)=@Thang AND Right(ThangNam,4)=Convert(nvarchar,@Nam))"

        sql &= " SELECT YEUCAUDEN.NgayChuyenMa,YEUCAUDEN.SoPhieu,KHACHHANG.ttcMa,VATTU.Model,KT.Ten AS KyThuat,KD.Ten AS KinhDoanh,View_LoiNhuan.Ngay AS NgayThuTien,"
        sql &= " (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))*@TyLeQD*@TyLeCM AS Diem "
        sql &= " FROM YEUCAUDEN "
        sql &= " INNER JOIN BANGYEUCAU ON BANGYEUCAU.SoPhieu=YEUCAUDEN.SoPhieu AND BANGCHAOGIA.CongTrinh=0 "
        sql &= " INNER JOIN NHANSU AS KT ON KT.ID=YEUCAUDEN.IDChuyenMa AND IDDepatment=2"
        sql &= " INNER JOIN NHANSU AS KD ON KD.ID=BANGYEUCAU.IDTakeCare"
        sql &= " INNER JOIN KHACHHANG ON BANGYEUCAU.IDKhachHang=KHACHHANG.ID"
        sql &= " INNER JOIN View_LoiNhuan ON View_LoiNhuan.MaSoDatHang=BANGYEUCAU.SoPhieu AND Month(View_LoiNhuan.Ngay)=@Thang AND Year(View_LoiNhuan.Ngay)=@Nam"
        sql &= " INNER JOIN V_XuatKhoCal ON YEUCAUDEN.IDVatTu=V_XuatKhoCal.IDVattu AND V_XuatKhoCal.SoPhieu = View_LoiNhuan.SoPhieuXK"
        sql &= " INNER JOIN VATTU ON VATTU.ID=YEUCAUDEN.IDVatTu"
        sql &= " WHERE IDChuyenMa<>BANGYEUCAU.IDTakeCare AND (V_XuatKhoCal.GiaBan - ISNULL(V_XuatKhoCal.GiaNhap,V_XuatKhoCal.GiaBan))>0"
        If Not cbNhanVien.EditValue Is Nothing Then
            AddParameterWhere("@IDCM", cbNhanVien.EditValue)
            sql &= " AND IDChuyenMa = @IDCM"
        End If
        sql &= " ORDER BY SoPhieu"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            gdv.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub
End Class