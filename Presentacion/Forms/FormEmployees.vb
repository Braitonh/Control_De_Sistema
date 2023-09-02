Imports Domain
Public Class FormEmployees

    Dim employeeModel As New EmployeeModel
    Private Sub FormEmployees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListEmployes()
    End Sub

    Public Sub ListEmployes()
        Try
            DataGridView1.DataSource = employeeModel.GetAllEmployees()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class