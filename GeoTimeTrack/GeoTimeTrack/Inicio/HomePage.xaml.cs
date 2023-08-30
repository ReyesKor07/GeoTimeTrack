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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Navegar a la página principal
            await Navigation.PushModalAsync(new LoginPage());
            // await Navigation.PushModalAsync(new WorkPage());
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Navegar a la página principal
            await Navigation.PushModalAsync(new AccountCreationPage());
        }
    }
}