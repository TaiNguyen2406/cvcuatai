Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraTreeList.Nodes

Public Class frmCNLichLamViec
    Public NVThamGia As New ArrayList
    Public NVThongBao As New ArrayList
    Public CongTrinh As Boolean = False
    Public IDUser As Int32

    Private Sub frmCNLichLamViec_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If TrangThai.isAddNew Then
            Me.Text = "Thêm công việc mới"
            tbNgayBatDau.EditValue = deskTop.SchLichLamViec.SelectedInterval.Start.Date
            tbNgayKetThuc.EditValue = deskTop.SchLichLamViec.SelectedInterval.End.Date
            tbGioBatDau.EditValue = deskTop.SchLichLamViec.SelectedInterval.Start.TimeOfDay
            tbGioKetThuc.EditValue = deskTop.SchLichLamViec.SelectedInterval.End.TimeOfDay
            cbDoKhanCap.SelectedIndex = 0
            cbDoQuanTrong.SelectedIndex = 0
            NVThamGia.Clear()
            NVThamGia.AddRange(TaiKhoan.ToString.Split(","))
            NVThongBao.Clear()
            ' NVThongBao.AddRange(TaiKhoan.ToString.Split(","))
        Else


            Me.Text = "Cập nhật thông tin công việc"
            Dim sql As String = ""
            AddParameterWhere("@ID", objID)
            If CongTrinh Then
                sql &= " SELECT BANGCHAOGIA.Tenduan AS TieuDe, KHACHHANG.ttcMa AS DiaDiem,(0)DoQuanTrong,(0)DoKhanCap,"
                sql &= "     TGBatDau AS BatDau,TGKetThuc AS KetThuc,NoiDung, BANGCHAOGIA.IDNgXuLy AS IDUser, NVThucHien AS NguoiThucHien,NguoiThongBao"
                sql &= " FROM CHAOGIAAUX INNER JOIN BANGCHAOGIA ON BANGCHAOGIA.Sophieu=CHAOGIAAUX.Sophieu"
                sql &= "     INNER JOIN KHACHHANG ON BANGCHAOGIA.IDKhachhang=KHACHHANG.ID"
                sql &= " WHERE CHAOGIAAUX.ID=@ID"
            Else
                sql = "SELECT * FROM tblLichLamViec WHERE ID=@ID"
            End If

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tbTieuDe.EditValue = tb.Rows(0)("TieuDe")
                tbDiaDiem.EditValue = tb.Rows(0)("DiaDiem")
                tbNgayBatDau.EditValue = CType(tb.Rows(0)("BatDau"), DateTime).Date
                tbGioBatDau.EditValue = CType(tb.Rows(0)("BatDau"), DateTime).TimeOfDay
                tbNgayKetThuc.EditValue = CType(tb.Rows(0)("KetThuc"), DateTime).Date
                tbGioKetThuc.EditValue = CType(tb.Rows(0)("KetThuc"), DateTime).TimeOfDay
                If Not IsDBNull(tb.Rows(0)("DoKhanCap")) Then
                    cbDoKhanCap.SelectedIndex = CType(tb.Rows(0)("DoKhanCap"), Byte)
                End If

                tbNoiDung.EditValue = tb.Rows(0)("NoiDung")
                If Not IsDBNull(tb.Rows(0)("DoQuanTrong")) Then
                    cbDoQuanTrong.SelectedIndex = CType(tb.Rows(0)("DoQuanTrong"), Byte)
                End If
                NVThamGia.Clear()
                If CongTrinh Then
                    NVThamGia.AddRange(tb.Rows(0)("NguoiThucHien").ToString.Split(";"))
                    NVThongBao.AddRange(tb.Rows(0)("NguoiThongBao").ToString.Split(";"))
                Else
                    NVThamGia.AddRange(tb.Rows(0)("NguoiThucHien").ToString.Split(","))
                    NVThongBao.AddRange(tb.Rows(0)("NguoiThongBao").ToString.Split(","))
                End If
                If CType(tbNgayBatDau.EditValue, DateTime).Add(Convert.ToDateTime(tbGioBatDau.Text).TimeOfDay) < GetServerTime() Then
                    tbNgayBatDau.Enabled = False
                    tbNgayKetThuc.Enabled = False
                    tbGioBatDau.Enabled = False
                    tbGioKetThuc.Enabled = False
                End If
            End If

            If CongTrinh Then
                btLuu.Enabled = False
                btLuuVaThem.Enabled = False
                cbDoQuanTrong.Properties.ReadOnly = True
                cbDoKhanCap.Properties.ReadOnly = True
                tbTieuDe.Properties.ReadOnly = True
                tbDiaDiem.Properties.ReadOnly = True
                tbGioBatDau.Properties.ReadOnly = True
                tbGioKetThuc.Properties.ReadOnly = True
                tbNoiDung.Properties.ReadOnly = True
                tbNgayBatDau.Properties.ReadOnly = True
                tbNgayKetThuc.Properties.ReadOnly = True
            End If
        End If
        LoadDSNhanVien()
        GanSoureChoGridFile()
    End Sub

    Public Sub GanSoureChoGridFile()
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvListFile.DataSource = tb
    End Sub

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        gFileDinhKem.Visible = False
        GanSoureChoGridFile()
        'If IDUser <> Nothing And IDUser <> Convert.ToInt32(TaiKhoan) Then
        '    ShowCanhBao("Bạn không có quyền sửa lịch của người khác !")
        '    Exit Sub
        'End If
        'GhiLai()
        TrangThai.isAddNew = True
        tbTieuDe.EditValue = ""
        tbDiaDiem.EditValue = ""
        tbNoiDung.EditValue = ""
        treeNV.BeginUpdate()
        For i As Integer = 0 To treeNV.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNV.Nodes(i)
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)

                If nod2.Checked Then
                    nod2.CheckState = CheckState.Unchecked
                End If
            Next
        Next
        treeNV.EndUpdate()
        treeNVThongBao.BeginUpdate()
        For i As Integer = 0 To treeNVThongBao.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNVThongBao.Nodes(i)
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNVThongBao.Nodes(i).Nodes(j)

                If nod2.Checked Then
                    nod2.CheckState = CheckState.Unchecked
                End If
            Next
        Next
        treeNVThongBao.EndUpdate()
    End Sub

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        gFileDinhKem.Visible = False
        If IDUser <> Nothing And IDUser <> Convert.ToInt32(TaiKhoan) Then
            ShowCanhBao("Bạn không có quyền sửa lịch của người khác !")
            Exit Sub
        End If
        GhiLai()
    End Sub

    Private Sub GhiLai()
        If tbGioBatDau.Enabled = True Then
            If CType(tbNgayBatDau.EditValue, DateTime).Add(Convert.ToDateTime(tbGioBatDau.Text).TimeOfDay) < GetServerTime() Then
                ShowCanhBao("Không thể lập lịch làm việc đã qua !")
                Exit Sub
            End If
        End If
        AddParameter("@TieuDe", tbTieuDe.EditValue)
        AddParameter("@DiaDiem", tbDiaDiem.EditValue)
        AddParameter("@DoQuanTrong", cbDoQuanTrong.SelectedIndex)
        AddParameter("@BatDau", CType(tbNgayBatDau.EditValue, DateTime).Add(Convert.ToDateTime(tbGioBatDau.Text).TimeOfDay))
        AddParameter("@KetThuc", CType(tbNgayKetThuc.EditValue, DateTime).Add(Convert.ToDateTime(tbGioKetThuc.Text).TimeOfDay))
        AddParameter("@NoiDung", tbNoiDung.EditValue)
        AddParameter("@IDUser", Convert.ToInt32(TaiKhoan))
        If cbDoQuanTrong.SelectedIndex >= 0 Then
            AddParameter("@DoKhanCap", cbDoKhanCap.SelectedIndex)
        Else
            AddParameter("@DoKhanCap", DBNull.Value)
        End If

        Dim strNVThamGia As String = ","
        Dim strEmail As String = ""

        Dim strNVThongBao As String = ","
        Dim strEmailThongBao As String = ""

        For i As Integer = 0 To treeNV.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNV.Nodes(i)
            If nod1.Checked Then strNVThamGia &= CType(nod1(0), Utils.ItemObject).Value & ","
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                If nod2.Checked Then
                    strNVThamGia &= CType(nod2(0), Utils.ItemObject).Value & ","
                    strEmail &= nod2.Tag & ","
                End If
            Next
        Next
        For i As Integer = 0 To treeNVThongBao.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNVThongBao.Nodes(i)
            If nod1.Checked Then strNVThongBao &= CType(nod1(0), Utils.ItemObject).Value & ","
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNVThongBao.Nodes(i).Nodes(j)
                If nod2.Checked Then
                    strNVThongBao &= CType(nod2(0), Utils.ItemObject).Value & ","
                    strEmailThongBao &= nod2.Tag & ","
                End If
            Next
        Next

        AddParameter("@NguoiThucHien", strNVThamGia)
        AddParameter("@NguoiThongBao", strNVThongBao)
        'strEmail = strNVThamGia.Remove(strNVThamGia.Length - 1, 1)

        If TrangThai.isAddNew Then
            objID = doInsert("tblLichLamViec")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)

            Else
                deskTop.LoadLichLamViec()
                TrangThai.isUpdate = True
                Me.Text = "Cập nhật lịch làm việc"
                'If chkGuiMail.Checked Then
                '    BACSOFT.Utils.Email.SendToList(
                'End If
                ShowAlert("Đã thêm lịch làm việc !")

            End If
        Else
            AddParameterWhere("@ID", objID)
            If doUpdate("tblLichLamViec", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                deskTop.LoadLichLamViec()
                ShowAlert("Đã cập nhật lịch làm việc !")
            End If
        End If
        TaoThongBao()
        Dim strNoiDung As String = ""


        strNoiDung &= "- Thời gian: " & CType(tbNgayBatDau.EditValue, DateTime).Add(Convert.ToDateTime(tbGioBatDau.Text).TimeOfDay).ToString("HH:mm dd/MM/yyyy")
        strNoiDung &= " -> " & CType(tbNgayKetThuc.EditValue, DateTime).Add(Convert.ToDateTime(tbGioKetThuc.Text).TimeOfDay).ToString("HH:mm dd/MM/yyyy")
        strNoiDung &= " <br /> "
        If Not IsDBNull(tbDiaDiem.EditValue) And Not tbDiaDiem.EditValue Is Nothing Then
            strNoiDung &= "- Địa điểm: " & tbDiaDiem.EditValue.ToString & " <br /> "
        End If

        If tbNoiDung.EditValue <> "" Then
            strNoiDung &= "- Nội dung: " & " <br /> " & tbNoiDung.EditValue.ToString.Replace(Chr(10), " <BR /> ")
        End If

        Dim strTieuDe As String = tbTieuDe.EditValue

        If Not IsDBNull(tbDiaDiem.EditValue) And Not tbDiaDiem.EditValue Is Nothing Then
            strTieuDe &= " (Địa điểm: " & tbDiaDiem.EditValue.ToString & ")"
        End If


        If chkGuiMail.Checked And chkBCC.Checked Then
            Utils.Email.SendToList(DataSourceDSFile(strEmail, "Email", ","), strTieuDe, strNoiDung, EmailNguoiDung, CType(gdvListFile.DataSource, DataTable), DataSourceDSFile(strEmailThongBao, "Email", ","))
        ElseIf chkGuiMail.Checked And chkBCC.Checked = False Then
            Utils.Email.SendToList(DataSourceDSFile(strEmail, "Email", ","), strTieuDe, strNoiDung, , CType(gdvListFile.DataSource, DataTable), DataSourceDSFile(strEmailThongBao, "Email", ","))
        End If

    End Sub

    Public Sub TaoThongBao()
        Dim strNoiDung As String = "Lịch làm việc" & vbCrLf


        strNoiDung &= "- Thời gian: " & CType(tbNgayBatDau.EditValue, DateTime).Add(Convert.ToDateTime(tbGioBatDau.Text).TimeOfDay).ToString("HH:mm dd/MM/yyyy")
        strNoiDung &= " -> " & CType(tbNgayKetThuc.EditValue, DateTime).Add(Convert.ToDateTime(tbGioKetThuc.Text).TimeOfDay).ToString("HH:mm dd/MM/yyyy")
        strNoiDung &= vbCrLf
        strNoiDung &= "Tiêu đề: " & tbTieuDe.EditValue & vbCrLf

        If Not IsDBNull(tbDiaDiem.EditValue) And Not tbDiaDiem.EditValue Is Nothing Then
            strNoiDung &= "- Địa điểm: " & tbDiaDiem.EditValue.ToString & vbCrLf
        End If

        If tbNoiDung.EditValue <> "" Then
            strNoiDung &= "- Nội dung: " & vbCrLf & tbNoiDung.EditValue.ToString
        End If

        For i As Integer = 0 To treeNV.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNV.Nodes(i)
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                If nod2.Checked Then
                    ThemThongBaoChoNV(strNoiDung, CType(nod2(0), Utils.ItemObject).Value)
                End If
            Next
        Next

        For i As Integer = 0 To treeNVThongBao.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNVThongBao.Nodes(i)
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNVThongBao.Nodes(i).Nodes(j)
                If nod2.Checked Then
                    ThemThongBaoChoNV(strNoiDung, CType(nod2(0), Utils.ItemObject).Value)
                End If
            Next
        Next
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub cbTrangThai_Properties_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbDoQuanTrong.Properties.ButtonClick
        If e.Button.Index = 1 Then
            cbDoQuanTrong.EditValue = Nothing
        End If
    End Sub

    Private Sub LoadDSNhanVien()
        Dim sql As String = ""
        sql &= " SELECT 'PB'+Convert(nvarchar, ID)ID,Ten FROM DEPATMENT "
        sql &= " SELECT NHANSU.ID,NHANSU.Ten,'PB'+Convert(nvarchar, NHANSU.IDDepatment)IDDepatment,Email"
        sql &= " FROM NHANSU "
        sql &= " LEFT JOIN NhanSu_BoPhan ON NhanSu_BoPhan.Ma=NhanSu.IDBoPhan"
        sql &= " WHERE NHANSU.Noictac=74 AND NHANSU.Trangthai=1 "
        sql &= " ORDER BY NHanSu.IDDepatment,NhanSu_BoPhan.MaBP,NHANSU.ChucVu,NhanSU.ID"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)

        If Not ds Is Nothing Then

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim item As New Utils.ItemObject(ds.Tables(0).Rows(i)("ID"), ds.Tables(0).Rows(i)("Ten"))
                Dim nodeCha As TreeListNode = Nothing
                Dim node As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item.Value, item.Name)}, nodeCha)
                'node.Tag = ds.Tables(0).Rows(i)("Email")
                If NVThamGia.Contains(item.Value) Then node.Checked = True
                'If deskTop.BarMenu.ItemLinks.Item(i).ToString = "DevExpress.XtraBars.BarSubItemLink" Then
                For j As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    If ds.Tables(1).Rows(j)("IDDepatment") = ds.Tables(0).Rows(i)("ID") Then
                        Dim item2 As New Utils.ItemObject(ds.Tables(1).Rows(j)("ID"), ds.Tables(1).Rows(j)("Ten"))
                        Dim nodecon As TreeListNode = treeNV.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item2.Value, item2.Name)}, node)
                        If NVThamGia.Contains(item2.Value.ToString) Then nodecon.Checked = True
                        nodecon.Tag = ds.Tables(1).Rows(j)("Email")
                    End If
                Next
                'End If
            Next

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim item As New Utils.ItemObject(ds.Tables(0).Rows(i)("ID"), ds.Tables(0).Rows(i)("Ten"))
                Dim nodeCha As TreeListNode = Nothing
                Dim node As TreeListNode = treeNVThongBao.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item.Value, item.Name)}, nodeCha)
                'node.Tag = ds.Tables(0).Rows(i)("Email")
                If NVThongBao.Contains(item.Value) Then node.Checked = True
                'If deskTop.BarMenu.ItemLinks.Item(i).ToString = "DevExpress.XtraBars.BarSubItemLink" Then
                For j As Integer = 0 To ds.Tables(1).Rows.Count - 1
                    If ds.Tables(1).Rows(j)("IDDepatment") = ds.Tables(0).Rows(i)("ID") Then
                        Dim item2 As New Utils.ItemObject(ds.Tables(1).Rows(j)("ID"), ds.Tables(1).Rows(j)("Ten"))
                        Dim nodecon As TreeListNode = treeNVThongBao.AppendNode(New Utils.ItemObject() {New Utils.ItemObject(item2.Value, item2.Name)}, node)
                        If NVThongBao.Contains(item2.Value.ToString) Then nodecon.Checked = True
                        nodecon.Tag = ds.Tables(1).Rows(j)("Email")
                    End If
                Next
                'End If
            Next
            treeNV.ExpandAll()
            treeNVThongBao.ExpandAll()
        End If
    End Sub

    Private Sub treeNV_AfterCheckNode(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.NodeEventArgs) Handles treeNV.AfterCheckNode, treeNVThongBao.AfterCheckNode
        If e.Node.Nodes.Count > 0 Then
            For i As Integer = 0 To e.Node.Nodes.Count - 1
                e.Node.Nodes(i).Checked = e.Node.Checked
            Next
        Else
            Dim _count As Integer = 0
            If e.Node.ParentNode Is Nothing Then Exit Sub
            For i As Integer = 0 To e.Node.ParentNode.Nodes.Count - 1
                If e.Node.ParentNode.Nodes(i).Checked Then _count += 1
            Next

            If _count > 0 Then
                e.Node.ParentNode.CheckState = CheckState.Checked
            Else
                e.Node.ParentNode.CheckState = CheckState.Indeterminate
            End If
        End If
    End Sub

    Private Sub btGuiMail_Click(sender As System.Object, e As System.EventArgs) Handles btGuiMail.Click
        gFileDinhKem.Visible = False
        Dim strEmail As String = ""

        For i As Integer = 0 To treeNV.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNV.Nodes(i)
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                If nod2.Checked Then
                    strEmail &= nod2.Tag & ","
                End If
            Next
        Next

        If chkBCC.Checked Then
            Utils.Email.SendToList(DataSourceDSFile(strEmail, "Email", ","), tbTieuDe.EditValue, tbNoiDung.EditValue.ToString.Replace(Chr(10), " <BR /> "), EmailNguoiDung, CType(gdvListFile.DataSource, DataTable))
        Else
            Utils.Email.SendToList(DataSourceDSFile(strEmail, "Email", ","), tbTieuDe.EditValue, tbNoiDung.EditValue.ToString.Replace(Chr(10), " <BR /> "), "", CType(gdvListFile.DataSource, DataTable))
        End If
    End Sub

    Private Sub frmCNLichLamViec_Click(sender As System.Object, e As System.EventArgs) Handles MyBase.Click
        gFileDinhKem.Visible = False
    End Sub

    Private Sub lbFileDinhKem_Click(sender As System.Object, e As System.EventArgs) Handles lbFileDinhKem.Click
        gFileDinhKem.Visible = Not gFileDinhKem.Visible
    End Sub

    Private Sub mThemFile_Click(sender As System.Object, e As System.EventArgs) Handles mThemFile.Click
        Dim path As String = ""
        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each file In OpenFile.FileNames
                gdvListFileCT.AddNewRow()
                gdvListFileCT.SetFocusedRowCellValue("File", file)
            Next
        End If
        gdvListFileCT.CloseEditor()
        gdvListFileCT.UpdateCurrentRow()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        If gdvListFileCT.FocusedRowHandle < 0 Then Exit Sub
        gdvListFileCT.DeleteSelectedRows()
    End Sub
End Class