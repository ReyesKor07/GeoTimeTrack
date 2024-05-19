using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Essentials;
using Android;

namespace GeoTimeTrack.Droid
{
    [Activity(Label = "GeoTimeTrack", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        // Identificador para la solicitud de permiso de ubicación
        const int RequestLocationId = 0;
        // Arreglo de permisos de ubicación necesarios
        readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnStart()
        {
            base.OnStart();
            // Verifica los permisos de ubicación si la versión de Android es 23 o superior (Android Marshmallow)
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    // Solicita permisos de ubicación si no están concedidos
                    RequestPermissions(LocationPermissions, RequestLocationId);
                }
                else
                { /*Permissions already granted - display a message.*/ }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Inicializa los servicios de Xamarin.Forms y Xamarin.Essentials

            Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            // Carga la aplicación principal
            LoadApplication(new App());
        }

        // Maneja las respuestas a las solicitudes de permisos
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if (requestCode == RequestLocationId)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                { /*Permissions granted - display a message.*/ }
                else
                { /*Permissions denied - display a message.*/ }
            }
            else // Pasa el resultado de la solicitud de permisos a Xamarin.Essentials
            { Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults); }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);    
        }

        // Método obsoleto para manejar el evento BackButtonPressed
        [Obsolete]
        private void OnBackButtonPressed(object sender, EventArgs e)
        {

        }

        // Método para iniciar un servicio en primer plano
        public void StartForegroundService()
        {

        }

    }
}