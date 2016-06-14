Imports System.Data
Class MainWindow
    Dim mysql As New DB_IntegrationMethods 'ייבוא אינסטנס של מסד הנתונים
    '*************************************************************
    '*********************טיפול בכפתורים*************************
    '*************************************************************
    '<<<<כפתור הצגת השחקנים ופרטיהם על גבי חלון המשחק
    Private Sub btnFame_Click(sender As Object, e As RoutedEventArgs) Handles btnFame.Click
        Dim ds As DataSet 'טיפוס להכלת טבלאות של בסיסי נתונים
        'Dim selectall As String = "select * from suduku"
        ds = mysql.sqlQueryToDataSet("select * from suduku") 'לצורך הזנת משפטי שליפה ס.ק.ל.-ים
        For x As Integer = 0 To ds.Tables(0).Rows.Count() - 1
            'For y As Integer = 0 To ds.Tables(0).Columns.Count() - 1
            lbl1.Content &= "player: " & ds.Tables(0).Rows(x)(1).ToString() & " | " & "last level played: " & ds.Tables(0).Rows(x)(2).ToString() & " | " & ds.Tables(0).Rows(x)(3).ToString() & " puzzles solved"
            lbl1.Content &= vbCrLf
        Next
        'Next
    End Sub

    '<<<<<<כפתור יציאה מהתכנית
    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    '<<<<<כפתור הוראות המשחק
    Private Sub btnHelp_Click(sender As Object, e As RoutedEventArgs) Handles btnHelp.Click
        Dim hlp As New help
        hlp.Show()
    End Sub
    'Private Sub radio_Click(sender As Object, e As RoutedEvent ) Handles 
    '   End Sub
    Function nameCheck() As Boolean
        Dim check As Boolean = False
        If txt1.Text <> "" Or txt1.Text = " " Then
            check = True
        End If
        Return check

    End Function

    '***************כפתורי הרדיו*****************
    Dim getLevel As String
    '<<<<רמה 1- קל
    Private Sub RadioButton_checked_1(sender As Object, e As RoutedEventArgs)
        getLevel = "easy"
        ' mysql.executeSqlQuery("insert into Suduku (Best_Level) values ('easy')")
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub
    '<<<<רמה 2- בינוני
    Private Sub RadioButton_checked_2(sender As Object, e As RoutedEventArgs)
        getLevel = "medium"
        'ysql.executeSqlQuery("insert into Suduku (Best_Level) values ('medium')")
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub
    '<<<<רמה 3- קשה
    Private Sub RadioButton_checked_3(sender As Object, e As RoutedEventArgs)
        getLevel = "expert"
        btnPlay.Visibility = Windows.Visibility.Visible
    End Sub
    '<<<כפתור התחלת משחק - מוביל לחלון המשחק
    Private Sub btnPlay_Click(sender As Object, e As RoutedEventArgs) Handles btnPlay.Click
        If txt1.Text = Nothing Then
            lbl2.Content = "enter your name !"
        Else
            Dim startgame As New Game
            startgame.Show()
            Me.Close()
            ' Dim NewPlayer As String = ("Insert into suduku (Player) Values (" & txt1.Text & ")")
            'Dim insertNewPlayer = mysql.executeSqlQuery(NewPlayer)
            mysql.executeSqlQuery("Insert into Suduku (Player) Values (" & txt1.Text & ")")
        End If
    End Sub
End Class
