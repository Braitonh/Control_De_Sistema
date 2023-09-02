Imports System.Data
Imports System.Data.SqlClient

Public MustInherit Class Repository

    Private ReadOnly connectionString As String

    Public Sub New()
        connectionString = "server=localhost; user=root; password=root; database=mycompany;"
    End Sub

    Protected Function GetConnection() As SqlConnection

        Return New SqlConnection(connectionString)

    End Function

End Class
