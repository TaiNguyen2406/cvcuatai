Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Public Class frmThemNsdTS
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
        query = " select TaiSan_TaiSan.id,  ten, Model from Taisan_TaiSan inner join XUATKHO on XUATKHO.ID=TaiSan_TaiSan.idxuatkho inner join VATTU on VATTU.ID=TaiSan_TaiSan.idvattu inner join TENVATTU ON VATTU.IDTenvattu =TENVATTU.ID"
        lueTaiSan.Properties.DataSource = ExecuteSQLDataTable(query)
        query = "select TaiSan_ChiTietTaiSan.id, tenchitiettaisan from TaiSan_ChiTietTaiSan inner join TaiSan_TaiSan on idtaisan=TaiSan_TaiSan.id where idloaitaisan=2"
        If _message = 0 Then
            query &= " and TaiSan_ChiTietTaiSan.id  not in(select idchitiettaisan from TaiSan_NguoiSuDung)"
        End If
        query &= " order by tenchitiettaisan  "
        lueChiTietTaiSan.Properties.DataSource = ExecuteSQLDataTable(query)
        lueChiTietTaiSan.ItemIndex = 0
        
        deNgayNhan.EditValue = Today
        deNgayTra.EditValue = Today
        If _message <> 0 Then
            query = "select * from TaiSan_NguoiSuDung where id=@id"
            AddParameterWhere("@id", _message)
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            lueChiTietTaiSan.EditValue = dt.Rows(0).Item(1)
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
        AddParameter("@idchitiettaisan", lueChiTietTaiSan.EditValue)
        AddParameter("@idnhansu", glueNSD.EditValue)
        AddParameter("@ngaynhantaisan", deNgayNhan.EditValue)
        AddParameter("@ghichunsd", txtGhichuNSD.EditValue)
        If deNgayTra.Enabled = True Then
            AddParameter("@ngaytrataisan", deNgayTra.EditValue)
        End If

        If _message <> "0" Then
            AddParameterWhere("@id", _message)
            If doUpdate("TaiSan_NguoiSuDung", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật thành công")
            End If
        Else
            If doInsert("TaiSan_NguoiSuDung") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã thêm thành công")
                _message = ExecuteSQLScalar("Select top 1 id from TaiSan_NguoiSuDung order by id desc")
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

    Private Sub lueTaiSan_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles lueTaiSan.ButtonClick
        If e.Button .Index =1 Then
            lueTaiSan.EditValue = Nothing
        End If
    End Sub

    Private Sub lueTaiSan_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueTaiSan.EditValueChanged
        Dim query = "select TaiSan_ChiTietTaiSan.id, tenchitiettaisan from TaiSan_ChiTietTaiSan inner join TaiSan_TaiSan on idtaisan=TaiSan_TaiSan.id where idloaitaisan=2 "
        If lueTaiSan.EditValue IsNot Nothing Then
            query &= " and idtaisan=@idtaisan"
            AddParameterWhere("@idtaisan", lueTaiSan.EditValue)
        End If
        query &= " order by tenchitiettaisan  "
        lueChiTietTaiSan.Properties.DataSource = ExecuteSQLDataTable(query)
        lueChiTietTaiSan.ItemIndex = 0

    End Sub
End Class