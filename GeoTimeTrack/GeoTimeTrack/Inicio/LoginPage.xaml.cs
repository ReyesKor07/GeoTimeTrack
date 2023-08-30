using GeoTimeTrack.FlyoutTabbed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnMainPageButtonClicked(object sender, EventArgs e)
        {
            // Navegar a la página principal
            await Navigation.PushModalAsync(new DeploymentPage());
        }

        private async void OnForgotPasswordLabelTapped(object sender, EventArgs e)
        {
            // Navega a la página ForgotPasswordPage
            await Navigation.PushModalAsync(new ForgotPasswordPage());
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            passwordEntry.IsPassword = !e.Value; // Cambia el valor de IsPassword según el estado del Switch
        }
    }
}