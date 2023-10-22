using GeoTimeTrack.FlyoutTabbed.DeployPageFlyout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack.FlyoutTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeploymentPageFlyout : ContentPage
    {
        int UserId;
        string Nombre, ApellidoP, ApellidoM, Email, Password, Rol;

        public ListView ListView;

        public DeploymentPageFlyout()
        {
            InitializeComponent();

            BindingContext = new DeploymentPageFlyoutViewModel();
            ListView = MenuItemsListView;

            Nombre = LoginPage.Name;
            ApellidoP = LoginPage.LastName;
            Email = LoginPage.Email;
            Rol = LoginPage.Rol;
            Usuario.Text = $"{Nombre} {ApellidoP} \n{Email}";

            // Verificar el rol del usuario y mostrar/ocultar el botón "Admin" en consecuencia
            if (Rol == "Administrador")
            {
                AdminButton.IsVisible = true; // Mostrar el botón si el usuario es administrador
            }
            else
            {
                AdminButton.IsVisible = false; // Ocultar el botón si el usuario no es administrador
            }
        }

        public async void navigation()
        {
            await Navigation.PushModalAsync(new ProfilePage());
        }

        private async void Cuenta_Clicked(object sender, EventArgs e)
        {
            try
            {
                navigation(); // Realiza la navegación a la página de perfil
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "DeploymentPageFlyout.Cuenta_Clicked", "OK");
            }
        }

        private void Admin_Clicked(object sender, EventArgs e)
        {

        }

        class DeploymentPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<DeploymentPageFlyoutMenuItem> MenuItems { get; set; }
            
            public DeploymentPageFlyoutViewModel()
            {

            }
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;

            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}