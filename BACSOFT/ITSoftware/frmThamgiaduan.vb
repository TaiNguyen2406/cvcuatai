Imports BACSOFT.Db.SqlHelper

Public Class frmThamgiaduan

    Public _idVattuPhanmem As String
    ' Lưu số yêu cầu
    Public _soYC As String
    Public _tenPM As String
    Public _model As String
    Public _code As String

    ' Method Select các thành viên dự án theo mã vật tư phần mềm
    Sub SelectThanhvienDuan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT IdRole, IdNhansu, TuNgay, Ghichu FROM tblThanhvienduan_IT where IdVatTu = " & _idVattuPhanmem)
        If Not tb Is Nothing Then
            grdDS.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Sub FillComboboxRole()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Id, Tenrole FROM tblRoles_IT")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit2.DataSource = tb
            RepositoryItemLookUpEdit2.DisplayMember = "Tenrole"
            RepositoryItemLookUpEdit2.ValueMember = "Id"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    ' Fill danh sách nhân sự vào combobox để chọn
    Sub FillComboboxNhansu()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Id, Ten FROM NHANSU order by IDDepatment asc")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit3.DataSource = tb
            RepositoryItemLookUpEdit3.DisplayMember = "Ten"
            RepositoryItemLookUpEdit3.ValueMember = "Id"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        Dim _iD, _del As Object
        Dim count = 0
        Dim flagStop = False

        ' Xoá bản ghi trước
        _del = ExecuteSQLNonQuery("exec sp_Thanhvienduan_IT @activity = 'xoa', @idvattu = " & _idVattuPhanmem)

        For i = 0 To grdViewDS.DataRowCount - 1
            If Not grdViewDS.GetRowCellValue(i, "IdRole").ToString() Is Nothing And
               grdViewDS.GetRowCellValue(i, "IdRole").ToString() <> "-1" And
               grdViewDS.GetRowCellValue(i, "IdRole").ToString() <> "" And
               grdViewDS.GetRowCellValue(i, "IdNhansu").ToString() <> "" Then

                ' Bắt đầu phiên
                BeginTransaction()

                ' Truyền tham số và giá trị để insert/update dữ liệu
                AddParameter("@IdVatTu", _idVattuPhanmem)
                AddParameter("@IdRole", grdViewDS.GetRowCellValue(i, "IdRole"))
                AddParameter("@IdNhansu", grdViewDS.GetRowCellValue(i, "IdNhansu"))

                If grdViewDS.GetRowCellValue(i, "TuNgay").ToString() = "" Then
                    AddParameter("@TuNgay", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                Else
                    AddParameter("@TuNgay", grdViewDS.GetRowCellValue(i, "TuNgay"))
                End If

                AddParameter("@Ghichu", grdViewDS.GetRowCellValue(i, "Ghichu"))
                AddParameter("@IdUser", TaiKhoan)

                _iD = doInsert("tblThanhvienduan_IT")

                If _iD Is Nothing Or _del Is Nothing Then
                    ' Có lỗi thì Huỷ phiên
                    RollBackTransaction()
                    ' Báo lỗi
                    ShowBaoLoi(LoiNgoaiLe)
                    flagStop = True
                Else
                    count = count + 1
                    ' Xác nhận phiên
                    ComitTransaction()
                End If
            End If
        Next i

        If flagStop = False Then
            ShowAlert("Đã cập nhật thành công!")
        End If

        ' Refresh ứng dụng
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhsachVatTu).FillDSVaitro(_idVattuPhanmem)
    End Sub

    Private Sub frmThamgiaduan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtTenpm.Text = _idVattuPhanmem
        txtSoyc.Text = _soYC
        txtCode.Text = _code
        txtModel.Text = _model
        txtTenpm.Text = _tenPM

        ShowWaiting("Đang tải dữ liệu ...")
        FillComboboxRole()
        FillComboboxNhansu()
        SelectThanhvienDuan()

        CloseWaiting()

        ' Khởi tạo một dòng mới, chờ người dùng nhập thông tin để bấm lưu
        NewDefaultRow()
    End Sub

    ' Method này cho phép khởi tạo một dòng dữ liệu mới
    Sub NewDefaultRow()
        grdViewDS.AddNewRow()
        grdViewDS.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        grdViewDS.OptionsBehavior.Editable = True
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If grdViewDS.FocusedRowHandle < 0 Then Exit Sub
        grdViewDS.DeleteSelectedRows()
    End Sub
End Class