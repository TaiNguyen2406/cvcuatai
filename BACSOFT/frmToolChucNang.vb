Imports BACSOFT.Db.SqlHelper

Public Class frmToolChucNang



    Private Sub frmToolChucNang_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        XtraTabControl1.SelectedTabPageIndex = 0

    End Sub


    Private Sub btnLayDiemGocVaKyNang_Click(sender As System.Object, e As System.EventArgs) Handles btnLayDiemGocVaKyNang.Click

        Dim dtNhanSu As DataTable = ExecuteSQLDataTable("select ID,Ten from NHANSU WHERE NoiCtac=74 AND TrangThai=1")
        lstDiemKyNang.Items.Add(New ListItemDataTable("dtNhanSu", dtNhanSu))

        Dim dtDiemChuan As DataTable = ExecuteSQLDataTable("SELECT ID,Diem FROM NLDanhSach")
        lstDiemKyNang.Items.Add(New ListItemDataTable("dtDiemChuan", dtDiemChuan))

        Dim dtDuLieuGoc As DataTable = ExecuteSQLDataTable("select * from tblDiemThiKyNang where isNew = 1 order by NgayThi,IdKyNang,IDNhanVien")
        lstDiemKyNang.Items.Add(New ListItemDataTable("dtDuLieuGoc", dtDuLieuGoc))

        Dim dtKyNangNgayThi As DataTable = ExecuteSQLDataTable("select distinct IDKyNang,NgayThi from tblDiemThiKyNang where isNew = 1 order by NgayThi,IdKyNang")
        lstDiemKyNang.Items.Add(New ListItemDataTable("dtKyNangNgayThi", dtKyNangNgayThi))

        Dim dtKq_New As New DataTable
        dtKq_New.Columns.Add(New DataColumn("IDNhanVien", Type.GetType("System.Int32")))
        dtKq_New.Columns.Add(New DataColumn("IDKyNang", Type.GetType("System.Int32")))
        dtKq_New.Columns.Add(New DataColumn("Diem", Type.GetType("System.Double")))
        dtKq_New.Columns.Add(New DataColumn("NgayThi", Type.GetType("System.DateTime")))
        dtKq_New.Columns.Add(New DataColumn("ThoiGian", Type.GetType("System.Double")))
        dtKq_New.Columns.Add(New DataColumn("isNew", Type.GetType("System.Double")))
        lstDiemKyNang.Items.Add(New ListItemDataTable("dtKq_New", dtKq_New))


        btnLayDiemGocVaKyNang.Enabled = False
        btnTinhDiemThiKyNang.Enabled = True

    End Sub


#Region "-- ListItemDataTable --"
    Private Class ListItemDataTable
        Private _dt As DataTable
        Public Property Dt() As DataTable
            Get
                Return _dt
            End Get
            Set(ByVal value As DataTable)
                _dt = value
            End Set
        End Property
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Sub New(__name As String, __dt As DataTable)
            _name = __name
            _dt = __dt
        End Sub
        Public Overrides Function ToString() As String
            Return _name
        End Function
    End Class
#End Region


    Private Sub lstDiemKyNang_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstDiemKyNang.SelectedIndexChanged
        gdvDiemKyNang.DataSource = Nothing
        If lstDiemKyNang.SelectedIndex >= 0 Then
            gdvDataDiemKyNang.Columns.Clear()
            gdvDiemKyNang.DataSource = TryCast(lstDiemKyNang.SelectedItem, ListItemDataTable).Dt
        End If
    End Sub

    Private Sub btnTinhDiemThiKyNang_Click(sender As System.Object, e As System.EventArgs) Handles btnTinhDiemThiKyNang.Click

        Dim dtKetQua As DataTable = GetDT("dtKq_New").Clone
        lstDiemKyNang.Items.Add(New ListItemDataTable("dtKetQua", dtKetQua))
        'Duyet qua cac ngay thi va mon thi
        Dim dtKyNangNgayThi As DataTable = GetDT("dtKyNangNgayThi")
        Dim dtNhanSu As DataTable = GetDT("dtNhanSu")
        Dim dtDuLieuGoc As DataTable = GetDT("dtDuLieuGoc")


        gdvDataDiemKyNang.Columns.Clear()
        gdvDiemKyNang.DataSource = dtKetQua
        prc.Visible = True
        prc.EditValue = 0
        prc.Properties.Minimum = 0
        prc.Properties.Maximum = dtKyNangNgayThi.Rows.Count * dtNhanSu.Rows.Count

        Dim posBegin As Integer = 0
        Dim posEnd As Integer = 0
        Dim indexRow As Integer = 0

        For i As Integer = 0 To dtKyNangNgayThi.Rows.Count - 1 'duyet qua ngay thi

            Application.DoEvents()

            For j As Integer = 0 To dtNhanSu.Rows.Count - 1 'duyet qua nhan su

                Application.DoEvents()
                Dim row As DataRow = dtKetQua.NewRow()
                row("IDNhanVien") = dtNhanSu.Rows(j)("ID")
                row("IDKyNang") = dtKyNangNgayThi.Rows(i)("IDKyNang")
                row("Diem") = 0
                row("NgayThi") = dtKyNangNgayThi.Rows(i)("NgayThi")

                row("ThoiGian") = 0
                row("isNew") = 0

                Dim kt As Boolean = False
                Dim rowBaiThiCu() As DataRow = dtDuLieuGoc.Select("IDNhanVien = " & row("IDNhanVien") & " AND IDKyNang = " & row("IDKyNang") & " AND NgayThi = #" & CType(row("NgayThi"), DateTime).ToString("MM/dd/yyyy") & "#")
                If rowBaiThiCu.Length > 0 Then
                    row("ThoiGian") = rowBaiThiCu(0)("ThoiGian")
                    row("isNew") = 1
                    kt = True
                End If


                If Not kt Then
                    Dim rowTmp() As DataRow = dtKetQua.Select("IDNhanVien = " & row("IDNhanVien") & " AND IDKyNang = " & row("IDKyNang") & " AND NgayThi < #" & CType(row("NgayThi"), DateTime).ToString("MM/dd/yyyy") & "#", "NgayThi DESC")
                    If rowTmp.Length > 0 Then
                        row("ThoiGian") = rowTmp(0)("ThoiGian")
                        row("isNew") = 0
                    End If
                End If

                If row("ThoiGian") > 0 Then
                    dtKetQua.Rows.InsertAt(row, dtKetQua.Rows.Count)
                    indexRow += 1
                End If

                prc.EditValue += 1
                gdvDataDiemKyNang.FocusedRowHandle = dtKetQua.Rows.Count - 1

            Next

        Next


        'Tinh lai diem
        Dim dtDiemChuan As DataTable = GetDT("dtDiemChuan")
        For i As Integer = 0 To dtKetQua.Rows.Count - 1

            Dim tgMin As Double = GetMinTG(dtKetQua.Select("NgayThi=#" & CType(dtKetQua.Rows(i)("NgayThi"), DateTime).ToString("MM/dd/yyyy") & "# AND IDKyNang = " & dtKetQua.Rows(i)("IDKyNang")))
            Dim diemChuan As Double = dtDiemChuan.Select("ID=" & dtKetQua.Rows(i)("IDKyNang"))(0)("Diem")
            Dim diem As Double = Math.Round((tgMin / dtKetQua.Rows(i)("ThoiGian")) * diemChuan, 2, MidpointRounding.AwayFromZero)
            dtKetQua.Rows(i)("Diem") = diem
        Next


        prc.Visible = False

        btnTinhDiemThiKyNang.Enabled = False
        btnCapNhatCSDL.Enabled = True

    End Sub

    Private Function GetMinTG(row() As DataRow) As Double
        Dim arr As New List(Of Integer)
        For i As Integer = 0 To row.Length - 1
            arr.Add(row(i)("ThoiGian"))
        Next
        arr.Sort()
        If arr.Count > 0 Then Return arr(0)
        Return 0
    End Function

    Private Function GetDT(name As String) As DataTable
        For i As Integer = 0 To lstDiemKyNang.Items.Count - 1
            If lstDiemKyNang.Items(i).ToString = name Then
                Return TryCast(lstDiemKyNang.Items(i), ListItemDataTable).Dt
            End If
        Next
        Return Nothing
    End Function


    Private Sub btnCapNhatCSDL_Click(sender As System.Object, e As System.EventArgs) Handles btnCapNhatCSDL.Click
        Try
            prc.Visible = True
            gdvDiemKyNang.DataSource = GetDT("dtKetQua")
            prc.EditValue = 0
            prc.Properties.Minimum = 0
            prc.Properties.Maximum = gdvDataDiemKyNang.DataRowCount
            BeginTransaction()
            If doDelete("tblDiemThiKyNang", "1=1") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            For i As Integer = 0 To gdvDataDiemKyNang.DataRowCount - 1
                Application.DoEvents()
                AddParameter("@IDNhanVien", gdvDataDiemKyNang.GetRowCellValue(i, "IDNhanVien"))
                AddParameter("@IDKyNang", gdvDataDiemKyNang.GetRowCellValue(i, "IDKyNang"))
                AddParameter("@Diem", gdvDataDiemKyNang.GetRowCellValue(i, "Diem"))
                AddParameter("@NgayThi", gdvDataDiemKyNang.GetRowCellValue(i, "NgayThi"))
                AddParameter("@ThoiGian", gdvDataDiemKyNang.GetRowCellValue(i, "ThoiGian"))
                AddParameter("@FileDinhKem", gdvDataDiemKyNang.GetRowCellValue(i, "FileDinhKem"))
                AddParameter("@isNew", gdvDataDiemKyNang.GetRowCellValue(i, "isNew"))
                If doInsert("tblDiemThiKyNang") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            Next
            If ShowCauHoi("Lưu dữ liệu ?") Then
                ComitTransaction()
            Else
                RollBackTransaction()
            End If

            MsgBox("ok")
        Catch ex As Exception
            RollBackTransaction()
            ShowBaoLoi(LoiNgoaiLe)
        End Try
    End Sub


    Private Sub btTaiDS7So_Click(sender As System.Object, e As System.EventArgs) Handles btTaiDS7So.Click
       
        Dim tb As DataTable = ExecuteSQLDataTable("SELECT SoPhieu,NgayThang,'' as SoYCMoi FROM BANGYEUCAU WHERE Len(SoPhieu)=7")
        If Not tb Is Nothing Then
            gdvSoYC.DataSource = tb
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Private Sub btCapNhapSP_Click(sender As System.Object, e As System.EventArgs) Handles btCapNhapSP.Click
        Dim sql As String = ""
        For i As Integer = 0 To gdvSoYCCT.RowCount - 1
            Application.DoEvents()
            gdvSoYCCT.FocusedRowHandle = i
            gdvSoYCCT.SelectRow(i)
            Dim _SP As Object = LaySoPhieu2("BANGYEUCAU", gdvSoYCCT.GetRowCellValue(i, "NgayThang"))
            If Not _SP Is Nothing Then
                sql = " UPDATE YEUCAUDEN SET SoPhieu='" & _SP & "' WHERE SoPhieu='" & gdvSoYCCT.GetRowCellValue(i, "SoPhieu") & "'"
                sql &= " UPDATE BANGYEUCAU SET SoPhieu='" & _SP & "' WHERE SoPhieu='" & gdvSoYCCT.GetRowCellValue(i, "SoPhieu") & "'"
                sql &= " UPDATE BANGCHAOGIA SET MaSoDatHang='" & _SP & "' WHERE MaSoDatHang='" & gdvSoYCCT.GetRowCellValue(i, "SoPhieu") & "'"
                sql &= " UPDATE tblBaoCaoLichThiCong SET SoYC='" & _SP & "' WHERE SoYC='" & gdvSoYCCT.GetRowCellValue(i, "SoPhieu") & "'"
                If Not ExecuteSQLNonQuery(sql) Is Nothing Then
                    gdvSoYCCT.SetRowCellValue(i, "SoYCMoi", _SP)
                End If
            End If

        Next
    End Sub
End Class



