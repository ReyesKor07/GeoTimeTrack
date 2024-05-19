using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Navega a la página de inicio de sesión
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Navega a la página de creación de cuenta
            await Navigation.PushModalAsync(new AccountCreationPage());
        }
    }
}