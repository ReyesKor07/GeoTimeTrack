using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Media;
using Xamarin.Essentials;
using Android;

namespace GeoTimeTrack.Droid
{
    [Activity(Label = "GeoTimeTrack", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int RequestLocationId = 0;

        readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnStart()
        {
            base.OnStart();

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    RequestPermissions(LocationPermissions, RequestLocationId);
                }
                else
                { /*Permissions already granted - display a message.*/ }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            // Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App());
            // RetrieveUserCredentials();
        }

        //private async void RetrieveUserCredentials()
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
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar cualquier excepción que pueda ocurrir al recuperar del almacenamiento seguro
        //        Console.WriteLine($"Error al recuperar del almacenamiento seguro: {ex.Message}. MainActivity.cs\n");
        //    }
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if (requestCode == RequestLocationId)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                { /*Permissions granted - display a message.*/ }
                else
                { /*Permissions denied - display a message.*/ }
            }
            else
            { Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults); }

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