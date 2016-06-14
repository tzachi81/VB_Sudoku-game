Imports System.Data
Public Class HallofFame
    Dim mysql = New DB_IntegrationMethods

    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    Public Sub HallofFame_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim ds = mysql.sqlQueryToDataSet("select * from " & MainWindow.getlevel & " order by pTime Desc") 'לצורך הזנת משפטי שליפה 
        lblFame.Content &= vbCrLf
        For x As Integer = 0 To ds.Tables(0).Rows.Count() - 1
            lblFame.Content &= ds.Tables(0).Rows(x)(1).ToString() & "                                  " & ds.Tables(0).Rows(x)(2).ToString() & ""
            lblFame.Content &= vbCrLf
        Next
    End Sub
End Class
