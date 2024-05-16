using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Essentials;

namespace GeoTimeTrack.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            LoadApplication(new App());
            RetrieveUserCredentials();

            return base.FinishedLaunching(app, options);
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
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al recuperar del almacenamiento seguro
                Console.WriteLine($"Error al recuperar del almacenamiento seguro: {ex.Message}. AppDelegate.cs\n");
            }
        }

        //private async void RetrieveUserCredentials()
        //{
        //    try
        //    {
        //        // Recuperar las credenciales del almacenamiento seguro
        //        string userId = await SecureStorage.GetAsync("UserId");
        //        string password = await SecureStorage.GetAsync("Password");

        //        if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
        //        {
        //            // Autenticar automáticamente al usuario utilizando las credenciales recuperadas
        //            // Esto debería ser similar a la lógica en la página de inicio de sesión (LoginPage.cs)
        //            var app = Xamarin.Forms.Application.Current as App;
        //            await app.AutenticarUsuario(userId, password);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar cualquier excepción que pueda ocurrir al recuperar las credenciales
        //        System.Console.WriteLine($"Error al recuperar del almacenamiento seguro: {ex.Message}");
        //    }
        //}
    }
} 