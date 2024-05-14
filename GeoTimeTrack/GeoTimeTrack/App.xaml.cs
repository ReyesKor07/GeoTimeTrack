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

            // MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
            // MainPage = new NavigationPage(new DeploymentPage());
            // MainPage = new NavigationPage(new DeploymentPageFlyout());
            // MainPage = new NavigationPage(new AccountCreationPage());
            // MainPage = new NavigationPage(new TrackTimePage());
            // MainPage = new NavigationPage(new ProfilePage());
            // MainPage = new NavigationPage(new AdminPage());
        }

        protected override async void OnStart()
        {
            // Verificar la conectividad de red
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                // No hay conexión a Internet, mostrar mensaje de advertencia
                Console.WriteLine("Necesitas estar conectado a Internet para usar la aplicación correctamente.");
            }
            else
            {
                try
                {
                    // Recuperar las credenciales del almacenamiento seguro
                    string usuarioID = await SecureStorage.GetAsync("UsuarioID");
                    string password = await SecureStorage.GetAsync("Password");

                    if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(password))
                    {
                        // Autenticar automáticamente con las credenciales guardadas
                        await AutenticarUsuario(usuarioID, password);
                    }
                    else
                    {
                        // No hay credenciales guardadas, navegar a la página principal (HomePage)
                        MainPage = new NavigationPage(new HomePage());
                        // MainPage = new NavigationPage(new TrackTimePage());
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que pueda ocurrir al recuperar del almacenamiento seguro
                    Console.WriteLine($"Error al recuperar del almacenamiento seguro: {ex.Message}");
                }
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
                            await MainPage.Navigation.PushModalAsync(new DeploymentPage());
                        }
                        else
                        {
                            // Autenticación fallida, mostrar mensaje de error o navegar a la página de inicio de sesión
                            Console.WriteLine("No se pudieron recuperar las credenciales almacenadas.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la autenticación
                Console.WriteLine($"Ocurrió un error al autenticar al usuario: {ex.Message}");
            }
            finally
            {
                ConexionSQLServer.Cerrar();
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