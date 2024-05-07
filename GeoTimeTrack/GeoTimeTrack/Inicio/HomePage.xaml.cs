using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Data;
using GeoTimeTrack.Data;
using GeoTimeTrack.FlyoutTabbed;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            OnAppearing();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Comprobar si hay credenciales guardadas
            string userId = await SecureStorage.GetAsync("UserId");
            string password = await SecureStorage.GetAsync("Password");

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
            {
                // Autenticar automáticamente con las credenciales guardadas
                await AutenticarUsuario(userId, password);
            }
        }

        private async Task AutenticarUsuario(string userId, string password)
        {
            try
            {
                // Realizar la autenticación utilizando las credenciales guardadas
                ConexionSQLServer.Abrir();
                string query = "SELECT * FROM Usuario_B WHERE IdUsuario = @UserId AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, ConexionSQLServer.cn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Autenticación exitosa, navegar a la página principal
                            await Navigation.PushModalAsync(new DeploymentPage());
                        }
                        else
                        {
                            // Autenticación fallida, mostrar mensaje de error o navegar a la página de inicio de sesión
                            await DisplayAlert("Error", "No se pudieron recuperar las credenciales almacenadas.", "Aceptar");
                            // Alternativamente, navegar a la página de inicio de sesión:
                            // await Navigation.PushModalAsync(new LoginPage());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la autenticación
                await DisplayAlert("Error", "Ocurrió un error al autenticar al usuario: " + ex.Message, "Aceptar");
            }
            finally
            {
                ConexionSQLServer.Cerrar();
            }
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage()); // Navegar a la página principal
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AccountCreationPage()); // Navegar a la página principal
        }
    }
}