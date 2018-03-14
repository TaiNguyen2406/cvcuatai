Imports BACSOFT.Db.SqlHelper
Public Class frmNhanxulyBaotri

    Public _recordId As String
    Public _idVattuPhanmem As String

    ' Form load
    Private Sub frmNhanxulyBaotri_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Hiển thị mặc định ngày hôm nay.
        txtNgaynhanxuly.EditValue = GetServerTime()
        txtNguoinhanxuly.Text = NguoiDung
        ' Focus con trỏ vào control ghi chú
        txtGhichu.Select()
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    ' Bấm nút lưu lại
    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click

        ' Truyền tham số và giá trị để insert/update dữ liệu
        AddParameter("@Ngaynhanxuly", txtNgaynhanxuly.EditValue)
        AddParameter("@Nguoinhanxuly", TaiKhoan)
        AddParameter("@Trangthaixuly", 1)
        AddParameter("@Ghichu", txtGhichu.Text)

        ' Bắt đầu phiên
        BeginTransaction()

        AddParameterWhere("@Id", _recordId)
        Dim _iD = doUpdate("tblDulieubaotri_IT", "Id = @Id")
        If _iD Is Nothing Then
            ' Có lỗi thì Huỷ phiên
            RollBackTransaction()
            ' Báo lỗi
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã nhận xử lý thành công!")
            ' Xác nhận phiên
            ComitTransaction()
            ' Tải lại dữ liệu của gridview sau khi thêm mới
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhsachBaotri).SelectDulieubaotri(_idVattuPhanmem)
        End If
    End Sub
End Class