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
            MainPage = new NavigationPage(new HomePage());
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
        //    var current = Connectivity.NetworkAccess; // Verificar la conectividad de red
        //    if (current != NetworkAccess.Internet)
        //    {
        //        Console.WriteLine("Necesitas estar conectado a Internet para usar la aplicación correctamente."); // No hay conexión a Internet, mostrar mensaje de advertencia
        //    }
        //    else
        //    {
        //        try
        //        {
        //            // Recuperar las credenciales del almacenamiento seguro
        //            string usuarioID = await SecureStorage.GetAsync("UsuarioID");
        //            string password = await SecureStorage.GetAsync("Password");
        //            if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(password))
        //            {
        //                await AutenticarUsuario(usuarioID, password); // Autenticar automáticamente con las credenciales guardadas
        //            }
        //            else
        //            {
        //                MainPage = new NavigationPage(new HomePage()); // No hay credenciales guardadas, navegar a la página principal (HomePage)
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Manejar cualquier excepción que pueda ocurrir al recuperar del almacenamiento seguro
        //            Console.WriteLine($"Error al recuperar del almacenamiento seguro: {ex.Message}. App.xaml.cs\n");
        //        }
        //    }
        //}

        protected override async void OnStart()
        {
            //try
            //{
            //    var current = Connectivity.NetworkAccess; // Verificar la conectividad de red
            //    if (current != NetworkAccess.Internet)
            //    {
            //        Console.WriteLine("Necesitas estar conectado a Internet para usar la aplicación correctamente."); // No hay conexión a Internet, mostrar mensaje de advertencia
            //    }
            //    // Recuperar las credenciales del almacenamiento seguro
            //    string usuarioID = await SecureStorage.GetAsync("UsuarioID");
            //    string password = await SecureStorage.GetAsync("Password");
            //    if (!string.IsNullOrEmpty(usuarioID) && !string.IsNullOrEmpty(password))
            //    {
            //        await AutenticarUsuario(usuarioID, password); // Autenticar automáticamente con las credenciales guardadas
            //    }
            //    else
            //    {
            //        MainPage = new NavigationPage(new HomePage()); // No hay credenciales guardadas, navegar a la página principal (HomePage)
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Manejar cualquier excepción que pueda ocurrir al recuperar del almacenamiento seguro
            //    Console.WriteLine($"Error al recuperar del almacenamiento seguro: {ex.Message}. App.xaml.cs\n");
            //}
        }

        //public async Task AutenticarUsuario(string userId, string password)
        //{
        //    try
        //    {
        //        // Realizar la autenticación utilizando las credenciales guardadas
        //        ConexionSQLServer.Abrir();
        //        string query = "SELECT * FROM Usuario_B WHERE IdUsuario = @UserId AND Password = @Password";
        //        using (SqlCommand cmd = new SqlCommand(query, ConexionSQLServer.cn))
        //        {
        //            cmd.Parameters.AddWithValue("@UserId", userId);
        //            cmd.Parameters.AddWithValue("@Password", password);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    await MainPage.Navigation.PushModalAsync(new DeploymentPage()); // Autenticación exitosa, navegar a la página principal
        //                }
        //                else
        //                {
        //                    // Autenticación fallida, mostrar mensaje de error o navegar a la página de inicio de sesión
        //                    Console.WriteLine("No se pudieron recuperar las credenciales almacenadas. App.xaml.cs\n");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar cualquier excepción que pueda ocurrir durante la autenticación
        //        Console.WriteLine($"Ocurrió un error al autenticar al usuario: {ex.Message}. App.xaml.cs\n");
        //    }
        //    finally
        //    {
        //        ConexionSQLServer.Cerrar();
        //    }
        //}

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}