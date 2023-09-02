Imports DataAccess
Imports System.ComponentModel.DataAnnotations

Public Class EmployeeModel

    'Fields
    Private _IdPk As Integer
    Private _IdNumber As String
    Private _Name As String
    Private _Mail As String
    Private _Birthday As Date
    Private _Age As Integer
    Private _State As EntityState
    Private Repository As IEmployeeRepository

    'Properties
#Region "Getters y Setters"
    Public Property IdPk As Integer
        Get
            Return _IdPk
        End Get
        Set(value As Integer)
            _IdPk = value
        End Set
    End Property

    <Required(ErrorMessage:="El campo number es obligatorio")>
    <RegularExpression("([0-9])+", ErrorMessage:="Solo se permite numeros")>
    <StringLength(3, MinimumLength:=3, ErrorMessage:="El campo debe ser de 3 digitos")>
    Public Property IdNumber As String
        Get
            Return _IdNumber
        End Get
        Set(value As String)
            _IdNumber = value
        End Set
    End Property

    <Required(ErrorMessage:="El campo number es obligatorio")>
    <RegularExpression("^[a-zA-Z]+$", ErrorMessage:="Campo solo admite letras")>
    <StringLength(30, MinimumLength:=3)>
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Public Property Mail As String
        Get
            Return _Mail
        End Get
        Set(value As String)
            _Mail = value
        End Set
    End Property

    Public Property Birthday As Date
        Get
            Return _Birthday
        End Get
        Set(value As Date)
            _Birthday = value
        End Set
    End Property

    Public Property Age As Integer
        Get
            Return _Age
        End Get
        Set(value As Integer)
            _Age = value
        End Set
    End Property

    Public Property State As EntityState
        Get
            Return _State
        End Get
        Set(value As EntityState)
            _State = value
        End Set
    End Property
#End Region

    'Constructor
    Public Sub New()
        Repository = New EmployeeRepositorie()

    End Sub

    'Methods
    Public Function SaveChanges() As String

        Dim message As String = Nothing

        Try

            Dim employeeDataModel As New Employee()

            employeeDataModel.IdPk = IdPk
            employeeDataModel.IdNumber = IdNumber
            employeeDataModel.Name = Name
            employeeDataModel.Mail = Mail
            employeeDataModel.Birthday = Birthday

            Select Case State
                Case EntityState.Added
                    Repository.Add(employeeDataModel)
                    message = "Empleado Agregado"
                Case EntityState.Modified
                    Repository.Edit(employeeDataModel)
                    message = "Empleado Editado"
                Case EntityState.Added
                    Repository.Remove(IdPk)
                    message = "Empleado Eliminado"
            End Select


        Catch ex As Exception
            Dim sqlEx As System.Data.SqlClient.SqlException = TryCast(ex, System.Data.SqlClient.SqlException)

            If sqlEx IsNot Nothing AndAlso sqlEx.Number = 2627 Then

                message = "Registro duplicado"

            Else
                message = ex.ToString

            End If

        End Try

        Return message

    End Function

    Public Function GetAllEmployees() As List(Of EmployeeModel)

        Dim ListEmployeeDataModel = Repository.GetAll()
        Dim ListEmployeeViewModel = New List(Of EmployeeModel)

        For Each item As Employee In ListEmployeeDataModel

            Dim birthDate = item.Birthday

            ListEmployeeViewModel.Add(New EmployeeModel With {
            .IdPk = item.IdPk,
            .IdNumber = item.IdNumber,
            .Name = item.Name,
            .Mail = item.Mail,
            .Birthday = item.Birthday,
            .Age = CalculateAge(birthDate)
            })

        Next

        Return ListEmployeeViewModel

    End Function

    Private Function CalculateAge(birth As Date) As Integer

        Dim dateNow = Date.Now

        Return dateNow.Year - birth.Year

    End Function

End Class
