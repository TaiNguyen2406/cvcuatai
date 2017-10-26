Imports BACSOFT.Db.SqlHelper

Public Class frmKetQuaXuLyYCCungUng

    Private Sub frmKetQuaXuLyYCCungUng_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuNgay.EditValue = New DateTime(Today.Year, Today.Month, 1)
        tbDenNgay.EditValue = Today.Date
    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick

        ShowWaiting("Đang tải dữ liệu biểu đồ ...")
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", Convert.ToDateTime(tbDenNgay.EditValue).AddMinutes(1439))
        Dim ds As DataSet = ExecutePrcDataSet("prc_get_BaoCaoXuLyYCCungUng")

        If Not ds Is Nothing Then
            c1.Series(0).Points(0).Values(0) = ds.Tables(0).Rows(0)(0)
            c1.Series(0).Points(1).Values(0) = ds.Tables(1).Rows(0)(0)
            c1.Series(0).Points(2).Values(0) = ds.Tables(2).Rows(0)(0)
            c1.Series(0).Points(3).Values(0) = ds.Tables(3).Rows(0)(0)
            c1.RefreshData()
            Application.DoEvents()
            c2.Series(0).Points(0).Values(0) = ds.Tables(4).Rows(0)(0)
            c2.Series(0).Points(1).Values(0) = ds.Tables(5).Rows(0)(0)
            c2.Series(0).Points(2).Values(0) = ds.Tables(6).Rows(0)(0)
            c2.RefreshData()
            Application.DoEvents()
            c3.Series(0).Points(0).Values(0) = ds.Tables(7).Rows(0)(0)
            c3.Series(0).Points(1).Values(0) = ds.Tables(8).Rows(0)(0)
            c3.Series(0).Points(2).Values(0) = ds.Tables(9).Rows(0)(0)
            c3.RefreshData()
            Application.DoEvents()
            c4.Series(0).Points(0).Values(0) = ds.Tables(10).Rows(0)(0)
            c4.Series(0).Points(1).Values(0) = ds.Tables(11).Rows(0)(0)
            c4.Series(0).Points(2).Values(0) = ds.Tables(12).Rows(0)(0)
            c4.RefreshData()
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
        Application.DoEvents()
        ShowWaiting("Đang tải dữ liệu chi tiết ...")
        Threading.Thread.Sleep(50)
        AddParameterWhere("@TuNgay", tbTuNgay.EditValue)
        AddParameterWhere("@DenNgay", Convert.ToDateTime(tbDenNgay.EditValue).AddMinutes(1439))
        ds = ExecutePrcDataSet("prc_get_BaoCaoXuLyYCCungUngCT")
        If Not ds Is Nothing Then
            Application.DoEvents()
            gdv.DataSource = ds.Tables(0)

            Application.DoEvents()
            gdvDaXL.DataSource = ds.Tables(1)

            Application.DoEvents()
            gdvNhanChuaXL.DataSource = ds.Tables(2)

            Application.DoEvents()
            gdvChuaNhanXL.DataSource = ds.Tables(3)
            CloseWaiting()
        Else
            CloseWaiting()
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub tbDenNgay_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles tbDenNgay.EditValueChanged
        tbTuNgay.EditValue = New DateTime(Convert.ToDateTime(tbDenNgay.EditValue).Year, Convert.ToDateTime(tbDenNgay.EditValue).Month, 1)
    End Sub
End Class
