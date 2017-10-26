Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper

Public Class frmQuyenTruyCap
    Public dsQuyen As DataTable
    Public _exit As Boolean = False

    Private Sub frmQuyenTruyCap_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        loadDSNhomNguoiDung()
        LoadDSChucNang()
        loadQuyen()
    End Sub

    Public Sub loadDSNhomNguoiDung()
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT MaTruyCap as ID,MaTruyCap,Quyen FROM QUYENTRUYCAP ORDER BY ID")
        If Not tb Is Nothing Then
            gdvNhomNgDung.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSChucNang()
        treePQ.ClearNodes()
        For i As Integer = 0 To deskTop.BarMenu.ItemLinks.Count - 1
            Dim item As New Utils.ItemObject(deskTop.BarMenu.ItemLinks.Item(i).Item.Name, deskTop.BarMenu.ItemLinks.Item(i).Item.Caption)
            Dim nodeCha As TreeListNode = Nothing
            Dim node As TreeListNode = treePQ.AppendNode(New Object() {New Utils.ItemObject(item.Value, item.Name)}, nodeCha)
            DeQuyAddNode(deskTop.BarMenu.ItemLinks(i), node)
        Next
        treePQ.ExpandAll()
    End Sub


    Public Sub DeQuyAddNode(item As DevExpress.XtraBars.BarItemLink, n As TreeListNode)
        If item.ToString = "DevExpress.XtraBars.BarSubItemLink" Then
            For i As Integer = 0 To CType(item, BarSubItemLink).Item.ItemLinks.Count - 1
                Dim item2 As New Utils.ItemObject(CType(item, BarSubItemLink).Item.ItemLinks(i).Item.Name, CType(item, BarSubItemLink).Item.ItemLinks(i).Item.Caption)
                Dim nodecon As TreeListNode = treePQ.AppendNode(New Object() {New Utils.ItemObject(item2.Value, item2.Name)}, n)
                If CType(item, BarSubItemLink).Item.ItemLinks.Count > 0 Then
                    DeQuyAddNode(CType(item, BarSubItemLink).Item.ItemLinks(i), nodecon)
                End If
            Next
        End If
    End Sub

    Public Sub loadQuyen()

        Dim arr As New ArrayList
        dsQuyen = CauTrucQuyenTruyCap()
        arr.AddRange(gdvNhomNgDungCT.GetFocusedRowCellValue("Quyen").ToString.Split(New Char() {","c}))
        For i As Integer = 0 To arr.Count - 1
            Dim tmp As New ArrayList
            tmp.AddRange(arr(i).ToString.Split(New Char() {";"c}))
            Dim r = dsQuyen.NewRow()
            dsQuyen.Rows.Add(r)
            dsQuyen.Rows(dsQuyen.Rows.Count - 1)(0) = tmp(0)
            For j As Integer = 1 To tmp.Count - 1
                dsQuyen.Rows(dsQuyen.Rows.Count - 1)(j) = CType(tmp(j), Boolean)
            Next
        Next

        treePQ.BeginUpdate()
        For i As Integer = 0 To treePQ.Nodes.Count - 1
            Dim r As DataRow = KiemTraQuyenMenuDr("Menu", CType(treePQ.Nodes(i)(0), Utils.ItemObject).Value)
            If Not r Is Nothing Then
                treePQ.Nodes(i).Checked = True
                For j As Integer = 1 To treePQ.Columns.Count - 1
                    treePQ.Nodes(i)(j) = CType(r(j), Boolean)
                Next
            Else
                treePQ.Nodes(i).Checked = False
                For j As Integer = 1 To treePQ.Columns.Count - 1
                    treePQ.Nodes(i)(j) = False
                Next
            End If
            deQuyLoadQuyen(treePQ.Nodes(i))
        Next

        treePQ.EndUpdate()

    End Sub


    Private Sub deQuyLoadQuyen(n As TreeListNode)
        For k As Integer = 0 To n.Nodes.Count - 1
            Dim r As DataRow = KiemTraQuyenMenuDr("Menu", CType(n.Nodes(k)(0), Utils.ItemObject).Value)
            If Not r Is Nothing Then
                n.Nodes(k).Checked = True
                For l As Integer = 1 To treePQ.Columns.Count - 1
                    n.Nodes(k)(l) = CType(r(l), Boolean)
                Next
            Else
                n.Nodes(k).Checked = False
                For l As Integer = 1 To treePQ.Columns.Count - 1
                    n.Nodes(k)(l) = False
                Next
            End If
            If n.Nodes(k).Nodes.Count > 0 Then deQuyLoadQuyen(n.Nodes(k))
        Next
    End Sub

    Public Function KiemTraQuyenMenuDr(ByVal _QuyenTruyCap As String, ByVal _TenMenu As Object) As DataRow
        Dim dr() As DataRow = dsQuyen.Select(String.Format("{0} = '{1}'", _QuyenTruyCap, _TenMenu))
        If dr.Length > 0 And Not dr Is Nothing Then
            Return dr(0)
        Else
            Return Nothing
        End If
    End Function

    Private Sub gdvNhomNgDungCT_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvNhomNgDungCT.CellValueChanged
        If e.Column.FieldName = "ID" Or e.Column.FieldName = "Quyen" Then Exit Sub
        AddParameter("@Matruycap", gdvNhomNgDungCT.GetFocusedRowCellValue("MaTruyCap"))
        If IsDBNull(gdvNhomNgDungCT.GetFocusedRowCellValue("ID")) Or gdvNhomNgDungCT.GetFocusedRowCellValue("ID") Is Nothing Then
            If doInsert("QUYENTRUYCAP") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                'Dim index As Integer = gdvNhomNgDungCT.FocusedRowHandle
                loadDSNhomNguoiDung()
                'gdvNhomNgDungCT.FocusedRowHandle = index
            Else
                gdvNhomNgDungCT.SetFocusedRowCellValue("ID", gdvNhomNgDungCT.GetFocusedRowCellValue("MaTruyCap"))
            End If
        Else
            AddParameterWhere("@ID", gdvNhomNgDungCT.GetFocusedRowCellValue("ID"))
            If doUpdate("QUYENTRUYCAP", "Matruycap=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Dim index As Integer = gdvNhomNgDungCT.FocusedRowHandle
                loadDSNhomNguoiDung()
                gdvNhomNgDungCT.FocusedRowHandle = index
            Else
                gdvNhomNgDungCT.SetFocusedRowCellValue("ID", gdvNhomNgDungCT.GetFocusedRowCellValue("MaTruyCap"))

            End If
        End If
        gdvNhomNgDungCT.OptionsBehavior.Editable = False
    End Sub


    Private Sub treePQ_AfterCheckNode(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.NodeEventArgs) Handles treePQ.AfterCheckNode
        If e.Node.Nodes.Count > 0 Then
            For i As Integer = 0 To e.Node.Nodes.Count - 1
                e.Node.Nodes(i).Checked = e.Node.Checked
            Next
        End If
    End Sub


    Dim strPhanQuyen As String

    Private Sub btLuuLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btLuuLai.ItemClick, mLuuLai.ItemClick

        treePQ.CloseEditor()
        treePQ.EndUpdate()

        strPhanQuyen = ""

        For i As Integer = 0 To treePQ.Nodes.Count - 1
            Dim nod1 As TreeListNode = treePQ.Nodes(i)
            If nod1.Checked Then
                strPhanQuyen &= CType(nod1(0), Utils.ItemObject).Value
                For k As Integer = 1 To treePQ.Columns.Count - 1
                    strPhanQuyen &= ";"
                    strPhanQuyen &= CType(nod1(k), Int32)
                Next
                strPhanQuyen &= ","
            End If
            deQuyLayQuyenNode(nod1)
        Next

        'ShowCanhBao(strPhanQuyen)
        'Clipboard.SetText(strPhanQuyen)

        AddParameter("@Quyen", strPhanQuyen)
        AddParameterWhere("@ID", gdvNhomNgDungCT.GetFocusedRowCellValue("ID"))
        If doUpdate("QUYENTRUYCAP", "Matruycap=@ID") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else
            gdvNhomNgDungCT.SetFocusedRowCellValue("Quyen", strPhanQuyen)
            ShowAlert("Đã cập nhật quyền truy cập !")
        End If

    End Sub

    Private Sub deQuyLayQuyenNode(n As TreeListNode)
        For i As Integer = 0 To n.Nodes.Count - 1
            If n.Nodes(i).Checked Then
                strPhanQuyen &= CType(n.Nodes(i)(0), Utils.ItemObject).Value
                For j As Integer = 1 To treePQ.Columns.Count - 1
                    strPhanQuyen &= ";"
                    strPhanQuyen &= CType(n.Nodes(i)(j), Int32)
                Next
                strPhanQuyen &= ","
            End If
            If n.Nodes(i).Nodes.Count > 0 Then
                deQuyLayQuyenNode(n.Nodes(i))
            End If
        Next
    End Sub

    Private Sub mChonToanBoHang_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mChonToanBoHang.ItemClick
        Dim check As Boolean = Not treePQ.Selection(0).Checked
        treePQ.BeginUpdate()
        For Each r In treePQ.Selection()
            r.Checked = check
            For j As Integer = 1 To treePQ.Columns.Count - 1
                r(j) = check
            Next
        Next
        treePQ.EndUpdate()
    End Sub

    Private Sub btChonHet_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btChonHet.ItemClick
        Dim check As Boolean = Not treePQ.Nodes(0).Checked
        treePQ.BeginUpdate()
        For i As Integer = 0 To treePQ.Nodes.Count - 1
            treePQ.Nodes(i).Checked = check
            For j As Integer = 1 To treePQ.Columns.Count - 1
                treePQ.Nodes(i)(j) = check
            Next
            For k As Integer = 0 To treePQ.Nodes(i).Nodes.Count - 1
                treePQ.Nodes(i).Nodes(k).Checked = check
                For l As Integer = 1 To treePQ.Columns.Count - 1
                    treePQ.Nodes(i).Nodes(k)(l) = check
                Next
            Next
        Next
        treePQ.EndUpdate()

    End Sub

    Private Sub gdvNhomNgDungCT_FocusedRowChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gdvNhomNgDungCT.FocusedRowChanged
        If gdvNhomNgDungCT.FocusedRowHandle < 0 Then Exit Sub
        loadQuyen()
    End Sub

    Private Sub treePQ_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles treePQ.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btLuuLai.PerformClick()
        End If
    End Sub

    Private Sub gdvNhomNgDungCT_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles gdvNhomNgDungCT.KeyDown
        If e.KeyCode = Keys.Delete Then
            If ShowCauHoi("Xoá nội dung được chọn ?") Then
                AddParameterWhere("@ID", gdvNhomNgDungCT.GetFocusedRowCellValue("ID"))
                If doDelete("QUYENTRUYCAP", "Matruycap=@ID") Is Nothing Then
                    ShowBaoLoi(LoiNgoaiLe)
                Else
                    gdvNhomNgDungCT.DeleteSelectedRows()
                    ShowAlert("Đã xoá")
                End If
            End If
        End If

    End Sub

    Private Sub gdvNhomNgDungCT_DoubleClick(sender As System.Object, e As System.EventArgs) Handles gdvNhomNgDungCT.DoubleClick
        gdvNhomNgDungCT.OptionsBehavior.Editable = True
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub gdvNhomNgDungCT_HiddenEditor(sender As System.Object, e As System.EventArgs) Handles gdvNhomNgDungCT.HiddenEditor
        gdvNhomNgDungCT.OptionsBehavior.Editable = False
    End Sub

    Private Sub btXemBangPhanQuyen_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btXemBangPhanQuyen.ItemClick
        Dim f As New frmXemBangPhanQuyen
        f.Show()
    End Sub
End Class
