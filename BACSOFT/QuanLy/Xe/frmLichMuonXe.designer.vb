<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLichMuonXe
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLichMuonXe))
        Dim TimeRuler1 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Dim TimeScaleYear1 As DevExpress.XtraScheduler.TimeScaleYear = New DevExpress.XtraScheduler.TimeScaleYear()
        Dim TimeScaleQuarter1 As DevExpress.XtraScheduler.TimeScaleQuarter = New DevExpress.XtraScheduler.TimeScaleQuarter()
        Dim TimeScaleMonth1 As DevExpress.XtraScheduler.TimeScaleMonth = New DevExpress.XtraScheduler.TimeScaleMonth()
        Dim TimeScaleWeek1 As DevExpress.XtraScheduler.TimeScaleWeek = New DevExpress.XtraScheduler.TimeScaleWeek()
        Dim TimeScaleDay1 As DevExpress.XtraScheduler.TimeScaleDay = New DevExpress.XtraScheduler.TimeScaleDay()
        Dim TimeScaleFixedInterval1 As DevExpress.XtraScheduler.TimeScaleFixedInterval = New DevExpress.XtraScheduler.TimeScaleFixedInterval()
        Dim TimeRuler2 As DevExpress.XtraScheduler.TimeRuler = New DevExpress.XtraScheduler.TimeRuler()
        Me.SchedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
        Me.SchedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.RibbonViewSelector1 = New DevExpress.XtraScheduler.UI.RibbonViewSelector(Me.components)
        Me.RibbonViewNavigator1 = New DevExpress.XtraScheduler.UI.RibbonViewNavigator()
        Me.DateNavigator1 = New DevExpress.XtraScheduler.DateNavigator()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SchedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonViewSelector1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonViewNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SchedulerStorage1
        '
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Empty, ""))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel("Không quan trọng", "&Không quan trọng"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Lime, "Bình thường", "&Bình thường"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer)), "Quan trọng", "&Quan trọng"))
        Me.SchedulerStorage1.Appointments.Labels.Add(New DevExpress.XtraScheduler.AppointmentLabel(System.Drawing.Color.Magenta, "Khẩn cấp", "&Khẩn cấp"))
        Me.SchedulerStorage1.Appointments.Statuses.Add(New DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.Custom, System.Drawing.Color.Empty, ""))
        Me.SchedulerStorage1.Appointments.Statuses.Add(New DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.Tentative, "Chưa nhận xe", "&Chưa nhận xe"))
        Me.SchedulerStorage1.Appointments.Statuses.Add(New DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.Busy, System.Drawing.Color.FromArgb(CType(CType(74, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(226, Byte), Integer)), "Đã nhận xe", "&Đã nhận xe"))
        Me.SchedulerStorage1.Appointments.Statuses.Add(New DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.OutOfOffice, System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer)), "Đã trả xe", "&Đã trả xe"))
        Me.SchedulerStorage1.Appointments.Statuses.Add(New DevExpress.XtraScheduler.AppointmentStatus(DevExpress.XtraScheduler.AppointmentStatusType.Custom, System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer)), "Hủy", "&Hủy"))
        Me.SchedulerStorage1.EnableReminders = False
        Me.SchedulerStorage1.Resources.Mappings.Caption = "tenxe"
        Me.SchedulerStorage1.Resources.Mappings.Id = "id"
        '
        'SchedulerControl1
        '
        Me.SchedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Timeline
        Me.SchedulerControl1.Appearance.ResourceHeaderCaption.Font = CType(resources.GetObject("SchedulerControl1.Appearance.ResourceHeaderCaption.Font"), System.Drawing.Font)
        Me.SchedulerControl1.Appearance.ResourceHeaderCaption.Options.UseFont = True
        Me.SchedulerControl1.Appearance.ResourceHeaderCaptionLine.Font = CType(resources.GetObject("SchedulerControl1.Appearance.ResourceHeaderCaptionLine.Font"), System.Drawing.Font)
        Me.SchedulerControl1.Appearance.ResourceHeaderCaptionLine.Options.UseFont = True
        resources.ApplyResources(Me.SchedulerControl1, "SchedulerControl1")
        Me.SchedulerControl1.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource
        Me.SchedulerControl1.MenuManager = Me.BarManager1
        Me.SchedulerControl1.Name = "SchedulerControl1"
        Me.SchedulerControl1.OptionsBehavior.SelectOnRightClick = True
        Me.SchedulerControl1.OptionsCustomization.AllowAppointmentCopy = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.SchedulerControl1.OptionsCustomization.AllowAppointmentDrag = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.SchedulerControl1.OptionsCustomization.AllowAppointmentDragBetweenResources = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.SchedulerControl1.OptionsCustomization.AllowInplaceEditor = DevExpress.XtraScheduler.UsedAppointmentType.None
        Me.SchedulerControl1.OptionsView.FirstDayOfWeek = DevExpress.XtraScheduler.FirstDayOfWeek.Monday
        Me.SchedulerControl1.OptionsView.NavigationButtons.NextCaption = resources.GetString("SchedulerControl1.OptionsView.NavigationButtons.NextCaption")
        Me.SchedulerControl1.OptionsView.NavigationButtons.PrevCaption = resources.GetString("SchedulerControl1.OptionsView.NavigationButtons.PrevCaption")
        Me.SchedulerControl1.OptionsView.ResourceHeaders.RotateCaption = False
        Me.SchedulerControl1.ResourceNavigator.Visibility = DevExpress.XtraScheduler.ResourceNavigatorVisibility.Always
        Me.SchedulerControl1.Start = New Date(2016, 8, 6, 0, 0, 0, 0)
        Me.SchedulerControl1.Storage = Me.SchedulerStorage1
        Me.SchedulerControl1.Views.DayView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.DayView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.DayView.DayCount = 3
        Me.SchedulerControl1.Views.DayView.DisplayName = resources.GetString("SchedulerControl1.Views.DayView.DisplayName")
        Me.SchedulerControl1.Views.DayView.MenuCaption = resources.GetString("SchedulerControl1.Views.DayView.MenuCaption")
        Me.SchedulerControl1.Views.DayView.ResourcesPerPage = 5
        Me.SchedulerControl1.Views.DayView.ShowWorkTimeOnly = True
        TimeRuler1.TimeZone.DaylightBias = System.TimeSpan.Parse("-01:00:00")
        TimeRuler1.TimeZone.DaylightZoneName = resources.GetString("resource.DaylightZoneName")
        TimeRuler1.TimeZone.DisplayName = resources.GetString("resource.DisplayName")
        TimeRuler1.TimeZone.StandardZoneName = resources.GetString("resource.StandardZoneName")
        TimeRuler1.TimeZone.UtcOffset = System.TimeSpan.Parse("07:00:00")
        TimeRuler1.UseClientTimeZone = False
        Me.SchedulerControl1.Views.DayView.TimeRulers.Add(TimeRuler1)
        Me.SchedulerControl1.Views.DayView.VisibleTime.Start = System.TimeSpan.Parse("06:00:00")
        Me.SchedulerControl1.Views.DayView.WorkTime.End = System.TimeSpan.Parse("17:30:00")
        Me.SchedulerControl1.Views.DayView.WorkTime.Start = System.TimeSpan.Parse("07:30:00")
        Me.SchedulerControl1.Views.MonthView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.MonthView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.MonthView.DisplayName = resources.GetString("SchedulerControl1.Views.MonthView.DisplayName")
        Me.SchedulerControl1.Views.MonthView.MenuCaption = resources.GetString("SchedulerControl1.Views.MonthView.MenuCaption")
        Me.SchedulerControl1.Views.MonthView.ResourcesPerPage = 3
        Me.SchedulerControl1.Views.MonthView.ShowMoreButtons = False
        Me.SchedulerControl1.Views.TimelineView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.TimelineView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.TimelineView.DisplayName = resources.GetString("SchedulerControl1.Views.TimelineView.DisplayName")
        Me.SchedulerControl1.Views.TimelineView.MenuCaption = resources.GetString("SchedulerControl1.Views.TimelineView.MenuCaption")
        Me.SchedulerControl1.Views.TimelineView.ResourcesPerPage = 10
        resources.ApplyResources(TimeScaleYear1, "TimeScaleYear1")
        TimeScaleYear1.Enabled = False
        resources.ApplyResources(TimeScaleQuarter1, "TimeScaleQuarter1")
        TimeScaleQuarter1.Enabled = False
        resources.ApplyResources(TimeScaleMonth1, "TimeScaleMonth1")
        TimeScaleMonth1.Enabled = False
        resources.ApplyResources(TimeScaleWeek1, "TimeScaleWeek1")
        TimeScaleWeek1.Enabled = False
        TimeScaleDay1.DisplayFormat = "dddd, dd MMMM yyyy"
        resources.ApplyResources(TimeScaleDay1, "TimeScaleDay1")
        TimeScaleFixedInterval1.DisplayFormat = "HH:mm"
        resources.ApplyResources(TimeScaleFixedInterval1, "TimeScaleFixedInterval1")
        TimeScaleFixedInterval1.Value = System.TimeSpan.Parse("00:30:00")
        TimeScaleFixedInterval1.Width = 40
        Me.SchedulerControl1.Views.TimelineView.Scales.Add(TimeScaleYear1)
        Me.SchedulerControl1.Views.TimelineView.Scales.Add(TimeScaleQuarter1)
        Me.SchedulerControl1.Views.TimelineView.Scales.Add(TimeScaleMonth1)
        Me.SchedulerControl1.Views.TimelineView.Scales.Add(TimeScaleWeek1)
        Me.SchedulerControl1.Views.TimelineView.Scales.Add(TimeScaleDay1)
        Me.SchedulerControl1.Views.TimelineView.Scales.Add(TimeScaleFixedInterval1)
        Me.SchedulerControl1.Views.TimelineView.TimelineScrollBarVisible = True
        Me.SchedulerControl1.Views.TimelineView.WorkTime.End = System.TimeSpan.Parse("17:10:00")
        Me.SchedulerControl1.Views.TimelineView.WorkTime.Start = System.TimeSpan.Parse("08:00:00")
        Me.SchedulerControl1.Views.WeekView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.WeekView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.WeekView.DisplayName = resources.GetString("SchedulerControl1.Views.WeekView.DisplayName")
        Me.SchedulerControl1.Views.WeekView.MenuCaption = resources.GetString("SchedulerControl1.Views.WeekView.MenuCaption")
        Me.SchedulerControl1.Views.WeekView.ResourcesPerPage = 5
        Me.SchedulerControl1.Views.WorkWeekView.AppointmentDisplayOptions.EndTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.WorkWeekView.AppointmentDisplayOptions.StartTimeVisibility = DevExpress.XtraScheduler.AppointmentTimeVisibility.Never
        Me.SchedulerControl1.Views.WorkWeekView.ResourcesPerPage = 5
        Me.SchedulerControl1.Views.WorkWeekView.ShowFullWeek = True
        TimeRuler2.TimeZone.DaylightBias = System.TimeSpan.Parse("-01:00:00")
        TimeRuler2.TimeZone.DaylightZoneName = resources.GetString("resource.DaylightZoneName1")
        TimeRuler2.TimeZone.DisplayName = resources.GetString("resource.DisplayName1")
        TimeRuler2.TimeZone.StandardZoneName = resources.GetString("resource.StandardZoneName1")
        TimeRuler2.TimeZone.UtcOffset = System.TimeSpan.Parse("07:00:00")
        TimeRuler2.UseClientTimeZone = False
        Me.SchedulerControl1.Views.WorkWeekView.TimeRulers.Add(TimeRuler2)
        Me.SchedulerControl1.Views.WorkWeekView.VisibleTime.Start = System.TimeSpan.Parse("06:00:00")
        Me.SchedulerControl1.Views.WorkWeekView.WorkTime.End = System.TimeSpan.Parse("17:30:00")
        Me.SchedulerControl1.Views.WorkWeekView.WorkTime.Start = System.TimeSpan.Parse("07:30:00")
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1})
        Me.BarManager1.MainMenu = Me.Bar2
        Me.BarManager1.MaxItemId = 1
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, Me.BarButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        resources.ApplyResources(Me.Bar2, "Bar2")
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Appearance.Font = CType(resources.GetObject("BarButtonItem1.Appearance.Font"), System.Drawing.Font)
        Me.BarButtonItem1.Appearance.Options.UseFont = True
        resources.ApplyResources(Me.BarButtonItem1, "BarButtonItem1")
        Me.BarButtonItem1.Glyph = Global.BACSOFT.My.Resources.Resources.refresh_18
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        '
        'RibbonViewSelector1
        '
        Me.RibbonViewSelector1.SchedulerControl = Me.SchedulerControl1
        Me.RibbonViewSelector1.TargetRibbonPageName = Nothing
        '
        'RibbonViewNavigator1
        '
        Me.RibbonViewNavigator1.SchedulerControl = Me.SchedulerControl1
        Me.RibbonViewNavigator1.TargetRibbonPageName = Nothing
        '
        'DateNavigator1
        '
        resources.ApplyResources(Me.DateNavigator1, "DateNavigator1")
        Me.DateNavigator1.HotDate = Nothing
        Me.DateNavigator1.Name = "DateNavigator1"
        Me.DateNavigator1.SchedulerControl = Me.SchedulerControl1
        '
        'TableLayoutPanel1
        '
        resources.ApplyResources(Me.TableLayoutPanel1, "TableLayoutPanel1")
        Me.TableLayoutPanel1.Controls.Add(Me.SchedulerControl1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateNavigator1, 1, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        '
        'frmLichMuonXe
        '
        Me.Appearance.ForeColor = CType(resources.GetObject("frmLichMuonXe.Appearance.ForeColor"), System.Drawing.Color)
        Me.Appearance.Options.UseForeColor = True
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Name = "frmLichMuonXe"
        CType(Me.SchedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SchedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonViewSelector1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonViewNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SchedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
    Friend WithEvents SchedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
    Friend WithEvents RibbonViewSelector1 As DevExpress.XtraScheduler.UI.RibbonViewSelector
    Friend WithEvents RibbonViewNavigator1 As DevExpress.XtraScheduler.UI.RibbonViewNavigator
    Friend WithEvents DateNavigator1 As DevExpress.XtraScheduler.DateNavigator
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
End Class
