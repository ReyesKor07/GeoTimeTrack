using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
// [1] // using Xamarin.Forms.PlatformConfiguration;
// [1] // using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

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