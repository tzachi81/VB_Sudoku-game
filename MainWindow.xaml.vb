Imports System.Data
Class MainWindow
    Public Shared mysql = New DB_IntegrationMethods 'ייבוא אינסטנס משותף של טיפול במסד הנתונים
    Public Shared playerName As String  ' משתנה  ---משותף לכל החלונות בפרויקט --- הקולט את שם השחקן המוקלד בתיבת הטקסט 
    Public Shared getlevel As String  'משתנה  ---משותף לכל החלונות בפרויקט --- הקולט את רמת הקושי שבחר השחקן מכפתורי הרדיו
    '*****************************************************************************************************************
    'פונקצייה לבדיקת תקינות ההקלדה של שם השחקן בתיבת הטקסט
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
    '*********************טיפול בכפתורים*************************

    '<<<<כפתור הצגת השחקנים ופרטיהם על גבי חלון המשחק
    Dim ds As DataSet 'הגדרת טיפוס להכלת טבלאות של בסיסי נתונים
    Private Sub btnFame_Click(sender As Object, e As RoutedEventArgs) Handles btnFame.Click
        If MainWindow.getlevel = Nothing Then
            lblMsg.Content = "first choose a level"
        Else
            lblMsg.Content = ""
            Dim fameshow As New HallofFame()
            fameshow.Show()
        End If
    End Sub

    '<<<<<<כפתור יציאה מהתכנית
    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Dim rushure2 As MsgBoxResult = MessageBox.Show("Are you sure you want to EXIT?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If rushure2 = vbYes Then
            Me.Close()
        End If
    End Sub

    '<<<<<כפתור הוראות המשחק
    Private Sub btnHelp_Click(sender As Object, e As RoutedEventArgs) Handles btnHelp.Click
        Dim hlp As New help
        hlp.Show()
    End Sub

    '***************כפתורי הרדיו*****************
    '<<<<רמה 1- קל
    Private Sub RadioButton_checked_1(sender As Object, e As RoutedEventArgs)
        getLevel = "easy"
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub
    '<<<<רמה 2- בינוני
    Private Sub RadioButton_checked_2(sender As Object, e As RoutedEventArgs)
        getLevel = "medium"
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub
    '<<<<רמה 3- קשה
    Private Sub RadioButton_checked_3(sender As Object, e As RoutedEventArgs)
        getLevel = "hard"
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub

    '<<<כפתור התחלת משחק - מוביל לחלון המשחק
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
