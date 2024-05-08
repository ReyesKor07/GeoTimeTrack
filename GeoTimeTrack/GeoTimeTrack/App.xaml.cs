using GeoTimeTrack.Data;
using GeoTimeTrack.FlyoutTabbed;
using GeoTimeTrack.FlyoutTabbed.DeployPageFlyout;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Envolver la página en un NavigationPage
            MainPage = new NavigationPage(new HomePage());
            // MainPage = new MainPage();
            // MainPage = new NavigationPage(new MainPage());
            // MainPage = new NavigationPage(new DeploymentPage());
            // MainPage = new NavigationPage(new DeploymentPageFlyout());
            // MainPage = new NavigationPage(new AccountCreationPage());
            // MainPage = new NavigationPage(new TrackTimePage());
            // MainPage = new NavigationPage(new ProfilePage());
            // MainPage = new NavigationPage(new AdminPage());
        }

        //protected override async void OnStart()
        //{
        //    try
        //    {
        //        // Recuperar las credenciales del almacenamiento seguro
        //        string usuarioID = await SecureStorage.GetAsync("UsuarioID");
        //        string nombre = await SecureStorage.GetAsync("Nombre");
        //        string apellidoP = await SecureStorage.GetAsync("ApellidoP");
        //        string apellidoM = await SecureStorage.GetAsync("ApellidoM");
        //        string email = await SecureStorage.GetAsync("Email");
        //        string password = await SecureStorage.GetAsync("Password");
        //        string rol = await SecureStorage.GetAsync("Rol");

        //        // Convertir las cadenas recuperadas según sea necesario
        //        int userID = Convert.ToInt32(usuarioID);

        //        // Utilizar las credenciales recuperadas como sea necesario
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar cualquier excepción que pueda ocurrir al recuperar del almacenamiento seguro
        //        Console.WriteLine($"Error al recuperar del almacenamiento seguro: {ex.Message}");
        //    }
        //}

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
                            await MainPage.Navigation.PushModalAsync(new DeploymentPage());
                        }
                        else
                        {
                            // Autenticación fallida, mostrar mensaje de error o navegar a la página de inicio de sesión
                            await MainPage.DisplayAlert("Error", "No se pudieron recuperar las credenciales almacenadas.", "Aceptar");
                            // Alternativamente, navegar a la página de inicio de sesión:
                            // await MainPage.Navigation.PushModalAsync(new LoginPage());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la autenticación
                await MainPage.DisplayAlert("Error", "Ocurrió un error al autenticar al usuario: " + ex.Message, "Aceptar");
            }
            finally
            {
                ConexionSQLServer.Cerrar();
            }
        }


        protected override async void OnStart()
        {
            try
            {
                // Recuperar las credenciales del almacenamiento seguro
                string usuarioID = await SecureStorage.GetAsync("UsuarioID");
                string nombre = await SecureStorage.GetAsync("Nombre");
                string apellidoP = await SecureStorage.GetAsync("ApellidoP");
                string apellidoM = await SecureStorage.GetAsync("ApellidoM");
                string email = await SecureStorage.GetAsync("Email");
                string password = await SecureStorage.GetAsync("Password");
                string rol = await SecureStorage.GetAsync("Rol");

                if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(password))
                {
                    // Autenticar automáticamente con las credenciales guardadas
                    await AutenticarUsuario(usuarioID, password);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al recuperar del almacenamiento seguro
                Console.WriteLine($"Error al recuperar del almacenamiento seguro: {ex.Message}");
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}