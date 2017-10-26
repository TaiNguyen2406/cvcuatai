Imports BACSOFT.Db.SqlHelper

Public Class frmCNHDHangNgay
    Public _VanHoa As Boolean = False

    Private Sub frmCNHDHangNgay_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDSNhom()
        If TrangThai.isAddNew Then
            If _VanHoa Then
                Me.Text = "Thêm mục báo cáo điểm văn hóa"
            Else
                Me.Text = "Thêm mục báo cáo hoạt động hàng ngày"
            End If

            tbDiemChuan.EditValue = 100
        Else
            If _VanHoa Then
                Me.Text = "Cập nhật nội dung báo cáo điểm văn hóa"
            Else
                Me.Text = "Cập nhật báo cáo hoạt động hàng ngày"
            End If
            Dim sql As String = ""
            If _VanHoa Then
                sql &= "SELECT * FROM VHDanhSach where ID=@ID"
            Else
                sql &= "SELECT * FROM HDDanhSach where ID=@ID"
            End If
            AddParameterWhere("@ID", objID)
            Dim tb As DataTable = ExecuteSQLDataTable(sql)
            If Not tb Is Nothing Then
                cbNhom.EditValue = tb.Rows(0)("IDNhom")
                cbTen.EditValue = tb.Rows(0)("IDTen")
                tbMoTa.EditValue = tb.Rows(0)("Mota")
                tbDiemChuan.EditValue = tb.Rows(0)("Diem")

            Else
                ShowBaoLoi(LoiNgoaiLe)
            End If

        End If
    End Sub

    Public Sub LoadDSNhom()
        Dim sql As String = ""
        If _VanHoa Then
            sql &= "SELECT ID,Ten FROM VHNhom ORDER BY Ten SELECT ID,Ten FROM VHTen ORDER BY Ten"
        Else
            sql &= "SELECT ID,Ten FROM HDNhom ORDER BY Ten SELECT ID,Ten FROM HDTen ORDER BY Ten"
        End If
        Dim ds As DataSet = ExecuteSQLDataSet(sql)
        If Not ds Is Nothing Then
            cbNhom.Properties.DataSource = ds.Tables(0)
            cbTen.Properties.DataSource = ds.Tables(1)
        Else
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

    Public Function GhiLai() As Boolean
        Dim _TenBang As String = ""
        If _VanHoa Then
            _TenBang = "VHDanhSach"
        Else
            _TenBang = "HDDanhSach"
        End If
        AddParameter("@IDTen", cbTen.EditValue)
        AddParameter("@IDNhom", cbNhom.EditValue)
        AddParameter("@Diem", tbDiemChuan.EditValue)
        AddParameter("@Mota", tbMoTa.EditValue)

        If TrangThai.isAddNew Then
            AddParameter("@IDPhong", 1)
            If doInsert(_TenBang) Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Return False
            End If
        Else
            AddParameterWhere("@ID", objID)
            If doUpdate(_TenBang, "ID=@ID") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Return False
            End If
        End If
        If _VanHoa Then
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmTieuChiBaoCao).LoadDSVanHoa()
        Else
            CType(deskTop.tabMain.SelectedTabPage.Controls(0), frmTieuChiBaoCao).LoadDSHoatDong()
        End If

        Return True
    End Function

    Private Sub btLuuVaThem_Click(sender As System.Object, e As System.EventArgs) Handles btLuuVaThem.Click
        If GhiLai() Then
            'tbTenKyNang.EditValue = ""
            tbMoTa.EditValue = ""
            tbDiemChuan.EditValue = 10
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