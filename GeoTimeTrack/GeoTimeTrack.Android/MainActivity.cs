using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Media;
using Xamarin.Essentials;

namespace GeoTimeTrack.Droid
{
    [Activity(Label = "GeoTimeTrack", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

            LoadApplication(new App());

            RetrieveUserCredentials();
        }

        private async void RetrieveUserCredentials()
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);    
        }

        [Obsolete]
        // Maneja el evento BackButtonPressed para salir de la aplicación
        private void OnBackButtonPressed(object sender, EventArgs e)
        {

        }

        public void StartForegroundService()
        {

        }

    }
}