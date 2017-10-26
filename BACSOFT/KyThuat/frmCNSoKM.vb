Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Popup
Imports DevExpress.Utils.Win

Public Class frmCNSoKM
    Public idsokm As Integer
    Private Sub frmCNLichThiCong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        lueKhachHang.Properties.DataSource = ExecuteSQLDataTable("select ID, ttcMa from KhachHang")
        If TrangThai.isAddNew Then
            Me.Text = "Thêm số Km"
            deNgayApDung.EditValue = GetServerTime()

        Else
            Me.Text = "Cập nhật số Km"
            AddParameterWhere("@Id", idsokm)
            Dim tb As DataTable = ExecuteSQLDataTable("select * from KHACHHANG_SoKM where Id=@Id")
            If tb IsNot Nothing Then
                lueKhachHang.EditValue = tb.Rows(0)("IdKhachHang")
                seSoKm.EditValue = tb.Rows(0)("SoKM")
                deNgayApDung.EditValue = tb.Rows(0)("NgayApDung")
            End If

        End If


    End Sub


    Private Sub btnLuu_Click(sender As System.Object, e As System.EventArgs) Handles btnLuu.Click
        AddParameter("@IdKhachHang", lueKhachHang.EditValue)
        AddParameter("@SoKM", seSoKm.EditValue)
        AddParameter("@NgayApDung", deNgayApDung.EditValue)
        If TrangThai.isAddNew = True Then
            If doInsert("KHACHHANG_SoKM") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("cập nhật thành công")
            End If
        Else
            AddParameterWhere("@Id", idsokm)
            If doUpdate("KHACHHANG_SoKM", "Id=@Id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("cập nhật thành công")
            End If
        End If
      
    End Sub

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        TrangThai.isAddNew = True
        lueKhachHang.EditValue = Nothing
        seSoKm.EditValue = 0
    End Sub

   

End Class