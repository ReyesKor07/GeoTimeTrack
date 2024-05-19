using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack.FlyoutTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeploymentPage : FlyoutPage
    {
        public DeploymentPage()
        {
            InitializeComponent();
            // Manejo del evento cuando se selecciona un elemento del menú
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Obtener el elemento seleccionado del menú
            var item = e.SelectedItem as DeploymentPageFlyoutMenuItem;
            if (item == null)
                return;

            // Crear una instancia de la página correspondiente al elemento seleccionado
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            // Establecer la página seleccionada como detalle del FlyoutPage
            Detail = new NavigationPage(page);
            IsPresented = false; // Cerrar el menú lateral

            FlyoutPage.ListView.SelectedItem = null; // Desmarcar el elemento seleccionado en el menú
        }
    }
}