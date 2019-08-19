Imports System.Windows.Threading 
Imports System.Data
Public Class Game
    Dim timer As New DispatcherTimer
    Dim timeCounter As Integer = 0
    Public Shared timeScore As Integer
    Public Shared hrs As Integer
    Public Shared mins As Integer
    Public Shared secs As Integer
    Sub timershow()
        hrs = Int((timeCounter \ 3600)) 
        mins = (timeCounter - (hrs * 3600)) \ 60 
        secs = timeCounter Mod 60 
        Dim hrsStr As String = hrs
        Dim minsStr As String = mins
        Dim secsStr As String = secs
        If hrs < 10 Then
            hrsStr = "0" & hrs
        End If
        If mins < 10 Then
            minsStr = "0" & mins
        End If
        If secs < 10 Then
            secsStr = "0" & secs
        End If
        timeCounter = timeCounter + 1
        lblTimer.Content = hrsStr & ":" & minsStr & ":" & secsStr
    End Sub

    Private Sub resetTimer()
        lblTimer.Content = "00:00:00"
        timeCounter = 0
    End Sub
  
    'Board Design
    Private Sub TextChange(sender As Object, e As TextChangedEventArgs)
        If IsNumeric(sender.text) <> True Then
            sender.text = Nothing
        ElseIf sender.text = 0 Then
            sender.text = Nothing
        End If
    End Sub

    'Matriz initialization
    Dim cell(8, 8) As TextBox   
    Dim grid(8, 8) As String '
    Dim redorblack As Boolean = False  
    Public Shared solver As Boolean = False

    'Create NNew board game
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        resetTimer()
        timer.Interval = TimeSpan.FromMilliseconds(1000) 
        AddHandler timer.Tick, AddressOf timershow
        timer.Start()
        For x As Integer = 0 To 8
            For y As Integer = 0 To 8
        'cell design
                cell(x, y) = New TextBox 
                cell(x, y).Text = Nothing  
                cell(x, y).Width = 20 
                cell(x, y).Height = 20 
                cell(x, y).MaxLength = 1 
                cell(x, y).TextAlignment = TextAlignment.Center 
                cell(x, y).FontWeight = FontWeights.Bold 
                Canvas.SetTop(cell(x, y), x * 20) 
                Canvas.SetLeft(cell(x, y), y * 20)

            
                If x > 2 Then
                    Canvas.SetTop(cell(x, y), Canvas.GetTop(cell(x, y)) + 5)
                End If
                If x > 5 Then
                    Canvas.SetTop(cell(x, y), Canvas.GetTop(cell(x, y)) + 5)
                End If
                If y > 2 Then
                    Canvas.SetLeft(cell(x, y), Canvas.GetLeft(cell(x, y)) + 5)
                End If
                If y > 5 Then
                    Canvas.SetLeft(cell(x, y), Canvas.GetLeft(cell(x, y)) + 5)
                End If
                AddHandler cell(x, y).TextChanged, AddressOf TextChange
                board.Children.Add(cell(x, y))
            Next
        Next 

    'use "auto solve" function 
        solver = True
        For x As Integer = 0 To 8
            For y As Integer = 0 To 8
                grid(x, y) = cell(x, y).Text
            Next
        Next
        solve(0, 0) !!
        For x = 0 To 8
            For y = 0 To 8
                cell(x, y).Text = grid(x, y)
                cell(x, y).IsReadOnly = True 
                cell(x, y).Background = Brushes.LightGray
                ' Dim rndX As Integer = rndMinmax(0, 8)
                '  Dim rndY As Integer = rndMinmax(0, 8)
                ' cell(rndX, rndY).Text = Nothing
                '  cell(rndX, rndY).IsReadOnly = False
                '   cell(rndX, rndY).Background = Brushes.White
                cell(1, 8).Text = Nothing
                cell(1, 8).IsReadOnly = False
                cell(1, 8).Background = Brushes.White

            Next
        Next
        solver = False
    End Sub

'Randomize numbers onto the board
Function rndMinmax(min As Integer, max As Integer) As Integer
        Randomize()
        Dim num As Integer = Int((max - min + 1) * Rnd()) + min
        Return num
    End Function

'checker functions

     Function check_rows(ByVal xVal, ByVal yVal) As Boolean
        Dim noclash As Boolean = True 
        For x As Integer = 0 To 8
            If grid(x, yVal) <> Nothing Then
                If x <> xVal Then 
                    If grid(x, yVal) = grid(xVal, yVal) Then
                        noclash = False
                    End If
                End If
            End If
        Next
        Return noclash 
    End Function

    Function check_columns(ByVal xVal, ByVal yVal) As Boolean
        Dim noclash As Boolean = True
        For y As Integer = 0 To 8
            If grid(xVal, y) <> Nothing Then
                If y <> yVal Then
                    If grid(xVal, y) = grid(xVal, yVal) Then
                        noclash = False
                    End If
                End If
            End If
        Next
        Return noclash
    End Function

    Function check_box(ByVal xVal, ByVal yVal) As Boolean
        Dim noclash As Boolean = True
        Dim xstart As Integer 
        Dim ystart As Integer

        If xVal < 3 Then
            xstart = 0
        ElseIf xVal < 6 Then
            xstart = 3
        Else
            xstart = 6
        End If
        If yVal < 3 Then
            ystart = 0
        ElseIf yVal < 6 Then
            ystart = 3
        Else
            ystart = 6
        End If

        For y As Integer = ystart To (ystart + 2)
            For x As Integer = xstart To (xstart + 2)
                If grid(x, y) <> Nothing Then
                    If Not (x = xVal And y = yVal) Then
                        If grid(x, y) = grid(xVal, yVal) Then
                            noclash = False
                        End If
                    End If
                End If
            Next
        Next
        Return noclash
    End Function


    Function solve(ByVal x As Integer, ByVal y As Integer) As Boolean
        Dim numbers As Integer = 1
        If grid(x, y) = Nothing Then
            Do
                grid(x, y) = CStr(numbers) 
                If check_rows(x, y) Then
                    If check_columns(x, y) Then  
                        If check_box(x, y) Then 
                            y = y + 1 
                            If y = 9 Then 
                                y = 0 
                                x = x + 1 
                                If x = 9 Then Return True 
                            End If
                            If solve(x, y) Then Return True
                            y = y - 1
                            If y < 0 Then
                                y = 8
                                x = x - 1
                            End If
                        End If
                    End If
                End If
                numbers = numbers + 1
            Loop Until numbers = 10
            grid(x, y) = Nothing
            Return False
        Else
            y = y + 1
            If y = 9 Then
                y = 0
                x = x + 1
                If x = 9 Then Return True
            End If
            Return solve(x, y)
        End If
    End Function

' Board Buttons

'show_instructions button
Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles btnHelp.Click
        Dim hlp As New help
        hlp.Show()
    End Sub

'clean board button
Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim rushure As MsgBoxResult = MessageBox.Show("Do you really want to clear the Board?", "clear", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If rushure = vbYes Then 
            For x As Integer = 0 To 8
                For y As Integer = 0 To 8
                    If cell(x, y).IsReadOnly = False Then 
                        cell(x, y).Text = Nothing
                        cell(x, y).Background = Brushes.White
                    End If
                Next
            Next
        End If
    End Sub

'show solution button
    Private Sub btnSolve_Click(sender As Object, e As RoutedEventArgs) Handles btnSolve.Click
        solver = True
        For x As Integer = 0 To 8
            For y As Integer = 0 To 8
                grid(x, y) = cell(x, y).Text
            Next
        Next
        solve(0, 0) 
        For x = 0 To 8
            For y = 0 To 8
                cell(x, y).Text = grid(x, y)
                cell(x, y).IsReadOnly = True 'שלילת האפשרות לשכתוב התאים בלוח
            Next
        Next
        solver = False

        btnClear.Visibility = Windows.Visibility.Hidden
        btnCheck.Visibility = Windows.Visibility.Hidden
        btnSolve.Visibility = Windows.Visibility.Hidden

        lblScore.Visibility = Windows.Visibility.Visible
        lblScore.Content = "You used the SOLVE button!" & vbCrLf & "Your get NO points"
        timer.Stop()
    End Sub

'Exit button
    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnMenu.Click
        Dim rushure As MsgBoxResult = MessageBox.Show("Are you sure you want to EXIT to main menu?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If rushure = vbYes Then
            Dim startgame = New MainWindow()
            startgame.Show()
            Me.Close()
        End If
    End Sub

    'btnCheck_click1     
    Private Sub btnCheck_Click1(sender As Object, e As RoutedEventArgs) Handles btnCheck.Click
        For x = 0 To 8
            For y = 0 To 8
                grid(x, y) = cell(x, y).Text
                cell(x, y).Foreground = Brushes.Black  
                lblScore.Visibility = Windows.Visibility.Hidden
            Next
        Next
        For x = 0 To 8
            For y = 0 To 8
                If check_rows(x, y) Then
                    If check_columns(x, y) Then
                        If Not check_box(x, y) Then
                            cell(x, y).Foreground = Brushes.Red
                            solver = False
                        End If
                    Else
                        cell(x, y).Foreground = Brushes.Red
                        solver = False
                    End If
                Else
                    cell(x, y).Foreground = Brushes.Red
                    solver = False
                End If
            Next
        Next
        If solver = True Then
            timeScore = (hrs * 10000) + (mins * 100) + secs
            MsgBox("You Win!")
            timer.Stop()
        Else
            MsgBox("fill in the correct numbers")
        End If

'DB updates
        Dim sqlUpdate As String 
        Dim dsName As DataSet 
        Dim dsMaxTime As DataSet 
        Dim dsId As DataSet  
        Dim dsTimebyid As DataSet 
        Dim mysql As New DB_IntegrationMethods

        dsName = mysql.sqlQueryToDataSet("select count (PName) from " & MainWindow.getlevel & " where PName = '" & MainWindow.playerName & "'")
        dsMaxTime = mysql.sqlQueryToDataSet("select max(PTime) from " & MainWindow.getlevel)

        If dsMaxTime.Tables(0).Rows(0)(0) >= timeScore Then
            If dsName.Tables(0).Rows(0)(0) = 0 Then
                sqlUpdate = "update " & MainWindow.getlevel & " set PName = '" & MainWindow.playerName & "', PTime = '" & timeScore & "' where PTime = '" & dsMaxTime.Tables(0).Rows(0)(0) & "'"
                mysql.executeSqlQuery(sqlUpdate)
            End If
        Else
            dsId = mysql.sqlQueryToDataSet("select id from " & MainWindow.getlevel & " where PName = '" & MainWindow.playerName & "'") ' מקבל את ה ID של השם 
            dsTimebyid = mysql.sqlQueryToDataSet("select PTime from " & MainWindow.getlevel & " where id = " & dsId.Tables(0).Rows(0)(0))
            If dsTimebyid.Tables(0).Rows(0)(0) >= timeCounter Then
                sqlUpdate = "update " & MainWindow.getlevel & " set PTime = '" & timeCounter & "' where PTime = '" & dsTimebyid.Tables(0).Rows(0)(0) & "'"
                mysql.executeSqlQuery(sqlUpdate)
            End If
        End If
    End Sub
End Class
