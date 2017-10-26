Imports BACSOFT.Db.SqlHelper
Public Class rptTongHopCongNoPhaiThuPhaiTra

    Public TuNgay As DateTime
    Public DenNgay As DateTime
    Public TenBaoCao As TieuChiThoiGian
    Public LoaiBaoCao As frmTieuChiBaoCaoCongNoThue.LoaiBaoCaoCongNo



    Private Sub XrTableCell8_PreviewDoubleClick(sender As System.Object, e As DevExpress.XtraReports.UI.PreviewMouseEventArgs) Handles XrTableCell8.PreviewDoubleClick

   

        Dim IdKH As Object = ExecuteSQLDataTable("SELECT ID FROM KHACHHANG WHERE ttcMa = N'" & e.Brick.TextValue & "'").Rows(0)(0)

        Dim f As New frmTieuChiBaoCaoCongNoThue(LoaiBaoCao)
       
        f.cmbTieuChi.SelectedItem = TenBaoCao
        f.txtTuNgay.EditValue = TuNgay
        f.txtDenNgay.EditValue = DenNgay
        f.cmbDoiTuong.EditValue = IdKH
        If LoaiBaoCao = frmTieuChiBaoCaoCongNoThue.LoaiBaoCaoCongNo.TongHopCongNoPhaiThu Then
            f.BaoCaoChiTietTongNoPhaiThu()
        ElseIf LoaiBaoCao = frmTieuChiBaoCaoCongNoThue.LoaiBaoCaoCongNo.TonngHopCongNoPhaiTra Then
            f.BaoCaoChiTietTongNoPhaiTra()
        End If



    End Sub


End Class