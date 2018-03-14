Imports BACSOFT.Db.SqlHelper

Public Class frmBaogiaVatTu
    Public _idVattuPhanmem As String
    Public _baogia As String
        ' Biến này lưu tạm các file đính kèm
    Dim dt As New DataTable

    ' Method này select tất cả bản ghi bảng tiền tệ
    Sub SelectTiente()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID, Ten FROM tblTienTe")
        If Not tb Is Nothing Then
            cboTiente.Properties.DataSource = tb
            cboTiente.Text = "vnd"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        Dim files = ""
        For i = 0 To gdvFileCT.DataRowCount - 1
            files = files & gdvFileCT.GetRowCellValue(i, "File").ToString() & ";"
        Next i

        On Error Resume Next

        ' Truyền tham số và giá trị để insert/update dữ liệu
        AddParameter("@Dongia1", txtGia.EditValue)
        AddParameter("@Tiente1", cboTiente.EditValue)
        AddParameterWhere("@Id", _idVattuPhanmem)

        ' Bắt đầu phiên
        BeginTransaction()
        Dim _iD As Object

        _iD = doUpdate("VATTU", "Id = @Id")
        If _iD Is Nothing Then
            ' Có lỗi thì Huỷ phiên
            RollBackTransaction()
            ' Báo lỗi
            ShowBaoLoi(LoiNgoaiLe)
        Else
            ShowAlert("Đã cập nhật thành công!")
            ' Xác nhận phiên
            ComitTransaction()
            ' Tải lại dữ liệu của gridview sau khi thêm mới
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhsachVatTu).GetgrdViewDSVattu().GetFocusedDataRow()("Dongia") = txtGia.EditValue


            ' Thêm file báo giá đính kèm
             ' Xoá bản ghi trước
             ExecuteSQLNonQuery("delete from tblFileBaogiaPhanmem_IT where IdVattu = '" & _idVattuPhanmem & "'")
            AddParameter("@Ngaybaogia", GetServerTime().ToString("dd/MM/yyyy HH:mm"))
             AddParameter("@IdVattu", _idVattuPhanmem)
             AddParameter("@FileBaogia", files)
             doInsert("tblFileBaogiaPhanmem_IT")
        End If
        txtGia.Select()
    End Sub

    Sub LoadFileBaogia(IdVattu As string)
        Dim _Files As String = nothing
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT FileBaogia FROM tblFileBaogiaPhanmem_IT Where IdVattu = '" & IdVattu & "'")
        If Not tb Is Nothing Then
             For Each item As DataRow In tb.Rows
                _Files = item(0).ToString()
            Next
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        If Not _Files Is Nothing Then

            For Each file In _Files.Split(";")
                If file.Trim().Length > 0 Then

                    Dim dr As DataRow = dt.NewRow()
                    dr(0) = file
                    dt.Rows.Add(dr)

                End If
            Next

            gdvFile.DataSource = dt
        End If
    End Sub

    Private Sub frmBaogiaVatTu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
          ' Thêm cột vào table chứa file tạm
        dt.Columns.Add("colFile")
        dt.Columns(0).ColumnName = "File"

        txtNgaynhap.Text = DateTime.Now.ToString("dd/MM/yyyy")
        txtNguoinhap.Text = NguoiDung
        txtMavattu.Text = _idVattuPhanmem
        txtSoluong.Text = "1"
        txtGia.Text = _baogia
        txtGia.Select()
        SelectTiente()
        LoadFileBaogia(_idVattuPhanmem)
    End Sub

    Private Sub btThemFile_Click(sender As Object, e As EventArgs) Handles btThemFile.Click
        Dim openFile As New OpenFileDialog
        openFile.Multiselect = True

        If openFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim Impersonator As New Impersonator()
            Impersonator.BeginImpersonation()

            If Not IO.Directory.Exists(UrlITPhanmem) Then
                IO.Directory.CreateDirectory(UrlITPhanmem)
            End If

            For Each file In openFile.FileNames
                    ShowWaiting("Đang chuyển file lên server ...")
                    Try
                        IO.File.Copy(file, UrlITPhanmem & IO.Path.GetFileName(file), true)
                    Catch ex As Exception
                        ShowBaoLoi(ex.Message)
                    End Try
                    CloseWaiting()

                Dim dr As DataRow = dt.NewRow()
                dr(0) = IO.Path.GetFileName(file)
                dt.Rows.Add(dr)
            Next
            Impersonator.EndImpersonation()
            gdvFile.DataSource = dt
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As Object, e As EventArgs) Handles btXoaFile.Click
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        gdvFileCT.DeleteSelectedRows()
    End Sub

    Private Sub gdvFileCT_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            'OpenFileOnLocal(UrlTaiLieuVatTu & gdvCT.GetFocusedRowCellValue("TenNhom_ENG") & "\" & gdvCT.GetFocusedRowCellValue("TenHang") & "\" & e.CellValue, e.CellValue)
            'OpenFileOnLocal(UrlITPhanmem & e.CellValue, e.CellValue)
            OpenFileOnLocal(UrlITPhanmem & e.CellValue, e.CellValue)
        End If
    End Sub
End Class