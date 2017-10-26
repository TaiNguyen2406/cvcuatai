Imports BACSOFT.Db.SqlHelper

Public Class frmCNDiemThiKyNang



    Private Sub frmCNDiemThiKyNang_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSKyNang()
        LoadDSNhanSu()

        If TrangThai.isAddNew Then
            Dim sql As String = " SET DATEFORMAT DMY "
            sql &= " SELECT tblDiemThiKyNang.ID,DEPATMENT.Ten AS Phong,NHANSU.ID AS IDNhanVien,tblDiemThiKyNang.IDKyNang,tblDiemThiKyNang.NgayThi,ISNULL(tblDiemThiKyNang.ThoiGian,0)ThoiGian,tblDiemThiKyNang.FileDinhKem,Diem,isnull(isNew,Convert(bit,0))isNew "
            sql &= " FROM NHANSU LEFT JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID LEFT JOIN tblDiemThiKyNang ON NHANSU.ID=tblDiemThiKyNang.IDNhanVien "

            Me.Text = "Thêm điểm thi kỹ năng"
            tbNgayThi.EditValue = Today.Date
            sql &= " AND tblDiemThiKyNang.ID=0  WHERE NHANSU.Noictac=74  AND NHANSU.TrangThai=1 "
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                gdvNhanVien.DataSource = tb
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        Else
            Me.Text = "Cập nhật điểm thi kỹ năng"
            'gdvNhanVienCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
            AddParameterWhere("@ID", objID)
            Dim tbTmp As DataTable = ExecuteSQLDataTable("SELECT * FROM tblDiemThiKyNang where ID=@ID")

            AddParameterWhere("@NgayThi", tbTmp.Rows(0)("NgayThi"))
            AddParameterWhere("@IdKyNang", tbTmp.Rows(0)("IdKyNang"))
            Dim tb2 As DataTable = ExecuteSQLDataTable("SELECT *,  (select ten from DEPATMENT where id =  (SELECT IdDepatment FROM NHANSU WHERE ID = tblDiemThiKyNang.IDNhanVien)) as Phong  FROM tblDiemThiKyNang where NgayThi =  @NgayThi AND IdKyNang = @IdKyNang")
            If Not tb2 Is Nothing Then
                cbTenKyNang.EditValue = tb2.Rows(0)("IDKyNang")
                tbNgayThi.EditValue = tb2.Rows(0)("NgayThi")
                gdvNhanVien.DataSource = tb2
                tbDL = tb2.Copy
                For i As Integer = 0 To gdvNhanVienCT.DataRowCount - 1
                    If gdvNhanVienCT.GetRowCellValue(i, "ID") = objID Then
                        gdvNhanVienCT.ClearSelection()
                        gdvNhanVienCT.SelectRow(i)
                        gdvNhanVienCT.FocusedRowHandle = i
                        gdvNhanVienCT.FocusedColumn = gdvNhanVienCT.Columns("ThoiGian")
                        gdvNhanVien.ForceInitialize()
                        gdvNhanVienCT.MakeRowVisible(i, False)
                        Exit For
                    End If
                Next
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If
    End Sub

    Private tbDL As New DataTable

    Public Sub LoadDSKyNang()
        AddParameterWhere("@KyNang", LoaiNangLuc.KyNang)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT NLDanhSach.ID,NLTen.Ten AS TenKyNang,Diem FROM NLDanhSach INNER JOIN NLTen ON NLTen.ID = NLDanhSach.IDTen WHERE Loai=@KyNang")
        If Not tb Is Nothing Then
            cbTenKyNang.Properties.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNhanSu()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM NHANSU WHERE Noictac=74 ORDER BY ID")
        If Not tb Is Nothing Then
            rgdvHocVien.DataSource = tb
            With rgdvHocVien.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "Mã"
                colID.Visible = False
                Dim colTen = .AddField("Ten")
                colTen.Caption = "Họ tên"
                colTen.VisibleIndex = 0
                'colTen.Width = 200
                colTen.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    '160900457


    Private Function getMin(gdv As DevExpress.XtraGrid.Views.Grid.GridView, tenCot As String)
        Dim arr As New List(Of Integer)
        For i As Integer = 0 To gdv.RowCount - 1
            If gdv.GetRowCellValue(i, tenCot) = 0 Then Continue For
            arr.Add(gdv.GetRowCellValue(i, tenCot))
        Next
        arr.Sort()
        If arr.Count > 0 Then Return arr(0)
        Return 0
    End Function

    Private Sub btTinhDiem_Click(sender As System.Object, e As System.EventArgs) Handles btTinhDiem.Click

        gdvNhanVienCT.CloseEditor()
        gdvNhanVienCT.UpdateCurrentRow()

        Try
            If cbTenKyNang.EditValue Is Nothing Then Throw New Exception("Chưa chọn kỹ năng !")

            Dim Sql As String = "SET DATEFORMAT DMY "
            Sql &= " SELECT tb2.ID,tmpTb.IDNhanVien,tb2.ThoiGian FROM ("
            Sql &= "  select"
            Sql &= "      tb.IDNhanVien,tb.IDKyNang,"
            Sql &= "      max(tb.NgayThi) as Ngay"
            Sql &= "  from"
            Sql &= "      tblDiemThiKyNang tb"
            Sql &= "  where tb.IDKyNang=@KN AND Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) < convert(datetime,'" & Convert.ToDateTime(tbNgayThi.EditValue).ToString("dd/MM/yyyy") & "',103) "
            Sql &= "  group by IDNhanVien,IDKyNang) tmpTb  "
            Sql &= "  INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"

            AddParameter("@KN", cbTenKyNang.EditValue)
            Dim dtDiem As DataTable = ExecuteSQLDataTable(Sql)
            If TrangThai.isAddNew Then
                tbDL = dtDiem.Copy
            End If

            'Tinh thoi gian
            For i As Integer = 0 To gdvNhanVienCT.DataRowCount - 1
                If gdvNhanVienCT.GetRowCellValue(i, "ThoiGian") <= 0 Then
                    Dim row As DataRow() = dtDiem.Select("IDNhanVien=" & gdvNhanVienCT.GetRowCellValue(i, "IDNhanVien"))
                    If row.Length > 0 Then
                        gdvNhanVienCT.SetRowCellValue(i, "ThoiGian", row(0)("ThoiGian")) 'lay thoi gian bai thi gan nhat
                    Else
                        gdvNhanVienCT.SetRowCellValue(i, "ThoiGian", 0)
                    End If
                    gdvNhanVienCT.SetRowCellValue(i, "isNew", 0) 'danh dau day la thoi gian bai thi cu
                Else
                    Dim row() As DataRow = tbDL.Select("IDNhanVien=" & gdvNhanVienCT.GetRowCellValue(i, "IDNhanVien"))
                    If row.Length > 0 Then
                        If row(0)("ThoiGian") <> gdvNhanVienCT.GetRowCellValue(i, "ThoiGian") Then
                            gdvNhanVienCT.SetRowCellValue(i, "isNew", 1)
                        End If
                    Else
                        gdvNhanVienCT.SetRowCellValue(i, "isNew", 1)
                    End If
                    'If TrangThai.isAddNew Then
                    '    gdvNhanVienCT.SetRowCellValue(i, "isNew", 1) 'danh dau day la thoi gian bai thi moi

                    'ElseIf TrangThai.isUpdate Then 'kiem tra khi update co thay doi thoi gian bai thi khong -> cap nhat lai trang thai bai thi
                    '    Dim row() As DataRow = tbDL.Select("IDNhanVien=" & gdvNhanVienCT.GetRowCellValue(i, "IDNhanVien"))
                    '    If row.Length > 0 Then
                    '        If row(0)("ThoiGian") <> gdvNhanVienCT.GetRowCellValue(i, "ThoiGian") Then
                    '            gdvNhanVienCT.SetRowCellValue(i, "isNew", 1)
                    '        End If
                    '    End If
                    'End If
                End If
            Next
            'Tinh diem
            Dim thoiGianMin As Double = getMin(gdvNhanVienCT, "ThoiGian")
            Dim diemChuan As Double = cbTenKyNang.Properties.GetDataSourceRowByKeyValue(cbTenKyNang.EditValue)("Diem")
            For i As Integer = 0 To gdvNhanVienCT.DataRowCount - 1
                'If gdvNhanVienCT.GetRowCellValue(i, "ThoiGian") = 0 Then 'Neu thoi gian = 0 lay 1/2 diem chuan cua mon do
                '    gdvNhanVienCT.SetRowCellValue(i, "Diem", Math.Round(diemChuan / 2.0F, 2, MidpointRounding.AwayFromZero))
                'Else
                '    Dim diem As Double = 0 'tinh diem cua tung nguoi
                '    diem = Math.Round((thoiGianMin / gdvNhanVienCT.GetRowCellValue(i, "ThoiGian")) * diemChuan, 2, MidpointRounding.AwayFromZero)
                '    gdvNhanVienCT.SetRowCellValue(i, "Diem", diem) '
                'End If
                If gdvNhanVienCT.GetRowCellValue(i, "ThoiGian") > 0 Then
                    Dim diem As Double = 0 'tinh diem cua tung nguoi
                    diem = Math.Round((thoiGianMin / gdvNhanVienCT.GetRowCellValue(i, "ThoiGian")) * diemChuan, 2, MidpointRounding.AwayFromZero)
                    gdvNhanVienCT.SetRowCellValue(i, "Diem", diem) '
                Else
                    gdvNhanVienCT.SetRowCellValue(i, "Diem", 0)
                End If

            Next


            cbTenKyNang.Enabled = False
            tbNgayThi.Enabled = False
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
    End Sub

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        btTinhDiem.PerformClick()
        gdvNhanVienCT.CloseEditor()
        gdvNhanVienCT.UpdateCurrentRow()

        Try

            If gdvNhanVienCT.DataRowCount = 0 Then Throw New Exception("Chưa có thông tin nhân viên thi kỹ năng !")

            'Kiem tra du lieu mon thi ngay do
            Dim isCoDuLieu As Boolean = False
            If TrangThai.isAddNew Then
                AddParameterWhere("@IDKyNang", cbTenKyNang.EditValue)
                AddParameterWhere("@NgayThi", tbNgayThi.EditValue)
                Dim dtCheck As DataTable = ExecuteSQLDataTable("SELECT * FROM tblDiemThiKyNang WHERE IDKyNang = @IDKyNang AND NgayThi = @NgayThi ")
                If dtCheck.Rows.Count > 0 Then isCoDuLieu = True
            End If

            BeginTransaction()

            If TrangThai.isAddNew Then
                If isCoDuLieu Then
                    If ShowCauHoi("Điểm thi kỹ năng môn và ngày này đã có trong hệ thống, có muốn ghi đè không ?") Then
                        'Delete du lieu cu
                        AddParameterWhere("@IDKyNang", cbTenKyNang.EditValue)
                        AddParameterWhere("@NgayThi", tbNgayThi.EditValue)
                        If doDelete("tblDiemThiKyNang", "IDKyNang=@IDKyNang AND NgayThi = @NgayThi") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        'ComitTransaction()

                        'btTinhDiem.PerformClick()
                        'gdvNhanVienCT.CloseEditor()
                        'gdvNhanVienCT.UpdateCurrentRow()
                    Else
                        Throw New Exception("Kiểm tra lại dữ liệu đã nhập !")
                    End If
                End If
                'Add vao database
                For i As Integer = 0 To gdvNhanVienCT.DataRowCount - 1
                    If gdvNhanVienCT.GetRowCellValue(i, "ThoiGian") > 0 Then
                        AddParameter("@IDKyNang", cbTenKyNang.EditValue)
                        AddParameter("@NgayThi", tbNgayThi.EditValue)
                        AddParameter("@FileDinhKem", gdvNhanVienCT.GetRowCellValue(i, "FileDinhKem"))
                        AddParameter("@IDNhanVien", gdvNhanVienCT.GetRowCellValue(i, "IDNhanVien"))
                        AddParameter("@ThoiGian", gdvNhanVienCT.GetRowCellValue(i, "ThoiGian"))
                        AddParameter("@Diem", gdvNhanVienCT.GetRowCellValue(i, "Diem"))
                        If isCoDuLieu Then
                            AddParameter("@isNew", 1)
                        Else
                            If IsDBNull(gdvNhanVienCT.GetRowCellValue(i, "isNew")) Then
                                AddParameter("@isNew", 0)
                            Else
                                AddParameter("@isNew", gdvNhanVienCT.GetRowCellValue(i, "isNew"))
                            End If

                        End If

                        If doInsert("tblDiemThiKyNang") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                    
                Next
            Else
                For i As Integer = 0 To gdvNhanVienCT.DataRowCount - 1
                    If gdvNhanVienCT.GetRowCellValue(i, "ThoiGian") > 0 Then
                        AddParameter("@IDKyNang", cbTenKyNang.EditValue)
                        AddParameter("@NgayThi", tbNgayThi.EditValue)
                        AddParameter("@FileDinhKem", gdvNhanVienCT.GetRowCellValue(i, "FileDinhKem"))
                        AddParameter("@IDNhanVien", gdvNhanVienCT.GetRowCellValue(i, "IDNhanVien"))
                        AddParameter("@ThoiGian", gdvNhanVienCT.GetRowCellValue(i, "ThoiGian"))
                        AddParameter("@Diem", gdvNhanVienCT.GetRowCellValue(i, "Diem"))
                        If IsDBNull(gdvNhanVienCT.GetRowCellValue(i, "isNew")) Then
                            AddParameter("@isNew", 0)
                        Else
                            AddParameter("@isNew", gdvNhanVienCT.GetRowCellValue(i, "isNew"))
                        End If
                        AddParameterWhere("@ID", gdvNhanVienCT.GetRowCellValue(i, "ID"))
                        If doUpdate("tblDiemThiKyNang", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Else
                        
                        AddParameterWhere("@ID", gdvNhanVienCT.GetRowCellValue(i, "ID"))
                        If doDelete("tblDiemThiKyNang", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                    
                Next
            End If
            ComitTransaction()
            Me.Close()
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDiemThiKyNang).LoadDS()
            ShowAlert(Me.Text & " thành công !")

        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub gdvNhanVienCT_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvNhanVienCT.RowCellStyle
        If gdvNhanVienCT.GetRowCellValue(e.RowHandle, "isNew") Is DBNull.Value Then
            e.Appearance.ForeColor = Color.Black
        ElseIf Convert.ToBoolean(gdvNhanVienCT.GetRowCellValue(e.RowHandle, "isNew")) Then
            e.Appearance.ForeColor = Color.Red
        Else
            e.Appearance.ForeColor = Color.Black
        End If
    End Sub

    'Private Sub gdvNhanVienCT_ShowingEditor(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles gdvNhanVienCT.ShowingEditor
    '    If TrangThai.isUpdate Then
    '        If Not gdvNhanVienCT.GetFocusedRowCellValue("isNew") Then
    '            ShowCanhBao("Chỉ được phép sửa thời gian thi của người thi mới !")
    '            e.Cancel = True
    '            'Exit Sub

    '        End If
    '    End If
    'End Sub
End Class