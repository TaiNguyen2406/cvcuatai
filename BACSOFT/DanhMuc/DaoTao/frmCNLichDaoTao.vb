Imports BACSOFT.Db.SqlHelper

Public Class frmCNLichDaoTao

    Private Sub frmCNLichDaoTao_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        gdvGiaoVien.DataSource = DataSourceDSFile(, "IDNhanSu")

        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Int32"))
        tb.Columns.Add("IDNhanSu", Type.GetType("System.Int32"))
        gdvHocVien.DataSource = tb

        Dim tb1 As New DataTable
        tb1.Columns.Add("Time", Type.GetType("System.DateTime"))
        gdvNgay.DataSource = tb1

        loadDSTuDien()

        If TrangThai.isAddNew Then
            Me.Text = "Thêm môn học"
        Else
            Me.Text = "Cập nhật lịch đào tạo"
            Dim sql As String = ""
            sql &= " SELECT * FROM tblLichDaoTao WHERE ID=@ID"
            sql &= " SELECT ID,IDNhanSu FROM tblHocVien WHERE IDLichDaoTao=@ID"
            AddParameterWhere("@ID", objID)
            Dim ds As DataSet = ExecuteSQLDataSet(sql)
            If Not ds Is Nothing Then
                cbKhoaDaoTao.EditValue = ds.Tables(0).Rows(0)("IDKhoa")
                cbNoiDungDaoTao.EditValue = ds.Tables(0).Rows(0)("IDNoiDung")
                gdvGiaoVien.DataSource = DataSourceDSFile(ds.Tables(0).Rows(0)("PhuTrach").ToString, "IDNhanSu")
                gdvNgay.DataSource = DataSourceDSTime(ds.Tables(0).Rows(0)("Ngay").ToString, "Time")
                gdvHocVien.DataSource = ds.Tables(1)
            End If
        End If

    End Sub



    Public Sub loadDSTuDien()
        AddParameterWhere("@Loai", LoaiTuDien.NhomMonHoc)
        Dim sql As String = ""
        sql &= " SELECT ID,TenMH FROM tblNoiDungDaoTao ORDER BY TenMH "
        sql &= " SELECT NHANSU.ID,NHANSU.Ten,KHACHHANG.ttcMa FROM NHANSU LEFT JOIN KHACHHANG ON KHACHHANG.ID=NHANSU.Noictac"
        '     sql &= " SELECT ID,Ten FROM NHANSU "
        sql &= " SELECT ID,Ten FROM tblKhoaDaoTao ORDER BY Ten "

        Dim dt As DataSet = ExecuteSQLDataSet(sql)
        If Not dt Is Nothing Then
            cbNoiDungDaoTao.Properties.DataSource = dt.Tables(0)
            If dt.Tables(0).Rows.Count > 0 Then
                cbNoiDungDaoTao.EditValue = dt.Tables(0).Rows(0)(0)
            End If
            rgdvGiangDay.DataSource = dt.Tables(1)
            rgdvHocVien.DataSource = dt.Tables(1)


            With rgdvGiangDay.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "Mã"
                colID.Visible = False
                Dim colTen = .AddField("Ten")
                colTen.Caption = "Họ tên"
                colTen.VisibleIndex = 0
                'colTen.Width = 200
                colTen.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

                Dim colNhom = .AddField("ttcMa")
                colNhom.Caption = "Nhóm"
                colNhom.GroupIndex = 0

            End With

            With rgdvHocVien.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "Mã"
                colID.Visible = False
                Dim colTen = .AddField("Ten")
                colTen.Caption = "Họ tên"
                colTen.VisibleIndex = 0
                'colTen.Width = 200
                colTen.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

                Dim colNhom = .AddField("ttcMa")
                colNhom.Caption = "Nhóm"
                colNhom.GroupIndex = 0

            End With



            cbKhoaDaoTao.Properties.DataSource = dt.Tables(2)
            'If dt.Tables(3).Rows.Count > 0 Then
            '    cbKhoaDaoTao.EditValue = dt.Tables(3).Rows(dt.Tables(3).Rows.Count - 1)("ID")
            'End If
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Function DataSourceDSTime(Optional ByVal DSFile As String = "", Optional ByVal FieldName As String = "Time", Optional ByVal _char As String = ";") As DataTable
        Dim tb As New DataTable
        tb.Columns.Add(FieldName)
        If DSFile <> "" Then
            Dim listFile() As String = DSFile.ToString.Split(New Char() {_char})
            For Each file In listFile
                If file <> "" Then
                    Try
                        Convert.ToDateTime(file)
                        tb.Rows.Add(tb.NewRow)
                        tb.Rows(tb.Rows.Count - 1)(FieldName) = file
                    Catch ex As Exception

                    End Try
                    
                End If
            Next
        End If
        Return tb
    End Function

    Public Sub loadDSKhoaDT()
        Dim dt As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM tblKhoaDaoTao ORDER BY Ten")
        If Not dt Is Nothing Then
            cbKhoaDaoTao.Properties.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub loadDSNoiDungDaoTao()
        Dim dt As DataTable = ExecuteSQLDataTable(" SELECT ID,TenMH FROM tblNoiDungDaoTao ORDER BY TenMH ")
        If Not dt Is Nothing Then
            cbNoiDungDaoTao.Properties.DataSource = dt
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btGhiLai_Click(sender As System.Object, e As System.EventArgs) Handles btGhiLai.Click
        gdvGiaoVienCT.CloseEditor()
        gdvGiaoVienCT.UpdateCurrentRow()
        gdvNgayCT.CloseEditor()
        gdvNgayCT.UpdateCurrentRow()
        gdvHocVienCT.CloseEditor()
        gdvHocVienCT.UpdateCurrentRow()


        If cbKhoaDaoTao.EditValue Is Nothing Then
            ShowCanhBao("Chưa có tên khoá đào tạo !")
            Exit Sub
        End If

        If cbNoiDungDaoTao.EditValue Is Nothing Then
            ShowCanhBao("Chưa có nội dung đào tạo !")
            Exit Sub
        End If



        Try
            BeginTransaction()

            AddParameter("@IDKhoa", cbKhoaDaoTao.EditValue)
            AddParameter("@IDNoiDung", cbNoiDungDaoTao.EditValue)
            AddParameter("@PhuTrach", StrDSFile(gdvGiaoVienCT, "IDNhanSu"))
            AddParameter("@Ngay", StrDSFile(gdvNgayCT, "Time"))

            If TrangThai.isAddNew Then
                objID = doInsert("tblLichDaoTao")
                If objID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                TrangThai.isUpdate = True
            Else
                AddParameterWhere("@ID", objID)
                If doUpdate("tblLichDaoTao", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            With gdvHocVienCT
                For i As Integer = 0 To .RowCount - 2
                    If IsDBNull(.GetRowCellValue(i, "ID")) Or .GetRowCellValue(i, "ID") Is Nothing Then
                        AddParameter("@IDLichDaoTao", objID)
                        AddParameter("@IDNhanSu", .GetRowCellValue(i, "IDNhanSu"))
                        Dim tmpID As Object = doInsert("tblHocVien")
                        If tmpID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    Else
                        AddParameter("@IDNhanSu", .GetRowCellValue(i, "IDNhanSu"))
                        AddParameterWhere("@ID", .GetRowCellValue(i, "ID"))
                        If doUpdate("tblHocVien", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)

                    End If
                Next
            End With

            ComitTransaction()
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmLichDaoTao).LoadLichDaoTao()
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmLichDaoTao).LoadDSHocVien()
            ShowAlert("Đã lưu lại !")
        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
        End Try

    End Sub

    Private Sub cbNhomMH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNoiDungDaoTao.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNNoiDungDaoTao
            f.Tag = "CNLichDaoTao"
            f.ShowDialog()
        End If
    End Sub


    Private Sub mXoaNgay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaNgay.ItemClick
        If Not gdvNgayCT.FocusedRowHandle < 0 Then
            If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
            If ShowCauHoi("Xoá ngày được chọn ?") Then
                gdvNgayCT.DeleteSelectedRows()
            End If

        End If
    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        gdvGiaoVien.DataSource = DataSourceDSFile(, "IDNhanSu")

        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Int32"))
        tb.Columns.Add("IDNhanSu", Type.GetType("System.Int32"))
        gdvHocVien.DataSource = tb

        Dim tb1 As New DataTable
        tb1.Columns.Add("Time", Type.GetType("System.DateTime"))
        gdvNgay.DataSource = tb1
        cbNoiDungDaoTao.EditValue = Nothing
        TrangThai.isAddNew = True
    End Sub

    Private Sub gdvBaiVietCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvNgayCT.RowCellClick
        If e.CellValue Is Nothing Then Exit Sub
        If e.CellValue.ToString = "" Then Exit Sub
        OpenFileOnLocal(UrlDaoTao & objID & "\" & e.CellValue, e.CellValue)
    End Sub

    Private Sub mXoaNgGiangDay_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaNgGiangDay.ItemClick
        If gdvGiaoVienCT.FocusedRowHandle < 0 Then Exit Sub
        If KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If ShowCauHoi("Xoá dòng này ?") Then
            gdvGiaoVienCT.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gdvGiaoVienCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvGiaoVienCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Delete Then
            mXoaNgGiangDay.PerformClick()
        End If

    End Sub

    Private Sub gdvBaiVietCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvNgayCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Delete Then
            mXoaNgay.PerformClick()
        End If
    End Sub

    Private Sub cbKhoaDaoTao_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbKhoaDaoTao.ButtonClick
        If e.Button.Index = 1 Then
            Dim f As New frmCNKhoaDaoTao
            f.Tag = "CNLichDaoTao"
            f.ShowDialog()
        End If
    End Sub

    Private Sub mXoaHocVien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mXoaHocVien.ItemClick
        If gdvHocVienCT.FocusedRowHandle < 0 Then Exit Sub
        If Not KiemTraQuyenSuDung("Menu", Me.Tag, DanhMucQuyen.QuyenXoa) Then Exit Sub
        If ShowCauHoi("Xoá dòng này ?") Then
            If IsDBNull(gdvHocVienCT.GetFocusedRowCellValue("ID")) Or gdvHocVienCT.GetFocusedRowCellValue("ID") Is Nothing Then
                gdvHocVienCT.DeleteSelectedRows()
            Else
                AddParameterWhere("@ID", gdvHocVienCT.GetFocusedRowCellValue("ID"))
                If doDelete("tblHocVien", "ID=@ID") Is Nothing Then
                    ShowCanhBao(LoiNgoaiLe)
                Else
                    gdvHocVienCT.DeleteSelectedRows()
                End If
            End If
        End If
    End Sub

    Private Sub btnDong_Click(sender As System.Object, e As System.EventArgs) Handles btnDong.Click
        Me.Close()
    End Sub
End Class