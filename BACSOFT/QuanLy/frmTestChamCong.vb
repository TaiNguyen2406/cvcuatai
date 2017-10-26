
Public Class frmTestChamCong

    Private Sub frmTestChamCong_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tbThang.EditValue = Today.Month
        tbNam.EditValue = Today.Year
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles btLayDuLieu.Click
        Dim tb As DataTable = DbChamCong.doSelect("SELECT Userid,CheckTime,(Name & ' - ID ' & Userid) AS Name,DeptName FROM V_Record WHERE month(CheckTime)=" & tbThang.EditValue.ToString & "AND Year(CheckTime)=" & tbNam.EditValue.ToString)
        If Not tb Is Nothing Then
            tb.Columns.Add("Lan1", GetType(DateTime))
            tb.Columns.Add("Lan2", GetType(DateTime))
            tb.Columns.Add("Lan3", GetType(DateTime))
            tb.Columns.Add("Lan4", GetType(DateTime))
            tb.Columns.Add("Lan5", GetType(DateTime))
            tb.Columns.Add("Lan6", GetType(DateTime))
            Dim tbc = tb.Clone
            Dim r = tbc.NewRow()

            r("UserID") = tb.Rows(0)("UserID")
            r("CheckTime") = tb.Rows(0)("CheckTime")
            r("Name") = tb.Rows(0)("Name")
            r("DeptName") = tb.Rows(0)("DeptName")
            r("Lan1") = tb.Rows(0)("CheckTime")
            tbc.Rows.Add(r)
            Dim lan As Integer = 1
            Dim strErr As String = ""
            For i As Integer = 1 To tb.Rows.Count - 1
                If Convert.ToDateTime(tb.Rows(i)("CheckTime")).Date = Convert.ToDateTime(tb.Rows(i - 1)("CheckTime")).Date And tb.Rows(i)("UserID") = tb.Rows(i - 1)("UserID") Then
                    lan += 1
                    Try
                        tbc.Rows(tbc.Rows.Count - 1)("Lan" & lan) = tb.Rows(i)("CheckTime")
                    Catch ex As Exception
                        strErr &= tb.Rows(i)("UserID") & " " & tb.Rows(i)("CheckTime") & " " & tb.Rows(i)("Name") & vbCrLf
                    End Try

                Else
                    If tb.Rows(i)("UserID") <> tb.Rows(i - 1)("UserID") Then
                        Dim dl As Integer = Convert.ToDateTime(tb.Rows(i)("CheckTime")).Day - 1
                        If dl > 0 Then
                            For k As Integer = dl To 1 Step -1
                                Dim r1 = tbc.NewRow()
                                'Dim a = Convert.ToDateTime(tb.Rows(i)("CheckTime")).DayOfWeek
                                r1("UserID") = tb.Rows(i)("UserID")
                                r1("CheckTime") = DateAdd(DateInterval.Day, -k, Convert.ToDateTime(tb.Rows(i)("CheckTime")))
                                r1("Name") = tb.Rows(i)("Name")
                                r1("DeptName") = tb.Rows(i)("DeptName")
                                'r1("Lan1") = tb.Rows(i)("CheckTime")
                                tbc.Rows.Add(r1)

                            Next
                        End If
                    End If

                    If tb.Rows(i)("UserID") <> tb.Rows(i - 1)("UserID") Then
                        Dim dr As Integer = DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, tbThang.EditValue + 1, 0)).Day - Convert.ToDateTime(tb.Rows(i - 1)("CheckTime")).Day
                        If dr > 0 Then
                            For k As Integer = 1 To dr
                                Dim r1 = tbc.NewRow()
                                'Dim a = Convert.ToDateTime(tb.Rows(i)("CheckTime")).DayOfWeek
                                r1("UserID") = tb.Rows(i - 1)("UserID")
                                r1("CheckTime") = DateAdd(DateInterval.Day, k, Convert.ToDateTime(tb.Rows(i - 1)("CheckTime")))
                                r1("Name") = tb.Rows(i - 1)("Name")
                                r1("DeptName") = tb.Rows(i - 1)("DeptName")
                                'r1("Lan1") = tb.Rows(i)("CheckTime")
                                tbc.Rows.Add(r1)

                            Next
                        End If
                    End If

                    Dim c As Integer = Convert.ToDateTime(tb.Rows(i)("CheckTime")).Day - Convert.ToDateTime(tb.Rows(i - 1)("CheckTime")).Day
                    If c > 1 Then
                        For k As Integer = 1 To c - 1
                            Dim r1 = tbc.NewRow()
                            'Dim a = Convert.ToDateTime(tb.Rows(i)("CheckTime")).DayOfWeek
                            r1("UserID") = tb.Rows(i)("UserID")
                            r1("CheckTime") = DateAdd(DateInterval.Day, k, Convert.ToDateTime(tb.Rows(i - 1)("CheckTime")))
                            r1("Name") = tb.Rows(i)("Name")
                            r1("DeptName") = tb.Rows(i)("DeptName")
                            'r1("Lan1") = tb.Rows(i)("CheckTime")
                            tbc.Rows.Add(r1)

                        Next

                    End If
                    Dim r2 = tbc.NewRow()
                    Dim a = Convert.ToDateTime(tb.Rows(i)("CheckTime")).DayOfWeek
                    r2("UserID") = tb.Rows(i)("UserID")
                    r2("CheckTime") = tb.Rows(i)("CheckTime")
                    r2("Name") = tb.Rows(i)("Name")
                    r2("DeptName") = tb.Rows(i)("DeptName")
                    r2("Lan1") = tb.Rows(i)("CheckTime")
                    tbc.Rows.Add(r2)
                    lan = 1

                End If
            Next


            gdvChamCong.DataSource = tbc
            If strErr.Trim <> "" Then
                ShowCanhBao(strErr)
            End If


        Else
            ShowBaoLoi(DbChamCong.loiNgoaiLe)
        End If
    End Sub

    Private Sub btKetXuat_Click(sender As System.Object, e As System.EventArgs) Handles btKetXuat.Click
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "TH_CHAMCONG_" & tbThang.EditValue.ToString & "_" & tbNam.EditValue.ToString & ".xls"
        saveFile.OverwritePrompt = False
        If saveFile.ShowDialog = DialogResult.OK Then

            Try
                ShowWaiting("Đang kết xuất ...")
                Utils.ExportGdv.ExportToExcel(saveFile.FileName, gdvChamCongCT)
                CloseWaiting()
                If ShowCauHoi("Mở file vừa kết xuất ?") Then
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = saveFile.FileName
                    psi.UseShellExecute = True
                    Process.Start(psi)
                End If
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            Finally
                CloseWaiting()
            End Try

        End If
    End Sub


    Private Sub btXoaCC_Click(sender As System.Object, e As System.EventArgs) Handles btXoaCC.Click
        DbChamCong.runSql("DELETE FROM Checkinout WHERE CheckTime<='#30/05/2014#'")
    End Sub
End Class