Imports BACSOFT.Db.SqlHelper

Public Class frmLoaicongviec

    ' Biến này cho biết form làm nhiệm vụ Insert hay Update
    Dim _flagForm = True

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        ' Truyền tham số và giá trị để insert/update dữ liệu
        AddParameter("@Tencongviec", txtTen.Text)
        AddParameter("@Ghichu", txtGhichu.Text)
        AddParameter("@Active", chkActive.Checked)
        ' Bắt đầu phiên
        BeginTransaction()
        Dim _iD As Object

        If _flagForm = False Then
            ' Update bảng CSDL VATTU
            AddParameterWhere("@Id", grdViewDSCV.GetFocusedRowCellValue("Id"))
            _iD = doUpdate("tblLoaicongviec_IT", "Id = @Id")
        Else
            ' Insert bản ghi vào bảng CSDL VATTU
            _iD = doInsert("tblLoaicongviec_IT")
        End If

        If _iD Is Nothing Then
            ' Có lỗi thì Huỷ phiên
            RollBackTransaction()
            ' Báo lỗi
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật thành công!")

            ' Xác nhận phiên
            ComitTransaction()

            SelectLoaicongviec()
        End If
    End Sub

    Sub SelectLoaicongviec()
        Dim sql = "SELECT * FROM tblLoaicongviec_IT"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            grdDSCV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btnThem_Click(sender As Object, e As EventArgs) Handles btnThem.Click
        _flagForm = True ' Thêm mới
        txtGhichu.Text = ""
        txtTen.Text = ""
        chkActive.Checked = True
    End Sub

    Private Sub frmLoaicongviec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelectLoaicongviec()
    End Sub

    Private Sub grdViewDSCV_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSCV.FocusedRowChanged
        _flagForm = False
        On Error Resume Next
        If _flagForm = False Then
            txtGhichu.Text = grdViewDSCV.GetFocusedRowCellValue("Ghichu")
            txtTen.Text = grdViewDSCV.GetFocusedRowCellValue("Tencongviec")
            chkActive.Checked = grdViewDSCV.GetFocusedRowCellValue("Active")
        End If
    End Sub
End Class