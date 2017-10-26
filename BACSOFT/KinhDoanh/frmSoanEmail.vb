Imports System.IO
Imports System.Drawing
Imports BACSOFT.Db.SqlHelper
Imports System.Xml
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security
Imports System.Net

Public Class frmSoanEmail


    Private URL_FILE_MAYCHU As String = ServerName & "\Data$\EmailKinhDoanh\"
    Private URL_Upload As String = "http://baoanjsc.com.vn/Images/EmailMarketing/"
    Private FTP_Hostname As String = "ftp://ftp.baoanjsc.com.vn/httpdocs/Images/EmailMarketing/"
    Private FTP_Username As String = "nhbaosov"
    Private FTP_Passsword As String = "l-^v9NoIP4-3(!"

    Private _strDaGui As String = ""
    Private arrEmail As List(Of DiaChiEmail)
    Private isOk As Boolean = False
    Private tbCanGui As DataTable
    Private _nguoiLapEmail As String = ""
    Private _matKhauEmailCaNhan As String = ""

    Private _IdEmail As Object

    Private _TrangThaiGui As Boolean = False 'dang gui = true, da gui xong = false


    Private _SoEmailDaGui As Integer = 0
    Private strHtmlNoiDung As String = ""
    Private isDaDuyet As Boolean

    Public isNguoiSoanEmail As Boolean



    Private Sub frmSoanEmail_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        txtNoiDung.Navigate(Application.StartupPath & "\ckeditor\index.htm")
        rcbDoiTuongNhanEmail.DataSource = getDataCbDoiTuongNhanEmail()


        'WebKitBrowser1.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:36.0) Gecko/20100101 Firefox/36.0"
        'WebKitBrowser1.Navigate(Application.StartupPath & "\ckeditor\index.htm")
        'WebKitBrowser1.Url = New Uri(Application.StartupPath & "\ckeditor\index.htm")



        cmbLocTrangThaiEmail.SelectedIndex = 0
        txtMaKH.EditValue = DBNull.Value


        arrEmail = New List(Of DiaChiEmail)
        txtMaKH.EditValue = DBNull.Value
        gdv.DataSource = ExecuteSQLDataTable("SELECT DISTINCT Id,convert(bit,0)colChon,(SELECT Ten FROM NHANSU AS NS WHERE NS.ID=NHANSU.ChamSoc)TakeCare,XungHo, Ten,Email,noictac,(SELECT ttcMa FROM KHACHHANG WHERE ID = NHANSU.noictac)KhachHang,(SELECT Ten FROM KHACHHANG WHERE ID = NHANSU.noictac)TenCTy,Isnull(ChamSoc,0)ChamSoc,DoiTuongNhanEmail FROM NHANSU WHERE rtrim(lTrim( Email)) <>'' and ChamSoc in (0," & TaiKhoan & ")")


        If TrangThai.isAddNew Or TrangThai.isCopy Then
            Me.Text = "Tạo mới nội dung Email kinh doanh"
            cmbPtrangThai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnChuanBiGuiEmail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            cmbEmaiGui.SelectedIndex = 0
        ElseIf TrangThai.isUpdate Then
            Me.Text = "Xem nội dung Email kinh doanh"
        End If

        If TrangThai.isUpdate Or TrangThai.isCopy Then
            AddParameter("@Id", MaTuDien)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT * FROM EMAILKD WHERE Id = @Id")
            txtTieuDe.Text = dt.Rows(0)("TieuDe").ToString
            tbGhiChu.EditValue = dt.Rows(0)("GhiChu")
            If Not IsDBNull(dt.Rows(0)("LoaiChuDe")) Then
                Select Case dt.Rows(0)("LoaiChuDe")
                    Case 0
                        cbLoaiChuDe.EditValue = "Sản phẩm/Dịch vụ/Đào tạo/Hội thảo"
                    Case 1
                        cbLoaiChuDe.EditValue = "Giá bán/Khuyến mãi"
                End Select
            End If






            strHtmlNoiDung = dt.Rows(0)("NoiDung").ToString

            'txtNoiDung.Document.InvokeScript("setValue", New String() {dt.Rows(0)("NoiDung").ToString})






            Select Case dt.Rows(0)("TrangThai").ToString
                Case ""
                    rGroupTrangThai.SelectedIndex = 0
                Case "True"
                    rGroupTrangThai.SelectedIndex = 1
                Case "False"
                    rGroupTrangThai.SelectedIndex = 2
            End Select

            'If rGroupTrangThai.SelectedIndex = 1 And TrangThai.isUpdate Then
            '    btnChuanBiGuiEmail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            'Else
            '    btnChuanBiGuiEmail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            'End If
            'Lay danh sach nguoi nhan
            Dim strNguoiNhan() As String = dt.Rows(0)("NguoiNhan").ToString.Split(New Char() {";"c})
            Dim dtKH As DataTable = CType(gdv.DataSource, DataTable)
            For Each str As String In strNguoiNhan
                If str.Trim = "" Then Continue For
                Dim row() As DataRow = dtKH.Select("Id=" & str)
                If row.Length > 0 Then
                    Dim r As DataRow = row(0)
                    Dim st = True

                    If Not IsDBNull(row(0)("DoiTuongNhanEmail")) Then
                        Select Case cbLoaiChuDe.EditValue
                            Case "Sản phẩm/Dịch vụ/Đào tạo/Hội thảo"

                                Select Case row(0)("DoiTuongNhanEmail")
                                    Case 0, 1, 6
                                        r("colChon") = False
                                        st = False
                                    Case 2, 3, 4, 5
                                        r("colChon") = True
                                End Select
                            Case Else
                                Select Case row(0)("DoiTuongNhanEmail")
                                    Case 0, 1, 2, 6
                                        r("colChon") = False
                                        st = False
                                    Case 3, 4, 5
                                        r("colChon") = True
                                End Select
                        End Select
                    Else
                        r("colChon") = True

                    End If
                    If st = True Then
                        arrEmail.Add(New DiaChiEmail(r("Id"), r("XungHo").ToString, r("Ten"), r("Email"), r("ChamSoc")))
                    End If

                End If
            Next
            lblNguoiNhan.Text = String.Format("Người nhận: {0:N0} email.", arrEmail.Count)
            'Lay danh sach file dinh kem
            If TrangThai.isUpdate Then
                Dim strFile() As String = dt.Rows(0)("Files").ToString.Split(New Char() {";"c})
                For Each f As String In strFile
                    If f.Trim = "" Then Continue For
                    Dim obj As New FileDinhKem(URL_FILE_MAYCHU & f, True)
                    Dim item As New ListViewItem
                    item.Tag = obj
                    item.Text = obj.GetFileNameX
                    imgList.Images.Add(obj.GetFileName, obj.GetIcon)
                    item.ImageKey = obj.GetFileName
                    lstFiles.Items.Add(item)
                Next
            End If
            lblFileKem.Text = lstFiles.Items.Count & " files kèm"
            'Cach gui email
            If TrangThai.isUpdate Then
                cmbEmaiGui.SelectedIndex = dt.Rows(0)("CachGui")
                cmbDiaChiEmailGui.Text = dt.Rows(0)("EmailGui").ToString
                _nguoiLapEmail = dt.Rows(0)("NguoiGui").ToString
                _TrangThaiGui = dt.Rows(0)("TrangThaiGui").ToString
                _strDaGui = dt.Rows(0)("DaGui").ToString
                _IdEmail = dt.Rows(0)("Id")
                If _TrangThaiGui Then ' dang gui email

                End If
            End If

            If TrangThai.isUpdate Then
                If dt.Rows(0)("NguoiGui").ToString <> TaiKhoan Then
                    btnLuu.Enabled = False
                    cmbDiaChiEmailGui.Text = ExecuteSQLDataTable("select email from nhansu where Id = " & TaiKhoan).Rows(0)(0).ToString
                End If
            End If
        End If


        If Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.Admin) And Not KiemTraQuyenSuDungKhongCanhBao("Menu", Me.Tag, DanhMucQuyen.TPKinhDoanh) Then
            cmbPtrangThai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

        'Hien nut gui email
        If TrangThai.isAddNew Or TrangThai.isCopy Or rGroupTrangThai.SelectedIndex <> 1 Then
            btnChuanBiGuiEmail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            btnChuanBiGuiEmail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If


        'MsgBox(TaiKhoan)

    End Sub

    Private Sub txtNoiDung_DocumentCompleted(sender As Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles txtNoiDung.DocumentCompleted
        If TrangThai.isUpdate Or TrangThai.isCopy Then
            'txtNoiDung.Document.GetElementById("txtNoiDung").SetAttribute("value", strHtmlNoiDung)
            txtNoiDung.Document.InvokeScript("GanGiaTri", New String() {strHtmlNoiDung})
        End If
    End Sub

    Private Sub frmSoanEmail_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If isOk Then Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Function getDataCbDoiTuongNhanEmail() As DataTable
        Dim tb As New DataTable
        tb.Columns.Add("ID", Type.GetType("System.Int32"))
        tb.Columns.Add("Ten", Type.GetType("System.String"))
        Dim r1 As DataRow
        r1 = tb.NewRow
        r1("ID") = 0
        r1("Ten") = "Chính hãng"
        Dim r2 As DataRow
        r2 = tb.NewRow
        r2("ID") = 1
        r2("Ten") = "Đăng ký không làm phiền"
        Dim r3 As DataRow
        r3 = tb.NewRow
        r3("ID") = 2
        r3("Ten") = "End user - Kỹ thuật"
        Dim r4 As DataRow
        r4 = tb.NewRow
        r4("ID") = 3
        r4("Ten") = "End user - Mua hàng, Lãnh đạo"
        Dim r5 As DataRow
        r5 = tb.NewRow
        r5("ID") = 4
        r5("Ten") = "Thương mại"
        Dim r6 As DataRow
        r6 = tb.NewRow
        r6("ID") = 5
        r6("Ten") = "Trường học"
        Dim r7 As DataRow
        r7 = tb.NewRow
        r7("ID") = 6
        r7("Ten") = "Email lỗi"
        tb.Rows.Add(r1)
        tb.Rows.Add(r2)
        tb.Rows.Add(r3)
        tb.Rows.Add(r4)
        tb.Rows.Add(r5)
        tb.Rows.Add(r6)
        tb.Rows.Add(r7)
        Return tb
    End Function

    Private Function FindRowHandleByDataRow(ByVal row As DataRow) As Integer
        Dim I As Integer
        For I = 0 To gdvData.DataRowCount - 1
            If row.Equals(gdvData.GetDataRow(I)) Then
                Return I
            End If
        Next
        Return DevExpress.XtraGrid.GridControl.InvalidRowHandle
    End Function

    Private Sub frmSoanEmail_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        wDlg.Hide()
    End Sub

#Region " -- Class FileDinhKem -- "
    Private Class FileDinhKem
        Public Sub New(path As String, Optional isServer As Boolean = False)
            _fullpath = path
            _isServer = isServer
        End Sub

        Private _fullpath As String

        Public Property Fullpath() As String
            Get
                Return _fullpath
            End Get
            Set(ByVal value As String)
                _fullpath = value
            End Set
        End Property
        Private _isServer As Boolean
        Public ReadOnly Property IsServer() As Boolean
            Get
                Return _isServer
            End Get
        End Property
        Public Sub setStatusIsServer(st As Boolean)
            _isServer = st
        End Sub

        Public ReadOnly Property GetFileName() As String
            Get
                Return System.IO.Path.GetFileName(_fullpath)
            End Get
        End Property

        Public ReadOnly Property GetFileNameX() As String
            Get
                If IsServer Then
                    Dim str As String = ""
                    str = GetFileNameWithoutExt
                    str = str.Substring(0, str.Length - 16)
                    str = str & GetFileExt
                    Return str
                Else
                    Return GetFileName
                End If
            End Get
        End Property

        Public ReadOnly Property GetFileNameWithoutExt() As String
            Get
                Return System.IO.Path.GetFileNameWithoutExtension(_fullpath)
            End Get
        End Property

        Public ReadOnly Property GetFileExt() As String
            Get
                Return System.IO.Path.GetExtension(_fullpath)
            End Get
        End Property

        Public ReadOnly Property GetIcon() As Icon
            Get
                Try
                    Return Icon.ExtractAssociatedIcon(_fullpath)
                Catch ex As Exception
                    Return Icon.ExtractAssociatedIcon(Application.StartupPath & "\sa.cfg")
                End Try
            End Get
        End Property


    End Class
#End Region

#Region " -- Class DiaChiEmail -- "
    Private Class DiaChiEmail
        Public Sub New(strId As Integer, strXungHo As String, strTen As String, strEmail As String, IdChamSoc As Integer)
            _Id = strId
            _Ten = strTen
            _Email = strEmail
            _IdChamSoc = IdChamSoc
            _XungHo = strXungHo
            AddParameter("@Id", _IdChamSoc)
            Dim dt As DataTable = ExecuteSQLDataTable("SELECT Ten,Email,MatKhauEmail,ChuKyEmail,(SELECT Ten FROM KHACHHANG WHERE KHACHHANG.ID=NHANSU.NoiCtac)TenCTy FROM NHANSU WHERE ID = @Id AND Email is not null")
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 AndAlso dt.Rows(0)("MatKhauEmail").ToString <> "" Then
                _TenNvTakeCare = dt.Rows(0)("Ten")
                _EmailNvTakeCare = dt.Rows(0)("Email")
                _MatKhauEmailNvTakeCare = Utils.BaoMat.GiaiMaMaStr(dt.Rows(0)("MatKhauEmail"), key)
                _ChuKy = dt.Rows(0)("ChuKyEmail").ToString
                _TenCTy = dt.Rows(0)("TenCTy").ToString
            Else
                _TenNvTakeCare = Utils.Email.TenEmail
                _EmailNvTakeCare = Utils.Email.DiaChiEmail
                _MatKhauEmailNvTakeCare = Utils.Email.MatKhauEmail
                _ChuKy = Utils.Email.ChuKyEmail
                If dt.Rows.Count > 0 Then
                    _TenCTy = dt.Rows(0)("TenCTy").ToString
                End If
            End If
        End Sub

        Private _Id As Integer
        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property

        Private _Ten As String
        Public Property Ten() As String
            Get
                Return _Ten
            End Get
            Set(ByVal value As String)
                _Ten = value
            End Set
        End Property

        Private _XungHo As String
        Public Property XungHo() As String
            Get
                Return _XungHo
            End Get
            Set(ByVal value As String)
                _XungHo = value
            End Set
        End Property

        Private _TenCTy As String
        Public Property TenCTy() As String
            Get
                Return _TenCTy
            End Get
            Set(ByVal value As String)
                _TenCTy = value
            End Set
        End Property

        Private _Email As String
        Public Property Email() As String
            Get
                Return _Email
            End Get
            Set(ByVal value As String)
                _Email = value
            End Set
        End Property
        Private _IdChamSoc As Integer
        Public Property IdChamSoc() As Integer
            Get
                Return _IdChamSoc
            End Get
            Set(ByVal value As Integer)
                _IdChamSoc = value
            End Set
        End Property
        Private _TenNvTakeCare As String
        Public ReadOnly Property TenNvTakeCare() As String
            Get
                Return _TenNvTakeCare
            End Get
        End Property
        Private _EmailNvTakeCare As String
        Public ReadOnly Property EmailNvTakeCare() As String
            Get
                Return _EmailNvTakeCare
            End Get
        End Property
        Private _MatKhauEmailNvTakeCare As String
        Public ReadOnly Property MatKhauEmailNvTakeCare() As String
            Get
                Return _MatKhauEmailNvTakeCare
            End Get
        End Property

        Private _ChuKy As String
        Public Property ChuKy() As String
            Get
                Return _ChuKy
            End Get
            Set(ByVal value As String)
                _ChuKy = value
            End Set
        End Property
    End Class
#End Region

#Region " -- Tim khach hang -- "



    Private Sub btnCheck_Click(sender As System.Object, e As System.EventArgs) Handles btnCheck.Click
        Dim st As Boolean = Convert.ToBoolean(btnCheck.Tag)
        gdvData.CloseEditor()
        gdvData.BeginUpdate()

        If st = True Then
            st = False
            For i As Integer = 0 To gdvData.DataRowCount - 1
                gdvData.SetRowCellValue(i, "colChon", st)
            Next
        Else
            st = True
            For i As Integer = 0 To gdvData.DataRowCount - 1
                If Not IsDBNull(gdvData.GetRowCellValue(i, "DoiTuongNhanEmail")) Then
                    Select Case cbLoaiChuDe.EditValue
                        Case "Sản phẩm/Dịch vụ/Đào tạo/Hội thảo"

                            Select Case gdvData.GetRowCellValue(i, "DoiTuongNhanEmail")
                                Case 0, 1, 6
                                    gdvData.SetRowCellValue(i, "colChon", Not st)
                                Case 2, 3, 4, 5
                                    gdvData.SetRowCellValue(i, "colChon", st)
                            End Select
                        Case Else
                            Select Case gdvData.GetRowCellValue(i, "DoiTuongNhanEmail")
                                Case 0, 1, 2, 6
                                    gdvData.SetRowCellValue(i, "colChon", Not st)
                                Case 3, 4, 5
                                    gdvData.SetRowCellValue(i, "colChon", st)
                            End Select
                    End Select
                Else
                    gdvData.SetRowCellValue(i, "colChon", st)
                End If

            Next
        End If

        ' gdvData.


        gdvData.EndUpdate()
        btnCheck.Tag = st
    End Sub

    Private Sub gdvData_CellValueChanged(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gdvData.CellValueChanged
        If e.Column.FieldName = "colChon" Then
            With gdvData
                If e.Value = True Then
                    Dim email As DiaChiEmail = arrEmail.Find(Function(i As DiaChiEmail) i.Id = .GetRowCellValue(e.RowHandle, "Id"))
                    If Not email Is Nothing Then arrEmail.Remove(email)
                    arrEmail.Add(New DiaChiEmail(.GetRowCellValue(e.RowHandle, "Id"), .GetRowCellValue(e.RowHandle, "XungHo").ToString, .GetRowCellValue(e.RowHandle, "Ten"), .GetRowCellValue(e.RowHandle, "Email"), .GetRowCellValue(e.RowHandle, "ChamSoc")))
                Else
                    Dim email As DiaChiEmail = arrEmail.Find(Function(i As DiaChiEmail) i.Id = .GetRowCellValue(e.RowHandle, "Id"))
                    If Not email Is Nothing Then arrEmail.Remove(email)
                End If
            End With
            lblNguoiNhan.Text = String.Format("Người nhận: {0:N0} email.", arrEmail.Count)
        End If
    End Sub

    Private Sub gdvData_RowCellClick(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles gdvData.RowCellClick
        If e.Column.FieldName <> "ChamSoc" And e.Column.FieldName <> "DoiTuongNhanEmail" And e.RowHandle >= 0 Then
            Dim st As Boolean = Convert.ToBoolean(gdvData.GetRowCellValue(e.RowHandle, "colChon"))
            If st = True Then
                st = False
            Else
                If Not IsDBNull(gdvData.GetRowCellValue(e.RowHandle, "DoiTuongNhanEmail")) Then
                    Select Case cbLoaiChuDe.EditValue
                        Case "Sản phẩm/Dịch vụ/Đào tạo/Hội thảo"

                            Select Case gdvData.GetRowCellValue(e.RowHandle, "DoiTuongNhanEmail")
                                Case 0, 1, 6
                                    st = False
                                Case 2, 3, 4, 5
                                    st = True
                            End Select
                        Case Else
                            Select Case gdvData.GetRowCellValue(e.RowHandle, "DoiTuongNhanEmail")
                                Case 0, 1, 2, 6
                                    st = False
                                Case 3, 4, 5
                                    st = True
                            End Select
                    End Select
                Else
                    st = True
                End If

            End If


            'Dim st As Boolean = Convert.ToBoolean(gdvData.GetRowCellValue(e.RowHandle, "colChon"))
            gdvData.SetRowCellValue(e.RowHandle, "colChon", st)
            If e.RowHandle = 0 Or e.RowHandle = gdvData.DataRowCount - 1 Then Exit Sub
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim index As Integer = e.RowHandle - 1
                While gdvData.GetRowCellValue(index, "noictac") = gdvData.GetRowCellValue(e.RowHandle, "noictac")
                    gdvData.SetRowCellValue(index, "colChon", st)
                    index -= 1
                End While
                index = e.RowHandle + 1
                While gdvData.GetRowCellValue(index, "noictac") = gdvData.GetRowCellValue(e.RowHandle, "noictac")
                    gdvData.SetRowCellValue(index, "colChon", st)
                    index += 1
                End While
            End If
            gdvData.CloseEditor()
            gdvData.UpdateCurrentRow()

        End If
    End Sub

#End Region


    Private Sub rGroupTrangThai_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles rGroupTrangThai.SelectedIndexChanged
        Select Case rGroupTrangThai.SelectedIndex
            Case 0
                cmbPtrangThai.EditValue = "[Chờ duyệt]"
            Case 1
                cmbPtrangThai.EditValue = "[Đã duyệt]"
            Case 2
                cmbPtrangThai.EditValue = "[Không duyệt]"
        End Select
    End Sub

    Private Sub btnXacNhanTrangThai_Click(sender As System.Object, e As System.EventArgs) Handles btnXacNhanTrangThai.Click

        Dim t As DateTime = GetServerTime()
        Select Case rGroupTrangThai.SelectedIndex
            Case 0
                AddParameter("@TrangThai", DBNull.Value)
            Case 1
                AddParameter("@TrangThai", 1)
            Case 2
                AddParameter("@TrangThai", 0)
        End Select
        AddParameter("@NguoiDuyet", Convert.ToInt32(TaiKhoan))
        AddParameter("@NgayDuyet", t)

        AddParameterWhere("@Id", MaTuDien)
        If doUpdate("EMAILKD", "Id=@Id") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        Else

            isOk = True
            If rGroupTrangThai.SelectedIndex = 1 Then
                btnChuanBiGuiEmail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                btnChuanBiGuiEmail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If

            'Send email thong bao
            If chkGuiThongBao.Checked Then
                Try
                    Dim strEmail As String = ExecuteSQLDataTable("select email from nhansu where Id = " & _nguoiLapEmail).Rows(0)(0).ToString
                    Dim strEmailNguoiDuyet As String = ExecuteSQLDataTable("select email from nhansu where Id = " & TaiKhoan).Rows(0)(0).ToString
                    ShowWaiting("Đang gửi bản thông báo đến địa chỉ " & strEmail)
                    Dim MyMailMessage As New MailMessage()
                    MyMailMessage.From = New MailAddress(Utils.Email.DiaChiEmail)
                    Dim str As String = "Email: """ & txtTieuDe.Text & """ của bạn "
                    Select Case rGroupTrangThai.SelectedIndex
                        Case 0
                            str &= "đang chờ duyệt "
                        Case 1
                            str &= "đã được duyệt "
                        Case 2
                            str &= "không được duyệt "
                    End Select
                    str &= "bởi " & NguoiDung & " vào lúc " & t.ToString("HH:mm:ss dd/MM/yyyy") & ".<br/>"
                    str &= "<b>Ghi chú:</b><br/>"
                    str &= txtGuiThongBao.Text


                    MyMailMessage.To.Add(strEmail)
                    MyMailMessage.CC.Add(strEmailNguoiDuyet)
                    MyMailMessage.Subject = "Kiểm duyệt nội dung email marketing"
                    MyMailMessage.IsBodyHtml = True
                    MyMailMessage.Body = str.Replace(vbCrLf, "<br/>")

                    Dim SMTPServer As New SmtpClient("smtp.gmail.com")
                    SMTPServer.Port = 25
                    SMTPServer.Credentials = New System.Net.NetworkCredential(Utils.Email.DiaChiEmail, Utils.Email.MatKhauEmail)
                    SMTPServer.EnableSsl = True
                    SMTPServer.Send(MyMailMessage)
                    CloseWaiting()
                Catch ex As Exception
                    CloseWaiting()
                    ShowBaoLoi(ex.Message)
                End Try
            End If



            ShowAlert("Cập nhật trạng thái thành công !")
        End If
        SendKeys.Send("{F4}")
    End Sub

    Private Function CheckDuLieu() As Boolean
        If txtTieuDe.Text.Trim = "" Then
            ShowBaoLoi("Chưa nhập nội dung tiêu đề !")
            ActiveControl = txtTieuDe
            Return False
        End If
        If cmbDiaChiEmailGui.Text.Trim = "" Then
            ShowBaoLoi("Chưa chọn email người gửi !")
            Return False
        End If
        'If arrEmail.Count = 0 Then
        '    ShowBaoLoi("Chưa chọn người nhận !")
        '    Return False
        'End If
        Return True
    End Function

    Private Sub txtMaKH_ButtonClick(sender As System.Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles txtMaKH.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            txtMaKH.EditValue = DBNull.Value
        End If
    End Sub

    Private Sub txtMaKH_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles txtMaKH.EditValueChanged
        If Not txtMaKH.EditValue Is DBNull.Value AndAlso txtMaKH.EditValue.ToString.Trim <> "" Then
            gdvData.ActiveFilterString = "[KhachHang] Like '%" & txtMaKH.EditValue & "%'"
        Else
            gdvData.ActiveFilterString = ""
        End If
    End Sub

    Private Sub btnLocTheoTakeCare_Click(sender As System.Object, e As System.EventArgs) Handles btnLocTheoTakeCare.Click
        gdvData.ActiveFilterString = "[TakeCare] Like '%" & NguoiDung & "%'"
        'CType(gdv.MainView, DevExpress.XtraGrid.Views.Base.ColumnView).ActiveFilterString = "[Chăm sóc] Like '%" & NguoiDung & "%'"
    End Sub

    Private Sub rcbDoiTuongNhanEmail_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles rcbDoiTuongNhanEmail.EditValueChanged
        AddParameter("@DoiTuongNhanEmail", CType(sender, DevExpress.XtraEditors.LookUpEdit).EditValue)
        AddParameterWhere("@IDNV", gdvData.GetFocusedRowCellValue("ID"))
        If doUpdate("NHANSU", "ID=@IDNV") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
        End If
    End Sub

#Region " -- Bam nut luu du lieu -- "

    Private Sub btnLuu_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLuu.ItemClick

        If Not CheckDuLieu() Then Exit Sub

        Dim tg As DateTime = GetServerTime()
        Dim t As String = tg.ToString("ddMMyyyy_HHmmss")
        Dim arrFileTmp As New List(Of String)
        Try
            BeginTransaction()
            'Lay danh sach nguoi nhan
            Dim strNguoiNhan As String = ""
            For Each email As DiaChiEmail In arrEmail
                strNguoiNhan &= email.Id & ";"
            Next
            'Add File kem len server
            Dim strFileDinhKem As String = ""
            For Each f As ListViewItem In lstFiles.Items
                Dim obj = CType(f.Tag, FileDinhKem)
                If Not obj.IsServer Then
                    Dim strName As String = URL_FILE_MAYCHU & obj.GetFileNameWithoutExt & "_" & t & obj.GetFileExt
                    File.Copy(obj.Fullpath, strName)
                    strFileDinhKem &= obj.GetFileNameWithoutExt & "_" & t & obj.GetFileExt & ";"
                    obj.setStatusIsServer(True)
                    obj.Fullpath = strName
                    f.Text = obj.GetFileNameX
                    arrFileTmp.Add(strName)
                Else
                    strFileDinhKem &= obj.GetFileName & ";"
                End If
            Next
            'Add vao CSDL
            Select Case cbLoaiChuDe.EditValue
                Case "Sản phẩm/Dịch vụ/Đào tạo/Hội thảo"
                    AddParameter("@LoaiChuDe", 0)
                Case "Giá bán/Khuyến mãi"
                    AddParameter("@LoaiChuDe", 1)
            End Select
            AddParameter("@TieuDe", txtTieuDe.Text)

            AddParameter("@CachGui", cmbEmaiGui.SelectedIndex)
            AddParameter("@EmailGui", cmbDiaChiEmailGui.Text)

            AddParameter("@NoiDung", txtNoiDung.Document.InvokeScript("LayGiaTri"))


            If TrangThai.isAddNew Or TrangThai.isCopy Then
                AddParameter("@NgayGui", tg)
                AddParameter("@NguoiGui", Convert.ToInt32(TaiKhoan))
                _nguoiLapEmail = TaiKhoan
            End If


            AddParameter("@Files", strFileDinhKem)
            AddParameter("@NguoiNhan", strNguoiNhan)
            AddParameter("@GhiChu", tbGhiChu.EditValue)
            If TrangThai.isAddNew Or TrangThai.isCopy Then
                AddParameter("@TrangThaiGui", False)
                MaTuDien = doInsert("EMAILKD")
                If MaTuDien Is Nothing Then Throw New Exception(LoiNgoaiLe)
                _IdEmail = MaTuDien
            ElseIf TrangThai.isUpdate Then
                AddParameterWhere("@Id", MaTuDien)
                If doUpdate("EMAILKD", "Id=@Id") Is Nothing Then Throw New Exception(LoiNgoaiLe)
            End If

            ShowAlert("Cập nhật nội dung vào CSDL thành công !")
            lblThongBao.Visible = True
            lblThongBao.Text = "Đã lưu thành công lúc " & Now.ToString("HH:mm:ss") & " !"
            TrangThai.isUpdate = True
            isOk = True
            ComitTransaction()

        Catch ex As Exception
            lblThongBao.Text = ""
            lblThongBao.Visible = False
            RollBackTransaction()
            For Each Str As String In arrFileTmp 'Xoa file vua day len server
                System.IO.File.Delete(Str)
            Next
            ShowBaoLoi(ex.Message)
        End Try

    End Sub
#End Region

#Region " -- Quan ly, Them, tai, xoa file dinh kem -- "

    Private Sub btnChonFile_Click(sender As System.Object, e As System.EventArgs) Handles btnChonFile.Click, mnuThemFile.ItemClick
        Dim openDlg As New OpenFileDialog
        openDlg.Multiselect = True
        If openDlg.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
        If openDlg.FileNames.Length = 0 Then Exit Sub
        For Each p As String In openDlg.FileNames
            Dim item As New ListViewItem
            Dim objFile As New FileDinhKem(p)
            item.Tag = objFile
            item.Text = objFile.GetFileName
            imgList.Images.Add(objFile.GetFileName, objFile.GetIcon)
            item.ImageKey = objFile.GetFileName
            lstFiles.Items.Add(item)
        Next
        lblFileKem.Text = lstFiles.Items.Count & " files kèm"
    End Sub

    Private Sub lstFiles_MouseDown(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstFiles.MouseDown
        Dim hit As ListViewHitTestInfo = lstFiles.HitTest(e.Location)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If hit.Item Is Nothing Then
                mnuXoaFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuTaiFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                mnuChenAnh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Else
                hit.Item.Selected = True
                mnuXoaFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuTaiFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuChenAnh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                mnuChenAnh.Enabled = False
                If CType(hit.Item.Tag, FileDinhKem).IsServer Then
                    Select Case CType(hit.Item.Tag, FileDinhKem).GetFileExt.ToLower
                        Case ".jpg", ".jpeg", ".bmp", ".png", ".gif"
                            mnuChenAnh.Enabled = True
                    End Select
                End If
            End If
            pMnuFile.ShowPopup(lstFiles.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub mnuXoaFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuXoaFile.ItemClick
        If lstFiles.SelectedItems.Count = 0 Then Exit Sub
        Dim obj As FileDinhKem = CType(lstFiles.SelectedItems(0).Tag, FileDinhKem)
        If Not obj.IsServer Then
            imgList.Images.RemoveByKey(obj.GetFileName)
            lstFiles.Items.Remove(lstFiles.SelectedItems(0))
        Else
            If Not ShowCauHoi("File này đã được lưu trên server, bạn có chắc muốn xóa không ?") Then Exit Sub
            Try
                File.Delete(obj.Fullpath)
                lstFiles.Items.Remove(lstFiles.SelectedItems(0))
                btnLuu_ItemClick(sender, e)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
        lblFileKem.Text = lstFiles.Items.Count & " files kèm"
    End Sub

    Private Sub mnuTaiFile_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTaiFile.ItemClick
        ShowWaiting("Đang tải dữ liệu ...")
        If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
        End If
        Dim f As FileDinhKem = CType(lstFiles.SelectedItems(0).Tag, FileDinhKem)
        If Not File.Exists(f.Fullpath) Then
            ShowBaoLoi("File này không tồn tại trên máy chủ !")
        Else
            Try
                System.IO.File.Copy(f.Fullpath, Application.StartupPath & "\tmp\" & f.GetFileName, True)
                Process.Start(Application.StartupPath & "\tmp\" & f.GetFileName)
            Catch ex As Exception
                ShowBaoLoi(ex.Message)
            End Try
        End If
        CloseWaiting()
    End Sub




#End Region



    'Private isMailHeThong As Boolean = False
    'Private isMailMarketing As Boolean = False


    'Private Sub mnuGuiEmailHeThong_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuiEmailHeThong.ItemClick
    '    If Not CheckDuLieu() Then Exit Sub
    '    TaoDataDsEmailGui(True)
    '    isMailHeThong = True
    '    isMailMarketing = False
    '    tabMain.SelectedTabPageIndex = 1
    'End Sub

    'Private Sub mnuGuiEmailNhanVien_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuiEmailNhanVien.ItemClick
    '    If Not CheckDuLieu() Then Exit Sub
    '    TaoDataDsEmailGui(False)
    '    isMailHeThong = False
    '    isMailMarketing = False
    '    tabMain.SelectedTabPageIndex = 1
    'End Sub

    'Private Sub mnuGuiEmailMarketing_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuGuiEmailMarketing.ItemClick
    '    If Not CheckDuLieu() Then Exit Sub
    '    TaoDataDsEmailGui(False, True)
    '    isMailHeThong = False
    '    isMailMarketing = True
    '    tabMain.SelectedTabPageIndex = 1
    'End Sub

    Private Sub TaoDataDsEmailGui()
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("ID", Type.GetType("System.Int32")))
        dt.Columns.Add(New DataColumn("XungHo", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("TenNguoiNhan", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("EmailNguoiNhan", Type.GetType("System.String")))
        dt.Columns.Add(New DataColumn("DaGui", Type.GetType("System.Boolean")))
        dt.Columns.Add(New DataColumn("TrangThai", Type.GetType("System.String")))
        Dim lstDaGui() = _strDaGui.Split(";")
        For Each email As DiaChiEmail In arrEmail
            Dim row As DataRow = dt.NewRow
            row("ID") = email.Id
            row("XungHo") = email.XungHo
            row("TenNguoiNhan") = email.Ten
            row("EmailNguoiNhan") = email.Email
            If Array.IndexOf(lstDaGui, email.Id.ToString) >= 0 Then
                row("DaGui") = True
            Else
                row("DaGui") = False
            End If
            dt.Rows.InsertAt(row, dt.Rows.Count)
        Next
        gdvDataEmail.OptionsBehavior.AutoExpandAllGroups = False
        gdvEmail.DataSource = dt

        Dim tg = GetServerTime().ToString("dd/MM/yyyy")

        AddParameter("@EmailNguoiGui", cmbDiaChiEmailGui.Text)
        AddParameter("@ThoiGian", tg)
        _SoEmailDaGui = ExecuteSQLDataTable("SET DATEFORMAT DMY; SELECT count(Id) FROM LOG_EMAILKD WHERE EmailNguoiGui = @EmailNguoiGui AND CONVERT(nvarchar,ThoiGian,103) = @ThoiGian").Rows(0)(0)


        Select Case cmbEmaiGui.SelectedIndex
            Case 0 'email nhan vien
                Dim f As New DevExpress.XtraEditors.XtraForm
                f.Text = "Nhập mật khẩu email: " & cmbDiaChiEmailGui.Text
                f.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
                f.StartPosition = FormStartPosition.CenterScreen
                f.Width = 400
                f.Height = 56
                f.MaximizeBox = False
                f.MinimizeBox = False
                Dim txtPassword As New DevExpress.XtraEditors.TextEdit
                txtPassword.Dock = DockStyle.Fill
                txtPassword.Properties.PasswordChar = "*"
                txtPassword.Font = New Font(Me.Font.FontFamily, 10, FontStyle.Bold)
                txtPassword.ForeColor = Color.Red
                AddHandler txtPassword.KeyDown, AddressOf LayMatKhauEmail1
                AddHandler txtPassword.MouseDown, AddressOf LayMatKhauEmail2
                AddHandler txtPassword.MouseUp, AddressOf LayMatKhauEmail3
                f.Controls.Add(txtPassword)
                f.ShowDialog()
            Case 1 'email marketing
                Try
                    AddParameter("@Email", cmbDiaChiEmailGui.Text)
                    _matKhauEmailCaNhan = ExecuteSQLDataTable("select MatKhau from DM_Email WHERE Email = @Email").Rows(0)(0).ToString
                Catch ex As Exception
                    _matKhauEmailCaNhan = ""
                End Try
            Case 2 'email he thong
                _matKhauEmailCaNhan = Utils.Email.MatKhauEmail
        End Select

    End Sub

    Private Sub LayMatKhauEmail1(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            _matKhauEmailCaNhan = CType(sender, DevExpress.XtraEditors.TextEdit).EditValue
            CType(CType(sender, Control).Parent, System.Windows.Forms.Form).Close()
        End If
    End Sub
    Private Sub LayMatKhauEmail2(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        CType(sender, DevExpress.XtraEditors.TextEdit).Properties.PasswordChar = ""
    End Sub
    Private Sub LayMatKhauEmail3(sender As Object, e As System.Windows.Forms.MouseEventArgs)
        CType(sender, DevExpress.XtraEditors.TextEdit).Properties.PasswordChar = "*"
    End Sub

    Private Sub btnQuayLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnQuayLai.ItemClick
        tabMain.SelectedTabPageIndex = 0
    End Sub

    Private strHTML As String = ""

    Private Sub btnBatDauGuiEmail_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnBatDauGuiEmail.ItemClick

        gdvDataEmail.OptionsView.ShowAutoFilterRow = False

        If (500 - _SoEmailDaGui) = 0 Then
            ShowCanhBao("Email " & cmbDiaChiEmailGui.Text & " đã gửi hết 500 mail trong ngày nên không thể tiếp tục gửi! ")
            Exit Sub
        End If

        If Not ShowCauHoi("Email " & cmbDiaChiEmailGui.Text & " có thể gửi " & 500 - _SoEmailDaGui & " trong ngày, bạn có muốn gửi email không?") Then
            Exit Sub
        End If

        strHTML = txtNoiDung.Document.InvokeScript("LayGiaTri")

        'gdvEmail.Enabled = False
        btnQuayLai.Enabled = False
        btnBatDauGuiEmail.Enabled = False
        barTienTrinh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        lblTrangThai.Caption = ""
        lblTrangThai.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        btDungLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        bgW.RunWorkerAsync()

    End Sub

    Private strLog As String = ""
    Private _isDaGuiHet As Boolean = False
    Private _isDaGuiThanhCong As Boolean = False

    Private Sub bgW_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgW.DoWork

        CheckForIllegalCrossThreadCalls = False
        AddParameter("@TrangThaiGui", 1)
        AddParameterWhere("@Id", _IdEmail)
        If doUpdate("EMAILKD", "Id=@Id") Is Nothing Then
            ShowBaoLoi(LoiNgoaiLe)
            Exit Sub
        End If

        Dim _SoEmailConDuocGui As Integer = 500 - _SoEmailDaGui
        Dim MyMailMessage As New MailMessage
        MyMailMessage.From = New MailAddress(cmbDiaChiEmailGui.Text)

        _strDaGui = ""
        '''''''''''
        'Gui BCC'''
        '''''''''''
        If cbCachGui.EditValue = "Gửi BCC" Then 'Add danh sach BCC
            Try
                lblTrangThai.Caption = "Đang gửi email hình thức BCC ..."
                For i As Integer = 0 To gdvDataEmail.RowCount - 1
                    If isNguoiSoanEmail Then
                        If CType(gdvDataEmail.GetRowCellValue(i, "DaGui"), Boolean) Then Continue For
                    End If
                    MyMailMessage.Bcc.Add(New MailAddress(gdvDataEmail.GetRowCellValue(i, "EmailNguoiNhan")))
                Next
                MyMailMessage.Subject = txtTieuDe.Text
                MyMailMessage.IsBodyHtml = True
                MyMailMessage.Body = strHTML
                If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
                End If
                For k As Integer = 0 To lstFiles.Items.Count - 1
                    Dim obj As FileDinhKem = CType(lstFiles.Items(k).Tag, FileDinhKem)
                    Try
                        System.IO.File.Copy(obj.Fullpath, Application.StartupPath & "\tmp\" & obj.GetFileNameX, True)
                    Catch ex As Exception
                    End Try
                    Dim att As New Attachment((Application.StartupPath & "\tmp\" & obj.GetFileNameX))
                    att.ContentId = obj.GetFileNameX
                    Select Case obj.GetFileExt.ToLower
                        Case ".jpg", ".jpeg", ".bmp", ".png", ".gif"
                            att.ContentType.MediaType = "image/jpeg"
                            att.ContentType.Name = obj.GetFileNameX
                            att.ContentDisposition.Inline = True
                            att.ContentDisposition.FileName = obj.GetFileNameX
                    End Select
                    MyMailMessage.Attachments.Add(att)
                Next
                Dim SMTPServer As SmtpClient

                If cmbDiaChiEmailGui.Text.IndexOf("@baoanjsc.com.vn") >= 0 Then
                    SMTPServer = New SmtpClient("mail.baoanjsc.com.vn")
                    SMTPServer.EnableSsl = False
                    SMTPServer.UseDefaultCredentials = False
                    SMTPServer.DeliveryMethod = SmtpDeliveryMethod.Network
                Else
                    SMTPServer = New SmtpClient("smtp.gmail.com")
                    SMTPServer.EnableSsl = True
                End If

                SMTPServer.Port = 25
                SMTPServer.Credentials = New System.Net.NetworkCredential(cmbDiaChiEmailGui.Text, _matKhauEmailCaNhan)
                SMTPServer.EnableSsl = True
                SMTPServer.Send(MyMailMessage)
                AddParameter("@IdEmail", _IdEmail)
                AddParameter("@TenNguoiNhan", "BCC")
                AddParameter("@EmailNguoiNhan", "BCC")
                AddParameter("@EmailNguoiGui", cmbDiaChiEmailGui.Text)
                AddParameter("@ThoiGian", DateTime.Now)
                AddParameter("@TrangThai", 1)
                AddParameter("@GhiChu", "Gửi thành công!")
                AddParameter("@IdNguoiGui", TaiKhoan)
                doInsert("LOG_EMAILKD")
            Catch ex As Exception
                AddParameter("@IdEmail", _IdEmail)
                AddParameter("@TenNguoiNhan", "BCC")
                AddParameter("@EmailNguoiNhan", "BCC")
                AddParameter("@EmailNguoiGui", cmbDiaChiEmailGui.Text)
                AddParameter("@ThoiGian", DateTime.Now)
                AddParameter("@TrangThai", 0)
                AddParameter("@GhiChu", "Lỗi: " & ex.Message)
                AddParameter("@IdNguoiGui", TaiKhoan)
                doInsert("LOG_EMAILKD")
            End Try
            _isDaGuiHet = True
            _SoEmailConDuocGui -= 1
        Else
            '''''''''''
            'Gui TO''''
            '''''''''''
            CheckForIllegalCrossThreadCalls = False
            For i As Integer = 0 To gdvDataEmail.RowCount - 1
                CheckForIllegalCrossThreadCalls = False
                If bgW.WorkerSupportsCancellation Then Exit For
                If _SoEmailConDuocGui = 0 Then
                    ShowCanhBao("Đã gửi tới giới hạn mail gửi trong 1 ngày ")
                    Exit For
                End If
                Try
                    Application.DoEvents()
                    gdvDataEmail.FocusedRowHandle = i
                    If i = gdvDataEmail.RowCount - 1 Then _isDaGuiHet = True
                    If isNguoiSoanEmail Then
                        If CType(gdvDataEmail.GetRowCellValue(i, "DaGui"), Boolean) Then Continue For
                    End If
                    lblTrangThai.Caption = "Đang gửi email " & gdvDataEmail.GetRowCellValue(i, "EmailNguoiNhan") & " ..."
                    MyMailMessage = New MailMessage
                    MyMailMessage.From = New MailAddress(cmbDiaChiEmailGui.Text)
                    MyMailMessage.To.Add(New MailAddress(gdvDataEmail.GetRowCellValue(i, "EmailNguoiNhan")))
                    MyMailMessage.Subject = txtTieuDe.Text
                    MyMailMessage.IsBodyHtml = True
                    MyMailMessage.Body = strHTML.Replace("[#{TEN_KH}#]", gdvDataEmail.GetRowCellValue(i, "TenNguoiNhan"))
                    MyMailMessage.Body = MyMailMessage.Body.Replace("[#{XUNG_HO}#]", gdvDataEmail.GetRowCellValue(i, "XungHo"))

                    If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
                        System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
                    End If
                    For k As Integer = 0 To lstFiles.Items.Count - 1
                        Dim obj As FileDinhKem = CType(lstFiles.Items(k).Tag, FileDinhKem)
                        Try
                            System.IO.File.Copy(obj.Fullpath, Application.StartupPath & "\tmp\" & obj.GetFileNameX, True)
                        Catch ex As Exception
                        End Try
                        Dim att As New Attachment((Application.StartupPath & "\tmp\" & obj.GetFileNameX))
                        att.ContentId = obj.GetFileNameX
                        Select Case obj.GetFileExt.ToLower
                            Case ".jpg", ".jpeg", ".bmp", ".png", ".gif"
                                att.ContentType.MediaType = "image/jpeg"
                                att.ContentType.Name = obj.GetFileNameX
                                att.ContentDisposition.Inline = True
                                att.ContentDisposition.FileName = obj.GetFileNameX
                        End Select
                        MyMailMessage.Attachments.Add(att)
                    Next

                    Dim SMTPServer As SmtpClient

                    If cmbDiaChiEmailGui.Text.IndexOf("@baoanjsc.com.vn") >= 0 Then
                        SMTPServer = New SmtpClient("mail.baoanjsc.com.vn")
                        SMTPServer.EnableSsl = False
                        SMTPServer.UseDefaultCredentials = False
                        SMTPServer.DeliveryMethod = SmtpDeliveryMethod.Network
                    Else
                        SMTPServer = New SmtpClient("smtp.gmail.com")
                        SMTPServer.EnableSsl = True
                    End If

                    SMTPServer.Port = 25
                    SMTPServer.Credentials = New System.Net.NetworkCredential(cmbDiaChiEmailGui.Text, _matKhauEmailCaNhan)
                    'ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf customCertValidation)


                    SMTPServer.Send(MyMailMessage)
                    AddParameter("@IdEmail", _IdEmail)
                    AddParameter("@TenNguoiNhan", gdvDataEmail.GetRowCellValue(i, "TenNguoiNhan"))
                    AddParameter("@EmailNguoiNhan", gdvDataEmail.GetRowCellValue(i, "EmailNguoiNhan"))
                    AddParameter("@EmailNguoiGui", cmbDiaChiEmailGui.Text)
                    AddParameter("@ThoiGian", DateTime.Now)
                    AddParameter("@TrangThai", 1)
                    AddParameter("@GhiChu", "Gửi thành công!")
                    AddParameter("@IdNguoiGui", TaiKhoan)
                    doInsert("LOG_EMAILKD")
                    _strDaGui &= gdvDataEmail.GetRowCellValue(i, "ID") & ";"

                    _indexGuiEmail = i
                    _isDaGuiThanhCong = True
                    _strThongBao = "Gửi thành công - " & Now.ToString("HH:mm:ss") & "!"
                    bgW.ReportProgress(100)
                Catch ex As Exception
                    AddParameter("@IdEmail", _IdEmail)
                    AddParameter("@TenNguoiNhan", gdvDataEmail.GetRowCellValue(i, "TenNguoiNhan"))
                    AddParameter("@EmailNguoiNhan", gdvDataEmail.GetRowCellValue(i, "EmailNguoiNhan"))
                    AddParameter("@EmailNguoiGui", cmbDiaChiEmailGui.Text)
                    AddParameter("@ThoiGian", DateTime.Now)
                    AddParameter("@TrangThai", 0)
                    AddParameter("@GhiChu", "Lỗi: " & ex.Message)
                    AddParameter("@IdNguoiGui", TaiKhoan)
                    doInsert("LOG_EMAILKD")
                    _indexGuiEmail = i
                    _isDaGuiThanhCong = False
                    _strThongBao = "Lỗi - " & ex.Message & " - " & Now.ToString("HH:mm:ss") & "!"
                    bgW.ReportProgress(100)
                End Try
                _SoEmailConDuocGui -= 1
            Next
        End If


     
    End Sub

    Private Shared Function customCertValidation(ByVal sender As Object, _
                                       ByVal cert As X509Certificate, _
                                       ByVal chain As X509Chain, _
                                       ByVal errors As SslPolicyErrors) As Boolean

        Return True

    End Function

    Private _indexGuiEmail As Integer = 0
    Private _strThongBao As String = ""

    Private Sub bgW_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgW.RunWorkerCompleted
        CheckForIllegalCrossThreadCalls = False
        tabMain.SelectedTabPageIndex = 0
        gdvEmail.Enabled = True
        btnQuayLai.Enabled = True
        btnBatDauGuiEmail.Enabled = True
        barTienTrinh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        lblTrangThai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        btDungLai.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Dim str As String = ""
        If _isDaGuiHet Then
            str = "Tiến trình gửi email đã hoàn thành, bạn có muốn xem log không ?"
            If isNguoiSoanEmail Then
                AddParameter("@DaGui", "")
                AddParameter("@TrangThaiGui", 0)
                AddParameterWhere("@Id", _IdEmail)
                doUpdate("EMAILKD", "Id=@Id")
            End If
        Else
            str = "Tiến trình gửi email đã bị dừng lại, bạn có muốn xem log không ?"
            If isNguoiSoanEmail Then
                AddParameter("@DaGui", _strDaGui)
                AddParameter("@TrangThaiGui", 1)
                AddParameterWhere("@Id", _IdEmail)
                doUpdate("EMAILKD", "Id=@Id")
            End If
        End If

        If ShowCauHoi(str) Then
            Dim f As New frmLogEmailKD
            f._IdEmail = _IdEmail
            f.ShowDialog()
        End If
    End Sub

    Private _isDangXuLy As Boolean = False
    Private Sub bgW_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgW.ProgressChanged
        If _isDangXuLy Then Exit Sub
        _isDangXuLy = True
        CheckForIllegalCrossThreadCalls = False
        gdvDataEmail.SetRowCellValue(_indexGuiEmail, "TrangThai", _strThongBao)
        If _isDaGuiThanhCong Then
            gdvDataEmail.SetRowCellValue(_indexGuiEmail, "DaGui", True)
        Else
            gdvDataEmail.SetRowCellValue(_indexGuiEmail, "DaGui", DBNull.Value)
        End If
        _isDangXuLy = False
    End Sub

    Private Sub btDungLai_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btDungLai.ItemClick
        bgW.WorkerSupportsCancellation = True
    End Sub


    Private Sub cmbEmaiGui_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbEmaiGui.SelectedIndexChanged
        cmbDiaChiEmailGui.Properties.Items.Clear()
        cmbDiaChiEmailGui.SelectedIndex = -1
        Try
            Select Case cmbEmaiGui.SelectedIndex
                Case 0 'Email ca nhan
                    cmbDiaChiEmailGui.Properties.Items.Add(ExecuteSQLDataTable("select email from nhansu where Id = " & TaiKhoan).Rows(0)(0).ToString)
                    cmbDiaChiEmailGui.SelectedIndex = 0
                Case 1
                    Dim dt As DataTable = ExecuteSQLDataTable("select Email from DM_EMAIL order by Email")
                    For i As Integer = 0 To dt.Rows.Count - 1
                        cmbDiaChiEmailGui.Properties.Items.Add(dt.Rows(i)(0).ToString)
                    Next
                    If cmbDiaChiEmailGui.Properties.Items.Count > 0 Then cmbDiaChiEmailGui.SelectedIndex = 0
                Case 2 'Email he thong
                    cmbDiaChiEmailGui.Properties.Items.Add(Utils.Email.DiaChiEmail)
                    cmbDiaChiEmailGui.SelectedIndex = 0
            End Select
        Catch ex As Exception
        End Try
    End Sub



    Private Enum CachGuiEmail
        EmailNhanVien = 0
        EmailMarketTing = 1
        EmailHeThong = 2
    End Enum


    Private Sub btnGuiNoiDung_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnGuiNoiDung.ItemClick


        Try
            Dim strEmail As String = ExecuteSQLDataTable("select email from nhansu where Id = " & TaiKhoan).Rows(0)(0).ToString
            If Not ShowCauHoi("Bạn có muốn gửi 1 bản test đến địa chỉ " & strEmail & " không?") Then Exit Sub

            ShowWaiting("Đang gửi bản test email marketing đến địa chỉ " & strEmail)

            Dim MyMailMessage As New MailMessage()

            MyMailMessage.From = New MailAddress(Utils.Email.DiaChiEmail)
            MyMailMessage.To.Add(strEmail)
            MyMailMessage.Subject = "[TEST] - " & txtTieuDe.Text
            MyMailMessage.IsBodyHtml = True
            MyMailMessage.Body = txtNoiDung.Document.InvokeScript("LayGiaTri")


            If Not System.IO.Directory.Exists(Application.StartupPath & "\tmp") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\tmp")
            End If
            For k As Integer = 0 To lstFiles.Items.Count - 1
                Dim obj As FileDinhKem = CType(lstFiles.Items(k).Tag, FileDinhKem)
                Try
                    System.IO.File.Copy(obj.Fullpath, Application.StartupPath & "\tmp\" & obj.GetFileNameX, True)
                Catch ex As Exception
                End Try
                Dim att As New Attachment((Application.StartupPath & "\tmp\" & obj.GetFileNameX))
                att.ContentId = obj.GetFileNameX
                Select Case obj.GetFileExt.ToLower
                    Case ".jpg", ".jpeg", ".bmp", ".png", ".gif"
                        att.ContentType.MediaType = "image/jpeg"
                        att.ContentType.Name = obj.GetFileNameX
                        att.ContentDisposition.Inline = True
                        att.ContentDisposition.FileName = obj.GetFileNameX
                End Select
                MyMailMessage.Attachments.Add(att)
            Next
            Dim SMTPServer As New SmtpClient("smtp.gmail.com")
            SMTPServer.Port = 25
            SMTPServer.Credentials = New System.Net.NetworkCredential(Utils.Email.DiaChiEmail, Utils.Email.MatKhauEmail)
            SMTPServer.EnableSsl = True
            SMTPServer.Send(MyMailMessage)
            CloseWaiting()
            ShowThongBao("Đã gửi bản test email marketing đến địa chỉ " & strEmail & " thành công!")
        Catch ex As Exception
            CloseWaiting()
            ShowBaoLoi(ex.Message)
        End Try


    End Sub

    Private Sub btnChuanBiGuiEmail_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnChuanBiGuiEmail.ItemClick
        If Not CheckDuLieu() Then Exit Sub
        TaoDataDsEmailGui()
        If _matKhauEmailCaNhan.Trim = "" Then
            ShowCanhBao("Chưa có mật khẩu email gửi đi !")
            Exit Sub
        End If
        gdvDataEmail.OptionsView.ShowAutoFilterRow = True
        lblThongBao.Visible = False
        lblDiaChiEmailGui.Caption = cmbDiaChiEmailGui.Text
        tabMain.SelectedTabPageIndex = 1
    End Sub

    Private Sub cmbLocTrangThaiEmail_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbLocTrangThaiEmail.SelectedIndexChanged
        Select Case cmbLocTrangThaiEmail.SelectedIndex
            Case 0
                gdvData.ActiveFilterString = ""
            Case 1
                gdvData.ActiveFilterString = "[colChon] = true"
            Case 2
                gdvData.ActiveFilterString = "[colChon] = false"
        End Select
    End Sub

    Private Sub btnXoaLoc_Click(sender As System.Object, e As System.EventArgs) Handles btnXoaLoc.Click
        gdvData.ActiveFilterString = ""
    End Sub



  


End Class