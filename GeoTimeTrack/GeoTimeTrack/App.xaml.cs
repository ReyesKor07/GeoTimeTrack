using GeoTimeTrack.FlyoutTabbed;
using GeoTimeTrack.FlyoutTabbed.DeployPageFlyout;
using System;
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

                // Convertir las cadenas recuperadas según sea necesario
                int userID = Convert.ToInt32(usuarioID);

                // Utilizar las credenciales recuperadas como sea necesario
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