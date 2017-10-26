Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Public Class frmThemCCDCHong

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
        '  lueChiTietCCDC.ItemIndex = 0
        deNgaySua.EditValue = Today
        ' seChiphi.EditValue = 0
        If _message <> 0 Then
            query = "select * from TaiSan_CCDCHong where id=@id"
            AddParameterWhere("@id", _message)
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            lueChiTietCCDC.EditValue = dt.Rows(0).Item(1)
            glueNSD.EditValue = dt.Rows(0).Item(2)
            deNgaySua.EditValue = dt.Rows(0).Item(3)
            seChiphi.EditValue = dt.Rows(0).Item(4)
            txtGhichuHongCCDC.EditValue = dt.Rows(0).Item(5)
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        AddParameter("@idchitietccdc", lueChiTietCCDC.EditValue)
        AddParameter("@idnhansu", glueNSD.EditValue)
        AddParameter("@ngaysua", deNgaySua.EditValue)
        AddParameter("@chiphi", seChiphi.EditValue)
        AddParameter("@ghichuhongccdc", txtGhichuHongCCDC.EditValue)
        If _message <> "0" Then
            AddParameterWhere("@id", _message)
            If doUpdate("TaiSan_CCDCHong", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công")
            End If
        Else
            If doInsert("TaiSan_CCDCHong") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm thành công")
                _message = ExecuteSQLScalar("select top 1 id from TaiSan_TaiSanHong order by id desc")
                AddParameter("@idtinhtrang", 3)
                AddParameterWhere("@id", lueChiTietCCDC.EditValue)
                AddParameter("@ngaythanhly", deNgaySua.EditValue)
                If doUpdate("TaiSan_ChiTietCCDC", "id=@id") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                End If
            End If
        End If
    End Sub
    Private Sub ChiPhi()
       
        If lueChiTietCCDC.EditValue IsNot Nothing Then
            If deNgaySua.EditValue Is Nothing Then
                AddParameter("@time", Today)
            Else
                AddParameter("@time", deNgaySua.EditValue)
            End If
            Dim query = "select case when idloaiccdc=1 then DonGia- DonGia/(thoigiankh*365)*  datediff(day, NgayThang, @time)  else 0 end saupb "
            query &= " from TaiSan_ChiTietCCDC inner join TaiSan_CongCuDungCu on TaiSan_ChiTietCCDC.idccdc=TaiSan_CongCuDungCu.id inner join XUATKHO on TaiSan_CongCuDungCu.idxuatkho=XUATKHO.id  INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID  LEFT OUTER JOIN Taisan_DinhMuc on Taisan_DinhMuc.id=idnhomccdc where 1=1 and ((idloaiccdc =1 and datediff(day, NgayThang, @time)<=thoigiankh *365 and TSorCCDC=2) or  (idloaiccdc =2  and datediff(day, NgayThang, @time)>=0 )) and TaiSan_ChiTietCCDC.id=" + lueChiTietCCDC.EditValue.ToString()

            seChiphi.EditValue = ExecuteSQLScalar(query)
        End If

    End Sub
    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        _message = 0
        loadData()
    End Sub

    Private Sub frmThemTaiSanHong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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

    Private Sub lueChiTietCCDC_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueChiTietCCDC.EditValueChanged
        ChiPhi()
    End Sub

    Private Sub deNgaySua_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles deNgaySua.EditValueChanged
        ChiPhi()
    End Sub
End Class