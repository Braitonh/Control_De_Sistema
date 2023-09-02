Imports System.Data
Imports MySql.Data.MySqlClient
Public MustInherit Class MasterRepository

    Inherits Repository

    Protected parameters As List(Of MySqlParameter)
    Protected Function ExecuteNonQuery(transactSql As String) As Integer

        Using connection = GetConnection()

            connection.Open()

            Using command = New MySqlCommand()

                command.Connection = connection
                command.CommandText = transactSql
                command.CommandType = CommandType.Text

                For Each item As MySqlParameter In parameters
                    command.Parameters.Add(item)
                Next

                Dim result = command.ExecuteNonQuery()
                parameters.Clear()

                Return result


            End Using

        End Using

    End Function

    Protected Function ExecuteRender(transactSql As String) As DataTable

        Using connection = GetConnection()

            connection.Open()

            Using command = New MySqlCommand()

                command.Connection = connection
                command.CommandText = transactSql
                command.CommandType = CommandType.Text

                Dim reader = command.ExecuteReader()

                Using table = New DataTable()

                    table.Load(reader)
                    reader.Dispose()

                    Return table
                End Using


            End Using

        End Using

    End Function

End Class
