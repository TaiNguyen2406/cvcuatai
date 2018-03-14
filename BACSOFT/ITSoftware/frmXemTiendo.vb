Imports BACSOFT.Db.SqlHelper
Public Class frmXemTiendo

    ' Biến này lưu tham chiếu ID vật tư phần mềm
    Public _idVattuPhanmem = ""

    Public Sub SelectDanhsachphanmem()
        Dim sql As String
        sql = "exec [sp_Xemtiendo_IT] @activity = 'xemtiendoduan', @idvattu = " & _idVattuPhanmem
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdDSphanmem.DataSource = dt
    End Sub

    Public Sub SelectTiendophanhe(idVattu As Integer)
        ' Truyền tham số mã vật tư phần mềm idMavattu
        Dim sql As String
        sql = "exec [sp_Xemtiendo_IT] @activity = 'xemtiendophanhe', @idvattu = '" & idVattu.ToString() & "'"
        Dim dt As DataTable = ExecuteSQLDataTable(sql)
        grdTiendophanhe.DataSource = dt
    End Sub

    Private Sub frmXemTiendo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowWaiting("Đang tải dữ liệu ...")
        SelectDanhsachphanmem()
        CloseWaiting()
    End Sub

    Private Sub grdViewDSphanmem_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdViewDSphanmem.FocusedRowChanged
        Dim idVatTu = grdViewDSphanmem.GetFocusedRowCellValue("IdVatTu")
        SelectTiendophanhe(idVatTu)
    End Sub
End Class
