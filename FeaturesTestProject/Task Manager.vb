Imports System.Math
Imports System.Diagnostics
Public Class TaskManager

    Private CPU As PerformanceCounter = New PerformanceCounter("Processor", "% Processor Time", "_Total")
    Private Memory As PerformanceCounter = New PerformanceCounter("Memory", "% Committed Bytes in Use")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Task Manager - Update 20170421"
        ShowPorgramList()
        PerformanceUsedRatio()

        '計時器
        Timer1.Interval = 1000
        Timer1.Start()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ShowPorgramList()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        KillProgram()
    End Sub

    Private Sub ShowPorgramList()
        ListView1.Items.Clear()
        ListView1.Columns.Clear()

        '顯示所有目前正在執行的程序
        Dim arraystring() As Process = Process.GetProcesses()
        ListView1.View = View.Details
        ' Allow the user to rearrange columns.
        ListView1.AllowColumnReorder = False
        ' Select the item and subitems when selection is made.
        ListView1.FullRowSelect = True
        ' Sort the items in the list in ascending order.
        ListView1.Sorting = SortOrder.Ascending
        ListView1.Columns.Add("Name", CInt(ListView1.Width * 0.3), HorizontalAlignment.Left)
        ListView1.Columns.Add("Time", CInt(ListView1.Width * 0.3), HorizontalAlignment.Left)
        ListView1.Columns.Add("RAM", CInt(ListView1.Width * 0.3), HorizontalAlignment.Right)

        For i = 0 To arraystring.Count - 1
            Dim str As String = ""
            Try
                Dim item As New ListViewItem(arraystring(i).ProcessName, 0)
                ' Place a check mark next to the item.
                item.Checked = True
                item.SubItems.Add(arraystring(i).StartTime)
                item.SubItems.Add(CStr(Round(arraystring(i).PrivateMemorySize64 / 1024 ^ 2, 1)) + " MB")
                'Add the items to the ListView.
                ListView1.Items.AddRange(New ListViewItem() {item})

            Catch ex As Exception
                Dim item As New ListViewItem(arraystring(i).ProcessName, 0)
                ' Place a check mark next to the item.
                item.Checked = True
                item.SubItems.Add(ex.Message)
                item.SubItems.Add(CStr(Round(arraystring(i).PrivateMemorySize64 / 1024 ^ 2, 1)) + " MB")
                'Add the items to the ListView.
                ListView1.Items.AddRange(New ListViewItem() {item})

            End Try
        Next

    End Sub

    Private Sub KillProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KillProgramToolStripMenuItem.Click
        KillProgram()
    End Sub

    Private Sub ReflashToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReflashToolStripMenuItem.Click

    End Sub

    Private Sub ListView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then ContextMenuStrip1.Show(Windows.Forms.Cursor.Position.X, Windows.Forms.Cursor.Position.Y)
    End Sub

    Private Sub KillProgram()
        If MsgBox("確定要強制中止 " + ListView1.FocusedItem.SubItems(0).Text + " 嗎?", MsgBoxStyle.OkCancel, "Notice") = DialogResult.OK Then
            Process.GetProcessesByName(ListView1.FocusedItem.SubItems(0).Text)(0).Kill()
            Button1.PerformClick()
        End If
    End Sub

    Private Sub PerformanceUsedRatio()
        ToolStripStatusLabel1.Text = "CPU：" + ProcessUseRatio() + "   "
        ToolStripStatusLabel2.Text = "RAM：" + MemoryUseRatio()
    End Sub

    Private Function ProcessUseRatio() As String
        ProcessUseRatio = Decimal.Round(Decimal.Parse(CPU.NextValue().ToString()), 0, MidpointRounding.ToEven).ToString + " %"
        Return ProcessUseRatio

    End Function
    Private Function MemoryUseRatio() As String
        MemoryUseRatio = Decimal.Round(Decimal.Parse(Memory.NextValue().ToString()), 0, MidpointRounding.ToEven).ToString + " %"
        Return MemoryUseRatio

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PerformanceUsedRatio()
    End Sub
End Class
