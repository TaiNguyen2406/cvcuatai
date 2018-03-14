Imports DevExpress.XtraBars
Imports BACSOFT.Db.SqlHelper
Public Class frmThemBaoduongxe
    Protected Shared _message As Integer = 0
    Public Property Message() As Integer
        Get
            Return _message
        End Get
        Set(ByVal value As Integer)
            _message = value
        End Set
    End Property
    Protected Shared _message2 As Integer = 0
    Public Property Message2() As Integer
        Get
            Return _message2
        End Get
        Set(ByVal value As Integer)
            _message2 = value
        End Set
    End Property
    Private Sub loadData()
        ckeNhapCu.Checked = False
        Dim query As String
        If _message2 <> 0 Then
            query = "select id, tenxe from xe where id=" + _message2.ToString()
        Else
            query = "select id, tenxe from xe "
        End If
        dtpNgaySuaChua.EditValue = Today
        lueXebd.Properties.DataSource = ExecuteSQLDataTable(query)
        ' lueXebd.Properties.PopulateColumns()
        lueXebd.Properties.Columns(lueXebd.Properties.ValueMember).Visible = False
        lueXebd.ItemIndex = 0
        query = "select id,ten from nhansu where trangthai=1 and noictac=74"
        glueNSD.Properties.DataSource = ExecuteSQLDataTable(query)
        glueNSD.Properties.View.PopulateColumns(glueNSD.Properties.DataSource)
        glueNSD.Properties.View.Columns(glueNSD.Properties.ValueMember).Visible = False
        glueNSD.EditValue = ExecuteSQLDataTable(query).Rows(0).Item(0)
        loadDinhmuc()
        If _message <> 0 Then
            ckeNhapCu.Checked = True
            query = "select baoduongxe.*, tenxe, ten from baoduongxe inner join xe on idxe=xe.id inner join nhansu on idnguoithuchien=nhansu.id where baoduongxe.id=@id"
            AddParameter("@id", _message)
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            If dt.Rows.Count > 0 Then
                lueXebd.EditValue = dt.Rows(0).Item(1)
                nudChiphi.Text = dt.Rows(0).Item(3).ToString()
                If dt.Rows(0).Item(4).ToString <> "" Then
                    dtpNgaySuaChua.EditValue = dt.Rows(0).Item(4)
                End If
                txtGhichu.Text = dt.Rows(0).Item(5).ToString()
                glueNSD.EditValue = dt.Rows(0).Item(6)
                lueViTri.EditValue = dt.Rows(0).Item(7)
                seSoKmKhiBD.EditValue = dt.Rows(0).Item(8)
            End If

        End If
    End Sub

    Private Sub frmThemBaoduongxe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
    End Sub

    Private Sub btnThem_Click(sender As System.Object, e As System.EventArgs) Handles btnThem.Click
        Dim query As String = ""
        AddParameter("@idxe", lueXebd.EditValue)
        AddParameter("@idnguoithuchien", glueNSD.EditValue)
        ' AddParameter("@vitribaoduong", txtVitri.Text)
        AddParameter("@chiphi", nudChiphi.Value)
        AddParameter("@ghichu", txtGhichu.Text)
        AddParameter("@ngaybaoduong", dtpNgaySuaChua.EditValue)
        AddParameter("@sokmkhibd", seSoKmKhiBD.EditValue)
        AddParameter("@iddinhmuc", lueViTri.EditValue)
        If _message <> 0 Then
            AddParameterWhere("@id", _message)
            If doUpdate("baoduongxe", "id=@id") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                'loadData()
            Else
                ShowAlert("Đã cập nhật !")
            End If
        Else
            If doInsert("baoduongxe") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                'loadData()

            Else
                ShowAlert("Đã thêm !")
                Dim check As String = "select top 1 id from baoduongxe order by id desc"
                _message = ExecuteSQLScalar(check)
                If ckeNhapCu.Checked = False Then
                    ' AddParameter("@id", lueViTri.EditValue)
                    'Dim chisohientai = ExecuteSQLScalar("select chisohientai-dinhmuc from DinhMuc where id=@id")
                    AddParameter("@chisohientai", 0)
                    AddParameterWhere("@id", lueViTri.EditValue)
                    If doUpdate("dinhmuc", "id=@id") Is Nothing Then
                        ShowAlert(LoiNgoaiLe)
                    Else
                        For i = 0 To deskTop.tabMain.TabPages.Count - 1
                            If deskTop.tabMain.TabPages(i).Controls(0).Tag = "frmDinhmuc" Then
                                CType(deskTop.tabMain.TabPages(i).Controls(0), frmDinhmuc).btnTailai.PerformClick()
                            End If
                        Next
                    End If
                    '  query = " update xe set id_tinhtrang=3 where id=@idxe"
                    AddParameter("@id_tinhtrang", 3)
                    AddParameterWhere("@idxe", lueXebd.EditValue)
                    If doUpdate("xe", "id=@idxe") Is Nothing Then
                        ShowAlert(LoiNgoaiLe)
                    End If
                    query = "select xe.id, chisohientai, dinhmuc from xe inner join dinhmuc on xe.id=dinhmuc.id_xe where chisohientai>=dinhmuc"
                    If ExecuteSQLDataTable(query).Rows.Count > 0 Then
                        deskTop.bsiTinhTrangXe.Enabled = True
                        deskTop.bsiTinhTrangXe.Caption = "có xe cần bảo dưỡng"
                    Else
                        deskTop.bsiTinhTrangXe.Enabled = False
                        deskTop.bsiTinhTrangXe.Caption = "."
                    End If
                End If
                'MsgBox(_message)
            End If
        End If
        '  query = "update sudungxe set luutru=1 where id_xe=" + lueXebd.EditValue.ToString + " and ngayve<='" + Convert.ToDateTime(dtpNgaySuaChua.EditValue).ToString("yyyy/MM/dd HH:mm") + "'"
        AddParameter("@luutru", 1)
        AddParameterWhere("@id_xe", lueXebd.EditValue)
        AddParameterWhere("@ngayve", dtpNgaySuaChua.EditValue)
        If doUpdate("sudungxe", "id_xe=@id_xe and ngayve<=@ngayve") Is Nothing Then
            ShowAlert(LoiNgoaiLe)
        End If
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        _message = 0
        _message2 = 0
        loadData()
        '   ShowAlert("Đã làm mới!")
    End Sub


    Private Sub lueViTri_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueViTri.EditValueChanged
        'Dim query = "select chisohientai from dinhmuc where id=@id"
        'AddParameterWhere("@id", lueViTri.EditValue)
        'seSoKmKhiBD.EditValue = ExecuteSQLScalar(query)
    End Sub
    Private Sub loadDinhmuc()
        Dim query As String
        query = "select dinhmuc.id as id, convert(varchar(10),dinhmuc)+' km thay '+tennhienvatlieu as tennvl from dinhmuc inner join nhienvatlieu on id_nhienvatlieu=nhienvatlieu.id where  id_xe=@id_xe"
        AddParameterWhere("@id_xe", lueXebd.EditValue)
        If ckeNhapCu.CheckState = CheckState.Unchecked Then
            query &= "  and chisohientai>=dinhmuc "
        End If
        Dim dt = ExecuteSQLDataTable(query)
        lueViTri.Properties.DataSource = dt
        'lueViTri.ItemIndex = 0
        If dt.Rows.Count < 1 Then
            lueViTri.EditValue = Nothing
        Else
            lueViTri.ItemIndex = 0
        End If
    End Sub
    Private Sub ckeNhapCu_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ckeNhapCu.CheckedChanged
        loadDinhmuc()
    End Sub

    Private Sub lueXebd_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueXebd.EditValueChanged
        loadDinhmuc()
    End Sub
End Class