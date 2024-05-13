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
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                // No hay conexión a Internet, mostrar mensaje de advertencia
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para ingresar al panel administrador.", "OK");
                return;
            }
            try // Realiza la navegación a la página de Administrador
            { NavigationAdminPage(); }
            catch (Exception ex)
            { await DisplayAlert("Error", ex.Message + "DeploymentPageFlyout.Cuenta_Clicked", "OK"); }
        }

        private async void Exit_Clicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                // No hay conexión a Internet, mostrar mensaje de advertencia
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para cerrar sesión.", "OK");
                return;
            }
            try
            {
                // Pedir confirmación al usuario antes de salir de la cuenta
                bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas salir de tu cuenta?", "Sí", "No");

                if (answer)
                {
                    // Si se confirma la salida, entonces navegar a la página de inicio de sesión
                    App.Current.MainPage = new NavigationPage(new LoginPage());

                    // Borrar las credenciales del usuario del almacenamiento seguro
                    await SecureStorage.SetAsync("UsuarioID", string.Empty);
                    await SecureStorage.SetAsync("Nombre", string.Empty);
                    await SecureStorage.SetAsync("ApellidoP", string.Empty);
                    await SecureStorage.SetAsync("ApellidoM", string.Empty);
                    await SecureStorage.SetAsync("Email", string.Empty);
                    await SecureStorage.SetAsync("Password", string.Empty);
                    await SecureStorage.SetAsync("Rol", string.Empty);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "DeploymentPageFlyout.Exit_Clicked", "OK");
            }
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