Imports BACSOFT.Db.SqlHelper

Public Class frmDanhLaiSoChungTu


    Private Sub frmDanhLaiSoChungTu_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim tg As DateTime = GetServerTime()
        txtThang.EditValue = tg.Month
        txtNam.EditValue = tg.Year

    End Sub


    Private Sub btnTaiDuLieu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnTaiDuLieu.ItemClick


        Dim sql As String = "select Id, NgayCT, convert(nvarchar,'') as SoCtMoi, SoCT as SoCtCu, LoaiCT, LoaiCT2, DienGiai from chungtu "
        sql &= "where year(NgayCT) = " & txtNam.EditValue & " and month(NgayCT) = " & txtThang.EditValue & " "
        sql &= "order by ngayct asc, id asc "


        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        dt.Columns.Add("NhomChungTu", Type.GetType("System.String"))
        For Each r As DataRow In dt.Rows
            r("NhomChungTu") = ChungTu.TenLoaiCT(r("LoaiCT"), r("LoaiCT2"))
        Next

        gdv.DataSource = dt

        gdvData.ExpandAllGroups()


    End Sub


    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click

        Dim nhomchungtu As String = ""
        Dim tiento As String = ""
        Dim indexStart As Integer = 1

        For i As Integer = 0 To gdvData.DataRowCount - 1
            Application.DoEvents()
            gdvData.FocusedRowHandle = i

            If gdvData.GetFocusedRowCellValue("NhomChungTu") <> nhomchungtu Then
                nhomchungtu = gdvData.GetFocusedRowCellValue("NhomChungTu")
                tiento = txtNam.EditValue.ToString.Substring(2)
                If txtThang.EditValue < 10 Then
                    tiento &= "0" & txtThang.EditValue.ToString
                Else
                    tiento &= txtThang.EditValue.ToString
                End If
                indexStart = 1
            End If

            Dim soct As String = tiento
            If indexStart.ToString.Length = 1 Then
                soct &= "00" & indexStart
            ElseIf indexStart.ToString.Length = 2 Then
                soct &= "0" & indexStart
            Else
                soct &= indexStart
            End If

            gdvData.SetFocusedRowCellValue("SoCtMoi", soct)

            indexStart += 1

        Next

    End Sub


    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click

        If Not ShowCauHoi("Cập nhật số chứng từ mới?") Then
            Exit Sub
        End If

        For i As Integer = 0 To gdvData.DataRowCount - 1
            Application.DoEvents()
            gdvData.FocusedRowHandle = i
            AddParameter("@SoCT", gdvData.GetFocusedRowCellValue("SoCtMoi"))
            AddParameterWhere("@Id", gdvData.GetFocusedRowCellValue("Id"))
            doUpdate("CHUNGTU", "Id=@Id")
        Next

        ShowAlert("Đã đánh lại số chứng từ tháng " & txtThang.EditValue & " năm " & txtNam.EditValue)

    End Sub
End Class


