Imports BACSOFT.Db.SqlHelper

Public Class frmCNDiemQuyTrinh

    Private Sub frmCNDiemQuyTrinh_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSQuyTrinh()
        LoadDSNhanSu()
        'Dim tb As New DataTable
        'tb.Columns.Add("IDNhanVien", Type.GetType("System.Int32"))
        'tb.Columns.Add("ThoiGian", Type.GetType("System.Double"))
        'tb.Columns.Add("Diem", Type.GetType("System.Double"))
        'tb.Columns.Add("FileDinhKem", Type.GetType("System.String"))
        'gdvNhanVien.DataSource = tb

        If TrangThai.isAddNew Then
            Dim sql As String = " SET DATEFORMAT DMY "
            sql &= " SELECT tblDiemQuyTrinh.ID,DEPATMENT.Ten AS Phong,NHANSU.ID AS IDNhanVien,tblDiemQuyTrinh.IDQuyTrinh,"
            sql &= " 	tblDiemQuyTrinh.NgayThi,ISNULL(tblDiemQuyTrinh.Diem,0)Diem,tblDiemQuyTrinh.FileDinhKem "
            sql &= " FROM NHANSU LEFT JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID LEFT JOIN tblDiemQuyTrinh ON NHANSU.ID=tblDiemQuyTrinh.IDNhanVien "

            Me.Text = "Thêm điểm thi kỹ năng"
            tbNgayThi.EditValue = Today.Date
            sql &= " AND tblDiemQuyTrinh.ID=0  WHERE NHANSU.Noictac=74  AND NHANSU.TrangThai=1 "
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                gdvNhanVien.DataSource = tb
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        Else
            Me.Text = "Cập nhật điểm thi kỹ năng"
            gdvNhanVienCT.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
            AddParameterWhere("@ID", objID)
            Dim tb2 As DataTable = ExecuteSQLDataTable("SELECT * FROM tblDiemQuyTrinh where ID=@ID")
            If Not tb2 Is Nothing Then
                cbQuyTrinh.EditValue = tb2.Rows(0)("IDQuyTrinh")
                tbNgayThi.EditValue = tb2.Rows(0)("NgayThi")
                gdvNhanVien.DataSource = tb2
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
            'Sql &= " AND tblDiemThiKyNang.IDKyNang= " & tb2.Rows(0)("IDKyNang") & " AND tblDiemThiKyNang.NgayThi='" & Convert.ToDateTime(tb2.Rows(0)("NgayThi")).ToString("dd/MM/yyyy") & "'"
            'Sql &= " WHERE NHANSU.Noictac=74 "
            'Dim tb As DataTable = ExecuteSQLDataTable(Sql)
            'If Not tb Is Nothing Then
            '    gdvNhanVien.DataSource = tb
            'End If

        End If
    End Sub

    Public Sub LoadDSQuyTrinh()
        AddParameterWhere("@QuyTrinh", LoaiNangLuc.QuyTrinh)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT NLDanhSach.ID,NLTen.Ten AS TenQuyTrinh FROM NLDanhSach INNER JOIN NLTen ON NLTen.ID = NLDanhSach.IDTen WHERE Loai=@QuyTrinh")
        If Not tb Is Nothing Then
            cbQuyTrinh.Properties.DataSource = tb
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

    Public Function GhiLai() As Boolean
        gdvNhanVienCT.CloseEditor()
        gdvNhanVienCT.UpdateCurrentRow()
        Try
            BeginTransaction()
            If TrangThai.isAddNew Then
                If gdvNhanVienCT.RowCount = 1 Then Throw New Exception("Chưa có thông tin nhân viên bảo vệ quy trình !")
            Else
                If gdvNhanVienCT.RowCount = 0 Then Throw New Exception("Chưa có thông tin nhân viên bảo vệ quy trình !")
            End If

            If cbQuyTrinh.EditValue Is Nothing Then Throw New Exception("Chưa có thông tin quy trình !")

            If TrangThai.isAddNew Then
                For i As Integer = 0 To gdvNhanVienCT.RowCount - 2
                    If gdvNhanVienCT.GetRowCellValue(i, "Diem") > 0 Then
                        AddParameter("@IDQuyTrinh", cbQuyTrinh.EditValue)
                        AddParameter("@NgayThi", tbNgayThi.EditValue)
                        AddParameter("@Diem", gdvNhanVienCT.GetRowCellValue(i, "Diem"))
                        AddParameter("@FileDinhKem", gdvNhanVienCT.GetRowCellValue(i, "FileDinhKem"))
                        AddParameter("@IDNhanVien", gdvNhanVienCT.GetRowCellValue(i, "IDNhanVien"))
                        If doInsert("tblDiemQuyTrinh") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If
                Next

            Else
                ' For i As Integer = 0 To gdvNhanVienCT.RowCount - 2
                If gdvNhanVienCT.GetRowCellValue(0, "Diem") > 0 Then
                    AddParameter("@IDQuyTrinh", cbQuyTrinh.EditValue)
                    AddParameter("@NgayThi", tbNgayThi.EditValue)
                    AddParameter("@Diem", gdvNhanVienCT.GetRowCellValue(0, "Diem"))
                    AddParameter("@FileDinhKem", gdvNhanVienCT.GetRowCellValue(0, "FileDinhKem"))
                    AddParameter("@IDNhanVien", gdvNhanVienCT.GetRowCellValue(0, "IDNhanVien"))
                    AddParameterWhere("@ID", objID)
                    If doUpdate("tblDiemQuyTrinh", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                End If
            End If

            ComitTransaction()
            'TinhDiem()
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDiemQuyTrinh).LoadDS()
            ShowAlert(Me.Text & " thành công !")
            Return True

        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(ex.Message)
            Return False
        End Try


    End Function

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        If GhiLai() Then
            Dim tb As New DataTable
            tb.Columns.Add("IDNhanVien", Type.GetType("System.Int32"))
            tb.Columns.Add("ThoiGian", Type.GetType("System.Double"))
            tb.Columns.Add("Diem", Type.GetType("System.Double"))
            tb.Columns.Add("FileDinhKem", Type.GetType("System.String"))
            gdvNhanVien.DataSource = tb
        End If
    End Sub

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        If GhiLai() Then
            Me.Close()
        End If
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub TinhDiem()
        AddParameterWhere("@KN", cbQuyTrinh.EditValue)
        Dim sql As String = ""
        sql &= " SELECT tb2.ID,tmpTb.IDNhanVien,tb2.ThoiGian FROM ("
        Sql &= "  select"
        Sql &= "      tb.IDNhanVien,tb.IDKyNang,"
        Sql &= "      max(tb.NgayThi) as Ngay"
        Sql &= "  from"
        Sql &= "      tblDiemThiKyNang tb"
        sql &= "  where tb.IDKyNang=@KN AND Convert(datetime,Convert(nvarchar,tb.NgayThi,103),103) <= convert(datetime,'" & Convert.ToDateTime(tbNgayThi.EditValue).ToString("dd/MM/yyyy") & "',103) "
        Sql &= "  group by IDNhanVien,IDKyNang) tmpTb  "
        Sql &= "  INNER JOIN tblDiemThiKyNang tb2 ON tmpTb.IDNhanVien=tb2.IDNhanVien AND tmpTb.IDKyNang=tb2.IDKyNang AND tmpTb.Ngay=tb2.NgayThi"

        Dim dt As DataSet = ExecuteSQLDataSet(sql & "  SELECT Diem FROM NLDanhSach WHERE ID=@KN")
        If Not dt Is Nothing Then
            Dim mintime As Double = 0
            For i As Integer = 0 To dt.Tables(0).Rows.Count - 1
                If dt.Tables(0).Rows(i)("ThoiGian") > 0 Then
                    'And gdvNhanVienCT.GetRowCellValue(i, "ThoiGian") < mintime Then
                    If mintime = 0 Then
                        mintime = dt.Tables(0).Rows(i)("ThoiGian")
                    Else
                        If dt.Tables(0).Rows(i)("ThoiGian") < mintime Then
                            mintime = dt.Tables(0).Rows(i)("ThoiGian")
                        End If
                    End If

                End If
            Next
            For i As Integer = 0 To dt.Tables(0).Rows.Count - 1
                If dt.Tables(0).Rows(i)("ThoiGian") > 0 Then
                    AddParameter("@Diem", Math.Round((mintime / dt.Tables(0).Rows(i)("ThoiGian")) * dt.Tables(1).Rows(0)(0), 2))
                    AddParameterWhere("@ID", dt.Tables(0).Rows(i)("ID"))
                    If doUpdate("tblDiemThiKyNang", "ID=@ID") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    End If

                End If
            Next
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If


    End Sub

End Class