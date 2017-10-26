Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports BACSOFT.TAI
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.VisualBasic
Imports DevExpress.Utils

'Imports DevExpress.XtraScheduler.UI.AppointmentFormController
Public Class frmThemsudungxe

    Protected Shared _message As Integer
    Protected Shared _muontra As Integer
    Protected Shared _id_xe As Integer = 0
    Private Shared luutru As Integer = 0
    Public Property Message() As Integer
        Get
            Return _message
        End Get
        Set(ByVal value As Integer)
            _message = value
        End Set
    End Property
    Public Property Muontra() As Integer
        Get
            Return _muontra
        End Get
        Set(ByVal value As Integer)
            _muontra = value
        End Set
    End Property
    Public Property MaXe() As Integer
        Get
            Return _id_xe
        End Get
        Set(ByVal value As Integer)
            _id_xe = value
        End Set
    End Property
    
    Private Sub loadData()
        'btnLuu.PerformClick()
        Dim hientai As DateTime = GetServerTime()
        luutru = 0
        Dim str As String = getSQLConnectionString()
        Dim conn As SqlConnection = New SqlConnection(str)
        conn.Open()
        Dim query As String
        'MsgBox("Loading..........")
        query = "select id, tenxe from xe"
        lueXe.Properties.DataSource = ExecuteSQLDataTable(query)
        lueXe.ItemIndex = 0
        query = "select id,ten from nhansu where trangthai=1 and noictac=74"
        glueNSD.Properties.DataSource = ExecuteSQLDataTable(query)
        glueNSD.Properties.View.PopulateColumns(glueNSD.Properties.DataSource)
        glueNSD.Properties.View.Columns(glueNSD.Properties.ValueMember).Visible = False
        glueNSD.EditValue = 1

        lueMucdo.Properties.DataSource = tableMucDo()
        'lueMucdo.Properties.PopulateColumns()
        ' lueMucdo.Properties.Columns(lueMucdo.Properties.ValueMember).Visible = False
        lueMucdo.EditValue = 1

        lueTrangthai.Properties.DataSource = tableTrangthai()
        lueTrangthai.EditValue = 1

        query = "select id, tenmucdich from mucdichsudung"
        lueMucdich.Properties.DataSource = ExecuteSQLDataTable(query)
        lueMucdich.ItemIndex = 0

        dtpNgayvedukien.EditValue = hientai.AddHours(1)
        dtpNgaydidukien.EditValue = hientai
        dtpNgaydi.EditValue = hientai
        dtpNgayve.EditValue = hientai.AddHours(1)
        Dim _date As System.Data.SqlTypes.SqlDateTime
        _date = System.Data.SqlTypes.SqlDateTime.Null
        If _message = 0 Then
            nudDonghokmsau.Value = nudDonghokmtruoc.Value
            nudSokmdachay.Value = 0
        End If
        If _muontra = 0 Then
            Me.Width = 480
            cbxNgayve.Checked = False
            cbxNgaydi.Checked = False
            cbxNgaydi.Enabled = False
            cbxNgayve.Checked = False
            cbxNgayve.Enabled = False
            btnLuu.Location = New Point(357, 325)
          
            If _id_xe <> 0 Then
                lueXe.EditValue = _id_xe
            End If

        Else
            If _muontra = 1 Then
                grbMuonxe.Enabled = False
                cbxNgayve.Checked = True
                cbxNgaydi.Checked = False
                cbxNgaydi.Enabled = False
                cbxNgayve.Checked = True
                ' cbxNgayve.Enabled = False
                'MessageBox.Show(cbbTrangthaixe.Text.ToString())
            Else
                grbMuonxe.Enabled = True
               
                If _muontra = 3 Then
                    luutru = 1
                    Me.Text = "Nhập cũ"
                End If
            End If
        End If
        If cbxNgaydi.Checked = True Then
            dtpNgaydi.Enabled = True
        Else
            dtpNgaydi.Enabled = False
        End If
        If cbxNgayve.Checked = True Then
            dtpNgayve.Enabled = True
            lueTrangthai.EditValue = 3
        Else
            dtpNgayve.Enabled = False
        End If
        txtIdSudungxe.Text = 0
        If _message <> 0 Then
            'btnThemmoi.Enabled = False
            Dim id As Integer = _message
            Dim sqlComm As SqlCommand = New SqlCommand("select * from sudungxe where id= " + id.ToString(), conn)
            Dim r As SqlDataReader = sqlComm.ExecuteReader()
            If r.Read() Then
                txtIdSudungxe.Text = r("id").ToString()
                lueXe.EditValue = r("id_xe")
                lueXe.Enabled = False
                glueNSD.EditValue = r("id_nguoisudung")
                lueMucdich.EditValue = r("id_mucdich")
                lueMucdo.EditValue = r("id_mucdo")
                If _muontra = 1 Then

                    lueTrangthai.Enabled = False
                    lueTrangthai.EditValue = "3"
                Else
                    lueTrangthai.EditValue = r("id_trangthaixe")
                End If

                If r("ngaydi").ToString() <> "" Then
                    dtpNgaydi.EditValue = r("ngaydi")
                    cbxNgaydi.Checked = True
                Else
                    dtpNgaydi.EditValue = hientai
                End If

                If r("ngayve").ToString() <> "" Then
                    dtpNgayve.EditValue = r("ngayve")
                    cbxNgayve.Checked = True
                Else
                    dtpNgayve.EditValue = hientai
                End If
                If r("ngaydidukien").ToString() <> "" Then
                    dtpNgaydidukien.EditValue = r("ngaydidukien")
                    'cbxNgaydidukien.Checked = True
                Else
                    dtpNgaydidukien.EditValue = hientai
                End If
                If r("ngayvedukien").ToString() <> "" Then
                    dtpNgayvedukien.EditValue = r("ngayvedukien")
                    'cbxNgaydidukien.Checked = True
                Else
                    dtpNgayvedukien.EditValue = hientai
                End If

                txtHanhtrinh.Text = r("hanhtrinh").ToString()
                nudDonghokmtruoc.Value = r("donghokm_truoc")
                If r("donghokm_sau") = 0 Then
                    nudDonghokmsau.Value = r("donghokm_truoc")
                Else
                    nudDonghokmsau.Value = r("donghokm_sau")

                End If

                nudSokmdachay.Value = r("sokmdachay")
                txtGhichukhimuon.Text = r("ghichu_muon").ToString()
                txtGhichukhitra.Text = r("ghichu_tra").ToString()


            End If
            query = "select * from huhaixe where id_sudungxe=@id_sudungxe"
            AddParameter("id_sudungxe", _message)
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            If dt.Rows.Count > 0 Then
                cbxHuhai.Checked = True

                txtVitri.Text = dt.Rows(0).Item(1).ToString()
                If dt.Rows(0).Item(4) = 1 Then
                    cbxSuachua.Checked = True
                    txtThaythe.Text = dt.Rows(0).Item(2).ToString()
                    nudChiphi.Value = dt.Rows(0).Item(3)
                    dtpNgaySuaChua.EditValue = dt.Rows(0).Item(6) 'Convert.ToDateTime(dt.Rows(0).Item(6)).ToString("yyyy/MM/dd HH:mm")
                End If

            End If

        End If
    End Sub
    Private Sub frmThemsudungxe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()

    End Sub
    Private Sub cbxNgayve_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxNgayve.CheckedChanged
        If cbxNgayve.Checked = True Then
            dtpNgayve.Enabled = True
            lueTrangthai.EditValue = 3
            nudDonghokmsau.Enabled = True
            nudSokmdachay.Enabled = True
            txtGhichukhitra.Enabled = True
        Else
            dtpNgayve.Enabled = False
            lueTrangthai.EditValue = 2
            nudDonghokmsau.Enabled = False
            nudSokmdachay.Enabled = False
            txtGhichukhitra.Enabled = False
        End If
    End Sub
    Private Sub loadDongHoKmTruoc()
        If _message = 0 Then
            Dim query As String = "select top 1 donghokm_sau from sudungxe where id_xe=@id_xe and  id_trangthaixe =3 order by ngaydi desc, id desc, donghokm_sau desc"
            AddParameter("@id_xe", lueXe.EditValue)
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            If dt.Rows.Count > 0 Then
                nudDonghokmtruoc.Value = dt.Rows(0).Item(0)
            Else
                nudDonghokmtruoc.Value = 0
            End If
        End If

    End Sub

    Private Sub button1_Click(sender As System.Object, e As System.EventArgs) Handles btnSokmDaChay.Click
        Try
            nudSokmdachay.Value = nudDonghokmsau.Value - nudDonghokmtruoc.Value
        Catch
            If nudDonghokmsau.Value = 0 Then
                nudSokmdachay.Value = 0
            Else
                If Convert.ToInt32(nudDonghokmsau.Value - nudDonghokmtruoc.Value) < 0 Then
                    nudSokmdachay.Value = 0
                    ShowBaoLoi("Số km đã chạy không được nhỏ hơn 0  ")
                End If
            End If
        End Try

    End Sub

    Private Sub cbxHuhai_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxHuhai.CheckedChanged
        If cbxHuhai.Checked = True Then
            grbHuhai.Enabled = True
        Else
            grbHuhai.Enabled = False
        End If
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick


    End Sub

    Private Sub cbxNgaydi_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxNgaydi.CheckedChanged
        If cbxNgaydi.Checked = True Then
            dtpNgaydi.Enabled = True
        Else
            dtpNgaydi.Enabled = False
        End If
    End Sub


    Private Sub cbxSuachua_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cbxSuachua.CheckedChanged
        If cbxSuachua.Checked = True Then
            dtpNgaySuaChua.Enabled = True
            txtThaythe.Enabled = True
            nudChiphi.Enabled = True
        Else
            dtpNgaySuaChua.Enabled = False
            txtThaythe.Enabled = False
            nudChiphi.Enabled = False
        End If
    End Sub
    Private Sub btnLuu_Click(sender As System.Object, e As System.EventArgs) Handles btnLuu.Click
        'MsgBox(luutru)
        Dim _date As System.Data.SqlTypes.SqlDateTime = System.Data.SqlTypes.SqlDateTime.Null
        Dim id As Integer = _message 'Convert .ToInt32(txtIdSudungxe.Text)
        Dim str As String = getSQLConnectionString()
        Dim query = ""
        Dim kt As String = ""
        Dim sodong As Integer = 0
        Dim conn As SqlConnection = New SqlConnection(str)
        Dim sod As Integer = 0
        conn.Open()
        If _message = 0 Then
            query = "insert into sudungxe(id_xe, id_nguoisudung, ngaydi, ngaydidukien, ngayve, ngayvedukien, hanhtrinh, donghokm_truoc, donghokm_sau, sokmdachay, ghichu_muon, ghichu_tra, id_mucdo, id_mucdich, id_trangthaixe, Allday, luutru)" +
                                 "values(@id_xe, @id_nguoisudung, @ngaydi, @ngaydidukien, @ngayve, @ngayvedukien, @hanhtrinh, @donghokm_truoc, @donghokm_sau, @sokmdachay, @ghichu_muon, @ghichu_tra, @id_mucdo, @id_mucdich, @id_trangthaixe, @Allday, @luutru) "

            kt = "select id from sudungxe where id_xe=" + lueXe.EditValue.ToString() + " and ngaydidukien='" + dtpNgaydidukien.EditValue + "' and ngayve is null"

            Dim kt2 As String = "select id from sudungxe where id=@id"
            AddParameter("@id", Convert.ToInt32(txtIdSudungxe.Text))
            Dim dt As DataTable = ExecuteSQLDataTable(kt2)
            sod = dt.Rows.Count
           
            If sod = 1 Then
                id = Convert.ToInt32(txtIdSudungxe.Text)
               
                query = "update sudungxe set id_xe=@id_xe, id_nguoisudung=@id_nguoisudung, ngaydi=@ngaydi, ngaydidukien=@ngaydidukien, hanhtrinh=@hanhtrinh, donghokm_truoc=@donghokm_truoc, ghichu_muon=@ghichu_muon, id_mucdo=@id_mucdo, id_mucdich=@id_mucdich where id=@id"
                AddParameter("@id", id)

            End If

        Else
            query = "update sudungxe set id_xe=@id_xe, id_nguoisudung=@id_nguoisudung, ngaydi=@ngaydi, ngaydidukien=@ngaydidukien, ngayve=@ngayve, ngayvedukien=@ngayvedukien, hanhtrinh=@hanhtrinh, donghokm_truoc=@donghokm_truoc, donghokm_sau=@donghokm_sau, sokmdachay=@sokmdachay, ghichu_muon=@ghichu_muon, ghichu_tra=@ghichu_tra, id_mucdo=@id_mucdo, id_mucdich=@id_mucdich, id_trangthaixe=@id_trangthaixe, Allday=@Allday, luutru=@luutru where id=@id"

            AddParameterWhere("@id", id)
            sod = 1
        End If
       
        AddParameter("@Allday", False)
       
        AddParameter("@id_xe", lueXe.EditValue)
        AddParameter("@id_nguoisudung", glueNSD.EditValue)
        If dtpNgaydi.Enabled = True Then
            AddParameter("@ngaydi", dtpNgaydi.EditValue)
        Else
            AddParameter("@ngaydi", _date)
        End If

        If dtpNgaydidukien.Enabled = True Then
            AddParameter("@ngaydidukien", dtpNgaydidukien.EditValue)
        Else
            AddParameter("@ngaydidukien", _date)
        End If
        If dtpNgayvedukien.Enabled = True Then
            AddParameter("@ngayvedukien", dtpNgayvedukien.EditValue)
        Else
            AddParameter("@ngayvedukien", _date)
        End If
        If dtpNgayve.Enabled = True Then
            AddParameter("@ngayve", dtpNgayve.EditValue)
        Else

            AddParameter("@ngayve", _date)
        End If
        AddParameter("@hanhtrinh", txtHanhtrinh.Text)
        AddParameter("@donghokm_truoc", nudDonghokmtruoc.Value)
        AddParameter("@donghokm_sau", nudDonghokmsau.Value)
        AddParameter("@sokmdachay", nudSokmdachay.Value)
        AddParameter("@ghichu_muon", txtGhichukhimuon.Text)
        AddParameter("@ghichu_tra", txtGhichukhitra.Text)
        AddParameter("@id_mucdo", lueMucdo.EditValue)
        AddParameter("@id_mucdich", lueMucdich.EditValue)
        AddParameter("@luutru", luutru)
        If _muontra = 0 Then
            AddParameter("@id_trangthaixe", 1)
        Else
            If _muontra = 1 Then

                AddParameter("@id_trangthaixe", 3)
            Else
                AddParameter("@id_trangthaixe", lueTrangthai.EditValue)
            End If

        End If


        'If sodong > 0 Then
        'ShowAlert(String.Format("Trong ngày {0:dd/MM/yyyy} đã có {1} người đặt xe {2}", Convert.ToDateTime(dtpNgaydi.EditValue), sodong, lueXe.Text))
        '  End If
        Dim loi As Integer = 0
        Try
            If _muontra = 0 Then
                If dtpNgaydidukien.Enabled = False Then
                    ShowBaoLoi("Phải nhập 1 trong 2 trường Ngày đi hoặc Ngày đi dự kiến")
                    loi += 1
                End If
                If dtpNgayvedukien.Enabled = False Then
                    ShowBaoLoi("Phải nhập Ngày về dự kiến")
                    loi += 1
                End If
                If dtpNgaydidukien.EditValue > dtpNgayvedukien.EditValue Then
                    ShowBaoLoi("Ngày về dự kiến không được nhỏ hơn ngày đi dự kiến")
                    loi += 1
                End If


            End If
            If cbxNgaydi.Checked = True Then
                If cbxNgayve.Checked = True Then
                    If dtpNgaydi.EditValue > dtpNgayve.EditValue Then
                        ShowBaoLoi("Ngày về không được nhỏ hơn ngày đi")
                        loi += 1
                    End If
                Else
                    If dtpNgaydi.EditValue > dtpNgayvedukien.EditValue Then
                        ShowBaoLoi("Ngày về dự kiến không được nhỏ hơn ngày đi")
                        loi += 1
                    End If
                End If

            End If
            If lueTrangthai.EditValue = 3 Then
                If nudDonghokmsau.Value - nudDonghokmtruoc.Value < 0 Then
                    ShowBaoLoi("Số km đã chạy không được nhỏ hơn 0  ")
                    loi += 1
                End If

            End If
            'MsgBox(loi)
            If loi = 0 Then

                ExecuteSQLNonQuery(query)

            Else
                sod = -1
            End If

            If _message = 0 And loi = 0 Then
                query = "select top 1 id from sudungxe order by id desc"
                txtIdSudungxe.Text = ExecuteSQLScalar(query)
            End If
            If sod = 1 Then
                ShowAlert("Đã câp nhật thành công")

            Else
                If sod = 0 Then
                    ShowAlert("Đã lưu thành công")
                Else
                    ShowAlert("Cập nhật thất bại")
                End If

            End If

        Catch ex As Exception

            ShowBaoLoi(LoiNgoaiLe)
        End Try
        If grbHuhai.Enabled = True Then
            Dim ckhuhai As String = "select * from huhaixe where id_sudungxe=@id_sudungxe"
            If txtIdSudungxe.Text = 0 Then
                AddParameter("@id_sudungxe", _message)
            Else
                AddParameter("@id_sudungxe", (txtIdSudungxe.Text))
            End If

            Dim dt As DataTable = ExecuteSQLDataSet(ckhuhai).Tables(0)
            Dim huhai As String = ""
            If dt.Rows.Count = 0 Then
                huhai = "insert into huhaixe(vitrihuhai, thaythe, chiphi, trangthaihuhai, id_sudungxe, ngaysua) values(@vitrihuhai, @thaythe, @chiphi, @trangthaihuhai, @id_sudungxe, @ngaysua)"
            Else
                huhai = "update huhaixe set vitrihuhai=@vitrihuhai, thaythe=@thaythe, chiphi=@chiphi, trangthaihuhai=@trangthaihuhai, ngaysua=@ngaysua where id_sudungxe=@id_sudungxe"
            End If

            If cbxSuachua.Checked = True Then
                Dim suachua As String = "update xe set id_tinhtrang=2 where id_xe=@id_xe"
                AddParameterWhere("@id_xe", lueXe.EditValue)
                ExecuteSQLNonQuery(suachua)
                AddParameter("@trangthaihuhai", 1)
                AddParameter("@ngaysua", dtpNgaySuaChua) ' Convert.ToDateTime(dtpNgaySuaChua.EditValue).ToString("yyyy/MM/dd HH:mm"))
                AddParameter("@vitrihuhai", txtVitri.Text)
                AddParameter("@thaythe", txtThaythe.Text)
                AddParameter("@chiphi", nudChiphi.Value)

            Else
                AddParameter("@trangthaihuhai", 0)
                AddParameter("@ngaysua", _date)
                AddParameter("@vitrihuhai", "")
                AddParameter("@thaythe", "")
                AddParameter("@chiphi", 0)
            End If
            AddParameter("@id_sudungxe", _message)
            Try
                ExecuteSQLNonQuery(huhai)
                'AlertControl1.Show(Me, "Sửa chữa", "Đã sửa chữa")
                ShowAlert("Đã cập nhật sửa chữa")
            Catch ex As Exception
                ShowBaoLoi(LoiNgoaiLe)
            End Try
        Else
            Dim ckhuhai As String = "select * from huhaixe where id_sudungxe=@id_sudungxe"
            If txtIdSudungxe.Text = 0 Then
                AddParameter("@id_sudungxe", _message)
            Else
                AddParameter("@id_sudungxe", (txtIdSudungxe.Text))
            End If
            Dim dt As DataTable = ExecuteSQLDataTable(ckhuhai)
            If dt.Rows.Count > 0 Then
                query = "delete from huhaixe where id_sudungxe=@id_sudungxe"
                If txtIdSudungxe.Text = 0 Then
                    AddParameter("@id_sudungxe", _message)
                Else
                    AddParameter("@id_sudungxe", (txtIdSudungxe.Text))
                End If
                ExecuteSQLNonQuery(query)
            End If
        End If
        query = "select xe.id, chiso, dinhmuc from xe inner join dinhmuc on xe.id=dinhmuc.id_xe where chiso>=dinhmuc"
        If ExecuteSQLDataTable(query).Rows.Count > 0 Then
            deskTop.bsiTinhTrangXe.Enabled = True
            deskTop.bsiTinhTrangXe.Caption = "có " + ExecuteSQLDataTable(query).Rows.Count.ToString() + " xe cần bảo dưỡng"
        Else
            deskTop.bsiTinhTrangXe.Enabled = False
            deskTop.bsiTinhTrangXe.Caption = "."
        End If
    End Sub


    Private Sub lueXe_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueXe.EditValueChanged
        loadDongHoKmTruoc()

    End Sub

    Private Sub nudDonghokmtruoc_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles nudDonghokmtruoc.EditValueChanged
        '  MsgBox(nudDonghokmsau.Value - nudDonghokmtruoc.Value)
        Try
            If lueTrangthai.EditValue = 1 Or lueTrangthai.EditValue = 2 Then
                nudDonghokmsau.Value = nudDonghokmtruoc.Value
            End If
            nudSokmdachay.Value = nudDonghokmsau.Value - nudDonghokmtruoc.Value
        Catch
            If nudDonghokmsau.Value = 0 Then
                nudSokmdachay.Value = 0
            Else
                If Convert.ToInt32(nudDonghokmsau.Value - nudDonghokmtruoc.Value) < 0 Then

                    nudSokmdachay.Value = 0
                End If
            End If
        End Try
    End Sub

    Private Sub nudDonghokmsau_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles nudDonghokmsau.EditValueChanged
        '  MsgBox(nudDonghokmsau.Value - nudDonghokmtruoc.Value)
        Try
            nudSokmdachay.Value = nudDonghokmsau.Value - nudDonghokmtruoc.Value
        Catch
            If nudDonghokmsau.Value = 0 Then
                nudSokmdachay.Value = 0
            Else
                If Convert.ToInt32(nudDonghokmsau.Value - nudDonghokmtruoc.Value) < 0 Then
                    nudSokmdachay.Value = 0
                End If
            End If
        End Try
    End Sub

    Private Sub frmThemsudungxe_Enter(sender As System.Object, e As System.EventArgs) Handles MyBase.Enter

    End Sub

    Private Sub lueTrangthai_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles lueTrangthai.EditValueChanged
        Dim hientai As DateTime = GetServerTime()
        If lueTrangthai.EditValue = 1 Then
            cbxNgaydi.Checked = False
            nudDonghokmsau.Enabled = False
            nudSokmdachay.Enabled = False
            txtGhichukhitra.Enabled = False
            ' cbxNgayve.Checked = False
        Else
            cbxNgaydi.Checked = True
            nudDonghokmsau.Enabled = False
            nudSokmdachay.Enabled = False
            txtGhichukhitra.Enabled = False
            '   dtpNgaydi.EditValue = hientai
            ' cbxNgayve.Checked = False
            If lueTrangthai.EditValue = 3 Then
                cbxNgayve.Checked = True
                dtpNgayve.EditValue = hientai
                nudDonghokmsau.Enabled = True
                nudSokmdachay.Enabled = True
                txtGhichukhitra.Enabled = True
                ' cbxNgayve.Checked = True
            End If
        End If
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        'Dim frm As New frmNhanSu
        'frm.ShowDialog()
        'Dim query = "select id,ten from nhansu where trangthai=1 and noictac=74"
        'glueNSD.Properties.DataSource = ExecuteSQLDataTable(query)
        'glueNSD.Properties.View.PopulateColumns(glueNSD.Properties.DataSource)
        'glueNSD.Properties.View.Columns(glueNSD.Properties.ValueMember).Visible = False
        'glueNSD.EditValue = 1
    End Sub
    Private Sub ShowToolTip(ByVal word As String)
        ToolTipController1.ShowHint(word)
    End Sub

    Private Sub SimpleButton1_MouseHover(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.MouseHover
        'ShowToolTip("Thêm nhân viên")
    End Sub

    Private Sub SimpleButton1_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles SimpleButton1.MouseMove
        ShowToolTip("Thêm nhân viên")
    End Sub

    Private Sub riCeNhapCu_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles riCeNhapCu.EditValueChanged
        ' BarMenu.Manager.ActiveEditItemLink.PostEditor()
    End Sub

    Private Sub btnThemmoi_Click(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnThemmoi.ItemClick
        _message = 0
        txtIdSudungxe.Text = ""
        loadData()
        ShowAlert("Đã làm mới!")
        Dim query As String = "select top 1 donghokm_sau from sudungxe where id_xe=@id_xe order by donghokm_sau desc"
        AddParameter("@id_xe", lueXe.EditValue)
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If dt.Rows.Count > 0 Then
            nudDonghokmtruoc.Value = dt.Rows(0).Item(0)
        Else
            nudDonghokmtruoc.Value = 0
        End If
    End Sub

    Private Sub nudDonghokmtruoc_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles nudDonghokmtruoc.ButtonClick
        If e.Button.Index = 1 Then
            Dim query As String = "select top 1 donghokm_sau from sudungxe where id_xe=@id_xe and  id_trangthaixe =3 order by ngaydi desc, id desc, donghokm_sau desc"
            AddParameter("@id_xe", lueXe.EditValue)
            Dim dt As DataTable = ExecuteSQLDataTable(query)
            If dt.Rows.Count > 0 Then
                nudDonghokmtruoc.Value = dt.Rows(0).Item(0)
            Else
                nudDonghokmtruoc.Value = 0
            End If
        End If
    End Sub
End Class