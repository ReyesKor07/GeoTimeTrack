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
    public partial class AccountCreationPage : ContentPage
    {
        public AccountCreationPage()
        {
            InitializeComponent();
        }

        private void OnCreateUserButtonClicked(object sender, EventArgs e)
        {
            
        }

        private async void OnLoginLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            // await Navigation.PushModalAsync(new LoginPage());
        }

        private void OnShowPasswordSwitchToggled(object sender, ToggledEventArgs e)
        {
            // Cambia el valor de IsPassword según el estado del Switch
            passwordEntry.IsPassword = !e.Value;
        }
    }
}