'Imports System.Diagnostics
Imports System.Threading
Imports System.Runtime.InteropServices
Public Class Feature_Test

    Private Const WM_COMMAND As Integer = &H111
    <DllImport("user32.dll", EntryPoint:="FindWindow")> _
    Public Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="GetMenu")> _
    Public Shared Function GetMenu(ByVal hwnd As Integer) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="GetSubMenu")> _
    Public Shared Function GetSubMenu(ByVal hMenu As Integer, ByVal nPos As Integer) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="GetMenuItemID")> _
    Public Shared Function GetMenuItemID(ByVal hMenu As Integer, ByVal nPos As Integer) As Integer
    End Function
    <DllImport("user32.dll", EntryPoint:="PostMessage")> _
    Public Shared Function PostMessage(ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function
    <DllImport("user32.dll")> _
    Private Shared Sub keybd_event(bVk As Byte, bScan As Byte, dwFlags As UInteger, dwExtraInfo As UIntPtr)
    End Sub

    Private Sub T1()
        '呼叫外部程式
        Process.Start("notepad")
        '等執行程式一下下
        Thread.Sleep(1000)
        '取得外部程式的Handle
        Dim NotepadHwnd As Integer = FindWindow("notepad", Nothing)
        '取得外部程式的選單Handle
        Dim gm As Integer = GetMenu(NotepadHwnd)
        '取得第一個子選單Handle
        gm = GetSubMenu(gm, 0) '選單的句柄 改變後面的0，就可以得到不同選單的句柄
        '取得子選單的ID
        Dim id As Integer = GetMenuItemID(gm, 3) '子選單"另存新檔"的ID
        '執行子選單
        PostMessage(NotepadHwnd, WM_COMMAND, id, 0)
    End Sub
    Private Sub T1_()
        '設定按鍵
        '參考網址：http://www.pinvoke.net/search.aspx?search=VK&namespace=%5BAll%5D
        '參考網址：https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
        Const VK_MENU As Byte = &H12  'Alt
        Const VK_F As Byte = &H46   'F
        Const KEYEVENTF_KEYUP As Byte = &H2
        '呼叫外部程式
        Process.Start("notepad")
        '等待1秒鐘
        Thread.Sleep(1000)
        '按下Alt
        keybd_event(VK_MENU, 0, 0, 0)
        keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0)
        '按下F
        keybd_event(VK_F, 0, 0, 0)
        keybd_event(VK_F, 0, KEYEVENTF_KEYUP, 0)
        '與T1()的行為相同 但操控方式不一樣

    End Sub
    Private Sub T2()
        'Dim procID As Integer
        Dim AppN As New ProcessStartInfo("calc", vbNormalFocus)

        Dim AppS As Process = Process.Start(AppN)
        'TextBox2.Text += AppS.Threads.Item(0)
        MsgBox("預備")
        AppS.WaitForExit(5000)
        SendKeys.Send("2")
        AppS.WaitForExit(5000)
        SendKeys.Send("+")
        AppS.WaitForExit(5000)
        SendKeys.Send("3")
        AppS.WaitForExit(5000)
        SendKeys.Send("=")
        ' Run calculator.
        'procID = Shell("calc", AppWinStyle.NormalFocus)


    End Sub
End Class