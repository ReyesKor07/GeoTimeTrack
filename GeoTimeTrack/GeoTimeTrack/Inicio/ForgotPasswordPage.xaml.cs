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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private void OnSendCodeButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("Mantenimiento", "Lo sentimos, esta función no está disponible en este momento debido a mantenimiento.", "OK");
        }
    }
}