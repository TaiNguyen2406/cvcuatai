Imports DevExpress.XtraTreeList.Nodes
Imports BACSOFT.Db.SqlHelper

Public Class frmCNYeuCauNoiBo
    Public NVThamGia As New ArrayList
    Public _SP As Object
    Private _stateChange As Boolean = False

    Private Sub frmCNYeuCauNoiBo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSNhom()
        setSourceGdvFileDinhKem()
        If TrangThai.isAddNew Then
            Me.Text = "Lập yêu cầu mới"
            'tbThoiGianLap.EditValue = GetServerTime()
            NVThamGia.Clear()

        Else
            Me.Text = "Cập nhật nội dung yêu cầu"
            Dim sql As String = ""
            AddParameterWhere("@SP", _SP)

            sql = "SELECT * FROM tblYeuCauNoiBo WHERE SoPhieu=@SP"

            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                tbThoiGianLap.EditValue = tb.Rows(0)("ThoiGianLap")
                tbThoiGianYCHoanThanh.EditValue = tb.Rows(0)("ThoiGianDuKien")
                tbNoiDung.EditValue = tb.Rows(0)("NoiDung")
                If Not IsDBNull(tb.Rows(0)("IDNhom")) Then
                    cbNhom.EditValue = Convert.ToByte(tb.Rows(0)("IDNhom"))
                End If

                gdvFile.DataSource = DataSourceDSFile(tb.Rows(0)("FileDinhKem").ToString)
                NVThamGia.Clear()
                NVThamGia.AddRange(tb.Rows(0)("IDThucHien").ToString.Split(","))
            End If
        End If
        LoadDSNhanVien()
    End Sub

    Public Sub setSourceGdvFileDinhKem()
        Dim tb As New DataTable
        tb.Columns.Add("File")
        gdvFile.DataSource = tb
    End Sub

    Private Sub LoadDSNhom()
        AddParameterWhere("@Loai", LoaiTuDien.NhomYCNoiBo)
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT Ma,NoiDung FROM tblTuDien WHERE Loai=@Loai ORDER BY NoiDung")
        If tb Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            cbNhom.Properties.DataSource = tb
        End If
    End Sub

    Private Sub tbThoiGianYCHoanThanh_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tbThoiGianYCHoanThanh.ButtonClick
        If e.Button.Index = 1 Then
            tbThoiGianYCHoanThanh.EditValue = Nothing
        End If
    End Sub

    Private Sub LoadDSNhanVien()
        Dim sql As String = ""
        sql &= " SELECT 'PB'+Convert(nvarchar, ID)ID,Ten FROM DEPATMENT "
        sql &= " SELECT ID,Ten,'PB'+Convert(nvarchar, IDDepatment)IDDepatment,Email"
        sql &= " FROM NHANSU WHERE NHANSU.Noictac=74 AND NHANSU.Trangthai=1 "
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
            treeNV.ExpandAll()
        End If
    End Sub

    Private Sub treeNV_AfterCheckNode(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.NodeEventArgs) Handles treeNV.AfterCheckNode
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

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        If TrangThai.isAddNew Then
            _SP = LaySoPhieu("tblYeuCauNoiBo")
        End If
        Dim tg As DateTime = GetServerTime()
        AddParameter("@ThoiGianLap", tg)
        AddParameter("@ThoiGianDuKien", tbThoiGianYCHoanThanh.EditValue)
        AddParameter("@IDNhom", cbNhom.EditValue)

        AddParameter("@NoiDung", tbNoiDung.EditValue)
        AddParameter("@FileDinhKem", StrDSFile(gdvFileCT, "File"))

        Dim strNVThamGia As String = ","
        Dim strEmail As String = ""

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
        AddParameter("@IDThucHien", strNVThamGia)
        'strEmail = strNVThamGia.Remove(strNVThamGia.Length - 1, 1)

        If TrangThai.isAddNew Then
            AddParameter("@SoPhieu", _SP)
            AddParameter("@IDNguoiLap", TaiKhoan)
            objID = doInsert("tblYeuCauNoiBo")
            If objID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                _SP = Nothing
            Else
                TrangThai.isUpdate = True
                Me.Text = "Cập nhật yêu cầu " & _SP
                'If chkGuiMail.Checked Then
                '    BACSOFT.Utils.Email.SendToList(
                'End If
                tbThoiGianLap.EditValue = tg
                ShowAlert("Đã thêm yêu cầu !")
                _stateChange = True
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauNoiBo).LoadDS()

            End If
        Else
            AddParameterWhere("@SP", _SP)
            If doUpdate("tblYeuCauNoiBo", "SoPhieu=@SP") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ShowAlert("Đã cập nhật yêu cầu !")
                _stateChange = True
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmYeuCauNoiBo).LoadDS()
            End If
        End If
    End Sub

    Private Sub cbNhom_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cbNhom.ButtonClick
        If e.Button.Index = 1 Then
            cbNhom.EditValue = Nothing
        End If
    End Sub

    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        For i As Integer = 0 To treeNV.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNV.Nodes(i)
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)

                If nod2.Checked Then
                    nod2.CheckState = CheckState.Unchecked
                End If
            Next
        Next
        _SP = Nothing
        tbThoiGianLap.EditValue = Nothing
        tbThoiGianYCHoanThanh.EditValue = Nothing
        tbNoiDung.EditValue = ""
        Me.Text = "Thêm yêu cầu"
    End Sub

    Private Sub frmCNYeuCauNoiBo_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _stateChange Then
            Dim tg As DateTime = GetServerTime()
            For i As Integer = 0 To treeNV.Nodes.Count - 1
                Dim nod1 As TreeListNode = treeNV.Nodes(i)
                For j As Integer = 0 To nod1.Nodes.Count - 1
                    Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                    If nod2.Checked Then
                        AddParameter("@NoiDung", "Có yêu cầu nội bộ từ : " & NguoiDung & "; SP: " & _SP & vbCrLf & " - Thời gian hoàn thành: " & tbThoiGianYCHoanThanh.EditValue & vbCrLf & " - nội dung yêu cầu: " & vbCrLf & tbNoiDung.EditValue)
                        AddParameter("@ThoiGian", tg)
                        AddParameter("@IDNhanVien", CType(nod2(0), Utils.ItemObject).Value)
                        If doInsert("tblThongBao") Is Nothing Then
                            ShowBaoLoi("Lỗi lập thông thông báo: " & LoiNgoaiLe)
                        End If
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

    Private Sub btThemFile_Click(sender As System.Object, e As System.EventArgs) Handles btThemFile.Click
        If TrangThai.isAddNew Then
            ShowCanhBao("Bạn cần lưu lại một lần trước khi thêm file đính kèm")
        End If

        Dim _NamThang As String = Convert.ToDateTime(tbThoiGianLap.EditValue).ToString("yyyyMM") & "\"

        Dim path As String = ""
        Dim OpenFile As New OpenFileDialog
        OpenFile.Multiselect = True
        If OpenFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            If Not System.IO.Directory.Exists(RootUrl & UrlYeuCauNoiBo & _NamThang) Then
                System.IO.Directory.CreateDirectory(RootUrl & UrlYeuCauNoiBo & _NamThang)
            End If
            For Each file In OpenFile.FileNames
                ShowWaiting("Đang chuyển file lên server ...")
                path = IO.Path.GetFileNameWithoutExtension(file) & " " & _SP & " " & TaiKhoan.ToString & IO.Path.GetExtension(file)
                Try
                    IO.File.Copy(file, RootUrl & UrlYeuCauNoiBo & _NamThang & path)
                    gdvFileCT.AddNewRow()
                    gdvFileCT.SetFocusedRowCellValue("File", path)
                Catch ex As Exception
                    ShowBaoLoi(ex.Message)
                End Try
                CloseWaiting()
            Next
        End If
        gdvFileCT.CloseEditor()
        gdvFileCT.UpdateCurrentRow()
    End Sub

    Private Sub gdvFileCT_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvFileCT.RowCellClick
        If e.Column.Name = "colFile" Then
            If e.CellValue Is Nothing Then Exit Sub
            If e.CellValue.ToString = "" Then Exit Sub
            OpenFileOnLocal(RootUrl & UrlYeuCauNoiBo & Convert.ToDateTime(tbThoiGianLap.EditValue).Year.ToString("yyyyMM") & "\" & e.CellValue, e.CellValue)
        End If
    End Sub

    Private Sub btXoaFile_Click(sender As System.Object, e As System.EventArgs) Handles btXoaFile.Click
        If gdvFileCT.FocusedRowHandle < 0 Then Exit Sub
        If ShowCauHoi("Xóa file được chọn ?") Then
            Try
                IO.File.Delete(RootUrl & UrlYeuCauNoiBo & Convert.ToDateTime(tbThoiGianLap.EditValue).Year.ToString("yyyyMM") & "\" & gdvFileCT.GetFocusedRowCellValue("File"))
                gdvFileCT.DeleteSelectedRows()
            Catch ex As Exception
                If Not IO.File.Exists(RootUrl & UrlYeuCauNoiBo & Convert.ToDateTime(tbThoiGianLap.EditValue).Year.ToString("yyyyMM") & "\" & gdvFileCT.GetFocusedRowCellValue("File")) Then
                    gdvFileCT.DeleteSelectedRows()
                End If
                ShowBaoLoi(ex.Message)
            End Try
        End If
    End Sub
End Class