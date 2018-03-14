Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Public Class frmThemTaiSanHong

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
        query = "  select TaiSan_TaiSan.id, isnull( ten,TenTaiSan) ten, isnull(Model,MaTS) Model from Taisan_TaiSan left join XUATKHO on XUATKHO.ID=TaiSan_TaiSan.idxuatkho left join VATTU on VATTU.ID=TaiSan_TaiSan.idvattu left join TENVATTU ON VATTU.IDTenvattu =TENVATTU.ID where IdGop is null"
        lueTaiSan.Properties.DataSource = ExecuteSQLDataTable(query)
        query = "select TaiSan_ChiTietTaiSan.id, tenchitiettaisan from TaiSan_ChiTietTaiSan inner join TaiSan_TaiSan on idtaisan=TaiSan_TaiSan.id"
        If _message = 0 Then
            query &= " and TaiSan_ChiTietTaiSan.id  not in(select idchitiettaisan from TaiSan_NguoiSuDung)  and IdGop is null"
        End If
        query &= " order by tenchitiettaisan  "
        lueChiTietTaiSan.Properties.DataSource = ExecuteSQLDataTable(query)
        '    lueChiTietTaiSan.ItemIndex = 0
        deNgaySua.EditValue = Today
        If _message <> 0 Then

            query = "select *,(Select idtaisan from TaiSan_ChiTietTaiSan where id=idchitiettaisan) idtaisan from TaiSan_TaiSanHong where id=@id"
            AddParameterWhere("@id", _message)
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            lueTaiSan.EditValue = dt.Rows(0).Item(6)
            lueChiTietTaiSan.EditValue = dt.Rows(0).Item(1)
            glueNSD.EditValue = dt.Rows(0).Item(2)
            deNgaySua.EditValue = dt.Rows(0).Item(3)
            seChiphi.EditValue = dt.Rows(0).Item(4)
            txtGhichuHongTS.EditValue = dt.Rows(0).Item(5)
        End If
    End Sub
    Private Sub ChiPhi()
      
        If lueChiTietTaiSan.EditValue IsNot Nothing Then
            If deNgaySua.EditValue Is Nothing Then
                AddParameter("@time", Today)
            Else
                AddParameter("@time", deNgaySua.EditValue)
            End If
            Dim query = "select  (DonGia- DonGia/(thoigiankh*365)*  datediff(day, NgayThang, @time)) as saukh "
            query &= " from TaiSan_ChiTietTaiSan inner join TaiSan_TaiSan on TaiSan_ChiTietTaiSan.idtaisan=TaiSan_TaiSan.id inner join XUATKHO on TaiSan_TaiSan.idxuatkho=XUATKHO.ID INNER JOIN VATTU ON XUATKHO.IDVatTu=VATTU.ID INNER JOIN PHIEUXUATKHO ON PHIEUXUATKHO.SoPhieu=XUATKHO.SoPhieu LEFT OUTER JOIN TENVATTU ON VATTU.IDTenvattu=TENVATTU.ID ,Taisan_DinhMuc where 1=1  and  DonGia>=mucdau and DonGia<=muccuoi and ngayapdung=(select top 1 ngayapdung from Taisan_DinhMuc  where Dongia>=mucdau and Dongia<=muccuoi and ngayapdung<=@time  and TSorCCDC=1  order by ngayapdung desc) and datediff(day, NgayThang, @time)<=thoigiankh *365 and datediff(day, NgayThang, @time)>=0 and TSorCCDC=1 and TaiSan_ChiTietTaiSan.id=" + lueChiTietTaiSan.EditValue.ToString()

            seChiphi.EditValue = ExecuteSQLScalar(query)
        End If
      
    End Sub
    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        AddParameter("@idchitiettaisan", lueChiTietTaiSan.EditValue)
        AddParameter("@idnhansu", glueNSD.EditValue)
        AddParameter("@ngaysua", deNgaySua.EditValue)
        AddParameter("@chiphi", seChiphi.EditValue)
        AddParameter("@ghichuhongts", txtGhichuHongTS.EditValue)
        If _message <> "0" Then
            AddParameterWhere("@id", _message)
            If doUpdate("TaiSan_TaiSanHong", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công")
            End If
        Else
            If doInsert("TaiSan_TaiSanHong") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm thành công")
                _message = ExecuteSQLScalar("select top 1 id from TaiSan_TaiSanHong order by id desc")
            End If
        End If
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        _message = 0
        loadData()
    End Sub

    Private Sub frmThemTaiSanHong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub lueTaiSan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles lueTaiSan.ButtonClick
        If e.Button.Index = 1 Then
            lueTaiSan.EditValue = Nothing
        End If
    End Sub

    Private Sub lueTaiSan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueTaiSan.EditValueChanged
        Dim query = "  select TaiSan_ChiTietTaiSan.id, tenchitiettaisan from TaiSan_ChiTietTaiSan inner join TaiSan_TaiSan on idtaisan=TaiSan_TaiSan.id where  IdGop is null "
        If lueTaiSan.EditValue IsNot Nothing Then
            query &= " and idtaisan=@idtaisan"
            AddParameterWhere("@idtaisan", lueTaiSan.EditValue)
        End If
        query &= " order by tenchitiettaisan  "
        lueChiTietTaiSan.Properties.DataSource = ExecuteSQLDataTable(query)
        lueChiTietTaiSan.ItemIndex = 0

    End Sub

    Private Sub lueChiTietTaiSan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueChiTietTaiSan.EditValueChanged
        ChiPhi()
    End Sub

    Private Sub deNgaySua_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles deNgaySua.EditValueChanged
        ChiPhi()
    End Sub
End Class