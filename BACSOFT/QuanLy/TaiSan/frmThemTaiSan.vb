Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Public Class frmThemTaiSan
    Protected Shared _message As Integer
    Public Property Message() As Integer
        Get
            Return _message
        End Get
        Set(ByVal value As Integer)
            _message = value
        End Set
    End Property

    Private Sub loadData()
        ' Dim query = "select id, tenloaitaisan from TaiSan_LoaiTaiSan"
        lueLoaiTS.Properties.DataSource = tableLoaiTS()
        lueLoaiTS.EditValue = 1
        'query = "select id, tentinhtrang from TaiSan_TinhTrangTaiSan"
        lueTinhTrangTS.Properties.DataSource = tableTinhTrangTS()
        lueTinhTrangTS.EditValue = 1
        deNgayNhap.EditValue = Today
        If _message <> 0 Then
            Dim query = "select * from TaiSan_TaiSan where id=@id"
            AddParameterWhere("@id", _message)
            Dim dt = ExecuteSQLDataTable(query)
            '  txtMaTS.Text = dt.Rows(0).Item(0)
            txtTenTS.Text = dt.Rows(0).Item(1)
            lueLoaiTS.EditValue = dt.Rows(0).Item(2)
            seDonGia.EditValue = dt.Rows(0).Item(3)
            deNgayNhap.EditValue = dt.Rows(0).Item(4)
            txtGhiChuTS.Text = dt.Rows(0).Item(5).ToString()
            seSoLuong.EditValue = dt.Rows(0).Item(6)
            ' seMucKH.EditValue = dt.Rows(0).Item(7)
            seTGKH.EditValue = dt.Rows(0).Item(7)
        End If
    End Sub

    Private Sub frmThemTaiSan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Dim kiemtraNSD As Integer = 0
        If _message <> 0 Then
            AddParameterWhere("@idtaisan", _message)
            kiemtraNSD = ExecuteSQLScalar("select COUNT(TaiSan_ChiTietTaiSan.id) from Taisan_ChiTietTaiSan where id in(select idchitiettaisan  from Taisan_NguoiSuDung) AND idtaisan=@idtaisan")
        End If

        AddParameter("@tentaisan", txtTenTS.Text)
        AddParameter("@idloaitaisan", lueLoaiTS.EditValue)
        AddParameter("@dongia", seDonGia.EditValue)
        AddParameter("@ngaynhap", deNgayNhap.EditValue)
        AddParameter("@ghichutaisan", txtGhiChuTS.Text)
        AddParameter("@soluong", seSoLuong.EditValue)
        AddParameter("@thoigiankh", seTGKH.EditValue)
        If _message <> 0 Then
            If kiemtraNSD > seSoLuong.EditValue Then
                ShowBaoLoi("số chi tiết tài sản nhỏ hơn số chi tiết tài sản đang được sử dụng")
            Else
                AddParameterWhere("@id", _message)
                If doUpdate("TaiSan_TaiSan", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    ShowAlert("Đã cập nhật thành công")
                    AddParameterWhere("@idtaisan", _message)
                    Dim kiemtrasoluong = ExecuteSQLScalar("select count(id)  from TaiSan_ChiTietTaiSan where idtaisan=@idtaisan")
                    If kiemtrasoluong < seSoLuong.EditValue Then
                        For i = kiemtrasoluong + 1 To seSoLuong.EditValue
                            AddParameter("@idtaisan", _message)
                            AddParameter("@tenchitiettaisan", txtTenTS.Text + " " + (i - 1).ToString())
                            AddParameter("@idtinhtrang", 1)
                            If doInsert("TaiSan_ChiTietTaiSan") Is Nothing Then
                                ShowBaoLoi(LoiNgoaiLe)
                            End If
                        Next
                    Else
                        If kiemtrasoluong > seSoLuong.EditValue Then
                            AddParameterWhere("@idtaisan", _message)
                            Dim top1 = ExecuteSQLScalar("select top 1 id from TaiSan_ChiTietTaiSan where idtaisan=@idtaisan order by id desc")
                            For i = seSoLuong.EditValue + 1 To kiemtrasoluong
                                AddParameterWhere("@idtaisan", _message)
                                top1 = ExecuteSQLScalar("select top 1 id from TaiSan_ChiTietTaiSan where idtaisan=@idtaisan order by id desc")
                                AddParameterWhere("@idchitiettaisan", top1)
                                Dim check = ExecuteSQLDataTable("select id from TaiSan_NguoiSuDung where idchitiettaisan=@idchitiettaisan ")
                                If check.Rows.Count > 0 Then
                                    '   Dim id = top1
                                    AddParameterWhere("@id", top1)
                                    AddParameterWhere("@idtaisan", _message)
                                    top1 = ExecuteSQLScalar("select top 1 id from TaiSan_ChiTietTaiSan where idtaisan=@idtaisan and id<@id order by id desc")
                                End If
                                AddParameterWhere("@id", top1)
                                If doDelete("TaiSan_ChiTietTaiSan", "id=@id") Is Nothing Then
                                    ShowBaoLoi(LoiNgoaiLe)
                                End If
                            Next
                        End If

                    End If
                End If
            End If

        Else
            If doInsert("TaiSan_TaiSan") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm thành công")
                _message = ExecuteSQLScalar("select top 1 id from TaiSan_TaiSan order by id desc")
                Dim i As Integer = 1
                For i = 1 To seSoLuong.EditValue
                    AddParameter("@idtaisan", _message)
                    AddParameter("@tenchitiettaisan", txtTenTS.Text + " " + i.ToString())
                    AddParameter("@idtinhtrang", 1)
                    If doInsert("TaiSan_ChiTietTaiSan") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If
                Next

            End If
        End If
    End Sub
    Private Sub seDonGia_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles seDonGia.EditValueChanged
        seTongGia.EditValue = seDonGia.EditValue * seSoLuong.EditValue
    End Sub

    Private Sub seSoLuong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles seSoLuong.EditValueChanged
        seTongGia.EditValue = seDonGia.EditValue * seSoLuong.EditValue
    End Sub

    Private Sub seTongGia_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles seTongGia.EditValueChanged
        If seTongGia.EditValue < 10 Then
            seTGKH.EditValue = 2
        Else
            If seTongGia.EditValue >= 10 And seTongGia.EditValue < 20 Then
                seTGKH.EditValue = 3
            Else
                If seTongGia.EditValue >= 20 Then
                    seTGKH.EditValue = 4
                End If
            End If
        End If
        seMucKH.EditValue = seTongGia.EditValue / seTGKH.EditValue / 365
    End Sub

    Private Sub seTGKH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles seTGKH.EditValueChanged
        seMucKH.EditValue = seTongGia.EditValue / seTGKH.EditValue / 365
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        _message = 0
        loadData()
    End Sub
End Class