using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack.FlyoutTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeploymentPageFlyout : ContentPage
    {
        public ListView ListView;

        public DeploymentPageFlyout()
        {
            InitializeComponent();

            BindingContext = new DeploymentPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class DeploymentPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<DeploymentPageFlyoutMenuItem> MenuItems { get; set; }
            
            public DeploymentPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<DeploymentPageFlyoutMenuItem>(new[]
                {
                    new DeploymentPageFlyoutMenuItem { Id = 0, Title = "Cuenta" },
                    new DeploymentPageFlyoutMenuItem { Id = 1, Title = "Configuraciones" },
                 // new DeploymentPageFlyoutMenuItem { Id = 2, Title = "Page 3" },
                 // new DeploymentPageFlyoutMenuItem { Id = 3, Title = "Page 4" },
                 // new DeploymentPageFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
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