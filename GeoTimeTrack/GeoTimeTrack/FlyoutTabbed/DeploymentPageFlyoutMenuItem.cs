using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoTimeTrack.FlyoutTabbed
{

    public class DeploymentPageFlyoutMenuItem
    {
        public DeploymentPageFlyoutMenuItem()
        {
            TargetType = typeof(DeploymentPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}