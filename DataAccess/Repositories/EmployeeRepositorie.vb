Imports System.Data
Imports MySql.Data.MySqlClient
Public Class EmployeeRepositorie
    Inherits MasterRepository
    Implements IEmployeeRepository

    Private selectAll As String
    Private insert As String
    Private update As String
    Private delete As String

    Public Sub New()

        selectAll = "SELECT * FROM employee"
        insert = "SELECT INTO employee VALUES(@idNumber,@name,@mail,@birthday)"
        update = "UPDATE employee SET IdNumber=@idNumber,Name=@name,Mail=@mail,Birthday=@birthday where IdPK=@IdPK"
        delete = "DELETE FROM employee WHERE IdPK=@IdPK"

    End Sub

    Public Function GetAll() As IEnumerable(Of Employee) Implements IGenericRepository(Of Employee).GetAll
        Dim resultTable = ExecuteRender(selectAll)
        Dim listEmployees = New List(Of Employee)

        For Each item As DataRow In resultTable.Rows
            listEmployees.Add(New Employee With {
                .IdPk = item(0),
                .IdNumber = item(1),
                .Name = item(2),
                .Mail = item(3),
                .Birthday = item(4)
            })
        Next

        Return listEmployees
    End Function

    Public Function Add(entity As Employee) As Integer Implements IGenericRepository(Of Employee).Add
        parameters = New List(Of MySqlParameter)
        parameters.Add(New MySqlParameter("@idNumber", entity.IdNumber))
        parameters.Add(New MySqlParameter("@name", entity.Name))
        parameters.Add(New MySqlParameter("@mail", entity.Mail))
        parameters.Add(New MySqlParameter("@birthday", entity.Birthday))

        Return ExecuteNonQuery(insert)

    End Function

    Public Function Edit(entity As Employee) As Integer Implements IGenericRepository(Of Employee).Edit
        parameters = New List(Of MySqlParameter)
        parameters.Add(New MySqlParameter("@IdPK", entity.IdPk))
        parameters.Add(New MySqlParameter("@idNumber", entity.IdNumber))
        parameters.Add(New MySqlParameter("@name", entity.Name))
        parameters.Add(New MySqlParameter("@mail", entity.Mail))
        parameters.Add(New MySqlParameter("@birthday", entity.Birthday))

        Return ExecuteNonQuery(update)
    End Function

    Public Function Remove(id As Integer) As Integer Implements IGenericRepository(Of Employee).Remove
        parameters = New List(Of MySqlParameter)
        parameters.Add(New MySqlParameter("@idNumber", id))


        Return ExecuteNonQuery(delete)
    End Function
End Class
