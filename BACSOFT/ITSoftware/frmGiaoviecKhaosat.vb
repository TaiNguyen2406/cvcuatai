Imports BACSOFT.Db.SqlHelper

Public Class frmGiaoviecKhaosat

    Public _idVattu As String
    Public _tenPhanmem As String

    Sub LoadThongtingiaoviec()
        Dim tb As DataTable = ExecuteSQLDataTable("select * from tblGiaoviecKhaosat_IT where IdVattu = '" & _idVattu & "' and IdNguoigiaoviec = " & TaiKhoan )
        If Not tb Is Nothing Then
            grdGiaoviec.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Sub LoadDSLoaicongviec()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID as IdLoaicongviec, Tencongviec FROM tblLoaicongviec_IT  WHERE Active = 1")
        If Not tb Is Nothing Then
            RepositoryItemLookUpEdit1.DataSource = tb
            RepositoryItemLookUpEdit1.DisplayMember = "Tencongviec"
            RepositoryItemLookUpEdit1.ValueMember = "IdLoaicongviec"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub
    Sub LoadDSNhanvien()
        Dim tb As DataTable = ExecuteSQLDataTable("select tbl1.IdNhansu Id, tbl2.Ten from tblThanhvienduan_IT tbl1 left join NHANSU tbl2 on tbl2.ID = tbl1.IdNhansu where tbl1.IdVatTu = '" & _idVattu & "' order by tbl2.IDDepatment asc")
        If Not tb Is Nothing Then
            RepositoryItemCheckedComboBoxEdit1.DataSource = tb
            RepositoryItemCheckedComboBoxEdit1.DisplayMember = "Ten"
            RepositoryItemCheckedComboBoxEdit1.ValueMember = "Id"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

        tb = ExecuteSQLDataTable("select Id, Ten from NHANSU order by IDDepatment asc")
        If Not tb Is Nothing Then
            RepositoryItemCheckedComboBoxEdit2.DataSource = tb
            RepositoryItemCheckedComboBoxEdit2.DisplayMember = "Ten"
            RepositoryItemCheckedComboBoxEdit2.ValueMember = "Id"
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Private sub SendEmail(IdDanhsachnguoiNhanviec As Object, IdDanhsachnguoiThongbao As Object)
        If IdDanhsachnguoiNhanviec Is Nothing Then
            Exit sub
        End If

        Dim tb As DataTable = ExecuteSQLDataTable("Select Email, Ten from NHANSU where ID in (" & IdDanhsachnguoiNhanviec & ")  and isnull(Email,'') <> ''")
        Dim strEmail = "", strTenNhanvien = ""

        ' Gửi email cho người nhận việc
        If Not tb Is Nothing Then
            For Each item As DataRow In tb.Rows
                strEmail &= item(0).ToString() & ","
                strTenNhanvien &= item(1).ToString() & ", "
            Next
            Utils.Email.SendToList(DataSourceDSFile(strEmail, "Email", ","), "Bộ phận IT-Phần mềm BAC thông báo Giao việc khảo sát phần mềm", "Bộ phận IT-Phần mềm thông báo cho bạn biết: Bạn đã được giao công việc Khảo sát phần mềm '" & _tenPhanmem & "'. Bạn truy cập ngay phần mềm BAC để thực hiện công việc.".ToString.Replace(Chr(10), " <BR /> "),"", Nothing, DataSourceDSFile(EmailNguoiDung, "Email", ","))
        End If

        If (strTenNhanvien.Trim().Length > 0) Then
            If strTenNhanvien.EndsWith(",") Then
                strTenNhanvien = strTenNhanvien.Substring(0, strTenNhanvien.Trim().Length - 1)
            End If
        End If
        
        If IdDanhsachnguoiThongbao Is Nothing Then
            Exit sub
        End If

        strEmail = ""
        ' Gửi email cho người thông báo
        tb = ExecuteSQLDataTable("Select Email from NHANSU where ID in (" & IdDanhsachnguoiThongbao & ")  and isnull(Email,'') <> ''")
        If Not tb Is Nothing Then
            For Each item As DataRow In tb.Rows
                strEmail &= item(0).ToString() & ","
            Next
            Utils.Email.SendToList(DataSourceDSFile(strEmail, "Email", ","), "Bộ phận IT-Phần mềm BAC thông báo nhân viên khảo sát phần mềm", "Bộ phận IT-Phần mềm thông báo cho bạn biết: Nhân viên " & strTenNhanvien & " đi khảo sát phần mềm '" & _tenPhanmem & "'.".ToString.Replace(Chr(10), " <BR /> "))
        End If
    End sub

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        Dim _iD, _del As Object
        Dim count = 0
        Dim flagStop = False
        Dim ngayFlagStop As Boolean = false

        ' Xoá bản ghi trước
        _del = ExecuteSQLNonQuery("delete from tblGiaoviecKhaosat_IT where IdVattu = '" & _idVattu & "' and IdNguoigiaoviec = " & TaiKhoan)
        
        ' Bắt đầu phiên
        BeginTransaction()

        For i = 0 To grdViewGiaoviec.DataRowCount - 1
            If grdViewGiaoviec.GetRowCellValue(i, "IdDanhsachnguoiNhanviec").ToString() <> "" And
               not grdViewGiaoviec.GetRowCellValue(i, "Motacongviec") Is Nothing  And
               grdViewGiaoviec.GetRowCellValue(i, "Batdau").ToString() <> ""  And
               grdViewGiaoviec.GetRowCellValue(i, "IdLoaicongviec").ToString() <> "" And
               grdViewGiaoviec.GetRowCellValue(i, "Kethuc").ToString() <> ""  Then
               
               If DateTime.Compare(DateTime.Parse(grdViewGiaoviec.GetRowCellValue(i, "Batdau")), DateTime.Parse(grdViewGiaoviec.GetRowCellValue(i, "Kethuc"))) > 0 Then
                    'ShowCanhBao("Cảnh báo: Có dòng Ngày bắt đầu phải nhỏ hơn ngày kết thúc.")
                    ngayFlagStop = true
               Else
                    If TimeSpan.Compare(DateTime.Parse(grdViewGiaoviec.GetRowCellValue(i, "Batdau")).TimeOfDay, DateTime.Parse(grdViewGiaoviec.GetRowCellValue(i, "Kethuc")).TimeOfDay) > 0 Then
                        'ShowCanhBao("Cảnh báo: Có dòng Ngày bắt đầu phải nhỏ hơn ngày kết thúc.")
                        ngayFlagStop = true
                    End If
               End If

                ' Truyền tham số và giá trị để insert/update dữ liệu
                AddParameter("@Ngaygiaoviec", GetServerTime().ToString("dd/MM/yyyy HH:mm"))
                AddParameter("@Batdau", grdViewGiaoviec.GetRowCellValue(i, "Batdau"))
                AddParameter("@Kethuc", grdViewGiaoviec.GetRowCellValue(i, "Kethuc"))
                AddParameter("@IdDanhsachnguoiNhanviec", grdViewGiaoviec.GetRowCellValue(i, "IdDanhsachnguoiNhanviec"))
                AddParameter("@DanhsachnguoiNhanviec", grdViewGiaoviec.GetRowCellDisplayText(i, "IdDanhsachnguoiNhanviec"))
                AddParameter("@IdDanhsachnguoiThongbao", grdViewGiaoviec.GetRowCellValue(i, "IdDanhsachnguoiThongbao"))
                AddParameter("@DanhsachnguoiThongbao", grdViewGiaoviec.GetRowCellDisplayText(i, "IdDanhsachnguoiThongbao"))
                AddParameter("@Motacongviec", grdViewGiaoviec.GetRowCellValue(i, "Motacongviec"))
                AddParameter("@IdVatTu", _idVattu)
                AddParameter("@IdLoaicongviec", grdViewGiaoviec.GetRowCellValue(i, "IdLoaicongviec"))
                AddParameter("@IdNguoigiaoviec", TaiKhoan)
                AddParameter("@TenNguoigiaoviec", NguoiDung)

                _iD = doInsert("tblGiaoviecKhaosat_IT")

                If _iD Is Nothing Then
                    ' Có lỗi thì Huỷ phiên
                    RollBackTransaction()
                    ' Báo lỗi
                    ShowBaoLoi(LoiNgoaiLe)
                    flagStop = True
                Else
                    count = count + 1
                    ' Xác nhận phiên
                    ComitTransaction()

                    ' Gửi email thông báo giao việc khảo sát phần mềm
                    SendEmail(grdViewGiaoviec.GetRowCellValue(i, "IdDanhsachnguoiNhanviec"), grdViewGiaoviec.GetRowCellValue(i, "IdDanhsachnguoiThongbao"))

                End If
            End If
        Next

        If flagStop = False Then
            If ngayFlagStop = False Then
                ShowAlert("Đã cập nhật thành công!")
                Else
                'ShowCanhBao("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.")
                ShowCanhBao("Cảnh báo: Có dòng Ngày bắt đầu phải nhỏ hơn ngày kết thúc.")
                ShowAlert("Đã cập nhật thành công! (Lưu ý: Bao gồm cả dòng có Ngày bắt đầu phải nhỏ hơn ngày kết thúc)")
            End If
        End If
    End Sub

    Private Sub frmGiaoviecKhaosat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowWaiting("Đang tải dữ liệu ...")
        LoadDSLoaicongviec()
        LoadDSNhanvien()
        LoadThongtingiaoviec()
        CloseWaiting()

        NewDefaultRow()

        ' Khởi tạo một dòng mới, chờ người dùng nhập thông tin để bấm lưu
        'If grdViewGiaoviec.RowCount = 0 Then
        '      NewDefaultRow()
        'End if
    End Sub

     Sub NewDefaultRow()
        grdViewGiaoviec.AddNewRow()
        grdViewGiaoviec.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        grdViewGiaoviec.OptionsBehavior.Editable = True
    End Sub
 
    Private Sub RepositoryItemLookUpEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemLookUpEdit1.EditValueChanging
        dim edit As DevExpress.XtraEditors.LookUpEdit
        edit = CType(sender,DevExpress.XtraEditors.LookUpEdit)
        Dim text = edit.Properties.GetDisplayValueByKeyValue(e.NewValue).ToString()
        grdViewGiaoviec.SetRowCellValue(grdViewGiaoviec.FocusedRowHandle, "IdLoaicongviec", RepositoryItemLookUpEdit1.GetKeyValueByDisplayText(text).ToString)
        Dim str = Now.ToString("HH:mm dd/MM/yyyy")
        grdViewGiaoviec.SetRowCellValue(grdViewGiaoviec.FocusedRowHandle, "Batdau", str)
        grdViewGiaoviec.SetRowCellValue(grdViewGiaoviec.FocusedRowHandle, "Kethuc", str)
        NewDefaultRow()
    End Sub
End Class