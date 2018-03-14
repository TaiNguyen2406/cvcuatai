Imports BACSOFT.Db.SqlHelper
Public Class frmGiaoXuLy
    Public _NoiDung As String
    Public _SoYC As String
    Public _Chuyengiao As Boolean
    Public _id As Object
    Public _email, _sdt As Object

    Private Sub frmGiaoXuLy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSoYC.EditValue = _SoYC
        memoNoiDung.EditValue = _NoiDung
        loadNhanVien()
        Dim str As String = ""
        If IsDBNull(_sdt) Then _sdt = ""
        If IsDBNull(_email) Then _email = ""

        If _sdt Is Nothing Or _sdt = "" Then
            If Len(_email) >= 8 Then
                str = " SELECT Ten,"
                str &= "(select Noictac FROM NHANSU WHERE REPLACE(LOWER(Email), ' ', '') LIKE '%' + @EMAIL + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select Chamsoc FROM NHANSU WHERE REPLACE(LOWER(Email), ' ', '') LIKE '%' + @EMAIL + '%' and noictac <> 74)"

                str &= " SELECT TEN,"
                str &= " (select Ten from KHACHHANG WHERE  REPLACE(LOWER(ttcEmail), ' ', '') LIKE '%' + @EMAIL + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select IDTakecare from KHACHHANG WHERE REPLACE(LOWER(ttcEmail), ' ', '') LIKE '%' + @EMAIL + '%') and Noictac = 74"

                AddParameterWhere("@EMAIL", Strings.Replace((_email.ToString).ToLower, " ", ""))
            End If
            
        End If

        If _email Is Nothing Or _email = "" Then
            If Len(_sdt) >= 8 Then
                str = " SELECT Ten,"
                str &= "(select Noictac FROM NHANSU WHERE replace(replace((ISNULL( DienthoaiCQ, '') + '\' + ISNULL(DienthoaiNR, '') + '\' + ISNULL(Mobile, '')  + '\' + ISNULL(Mobile1, '')), ' ', ''), '.','') LIKE '%' + @SDT + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select Chamsoc FROM NHANSU WHERE replace(replace((ISNULL( DienthoaiCQ, '') + '\' + ISNULL(DienthoaiNR, '') + '\' + ISNULL(Mobile, '')  + '\' + ISNULL(Mobile1, '')), ' ', ''), '.','') LIKE '%' + @SDT + '%' )"

                str &= " SELECT TEN,"
                str &= " (select Ten from KHACHHANG WHERE replace(replace(ttcDienthoai, ' ', ''), '.','')  LIKE '%' + @SDT + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select IDTakecare from KHACHHANG WHERE replace(replace(ttcDienthoai, ' ', ''), '.','')  LIKE '%' + @SDT + '%') and Noictac = 74"

                AddParameterWhere("@SDT", Strings.Right(Strings.Replace(Strings.Replace(_sdt.ToString, " ", ""), ".", ""), 8))

            End If
          
        End If

        If Not _sdt Is Nothing And _sdt <> "" And Not _email Is Nothing And _email <> "" Then
            If Len(_sdt) >= 8 And Len(_email) >= 8 Then
                str = " SELECT Ten,"
                str &= "(select Noictac FROM NHANSU WHERE replace(replace((ISNULL( DienthoaiCQ, '') + '\' + ISNULL(DienthoaiNR, '') + '\' + ISNULL(Mobile, '')  + '\' + ISNULL(Mobile1, '')), ' ', ''), '.','') LIKE '%' + @SDT + '%' or REPLACE(LOWER(Email), ' ', '') LIKE '%' + @EMAIL + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select Chamsoc FROM NHANSU WHERE replace(replace((ISNULL( DienthoaiCQ, '') + '\' + ISNULL(DienthoaiNR, '') + '\' + ISNULL(Mobile, '')  + '\' + ISNULL(Mobile1, '')), ' ', ''), '.','') LIKE '%' + @SDT + '%' or REPLACE(LOWER(Email), ' ', '') LIKE '%' + @EMAIL + '%' and noictac <> 74)"

                str &= " SELECT TEN,"
                str &= " (select Ten from KHACHHANG WHERE replace(replace(ttcDienthoai, ' ', ''), '.','')  LIKE '%' + @SDT + '%' OR REPLACE(LOWER(ttcEmail), ' ', '') LIKE '%' + @EMAIL + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select IDTakecare from KHACHHANG WHERE replace(replace(ttcDienthoai, ' ', ''), '.','')  LIKE '%' + @SDT + '%' OR REPLACE(LOWER(ttcEmail), ' ', '') LIKE '%' + @EMAIL + '%') and Noictac = 74"

                AddParameterWhere("@SDT", Strings.Right(Strings.Replace(Strings.Replace(_sdt.ToString, " ", ""), ".", ""), 8))
                AddParameterWhere("@EMAIL", Strings.Replace((_email.ToString).ToLower, " ", ""))
            End If
            If Len(_email) >= 8 And Len(_sdt) < 8 Then
                str = " SELECT Ten,"
                str &= "(select Noictac FROM NHANSU WHERE REPLACE(LOWER(Email), ' ', '') LIKE '%' + @EMAIL + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select Chamsoc FROM NHANSU WHERE REPLACE(LOWER(Email), ' ', '') LIKE '%' + @EMAIL + '%' and noictac <> 74)"

                str &= " SELECT TEN,"
                str &= " (select Ten from KHACHHANG WHERE  REPLACE(LOWER(ttcEmail), ' ', '') LIKE '%' + @EMAIL + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select IDTakecare from KHACHHANG WHERE REPLACE(LOWER(ttcEmail), ' ', '') LIKE '%' + @EMAIL + '%') and Noictac = 74"

                AddParameterWhere("@EMAIL", Strings.Replace((_email.ToString).ToLower, " ", ""))
            End If

            If Len(_sdt) >= 8 And Len(_email) < 8 Then
                str = " SELECT Ten,"
                str &= "(select Noictac FROM NHANSU WHERE replace(replace((ISNULL( DienthoaiCQ, '') + '\' + ISNULL(DienthoaiNR, '') + '\' + ISNULL(Mobile, '')  + '\' + ISNULL(Mobile1, '')), ' ', ''), '.','') LIKE '%' + @SDT + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select Chamsoc FROM NHANSU WHERE replace(replace((ISNULL( DienthoaiCQ, '') + '\' + ISNULL(DienthoaiNR, '') + '\' + ISNULL(Mobile, '')  + '\' + ISNULL(Mobile1, '')), ' ', ''), '.','') LIKE '%' + @SDT + '%' )"

                str &= " SELECT TEN,"
                str &= " (select Ten from KHACHHANG WHERE replace(replace(ttcDienthoai, ' ', ''), '.','')  LIKE '%' + @SDT + '%')Noictac"
                str &= " FROM NHANSU WHERE ID IN ( select IDTakecare from KHACHHANG WHERE replace(replace(ttcDienthoai, ' ', ''), '.','')  LIKE '%' + @SDT + '%') and Noictac = 74"
                AddParameterWhere("@SDT", Strings.Right(Strings.Replace(Strings.Replace(_sdt.ToString, " ", ""), ".", ""), 8))
            End If
        End If
        
        If str = "" Then Exit Sub
        Dim dt As New DataSet
        dt = ExecuteSQLDataSet(str)
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        str = ""
        If dt.Tables(0).Rows.Count > 0 Then

            For i = 0 To dt.Tables(0).Rows.Count - 1
                str &= dt.Tables(0).Rows(i)("Ten") & " - "
                Dim tenkh As New DataTable
                tenkh = ExecuteSQLDataTable("SELECT Ten FROM KHACHHANG WHERE ID = " & dt.Tables(0).Rows(i)("Noictac").ToString)
                If tenkh Is Nothing Then ShowBaoLoi(LoiNgoaiLe)
                str &= tenkh.Rows(0)(0) & "; " & vbCrLf
            Next
        End If
        If dt.Tables(1).Rows.Count > 0 Then
            For i = 0 To dt.Tables(1).Rows.Count - 1
                str &= dt.Tables(1).Rows(i)("Ten") & " - "
                str &= dt.Tables(1).Rows(i)("Noictac") & "; " & vbCrLf
            Next
        End If
        If str <> "" Then
            lblNote.Text = "Note: Nhân viên từng takecare:  " + str
        End If

    End Sub

    Private Sub btnHuy_Click(sender As Object, e As EventArgs) Handles btnHuy.Click
        Close()
    End Sub

    Public Sub loadNhanVien()
        Dim dt As New DataTable
        dt = ExecuteSQLDataTable(" SELECT nhansu.ID,nhansu.TEN, email, DEPATMENT.Ten as TenPhong FROM NHANSU inner join DEPATMENT on DEPATMENT.ID = NHANSU.IDDepatment where IDDepatment = 3 or IDDepatment = 4 and Trangthai = 1 and  Noictac = 74 ")
        If dt Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If
        gcbNhanVien.Properties.DataSource = dt

        With gcbNhanVien.Properties.View.Columns
            Dim colID = .AddField("ID")
            colID.Caption = "ID"
            colID.Visible = False
            Dim colTenNhom = .AddField("TenPhong")
            colTenNhom.Caption = "Phòng "
            colTenNhom.VisibleIndex = 0
            colTenNhom.GroupIndex = 0
            Dim colTenNV = .AddField("TEN")
            colTenNV.Caption = "Danh sách nhân viên"
            colTenNV.VisibleIndex = 1
            colTenNV.Width = 350
            colTenNV.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
            Dim colEmail = .AddField("email")
            colEmail.Caption = "email"
            colEmail.Visible = False
        End With

        If _Chuyengiao = False Then
            lblLyDo.Visible = False
            txtLyDo.Visible = False
        Else
            lblLyDo.Visible = True
            txtLyDo.Visible = True
        End If
    End Sub

    Private Sub btnGiao_Click(sender As Object, e As EventArgs) Handles btnGiao.Click
        If gcbNhanVien.EditValue Is Nothing Then
            ShowCanhBao("Bạn chưa chọn nhân viên được giao xử lý")
            Exit Sub
        End If
        If (txtLyDo.EditValue Is Nothing Or IsDBNull(txtLyDo.EditValue) Or txtLyDo.EditValue = "") And _Chuyengiao = True Then
            ShowCanhBao("Lý do không thể để trống")
            Exit Sub
        End If
        If (memoNoiDung.EditValue Is Nothing Or IsDBNull(txtLyDo.EditValue) Or memoNoiDung.EditValue = "") Then
            ShowCanhBao("Nội dung không thể để trống")
            Exit Sub
        End If
       

        If (_Chuyengiao = False) Then
            Dim dt As New DataTable
            dt = ExecuteSQLDataTable("SELECT TakeCare,TrangThai FROM YeuCauTuWeb WHERE Sophieu = '" & _SoYC & "'")
            If dt Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            If dt.Rows(0)("TrangThai") <> 0 Then
                ShowThongBao("Bạn đã giao việc. Không thể giao lại")
                Exit Sub
            End If
        End If

        'If _Chuyengiao = True Then
        '    If dt.Rows(0)("TakeCare") <> Convert.ToInt32(TaiKhoan) Then
        '        ShowThongBao("Bạn không thể chuyển giao việc. ")
        '        Exit Sub
        '    End If
        'End If
        Dim nv As New DataTable
        nv = ExecuteSQLDataTable("SELECT TEN FROM NHANSU WHERE ID = " & Convert.ToInt32(TaiKhoan))
        If nv Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        Dim str, tieude As String
        str = "Yêu cầu web: " & txtSoYC.EditValue
        str &= "<BR/> <BR/>" & memoNoiDung.EditValue.ToString.Replace(Chr(10), " <BR /> ")
        str &= "<BR/> <BR/> Người giao: " & nv.Rows(0)(0)
        If _Chuyengiao = False Then
            tieude = "Giao xử lý yêu cầu từ khách hàng"
        Else
            tieude = "Chuyển giao xử lý yêu cầu từ khách hàng"
        End If
        Dim _email As String = ""
        _email = gcbNhanVien.Properties.GetRowByKeyValue(gcbNhanVien.EditValue)("email")
        Dim tg = GetServerTime()
        If _Chuyengiao = False Then
            AddParameter("@TakeCare", gcbNhanVien.Properties.GetRowByKeyValue(gcbNhanVien.EditValue)("ID"))
            AddParameter("@NguoiGiao", Convert.ToInt32(TaiKhoan))
            AddParameter("@NgayGiao", tg)
            AddParameter("@TrangThai", 1)
            AddParameterWhere("@Sophieu1", _SoYC)
            If doUpdate("YeuCauTuWeb", "Sophieu = @Sophieu1") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
        Else
            AddParameter("@TakeCareGiao", Convert.ToInt32(TaiKhoan))
            AddParameter("@TakeCareNhan", gcbNhanVien.Properties.GetRowByKeyValue(gcbNhanVien.EditValue)("ID"))
            AddParameter("@NgayChuyen", tg)
            AddParameter("@LyDo", txtLyDo.EditValue)
            AddParameter("@IDYCWeb", _id)
            If doInsert("LichSuChuyenGiaoYCWeb") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            AddParameter("@TakeCare", gcbNhanVien.Properties.GetRowByKeyValue(gcbNhanVien.EditValue)("ID"))
            AddParameterWhere("@Sophieu", _SoYC)
            If doUpdate("YeuCauTuWeb", "Sophieu = @Sophieu") Is Nothing Then
                ShowBaoLoi(LoiNgoaiLe)
                Exit Sub
            End If
            _Chuyengiao = False
        End If

        If _email <> "" Then
            Utils.Email.Send(_email, tieude, str)
        End If

    End Sub
End Class