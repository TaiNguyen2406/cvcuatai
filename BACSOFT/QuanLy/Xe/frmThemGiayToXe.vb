Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Public Class frmThemGiayToXe
    Private Shared query As String
    Private Shared dt As DataTable
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
        query = "select id, tenxe from xe "
        lueXe.Properties.DataSource = ExecuteSQLDataTable(query)
        lueXe.ItemIndex = 0
        lueLoaiGiayTo.Properties.DataSource = tableLoaiGiayTo()
        lueLoaiGiayTo.ItemIndex = 0
        If _message <> 0 Then
            query = "select * from GiayToXe  where id=@id"
            AddParameterWhere("@id", _message)
            dt = ExecuteSQLDataTable(query)
            lueXe.EditValue = dt.Rows(0).Item(1)
            deNgayBatDau.EditValue = dt.Rows(0).Item(2)
            deNgayHetHan.EditValue = dt.Rows(0).Item(3)
            lueLoaiGiayTo.EditValue = dt.Rows(0).Item(4)
            txtGhichu.EditValue = dt.Rows(0).Item(5)
        End If

    End Sub

    Private Sub frmThemGiayToXe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub btnThem_Click(sender As System.Object, e As System.EventArgs) Handles btnThem.Click
        AddParameter("@idxe", lueXe.EditValue)
        AddParameter("@ngaybatdau", deNgayBatDau.EditValue)
        AddParameter("@ngayhethan", deNgayHetHan.EditValue)
        AddParameter("@ghichu", txtGhichu.EditValue)
        AddParameter("@idloaigiayto", lueLoaiGiayTo.EditValue)
        If _message <> 0 Then
            AddParameterWhere("@id", _message)
            If doUpdate("GiayToXe", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công")
            End If
        Else
            If doInsert("GiayToXe") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm thành công")
                _message = ExecuteSQLScalar("select top 1 id from GiayToXe order by id desc")
            End If
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        _message = 0
        loadData()
    End Sub
End Class