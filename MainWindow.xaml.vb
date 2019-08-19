Imports System.Data
Class MainWindow
    Public Shared mysql = New DB_IntegrationMethods 
    Public Shared playerName As String  
    Public Shared getlevel As String 
    
    Function nameCheck() As Boolean
        playerName = name.Text
        Dim check As Boolean = False
        If playerName <> Nothing Then
            check = True
        Else
            check = False
        End If
        Return check
    End Function

    Dim ds As DataSet 
    Private Sub btnFame_Click(sender As Object, e As RoutedEventArgs) Handles btnFame.Click
        If MainWindow.getlevel = Nothing Then
            lblMsg.Content = "first choose a level"
        Else
            lblMsg.Content = ""
            Dim fameshow As New HallofFame()
            fameshow.Show()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Dim rushure2 As MsgBoxResult = MessageBox.Show("Are you sure you want to EXIT?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If rushure2 = vbYes Then
            Me.Close()
        End If
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As RoutedEventArgs) Handles btnHelp.Click
        Dim hlp As New help
        hlp.Show()
    End Sub

    'Level 1 button
    Private Sub RadioButton_checked_1(sender As Object, e As RoutedEventArgs)
        getLevel = "easy"
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub
    'Level 2 button
    Private Sub RadioButton_checked_2(sender As Object, e As RoutedEventArgs)
        getLevel = "medium"
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub
    'Level 3 button
    Private Sub RadioButton_checked_3(sender As Object, e As RoutedEventArgs)
        getLevel = "hard"
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub

    'Begin game button
    Private Sub btnPlay_Click(sender As Object, e As RoutedEventArgs) Handles btnPlay.Click
        If nameCheck() = True Then
            Dim startgame As New Game
            startgame.Show()
            Me.Close()
        Else
            nameError.Content = "enter your name !"
        End If
    End Sub
End Class
