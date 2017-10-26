Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Imports BACSOFT.HoaDonGTGT

Public Class frmUpdateTonKhoThueTSCD


    Public idTonKho As Object
    Public Nam As Integer


    Private Sub frmUpdateTonKhoThueTSCD_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'LoadDsPhongBan()

        Dim sql As String = "SELECT ID, (case when rtrim(ltrim(isnull(TenHoaDon,''))) = '' then RTRIM(LTRIM(ISNULL((SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu),'') + ' ' + ISNULL(Model,''))) else TenHoaDon end) as TenVatTu, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT,  "
        sql &= "(select Ten from tennhom where ID = VATTU.IDTennhom)TenNhom "
        sql &= "FROM VATTU WHERE isTaiSanCoDinh = 1 ORDER BY TenHoaDon "
        gdvVT.DataSource = ExecuteSQLDataTable(sql)

        Dim tg As DateTime = GetServerTime()
        txtNgayMua.EditValue = tg
        txtNgaySuDung.EditValue = tg
        txtNgayTang.EditValue = tg
        txtNgayTinhKhauHao.EditValue = tg

        sql = "SELECT ID,ttcMa,Ten,ttcMasothue,ttcDiachi FROM KHACHHANG"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            cmbDoiTuong.Properties.DataSource = tb
        End If
        cmbDoiTuong.EditValue = DBNull.Value

        If TrangThai.isUpdate Then

            sql = "SELECT * FROM TONKHOTHUETSCD WHERE ID = " & idTonKho
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            Dim r As DataRow = dt.Rows(0)
            txtSoLuong.EditValue = r("SoLuong")

            cmbDoiTuong.EditValue = r("IDkhachhang")
            txtNgayMua.EditValue = r("NgayMua")
            txtNgayTang.EditValue = r("NgayGhiTang")
            txtNgaySuDung.EditValue = r("NgaySuDung")
            txtNgayTinhKhauHao.EditValue = r("NgayTinhKhauHao")

            txtSoLuong.EditValue = r("SoLuong")
            txtNguyenGia.EditValue = r("NguyenGia")
            txtNamSuDung.EditValue = r("SoNamSuDung")
            txtTyLeKhauHao.EditValue = r("TyLeKhauHaoThang")
            txtKhauHaoThang.EditValue = r("GiaTriKhauHaoThang")
            txtGiaTriConLai.EditValue = r("GiaTriConLai")
            txtKhauHaoLuyKe.EditValue = r("KhauHaoLuyKe")
            txtGhiChu.EditValue = r("GhiChu")


            Nam = r("Nam")

            gdvDataVT.ActiveFilterString = "[ID]=" & r("IdVatTu")
        End If


    End Sub



    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click

        If gdvDataVT.FocusedRowHandle < 0 Then
            ShowCanhBao("Chưa chọn tài sản cố định!")
            Exit Sub
        End If

        Try
            AddParameter("@IdVatTu", gdvDataVT.GetFocusedRowCellValue("ID"))
            AddParameter("@Nam", Nam)
            AddParameter("@SoLuong", txtSoLuong.EditValue)

            AddParameter("@IDkhachhang", cmbDoiTuong.EditValue)
            AddParameter("@NgayMua", txtNgayMua.EditValue)
            AddParameter("@NgayGhiTang", txtNgayTang.EditValue)
            AddParameter("@NgaySuDung", txtNgaySuDung.EditValue)
            AddParameter("@NgayTinhKhauHao", txtNgayTinhKhauHao.EditValue)

            AddParameter("@NguyenGia", txtNguyenGia.EditValue)
            AddParameter("@SoNamSuDung", txtNamSuDung.EditValue)
            AddParameter("@TyLeKhauHaoThang", txtTyLeKhauHao.EditValue)
            AddParameter("@GiaTriKhauHaoThang", txtKhauHaoThang.EditValue)
            AddParameter("@GiaTriConLai", txtGiaTriConLai.EditValue)
            AddParameter("@KhauHaoLuyKe", txtKhauHaoLuyKe.EditValue)
            AddParameter("@GhiChu", txtGhiChu.EditValue)


            If TrangThai.isAddNew Then
                idTonKho = doInsert("TONKHOTHUETSCD")
                If idTonKho Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ShowAlert("Thêm mới tồn kho thuế TSCĐ thành công!")
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@dk_ID", idTonKho)
                If doUpdate("TONKHOTHUETSCD", "Id = @dk_ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ShowAlert("Cập nhật tồn kho thuế TSCĐ thành công!")
            End If

            TrangThai.isUpdate = True
            btnThemMoi.Enabled = True
        Catch ex As Exception
            ShowBaoLoi(LoiNgoaiLe)
        End Try


    End Sub


    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub


    Private Sub TinhKhauHaoThang(sender As System.Object, e As System.EventArgs) Handles txtNamSuDung.ValueChanged, txtNguyenGia.ValueChanged
        Try
            Dim sothang As Integer = txtNamSuDung.EditValue * 12
            txtTyLeKhauHao.EditValue = Math.Round(100 / sothang, 0, MidpointRounding.AwayFromZero)
            txtKhauHaoThang.EditValue = Math.Round(txtNguyenGia.EditValue / sothang, 0, MidpointRounding.AwayFromZero)
        Catch ex As Exception
            txtKhauHaoThang.EditValue = 0
        End Try

    End Sub



    Private Sub btnThemMoi_Click(sender As System.Object, e As System.EventArgs) Handles btnThemMoi.Click
        btnThemMoi.Enabled = False

        txtSoLuong.EditValue = 1

        cmbDoiTuong.EditValue = DBNull.Value
        Dim tg As DateTime = GetServerTime()
        txtNgayMua.EditValue = tg
        txtNgayTang.EditValue = tg
        txtNgaySuDung.EditValue = tg
        txtNgayTinhKhauHao.EditValue = tg


        txtNguyenGia.EditValue = 0
        txtNamSuDung.EditValue = 1
        txtTyLeKhauHao.EditValue = 0
        txtKhauHaoThang.EditValue = 0
        txtGiaTriConLai.EditValue = 0
        txtKhauHaoLuyKe.EditValue = 0
        txtGhiChu.EditValue = ""
        TrangThai.isAddNew = True

    End Sub

    Private Sub txtKhauHaoLuyKe_ValueChanged(sender As System.Object, e As System.EventArgs) Handles txtKhauHaoLuyKe.ValueChanged
        Try
            txtGiaTriConLai.EditValue = txtNguyenGia.EditValue - txtKhauHaoLuyKe.EditValue
        Catch ex As Exception
            txtGiaTriConLai.EditValue = 0
        End Try
    End Sub
End Class