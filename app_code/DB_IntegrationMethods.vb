Imports System.Data

Public Class DB_IntegrationMethods

    Dim sqlSource As String = "app_data\sudukuDB.accdb" & ";"
    Dim sqlConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & sqlSource & "User ID=Admin;Password="
    ''' <summary>
    ''' Executes a Select SQL statement and retreives the results in a dataset structure
    ''' </summary>
    ''' <param name="sqlQ">enter an SQL statement</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function sqlQueryToDataSet(ByVal sqlQ As String) As DataSet
        Dim conn As New OleDb.OleDbConnection(sqlConnectionString)
        Dim dAdapter As New OleDb.OleDbDataAdapter(sqlQ, conn)
        Dim ds As New DataSet
        Dim s As String = sqlQ
        conn.Open()
        dAdapter.Fill(ds)
        conn.Close()

        Return ds
    End Function
    ''' <summary>
    ''' Executes an Insert/Update/Detete SQL statement
    ''' </summary>
    ''' <param name="sqlQ">enter an SQL statement</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function executeSqlQuery(ByVal sqlQ As String) As Boolean
        Dim conn As New OleDb.OleDbConnection(sqlConnectionString)
        Dim command As New OleDb.OleDbCommand(sqlQ, conn)
        command = New OleDb.OleDbCommand(sqlQ, conn)
        conn.Open()
        Dim answer As Boolean = False
        Try
            command.ExecuteNonQuery()
            answer = True
        Catch ex As Exception
            answer = False
        End Try
        conn.Close()
        Return answer
    End Function
End Class
