Imports BACSOFT.Db.SqlHelper

Public Class frmChamCong

    Public IPMCC As String = "192.168.1.10"
    Public PortMCC As String = "4370"

    Private Sub frmChamCong_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        cbThang.EditValue = Now.ToString("MM")
        tbNam.EditValue = Today.Year
        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Parent.Tag, DanhMucQuyen.Admin) Then
            btDongBoDuLieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub

    'Public Sub loadChamCong()
    '    Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM tblChamCong")
    '    If Not tb Is Nothing Then
    '        gdv.DataSource = tb
    '    Else
    '        ShowBaoLoi(LoiNgoaiLe)
    '    End If
    'End Sub

    'Public Sub LoadDSPhong()
    '    Dim tb As DataTable = ExecuteSQLDataTable("SELECT ID,Ten FROM DEPATMENT ORDER BY ID")
    '    If Not tb Is Nothing Then
    '        rcbPhong.DataSource = tb
    '    Else
    '        ShowBaoLoi(LoiNgoaiLe)
    '    End If
    'End Sub

    Private Sub mDongBoTenNhanVien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDongBoTenNhanVien.ItemClick
        Dim a As New zkemkeeper.CZKEM

        Try
            If Not a.Connect_Net(IPMCC, PortMCC) Then
                ShowBaoLoi("Cannot connect")
            End If
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try

        Application.DoEvents()
        Try
            ShowWaiting("Đang đồng bộ")
            Dim dsNV As DataTable = ExecuteSQLDataTable("SELECT ID, Ten FROM NHANSU WHERE TrangThai=1 AND Noictac=74")
            If dsNV Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Dim Ten As String = ""
            Dim Pass As String = ""
            Dim Pri As Integer
            Dim en As Boolean
            Dim _S As String = ""

            For i As Integer = 0 To dsNV.Rows.Count - 1
                If a.SSR_GetUserInfo(1, dsNV.Rows(i)("ID"), Ten, Pass, Pri, en) Then
                    If a.SSR_SetUserInfo(1, dsNV.Rows(i)("ID"), ChuyenKhongDau(dsNV.Rows(i)("Ten")), Pass, Pri, True) Then
                        _S += dsNV.Rows(i)("Ten") & vbCrLf
                    End If
                End If
            Next

            CloseWaiting()

            ShowThongBao("Đã đồng bộ " & vbCrLf & _S)
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        Finally
            a.Disconnect()
        End Try


    End Sub

    Public Function ChuyenKhongDau(ByVal strVietNamese As String) As String
        Dim FindText As String = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ"
        Dim ReplText As String = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY"
        Dim index As Integer = -1
        While (InlineAssignHelper(index, strVietNamese.IndexOfAny(FindText.ToCharArray()))) <> -1
            Dim index2 As Integer = FindText.IndexOf(strVietNamese(index))
            strVietNamese = strVietNamese.Replace(strVietNamese(index), ReplText(index2))
        End While
        Return strVietNamese
    End Function

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As t, value As t) As t
        target = value
        Return value
    End Function

    Private Sub mDongBoDuLieuChamCong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mDongBoDuLieuChamCong.ItemClick
        Dim a As New zkemkeeper.CZKEM

        Try
            If Not a.Connect_Net(IPMCC, PortMCC) Then
                ShowBaoLoi("Cannot connect")
            End If
            a.RegEvent(1, 65535)
        Catch ex As Exception
            ShowBaoLoi(ex.Message)
        End Try
        Try
            ShowWaiting("Đang tải dữ liệu ...")

            Dim dwEnrollNumber As String
            Dim dwVerifyMode As Long
            Dim dwInOutMode As Long

            Dim i As Integer
            Dim dwMachineNum, dwEMachineNum, dwYear, dwMonth, dwDay, dwHour, dwMinute,
            dwSecond, dwWorkcode, dwReserved As Integer
            Dim errCount As Integer = 0
            Dim NgayDauThang As DateTime = New DateTime(tbNam.EditValue, cbThang.EditValue, 1)
            Dim NgayDauThangSau As DateTime = New DateTime(tbNam.EditValue, cbThang.EditValue + 1, 1)
            Dim timeATT As DateTime
            'a.u
            a.EnableDevice(1, False)


            If a.ReadGeneralLogData(1) Then
                While a.SSR_GetGeneralLogData(1, dwEnrollNumber, dwVerifyMode, dwInOutMode, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond, dwWorkcode)
                    'Application.DoEvents()
                    'gdvCT.AddNewRow()
                    'gdvCT.SetFocusedRowCellValue("IDNhanVien", dwEnrollNumber)
                    'gdvCT.SetFocusedRowCellValue("ThoiGian", New DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond))

                    timeATT = New DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond)
                    If timeATT.Date >= NgayDauThang.Date And timeATT.Date < NgayDauThangSau Then
                        AddParameter("@IDNhanVien", dwEnrollNumber)
                        AddParameter("@ThoiGian", New DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond))
                        If doInsert("tblChamCong") Is Nothing Then
                            errCount += 1
                        End If
                    End If

                End While
            End If
            If errCount > 0 Then
                ShowBaoLoi(errCount)
            End If
            a.EnableDevice(1, True)
            a.Disconnect()
            '  loadChamCong()
            CloseWaiting()
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try
        


    End Sub

    Private Sub btXem_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXem.ItemClick
        Dim sql As String = ""
        sql &= " SELECT NHANSU.ID,(NHANSU.Ten + ' - ' + Convert(nvarchar,NHANSU.ID))Ten,tblChamCong.ThoiGian,DEPATMENT.Ten AS Phong"
        sql &= " FROM tblChamCong"
        sql &= " INNER JOIN NHANSU ON tblChamCong.IDNhanVien=NHANSU.ID"
        sql &= " INNER JOIN DEPATMENT ON NHANSU.IDDepatment=DEPATMENT.ID"
        sql &= " WHERE Month(tblChamCong.ThoiGian)=" & cbThang.EditValue & " AND Year(tblChamCong.ThoiGian)=" & tbNam.EditValue
        sql &= " ORDER BY tblChamCong.IDNhanVien,tblChamCong.ThoiGian"

        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            tb.Columns.Add("Lan1", GetType(DateTime))
            tb.Columns.Add("Lan2", GetType(DateTime))
            tb.Columns.Add("Lan3", GetType(DateTime))
            tb.Columns.Add("Lan4", GetType(DateTime))
            tb.Columns.Add("Lan5", GetType(DateTime))
            tb.Columns.Add("Lan6", GetType(DateTime))
            Dim tbc = tb.Clone
            Dim r = tbc.NewRow()

            r("ID") = tb.Rows(0)("ID")
            r("ThoiGian") = tb.Rows(0)("ThoiGian")
            r("Ten") = tb.Rows(0)("Ten")
            r("Phong") = tb.Rows(0)("Phong")
            r("Lan1") = tb.Rows(0)("ThoiGian")
            tbc.Rows.Add(r)
            Dim lan As Integer = 1
            Dim strErr As String = ""
            For i As Integer = 1 To tb.Rows.Count - 1
                If Convert.ToDateTime(tb.Rows(i)("ThoiGian")).Date = Convert.ToDateTime(tb.Rows(i - 1)("ThoiGian")).Date And tb.Rows(i)("ID") = tb.Rows(i - 1)("ID") Then
                    lan += 1
                    Try
                        tbc.Rows(tbc.Rows.Count - 1)("Lan" & lan) = tb.Rows(i)("ThoiGian")
                    Catch ex As Exception
                        strErr &= tb.Rows(i)("ID") & " " & tb.Rows(i)("ThoiGian") & " " & tb.Rows(i)("Ten") & vbCrLf
                    End Try

                Else
                    If tb.Rows(i)("ID") <> tb.Rows(i - 1)("ID") Then
                        Dim dl As Integer = Convert.ToDateTime(tb.Rows(i)("ThoiGian")).Day - 1
                        If dl > 0 Then
                            For k As Integer = dl To 1 Step -1
                                Dim r1 = tbc.NewRow()
                                'Dim a = Convert.ToDateTime(tb.Rows(i)("ThoiGian")).DayOfWeek
                                r1("ID") = tb.Rows(i)("ID")
                                r1("ThoiGian") = DateAdd(DateInterval.Day, -k, Convert.ToDateTime(tb.Rows(i)("ThoiGian")))
                                r1("Ten") = tb.Rows(i)("Ten")
                                r1("Phong") = tb.Rows(i)("Phong")
                                'r1("Lan1") = tb.Rows(i)("ThoiGian")
                                tbc.Rows.Add(r1)

                            Next
                        End If
                    End If

                    If tb.Rows(i)("ID") <> tb.Rows(i - 1)("ID") Then
                        Dim dr As Integer = DateAdd(DateInterval.Day, 0, DateSerial(tbNam.EditValue, CType(cbThang.EditValue, Integer) + 1, 0)).Day - Convert.ToDateTime(tb.Rows(i - 1)("ThoiGian")).Day
                        If dr > 0 Then
                            For k As Integer = 1 To dr
                                Dim r1 = tbc.NewRow()
                                'Dim a = Convert.ToDateTime(tb.Rows(i)("ThoiGian")).DayOfWeek
                                r1("ID") = tb.Rows(i - 1)("ID")
                                r1("ThoiGian") = DateAdd(DateInterval.Day, k, Convert.ToDateTime(tb.Rows(i - 1)("ThoiGian")))
                                r1("Ten") = tb.Rows(i - 1)("Ten")
                                r1("Phong") = tb.Rows(i - 1)("Phong")
                                'r1("Lan1") = tb.Rows(i)("ThoiGian")
                                tbc.Rows.Add(r1)

                            Next
                        End If
                    End If

                    Dim c As Integer = Convert.ToDateTime(tb.Rows(i)("ThoiGian")).Day - Convert.ToDateTime(tb.Rows(i - 1)("ThoiGian")).Day
                    If c > 1 Then
                        For k As Integer = 1 To c - 1
                            Dim r1 = tbc.NewRow()
                            'Dim a = Convert.ToDateTime(tb.Rows(i)("ThoiGian")).DayOfWeek
                            r1("ID") = tb.Rows(i)("ID")
                            r1("ThoiGian") = DateAdd(DateInterval.Day, k, Convert.ToDateTime(tb.Rows(i - 1)("ThoiGian")))
                            r1("Ten") = tb.Rows(i)("Ten")
                            r1("Phong") = tb.Rows(i)("Phong")
                            'r1("Lan1") = tb.Rows(i)("ThoiGian")
                            tbc.Rows.Add(r1)

                        Next

                    End If
                    Dim r2 = tbc.NewRow()
                    Dim a = Convert.ToDateTime(tb.Rows(i)("ThoiGian")).DayOfWeek
                    r2("ID") = tb.Rows(i)("ID")
                    r2("ThoiGian") = tb.Rows(i)("ThoiGian")
                    r2("Ten") = tb.Rows(i)("Ten")
                    r2("Phong") = tb.Rows(i)("Phong")
                    r2("Lan1") = tb.Rows(i)("ThoiGian")
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

    Private Sub mKetXuat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mKetXuat.ItemClick
        Dim saveFile As New SaveFileDialog
        saveFile.Filter = "Excel File|*.xls"
        saveFile.FileName = "TH_CHAMCONG_" & cbThang.EditValue & "_" & tbNam.EditValue.ToString & ".xls"
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
End Class