Imports System.Data
Public Class Top10
    Dim mysql As New DB_IntegrationMethods 'ייבוא אינסטנס של מסד הנתונים

    Private Sub Top10_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Dim ds As DataSet 'טיפוס להכלת טבלאות של בסיסי נתונים
        For x As Integer = 0 To ds.Tables(0).Rows.Count() - 1
            lbl1.Content &= "player: " & ds.Tables(0).Rows(x)(1).ToString() & " | " & "last level played: " & ds.Tables(0).Rows(x)(2).ToString() & " | " & ds.Tables(0).Rows(x)(3).ToString() & " puzzles solved"
            lbl1.Content &= vbCrLf
        Next
    End Sub

End Class
