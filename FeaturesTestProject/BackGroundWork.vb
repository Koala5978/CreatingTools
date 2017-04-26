Imports System.ComponentModel
Public Class BackGroundWork



    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If (e.Error IsNot Nothing) Then
            MessageBox.Show(e.Error.Message)
        ElseIf e.Cancelled Then
            MessageBox.Show("使用者取消執行!")
        Else
            'ProgressBar1.Visible = False   ' 執行完成隱藏 ProgressBar
            MessageBox.Show("工作完成!")
            'TextBox2.Text = Webtext
        End If
    End Sub
End Class