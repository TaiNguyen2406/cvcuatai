Imports BACSOFT.Db.SqlHelper
Imports System.Linq

Public Class frmUpdateTSCD


    Public IdVT As Object
    Public DVT As String
    Public TenHoaDon As String


    Private isOK As Boolean = False

    Private Sub frmThemVatTuNhanh_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        cmbTenVT.Properties.DataSource = ExecuteSQLDataTable("select Id,ten from tenvattu order by ten")
        cmbNhonVatTu.Properties.DataSource = ExecuteSQLDataTable("select Id,ten from tennhom order by ten")
        cmHangSX.Properties.DataSource = ExecuteSQLDataTable("select ID,TEN from tenhangsanxuat order by TEN")
        cmbDVT.Properties.DataSource = ExecuteSQLDataTable("select ID,TEN from tendonvitinh order by TEN")

        cmbTenVT.EditValue = DBNull.Value
        cmbNhonVatTu.EditValue = DBNull.Value
        cmHangSX.EditValue = DBNull.Value
        cmbDVT.EditValue = DBNull.Value

        If TrangThai.isUpdate Then
            Dim sql As String = "SELECT * FROM VATTU WHERE ID = @ID"
            AddParameter("@ID", MaTuDien)
            Dim dt As DataTable = ExecuteSQLDataTable(sql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                cmbTenVT.EditValue = dt.Rows(0)("IDTenvattu")
                cmbNhonVatTu.EditValue = dt.Rows(0)("IDTennhom")
                cmHangSX.EditValue = dt.Rows(0)("IDHangSanxuat")
                cmbDVT.EditValue = dt.Rows(0)("IDDonvitinh")
                txtModel.EditValue = dt.Rows(0)("Model").ToString
                txtTenHoaDon.EditValue = dt.Rows(0)("TenHoaDon").ToString
            End If
        End If

    End Sub


    Private Sub btGhiVaDuaVaoHD_Click(sender As System.Object, e As System.EventArgs) Handles btGhiVaDuaVaoHD.Click
        If LuuVaoCSDL() = False Then Exit Sub
        isOK = True
        Me.Close()
    End Sub

    Private Sub btnGhiVaThemMoiVT_Click(sender As System.Object, e As System.EventArgs) Handles btnGhiVaThemMoiVT.Click
        If LuuVaoCSDL() = False Then Exit Sub
        TrangThai.isAddNew = True
        cmbTenVT.EditValue = DBNull.Value
        txtTenHoaDon.EditValue = ""
        txtModel.Text = ""
        isOK = True
    End Sub

    Private Function LuuVaoCSDL()

        If cmbTenVT.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn tên vật tư!")
            Return False
        End If

        If cmbNhonVatTu.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn nhóm vật tư!")
            Return False
        End If

        If cmHangSX.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn hãng sản xuất!")
            Return False
        End If

        If cmbDVT.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn đơn vị tính!")
            Return False
        End If

        If txtTenHoaDon.EditValue.ToString.Trim = "" Then
            ShowCanhBao("Chưa nhập tên vật tư trên hóa đơn!")
            Return False
        End If

        AddParameter("@IDTenvattu", cmbTenVT.EditValue)
        AddParameter("@IDTennhom", cmbNhonVatTu.EditValue)
        AddParameter("@IDHangSanxuat", cmHangSX.EditValue)
        AddParameter("@IDDonvitinh", cmbDVT.EditValue)
        AddParameter("@Model", txtModel.Text)
        AddParameter("@TenHoaDon", txtTenHoaDon.Text)
        AddParameter("@isTaiSanCoDinh", 1)

        If TrangThai.isAddNew Then
            IdVT = doInsert("VATTU")
        ElseIf TrangThai.isUpdate Then
            AddParameterWhere("@ID", MaTuDien)
            doUpdate("VATTU", "ID=@ID")
            IdVT = MaTuDien
        End If


        If IdVT Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Return False
        End If

        TenHoaDon = txtTenHoaDon.Text
        DVT = cmbDVT.Text

        If TrangThai.isAddNew Then
            ShowAlert("Thêm tài sản cố định mới thành công!")
        ElseIf TrangThai.isUpdate Then
            ShowAlert("Cập nhật sản cố định mới thành công!")
        End If


        Return True

    End Function


    Private Sub cmbTenVT_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbTenVT.EditValueChanged
        LayTenHoaDon()
    End Sub

    Private Sub txtModel_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtModel.EditValueChanged
        LayTenHoaDon()
    End Sub

    Private Sub LayTenHoaDon()
        txtTenHoaDon.EditValue = cmbTenVT.Text & " " & txtModel.Text
    End Sub


    Private Sub frmThemVatTuNhanh_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If isOK Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub


    Private Sub cmbTenVT_ProcessNewValue(sender As System.Object, e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs) Handles cmbTenVT.ProcessNewValue
        If e.DisplayValue.ToString.Trim <> "" Then
            e.Handled = True
            If Not ShowCauHoi(e.DisplayValue & " chưa có trong hệ thống!" & vbCrLf & "Bạn có muốn thêm tên gọi này vào hệ thống không?") Then Exit Sub

            AddParameter("@ten", e.DisplayValue)
            AddParameter("@Ten_ENG", e.DisplayValue)
            Dim id As Object = doInsert("TENVATTU")
            If id Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

            Dim dt As DataTable = CType(cmbTenVT.Properties.DataSource, DataTable)
            Dim r As DataRow = dt.NewRow
            r("id") = id
            r("ten") = e.DisplayValue
            dt.Rows.InsertAt(r, 0)
            cmbTenVT.EditValue = id
            ShowAlert("Đã thêm " & e.DisplayValue & " vào hệ thống!")
            txtTenHoaDon.Focus()
        End If
    End Sub

    Private Sub cmbNhonVatTu_ProcessNewValue(sender As System.Object, e As DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs) Handles cmbNhonVatTu.ProcessNewValue
        If e.DisplayValue.ToString.Trim <> "" Then
            e.Handled = True
            If Not ShowCauHoi(e.DisplayValue & " chưa có trong hệ thống!" & vbCrLf & "Bạn có muốn thêm tên nhóm này vào hệ thống không?") Then Exit Sub

            AddParameter("@ten", e.DisplayValue)
            AddParameter("@Ten_ENG", e.DisplayValue)
            Dim id As Object = doInsert("TENNHOM")
            If id Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

            Dim dt As DataTable = CType(cmbNhonVatTu.Properties.DataSource, DataTable)
            Dim r As DataRow = dt.NewRow
            r("id") = id
            r("ten") = e.DisplayValue
            dt.Rows.InsertAt(r, 0)
            cmbNhonVatTu.EditValue = id
            ShowAlert("Đã thêm " & e.DisplayValue & " vào hệ thống!")
            txtTenHoaDon.Focus()
        End If
    End Sub


End Class