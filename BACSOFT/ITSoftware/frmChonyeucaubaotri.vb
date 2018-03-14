Imports BACSOFT.Db.SqlHelper

Public Class frmChonyeucaubaotri

    Public _idVattuPhanmem As String
    Public _maPhanhe As String
    Public _idChucnangSua = ""
    Public _flagPhanhe As Boolean = False
    Public _xemthongtin As Boolean = False

    ' Method này xem các bản ghi bảo trì theo mã vật tư
    Public Sub SelectDulieubaotri(idVattu As Integer)

        Dim sql As String
        sql = "exec sp_BaotriNangCapPhanmem_IT @activity = 'xem', @_Dabaotri = 0, @idvattu = " & _idVattuPhanmem
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDulieubaotri.DataSource = dt
    End Sub

    Private Sub btDong_Click(sender As Object, e As EventArgs) Handles btDong.Click
        Close()
    End Sub

    Private Sub btLuuLai_Click(sender As Object, e As EventArgs) Handles btLuuLai.Click
        If grdViewDulieubaotri.RowCount = 0 Then
            Close()
            Exit Sub
        End If

        ' Lưu chuỗi ID yêu cầu bảo trì
        Dim strIdYeucaubaotri = ""
        Dim strNoidungYeucaubaotri = ""

        For i = 0 To grdViewDulieubaotri.DataRowCount - 1
            If grdViewDulieubaotri.GetRowCellValue(i, "checkbox") Then
                strIdYeucaubaotri = strIdYeucaubaotri & grdViewDulieubaotri.GetRowCellValue(i, "Id") & ","
                strNoidungYeucaubaotri = strNoidungYeucaubaotri & grdViewDulieubaotri.GetRowCellValue(i, "Noidungthongbao") & "; "
            End If
        Next i

        If strIdYeucaubaotri.Trim().Length > 0 Then
            strIdYeucaubaotri = strIdYeucaubaotri.Substring(0, strIdYeucaubaotri.Trim().Length - 1)
        End If

        If _flagPhanhe Then
            CType(Owner, frmThemPhanhe).SetThongtinYeucaubaotri(strIdYeucaubaotri, strNoidungYeucaubaotri)
        Else
            CType(Owner, frmThemChucnang).SetThongtinYeucaubaotri(strIdYeucaubaotri, strNoidungYeucaubaotri)
        End If

        Close()
    End Sub
    Private Sub frmChonyeucaubaotri_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelectDulieubaotri(_idVattuPhanmem)

        If _xemthongtin Then
            Text = "Xem thông tin yêu cầu bảo trì"
            btLuuLai.Visible = False
            grdViewDulieubaotri.Columns("checkbox").Visible = False
        End If
    End Sub
End Class