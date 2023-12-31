﻿Imports System.Data.SqlClient
Public Class frmLectores
    'Private Sub frmLectores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    Abrir_Conexion()
    '    MsgBox("Conexion Creada con Exito", vbOKOnly + vbInformation, "Sistema Lectores")
    'End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblTitulo.Click

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub

    Dim ex, ey As Integer
    Dim Arrastre As Boolean
    Private Sub panelTitulo_MouseDown(sender As Object, e As MouseEventArgs) Handles panelTitulo.MouseDown
        ex = e.X
        ey = e.Y
        Arrastre = True
    End Sub

    Private Sub panelTitulo_MouseMove(sender As Object, e As MouseEventArgs) Handles panelTitulo.MouseMove
        If Arrastre Then Me.Location = Me.PointToScreen(New Point(frmLectores.MousePosition.X - Me.Location.X - ex, frmLectores.MousePosition.Y - Me.Location.Y - ey))
    End Sub

    Private Sub panelTitulo_Paint(sender As Object, e As PaintEventArgs) Handles panelTitulo.Paint

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataLectores.CellContentClick
        'Verificar si se ha dado clic sobre la columna de eliminar
        If e.ColumnIndex = dataLectores.Columns.Item("Eliminar").Index Then
            Dim result As DialogResult
            result = MsgBox("El registro será eliminado permanentemente del sistema y ya no podrá acceder a los datos de este lector. ¿Realmente desea eliminar este registro?", vbQuestion + vbOKCancel, "Sistema Lectores")
            If result = DialogResult.OK Then
                Dim cmd As SqlCommand
                Try
                    Abrir_Conexion()
                    cmd = New SqlCommand("Eliminar_Lector", conexion)
                    cmd.CommandType = 4
                    cmd.Parameters.AddWithValue("@idLector", dataLectores.SelectedCells.Item(1).Value)
                    cmd.ExecuteNonQuery()
                    Cerrar_Conexion()
                    Mostrar()
                Catch ex As Exception

                End Try
            Else
                MsgBox("Eliminación cancelada, el registro no será eliminado", vbInformation + vbOKOnly, "Sistema Lectores")
            End If
        End If
    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles lblNombre.Click

    End Sub

    Private Sub txtObservaciones_TextChanged(sender As Object, e As EventArgs) Handles txtObservaciones.TextChanged

    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        panelDatos.Visible = True
        btnGuardar.Enabled = True
        btnModificar.Enabled = False
        txtIdentidad.Focus()
    End Sub

    Private Sub panelTitulo_MouseUp(sender As Object, e As MouseEventArgs) Handles panelTitulo.MouseUp
        Arrastre = False
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Limpiar()
        btnGuardar.Enabled = True
        btnModificar.Enabled = True
        panelDatos.Visible = False
    End Sub

    Private Sub Limpiar()
        txtIdentidad.Clear()
        txtNombre.Clear()
        txtTelefono.Clear()
        txtDireccion.Clear()
        txtObservaciones.Clear()
        txtIdentidad.Focus()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim cmd As New SqlCommand
        If txtNombre.Text <> "" And txtIdentidad.Text <> "" Then
            Try
                Abrir_Conexion()
                cmd = New SqlCommand("Insertar_Lector", conexion)
                cmd.CommandType = 4

                cmd.Parameters.AddWithValue("@IdLector", txtIdentidad.Text.ToString)
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.ToString)
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text)
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.ToString)
                cmd.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text.ToString)
                cmd.ExecuteNonQuery()
                Cerrar_Conexion()
                panelDatos.Visible = False
                Limpiar()
                Mostrar()

            Catch ex As Exception

            End Try
        Else
            MsgBox("Los campos Numero de Identidad y Nombre, son obligatorios", vbInformation + vbOKOnly, "Sistema Lectores")
        End If
    End Sub

    Private Sub frmLectores_CausesValidationChanged(sender As Object, e As EventArgs) Handles Me.CausesValidationChanged

    End Sub

    Sub Mostrar()
        Dim dt As New DataTable
        Dim da As SqlDataAdapter

        Try
            Abrir_Conexion()
            da = New SqlDataAdapter("Mostrar_Lector", conexion)
            da.Fill(dt)
            dataLectores.DataSource = dt
            Cerrar_Conexion()

            'Cambiar ancho de las columnas (encabezados)
            dataLectores.Columns(0).Width = 35
            dataLectores.Columns(1).Width = 120
            dataLectores.Columns(2).Width = 250
            dataLectores.Columns(3).Width = 100
            dataLectores.Columns(4).Width = 300
            dataLectores.Columns(5).Width = 300

            'Cambiar apariencia de los encabezados
            dataLectores.EnableHeadersVisualStyles = False
            Dim estilo As DataGridViewCellStyle = New DataGridViewCellStyle()
            estilo.BackColor = Color.White
            estilo.ForeColor = Color.Black
            estilo.Font = New Font("Segoe UI", 10, FontStyle.Regular Or FontStyle.Bold)
            dataLectores.ColumnHeadersDefaultCellStyle = estilo
        Catch ex As Exception

        End Try
    End Sub

    Sub Buscar()
        Dim dt As New DataTable
        Dim da As SqlDataAdapter

        Try
            Abrir_Conexion()
            da = New SqlDataAdapter("Buscar_Lector", conexion)
            da.SelectCommand.CommandType = 4
            da.SelectCommand.Parameters.AddWithValue("@Busqueda", txtBuscar.Text)
            da.Fill(dt)
            dataLectores.DataSource = dt
            Cerrar_Conexion()

            'Cambiar ancho de las columnas (encabezados)
            dataLectores.Columns(0).Width = 35
            dataLectores.Columns(1).Width = 120
            dataLectores.Columns(2).Width = 250
            dataLectores.Columns(3).Width = 100
            dataLectores.Columns(4).Width = 300
            dataLectores.Columns(5).Width = 300

            'Cambiar apariencia de los encabezados
            dataLectores.EnableHeadersVisualStyles = False
            Dim estilo As DataGridViewCellStyle = New DataGridViewCellStyle()
            estilo.BackColor = Color.White
            estilo.ForeColor = Color.Black
            estilo.Font = New Font("Segoe UI", 10, FontStyle.Regular Or FontStyle.Bold)
            dataLectores.ColumnHeadersDefaultCellStyle = estilo
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtBuscar_TextChanged(sender As  Object, e As EventArgs) Handles txtBuscar.TextChanged
        Buscar()
        panelDatos.Visible = False
    End Sub
    Private Sub panelDatos_Paint(sender As Object, e As PaintEventArgs) Handles panelDatos.Paint

    End Sub

    Private Sub frmLectores_Load(sender As Object, e As EventArgs) Handles Me.Load
        Mostrar()
    End Sub

    Private Sub frmLectores_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Dim cmd As New SqlCommand
        If txtNombre.Text <> "" And txtIdentidad.Text <> "" Then
            Try
                Abrir_Conexion()
                cmd = New SqlCommand("Editar_Lector", conexion)
                cmd.CommandType = 4

                cmd.Parameters.AddWithValue("@IdLector", txtIdentidad.Text.ToString)
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.ToString)
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text)
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.ToString)
                cmd.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text.ToString)
                cmd.ExecuteNonQuery()
                Cerrar_Conexion()
                panelDatos.Visible = False
                Limpiar()
                Mostrar()

            Catch ex As Exception

            End Try
        Else
            MsgBox("Los campos Numero de Identidad y Nombre, son obligatorios", vbInformation + vbOKOnly, "Sistema Lectores")
        End If
    End Sub

    Private Sub dataLectores_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataLectores.CellContentDoubleClick
        panelDatos.Visible = True
        Try
            txtIdentidad.Text = dataLectores.SelectedCells.Item(1).Value
            txtNombre.Text = dataLectores.SelectedCells.Item(2).Value
            txtTelefono.Text = dataLectores.SelectedCells.Item(3).Value
            txtDireccion.Text = dataLectores.SelectedCells.Item(4).Value
            txtObservaciones.Text = dataLectores.SelectedCells.Item(5).Value

            btnGuardar.Enabled = False
            btnModificar.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
End Class
