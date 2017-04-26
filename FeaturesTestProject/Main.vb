Public Class Main

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex = 0 Then TaskManager.Show()
        If ListBox1.SelectedIndex = 1 Then IPAddress.Show()
        If ListBox1.SelectedIndex = 2 Then WebReader.Show()
        If ListBox1.SelectedIndex = 3 Then LanguageChange.Show()
        If ListBox1.SelectedIndex = 4 Then BackGroundWork.Show()
        'If ListBox1.SelectedIndex = 5 Then Form1.Show()
        'If ListBox1.SelectedIndex = 6 Then Form1.Show()
        'If ListBox1.SelectedIndex = 7 Then Form1.Show()
        'If ListBox1.SelectedIndex = 8 Then Form1.Show()
        'If ListBox1.SelectedIndex = 9 Then Form1.Show()

    End Sub
End Class