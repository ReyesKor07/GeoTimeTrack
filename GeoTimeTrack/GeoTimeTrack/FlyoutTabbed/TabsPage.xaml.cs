using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// [1] // using Xamarin.Forms.PlatformConfiguration;
// [1] // using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace GeoTimeTrack.FlyoutTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TabsPage : TabbedPage
    {
		public TabsPage ()
		{
			InitializeComponent ();
            // [1] // On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            // [1] // On<Android>().SetIsSmoothScrollEnabled(true);
        }
    }
}