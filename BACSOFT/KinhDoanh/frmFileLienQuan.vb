Imports BACSOFT.Db.SqlHelper
Public Class frmFileLienQuan

    Public SoYeuCau As Object
    Public SoChaoGia As Object
    Public MaKH As Object

    Private Sub frmFileLienQuan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        AddParameterWhere("@SYC", SoYeuCau)
        AddParameterWhere("@SCG", SoChaoGia)
        Dim sql As String = ""
        sql &= " SELECT ISNULL(FileDinhKem,'')FileDinhKem,NgayThang FROM BANGYEUCAU WHERE Sophieu=@SYC"
        sql &= " SELECT ISNULL(YEUCAUDEN.FileDinhKem,'')FileDinhKem,BANGYEUCAU.NgayThang FROM YEUCAUDEN INNER JOIN BANGYEUCAU ON BANGYEUCAU.Sophieu=YEUCAUDEN.Sophieu WHERE YEUCAUDEN.Sophieu=@SYC"
        sql &= " SELECT ISNULL(FileDinhKem,'')FileDinhKem,NgayThang FROM BANGCHAOGIA WHERE Sophieu=@SCG"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        Dim tb As New DataTable
        tb.Columns.Add("QuaTrinh", GetType(String))
        tb.Columns.Add("File", GetType(String))
        tb.Columns.Add("KD", GetType(Boolean))
        tb.Columns.Add("NgayThang", GetType(DateTime))
        If Not ds Is Nothing Then
            Dim strFile() As String
            If ds.Tables(0).Rows.Count > 0 Then
                strFile = ds.Tables(0).Rows(0)(0).ToString.Split(New Char() {";c"})
                For Each file In strFile
                    If file <> "" Then
                        tb.Rows.Add(tb.NewRow)
                        tb.Rows(tb.Rows.Count - 1)(0) = "Tiếp nhận, xử lý yêu cầu đến"
                        tb.Rows(tb.Rows.Count - 1)(1) = file
                        tb.Rows(tb.Rows.Count - 1)(2) = True
                        tb.Rows(tb.Rows.Count - 1)(3) = ds.Tables(0).Rows(0)(1)
                    End If
                Next
            End If


            For j As Integer = 0 To ds.Tables(1).Rows.Count - 1
                strFile = ds.Tables(1).Rows(j)(0).ToString.Split(New Char() {";c"})
                For Each file In strFile
                    If file <> "" Then

                        tb.Rows.Add(tb.NewRow)
                        tb.Rows(tb.Rows.Count - 1)(0) = "Tiếp nhận, xử lý yêu cầu đến"
                        tb.Rows(tb.Rows.Count - 1)(1) = file
                        tb.Rows(tb.Rows.Count - 1)(3) = ds.Tables(1).Rows(j)(1)
                        Try
                            Dim s() As String = file.Split("KT")
                            If s.Length > 0 Then

                                If "YC" & SoYeuCau & " CG" & SoChaoGia & " " = s(0) Then
                                    tb.Rows(tb.Rows.Count - 1)(2) = False
                                Else
                                    tb.Rows(tb.Rows.Count - 1)(2) = True
                                End If
                            Else
                                tb.Rows(tb.Rows.Count - 1)(2) = True
                            End If

                        Catch ex As Exception
                            tb.Rows(tb.Rows.Count - 1)(2) = True
                        End Try
                    End If
                Next
            Next
            If ds.Tables(2).Rows.Count > 0 Then
                strFile = ds.Tables(2).Rows(0)(0).ToString.Split(New Char() {";c"})
                For Each file In strFile
                    If file <> "" Then
                        Dim s1() As String = file.Split("KD")
                        If s1.Length > 0 Then
                            If "YC" & SoYeuCau & " CG" & SoChaoGia & " " = s1(0) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.BanQuanTri) Then
                                Continue For
                            End If
                        End If

                        tb.Rows.Add(tb.NewRow)
                        tb.Rows(tb.Rows.Count - 1)(0) = "Xử lý chào giá"
                        tb.Rows(tb.Rows.Count - 1)(1) = file
                        tb.Rows(tb.Rows.Count - 1)(3) = ds.Tables(2).Rows(0)(1)
                        Try
                            Dim s() As String = file.Split("KT")
                            If s.Length > 0 Then
                                If "YC" & SoYeuCau & " CG" & SoChaoGia & " " = s(0) Then
                                    tb.Rows(tb.Rows.Count - 1)(2) = False
                                Else
                                    tb.Rows(tb.Rows.Count - 1)(2) = True
                                End If
                            Else
                                tb.Rows(tb.Rows.Count - 1)(2) = True
                            End If

                        Catch ex As Exception
                            tb.Rows(tb.Rows.Count - 1)(2) = True
                        End Try
                    End If
                Next
            End If
            
        End If
        gdvListFile.DataSource = tb
    End Sub

    Private Sub gdvListFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvListFileCT.RowCellClick
        If e.Column.FieldName = "File" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub

            If Convert.ToBoolean(gdvListFileCT.GetFocusedRowCellValue("KD")) Then
                OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gdvListFileCT.GetRowCellValue(e.RowHandle, "NgayThang")).Year.ToString & "\" & UrlKinhDoanh & MaKH & "\" & e.CellValue, e.CellValue, True)

            Else
                OpenFileOnLocal(RootUrlOld & Convert.ToDateTime(gdvListFileCT.GetRowCellValue(e.RowHandle, "NgayThang")).Year.ToString & "\" & UrlKyThuat & MaKH & "\" & e.CellValue, e.CellValue, True)
            End If

        End If
    End Sub
End Class