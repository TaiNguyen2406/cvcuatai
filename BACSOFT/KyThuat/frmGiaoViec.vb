Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository

Public Class frmGiaoViec
    Public NVThamGia As New ArrayList
    Public SoYC As Object
    Public _exit As Boolean = False


    Private Sub frmGiaoViec_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadDSNoiDungThiCong()
        LoadDSNVThiCong()
        LoadDSachGiaoViec()
    End Sub

    Public Sub loadDSNoiDungThiCong()
        AddParameterWhere("@Loai", LoaiTuDien.NoiDungThiCong)
        AddParameterWhere("@Loai2", LoaiTuDien.NhomNoiDungThiCong)
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT tblTuDien.ID,tblTuDien.NoiDung,tbTmp.NoiDung AS Nhom FROM tblTuDien LEFT JOIN tblTuDien as tbTmp ON tblTuDien.IDP=tbTmp.ID and tbTmp.Loai=@loai2 WHERE tblTuDien.Loai=@Loai ORDER BY tbTmp.Ma,tblTuDien.Ma ")
        If Not tb Is Nothing Then
            rgdvHangMucThiCong.DataSource = tb

            With rgdvHangMucThiCong.View.Columns
                Dim colID = .AddField("ID")
                colID.Caption = "ID"
                colID.Visible = False
                Dim colNoiDung = .AddField("NoiDung")
                colNoiDung.Caption = "Nội dung"
                colNoiDung.VisibleIndex = 1
                colNoiDung.Width = 120
                colNoiDung.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                Dim colNhom = .AddField("Nhom")
                colNhom.Caption = "Nhóm"
                colNhom.VisibleIndex = 2
                colNhom.Width = 250
                colNhom.GroupIndex = 0
            End With
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub LoadDSNVThiCong()
        Dim sql As String = ""
        sql &= " SELECT 'PB'+Convert(nvarchar, ID)ID,Ten FROM DEPATMENT "
        sql &= " SELECT ID,Ten,'PB'+Convert(nvarchar, IDDepatment)IDDepatment"
        sql &= " FROM NHANSU WHERE NHANSU.Noictac=74 AND NHANSU.Trangthai=1 ORDER BY IDDepatment,IDBoPhan,ChucVu"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        treeNV.Nodes.Clear()
        If Not ds Is Nothing Then

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim item As New Utils.ItemObject(ds.Tables(0).Rows(i)("ID"), ds.Tables(0).Rows(i)("Ten"))
                Dim nodeCha As TreeListNode = Nothing
                Dim node As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item.Value, item.Name)}, nodeCha)
                If NVThamGia.Contains(item.Value) Then node.Checked = True
                'If deskTop.BarMenu.ItemLinks.Item(i).ToString = "DevExpress.XtraBars.BarSubItemLink" Then
                For j As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    If ds.Tables(1).Rows(j)("IDDepatment") = ds.Tables(0).Rows(i)("ID") Then
                        Dim item2 As New Utils.ItemObject(ds.Tables(1).Rows(j)("ID"), ds.Tables(1).Rows(j)("Ten"))
                        Dim nodecon As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item2.Value, item2.Name)}, node)
                        If NVThamGia.Contains(item2.Value.ToString) Then nodecon.Checked = True
                    End If
                Next
                'End If
            Next
            treeNV.ExpandAll()
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Public Sub LoadDSachGiaoViec()
        Dim sql As String = ""
        sql &= " SELECT ('')NgThucHien,('')NgThongBao,('')NVKiemDuyetLan1,('')NVKiemDuyetLan2,"
        sql &= " ID,NgayBatDau,NgayKetThuc,SoYC,IDNoiDung,MoTa,IDNgThucHien,IDNgThongBao,IDNgNhap,NgayNhap,IDNgDuyet,Duyet,NgayDuyet,IDNgKiemDuyet1,IDNgKiemDuyet2,GiaoViec,AZ"
        sql &= " FROM tblBaoCaoLichThiCong "
        sql &= " WHERE GiaoViec=1 AND SoYC=@SoYC"
        sql &= " ORDER BY AZ "
        AddParameterWhere("@SoYC", SoYC)
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            With tb
                For i As Integer = 0 To .Rows.Count - 1
                    .Rows(i)("AZ") = i + 1
                    Dim tb2 As DataTable = DataSourceDSFile(.Rows(i)("IDNgThucHien").ToString, , ",")
                    .Rows(i)("NgThucHien") = ""
                    For j As Integer = 0 To tb2.Rows.Count - 1
                        AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                        Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb3 Is Nothing Then
                            .Rows(i)("NgThucHien") &= "- " & tb3.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NgThucHien") = .Rows(i)("NgThucHien").ToString.Trim

                    Dim tb9 As DataTable = DataSourceDSFile(.Rows(i)("IDNgThongBao").ToString, , ",")
                    .Rows(i)("NgThongBao") = ""
                    For j As Integer = 0 To tb9.Rows.Count - 1
                        AddParameterWhere("@ID", tb9.Rows(j)("File").ToString)
                        Dim tb8 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb8 Is Nothing Then
                            .Rows(i)("NgThongBao") &= "- " & tb8.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NgThongBao") = .Rows(i)("NgThongBao").ToString.Trim


                    Dim tb4 As DataTable = DataSourceDSFile(.Rows(i)("IDNgKiemDuyet1").ToString, , ",")
                    .Rows(i)("NVKiemDuyetLan1") = ""
                    For j As Integer = 0 To tb4.Rows.Count - 1
                        AddParameterWhere("@ID", tb4.Rows(j)("File").ToString)
                        Dim tb5 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb5 Is Nothing Then
                            .Rows(i)("NVKiemDuyetLan1") &= "- " & tb5.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NVKiemDuyetLan1") = .Rows(i)("NVKiemDuyetLan1").ToString.Trim

                    Dim tb6 As DataTable = DataSourceDSFile(.Rows(i)("IDNgKiemDuyet2").ToString, , ",")
                    .Rows(i)("NVKiemDuyetLan2") = ""
                    For j As Integer = 0 To tb6.Rows.Count - 1
                        AddParameterWhere("@ID", tb6.Rows(j)("File").ToString)
                        Dim tb7 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                        If Not tb7 Is Nothing Then
                            .Rows(i)("NVKiemDuyetLan2") &= "- " & tb7.Rows(0)(0).ToString & vbCrLf
                        End If
                    Next
                    .Rows(i)("NVKiemDuyetLan2") = .Rows(i)("NVKiemDuyetLan2").ToString.Trim
                Next
            End With
            gdvThiCong.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub rgdvHangMucThiCong_Popup(sender As System.Object, e As System.EventArgs) Handles rgdvHangMucThiCong.Popup
        CType(sender, GridLookUpEdit).Properties.View.ExpandAllGroups()
    End Sub

    Private Sub rPopUpDSThiCong_Popup(sender As System.Object, e As System.EventArgs) Handles rPopUpDSThiCong.Popup
        NVThamGia.Clear()
        If CType(sender, PopupContainerEdit).EditValue.ToString <> "" Then
            NVThamGia.AddRange(CType(sender, PopupContainerEdit).EditValue.ToString.Split(","))
        End If
        LoadDSNVThiCong()
    End Sub

    Private Sub gdvThiCongCT_InitNewRow(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gdvThiCongCT.InitNewRow
        gdvThiCongCT.SetFocusedRowCellValue("AZ", Convert.ToInt32(gdvThiCongCT.GetRowCellValue(gdvThiCongCT.RowCount - 2, "AZ")) + 1)
        gdvThiCongCT.SetFocusedRowCellValue("NgayBatDau", New DateTime(Today.Year, Today.Month, Today.Day, 8, 0, 0))
        gdvThiCongCT.SetFocusedRowCellValue("NgayKetThuc", New DateTime(Today.Year, Today.Month, Today.Day, 17, 0, 0))
    End Sub

    Private Sub gdvThiCongCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvThiCongCT.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.Delete Then
            If gdvThiCongCT.FocusedRowHandle < 0 Then Exit Sub
            If ShowCauHoi("Xoá dòng hiện tại ?") Then
                If Not gdvThiCongCT.GetFocusedRowCellValue("ID") Is Nothing Then
                    AddParameterWhere("@ID", gdvThiCongCT.GetFocusedRowCellValue("ID"))
                    If doDelete("tblBaoCaoLichThiCong", "ID=@ID") Is Nothing Then
                        ShowBaoLoi(LoiNgoaiLe)
                    Else
                        CType(gdvThiCong.Views.Item(0).DataSource, DataView).Table.Rows.RemoveAt(gdvThiCongCT.GetFocusedDataSourceRowIndex)
                    End If
                End If
            End If
        ElseIf e.Control AndAlso e.KeyCode = Keys.Up Then
            If gdvThiCongCT.FocusedRowHandle = 0 Or gdvThiCongCT.FocusedRowHandle < 0 Then Exit Sub
            Dim _tmp As Object = gdvThiCongCT.GetFocusedRowCellValue("AZ")
            gdvThiCongCT.SetFocusedRowCellValue("AZ", gdvThiCongCT.GetRowCellValue(gdvThiCongCT.FocusedRowHandle - 1, "AZ"))
            gdvThiCongCT.SetRowCellValue(gdvThiCongCT.FocusedRowHandle - 1, "AZ", _tmp)
            gdvThiCongCT.FocusedRowHandle += 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.Down Then
            If gdvThiCongCT.FocusedRowHandle = gdvThiCongCT.RowCount - 2 Or gdvThiCongCT.FocusedRowHandle < 0 Then Exit Sub
            Dim _tmp As Object = gdvThiCongCT.GetFocusedRowCellValue("AZ")
            gdvThiCongCT.SetFocusedRowCellValue("AZ", gdvThiCongCT.GetRowCellValue(gdvThiCongCT.FocusedRowHandle + 1, "AZ"))
            gdvThiCongCT.SetRowCellValue(gdvThiCongCT.FocusedRowHandle + 1, "AZ", _tmp)
            gdvThiCongCT.FocusedRowHandle -= 1
        End If
    End Sub

    Private Sub rPopUpDSThiCong_Closed(sender As System.Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles rPopUpDSThiCong.Closed
        Dim _NV As String = ","
        Dim _NV2 As String = ""
        For i As Integer = 0 To treeNV.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNV.Nodes(i)
            'If nod1.Checked  Then strNVThamGia &= CType(nod1(0), Utils.ItemObject).Value & ","
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                If nod2.Checked Then
                    _NV &= CType(nod2(0), Utils.ItemObject).Value & ","
                    _NV2 &= "- " & CType(nod2(0), Utils.ItemObject).Name & vbCrLf
                End If

            Next
        Next
        Dim _C1 As String = ""
        Dim _C2 As String = ""

        Select Case gdvThiCongCT.FocusedColumn.FieldName
            Case "IDNgThucHien"
                _C1 = "IDNgThucHien"
                _C2 = "NgThucHien"
            Case "IDNgThongBao"
                _C1 = "IDNgThongBao"
                _C2 = "NgThongBao"
            Case "IDNgKiemDuyet1"
                _C1 = "IDNgKiemDuyet1"
                _C2 = "NVKiemDuyetLan1"
            Case "IDNgKiemDuyet2"
                _C1 = "IDNgKiemDuyet2"
                _C2 = "NVKiemDuyetLan2"
        End Select

        If _NV <> "," Then
            gdvThiCongCT.SetFocusedRowCellValue(_C1, _NV)
            gdvThiCongCT.SetFocusedRowCellValue(_C2, _NV2.Trim)
        Else
            gdvThiCongCT.SetFocusedRowCellValue(_C1, Nothing)
            gdvThiCongCT.SetFocusedRowCellValue(_C2, Nothing)
        End If

    End Sub


    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub btLuuLai_Click(sender As System.Object, e As System.EventArgs) Handles btLuuLai.Click
        gdvThiCongCT.CloseEditor()
        gdvThiCongCT.UpdateCurrentRow()
        Try
            Dim NgayXuLy As DateTime = GetServerTime()
            '    BeginTransaction()

            With gdvThiCongCT
                For i As Integer = 0 To .RowCount - 2
                    AddParameter("@SoYC", SoYC)
                    AddParameter("@IDNoiDung", .GetRowCellValue(i, "IDNoiDung"))
                    AddParameter("@MoTa", .GetRowCellValue(i, "MoTa"))
                    AddParameter("@NgayBatDau", .GetRowCellValue(i, "NgayBatDau"))
                    AddParameter("@NgayKetThuc", .GetRowCellValue(i, "NgayKetThuc"))
                    AddParameter("@IDNgThucHien", .GetRowCellValue(i, "IDNgThucHien"))
                    AddParameter("@IDNgThongBao", .GetRowCellValue(i, "IDNgThongBao"))
                    AddParameter("@IDNgKiemDuyet1", .GetRowCellValue(i, "IDNgKiemDuyet1"))
                    AddParameter("@IDNgKiemDuyet2", .GetRowCellValue(i, "IDNgKiemDuyet2"))
                    AddParameter("@GiaoViec", 1)
                    AddParameter("@AZ", .GetRowCellValue(i, "AZ"))
                    If IsDBNull(.GetRowCellValue(i, "ID")) Or .GetRowCellValue(i, "ID") Is Nothing Then
                        AddParameter("@IDNgNhap", TaiKhoan)
                        AddParameter("@NgayNhap", NgayXuLy)
                        Dim _ID As Object = doInsert("tblBaoCaoLichThiCong")
                        If _ID Is Nothing Then Throw New Exception(LoiNgoaiLe)
                        .SetRowCellValue(i, "ID", _ID)
                    Else
                        AddParameterWhere("@ID", .GetRowCellValue(i, "ID"))
                        If doUpdate("tblBaoCaoLichThiCong", "ID=@ID") Is Nothing Then Throw New Exception(LoiNgoaiLe)
                    End If

                    Dim sql As String = ""

                    sql &= " SELECT KHACHHANG.ttcMa,tblBaoCaoLichThiCong.SoYC,tblBaoCaoLichThiCong.SoCG,NGD.Ten AS NguoiGD, NGD.Mobile AS DTNgd,"
                    sql &= " KHACHHANG.Ten AS TenKH, KHACHHANG.ttcDiaChi,tblTuDien.NoiDung AS NhomCV,tblBaoCaoLichThiCong.NgayBatDau,tblBaoCaoLichThiCong.NgayKetThuc,"
                    sql &= " tblBaoCaoLichThiCong.MoTa,KD.Ten AS PhuTrach"
                    sql &= " FROM tblBaoCaoLichThiCong "
                    sql &= " INNER JOIN BANGYEUCAU ON tblBaoCaoLichThiCong.SoYC =BANGYEUCAU.SoPhieu"
                    sql &= " INNER JOIN KHACHHANG ON BANGYEUCAU.IDKhachHang=KHACHHANG.ID"
                    sql &= " LEFT JOIN NHANSU AS NGD ON NGD.ID=BANGYEUCAU.IDNgd"
                    sql &= " LEFT JOIN NHANSU AS KD ON KD.ID=BANGYEUCAU.IDTakeCare"
                    sql &= " LEFT JOIN tblTuDien ON tblBaoCaoLichThiCong.IDNoiDung=tblTuDien.ID AND tblTuDien.Loai=6 "
                    sql &= " WHERE GIaoViec=1 AND tblBaoCaoLichThiCong.ID=" & .GetRowCellValue(i, "ID")
                    Dim tb As DataTable = ExecuteSQLDataTable(sql)
                    Dim str As String = ""
                    If Not tb Is Nothing Then
                        If Convert.ToDateTime(tb.Rows(0)("NgayKetThuc")) > Now Then
                            Dim subj As String = ""

                            str &= "- Mã KH:  " & tb.Rows(0)("ttcMa") & " - YC: " & tb.Rows(0)("SoYC") & " - CG: " & tb.Rows(0)("SoCG") & " - Người GD: " & tb.Rows(0)("NguoiGD") & " " & tb.Rows(0)("DTNgd")
                            str &= vbCrLf & "- Tên KH: " & tb.Rows(0)("TenKH")
                            str &= vbCrLf & "- Đ/c KH: " & tb.Rows(0)("ttcDiaChi")
                            str &= vbCrLf & "- Công việc: " & tb.Rows(0)("NhomCV")
                            str &= vbCrLf & "- Thời gian: " & Convert.ToDateTime(tb.Rows(0)("NgayBatDau")).ToString("HH:mm dd/MM/yyyy") & " -> " & Convert.ToDateTime(tb.Rows(0)("NgayKetThuc")).ToString("HH:mm dd/MM/yyyy")
                            subj = tb.Rows(0)("NhomCV") & " từ: " & Convert.ToDateTime(tb.Rows(0)("NgayBatDau")).ToString("HH:mm dd/MM/yyyy") & " -> " & Convert.ToDateTime(tb.Rows(0)("NgayKetThuc")).ToString("HH:mm dd/MM/yyyy")
                            str &= vbCrLf & "- Nội dung chi tiết: " & tb.Rows(0)("Mota")
                            str &= vbCrLf & "- NV Kinh doanh: " & tb.Rows(0)("PhuTrach")
                            str &= vbCrLf & "- NV Thực hiện: "
                            Dim tb2 As DataTable = DataSourceDSFile(.GetRowCellValue(i, "IDNgThucHien").ToString, , ",")
                            Dim tb31 As DataTable = DataSourceDSFile(.GetRowCellValue(i, "IDNgThongBao").ToString, , ",")
                            For j As Integer = 0 To tb2.Rows.Count - 1
                                AddParameterWhere("@ID", tb2.Rows(j)("File").ToString)
                                Dim tb3 As DataTable = ExecuteSQLDataTable("SELECT Ten FROM NHANSU WHERE ID=@ID")
                                If Not tb3 Is Nothing Then
                                    str &= vbCrLf & "  . " & tb3.Rows(0)(0).ToString
                                End If
                            Next

                            Dim strThucHien As String = .GetRowCellValue(i, "IDNgThucHien").ToString.Trim
                            If strThucHien.StartsWith(",") Then
                                strThucHien = strThucHien.Substring(1, strThucHien.Length - 1)
                            End If

                            If strThucHien.EndsWith(",") Then
                                strThucHien = strThucHien.Substring(0, strThucHien.Length - 1)
                            End If

                            Dim strThongBao As String = .GetRowCellValue(i, "IDNgThongBao").ToString.Trim
                            If strThongBao.StartsWith(",") Then
                                strThongBao = strThongBao.Substring(1, strThongBao.Length - 1)
                            End If

                            If strThongBao.EndsWith(",") Then
                                strThongBao = strThongBao.Substring(0, strThongBao.Length - 1)
                            End If

                            ' Dim isSendM As Boolean = False
                            ' ShowAlert(strThucHien)
                            'For j As Integer = 0 To tb2.Rows.Count - 1
                            '    AddParameterWhere("@ND", str)
                            '    AddParameterWhere("@IDNV", tb2.Rows(j)("File").ToString)
                            '    Dim tb4 As DataTable = ExecuteSQLDataTable("SELECT COUNT(ID) FROM tblThongBao WHERE NoiDung=@ND AND IDNhanVien=@IDNV")
                            '    If Not tb4 Is Nothing Then
                            '        If tb4.Rows(0)(0) = 0 Then
                            '            ThemThongBaoChoNV(str, tb2.Rows(j)("File").ToString)
                            '            isSendM = True

                            '        End If
                            '    End If
                            'Next
                            For j As Integer = 0 To tb2.Rows.Count - 1
                                ThemThongBaoChoNV(str, tb2.Rows(j)("File").ToString)
                            Next
                            For j As Integer = 0 To tb31.Rows.Count - 1
                                ThemThongBaoChoNV(str, tb31.Rows(j)("File").ToString)
                            Next

                            Dim tb5 As DataTable = ExecuteSQLDataTable("select  Email from NHANSU where Id IN (" & strThucHien & ") AND rtrim(ltrim(Email))<>'' ")
                            Dim tb6 As DataTable = ExecuteSQLDataTable("select  Email from NHANSU where Id IN (" & strThongBao & ") AND rtrim(ltrim(Email))<>'' ")
                            If Not tb5 Is Nothing Then
                                If tb6 IsNot Nothing Then
                                    Utils.Email.SendToList(tb5, subj, str, , , tb6)
                                Else
                                    Utils.Email.SendToList(tb5, subj, str)
                                End If

                            End If

                        End If



                    Else
                        ShowBaoLoi(LoiNgoaiLe)
                    End If


                Next
            End With
            '   ComitTransaction()
            ShowAlert("Đã lưu lại !")

        Catch ex As Exception
            '   RollBackTransaction()
            ShowBaoLoi(LoiNgoaiLe)
        End Try

    End Sub



    Private Sub gdvThiCongCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvThiCongCT.CellValueChanged
        If e.Column.FieldName = "NgayBatDau" Then
            If _exit Then Exit Sub
            If Convert.ToDateTime(e.Value).Hour = 0 And Convert.ToDateTime(e.Value).Minute = 0 Then
                _exit = True
                gdvThiCongCT.SetFocusedRowCellValue("NgayBatDau", Convert.ToDateTime(e.Value).AddHours(7).AddMinutes(30))
                _exit = False
            End If
        ElseIf e.Column.FieldName = "NgayKetThuc" Then
            If _exit Then Exit Sub
            If Convert.ToDateTime(e.Value).Hour = 0 And Convert.ToDateTime(e.Value).Minute = 0 Then
                _exit = True
                gdvThiCongCT.SetFocusedRowCellValue("NgayKetThuc", Convert.ToDateTime(e.Value).AddHours(7).AddMinutes(30))
                _exit = False
            End If
        End If
    End Sub
End Class