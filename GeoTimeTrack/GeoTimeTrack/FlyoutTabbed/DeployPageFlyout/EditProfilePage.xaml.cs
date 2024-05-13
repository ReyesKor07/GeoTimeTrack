using GeoTimeTrack.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xamarin.Essentials;
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
        //int UserId;
        //string Nombre, ApellidoP, ApellidoM, Email, Password, Rol;

        public EditProfilePage(Usuario user)
        {
            InitializeComponent();
            SelectedUser = user;
            IdUsuarioEntry.Text = user.IdUsuario.ToString();
            
            NombreEntry.Text = user.Nombre;
            ApellidoPEntry.Text = user.ApellidoP;
            ApellidoMEntry.Text = user.ApellidoM;
            EmailEntry.Text = user.Email;
            RolPicker.SelectedItem = user.Rol; // Establece el valor seleccionado en el Picker
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

        private void RolPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void GuardarCambios_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                // No hay conexión a Internet, mostrar mensaje de advertencia
                DisplayAlert("Error", "Necesitas estar conectado a Internet para refrescar la página.", "OK");
                return;
            }
            try
            {
                // Verifica campos obligatorios
                if (string.IsNullOrWhiteSpace(NombreEntry.Text) || string.IsNullOrWhiteSpace(ApellidoPEntry.Text) || string.IsNullOrWhiteSpace(ApellidoMEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text))
                {
                    DisplayAlert("Advertencia", "Todos los campos obligatorios son requeridos y no pueden estar vacíos.", "Aceptar");
                    return;
                }

                // Verificar si el correo ha cambiado
                string newEmail = EmailEntry.Text;
                if (newEmail != SelectedUser.Email)
                {
                    ConexionSQLServer.Abrir();
                    string emailCheckQuery = "SELECT COUNT(*) FROM Usuario_B WHERE Email = @newEmail";
                    using (SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, ConexionSQLServer.cn))
                    {
                        emailCheckCmd.Parameters.AddWithValue("@newEmail", newEmail);
                        int emailCount = (int)emailCheckCmd.ExecuteScalar();
                        emailCheckCmd.Dispose();
                        if (emailCount > 0)
                        {
                            DisplayAlert("Error", "El nuevo correo proporcionado ya existe.", "Aceptar");
                            return;
                        }
                    }
                }

                ConexionSQLServer.Abrir();
                string updateQuery = "UPDATE Usuario_B SET Nombre = @Nombre, ApellidoP = @ApellidoP, ApellidoM = @ApellidoM, Email = @newEmail";

                // Verifica si se proporcionó una nueva contraseña
                if (!string.IsNullOrWhiteSpace(PasswordEntry.Text))
                {
                    updateQuery += ", Password = @Password";
                }

                updateQuery += " WHERE IdUsuario = @IdUsuario";

                using (SqlCommand cmd = new SqlCommand(updateQuery, ConexionSQLServer.cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", NombreEntry.Text);
                    cmd.Parameters.AddWithValue("@ApellidoP", ApellidoPEntry.Text);
                    cmd.Parameters.AddWithValue("@ApellidoM", ApellidoMEntry.Text);
                    cmd.Parameters.AddWithValue("@newEmail", newEmail);

                    // Agrega el parámetro de contraseña solo si se proporcionó una nueva
                    if (!string.IsNullOrWhiteSpace(PasswordEntry.Text))
                    {
                        cmd.Parameters.AddWithValue("@Password", PasswordEntry.Text);
                    }

                    cmd.Parameters.AddWithValue("@IdUsuario", SelectedUser.IdUsuario);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        DisplayAlert("Éxito", "Los cambios se guardaron correctamente. Los cambios serán visibles después.", "Aceptar");
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
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                // No hay conexión a Internet, mostrar mensaje de advertencia
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para refrescar la página.", "OK");
                return;
            }
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