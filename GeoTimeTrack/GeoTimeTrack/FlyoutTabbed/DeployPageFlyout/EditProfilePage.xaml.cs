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
            // Asigna los datos del usuario a los controles en la página
            IdUsuarioEntry.Text = user.IdUsuario.ToString();
            RolEntry.Text = user.Rol;
            NombreEntry.Text = user.Nombre;
            ApellidoPEntry.Text = user.ApellidoP;
            ApellidoMEntry.Text = user.ApellidoM;
            EmailEntry.Text = user.Email;
            PasswordEntry.Text = user.Password;// Asegúrate de tener la propiedad Email en la clase Usuario
        }

        private void GuardarCambios_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Verificar campos obligatorios
                if (string.IsNullOrWhiteSpace(NombreEntry.Text) || string.IsNullOrWhiteSpace(ApellidoPEntry.Text) || string.IsNullOrWhiteSpace(ApellidoMEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
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
                string updateQuery = "UPDATE Usuario_B SET Nombre = @Nombre, ApellidoP = @ApellidoP, ApellidoM = @ApellidoM, Password = @Password, Email = @newEmail WHERE IdUsuario = @IdUsuario";
                using (SqlCommand cmd = new SqlCommand(updateQuery, ConexionSQLServer.cn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", NombreEntry.Text);
                    cmd.Parameters.AddWithValue("@ApellidoP", ApellidoPEntry.Text);
                    cmd.Parameters.AddWithValue("@ApellidoM", ApellidoMEntry.Text);
                    cmd.Parameters.AddWithValue("@Password", PasswordEntry.Text);
                    cmd.Parameters.AddWithValue("@newEmail", newEmail);
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

        private async void RegistroUsuario_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button)
                {
                    if (button.BindingContext is Usuario selectedUser2)
                    {
                        // Crea una instancia de EditProfilePage y pasa los datos del usuario
                        EditTrackTimePage editTrackTimePage = new EditTrackTimePage(selectedUser2);
                        await Navigation.PushModalAsync(editTrackTimePage);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Se produjo un error: {ex.Message} AdminPage", "OK");
            }
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            { PasswordEntry.IsPassword = false; }
            else
            { PasswordEntry.IsPassword = true; }
        }
    }
}