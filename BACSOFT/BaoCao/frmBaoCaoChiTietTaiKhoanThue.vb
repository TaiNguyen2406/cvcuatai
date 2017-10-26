Imports BACSOFT.Db.SqlHelper

Public Class frmBaoCaoChiTietTaiKhoanThue

    Private Sub frmBaoCaoChiTietTaiKhoanThue_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        cmbTrangThai.EditValue = "Đã ghi sổ"
        Dim tg As DateTime = GetServerTime()
        txtDenNgay.EditValue = New DateTime(tg.Year, tg.Month, tg.Day)
        txtTuNgay.EditValue = New DateTime(tg.Year, tg.Month, tg.Day).AddDays(-30)

        Dim sql As String = "select TaiKhoan, TenGoi from taikhoanthue union all select null, N'--Tất cả tài khoản--' order by TaiKhoan "
        Dim dtTKno As DataTable = ExecuteSQLDataTable(sql)
        rcmbNo.DataSource = dtTKno
        rcmbCo.DataSource = dtTKno
        cmbNo.EditValue = DBNull.Value
        cmbCo.EditValue = DBNull.Value

    End Sub

    Private Sub rcmbNo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcmbNo.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbNo.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub rcmbCo_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcmbCo.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            cmbCo.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub btnTai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTai.ItemClick


        If cmbNo.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn thông tin tài khoản!")
            Exit Sub
        End If

        Dim sql As String = "DECLARE @TuNgay as datetime "
        sql &= "DECLARE @DenNgay as datetime "
        sql &= "SET @TuNgay = Convert(datetime,'" & Convert.ToDateTime(txtTuNgay.EditValue).ToString("dd/MM/yyyy") & "',103) "
        sql &= "SET @DenNgay = Convert(datetime,'" & Convert.ToDateTime(txtDenNgay.EditValue).ToString("dd/MM/yyyy") & "',103) "

        sql &= "SELECT b.ID,b.Id_Ct,a.SoCT,a.NgayCT,a.SoHD,a.NgayHD,b.DienGiai,b.TaiKhoanNo,b.TaiKhoanCo,b.ThanhTien,a.LoaiCT,a.LoaiCT2,a.TenKH "
        sql &= "FROM CHUNGTU a INNER JOIN CHUNGTUCHITIET b ON a.Id = b.Id_CT "

        sql &= "WHERE "
        sql &= "Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) >= @TuNgay "
        sql &= "AND Convert(datetime,CONVERT(nvarchar,NgayCT,103),103) <= @DenNgay "

        Select Case cmbTrangThai.EditValue
            Case "Đã ghi sổ"
                sql &= " and a.GhiSo = 1 "
            Case "Chưa ghi sổ"
                sql &= " and a.GhiSo = 0 "
            Case "Tất cả"
            Case "Không có bút toán"
        End Select

        If Not cmbNo.EditValue Is DBNull.Value Then
            sql &= " AND (b.TaiKhoanNo like N'" & cmbNo.EditValue & "%' OR b.TaiKhoanCo like N'" & cmbNo.EditValue & "%') "
        End If

        'If cmbTrangThai.EditValue <> "Không có bút toán" Then
        '    If Not cmbNo.EditValue Is DBNull.Value Then
        '        sql &= " AND b.TaiKhoanNo like N'" & cmbNo.EditValue & "%' "
        '    End If
        '    If Not cmbCo.EditValue Is DBNull.Value Then
        '        sql &= " AND b.TaiKhoanCo like N'" & cmbCo.EditValue & "%' "
        '    End If
        'Else
        '    sql &= " AND (isnull(b.TaiKhoanNo,'')='' or  isnull(b.TaiKhoanCo,'')='' ) "
        'End If

        sql &= " ORDER BY NgayCT, a.ID "

        Dim dt As DataTable = ExecuteSQLDataTable(sql)



        dt.Columns.Add("TenLoaiCT")
        dt.Columns.Add("TaiKhoanDoiUng")
        dt.Columns.Add(New DataColumn("PhatSinhNo", Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("PhatSinhCo", Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("DuNo", Type.GetType("System.Double")))
        dt.Columns.Add(New DataColumn("DuCo", Type.GetType("System.Double")))


        Dim r As DataRow = dt.NewRow
        r("DienGiai") = "-- Số dư đầu kỳ --"
        r("DuNo") = 0
        r("DuCo") = 0
        dt.Rows.InsertAt(r, 0)


        Dim _sodu As Double = 0


        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("TaiKhoanNo").ToString.Trim.ToLower = cmbNo.EditValue.ToString.Trim.ToLower Then
                dt.Rows(i)("PhatSinhNo") = dt.Rows(i)("ThanhTien")
                dt.Rows(i)("TaiKhoanDoiUng") = dt.Rows(i)("TaiKhoanCo")
            ElseIf dt.Rows(i)("TaiKhoanCo").ToString.Trim.ToLower = cmbNo.EditValue.ToString.Trim.ToLower Then
                dt.Rows(i)("PhatSinhCo") = dt.Rows(i)("ThanhTien")
                dt.Rows(i)("TaiKhoanDoiUng") = dt.Rows(i)("TaiKhoanNo")
            End If
            Try
                dt.Rows(i)("TenLoaiCT") = ChungTu.TenLoaiCT(dt.Rows(i)("LoaiCT"), dt.Rows(i)("LoaiCT2"))
            Catch ex As Exception
            End Try
            Try
                dt.Rows(i)("SoCT") = ChungTu.TienToCT(dt.Rows(i)("LoaiCT"), dt.Rows(i)("LoaiCT2")) & dt.Rows(i)("SoCT")
            Catch ex As Exception
            End Try
        Next
        gdv.DataSource = dt


    End Sub

    Private Sub btnKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnKetXuat.ItemClick

    End Sub

    Private Sub cmbTrangThai_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbTrangThai.EditValueChanged
        If cmbTrangThai.EditValue = "Không có bút toán" Then
            cmbCo.EditValue = DBNull.Value
            cmbNo.EditValue = DBNull.Value
            cmbNo.Enabled = False
            cmbCo.Enabled = False
        Else
            cmbNo.Enabled = True
            cmbCo.Enabled = True
        End If
    End Sub
End Class
