Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Public Class frmThemNsdCCDC
    Protected Shared _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Private Sub loadData()
        Dim query = "select id,ten from nhansu where trangthai=1 and noictac=74"
        glueNSD.Properties.DataSource = ExecuteSQLDataTable(query)
        glueNSD.Properties.View.PopulateColumns(glueNSD.Properties.DataSource)
        glueNSD.Properties.View.Columns(glueNSD.Properties.ValueMember).Visible = False
        glueNSD.EditValue = 1
        query = " select TaiSan_CongCuDungCu.id,  ten, Model from TaiSan_CongCuDungCu inner join XUATKHO on XUATKHO.ID=TaiSan_CongCuDungCu.idxuatkho inner join VATTU on VATTU.ID=TaiSan_CongCuDungCu.idvattu inner join TENVATTU ON VATTU.IDTenvattu =TENVATTU.ID"
        lueCCDC.Properties.DataSource = ExecuteSQLDataTable(query)
        query = "select TaiSan_ChiTietCCDC.id, tenchitietccdc from TaiSan_ChiTietCCDC inner join TaiSan_CongCuDungCu on idccdc=TaiSan_CongCuDungCu.id where 1=1 "
        If _message = 0 Then
            query &= " and TaiSan_ChiTietCCDC.id  not in(select idchitietccdc from Taisan_NguoiSuDungCCDC)"
        End If
        query &= " order by tenchitietccdc  "
        lueChiTietCCDC.Properties.DataSource = ExecuteSQLDataTable(query)
        lueChiTietCCDC.ItemIndex = 0
        deNgayNhan.EditValue = Today
        deNgayTra.EditValue = Today
        If _message <> 0 Then
            query = "select * from Taisan_NguoiSuDungCCDC where id=@id"
            AddParameterWhere("@id", _message)
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            lueChiTietCCDC.EditValue = dt.Rows(0).Item(1)
            glueNSD.EditValue = dt.Rows(0).Item(2)
            deNgayNhan.EditValue = dt.Rows(0).Item(3)
            If dt.Rows(0).Item(4).ToString() <> "" Then
                ceNgayTra.Checked = True
                deNgayTra.EditValue = dt.Rows(0).Item(4)
            End If
            txtGhichuNSD.EditValue = dt.Rows(0).Item(5)
        End If
    End Sub

    Private Sub frmThemNSD_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        AddParameter("@idchitietccdc", lueChiTietCCDC.EditValue)
        AddParameter("@idnhansu", glueNSD.EditValue)
        AddParameter("@ngaynhanccdc", deNgayNhan.EditValue)
        AddParameter("@ghichunsd", txtGhichuNSD.EditValue)
        If deNgayTra.Enabled = True Then
            AddParameter("@ngaytraccdc", deNgayTra.EditValue)
        End If

        If _message <> "0" Then
            AddParameterWhere("@id", _message)
            If doUpdate("Taisan_NguoiSuDungCCDC", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công")
            End If
        Else
            If doInsert("Taisan_NguoiSuDungCCDC") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm thành công")
                _message = ExecuteSQLScalar("Select top 1 id from Taisan_NguoiSuDungCCDC order by id desc")
            End If
        End If
    End Sub

    Private Sub CheckEdit1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ceNgayTra.CheckedChanged
        If ceNgayTra.Checked = True Then
            deNgayTra.Enabled = True
        Else
            deNgayTra.Enabled = False
        End If
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        _message = 0
        loadData()
    End Sub

    Private Sub lueCCDC_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles lueCCDC.ButtonClick
        If e.Button.Index = 1 Then
            lueCCDC.EditValue = Nothing
        End If
    End Sub

    Private Sub lueTaiSan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueCCDC.EditValueChanged
        Dim query = "select TaiSan_ChiTietCCDC.id, tenchitietccdc from TaiSan_ChiTietCCDC inner join TaiSan_CongCuDungCu on idccdc=TaiSan_CongCuDungCu.id where 1=1 "
        If lueCCDC.EditValue IsNot Nothing Then
            query &= " and idccdc=@idccdc"
            AddParameterWhere("@idccdc", lueCCDC.EditValue)
        End If
        query &= " order by tenchitietccdc  "
        lueChiTietCCDC.Properties.DataSource = ExecuteSQLDataTable(query)
        lueChiTietCCDC.ItemIndex = 0
    End Sub
End Class