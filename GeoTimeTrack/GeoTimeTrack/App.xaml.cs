using Xamarin.Forms;

namespace GeoTimeTrack
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // MainPage = new MainPage();
            MainPage = new NavigationPage(new HomePage());
            // MainPage = new NavigationPage(new MainPage());
            // MainPage = new NavigationPage(new DeploymentPage());
            // MainPage = new NavigationPage(new DeploymentPageFlyout());
            // MainPage = new NavigationPage(new AccountCreationPage());
            // MainPage = new NavigationPage(new TrackTimePage());
            // MainPage = new NavigationPage(new ProfilePage());
            // MainPage = new NavigationPage(new AdminPage());
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