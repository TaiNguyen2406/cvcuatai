Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraTreeList.Nodes

Public Class frmChaoGia_LSGiaoDich
    Public NVThamGia As New ArrayList
    Public _ID As Object
    Public _SoPhieu As Object
    Public _MaKH As String
    Public _IDNhanVien As Object

    Private Sub frmChaoGia_LSGiaoDich_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If TrangThai.isUpdate Then
            AddParameterWhere("@IDD", _ID)
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM BANGCHAOGIA_LSGIAODICH WHERE ID=@IDD")
            If Not tb Is Nothing Then
                tbThoiGian.EditValue = tb.Rows(0)("ThoiGian")
                tbNoiDung.EditValue = tb.Rows(0)("NoiDung")
            End If
        Else
            tbThoiGian.EditValue = Today.Date
        End If
        LoadDSNhanVien()
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


            treeNV.ExpandAll()

        End If
    End Sub


    Private Sub btThem_Click(sender As System.Object, e As System.EventArgs) Handles btThem.Click
        TrangThai.isAddNew = True
        tbThoiGian.EditValue = Today.Date
        tbNoiDung.EditValue = ""
    End Sub

    Private Sub btGhi_Click(sender As System.Object, e As System.EventArgs) Handles btGhi.Click
        AddParameter("@ThoiGian", tbThoiGian.EditValue)
        AddParameter("@SoPhieu", _SoPhieu)
        AddParameter("@NoiDung", tbNoiDung.EditValue)
        If TrangThai.isAddNew Then
            _ID = doInsert("BANGCHAOGIA_LSGiaoDich")
            If _ID Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                TrangThai.isUpdate = True
                ShowAlert("Đã cập nhật!")
                ThongBao()
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmChaoGia).LoadDSLSGiaoDich(_SoPhieu)
            End If
        Else
            AddParameterWhere("@IDD", _ID)
            If doUpdate("BANGCHAOGIA_LSGiaoDich", "ID=@IDD") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
            Else
                ThongBao()
                CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmChaoGia).LoadDSLSGiaoDich(_SoPhieu)
                ShowAlert("Đã cập nhật !")
            End If
        End If
    End Sub

    Private Sub ThongBao()
        Dim str As String = "Thông tin giao dịch khách hàng: " & _MaKH & " - CG:" & _SoPhieu
        str &= vbCrLf & tbNoiDung.EditValue
        ThemThongBaoChoNV(str, _IDNhanVien)
        For i As Integer = 0 To treeNV.Nodes.Count - 1
            Dim nod1 As TreeListNode = treeNV.Nodes(i)
            For j As Integer = 0 To nod1.Nodes.Count - 1
                Dim nod2 As TreeListNode = treeNV.Nodes(i).Nodes(j)
                If nod2.Checked Then
                    ThemThongBaoChoNV(str, CType(nod2(0), Utils.ItemObject).Value)
                End If
            Next
        Next
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

    Private Sub btClose_Click(sender As System.Object, e As System.EventArgs) Handles btClose.Click
        Me.Close()
    End Sub
End Class