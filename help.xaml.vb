

Public Class help
    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnVideolink_Click(sender As Object, e As RoutedEventArgs) Handles btnVideolink.Click
        System.Diagnostics.Process.Start("http://www.youtube.com/watch?v=OtKxtvMUahA")
    End Sub
End Class
