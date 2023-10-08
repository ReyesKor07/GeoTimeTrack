using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using GeoTimeTrack.Data;

namespace GeoTimeTrack.FlyoutTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackTimePage : ContentPage
    {
        public TrackTimePage()
        {
            InitializeComponent();
        }
    }
}