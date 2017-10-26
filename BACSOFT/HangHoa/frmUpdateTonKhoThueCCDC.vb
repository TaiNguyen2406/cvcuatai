Imports BACSOFT.Db.SqlHelper
Imports System.Linq
Imports BACSOFT.HoaDonGTGT

Public Class frmUpdateTonKhoThueCCDC


    Public idTonKho As Object
    Public Nam As Integer


    Private Sub frmUpdateTonKhoThueCCDC_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDsPhongBan()
        Dim sql As String = "SELECT ID, (case when rtrim(ltrim(isnull(TenHoaDon,''))) = '' then RTRIM(LTRIM(ISNULL((SELECT Ten FROM TENVATTU WHERE ID = VATTU.IDTenvattu),'') + ' ' + ISNULL(Model,''))) else TenHoaDon end) as TenVatTu, "
        sql &= "(SELECT Ten FROM TENDONVITINH WHERE ID = VATTU.IDDonvitinh)DVT  "
        sql &= "FROM VATTU WHERE isCongCuDungCu = 1 ORDER BY TenHoaDon "
        gdvVT.DataSource = ExecuteSQLDataTable(sql)

        If TrangThai.isUpdate Then
            sql = "SELECT * FROM TONKHOTHUECCDC WHERE ID = " & idTonKho
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            Dim r As DataRow = dt.Rows(0)
            txtSoLuong.EditValue = r("SoLuong")
            txtSoKyPhanBo.EditValue = r("SoKyPhanBo")
            txtSoKyConLai.EditValue = r("SoKyConLai")
            txtGiaTriConLai.EditValue = r("GiaTriConLai")
            cmbPhongBan.SelectedItem = cmbPhongBan.Properties.Items.Cast(Of ObjectItemCmb).Where(Function(x) x.GiaTri = r("IdPhongBan")).FirstOrDefault
            txtGhiChu.EditValue = r("GhiChu")
            Nam = r("Nam")
            gdvDataVT.ActiveFilterString = "[ID]=" & r("IdVatTu")
        End If
    End Sub

    Public Sub LoadDsPhongBan()
        Dim sql As String = "SELECT Id, (MaPhongBan + ' - ' + TenPhongBan) PhongBan FROM PhongBanThue order by TenPhongBan"
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            For Each r As DataRow In tb.Rows
                cmbPhongBan.Properties.Items.Add(New ObjectItemCmb(r("Id"), r("PhongBan")))
            Next
        End If
    End Sub

    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click

        If gdvDataVT.FocusedRowHandle < 0 Then
            ShowCanhBao("Chưa chọn công cụ dụng cụ!")
            Exit Sub
        End If

        If cmbPhongBan.SelectedItem Is Nothing Then
            ShowCanhBao("Chưa chọn phòng ban")
            Exit Sub
        End If


        Try
            AddParameter("@IdVatTu", gdvDataVT.GetFocusedRowCellValue("ID"))
            AddParameter("@Nam", Nam)
            AddParameter("@SoLuong", txtSoLuong.EditValue)
            AddParameter("@SoKyPhanBo", txtSoKyPhanBo.EditValue)
            AddParameter("@SoKyConLai", txtSoKyConLai.EditValue)
            AddParameter("@GiaTriConLai", txtGiaTriConLai.EditValue)
            AddParameter("@IdPhongBan", CType(cmbPhongBan.SelectedItem, ObjectItemCmb).GiaTri)
            AddParameter("@GhiChu", txtGhiChu.EditValue)

            If TrangThai.isAddNew Then
                idTonKho = doInsert("TONKHOTHUECCDC")
                If idTonKho Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ShowAlert("Thêm mới tồn kho thuế CCDC thành công!")
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@dk_ID", idTonKho)
                If doUpdate("TONKHOTHUECCDC", "Id = @dk_ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ShowAlert("Cập nhật tồn kho thuế CCDC thành công!")
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


End Class