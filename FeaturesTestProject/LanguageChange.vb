Imports System.Globalization
Imports System.Threading
Imports System.Resources

Public Class LanguageChange
    'Reference：http://ryan-tw.blogspot.tw/2012/02/vbnet.html

    '定義語系資源檔
    Dim res As System.Resources.ResourceManager
    Private Sub LanguageChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Language Change"
        Label2.Text = "Update 2017/04/26"

        '定義資源檔存取目標
        res = New Resources.ResourceManager("FeaturesTestProject.LanguageChange", Me.GetType().Assembly)

        FlagImage()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case Thread.CurrentThread.CurrentCulture.Name
            Case "zh-TW" : MsgBox("繁體中文")
            Case "ja" : MsgBox("日本語")
            Case "en" : MsgBox("English")
        End Select

    End Sub

    Private Sub 繁體中文zhtwToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 繁體中文zhtwToolStripMenuItem.Click
        '切換CurrentCulture
        Thread.CurrentThread.CurrentCulture = New CultureInfo("zh-TW")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("zh-TW")

        '把繁體中文的資源內容顯示在各個物件上
        Button1.Text = res.GetString("Button1.Text")
        Label1.Text = res.GetString("Label1.Text")
        LanguageToolStripMenuItem.Text = res.GetString("LanguageToolStripMenuItem.Text")
        ExitToolStripMenuItem.Text = res.GetString("ExitToolStripMenuItem.Text")

        '從網路抓取國旗圖片並顯示國旗
        FlagImage()
    End Sub

    Private Sub 日本語jaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 日本語jaToolStripMenuItem.Click
        '切換CurrentCulture
        Thread.CurrentThread.CurrentCulture = New CultureInfo("ja")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("ja")

        '把日文的資源內容顯示在各個物件上
        Button1.Text = res.GetString("Button1.Text")
        Label1.Text = res.GetString("Label1.Text")
        LanguageToolStripMenuItem.Text = res.GetString("LanguageToolStripMenuItem.Text")
        ExitToolStripMenuItem.Text = res.GetString("ExitToolStripMenuItem.Text")

        '從網路抓取國旗圖片並顯示國旗
        FlagImage()
    End Sub

    Private Sub EnglishenusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnglishenusToolStripMenuItem.Click
        '切換CurrentCulture
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en")

        '把英文文的資源內容顯示在各個物件上
        Button1.Text = res.GetString("Button1.Text")
        Label1.Text = res.GetString("Label1.Text")
        LanguageToolStripMenuItem.Text = res.GetString("LanguageToolStripMenuItem.Text")
        ExitToolStripMenuItem.Text = res.GetString("ExitToolStripMenuItem.Text")

        '從網路抓取國旗圖片並顯示國旗
        FlagImage()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub FlagImage()
        Dim ImageUri As String = ""
        Select Case Thread.CurrentThread.CurrentCulture.Name
            Case "zh-TW" : ImageUri = "https://www.ifreesite.com/world/image/taiwan_flag.png"
            Case "ja" : ImageUri = "https://www.ifreesite.com/world/image/japan_flag.png"
            Case "en" : ImageUri = "https://www.ifreesite.com/world/image/united_states_of_america_flag.png"
        End Select
        Dim RequestImage As System.Net.HttpWebRequest = System.Net.WebRequest.Create(ImageUri)
        Dim ResponImage As System.Net.HttpWebResponse = RequestImage.GetResponse()
        Try
            If ResponImage.StatusCode = System.Net.HttpStatusCode.OK Then PictureBox1.Image = New Bitmap(ResponImage.GetResponseStream())
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class