Imports System.IO
Imports System.Media
Imports System.Reflection
Imports HandyControl
Imports IniParser
Imports IniParser.Model
Imports System.Resources
Imports System.Windows.Threading

Class MainWindow
    Dim sp As SoundPlayer = New SoundPlayer(My.Resources.bgm)
    Dim sp2 As SoundPlayer = New SoundPlayer(My.Resources.bgm_00_00_10_00_00_25)
    Private Sub Window_SizeChanged(sender As Object, e As SizeChangedEventArgs)
        text1.FontSize = Me.Width / 10
    End Sub

    Private Sub Window_Initialized(sender As Object, e As EventArgs)
        If File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) & "\1.txt") Then
            HandyControl.Controls.MessageBox.Show("执行完成！", "DONE", MessageBoxButton.OK, MessageBoxImage.Information)
            File.Delete(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) & "\1.txt")
            End
        End If
        If File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) & "\2.txt") Then
            Try
                Dim Reg As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                Reg.SetValue("XBproplusmax.exe", Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) & "\XBproplusmax.exe")         '写入注册表，其中SCMS.exe，就是你需要启动的exe文件
                Reg.Close()
                HandyControl.Controls.MessageBox.Show("设置成功", "DONE", MessageBoxButton.OK, MessageBoxImage.Information)
                File.Delete(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) & "\2.txt")
                End
            Catch ex As Exception
                HandyControl.Controls.MessageBox.Show("设置失败", "DONE", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try


        End If
        If Not File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) & "\config.ini") Then
            HandyControl.Controls.MessageBox.Show("配置文件好吃吗？快吐出来！", "错误", MessageBoxButton.OK, MessageBoxImage.Error)
            End
        End If


        Try

            Dim parser = New FileIniDataParser()
            Dim config As IniData = parser.ReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) & "\config.ini")
            If config("config")("launchmode") = "1" Then
                Me.Height = SystemParameters.PrimaryScreenHeight
                Me.Width = SystemParameters.PrimaryScreenWidth
                Me.Top = 0
                Me.Left = 0
            Else
                Me.Topmost = False
                Me.ResizeMode = ResizeMode.CanResizeWithGrip
                Me.WindowStyle = WindowStyle.SingleBorderWindow
                Me.Height = 720
                Me.Width = 1280
            End If
            Dim dt_now As Date = Date.Now
            Dim dt_to As Date = New DateTime(config("config")("year"), config("config")("month"), config("config")("day"))
            If config("config")("onlytextmode") = "1" Then
                text1.Text = config("config")("text").ToString
            Else

                If dt_now.Date = dt_to.Date Then
                    text1.Text = "是日" & vbCrLf & config("config")("text").ToString
                ElseIf dt_now > dt_to Then
                    text1.Text = "距" & config("config")("text").ToString & vbCrLf & "已经过去" & (dt_now - dt_to).Days + 1 & "天"
                Else
                    text1.Text = "距" & config("config")("text").ToString & vbCrLf & "还有" & (dt_to - dt_now).Days + 1 & "天"
                End If
            End If


            If config("config")("automusic") = "1" Then
                sp.PlayLooping()
                bt_music.Content = "关闭音乐"
            End If
            If config("config")("automusic") = "2" Then
                sp2.Play()
            End If

            If Not config("config")("autoclose") = 0 Then
                Dim dt As DispatcherTimer = New DispatcherTimer()
                AddHandler dt.Tick, AddressOf dispatcherTimer_Tick

                dt.Interval = New TimeSpan(0, 0, Int(config("config")("autoclose")))
                dt.Start()
            End If
        Catch ex As Exception
            HandyControl.Controls.MessageBox.Show("配置文件出错！", "错误", MessageBoxButton.OK, MessageBoxImage.Error)
            End
        End Try

    End Sub

    Private Sub dispatcherTimer_Tick(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim i = HandyControl.Controls.MessageBox.Show("真的要退出嘛？", "退出？", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If i = 6 Then
            End
        End If
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Try
            Dim parser = New FileIniDataParser()
            Dim config As IniData = parser.ReadFile(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) & "\config.ini")
            Dim dt_now As Date = Date.Now
            Dim dt_to As Date = New DateTime(config("config")("year"), config("config")("month"), config("config")("day"))
            text1.Text = "距" & config("config")("text").ToString & vbCrLf & "还有" & (dt_to - dt_now).Days + 1 & "天"
        Catch ex As Exception
            HandyControl.Controls.MessageBox.Show("配置文件出错！", "错误", MessageBoxButton.OK, MessageBoxImage.Error)
            End
        End Try
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        If bt_music.Content = "播放音乐" Then
            sp.PlayLooping()
            bt_music.Content = "关闭音乐"
        Else
            sp.Stop()
            bt_music.Content = "播放音乐"
        End If

    End Sub

    Private Sub Button_Click_3(sender As Object, e As RoutedEventArgs)
        HandyControl.Controls.MessageBox.Show("DayCounter 喜报-Pro Plus Max V1.0" & vbCrLf & "Power by idk", "INFO", MessageBoxButton.OK, MessageBoxImage.Information)
    End Sub

    Private Sub Button_Click_4(sender As Object, e As RoutedEventArgs)

        HandyControl.Controls.MessageBox.Show("so sorrrrrrrrrrrrrrry" & vbCrLf & "没空写", "INFO", MessageBoxButton.OK, MessageBoxImage.Information)
    End Sub

    Private Sub Button_Click_5(sender As Object, e As RoutedEventArgs)
        Me.WindowState = 1
    End Sub

    Private Sub Button_Click_6(sender As Object, e As RoutedEventArgs)
        Me.Topmost = False
        Me.ResizeMode = ResizeMode.CanResizeWithGrip
        Me.WindowStyle = WindowStyle.SingleBorderWindow
        Me.Height = 720
        Me.Width = 1280
    End Sub

    Private Sub Button_Click_7(sender As Object, e As RoutedEventArgs)
        Me.Topmost = True
        Me.ResizeMode = ResizeMode.NoResize
        Me.WindowStyle = WindowStyle.None
        Me.Height = SystemParameters.PrimaryScreenHeight
        Me.Width = SystemParameters.PrimaryScreenWidth
        Me.Top = 0
        Me.Left = 0
    End Sub
End Class
