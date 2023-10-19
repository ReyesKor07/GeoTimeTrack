using GeoTimeTrack.FlyoutTabbed;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeoTimeTrack
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Envolver la página en un NavigationPage
            // MainPage = new MainPage();
            MainPage = new TrackTimePage();
            // MainPage = new NavigationPage(new HomePage());
            // MainPage = new NavigationPage(new DeploymentPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}