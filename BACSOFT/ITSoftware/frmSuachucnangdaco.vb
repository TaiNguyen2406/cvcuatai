Imports BACSOFT.Db.SqlHelper
Public Class frmSuachucnangdaco

    Public _idVattuPhanmem As String
    Public _maPhanhe As String
    Public _idChucnangSua = ""
    ' Biến này lưu chuỗi các giá trị Id chức năng
    Dim _listChucnang = ""

    ' Select các bản ghi chức năng theo mã phân hệ và mã vật tư
    Public Sub SelectDanhmucchucnang(maphanhe As Integer)
        Dim sql As String
        If _listChucnang.ToString().Length > 0 Then
            sql = "exec [sp_DSChucnang_IT] @activity = 'xgv', @IdPhanhe = " & maphanhe.ToString() & ", @Ids = '" & _listChucnang & "'"
        Else
            sql = "exec [sp_DSChucnang_IT] @activity = 'xemgv', @IdPhanhe = " & maphanhe.ToString()
        End If
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDSChucnang.DataSource = dt
    End Sub

    Public Sub SelectDulieubaotri(idVattu As Integer)
        Dim sql As String
        sql = "exec sp_BaotriNangCapPhanmem_IT @activity = 'xem', @_Dabaotri = 0, @idvattu = " & _idVattuPhanmem

        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDulieubaotri.DataSource = dt
    End Sub

    ' Select tất cả bản ghi chức năng cũ
    Public Sub SelectChucnangcu()
        If _idChucnangSua.ToString().Trim().Length > 0 Then
            Dim sql As String
            sql = "Select IdChucnangcu from tblDSChucnang_IT where Id = '" & _idChucnangSua & "'"
            Dim tb As DataTable = ExecuteSQLDataTable(sql)

            If Not tb Is Nothing Then
                For Each item As DataRow In tb.Rows
                    _listChucnang = item(0).ToString() ' item(0) có giá trị là chuỗi Id.
                Next
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If
        End If
    End Sub

    Private Sub frmSuachucnangdaco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelectChucnangcu()
        SelectDanhmucchucnang(_maPhanhe)
        SelectDulieubaotri(_idVattuPhanmem)

        If _idChucnangSua.ToString().Length > 0 Then
            btLuuLai.Visible = False
            grdViewDSChucnang.Columns("checkbox").Visible = False
        End If
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        Dim strIdChucnang = ""
        Dim strIdYeucaubaotri = ""

        For i = 0 To grdViewDSChucnang.DataRowCount - 1
            If grdViewDSChucnang.GetRowCellValue(i, "checkbox") Then
                strIdChucnang = strIdChucnang & grdViewDSChucnang.GetRowCellValue(i, "Id") & ","
            End If
        Next i

        If strIdChucnang.Trim().Length > 0 Then
            strIdChucnang = strIdChucnang.Substring(0, strIdChucnang.Trim().Length - 1)
        End If

        If grdViewDulieubaotri.RowCount > 0 Then
            strIdYeucaubaotri = grdViewDulieubaotri.GetFocusedRowCellValue("Id")
        End If

        CType(Owner, frmThemChucnang).SetChucnangcu(strIdChucnang, strIdYeucaubaotri)

        Close()
    End Sub
End Class