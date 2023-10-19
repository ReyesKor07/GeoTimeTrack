using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoTimeTrack.Data;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreationPage : ContentPage
    {
        public AccountCreationPage()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            usernameEntry.Text = null;
            userlastnameEntry.Text = null;
            usermiddlenameEntry.Text = null;
            emailEntry.Text = null;
            passwordEntry.Text = null;
        }

        private void OnCreateUserButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Validar si los campos de nombre, apellidos, correo y contraseña no están vacíos
                if (string.IsNullOrWhiteSpace(usernameEntry.Text) || string.IsNullOrWhiteSpace(userlastnameEntry.Text) || string.IsNullOrWhiteSpace(usermiddlenameEntry.Text) || string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
                    DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                    return;
                }
                ConexionSQLServer.Abrir();
                string emailCheckQuery = "SELECT COUNT(*) FROM Usuario WHERE Email = @email"; // Consulta SQL para verificar si el correo electrónico ya existe
                using (SqlCommand emailCheckCmd = new SqlCommand(emailCheckQuery, ConexionSQLServer.cn))
                {
                    emailCheckCmd.Parameters.AddWithValue("@email", emailEntry.Text);
                    int emailCount = (int)emailCheckCmd.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        // El correo ya existe
                        DisplayAlert("Error", "El correo proporcionado ya está registrado.", "OK");
                    }
                    else
                    {
                        // El correo no existe, insertar el nuevo usuario
                        SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, ApellidoP, ApellidoM, Email, Password) VALUES (@name, @apellidoP, @apellidoM, @email, @password)", ConexionSQLServer.cn);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@name", usernameEntry.Text);
                        cmd.Parameters.AddWithValue("@apellidoP", userlastnameEntry.Text);
                        cmd.Parameters.AddWithValue("@apellidoM", usermiddlenameEntry.Text);
                        cmd.Parameters.AddWithValue("@email", emailEntry.Text);
                        cmd.Parameters.AddWithValue("@password", passwordEntry.Text);
                        cmd.ExecuteNonQuery();
                        DisplayAlert("Info", "Usuario creado con éxito", "OK");
                        Clear();
                    }
                }
                ConexionSQLServer.Cerrar();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message + "AccountCreatePage", "OK");
            }
        }


        private async void OnLoginLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            // await Navigation.PushModalAsync(new LoginPage());
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Cambia el valor de IsPassword según el estado del Switch
            passwordEntry.IsPassword = !e.Value;
        }
    }
}