Imports BACSOFT.Db.SqlHelper
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraTreeList.Nodes
Imports System.Threading

Public Class frmBangChaoGiaCongTrinh


    Private dorongCot1 As Integer = 40
    Private dorongCot2 As Integer = 60

    Private varTuNgay As DateTime
    Private varDenNgay As DateTime

    Private Sub frmBaoCaoKinhDoanh_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim tg As DateTime = GetServerTime()

        LoadDSPhongBan()
        LoadDSNhanVien()

        tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        cbTieuChiX.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        cmbNhanVien.EditValue = "[Tùy chọn]"
        LoadDanhSachTreeNhanVien()

    End Sub

#Region "-- Danh sách tree nhân viên --"

    Private Sub LoadDanhSachTreeNhanVien()
        treeNhanVien.Nodes.Clear()
        Dim sql As String = "SELECT ID,Ten FROM DEPATMENT ORDER BY ID; "
        sql &= "SELECT ID,Ten,IDDepatment,TrangThai FROM NHANSU WHERE Noictac = 74 "
        If chkTrangThaiNV.Checked Then sql &= " AND TrangThai = 1 "
        sql &= "ORDER BY ID"
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        Dim nodeTatCa As TreeListNode = treeNhanVien.AppendNode(New Object() {-1, "[Tất cả]"}, 0)
        For Each rowPhong As DataRow In ds.Tables(0).Rows
            Dim nodePhong As TreeListNode = treeNhanVien.AppendNode(New Object() {rowPhong("Id"), rowPhong("Ten")}, nodeTatCa)
            If rowPhong("Id") = 3 Or rowPhong("Id") = 4 Then nodePhong.Checked = True 'Mac dinh check luon 2 phong nay
            Dim collectRowNV() As DataRow = ds.Tables(1).Select("IDDepatment=" & rowPhong("Id"))
            For Each rowNV As DataRow In collectRowNV
                Dim nodeNV As TreeListNode = treeNhanVien.AppendNode(New Object() {rowNV("Id"), rowNV("Ten")}, nodePhong)
                If rowPhong("Id") = 3 Or rowPhong("Id") = 4 Then nodeNV.Checked = True 'Mac dinh check luon 2 phong nay
            Next
        Next
        treeNhanVien.ExpandAll()
    End Sub

    Private Sub chkTrangThaiNV_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTrangThaiNV.CheckedChanged
        LoadDanhSachTreeNhanVien()
    End Sub

    Private Sub treeNhanVien_AfterCheckNode(sender As System.Object, e As DevExpress.XtraTreeList.NodeEventArgs) Handles treeNhanVien.AfterCheckNode
        For Each n1 As TreeListNode In e.Node.Nodes
            n1.Checked = e.Node.Checked
            If n1.HasChildren Then
                For Each n2 As TreeListNode In n1.Nodes
                    n2.Checked = e.Node.Checked
                Next
            End If
        Next
        Dim kt As Boolean = True
        For Each nod As TreeListNode In treeNhanVien.Nodes(0).Nodes
            For Each n As TreeListNode In nod.Nodes
                If Not n.Checked Then
                    kt = False
                    Exit For
                End If
            Next
        Next

        If kt Then
            cmbNhanVien.EditValue = "[Tất cả]"
            treeNhanVien.Nodes(0).Checked = True
        Else
            cmbNhanVien.EditValue = "[Tùy chọn]"
            treeNhanVien.Nodes(0).Checked = False
        End If
    End Sub

    Private Function LayIdDanhSachNhanVien() As String
        Dim str As String = ""
        For Each nod As TreeListNode In treeNhanVien.Nodes(0).Nodes
            For Each n As TreeListNode In nod.Nodes
                If n.Checked Then
                    str &= n.GetValue(colID).ToString & ","
                End If
            Next
        Next
        If str = "" Then Return Nothing
        Return str.Substring(0, str.Length - 1)
    End Function
#End Region

#Region " - Load phong ban va nhan vien - "
    Public Sub LoadDSPhongBan()
        Dim tb As DataTable = ExecuteSQLDataTable(" SELECT ID,Ten FROM DEPATMENT ")
        If Not tb Is Nothing Then
            rcbPhong.DataSource = tb
            rPhongBan.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Sub LoadDSNhanVien()
        On Error Resume Next
        Dim sql As String = ""
        If Not cbPhong.EditValue Is Nothing Then
            sql = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 and IDDepatment= " & cbPhong.EditValue
        Else
            sql = " SELECT ID,Ten FROM NHANSU WHERE Noictac=74 "
        End If
        Dim tb As DataTable = ExecuteSQLDataTable(sql)
        If Not tb Is Nothing Then
            rcbNhanVien.DataSource = tb
            rNhanVien.DataSource = tb
            'cbNhanVien.EditValue = TaiKhoan
            'rcbNV.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#End Region

#Region " - Cb button click - "
    Private Sub rcbNhanVien_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbNhanVien.ButtonClick
        If e.Button.Index = 1 Then
            cbNhanVien.EditValue = Nothing
        End If
    End Sub

    Private Sub rcbPhong_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles rcbPhong.ButtonClick
        If e.Button.Index = 1 Then
            cbPhong.EditValue = Nothing
        End If
    End Sub

    Private Sub cbPhong_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbPhong.EditValueChanged
        LoadDSNhanVien()
        cbNhanVien.EditValue = Nothing
    End Sub
#End Region

#Region " - Chon thoi gian - "
    Private Sub cbTieuChi_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cbTieuChi.EditValueChanged
        Dim value As Object = cbTieuChi.EditValue
        If value Is Nothing Then value = ""
        btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Dim tg As DateTime = GetServerTime()
        Select Case value
            Case "Tuần"
                tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                cbTieuChiX.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Select Case tg.Day
                    Case 1 To 7
                        cbTieuChiX.EditValue = "I"
                        Exit Select
                    Case 8 To 16
                        cbTieuChiX.EditValue = "II"
                        Exit Select
                    Case 17 To 24
                        cbTieuChiX.EditValue = "III"
                        Exit Select
                    Case 25 To 31
                        cbTieuChiX.EditValue = "IV"
                        Exit Select
                End Select
                tbTuNgay.Caption = "Tháng"
                rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.EditFormat.FormatString = "MM/yyyy"
                rtbTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.DisplayFormat.FormatString = "MM/yyyy"
                rtbTuNgay.EditMask = "MM/yyyy"
                tbTuNgay.EditValue = tg
                Exit Select
            Case "Tháng"
                tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                cbTieuChiX.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                tbTuNgay.Caption = "Tháng"
                rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.EditFormat.FormatString = "MM/yyyy"
                rtbTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.DisplayFormat.FormatString = "MM/yyyy"
                rtbTuNgay.EditMask = "MM/yyyy"
                tbTuNgay.EditValue = tg
                Exit Select
            Case "Quý"
                tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                cbTieuChiX.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                Select Case tg.Month
                    Case 1 To 3
                        cbTieuChiX.EditValue = "I"
                        Exit Select
                    Case 4 To 6
                        cbTieuChiX.EditValue = "II"
                        Exit Select
                    Case 7 To 9
                        cbTieuChiX.EditValue = "III"
                        Exit Select
                    Case 10 To 12
                        cbTieuChiX.EditValue = "IV"
                        Exit Select
                End Select
                tbTuNgay.Caption = "Năm"
                rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.EditFormat.FormatString = "yyyy"
                rtbTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.DisplayFormat.FormatString = "yyyy"
                rtbTuNgay.EditMask = "yyyy"
                tbTuNgay.EditValue = tg
                Exit Select
            Case "Năm"
                tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                cbTieuChiX.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.EditFormat.FormatString = "yyyy"
                rtbTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.DisplayFormat.FormatString = "yyyy"
                rtbTuNgay.EditMask = "yyyy"
                tbTuNgay.EditValue = tg
                Exit Select
            Case "Tùy Chọn"
                tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                cbTieuChiX.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                tbTuNgay.Caption = "Từ tháng"
                tbDenNgay.Caption = "Đến tháng"
                tbTuNgay.Width = 60
                tbDenNgay.Width = 60

                rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.EditFormat.FormatString = "MM/yyyy"
                rtbTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.DisplayFormat.FormatString = "MM/yyyy"
                rtbTuNgay.EditMask = "MM/yyyy"
                tbTuNgay.EditValue = New DateTime(tg.Year, 1, 1)
                rtbTuNgay.Buttons(0).Visible = False

                rtbDenNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbDenNgay.EditFormat.FormatString = "MM/yyyy"
                rtbDenNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbDenNgay.DisplayFormat.FormatString = "MM/yyyy"
                rtbDenNgay.EditMask = "MM/yyyy"
                tbDenNgay.EditValue = tg
                rtbDenNgay.Buttons(0).Visible = False
                Exit Select
            Case "Tùy Chọn Tuần"
                tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                cbTieuChiX.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                tbTuNgay.Caption = "Đầu tuần"
                tbDenNgay.Caption = "Cuối tuần"
                tbTuNgay.Width = 90
                tbDenNgay.Width = 90

                rtbTuNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.EditFormat.FormatString = "dd/MM/yyyy"
                rtbTuNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbTuNgay.DisplayFormat.FormatString = "dd/MM/yyyy"
                rtbTuNgay.EditMask = "dd/MM/yyyy"
                rtbTuNgay.Buttons(0).Visible = True
                tbTuNgay.EditValue = New DateTime(tg.Year, tg.Month, 1)

                rtbDenNgay.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbDenNgay.EditFormat.FormatString = "dd/MM/yyyy"
                rtbDenNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                rtbDenNgay.DisplayFormat.FormatString = "dd/MM/yyyy"
                rtbDenNgay.EditMask = "dd/MM/yyyy"
                rtbDenNgay.Buttons(0).Visible = True
                tbDenNgay.EditValue = tg
                Exit Select
            Case Else
                tbTuNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                tbDenNgay.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                btDuyetBC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                cbTieuChiX.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Exit Select
        End Select
    End Sub
#End Region

#Region "-- Tạo cột theo tiêu chí báo cáo --"

    Private Sub TaoCot()

        gdv.DataSource = Nothing
        gdvData.Columns.Clear()
        gdvData.Bands.Clear()

        Dim bPhong = createBand("Phòng")
        Dim cPhong = createColumn("cPhong", bPhong, 110)
        gdvData.Columns("cPhong").ColumnEdit = rPhongBan
        gdvData.Columns("cPhong").OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
        bPhong.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

        Dim bNhanVien = createBand("Nhân viên")
        Dim cNhanVien = createColumn("cNhanVien", bNhanVien, 120)
        gdvData.Columns("cNhanVien").ColumnEdit = rNhanVien
        gdvData.Columns("cNhanVien").OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
        bNhanVien.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

        Dim bNoiDungBaoCao = createBand("Nội dung báo cáo")
        Dim cNoiDungBaoCao = createColumn("cNoiDungBaoCao", bNoiDungBaoCao, 180)
        bNoiDungBaoCao.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

        Dim bTieuChi = createBand("Tiêu chí")
        createColumn("cIDTieuChi", bTieuChi, 100)
        bTieuChi.Visible = False


        Dim value As Object = cbTieuChi.EditValue
        If value Is Nothing Then value = ""

        Select Case value
            Case "Tuần"
                TaoCotTuan()
                Exit Select
            Case "Tháng"
                TaoCotThang()
                Exit Select
            Case "Quý"
                TaoCotQuy()
                Exit Select
            Case "Năm"
                TaoCotNam()
                Exit Select
            Case "Tùy Chọn"
                TaoCotTuyChon()
                Exit Select
            Case "Tùy Chọn Tuần"
                TaoCotTuyChonTuan()
                Exit Select
        End Select

        CreateDataSource()
        GetDataSource()
    End Sub

    Private Sub TaoCotTuan()
        Dim tg As DateTime = CType(tbTuNgay.EditValue, DateTime)
        Dim bTuan = createBand("Tuần " & cbTieuChiX.EditValue & " " & tg.ToString("MM/yyyy"))
        Dim cTuan_c1 = createColumn("cTuan_c1", bTuan, dorongCot1)
        Dim cTuan_c2 = createColumn("cTuan_c2", bTuan, dorongCot2)
        Select Case cbTieuChiX.EditValue
            Case "I"
                varTuNgay = New DateTime(tg.Year, tg.Month, 1)
                varDenNgay = varDenNgay.AddDays(7)
            Case "II"
                varTuNgay = New DateTime(tg.Year, tg.Month, 8)
                varDenNgay = varDenNgay.AddDays(7)
            Case "III"
                varTuNgay = New DateTime(tg.Year, tg.Month, 15)
                varDenNgay = varTuNgay.AddDays(7)
            Case "IV"
                varTuNgay = New DateTime(tg.Year, tg.Month, 23)
                varDenNgay = New DateTime(tg.Year, tg.Month, DateTime.DaysInMonth(tg.Year, tg.Month))
        End Select
    End Sub

    Private Sub TaoCotThang()
        Dim tg As DateTime = CType(tbTuNgay.EditValue, DateTime)
        Dim bThang = createBand("Tháng " & tg.ToString("MM/yyyy"))
        Dim cThang_c1 = createColumn("cThang_c1", bThang, dorongCot1)
        Dim cThang_c2 = createColumn("cThang_c2", bThang, dorongCot2)
        varTuNgay = New DateTime(tg.Year, tg.Month, 1)
        varDenNgay = New DateTime(tg.Year, tg.Month, DateTime.DaysInMonth(tg.Year, tg.Month))
    End Sub

    Private Sub TaoCotQuy()

        Dim tg As DateTime = CType(tbTuNgay.EditValue, DateTime)
        Dim value As Object = cbTieuChiX.EditValue
        If value Is Nothing Then value = ""
        Dim thang As Integer = 0

        Select Case value
            Case "I"
                thang = 1
                Exit Select
            Case "II"
                thang = 4
                Exit Select
            Case "III"
                thang = 7
                Exit Select
            Case "IV"
                thang = 10
                Exit Select
        End Select

        For i As Integer = thang To thang + 2
            Dim bThang = createBand("Tháng " & i & tg.ToString("/yyyy"))
            Dim cThang_c1 = createColumn("cThang" & i & "_c1", bThang, dorongCot1)
            Dim cThang_c2 = createColumn("cThang" & i & "_c2", bThang, dorongCot2)
        Next

        Dim bQuy = createBand("Quý " & cbTieuChiX.EditValue & " " & tg.ToString("yyyy"))
        Dim cQuy_c1 = createColumn("cQuy_c1", bQuy, dorongCot1)
        Dim cQuy_c2 = createColumn("cQuy_c2", bQuy, dorongCot2)
        TaoBgCot("cQuy_c1", "cQuy_c2")
        varTuNgay = New DateTime(tg.Year, thang, 1)
        varDenNgay = New DateTime(tg.Year, thang + 2, DateTime.DaysInMonth(tg.Year, thang + 2))

    End Sub

    Private Sub TaoCotNam()
        Dim tg As DateTime = CType(tbTuNgay.EditValue, DateTime)
        For i As Integer = 1 To 12
            Dim bThang = createBand("Tháng " & i & tg.ToString("/yyyy"))
            Dim cThang_c1 = createColumn("cThang" & i & "_c1", bThang, dorongCot1)
            Dim cThang_c2 = createColumn("cThang" & i & "_c2", bThang, dorongCot2)
        Next
        Dim bQuy1 = createBand("Quý I")
        Dim cQuy1_c1 = createColumn("cQuy1_c1", bQuy1, dorongCot1)
        Dim cQuy1_c2 = createColumn("cQuy1_c2", bQuy1, dorongCot2)
        TaoBgCot("cQuy1_c1", "cQuy1_c2")

        Dim bQuy2 = createBand("Quý II")
        Dim cQuy2_c1 = createColumn("cQuy2_c1", bQuy2, dorongCot1)
        Dim cQuy2_c2 = createColumn("cQuy2_c2", bQuy2, dorongCot2)
        TaoBgCot("cQuy2_c1", "cQuy2_c2")

        Dim bQuy3 = createBand("Quý III")
        Dim cQuy3_c1 = createColumn("cQuy3_c1", bQuy3, dorongCot1)
        Dim cQuy3_c2 = createColumn("cQuy3_c2", bQuy3, dorongCot2)
        TaoBgCot("cQuy3_c1", "cQuy3_c2")

        Dim bQuy4 = createBand("Quý IV")
        Dim cQuy4_c1 = createColumn("cQuy4_c1", bQuy4, dorongCot1)
        Dim cQuy4_c2 = createColumn("cQuy4_c2", bQuy4, dorongCot2)
        TaoBgCot("cQuy4_c1", "cQuy4_c2")
        '  Next
        Dim bTong = createBand("Tổng")
        Dim cTong_c1 = createColumn("cTong_c1", bTong, dorongCot1)
        Dim cTong_c2 = createColumn("cTong_c2", bTong, dorongCot2)
        TaoBgCot("cTong_c1", "cTong_c2")
        varTuNgay = New DateTime(tg.Year, 1, 1)
        varDenNgay = New DateTime(tg.Year, 12, DateTime.DaysInMonth(tg.Year, 12))
    End Sub

    Private Sub TaoCotTuyChon()

        Dim tuNgay As DateTime = tbTuNgay.EditValue
        Dim denNgay As DateTime = tbDenNgay.EditValue

        Dim tgBatDau As DateTime = New DateTime(tuNgay.Year, tuNgay.Month, 1)

        While tgBatDau <= denNgay
            Dim bThang = createBand("Tháng " & tgBatDau.ToString("MM/yyyy"))
            Dim cThang_c1 = createColumn("cThang_" & tgBatDau.Month & "_" & tgBatDau.Year & "_c1", bThang, dorongCot1)
            Dim cThang_c2 = createColumn("cThang_" & tgBatDau.Month & "_" & tgBatDau.Year & "_c2", bThang, dorongCot2)
            tgBatDau = tgBatDau.AddMonths(1)
        End While

        Dim bTong = createBand("Tổng")
        Dim cTong_c1 = createColumn("cTong_c1", bTong, dorongCot1)
        Dim cTong_c2 = createColumn("cTong_c2", bTong, dorongCot2)
        TaoBgCot("cTong_c1", "cTong_c2")
        varTuNgay = New DateTime(tuNgay.Year, tuNgay.Month, 1)
        varDenNgay = New DateTime(denNgay.Year, denNgay.Month, DateTime.DaysInMonth(denNgay.Year, denNgay.Month))

    End Sub

    Private Sub TaoCotTuyChonTuan()

        varTuNgay = Convert.ToDateTime(tbTuNgay.EditValue).Date
        varDenNgay = Convert.ToDateTime(tbDenNgay.EditValue).Date

        Dim bTuan = createBand("Từ " & varTuNgay.ToString("dd/MM/yy") & " - " & varDenNgay.ToString("dd/MM/yy"))
        Dim cTuan_c1 = createColumn("cTuan_c1", bTuan, dorongCot1)
        Dim cTuan_c2 = createColumn("cTuan_c2", bTuan, dorongCot2)

    End Sub

    Private Sub TaoBgCot(c1 As String, c2 As String)
        'gdvData.Columns(c1).AppearanceCell.BackColor = Color.LightGray
        'gdvData.Columns(c2).AppearanceCell.BackColor = Color.LightGray
        gdvData.Columns(c1).AppearanceCell.Font = New Font(gdv.Font, FontStyle.Bold)
        gdvData.Columns(c2).AppearanceCell.Font = New Font(gdv.Font, FontStyle.Bold)
    End Sub

    Private Sub CreateDataSource()
        Dim dt As New DataTable
        For i As Integer = 0 To gdvData.Columns.Count - 1
            dt.Columns.Add(gdvData.Columns(i).FieldName, Type.GetType("System.Object"))
        Next
        gdv.DataSource = dt
    End Sub

    Private Function createBand(hienthi As String) As GridBand
        Dim b As New GridBand
        b.Caption = hienthi
        gdvData.Bands.Add(b)
        Return b
    End Function

    Private Function createColumn(fieldName As String, b As GridBand, dorong As Integer) As BandedGridColumn
        Dim c As New BandedGridColumn
        c.Visible = True
        c.Width = dorong
        c.Caption = " "
        c.FieldName = fieldName
        c.OwnerBand = b
        c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
        If fieldName.IndexOf("_c1") >= 0 Then
            c.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        ElseIf fieldName.IndexOf("_c2") >= 0 Then
            c.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            c.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
            c.DisplayFormat.FormatString = "N0"
        End If
        Return c
    End Function

#Region " -- Dung chuot di chuyen grid -- "
    'Dim vitriTrai As Integer = 0
    'Dim vitriTren As Integer = 0
    'Dim isMove As Boolean = False

    'Private Sub gdvData_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseDown
    '    If e.Button = Windows.Forms.MouseButtons.Left Then
    '        isMove = True
    '        vitriTrai = Cursor.Position.X
    '        vitriTrai = Cursor.Position.X
    '    End If
    'End Sub

    'Private Sub gdvData_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseMove
    '    If isMove Then
    '        Cursor = Cursors.SizeAll
    '        If Cursor.Position.X > vitriTrai Then
    '            gdvData.LeftCoord += 10
    '        ElseIf Cursor.Position.X < vitriTrai Then
    '            gdvData.LeftCoord -= 10
    '        End If
    '    End If
    'End Sub

    'Private Sub gdvData_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles gdvData.MouseUp
    '    If isMove Then
    '        isMove = False
    '        Cursor = Cursors.Default
    '    End If
    'End Sub
#End Region

#End Region

    Private Sub btDuyetBC_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDuyetBC.ItemClick
        ShowWaiting("Đang tải báo cáo ...")
        TaoCot()
        If gdvData.DataRowCount > 0 Then gdvData.FocusedRowHandle = 0
        CloseWaiting()
    End Sub

#Region " -- GetDataSource -- "
    Private Sub GetDataTable()
        quy += 1
        Dim sql As String = My.Resources.BaoCao_BaoCaoKinhDoanh
        Dim tungay As DateTime
        Dim denngay As DateTime
        Select Case quy
            Case 1
                tungay = New DateTime(varTuNgay.Year, 1, 1)
                denngay = New DateTime(varDenNgay.Year, 3, DateTime.DaysInMonth(varDenNgay.Year, 3))
            Case 2
                tungay = New DateTime(varTuNgay.Year, 4, 1)
                denngay = New DateTime(varDenNgay.Year, 6, DateTime.DaysInMonth(varDenNgay.Year, 6))
            Case 3
                tungay = New DateTime(varTuNgay.Year, 7, 1)
                denngay = New DateTime(varDenNgay.Year, 9, DateTime.DaysInMonth(varDenNgay.Year, 9))
            Case 4
                tungay = New DateTime(varTuNgay.Year, 10, 1)
                denngay = New DateTime(varDenNgay.Year, 12, DateTime.DaysInMonth(varDenNgay.Year, 12))
        End Select
        sql = sql.Replace("{@TuNgay}", tungay.ToString("dd/MM/yyyy"))
        sql = sql.Replace("{@DenNgay}", denngay.ToString("dd/MM/yyyy"))
        sql = sql.Replace("{@IdNhanVien}", LayIdDanhSachNhanVien)
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        If Not dt Is Nothing Then
            dsSource.Tables.Add(dt)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If

    End Sub

    Private quy As Integer
    Private dsSource As DataSet
    Private Sub GetDataSource()

        dsSource = New DataSet
        Dim dt As New DataTable

        'Neu bao cao theo nam
        If DateDiff(DateInterval.Month, varTuNgay, varDenNgay) = 11 And varTuNgay.Year = varDenNgay.Year Then
            'Xy ly thanh 4 luong, moi luong la 1 quy
            quy = 0
            Dim q1 As New Thread(AddressOf GetDataTable) : q1.Start() : q1.Join()
            Dim q2 As New Thread(AddressOf GetDataTable) : q2.Start() : q2.Join()
            Dim q3 As New Thread(AddressOf GetDataTable) : q3.Start() : q3.Join()
            Dim q4 As New Thread(AddressOf GetDataTable) : q4.Start() : q4.Join()
            'q1.Join() : q2.Join() : q3.Join() : q4.Join()
            If dsSource.Tables.Count > 0 Then dt = dsSource.Tables(0).Clone
            For i As Integer = 0 To dsSource.Tables.Count - 1
                dt.Merge(dsSource.Tables(i))
            Next
        Else 'Cac bao cao khac khong xu ly da luong
            Dim sql As String = My.Resources.BaoCao_BaoCaoKinhDoanh
            sql = sql.Replace("{@TuNgay}", varTuNgay.ToString("dd/MM/yyyy"))
            sql = sql.Replace("{@DenNgay}", varDenNgay.ToString("dd/MM/yyyy"))
            sql = sql.Replace("{@IdNhanVien}", LayIdDanhSachNhanVien)

            dt = ExecuteSQLDataTable(sql)
            If dt Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If

        End If

        Dim dtDistinct As DataTable = New DataView(dt).ToTable(True, "ID", "Ten", "IDDepatment", "IDTieuChi", "NoiDung")
        gdvData.BeginDataUpdate()
        For i As Integer = 0 To dtDistinct.Rows.Count - 1
            gdvData.AddNewRow()
            gdvData.SetFocusedRowCellValue("cPhong", dtDistinct.Rows(i)("IDDepatment"))
            gdvData.SetFocusedRowCellValue("cNhanVien", dtDistinct.Rows(i)("ID"))
            gdvData.SetFocusedRowCellValue("cNoiDungBaoCao", dtDistinct.Rows(i)("NoiDung"))
            gdvData.SetFocusedRowCellValue("cIDTieuChi", dtDistinct.Rows(i)("IDTieuChi"))
        Next
        gdvData.EndDataUpdate()

        Dim value As Object = cbTieuChi.EditValue
        If value Is Nothing Then value = ""
        Select Case value
            Case "Tuần" 'Bind du lieu theo tuan
                For i As Integer = 0 To gdvData.RowCount - 1
                    Dim row() As DataRow = dt.Select("Id=" & gdvData.GetRowCellValue(i, "cNhanVien") & " AND IDTieuChi=" & gdvData.GetRowCellValue(i, "cIDTieuChi"))
                    If row.Length > 0 Then
                        gdvData.SetRowCellValue(i, "cTuan_c1", row(0)("SoLuong"))
                        gdvData.SetRowCellValue(i, "cTuan_c2", row(0)("SoTien"))
                    End If
                Next
                Exit Select
            Case "Tháng" 'Bind du lieu theo thang
                For i As Integer = 0 To gdvData.RowCount - 1
                    Dim row() As DataRow = dt.Select("Id=" & gdvData.GetRowCellValue(i, "cNhanVien") & " AND IDTieuChi=" & gdvData.GetRowCellValue(i, "cIDTieuChi"))
                    If row.Length > 0 Then
                        gdvData.SetRowCellValue(i, "cThang_c1", row(0)("SoLuong"))
                        gdvData.SetRowCellValue(i, "cThang_c2", row(0)("SoTien"))
                    End If
                Next
                Exit Select
            Case "Quý" 'Bind du lieu theo quy
                For i As Integer = 0 To gdvData.RowCount - 1
                    Dim tongSL_tuso As Integer = 0
                    Dim tongSL_mauso As Integer = 0
                    Dim tongST As Double = 0
                    For thang As Integer = varTuNgay.Month To varDenNgay.Month
                        Dim ngayCuoiThang As DateTime = New DateTime(varTuNgay.Year, thang, DateTime.DaysInMonth(varTuNgay.Year, thang))
                        Dim row() As DataRow = dt.Select("Id=" & gdvData.GetRowCellValue(i, "cNhanVien") & " AND IDTieuChi=" & gdvData.GetRowCellValue(i, "cIDTieuChi") & " AND Ngay=#" & ngayCuoiThang.ToString("yyyy/MM/dd") & "#")
                        If row.Length > 0 Then
                            gdvData.SetRowCellValue(i, "cThang" & thang & "_c1", row(0)("SoLuong"))
                            gdvData.SetRowCellValue(i, "cThang" & thang & "_c2", row(0)("SoTien"))
                            tongSL_tuso += tryToDouble(row(0)("SoLuong1"))
                            tongSL_mauso += tryToDouble(row(0)("SoLuong2"))
                            tongST += tryToDouble(row(0)("SoTien"))
                        End If
                    Next
                    If tongSL_mauso <> 0 Or tongSL_tuso <> 0 Then
                        gdvData.SetRowCellValue(i, "cQuy_c1", String.Format("{0:N0}/{1:N0}", tongSL_tuso, tongSL_mauso))
                    End If
                    If tongST > 0 Then
                        gdvData.SetRowCellValue(i, "cQuy_c2", tongST)
                    End If
                Next
                Exit Select
            Case "Năm" 'Bind du lieu theo nam
                For i As Integer = 0 To gdvData.RowCount - 1
                    Dim tongSL_tuso_quy As Integer = 0
                    Dim tongSL_mauso_quy As Integer = 0
                    Dim tongST_quy As Double = 0
                    Dim tongSL_tuso_nam As Integer = 0
                    Dim tongSL_mauso_nam As Integer = 0
                    Dim tongST_nam As Double = 0
                    Dim soQuy As Integer = 1
                    For thang As Integer = 1 To 12
                        Dim ngayCuoiThang As DateTime = New DateTime(varTuNgay.Year, thang, DateTime.DaysInMonth(varTuNgay.Year, thang))
                        Dim row() As DataRow = dt.Select("Id=" & gdvData.GetRowCellValue(i, "cNhanVien") & " AND IDTieuChi=" & gdvData.GetRowCellValue(i, "cIDTieuChi") & " AND Ngay=#" & ngayCuoiThang.ToString("yyyy/MM/dd") & "#")
                        If row.Length > 0 Then
                            gdvData.SetRowCellValue(i, "cThang" & thang & "_c1", row(0)("SoLuong"))
                            gdvData.SetRowCellValue(i, "cThang" & thang & "_c2", row(0)("SoTien"))
                            tongSL_tuso_quy += tryToDouble(row(0)("SoLuong1"))
                            tongSL_mauso_quy += tryToDouble(row(0)("SoLuong2"))
                            tongST_quy += tryToDouble(row(0)("SoTien"))
                            tongSL_tuso_nam += tryToDouble(row(0)("SoLuong1"))
                            tongSL_mauso_nam += tryToDouble(row(0)("SoLuong1"))
                            tongST_nam += tryToDouble(row(0)("SoTien"))
                        End If
                        Select Case thang
                            Case 3, 6, 9, 12
                                If tongSL_mauso_quy <> 0 Or tongSL_tuso_quy <> 0 Then
                                    gdvData.SetRowCellValue(i, "cQuy" & soQuy & "_c1", String.Format("{0:N0}/{1:N0}", tongSL_tuso_quy, tongSL_mauso_quy))
                                End If
                                If tongST_quy > 0 Then
                                    gdvData.SetRowCellValue(i, "cQuy" & soQuy & "_c2", tongST_quy)
                                End If
                                tongSL_tuso_quy = 0
                                tongSL_mauso_quy = 0
                                tongST_quy = 0
                                soQuy += 1
                        End Select
                    Next
                    If tongSL_mauso_nam <> 0 Or tongSL_tuso_nam <> 0 Then
                        gdvData.SetRowCellValue(i, "cTong_c1", String.Format("{0:N0}/{1:N0}", tongSL_tuso_nam, tongSL_mauso_nam))
                    End If
                    If tongST_nam > 0 Then
                        gdvData.SetRowCellValue(i, "cTong_c2", tongST_nam)
                    End If
                Next
                Exit Select
            Case "Tùy Chọn" 'Bind du lieu tu thang den thang
                If varTuNgay > varDenNgay Then
                    ShowBaoLoi("Khoảng thời gian không hợp lý !")
                    Exit Sub
                End If
                For i As Integer = 0 To gdvData.RowCount - 1
                    Dim tongSL_tuso As Integer = 0
                    Dim tongSL_mauso As Integer = 0
                    Dim tongST As Double = 0
                    Dim tgBatDau As DateTime = New DateTime(varTuNgay.Year, varTuNgay.Month, 1)
                    While tgBatDau <= varDenNgay
                        Dim ngayCuoiThang As DateTime = New DateTime(tgBatDau.Year, tgBatDau.Month, DateTime.DaysInMonth(tgBatDau.Year, tgBatDau.Month))
                        Dim row() As DataRow = dt.Select("Id=" & gdvData.GetRowCellValue(i, "cNhanVien") & " AND IDTieuChi=" & gdvData.GetRowCellValue(i, "cIDTieuChi") & " AND Ngay=#" & ngayCuoiThang.ToString("yyyy/MM/dd") & "#")
                        If row.Length > 0 Then
                            gdvData.SetRowCellValue(i, "cThang_" & tgBatDau.Month & "_" & tgBatDau.Year & "_c1", row(0)("SoLuong"))
                            gdvData.SetRowCellValue(i, "cThang_" & tgBatDau.Month & "_" & tgBatDau.Year & "_c2", row(0)("SoTien"))
                            tongSL_tuso += tryToDouble(row(0)("SoLuong1"))
                            tongSL_mauso += tryToDouble(row(0)("SoLuong2"))
                            tongST += tryToDouble(row(0)("SoTien"))
                        End If
                        tgBatDau = tgBatDau.AddMonths(1)
                    End While
                    If tongSL_mauso <> 0 Or tongSL_tuso <> 0 Then
                        gdvData.SetRowCellValue(i, "cTong_c1", String.Format("{0:N0}/{1:N0}", tongSL_tuso, tongSL_mauso))
                    End If
                    If tongST > 0 Then
                        gdvData.SetRowCellValue(i, "cTong_c2", tongST)
                    End If
                Next
                Exit Select
            Case "Tùy Chọn Tuần" 'Bind du lieu theo tuan
                For i As Integer = 0 To gdvData.RowCount - 1
                    Dim row() As DataRow = dt.Select("Id=" & gdvData.GetRowCellValue(i, "cNhanVien") & " AND IDTieuChi=" & gdvData.GetRowCellValue(i, "cIDTieuChi"))
                    If row.Length > 0 Then
                        'If row.Length = 1 Then
                        '    gdvData.SetRowCellValue(i, "cTuan_c1", row(0)("SoLuong"))
                        '    gdvData.SetRowCellValue(i, "cTuan_c2", row(0)("SoTien"))
                        'Else
                        '    gdvData.SetRowCellValue(i, "cTuan_c1", row(0)("SoLuong"))
                        '    gdvData.SetRowCellValue(i, "cTuan_c2", row(0)("SoTien"))
                        'End If
                        Dim _SL As String = ""
                        Dim _ST As Double = 0
                        For j As Integer = 0 To row.Length - 1
                            If Not IsDBNull(row(j)("SoLuong")) Then
                                Dim _tmpSL11 As Double = 0
                                Dim _tmpSL12 As Double = 0
                                Dim _tmpSL21 As Double = 0
                                Dim _tmpSL22 As Double = 0
                                If _SL <> "" Then
                                    Dim _tmpSL1() As String = _SL.Split("/")
                                    If _tmpSL1.Length = 2 Then
                                        Double.TryParse(_tmpSL1(0), _tmpSL11)
                                        Double.TryParse(_tmpSL1(1), _tmpSL12)
                                    End If
                                End If
                                Dim _tmpSL2() As String = row(j)("SoLuong").Split("/")
                                If _tmpSL2.Length = 2 Then
                                    Double.TryParse(_tmpSL2(0), _tmpSL21)
                                    Double.TryParse(_tmpSL2(1), _tmpSL22)
                                End If
                                _SL = Convert.ToString(_tmpSL11 + _tmpSL21) & "/" & Convert.ToString(_tmpSL12 + _tmpSL22)

                            End If
                            If Not IsDBNull(row(j)("SoTien")) Then _ST += row(j)("SoTien")
                        Next
                        gdvData.SetRowCellValue(i, "cTuan_c1", _SL)
                        If _ST > 0 Then gdvData.SetRowCellValue(i, "cTuan_c2", _ST)
                    End If
                Next
                Exit Select
        End Select
    End Sub
#End Region

#Region " -- Convert -- "
    Private Function tryToDouble(str As Object) As Double
        Try
            Return Convert.ToDouble(str)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function tryToDoubleX(str As Object, Optional isMauSo As Boolean = False) As Double
        Try
            If Not isMauSo Then
                Return Convert.ToInt32(str.Substring(0, str.IndexOf("/")))
            Else
                Return Convert.ToInt32(str.Substring(str.IndexOf("/") + 1))
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region

    Private Sub gdvData_RowCellStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles gdvData.RowCellStyle
        If gdvData.GetRowCellValue(e.RowHandle, "cNhanVien") = -1 Then
            e.Appearance.Font = New Font(gdv.Font, FontStyle.Bold)
            e.Appearance.ForeColor = Color.DarkSlateBlue
        End If
    End Sub



End Class
