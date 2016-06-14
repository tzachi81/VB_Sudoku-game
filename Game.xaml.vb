Imports System.Windows.Threading 'אינסטנס בשביל הטיימר
Imports System.Data
Public Class Game
    '********טיפול בטיימר**************
    Dim timer As New DispatcherTimer
    Dim timeCounter As Integer = 0
    Public Shared timeScore As Integer
    Public Shared hrs As Integer
    Public Shared mins As Integer
    Public Shared secs As Integer
    Sub timershow()
        hrs = Int((timeCounter \ 3600)) 'לחישוב מונה השעוצ
        mins = (timeCounter - (hrs * 3600)) \ 60 'לחישוב מונה הדקות
        secs = timeCounter Mod 60 'לחיושב מונה השניות
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
    '******************************************************

    '****************חלק א' - עיצוב הלוח*****************
    '******************************************************

    '*******טיפול בהקלדת תו השונה מספרה ,במקרה של הקלדת הספרה  אפס או רווח************
    Private Sub TextChange(sender As Object, e As TextChangedEventArgs)
        If IsNumeric(sender.text) <> True Then
            sender.text = Nothing
        ElseIf sender.text = 0 Then
            sender.text = Nothing
        End If
    End Sub

    '*****הגדרת משתנים ראשיים******
    Dim cell(8, 8) As TextBox  'הגדרת תיבות טקסט שנציב במטריצה (גלובלית), 9 על 9 של טקסטבוקסים 
    Dim grid(8, 8) As String 'הגדרת מטריצה 'פיקטיבית' שעליה נבצע את ההשוואה למטריצה המקורית והבדיקות של לוח המשחק כולל סימון טעויות באדום וכו'
    Dim redorblack As Boolean = False 'הגדרת משתנה בוליאני שיעזור לנו לצבוע ספרה לא נכונה שהוקלדה 
    Public Shared solver As Boolean = False


    '*************************************************************
    '******כאן נטפל בעיצוב לוח משחק חדש בעת "טעינת" לוח משחק*****
    '*************************************************************
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        resetTimer()
        timer.Interval = TimeSpan.FromMilliseconds(1000) 'מחזור שעון בן שניה אחת
        AddHandler timer.Tick, AddressOf timershow
        timer.Start()
        ' ניצור ונעצב  את מטריצת הטקסטבוקסים עבור כל אחד מהתאים שהגדרנו במשתנה CELL
        For x As Integer = 0 To 8
            For y As Integer = 0 To 8
                '>>>  הגדרות בסיס לעיצוב תוכן התאים<<<
                cell(x, y) = New TextBox 'הגדרת סוג תא במטריצה כאובייקט  טקסטבוקס
                cell(x, y).Text = Nothing  'איפוס טקסטבוקס
                cell(x, y).Width = 20 'הגבלת רוחב בפיקלסים
                cell(x, y).Height = 20 'הגבלת גובה בפיקסלים
                cell(x, y).MaxLength = 1 'הגבלה לתו בודד בכל טקסטבוקס
                cell(x, y).TextAlignment = TextAlignment.Center 'מרכוז התו הנכתב בתא
                cell(x, y).FontWeight = FontWeights.Bold 'תו יוצג כמודגש
                Canvas.SetTop(cell(x, y), x * 20) ' קביעת המרחק בין כל תא ותא
                Canvas.SetLeft(cell(x, y), y * 20)

                '*****************************************************************************************************
                'קביעת מיקום המרווחים לשם יצירת 9 מטריצות 'קטנות' בגודל 3 על 3 כל אחת
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
                'הצגת האובייקט טקסטבוקס על תכונותיו, במסך
                AddHandler cell(x, y).TextChanged, AddressOf TextChange
                board.Children.Add(cell(x, y))
            Next
        Next '<<<עד כאן הצבת תיבות הטקסט על גבי המסך

        '****************יצירת לוח משחק ************
        '*****בחלק זה השתמשנו בפונקציית הפתרון האוטומטי   '(SOLVE) 
        solver = True
        'קודם כל כל הלוח מתמלא במספרים כמו שעושה השגרה של כפתור 'פתרון אוטומטי'
        For x As Integer = 0 To 8
            For y As Integer = 0 To 8
                grid(x, y) = cell(x, y).Text
            Next
        Next
        solve(0, 0) ' זה חשוב - להתחיל לבדוק תמיד מהתא הראשון במטריצה!!
        For x = 0 To 8
            For y = 0 To 8
                cell(x, y).Text = grid(x, y)
                cell(x, y).IsReadOnly = True 'שלילת האפשרות לשכתוב התאים בלוח
                cell(x, y).Background = Brushes.LightGray
                'ורק אחרי שהלוח מלא בערכים - תאים רנדומליים הופכים לריקים ובעלי אפשרות כתיבה, כמובן, כך:
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
        '*****עד כאן יצירת לוח המשחק******
    End Sub

    'פונקציה להגרלת מספרים אקראיים לפי הגדרה - נשתמש בזה בהתאם לרמת הקושי שנבחרה
    Function rndMinmax(min As Integer, max As Integer) As Integer
        Randomize()
        Dim num As Integer = Int((max - min + 1) * Rnd()) + min
        Return num
    End Function

    '********************************סוף חלק א' - עיצוב**************************
    '*****************************************************************************************

    '******* חלק ב' - בדיקת לוח המשחק מכפילויות, טיפול בכפתור 'נקה לוח משחק'  *******
    '************פונקציות בדיקה לכפילויות מספרים**********
    '***השתמשנו בשלוש הפונקציות הבאות עם משתנה בוליאני מכיוון שערך התא יענה לנו על אחד מהשניים:
    '****המספר כפול או שהמספר בעל מופע יחיד<<<<:

    '********* פונקציה זו בודקת  אם יש מספר כפול בכל אחת מהשורות check_rows******
    Function check_rows(ByVal xVal, ByVal yVal) As Boolean
        Dim noclash As Boolean = True '<<< המשתנה הזה נתון בכל אחת מהפונקציות בכוונה כדי שהפונקציה האחרת לא תשנה אותו בטעות עבור הפונקציה הקודמת לה
        For x As Integer = 0 To 8
            If grid(x, yVal) <> Nothing Then
                If x <> xVal Then 'השוואה עם כל ערך 'שורתי' בנפרד
                    If grid(x, yVal) = grid(xVal, yVal) Then
                        noclash = False
                    End If
                End If
            End If
        Next
        Return noclash '<-- מחזיר את התשובה לשאלה: האם המספר כפול במטריצה או לא
    End Function

    '*****פונקציה זו בודקת  אם יש מספר כפול בכל אחד מהטורים check_columns*****
    Function check_columns(ByVal xVal, ByVal yVal) As Boolean
        Dim noclash As Boolean = True
        For y As Integer = 0 To 8
            If grid(xVal, y) <> Nothing Then
                If y <> yVal Then 'השוואה עם כל ערך 'טורי' בנפרד
                    If grid(xVal, y) = grid(xVal, yVal) Then
                        noclash = False
                    End If
                End If
            End If
        Next
        Return noclash
    End Function

    '********* פונקציה זו בודקת כפילות בכל אחת מהמטריצות הקטנות: (3 על 3) בלוח המשחק - שימו לב לחלוקה הראשונית במשפטי 'אם check_box******
    Function check_box(ByVal xVal, ByVal yVal) As Boolean
        Dim noclash As Boolean = True
        Dim xstart As Integer 'לבדוק אפשרות הצבת הערך 0 למשתנה
        Dim ystart As Integer
        'שימו לב לתנאים עם ההפרדות בין מטריצה למטריצה - דומה לחלק א' של התוכנית
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
                    If Not (x = xVal And y = yVal) Then ' נפתר הודות למדריך ביוטיוב:  בהתחלה לא התנינו על דרך השלילה והבדיקה לא עבדה כלל!!!
                        If grid(x, y) = grid(xVal, yVal) Then
                            noclash = False
                        End If
                    End If
                End If
            Next
        Next
        Return noclash
    End Function

    '***************פונקצית עזר לכפתור פתרון אוטומטי*****************

    Function solve(ByVal x As Integer, ByVal y As Integer) As Boolean
        'ניצור משתנה עבור כל מספר שייבדק:
        Dim numbers As Integer = 1
        'המתודה: אם התא ריק, אז תמלא אותו במספר הבא כל עוד הוא לא יוצר התנגשות עם מספר דומה בשורה, טור או מטריצה קטנה
        If grid(x, y) = Nothing Then
            Do
                grid(x, y) = CStr(numbers) ' המרה לסוג מחרוזת CStr: ' 
                If check_rows(x, y) Then ' אם אין כפילות בשורות
                    If check_columns(x, y) Then  ' אם אין כפילות בעמודות
                        If check_box(x, y) Then ' אם אין כפילות במטריצות הקטנות
                            y = y + 1 ' עבור לטור הבא
                            If y = 9 Then ' אבל אם הגעת לטור העשירי, כלומר השורה הבאה
                                y = 0 ' אפס את מונה הטור , תתחיל מטור מספר אחד
                                x = x + 1 '...של השורה הבאה( קידום מונה השורות)
                                If x = 9 Then Return True 'שורה זו מורה לנו על סוף המטריצה
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
                'התנאי: בצע הבדיקות עד שהצובר יגיע לערך 10 - מכיוון שלא ניתן להציב מספר מעל  9 בסודוקו
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

    '<****************************>
    '<*****סוף פונקציות בדיקה****>
    '<****************************>

    '********************************
    '********חלק ג' - כפתורים********
    '********************************

    '**************כפתור הצגת הוראות המשחק************
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles btnHelp.Click
        Dim hlp As New help
        hlp.Show()
    End Sub

    '****************כפתור נקה לוח'****************' 
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim rushure As MsgBoxResult = MessageBox.Show("Do you really want to clear the Board?", "clear", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If rushure = vbYes Then 'פעולת ניקוי הלוח מתבצעת באם המשתמש ענה בחיוב
            For x As Integer = 0 To 8
                For y As Integer = 0 To 8
                    If cell(x, y).IsReadOnly = False Then ' כדי להמנע ממחיקת המספרים הקבועים על גבי  הלוח
                        cell(x, y).Text = Nothing
                        cell(x, y).Background = Brushes.White
                    End If
                Next
            Next
        End If
    End Sub

    '*******************כפתור הצג פתרון****************
    'משתמש בפונקציית  SOLVER שממלאת את המספרים הנותרים / החסרים
    Private Sub btnSolve_Click(sender As Object, e As RoutedEventArgs) Handles btnSolve.Click
        solver = True
        For x As Integer = 0 To 8
            For y As Integer = 0 To 8
                grid(x, y) = cell(x, y).Text
            Next
        Next
        solve(0, 0) 'להתחיל (לבדוק ו-) לפתור מהתא הראשון במטריצה
        For x = 0 To 8
            For y = 0 To 8
                cell(x, y).Text = grid(x, y)
                cell(x, y).IsReadOnly = True 'שלילת האפשרות לשכתוב התאים בלוח
            Next
        Next
        solver = False

        'העלמת כל כפתורי הלוח
        btnClear.Visibility = Windows.Visibility.Hidden
        btnCheck.Visibility = Windows.Visibility.Hidden
        btnSolve.Visibility = Windows.Visibility.Hidden
        'הצגת ניקוד
        lblScore.Visibility = Windows.Visibility.Visible
        lblScore.Content = "You used the SOLVE button!" & vbCrLf & "Your get NO points"
        timer.Stop()
        'להוסיף כאן עדכון של DB***************
    End Sub
    '**************כפתור יציאה מהתכנית************
    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnMenu.Click
        Dim rushure As MsgBoxResult = MessageBox.Show("Are you sure you want to EXIT to main menu?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If rushure = vbYes Then
            Dim startgame = New MainWindow()
            startgame.Show()
            Me.Close()
        End If
    End Sub

    '******************************************************************************************
    'btnCheck_click1  השגרה
    'בודקת קודם כל אם היה שינוי בתא מסוים- ראו השוואה עם משתנה 'גריד'. אם המספר הוא כפול בתוך שורה ו/או בעמודה - ייצבע באדום
    'השגרה מתבססת על שלוש הפונקציות הבאות שנמצאות מעלה והן:
    'check_rows; check_columns;check_box
    '******************************************************************************************
    Private Sub btnCheck_Click1(sender As Object, e As RoutedEventArgs) Handles btnCheck.Click
        For x = 0 To 8
            For y = 0 To 8
                grid(x, y) = cell(x, y).Text
                cell(x, y).Foreground = Brushes.Black  ' קביעת צבע הפונט כ-שחור, במצב ברירת מחדל
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

        '*************טיפול בעדכון בסיס הנתונים בטבלה הרלבנטית**************
        Dim sqlUpdate As String 'מחרוזת להשמת משפט העדכון הרלבנטי
        Dim dsName As DataSet 'קריאה לרשומה בטבלה שמחזירה שם שחקן לפי תנאי
        Dim dsMaxTime As DataSet 'קריאה לרשומה בטבלה שמחזירה זמן מירבי ששוחק  לפי תנאי
        Dim dsId As DataSet 'קריאה לרשומה בטבלה שמחזירה  מספר רשומה של שם השחקן 
        Dim dsTimebyid As DataSet ' זמן מקבל את התוצאה לפי ערך הID
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
