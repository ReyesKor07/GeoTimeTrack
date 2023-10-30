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
        public string Usuario { get; set; }
        int UserId;
        string Nombre, ApellidoP, ApellidoM, Email, Password, Rol;

        public ListView ListView;

        public DeploymentPageFlyout()
        {
            InitializeComponent();

            BindingContext = new DeploymentPageFlyoutViewModel();
            ListView = MenuItemsListView;
            // UserId = 1; Nombre = "Brandon"; ApellidoP = "Reyes"; ApellidoM = "De La Cruz"; Email = "brandonreyes@gmail.com"; Password = "123"; Rol = "Administrador";
            UserId = LoginPage.UserID;
            Nombre = LoginPage.Name; ApellidoP = LoginPage.LastName;
            Email = LoginPage.Email;
            Rol = LoginPage.Rol;
            NombreLabel.Text = $"{Nombre} {ApellidoP}, ID: {UserId}";
            EmailLabel.Text = $"Email: {Email}";

            if (Rol == "Administrador")
            {   AdminButton.IsVisible = true; RolLabel.IsVisible = true; RolLabel.Text = $"{Rol}"; }
            else
            { AdminButton.IsVisible = false; RolLabel.IsVisible = false; }
        }

        public async void NavigationProfilePage()
        { await Navigation.PushModalAsync(new ProfilePage()); }
        public async void NavigationAdminPage()
        { await Navigation.PushModalAsync(new AdminPage()); }

        private async void Cuenta_Clicked(object sender, EventArgs e)
        {
            try // Realiza la navegación a la página de perfil
            { NavigationProfilePage(); }
            catch (Exception ex)
            { await DisplayAlert("Error", ex.Message + "DeploymentPageFlyout.Cuenta_Clicked", "OK"); }
        }

        private async void Admin_Clicked(object sender, EventArgs e)
        {
            try // Realiza la navegación a la página de Administrador
            { NavigationAdminPage(); }
            catch (Exception ex)
            { await DisplayAlert("Error", ex.Message + "DeploymentPageFlyout.Cuenta_Clicked", "OK"); }
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