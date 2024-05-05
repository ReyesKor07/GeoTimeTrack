using GeoTimeTrack.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack.FlyoutTabbed.DeployPageFlyout
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfilePage : ContentPage
	{

        private Usuario SelectedUser;
        int UserId;
        string Nombre, ApellidoP, ApellidoM, Email, Password, Rol;

        public EditProfilePage(Usuario user)
        {
            InitializeComponent();
            SelectedUser = user;
            IdUsuarioEntry.Text = user.IdUsuario.ToString();
            
            NombreEntry.Text = user.Nombre;
            ApellidoPEntry.Text = user.ApellidoP;
            ApellidoMEntry.Text = user.ApellidoM;
            EmailEntry.Text = user.Email;
            // Establece el valor seleccionado en el Picker
            RolPicker.SelectedItem = user.Rol;
            // PasswordEntry.Text = user.Password;
            // RolEntry.Text = user.Rol; // Eliminar esta línea
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            { PasswordEntry.IsPassword = false; }
            else
            { PasswordEntry.IsPassword = true; }
        }

        // Dentro de la clase EditProfilePage
        private void RolPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí puedes agregar lógica si deseas realizar alguna acción cuando el valor del Picker cambie
        }

        private void GuardarCambios_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Verifica campos obligatorios
                if (string.IsNullOrWhiteSpace(NombreEntry.Text) || string.IsNullOrWhiteSpace(ApellidoPEntry.Text) || string.IsNullOrWhiteSpace(ApellidoMEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text) || RolPicker.SelectedItem == null)
                {
                    DisplayAlert("Advertencia", "Todos los campos obligatorios son requeridos y no pueden estar vacíos.", "Aceptar");
                    return;
                }

                // Verificar si el correo ha cambiado
                string newEmail = EmailEntry.Text;
                if (newEmail != SelectedUser.Email)
                {
                    // Código de verificación de correo electrónico...
                }

                ConexionSQLServer.Abrir();
                string updateQuery = "UPDATE Usuario_B SET Nombre = @Nombre, ApellidoP = @ApellidoP, ApellidoM = @ApellidoM, Password = @Password, Email = @newEmail, Rol = @Rol WHERE IdUsuario = @IdUsuario";
                using (SqlCommand cmd = new SqlCommand(updateQuery, ConexionSQLServer.cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", NombreEntry.Text);
                    cmd.Parameters.AddWithValue("@ApellidoP", ApellidoPEntry.Text);
                    cmd.Parameters.AddWithValue("@ApellidoM", ApellidoMEntry.Text);
                    cmd.Parameters.AddWithValue("@Password", PasswordEntry.Text);
                    cmd.Parameters.AddWithValue("@newEmail", newEmail);
                    cmd.Parameters.AddWithValue("@Rol", RolPicker.SelectedItem.ToString()); // Obtener el valor seleccionado del Picker
                    cmd.Parameters.AddWithValue("@IdUsuario", SelectedUser.IdUsuario);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        DisplayAlert("Éxito", "Los cambios se guardaron correctamente.", "Aceptar");
                    }
                    else
                    {
                        DisplayAlert("Error", "No se pudieron guardar los cambios.", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Ocurrió un error al guardar los cambios: " + ex.Message, "Aceptar");
            }
            finally
            {
                ConexionSQLServer.Cerrar();
            }
        }

        private async void EliminarUsuario_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Mostrar un mensaje de confirmación antes de eliminar el usuario
                bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas eliminar este usuario?", "Sí", "No");
                if (!answer)
                    return;

                // Eliminar los registros relacionados en la tabla "Registro_B"
                ConexionSQLServer.Abrir();
                string deleteRegistrosQuery = "DELETE FROM Registro_B WHERE IdUsuario = @IdUsuario";
                using (SqlCommand cmdDeleteRegistros = new SqlCommand(deleteRegistrosQuery, ConexionSQLServer.cn))
                {
                    cmdDeleteRegistros.Parameters.AddWithValue("@IdUsuario", SelectedUser.IdUsuario);
                    cmdDeleteRegistros.ExecuteNonQuery();
                }

                // Eliminar el usuario
                string deleteUsuarioQuery = "DELETE FROM Usuario_B WHERE IdUsuario = @IdUsuario";
                using (SqlCommand cmdDeleteUsuario = new SqlCommand(deleteUsuarioQuery, ConexionSQLServer.cn))
                {
                    cmdDeleteUsuario.Parameters.AddWithValue("@IdUsuario", SelectedUser.IdUsuario);
                    int rowsAffected = cmdDeleteUsuario.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        await DisplayAlert("Éxito", "El usuario se eliminó correctamente.\nLos cambios se reflejan cuando el administrador actualiza la página utilizando el botón 'Refresh' en la esquina superior derecha.", "Aceptar");
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudo eliminar el usuario.", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al eliminar el usuario: {ex.Message}", "Aceptar");
            }
            finally
            {
                ConexionSQLServer.Cerrar();
            }
        }
    }
}