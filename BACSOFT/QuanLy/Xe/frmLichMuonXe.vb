Imports BACSOFT.Db.SqlHelper
Imports BACSOFT.Db.ThamSo
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraScheduler
Imports System.Globalization
Imports System.Threading
Imports DevExpress.XtraScheduler.Localization

Partial Public Class frmLichMuonXe
    Inherits DevExpress.XtraEditors.XtraForm


    Public Sub New()
        DevExpress.XtraEditors.Controls.Localizer.Active = New EditorLocalizer()
        InitializeComponent()
    End Sub

    Private DXSchedulerDataset As DataSet
    Private AppointmentDataAdapter As SqlDataAdapter
    Private ResourceDataAdapter As SqlDataAdapter
    Private DXSchedulerConn As SqlConnection

    Private Sub loadData()
        Dim Culture = CultureInfo.CreateSpecificCulture("vi-VN")

        ' The following line provides localization for the application's user interface. 
        Thread.CurrentThread.CurrentUICulture = Culture

        ' The following line provides localization for data formats. 
        Thread.CurrentThread.CurrentCulture = Culture
        Thread.CurrentThread.CurrentUICulture = Culture
        SchedulerStorage1.Appointments.ResourceSharing = True
        SchedulerControl1.GroupType = SchedulerGroupType.Resource
        'SchedulerControl1.Start = Today

        DXSchedulerDataset = New DataSet()
        Dim selectAppointments As String = "select distinct sudungxe.id as id, id_xe, id_nguoisudung, id_mucdo, id_trangthaixe, ten+N' đi '+hanhtrinh  as hanhtrinh,id_mucdich, ResourceID,ResourceIDs, Allday, ngaydi= case when ngaydi is null then ngaydidukien else ngaydi end, ngayve= case when ngayve is null then ngayvedukien else ngayve end from sudungxe inner join nhansu on nhansu.id=id_nguoisudung where id_trangthaixe !=4"
        Dim selectResources As String = "SELECT * FROM xe"

        DXSchedulerConn = New SqlConnection(getSQLConnectionString())
        DXSchedulerConn.Open()

        AppointmentDataAdapter = New SqlDataAdapter(selectAppointments, DXSchedulerConn)
        ' Subscribe to RowUpdated event to retrieve identity value for an inserted row.
        AddHandler AppointmentDataAdapter.RowUpdated, AddressOf AppointmentDataAdapter_RowUpdated
        AppointmentDataAdapter.Fill(DXSchedulerDataset, "Appointments")

        ResourceDataAdapter = New SqlDataAdapter(selectResources, DXSchedulerConn)
        ResourceDataAdapter.Fill(DXSchedulerDataset, "Resources")
        ' Specify mappings.
        MapAppointmentData()
        MapResourceData()

        ' Generate commands using CommandBuilder.  
        Dim cmdBuilder As New SqlCommandBuilder(AppointmentDataAdapter)
        AppointmentDataAdapter.InsertCommand = cmdBuilder.GetInsertCommand()
        AppointmentDataAdapter.DeleteCommand = cmdBuilder.GetDeleteCommand()
        AppointmentDataAdapter.UpdateCommand = cmdBuilder.GetUpdateCommand()
        SchedulerStorage1.Appointments.DataSource = DXSchedulerDataset
        SchedulerStorage1.Appointments.DataMember = "Appointments"
        SchedulerStorage1.Resources.DataSource = DXSchedulerDataset
        SchedulerStorage1.Resources.DataMember = "Resources"

        DXSchedulerConn.Close()
    End Sub
    Private Sub loadLabelnStatus()
        Dim dt As DataTable = ExecuteSQLDataSet("select * from mucdo").Tables(0)
        Dim testNum As Integer = 12
        SchedulerStorage1.Appointments.Labels.Clear()
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim labelId As Object = dt.Rows(i).Item(0)
            Dim labelDisplayName As String = dt.Rows(i).Item(1).ToString()
            Dim r As Integer = If((i + 50) * 20 <= 225, (i + 50) * 20, (i + 50) * 20 Mod 225)
            Dim g As Integer = If((i + 10) * 30 <= 225, (i + 10) * 30, (i + 10) * 30 Mod 225)
            Dim b As Integer = If((i + 5) * 40 <= 225, (i + 5) * 40, (i + 5) * 40 Mod 225)
            Dim labelColor As Color = Color.FromArgb(127 * i \ (testNum - 1) + 128, 225 \ (i + 1), 127 * i \ (testNum - 1) + 128)
            'MessageBox.Show(labelDisplayName + "-" + labelColor.ToString())
            'MessageBox.Show(labelDisplayName + ": " + r.ToString() + "," + g.ToString() + "," + b.ToString())
            SchedulerStorage1.Appointments.Labels.Add(labelColor, labelDisplayName)
        Next i
        SchedulerStorage1.Appointments.Statuses.Clear()
        dt = ExecuteSQLDataSet("select * from nhansu").Tables(0)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim labelId As Object = dt.Rows(i).Item(0)
            Dim labelDisplayName As String = dt.Rows(i).Item(1).ToString()
            Dim labelColor As Color = Color.FromKnownColor(KnownColor.Blue)
            'MessageBox.Show(labelDisplayName + "-" + labelColor.ToString())
            'MessageBox.Show(labelDisplayName + ": " + r.ToString() + "," + g.ToString() + "," + b.ToString())
            SchedulerStorage1.Appointments.Statuses.Add(labelColor, labelDisplayName, labelDisplayName)
        Next i
    End Sub


    Private Sub frmLichdungxe_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        loadData()
        SchedulerControl1.Start = Today
    End Sub
    Private Sub MapAppointmentData()
        'SchedulerStorage1.Appointments.Mappings.AllDay = "Allday"
        'SchedulerStorage1.Appointments.Mappings.Description = "ten"
        ' Required mapping.
        SchedulerStorage1.Appointments.Mappings.End = "ngayve"
        SchedulerStorage1.Appointments.Mappings.Label = "id_mucdo"
        SchedulerStorage1.Appointments.Mappings.Location = "hanhtrinh"
        ' SchedulerStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo"
        'SchedulerStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo"
        ' Required mapping.
        SchedulerStorage1.Appointments.Mappings.Start = "ngaydi"
        SchedulerStorage1.Appointments.Mappings.Status = "id_trangthaixe"
        'SchedulerStorage1.Appointments.Mappings.Subject = "ghichu_muon"
        'SchedulerStorage1.Appointments.Mappings.Type = "ghichu_muon"
        SchedulerStorage1.Appointments.Mappings.ResourceId = "ResourceIDs"
        SchedulerStorage1.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("id", "id"))
        ' SchedulerStorage1.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("donghokm_truoc", "donghokm_truoc"))
        ' SchedulerStorage1.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("donghokm_sau", "donghokm_sau"))
        'SchedulerStorage1.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("sokmdachay", "sokmdachay"))
        SchedulerStorage1.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("id_xe", "id_xe"))
        SchedulerStorage1.Appointments.CustomFieldMappings.Add(New AppointmentCustomFieldMapping("id_nguoisudung", "id_nguoisudung"))
    End Sub
    Private Sub MapResourceData()
        SchedulerStorage1.Resources.Mappings.Id = "id"
        SchedulerStorage1.Resources.Mappings.Caption = "tenxe"
    End Sub
    Private Sub AppointmentDataAdapter_RowUpdated(ByVal sender As Object, ByVal e As SqlRowUpdatedEventArgs)
        If e.Status = UpdateStatus.Continue AndAlso e.StatementType = StatementType.Insert Then
            Dim id As Integer = 0
            Using cmd As New SqlCommand("SELECT IDENT_CURRENT('sudungxe')", DXSchedulerConn)
                id = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
            e.Row("id") = id
        End If
    End Sub


    Private Sub SchedulerControl1_EditAppointmentFormShowing(sender As System.Object, e As DevExpress.XtraScheduler.AppointmentFormEventArgs) Handles SchedulerControl1.EditAppointmentFormShowing
        e.Handled = True
        Dim frm As frmThemsudungxe = New frmThemsudungxe()
        If SchedulerControl1.SelectedAppointments.Count > 0 Then
            Dim id As Integer = Convert.ToInt32(SchedulerControl1.SelectedAppointments(0).CustomFields("id"))
            ' MsgBox(id)
            frm.Message = id
            frm.Muontra = 2

        Else
            ' MsgBox("0")
            frm.Message = 0
            frm.Muontra = 0
        End If
        frm.ShowDialog()
        loadData()
    End Sub

    Private Sub SchedulerControl1_PopupMenuShowing(sender As System.Object, e As DevExpress.XtraScheduler.PopupMenuShowingEventArgs) Handles SchedulerControl1.PopupMenuShowing
        Dim item As SchedulerMenuItem
        If e.Menu.Id = SchedulerMenuItemId.DefaultMenu Then
            ' Disable the "New Recurring Appointment" menu item.
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent)
            ' Hide the "New Recurring Event" menu item.
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.TimeScaleVisible)
            ' Enable the "Go To Today" menu item.
            e.Menu.EnableMenuItem(SchedulerMenuItemId.GotoToday)

            ' Find the "New Appointment" menu item and rename it.
            item = e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment)
            If item IsNot Nothing Then
                item.Caption = "Đăng ký mượn xe mới"
            End If
            item = e.Menu.GetMenuItemById(SchedulerMenuItemId.GotoToday, False)
            If item IsNot Nothing Then
                item.Caption = "Ngày hôm nay"
            End If
            item = e.Menu.GetMenuItemById(SchedulerMenuItemId.GotoDate, False)
            If item IsNot Nothing Then
                item.Caption = "Chuyển đến ngày..."
            End If
            If SchedulerControl1.ActiveViewType = SchedulerViewType.Timeline Then
                e.Menu.Items(3).Caption = "Tùy chọn hiển thị"
            End If
            If SchedulerControl1.ActiveViewType = SchedulerViewType.Timeline Then
                e.Menu.Items(4).Caption = "Chế độ hiển thị"
            End If
            If SchedulerControl1.ActiveViewType = SchedulerViewType.Week Or SchedulerControl1.ActiveViewType = SchedulerViewType.Month Then
                e.Menu.Items(1).Caption = "Đến ngày hôm nay"
                e.Menu.Items(4).Caption = "Chế độ hiển thị"
            End If
            If SchedulerControl1.ActiveViewType = SchedulerViewType.Day Then
                e.Menu.Items(3).Caption = "Chế độ hiển thị"
            End If
        End If

        If e.Menu.Id = SchedulerMenuItemId.AppointmentMenu Then
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu)
            item = e.Menu.GetMenuItemById(SchedulerMenuItemId.OpenAppointment, False)
            If item IsNot Nothing Then
                item.Caption = "Xem"
            End If
            item = e.Menu.GetMenuItemById(SchedulerMenuItemId.DeleteAppointment, False)
            If item IsNot Nothing Then
                item.Caption = "Xóa"
            End If
        End If


    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        loadData()
    End Sub

    Private Sub SchedulerStorage1_AppointmentDeleting(sender As System.Object, e As DevExpress.XtraScheduler.PersistentObjectCancelEventArgs) Handles SchedulerStorage1.AppointmentDeleting
        Dim id_sudungxe As Integer = Convert.ToInt32(SchedulerControl1.SelectedAppointments(0).CustomFields("id"))
        Dim query = "select id from huhaixe where id_sudungxe=@id_sudungxe"
        AddParameter("@id_sudungxe", id_sudungxe)
        Dim dt As DataTable = ExecuteSQLDataTable(query)
        If dt.Rows.Count > 0 Then
            If ShowCauHoi("Lần mượn xe này có hư hại vẫn muốn xóa ?") = False Then
                e.Cancel = True
            Else
                query = "delete from huhaixe where id_sudungxe=@id_sudungxe"
                AddParameter("@id_sudungxe", id_sudungxe)
                ExecuteSQLNonQuery(query)
            End If
        Else
            If ShowCauHoi("Có muốn xóa không ?") = False Then
                e.Cancel = True
            End If
        End If

    End Sub

    Private Sub SchedulerStorage1_AppointmentsDeleted(sender As System.Object, e As DevExpress.XtraScheduler.PersistentObjectsEventArgs) Handles SchedulerStorage1.AppointmentsDeleted
        Try

            AppointmentDataAdapter.Update(DXSchedulerDataset.Tables("Appointments"))
            DXSchedulerDataset.AcceptChanges()
        Catch ex As Exception
            ShowBaoLoi("Lỗi không thể xóa bản ghi này !")
        End Try
    End Sub

End Class

Public Class EditorLocalizer
    Inherits DevExpress.XtraEditors.Controls.Localizer
    Public Overrides Function GetLocalizedString(ByVal id As DevExpress.XtraEditors.Controls.StringId) As String

        If id = DevExpress.XtraEditors.Controls.StringId.DateEditToday Then
            Return "Hôm nay"
        End If
        Return MyBase.GetLocalizedString(id)
    End Function
End Class