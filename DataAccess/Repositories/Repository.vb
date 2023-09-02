Imports System.Data
Imports MySql.Data.MySqlClient

Public MustInherit Class Repository

    Private ReadOnly connectionString As String

    Public Sub New()
        connectionString = "server=localhost; user=root; password=root; database=mycompany;"
    End Sub

    Protected Function GetConnection() As MySqlConnection

        Return New MySqlConnection(connectionString)

    End Function

End Class
