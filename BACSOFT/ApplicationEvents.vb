Imports DevExpress.LookAndFeel
Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            System.Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("vi-VN", True)
            DevExpress.Utils.Paint.XPaint.ForceGDIPlusPaint()
            Application.EnableVisualStyles = True
            DevExpress.Skins.SkinManager.EnableFormSkins()
            DevExpress.UserSkins.OfficeSkins.Register()
            DevExpress.UserSkins.BonusSkins.Register()
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(GetType(DevExpress.UserSkins.BACSKIN).Assembly)
        End Sub

    End Class

End Namespace

