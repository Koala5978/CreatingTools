Imports System.Net
Imports System.Net.NetworkInformation
'Imports System.Int32
'Imports System.String
Imports System

Public Class IPAddress
    Dim Webtext As String
    
    Private Sub IPAddress_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "IP Adress and Network Card Information - Update 20170426"
        'IPInfo
        IP_Information()
        NetworkStatic()
        '測試Ping 8.8.8.8
        Timer1.Interval = 1000
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            PingColor(My.Computer.Network.Ping("8.8.8.8"))
        Catch ex As Exception
            PingColor(False)
        End Try

    End Sub
    Private Sub IP_Information()
        Label1.Text = "IP：" + GetIP()
        Label2.Text = "Country：" + GetCountry()
        Label3.Text = "Location：" + GetLocation()
        Label4.Text = "City："
        Label5.Text = "ISP：" + GetISP()
        LinkLabel1.Text = "Power by ipinfo"
        LinkLabel1.Links.Add(0, 16, "http://ipinfo.io")
        LinkLabel1.Links(0).Enabled = True

    End Sub
    Private Sub NetworkStatic()
        For i = 0 To Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Count - 1
            If Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(i).GetIPProperties.DhcpServerAddresses.Count > 0 Then
                Label7.Text = "Network Card：" + GetConnectingName(i)
                Label8.Text = "Description：" + GetConnectingDescription(i)
                Label9.Text = "Type：" + GetConnectingType(i)
                Label10.Text = "MAC Address：" + GetConnectingMAC(i)
                Label11.Text = "IPv4 Address：" + GetIPv4(i)
                Label12.Text = "DHCP Server：" + GetDHCPServer(i)
                Label13.Text = "DNS Server：" + GetConnectingDNS(i)
            End If
        Next
    End Sub

    Private Function PingColor(ByVal Ping As Boolean) As Boolean
        Dim g As Graphics = PictureBox1.CreateGraphics()
        Dim pen As Pen = New Pen(Color.Green, 2)
        Dim Brust1 As SolidBrush = New SolidBrush(Color.Green)
        Dim Brust2 As SolidBrush = New SolidBrush(Color.Red)
        If Ping = True Then g.FillEllipse(Brust1, 6, 6, 15, 15)
        If Ping = False Then g.FillEllipse(Brust2, 6, 6, 15, 15)

        Return True
    End Function

    Private Function GetInformation(ByVal RequrstString As String) As String
        Dim ResponesString As String = ""
        Try
            Dim myWebreq As WebRequest = WebRequest.Create("http://ipinfo.io")
            Dim myWebresp As WebResponse = myWebreq.GetResponse
            Dim myStream As IO.Stream = myWebresp.GetResponseStream
            Dim myReader As New IO.StreamReader(myStream, System.Text.Encoding.Default)
            Dim str(1) As String
            Do
                str = Split(Replace(LTrim(myReader.ReadLine), """", ""), ":")

                If RequrstString = str(0) Then
                    Select Case RequrstString
                        Case "ip" : ResponesString = str(1)
                        Case "hostname" : ResponesString = str(1)
                        Case "city" : ResponesString = str(1)
                        Case "region" : ResponesString = str(1)
                        Case "country" : ResponesString = str(1)
                        Case "loc" : ResponesString = str(1)
                        Case "org" : ResponesString = str(1)
                    End Select
                    Exit Do
                End If
            Loop Until myReader.EndOfStream <> False

        Catch ex As Exception
            ResponesString = "Erro"
        End Try
        Return ResponesString
    End Function

    Private Function GetIP() As String
        Dim IP As String = ""
        Try
            Dim myWebreq As WebRequest = WebRequest.Create("https://api.ipify.org/")
            Dim myWebresp As WebResponse = myWebreq.GetResponse
            Dim myStream As IO.Stream = myWebresp.GetResponseStream
            Dim myReader As New IO.StreamReader(myStream, System.Text.Encoding.Default)
            IP = myReader.ReadLine
        Catch ex As Exception
            IP = "Erro"
        End Try

        Return IP
    End Function

    Private Function GetCountry() As String
        Dim Country As String = ""

        Country = LTrim(Split(GetInformation("country"), ",")(0))
        Return Country
    End Function

    Private Function GetLocation() As String
        Dim Location, Latitude, Longitude As String

        'Latitude debug
        Try
            Latitude = LTrim(Split(GetInformation("loc"), ",")(0))
        Catch ex As Exception
            Latitude = "Erro"
        End Try

        'Longitude debug
        Try
            Longitude = LTrim(Split(GetInformation("loc"), ",")(1))
        Catch ex As Exception
            Longitude = "Erro"
        End Try
        Location = Latitude + ", " + Longitude

        Return Location
    End Function

    Private Function GetCity() As String
        Dim City As String = ""
        City = GetInformation("city")
        Return City
    End Function

    Private Function GetISP() As String
        Dim ISP As String = ""

        'debug
        Try
            For i = 0 To 5
                ISP = LTrim(Split(GetInformation("hostname"), ",")(0))
                If ISP <> "No Hostname" Then Exit For
            Next
        Catch ex As Exception
            ISP = "Erro"
        End Try
        
        Return ISP
    End Function

    Private Function GetOrganization() As String
        Dim Organization As String = ""
        'debug
        Try
            Organization = GetInformation("org")
        Catch ex As Exception
            Organization = "Erro"
        End Try

        Return Organization
    End Function

    Private Function GetConnectingName(ByVal ID As Integer) As String
        Dim ReturnString As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).Name
        Return ReturnString
    End Function
    Private Function GetConnectingType(ByVal ID As Integer) As String
        Dim ReturnString As String = ""
        'If Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).NetworkInterfaceType = "Ethernet" Then ReturnName = "乙太網路(Ethernet)"
        Select Case Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).NetworkInterfaceType.ToString
            Case "AsymmetricDsl" : ReturnString = "非對稱式數位用戶線路(ADSL)"
            Case "Atm" : ReturnString = "非同步傳輸模式(ATM)"
            Case "BasicIsdn" : ReturnString = "基本速率介面的整合式服務數位網路(ISDN)"
            Case "Ethernet" : ReturnString = "乙太網路"
            Case "Ethernet3Megabit" : ReturnString = "乙太網路(3 Mbit/秒)"
            Case "FastEthernetFx" : ReturnString = "光纖網路"
            Case "FastEthernetT" : ReturnString = "雙絞線網路"
            Case "Fddi" : ReturnString = "光纖分佈資料介面"
            Case "GenericModem" : ReturnString = "數據機網路"
            Case "GigabitEthernet" : ReturnString = "Gigabit 乙太網路"
            Case "HighPerformanceSerialBus" : ReturnString = "高效能序列匯流排"
            Case "IPOverAtm" : ReturnString = "非同步傳輸模式(ATM) 使用網際網路通訊協定(IP)"
            Case "Isdn" : ReturnString = "針對 ISDN 和 X.25 通訊協定"
            Case "Loopback" : ReturnString = "測試網路"
            Case "MultiRateSymmetricDsl" : ReturnString = "多速率數位"
            Case "Ppp" : ReturnString = "點對點通訊協定"
            Case "PrimaryIsdn" : ReturnString = "整合式服務數位網路 (ISDN)"
            Case "RateAdaptDsl" : ReturnString = "可調速率數位用戶線路 (RADSL)"
            Case "Slip" : ReturnString = "序列線路網際網路通訊協定 (SLIP)"
            Case "SymmetricDsl" : ReturnString = "使用對稱式數位用戶線路 (SDSL)"
            Case "TokenRing" : ReturnString = "Token-Ring"
            Case "Tunnel" : ReturnString = "通道連線"
            Case "Unknown" : ReturnString = "未知"
            Case "VeryHighSpeedDsl" : ReturnString = "超高速數位用戶線路 (VDSL)"
            Case "Wireless80211" : ReturnString = "802.11n"
            Case "Wman" : ReturnString = "WiMax 裝置的行動式寬頻介面"
            Case "Wwanpp" : ReturnString = "GSM 型裝置的行動式寬頻介面"
            Case "Wwanpp2" : ReturnString = "CDMA 型裝置的行動式寬頻介面"
        End Select
        '-----------------------------------
        'Reference：https://msdn.microsoft.com/zh-tw/library/system.net.networkinformation.networkinterfacetype(v=vs.110).aspx
        'AsymmetricDsl	網路介面使用非對稱式數位用戶線路 (ADSL)。
        'Atm	網路介面使用非同步傳輸模式 (ATM) 進行資料傳輸。
        'BasicIsdn	網路介面使用基本速率介面的整合式服務數位網路 (ISDN) 連線。 ISDN 是透過電話線的一組資料傳輸標準。
        'Ethernet	網路介面使用乙太網路連線。 乙太網路定義於 IEEE 標準 802.3 中。
        'Ethernet3Megabit	網路介面使用乙太網路 3 Mbit/秒連線。 這個乙太網路版本定義於 IETF RFC 895 中。
        'FastEthernetFx	網路介面透過光纖使用 Fast Ethernet 連線，並提供每秒 100 Mbit 的資料速率。 此類型的連線也稱為 100Base-FX。
        'FastEthernetT	網路介面透過雙絞線使用 Fast Ethernet 連線，並提供每秒 100 Mbit 的資料速率。 此類型的連線也稱為 100Base-T。
        'Fddi	網路介面使用光纖分佈資料介面 (FDDI) 連線。 FDDI 是區域網路中光纖線路上的一組資料傳輸標準。
        'GenericModem	網路介面使用數據機。
        'GigabitEthernet	網路介面使用 Gigabit Ethernet 連線，並提供每秒 1,000 Mbit (每秒 1 Gigabit) 的資料速率。
        'HighPerformanceSerialBus	網路介面使用高效能序列匯流排。
        'IPOverAtm	網路介面搭配非同步傳輸模式 (ATM) 使用網際網路通訊協定 (IP) 來進行資料傳輸。
        'Isdn	網路介面使用針對 ISDN 和 X.25 通訊協定設定的連線。 X.25 允許公用網路上的電腦使用中繼電腦進行通訊。
        'Loopback	網路介面是回送介面卡。 這類介面通常用於測試；不會透過網路傳送任何流量。
        'MultiRateSymmetricDsl	網路介面使用多速率數位用戶線路。
        'Ppp	網路介面使用點對點通訊協定 (PPP) 連線。 PPP 是使用序列裝置的資料傳輸通訊協定。
        'PrimaryIsdn	網路介面使用主要速率介面的整合式服務數位網路 (ISDN) 連線。 ISDN 是透過電話線的一組資料傳輸標準。
        'RateAdaptDsl	網路介面使用可調速率數位用戶線路 (RADSL)。
        'Slip	網路介面使用序列線路網際網路通訊協定 (SLIP) 連線。 SLIP 定義於 IETF RFC 1055 中。
        'SymmetricDsl	網路介面使用對稱式數位用戶線路 (SDSL)。
        'TokenRing	網路介面使用 Token-Ring 連線。 Token-Ring 定義於 IEEE 標準 802.5 中。
        'Tunnel	網路介面使用通道連線。
        'Unknown	介面類型是未知的。
        'VeryHighSpeedDsl	網路介面使用超高速數位用戶線路 (VDSL)。
        'Wireless80211	網路介面使用無線區域網路連線 (IEEE 802.11 標準)。
        'Wman	網路介面使用適用於 WiMax 裝置的行動式寬頻介面。
        'Wwanpp	網路介面使用適用於 GSM 型裝置的行動式寬頻介面。
        'Wwanpp2	網路介面使用適用於 CDMA 型裝置的行動式寬頻介面。
        '--------------------------------------------------------------------------------------------------------------------------
        Return ReturnString
    End Function
    Private Function GetConnectingMAC(ByVal ID As Integer) As String
        Dim ReturnString As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).GetPhysicalAddress.ToString()
        For i = 5 To 0 Step -1
            If i > 0 Then ReturnString = ReturnString.Insert(i * 2, "-")
        Next
        Return ReturnString
    End Function
    Private Function GetConnectingIPv4Address(ByVal ID As Integer) As String
        Dim ReturnString As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).GetPhysicalAddress.ToString
        Return ReturnString
    End Function
    Private Function GetConnectingIPv6Address(ByVal ID As Integer) As String
        Dim ReturnString As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).GetPhysicalAddress.ToString
        Return ReturnString
    End Function
    Private Function GetConnectingID(ByVal ID As Integer) As String
        Dim ReturnString As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).Id.ToString
        Return ReturnString
    End Function
    Private Function GetConnectingDescription(ByVal ID As Integer) As String
        Dim ReturnString As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).Description.ToString
        Return ReturnString
    End Function
    Private Function GetConnectingSpeed(ByVal ID As Integer) As String
        Dim ReturnString As String = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).Speed.ToString.ToString
        Return ReturnString
    End Function
    Private Function GetConnectingDNS(ByVal ID As Integer) As String
        Dim ReturnString As String = ""
        For i = 0 To Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).GetIPProperties.DnsAddresses.Count - 1
            Try
                ReturnString += Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).GetIPProperties.DnsAddresses.Item(i).ToString
                If i <> Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).GetIPProperties.DnsAddresses.Count - 1 Then ReturnString += ", "
            Catch ex As Exception
                ReturnString += ex.Message
            End Try
        Next
        Return ReturnString
    End Function
    Private Function GetDHCPServer(ByVal ID As Integer) As String
        Dim ReturnString As String = ""
        Try
            ReturnString = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(ID).GetIPProperties.DhcpServerAddresses.Item(0).ToString
        Catch ex As Exception
            ReturnString = "Erro"
        End Try

        Return ReturnString
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Function GetIPv4(ByVal ID As Double) As String
        Dim ReturnString As String = ""
        Try
            For i = 0 To Dns.GetHostEntry(Dns.GetHostName()).AddressList().Length - 1
                If IPv4Similer(Dns.GetHostEntry(Dns.GetHostName()).AddressList(i).ToString) = 0.75 Then ReturnString = Dns.GetHostEntry(Dns.GetHostName()).AddressList(i).ToString
            Next
        Catch ex As Exception
            ReturnString = "Erro"
        End Try
        Return ReturnString
    End Function
    Private Function IPv4Similer(ByVal IP As String) As Double
        'DHCP 的 IP 為 192.168.XXX.XXX
        Dim DHCP As String = ""
        For i = 0 To Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Count - 1
            If Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(i).GetIPProperties.DhcpServerAddresses.Count > 0 Then
                DHCP = Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(i).GetIPProperties.DhcpServerAddresses.Item(0).ToString
                Exit For
            End If
        Next
        '預設陣列為 {192, 168, 1, 1}
        Dim DHCP_str() As String
        '預設陣列為 {192, 168, 1, 1}
        Dim IP_str() As String
        Dim Compare() As Boolean = {False, False, False, False}
        Dim Percentage As Double = 0
        Try
            DHCP_str = Split(DHCP, ".")
            IP_str = Split(IP, ".")

            '相似度 25%
            If DHCP_str(0).Equals(IP_str(0)) Then Compare(0) = True
            '相似度 50%
            If DHCP_str(1).Equals(IP_str(1)) Then Compare(1) = True
            '相似度 75%
            If DHCP_str(2).Equals(IP_str(2)) Then Compare(2) = True
            '相似度 100%
            If DHCP_str(3).Equals(IP_str(3)) Then Compare(3) = True

            For i = 0 To DHCP_str.Count - 1
                If Compare(i) Then Percentage = Percentage + 0.25
                If Compare(i) = False Then Exit For
            Next
        Catch ex As Exception
            '中途遇到錯誤就回報 -1
            Percentage = -1
        End Try
        Return Percentage
    End Function

    Private Sub LinkLabel1_Click(sender As Object, e As EventArgs) Handles LinkLabel1.Click

        If MsgBox("Are you sure to open the link http://ipinfo.io ?", MsgBoxStyle.YesNo, "注意!") = MsgBoxResult.Yes Then Shell("Rundll32.exe url.dll, FileProtocolHandler http://ipinfo.io", vbNormalFocus)
    End Sub
End Class