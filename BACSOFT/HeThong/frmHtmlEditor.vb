Public Class frmHtmlEditor 

    Private Sub frmHtmlEditor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtNoiDungEmail.Navigate("about:blank")
        txtNoiDungEmail.Document.DomDocument.DesignMode = "On"
        txtNoiDungEmail.IsWebBrowserContextMenuEnabled = False
        'txtNoiDungEmail.Visible = True
        For Each f As FontFamily In FontFamily.Families
            rcmbFont.Items.Add(f.Name)
        Next
        cmbFont.EditValue = "Times New Roman"
        cmbSize.EditValue = 3
    End Sub

#Region " -- EDITTOR BUTTON COMMAND -- "
    Private Sub btnPaste_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPaste.ItemClick
        txtNoiDungEmail.Document.ExecCommand("Paste", False, Nothing)
    End Sub

    Private Sub btnCopy_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCopy.ItemClick
        txtNoiDungEmail.Document.ExecCommand("Copy", False, Nothing)
    End Sub

    Private Sub btnCut_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCut.ItemClick
        txtNoiDungEmail.Document.ExecCommand("Cut", False, Nothing)
    End Sub

    Private Sub cmbFont_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbFont.EditValueChanged
        txtNoiDungEmail.Document.ExecCommand("FontName", False, cmbFont.EditValue)
    End Sub

    Private Sub cmbSize_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbSize.EditValueChanged
        txtNoiDungEmail.Document.ExecCommand("FontSize", False, cmbSize.EditValue)
    End Sub

    Private Sub btnB_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnB.ItemClick
        txtNoiDungEmail.Document.ExecCommand("Bold", False, Nothing)
    End Sub

    Private Sub btnU_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnU.ItemClick
        txtNoiDungEmail.Document.ExecCommand("Underline", False, Nothing)
    End Sub

    Private Sub btnI_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnI.ItemClick
        txtNoiDungEmail.Document.ExecCommand("Italic", False, Nothing)
    End Sub

    Private Sub btnInsertImage_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnInsertImage.ItemClick
        txtNoiDungEmail.Document.ExecCommand("insertImage", True, Nothing)
    End Sub

    Private Sub btnLink_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLink.ItemClick
        txtNoiDungEmail.Document.ExecCommand("createLink", True, Nothing)
    End Sub

    Private Sub btnLeft_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnLeft.ItemClick
        txtNoiDungEmail.Document.ExecCommand("justifyLeft", False, Nothing)
    End Sub

    Private Sub btnCenter_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCenter.ItemClick
        txtNoiDungEmail.Document.ExecCommand("justifyCenter", False, Nothing)
    End Sub

    Private Sub btnRight_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRight.ItemClick
        txtNoiDungEmail.Document.ExecCommand("justifyRight", False, Nothing)
    End Sub

    Private Sub btnMiddle_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnMiddle.ItemClick
        txtNoiDungEmail.Document.ExecCommand("justifyFull", False, Nothing)
    End Sub

    Private Sub btnRLeft_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRLeft.ItemClick
        txtNoiDungEmail.Document.ExecCommand("outdent", False, Nothing)
    End Sub

    Private Sub btnRRight_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRRight.ItemClick
        txtNoiDungEmail.Document.ExecCommand("indent", False, Nothing)
    End Sub

    Private Sub btnListU_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnListU.ItemClick
        txtNoiDungEmail.Document.ExecCommand("insertUnorderedList", False, Nothing)
    End Sub

    Private Sub btnListNumber_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnListNumber.ItemClick
        txtNoiDungEmail.Document.ExecCommand("InsertOrderedList", False, Nothing)
    End Sub

    Private Sub btnUp_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnUp.ItemClick
        txtNoiDungEmail.Document.ExecCommand("superscript", False, Nothing)
    End Sub

    Private Sub btnDown_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDown.ItemClick
        txtNoiDungEmail.Document.ExecCommand("subscript", False, Nothing)
    End Sub

    Private Sub btnChangeForeColor_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btnChangeForeColor.EditValueChanged
        txtNoiDungEmail.Document.ExecCommand("foreColor", False, ColorTranslator.ToHtml(btnChangeForeColor.EditValue))
    End Sub

    Private Sub btnChangeBgColor_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles btnChangeBgColor.EditValueChanged
        txtNoiDungEmail.Document.ExecCommand("backColor", False, ColorTranslator.ToHtml(btnChangeBgColor.EditValue))
    End Sub

    Private Sub btnClearFormat_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClearFormat.ItemClick
        txtNoiDungEmail.Document.ExecCommand("removeFormat", False, Nothing)
    End Sub

    Private Sub btnHtmlView_CheckedChanged(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnHtmlView.CheckedChanged
        If btnHtmlView.Checked Then
            txtNoiDungHTML.Left = txtNoiDungEmail.Left
            txtNoiDungHTML.Top = txtNoiDungEmail.Top
            txtNoiDungHTML.Width = txtNoiDungEmail.Width
            txtNoiDungHTML.Height = txtNoiDungEmail.Height
            txtNoiDungHTML.Visible = True
            txtNoiDungEmail.Visible = False
            For i As Integer = 0 To Bar3.ItemLinks.Count - 1
                Bar3.ItemLinks(i).Item.Enabled = False
            Next
            btnHtmlView.Enabled = True
            txtNoiDungHTML.Text = txtNoiDungEmail.DocumentText
        Else
            For i As Integer = 0 To Bar3.ItemLinks.Count - 1
                Bar3.ItemLinks(i).Item.Enabled = True
            Next
            txtNoiDungEmail.Document.OpenNew(True)
            txtNoiDungEmail.Document.Write(String.Empty)

            txtNoiDungEmail.Document.Write(txtNoiDungHTML.Text)
            txtNoiDungEmail.Visible = True
            txtNoiDungHTML.Visible = False
        End If
    End Sub

    Public Sub NoiDungMoi()
        txtNoiDungEmail.Document.Write(String.Empty)
        txtNoiDungEmail.DocumentText = ""
    End Sub


    Public Sub ChenImageInline(Id As String, Url As String)
        Dim tagImg = txtNoiDungEmail.Document.CreateElement("img")
        tagImg.SetAttribute("src", Url)
        tagImg.SetAttribute("cid", Id)
        txtNoiDungEmail.Document.Body.AppendChild(tagImg)
    End Sub

    Public Function getHtmlBodyConverted() As String
        Dim htmlBody As HtmlElement = txtNoiDungEmail.Document.CreateElement("body")
        htmlBody.InnerHtml = txtNoiDungEmail.Document.Body.InnerHtml
        Dim tagImgCollect As HtmlElementCollection = htmlBody.GetElementsByTagName("img")
        For Each img As HtmlElement In tagImgCollect
            If img.GetAttribute("cid") = "" Then Continue For
            img.SetAttribute("src", "cid:" & img.GetAttribute("cid"))
        Next
        Return htmlBody.InnerHtml
    End Function


#End Region

End Class