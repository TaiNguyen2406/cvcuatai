Imports BACSOFT.Db.SqlHelper
Public Class frmCNQuaTrinhBaoGia
    Public _IDYC As Object
    Private _tmpTrangThai As New Utils.TrangThai
    Private _DaXuLy As Boolean = False
    Public _SoYC As String
    Public _IdPhuTrach As Object

    Private Sub frmCNQuaTrinhBaoGia_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDataCb()
        If TrangThai.isAddNew Then
            Me.Text = "Thêm quá trình báo giá"
            _tmpTrangThai.isAddNew = True
        Else
            Me.Text = "Cập nhật quá trình báo giá"
            _tmpTrangThai.isUpdate = True
            AddParameterWhere("@ID", objID)
            Dim sql As String = "SELECT * FROM tblQuaTrinhBaoGia WHERE ID=@ID"
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tbGia.EditValue = tb.Rows(0)("Gia")
                cbTienTe.EditValue = Convert.ToByte(tb.Rows(0)("IDTienTe"))
                cbTGCungUng.EditValue = Convert.ToByte(tb.Rows(0)("IDTGCungUng"))
                tbGhiChu.EditValue = tb.Rows(0)("GhiChu")
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If

    End Sub

    Private Sub LoadDataCb()
        Dim sql As String = ""
        sql &= " SELECT ID,Ten FROM tblTienTe"
        sql &= " SELECT Ma,NoiDung FROm tblTuDien WHERE Loai=@ThoiGianCungUng "
        AddParameterWhere("@ThoiGianCungUng", LoaiTuDien.TGCungUng)
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbTienTe.Properties.DataSource = ds.Tables(0)
            cbTGCungUng.Properties.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub


    Private Sub btLuuLai_Click(sender As System.Object, e As System.EventArgs) Handles btLuuLai.Click
        Try
            Dim tg As DateTime = GetServerTime()
            AddParameter("@IDYeuCau", _IDYC)
            AddParameter("@ThoiGianBaoGia", tg)
            AddParameter("@IDCungUng", TaiKhoan)
            AddParameter("@Gia", tbGia.EditValue)
            AddParameter("@IDTGCungUng", cbTGCungUng.EditValue)
            AddParameter("@IDTienTe", cbTienTe.EditValue)
            AddParameter("@GhiChu", tbGhiChu.EditValue)
            If TrangThai.isAddNew Then
                objID = doInsert("tblQuaTrinhBaoGia")
                If objID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                TrangThai.isUpdate = True
                ShowAlert("Đã thêm quá trình báo giá")
                Me.Text = "Cập nhật quá trình báo giá"
            Else
                AddParameterWhere("@ID", objID)
                If doUpdate("tblQuaTrinhBaoGia", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                ShowAlert("Đã cập nhật quá trình báo giá")
            End If
            _DaXuLy = True
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
        Dim index As Object = CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle
        'Dim indexGD As Integer = CType(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.GetDetailView(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle, CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.GetRelationIndex(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).LoadYeuCau()
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.ClearSelection()
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle = index

        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.SelectRow(index)
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.ExpandMasterRow(index, "Quá trình báo giá")
        ' CType(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.GetDetailView(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle, CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.GetRelationIndex(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).ClearSelection()
        'CType(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.GetDetailView(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle, CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.GetRelationIndex(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle = indexGD
        'CType(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.GetDetailView(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle, CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.GetRelationIndex(CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmXuLyYeuCau).gdvYCCT.FocusedRowHandle, "Quá trình báo giá")), DevExpress.XtraGrid.Views.Grid.GridView).SelectRow(indexGD)

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        If _DaXuLy = True Then
            Dim str As String = ""
            If _tmpTrangThai.isAddNew = True Then
                str = "Đã có báo giá mới cho YC: " & _SoYC & " từ " & NguoiDung
            Else
                str = "Đã cập nhật báo giá cho YC: " & _SoYC & " từ " & NguoiDung
            End If
            ThemThongBaoChoNV(str, _IdPhuTrach)
        End If
        
        Me.Close()

    End Sub
End Class