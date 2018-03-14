Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.TAI
Public Class frmCNHinhThucTT2
    Public idhinhthucTT As Integer = 0
    Private Sub frmCNHinhThucTT_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub
    Private Sub loadData()
        lueNhom.Properties.DataSource = tableNhomHinhThucTT()
        chkTrangThai.Checked = True
        If TrangThai.isUpdate = True Then
            AddParameterWhere("@ID", idhinhthucTT)
            Dim dt As DataTable = ExecuteSQLDataTable("select * from DM_HINH_THUC_TT where ID=@ID")
            If dt Is Nothing Then Exit Sub
            With dt
                lueNhom.EditValue = .Rows(0)("Nhom")
                seTruoc1.EditValue = .Rows(0)("TraTruoc1")
                seTruoc2.EditValue = .Rows(0)("TraTruoc2")
                seSau1.EditValue = .Rows(0)("TraSau1")
                seSau2.EditValue = .Rows(0)("TraSau2")
                seHanThu.EditValue = .Rows(0)("SoNgayHT")
                seSoTT.EditValue = .Rows(0)("SoTT")
                chkTrangThai.EditValue = .Rows(0)("TrangThai")
                meGiaiThich.EditValue = .Rows(0)("GiaiThich")
            End With
        End If
    End Sub

    Private Sub btLuuVaDong_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaDong.Click
        GhiLai()
        Me.Close()
    End Sub

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        GhiLai()
        TrangThai.isAddNew = True
    
    End Sub

    Private Sub GhiLai()
        AddParameter("@TraTruoc1", seTruoc1.EditValue)
        AddParameter("@TraTruoc2", seTruoc2.EditValue)
        AddParameter("@TraSau1", seSau1.EditValue)
        AddParameter("@TraSau2", seSau2.EditValue)
        AddParameter("@Nhom", lueNhom.EditValue)
        AddParameter("@SoNgayHT", seHanThu.EditValue)
        AddParameter("@GiaiThich", meGiaiThich.EditValue)
        If chkTrangThai.Checked = True Then
            AddParameter("@TrangThai", 1)
        Else
            AddParameter("@TrangThai", 0)
        End If

        AddParameter("@SoTT", seSoTT.EditValue)
        If TrangThai.isAddNew = True Then
            If doInsert("DM_HINH_THUC_TT") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Thêm hình thức thanh toán mới thành công ")
            End If
        Else
            AddParameterWhere("@ID", idhinhthucTT)
            If doUpdate("DM_HINH_THUC_TT", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Cập nhật hình thức thanh toán thành công ")
            End If
        End If

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub
End Class