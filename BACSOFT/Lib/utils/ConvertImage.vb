Namespace Utils
    Public Class ConvertImage
        Public Shared Function Image2ByteFromImgUrl(ByVal imagePath As String) As Byte()
            Dim fs As System.IO.FileStream = New System.IO.FileStream(imagePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read)
            Dim rs() As Byte = New Byte(fs.Length) {}
            fs.Read(rs, 0, System.Convert.ToInt32(fs.Length))
            fs.Close()
            Return rs
        End Function

        Public Shared Function Image2ByteFromImgUrlS100(ByVal imagePath As String, Optional ByVal w As Integer = 100) As Byte()
            Dim bm As Image = Image.FromFile(imagePath)
            ResizeImage(bm, w).Save(Application.StartupPath & "\tmp.png", System.Drawing.Imaging.ImageFormat.Png)
            Return Image2ByteFromImgUrl(Application.StartupPath & "\tmp.png")
        End Function

        Public Shared Function ResizeImgFromURL(ByVal imagePath As String, Optional ByVal w As Integer = 100) As String
            Dim bm As Image = Image.FromFile(imagePath)
            ResizeImage(bm, w).Save(Application.StartupPath & "\tmp.png", System.Drawing.Imaging.ImageFormat.Png)
            Return Application.StartupPath & "\tmp.png"
        End Function

        Public Shared Function Byte2Image(ByVal byteImg() As Byte) As System.Drawing.Image
            Dim ms As New System.IO.MemoryStream(byteImg)
            Dim bm As New System.Drawing.Bitmap(ms)
            ms.Close()
            Return bm
        End Function

        Public Shared Function ResizeImage(ByVal image As Image, Optional ByVal newWidth As Integer = 100) As Image
            Dim newHeight As Integer = Math.Round((newWidth / image.Width) * image.Height)
            Dim newImage As Image = New Bitmap(newWidth, newHeight)
            Using graphicsHandle As Graphics = Graphics.FromImage(newImage)
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight)
            End Using
            Return newImage
        End Function
    End Class
End Namespace
