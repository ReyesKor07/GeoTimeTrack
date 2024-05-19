using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeoTimeTrack.FlyoutTabbed.DeployPageFlyout;

namespace GeoTimeTrack.FlyoutTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeploymentPageFlyout : ContentPage
    {
        public int UserID { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Rol { get; private set; }

        public ListView ListView;

        public DeploymentPageFlyout()
        {
            InitializeComponent();
            InitializeUserData();
            BindingContext = new DeploymentPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private void InitializeUserData()
        {
            // Obtiene los datos del usuario desde LoginPage
            UserID = LoginPage.UserID;
            Name = LoginPage.Rol;
            LastName = LoginPage.LastName;
            Email = LoginPage.Email;
            Rol = LoginPage.Rol;

            // Actualiza los Labels con la información del usuario
            NombreLabel.Text = $"ID: {UserID} \n{Name} {LastName}";
            EmailLabel.Text = $"Email: \n{Email}";

            // Muestra u oculta elementos basados en el rol del usuario
            if (Rol == "Administrador")
            {
                AdminButton.IsVisible = true;
                RolLabel.IsVisible = true;
                RolLabel.Text = $"{Rol}";
            }
            else
            {
                AdminButton.IsVisible = false;
                RolLabel.IsVisible = false;
            }
        }

        public async void NavigationProfilePage()
        {
            // Navega a la página de perfil
            await Navigation.PushModalAsync(new ProfilePage());
        }

        public async void NavigationAdminPage()
        {
            // Navega a la página de administrador
            await Navigation.PushModalAsync(new AdminPage());
        }

        private async void Cuenta_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Realiza la navegación a la página de perfil
                NavigationProfilePage();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "DeploymentPageFlyout.Cuenta_Clicked", "OK");
            }
        }

        private async void Admin_Clicked(object sender, EventArgs e)
        {
            // Verifica la conectividad de red antes de navegar
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para ingresar al panel administrador.", "OK");
                return;
            }

            try
            {
                // Realiza la navegación a la página de administrador
                NavigationAdminPage();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + "DeploymentPageFlyout.Admin_Clicked", "OK");
            }
        }

        private async void Exit_Clicked(object sender, EventArgs e)
        {
            // Verifica la conectividad de red antes de cerrar sesión
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Necesitas estar conectado a Internet para cerrar sesión.", "OK");
                return;
            }

            try
            {
                // Pide confirmación al usuario antes de cerrar sesión
                bool answer = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas salir de tu cuenta?", "Sí", "No");

                if (answer)
                {
                    // Navega a la página de inicio de sesión si se confirma la salida
                    App.Current.MainPage = new NavigationPage(new LoginPage());
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
                // Inicializa la colección de elementos del menú
                MenuItems = new ObservableCollection<DeploymentPageFlyoutMenuItem>
                {
                    //new DeploymentPageFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    //new DeploymentPageFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    // Añadir más elementos si es necesario
                };
            }

            #region INotifyPropertyChanged Implementation

            public event PropertyChangedEventHandler PropertyChanged;

            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }
    }
}