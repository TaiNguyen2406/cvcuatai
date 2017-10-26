Imports BACSOFT.Db.SqlHelper

Public Class frmTHDiemNV


    Private Sub frmTHDiemNV_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tbTuThang.EditValue = New DateTime(Today.Year, 1, 1)
        tbDenThang.EditValue = Today.Date
        LoadPhongBan()
    End Sub

    Public Sub LoadPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub



    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        Dim sql As String = ""
        Dim _i As Integer = DateDiff(DateInterval.Month, tbTuThang.EditValue, tbDenThang.EditValue)
        Dim _month As String = ""
        Dim _monthIn As String = ""
        sql &= " SET DATEFORMAT DMY "

        sql &= " select ROW_NUMBER() OVER(ORDER BY IDDepatment,ID) AS STT,  IDDepatment,ID,[Nhân viên]"
        For i As Integer = Convert.ToDateTime(tbTuThang.EditValue).Month To Convert.ToDateTime(tbTuThang.EditValue).Month + _i
            _month &= ",[" & DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbTuThang.EditValue).Year, i + 1, 0)).ToString("MM/yyyy") & "]"
        Next
        sql &= _month & ",convert(float,0) AS [Trung bình], convert(float,0) AS [% TB]"

        Sql &= " FROM"
        sql &= " (SELECT NHANSU.IDDepatment,NHANSU.ID,NHANSU.Ten AS N'Nhân viên',round(Diem1,0)Diem1,[Month] FROM tblTHChamCong "
        sql &= " INNER JOIN NHANSU ON NHANSU.ID=tblTHChamCong.IDNhanVien AND NHANSU.Noictac=74"
        sql &= " WHERE [Month] IN ("
        _i = DateDiff(DateInterval.Month, tbTuThang.EditValue, tbDenThang.EditValue)

        For i As Integer = Convert.ToDateTime(tbTuThang.EditValue).Month To Convert.ToDateTime(tbTuThang.EditValue).Month + _i
            _monthIn &= ",'" & DateAdd(DateInterval.Day, 0, DateSerial(Convert.ToDateTime(tbTuThang.EditValue).Year, i + 1, 0)).ToString("MM/yyyy") & "'"
        Next
        sql &= _monthIn.Substring(1, _monthIn.Length - 1) & ")"

        Sql &= " )p"
        Sql &= " PIVOT"
        Sql &= " ("
        Sql &= " 	SUM(Diem1)"
        sql &= " 	FOR [Month] IN ("
        sql &= _month.Substring(1, _month.Length - 1)
        sql &= " )"
        Sql &= " ) AS PVT"
        sql &= " ORDER BY IDDepatment,ID"

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            gdvCT.BeginUpdate()
            gdvCT.Columns.Clear()
            gdv.DataSource = dt
            gdvCT.Columns("IDDepatment").Visible = False
            gdvCT.Columns("ID").Visible = False

            gdvCT.Columns("STT").Width = 40
            gdvCT.Columns("STT").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            gdvCT.Columns("Nhân viên").Width = 150
            gdvCT.Columns("Nhân viên").Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

            For j As Integer = 0 To gdvCT.Columns.Count - 1
                If gdvCT.Columns(j).FieldName <> "STT" And gdvCT.Columns(j).FieldName <> "Nhân viên" And gdvCT.Columns(j).FieldName <> "% TB" Then
                    gdvCT.Columns(j).ColumnEdit = tbN2
                End If
            Next
            Dim _SoThang As Integer = 0
            Dim _TongDiem As Double = 0
            For i As Integer = 0 To gdvCT.RowCount - 1
                _SoThang = 0
                _TongDiem = 0
                For j As Integer = 0 To gdvCT.Columns.Count - 1
                    If gdvCT.Columns(j).FieldName <> "STT" And gdvCT.Columns(j).FieldName <> "Nhân viên" And gdvCT.Columns(j).FieldName <> "% TB" And gdvCT.Columns(j).FieldName <> "Trung bình" And gdvCT.Columns(j).FieldName <> "ID" And gdvCT.Columns(j).FieldName <> "IDDepatment" Then
                        If Not IsDBNull(gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName)) Then
                            If gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName) > 0 Then
                                _TongDiem += gdvCT.GetRowCellValue(i, gdvCT.Columns(j).FieldName)
                                _SoThang += 1
                            End If
                        End If
                    End If
                Next
                If _TongDiem = 0 Then
                    gdvCT.SetRowCellValue(i, "Trung bình", 0)
                Else
                    gdvCT.SetRowCellValue(i, "Trung bình", Math.Round(_TongDiem / _SoThang))
                End If

                If _TongDiem = 0 Then
                    gdvCT.SetRowCellValue(i, "% TB", 0)
                Else
                    gdvCT.SetRowCellValue(i, "% TB", Math.Round(((_TongDiem / _SoThang) / 4000) * 100, 2))
                End If

                'Try

                'Catch ex As Exception
                '    gdvCT.SetRowCellValue(i, "Trung bình", 0)
                'End Try

                'Try

                'Catch ex As Exception
                '    gdvCT.SetRowCellValue(i, "% TB", 0)
                'End Try

            Next

            gdvCT.EndUpdate()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub



    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub btXuatExcel_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXuatExcel.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"

        saveFile.FileName = "Diem NV " & Convert.ToDateTime(tbTuThang.EditValue).ToString("MM-yyyy") & "  " & Convert.ToDateTime(tbDenThang.EditValue).ToString("MM-yyyy") & ".xls"

        If saveFile.ShowDialog = DialogResult.OK Then
            ShowWaiting("Đang kết xuất ...")
            Try
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvCT, False)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                CloseWaiting()
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub
End Class