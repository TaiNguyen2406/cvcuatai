Imports BACSOFT.Db.SqlHelper
Imports System.Linq

Public Class frmThemVatTuNhanh


    Public IdVT As Object
    Public DVT As String
    Public TenHoaDon As String


    Private isOK As Boolean = False

    Private Sub frmThemVatTuNhanh_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        cmbTenVT.Properties.DataSource = ExecuteSQLDataTable("select Id,ten from tenvattu order by ten")
        cmHangSX.Properties.DataSource = ExecuteSQLDataTable("select ID,TEN from tenhangsanxuat order by TEN")
        cmbDVT.Properties.DataSource = ExecuteSQLDataTable("select ID,TEN from tendonvitinh order by TEN")

        cmbTenVT.EditValue = DBNull.Value
        cmHangSX.EditValue = DBNull.Value
        cmbDVT.EditValue = DBNull.Value

    End Sub





    Private Sub btGhiVaDuaVaoHD_Click(sender As System.Object, e As System.EventArgs) Handles btGhiVaDuaVaoHD.Click
        LuuVaoCSDL()
        isOK = True
        Me.Close()
    End Sub

    Private Sub btnGhiVaThemMoiVT_Click(sender As System.Object, e As System.EventArgs) Handles btnGhiVaThemMoiVT.Click
        LuuVaoCSDL()
        txtModel.Text = ""
        isOK = False
    End Sub

    Private Function LuuVaoCSDL()

        If cmbTenVT.EditValue Is DBNull.Value Then
            ShowCanhBao("Chưa chọn tên vật tư!")
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

        If txtModel.EditValue.ToString.Trim = "" Then
            ShowCanhBao("Chưa nhập model vật tư!")
            Return False
        End If

        If txtTenHoaDon.EditValue.ToString.Trim = "" Then
            ShowCanhBao("Chưa nhập tên vật tư trên hóa đơn!")
            Return False
        End If


        AddParameter("@IDTenvattu", cmbTenVT.EditValue)
        AddParameter("@IDHangSanxuat", cmHangSX.EditValue)
        AddParameter("@IDDonvitinh", cmbDVT.EditValue)
        AddParameter("@Model", txtModel.Text)
        AddParameter("@TenHoaDon", txtTenHoaDon.Text)

        IdVT = doInsert("VATTU")

        If IdVT Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Return False
        End If

        TenHoaDon = txtTenHoaDon.Text
        DVT = cmbDVT.Text

        If Not HoaDonGTGT.CacheData.dataVatTu Is Nothing Then
            Dim r As DataRow = HoaDonGTGT.CacheData.dataVatTu.Table.NewRow
            r("ID") = IdVT
            r("TenVatTu") = TenHoaDon
            r("DVT") = DVT
            r("Ton") = DBNull.Value
            HoaDonGTGT.CacheData.dataVatTu.Table.Rows.InsertAt(r, 0)
        End If

        ShowAlert("Thêm vật tư mới thành công!")
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





End Class