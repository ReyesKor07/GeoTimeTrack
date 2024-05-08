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
        string Nombre, ApellidoP, Email, Rol;

        public ListView ListView;

        public DeploymentPageFlyout()
        {
            InitializeComponent();

            BindingContext = new DeploymentPageFlyoutViewModel();
            ListView = MenuItemsListView;
            //UserId = LoginPage.UserID; Nombre = LoginPage.Name; ApellidoP = LoginPage.LastName; Email = LoginPage.Email; Rol = LoginPage.Rol;
            InitializeUserData();
            NombreLabel.Text = $"ID: {UserId} \n{Nombre} {ApellidoP}";
            EmailLabel.Text = $"Email: \n{Email}";

            if (Rol == "Administrador")
            { AdminButton.IsVisible = true; RolLabel.IsVisible = true; RolLabel.Text = $"{Rol}"; }
            else
            { AdminButton.IsVisible = false; RolLabel.IsVisible = false; }
        }

        private async void InitializeUserData()
        {
            UserId = Convert.ToInt32(await SecureStorage.GetAsync("UsuarioID"));
            Nombre = await SecureStorage.GetAsync("Nombre");
            ApellidoP = await SecureStorage.GetAsync("ApellidoP");
            Email = await SecureStorage.GetAsync("Email");
            Rol = await SecureStorage.GetAsync("Rol");
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