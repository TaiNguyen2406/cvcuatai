Public Class frmIn

    Public Shared rpt As Object
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal strTieuDe As String)
        InitializeComponent()
        Me.Text = strTieuDe

    End Sub

   
    Private Sub frmIn_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        printControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.HandTool, New Object() {True})
        'CloseWaiting()
        'printControl.PrintingSystem.
    End Sub

End Class