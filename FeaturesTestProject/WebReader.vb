Imports System.Net
Public Class WebReader
    Private Webtext As String
    Private Example_https As String = "https://www.example.com"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        WebSource()
        TextBox2.Text = Webtext
        Webtext = ""
    End Sub

    Private Sub WebSource()
        Dim page As String = TextBox1.Text
        If TextBox1.Text <> "" Then
            Try
                page = TextBox1.Text

                Dim myWebreq As WebRequest = WebRequest.Create(page)
                Dim myWebresp As WebResponse = myWebreq.GetResponse
                '-------------------------------------------
                '取得網頁資料流
                Dim myStream As IO.Stream = myWebresp.GetResponseStream
                '-------------------------------------------
                '讀取網頁
                Dim myReader As New IO.StreamReader(myStream, System.Text.Encoding.Default)
                '-------------------------------------------
                '取得網頁內容
                Do
                    Webtext += myReader.ReadLine + vbNewLine
                Loop Until myReader.EndOfStream <> False
            Catch ex As Exception
                MsgBox("讀取錯誤：" + ex.Message)
            End Try

        Else
            MsgBox("請輸入網址!!")
        End If
       

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        Watermark()
    End Sub

    Private Sub TextBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseDown
        Watermark()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Watermark()
    End Sub

    Private Sub WebReader_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = Example_https
        TextBox1.ForeColor = Color.Gray
        Me.Text = "Web Reader - Update 2017/04/21"
    End Sub

    Private Sub Watermark()
        '判斷Textbox1有沒有被注意到
        If TextBox1.Focused Then
            '判斷有沒有該字串
            If TextBox1.Text.IndexOf(Example_https) = -1 Then
                '判斷是否為空字串
                If TextBox1.Text = "" Then
                    TextBox1.Text = Example_https
                    TextBox1.ForeColor = Color.Gray
                End If
            Else
                '判斷是否跟預設字串完全一樣
                If TextBox1.Text = Example_https Then
                    TextBox1.Select(0, 0)
                Else
                    TextBox1.Select(0, 0)
                    TextBox1.Text = Split(TextBox1.Text, Example_https)(0)
                    TextBox1.ForeColor = Color.Black
                    TextBox1.Select(TextBox1.Text.Length, 0)
                End If
            End If

        End If
    End Sub

End Class