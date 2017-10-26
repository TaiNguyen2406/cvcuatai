Imports BACSOFT.Db.SqlHelper

Public Class frmCNKyNang

    Private Sub frmCNKyNang_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSNhomKyNang()
        If TrangThai.isAddNew Then
            Me.Text = "Thêm kỹ năng"
            tbDiemChuan.EditValue = 100
        Else
            Me.Text = "Cập nhật kỹ năng"
            AddParameterWhere("@ID", objID)
            Dim tb As DataTable = ExecuteSQLDataTable("SELECT * FROM NLDanhSach where ID=@ID")
            If Not tb Is Nothing Then
                cbTenKyNang.EditValue = tb.Rows(0)("IDTen")
                tbMoTa.EditValue = tb.Rows(0)("Mota")
                tbDiemChuan.EditValue = tb.Rows(0)("Diem")
                cbNhom.EditValue = tb.Rows(0)("IDNhomKN")
            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If
    End Sub

    Public Sub LoadDSNhomKyNang()
        AddParameterWhere("@NhomKyNang", LoaiTuDien.NhomNoiDungThiCong)
        Dim ds As DataSet = ExecuteSQLDataSet("SELECT  Ma,NoiDung AS TenNhom FROM tblTuDien WHERE Loai=@NhomKyNang ORDER BY Ma SELECT ID,Ten AS TenKN FROM NLTen ORDER BY Ten")
        If Not ds Is Nothing Then
            cbNhom.Properties.DataSource = ds.Tables(0)
            cbTenKyNang.Properties.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Function GhiLai() As Boolean
        AddParameter("@IDTen", cbTenKyNang.EditValue)
        AddParameter("@IDNhomKN", cbNhom.EditValue)
        AddParameter("@Diem", tbDiemChuan.EditValue)
        AddParameter("@Mota", tbMoTa.EditValue)
        AddParameter("@Loai", LoaiNangLuc.KyNang)
        If TrangThai.isAddNew Then
            If doInsert("NLDanhSach") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Return False
            End If
        Else
            AddParameterWhere("@ID", objID)
            If doUpdate("NLDanhSach", "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Return False
            End If
        End If
        CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmDanhMucNangLuc).LoadDSKyNang()
        Return True
    End Function

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        If GhiLai() Then
            'tbTenKyNang.EditValue = ""
            tbMoTa.EditValue = ""
            tbDiemChuan.EditValue = 100
        End If
    End Sub

    Private Sub btLuu_Click(sender As System.Object, e As System.EventArgs) Handles btLuu.Click
        If GhiLai() Then
            Me.Close()
        End If
    End Sub

    Private Sub btDong_Click(sender As System.Object, e As System.EventArgs) Handles btDong.Click
        Me.Close()
    End Sub

End Class