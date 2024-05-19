using System;

namespace GeoTimeTrack.FlyoutTabbed
{

    public class DeploymentPageFlyoutMenuItem
    {
        public DeploymentPageFlyoutMenuItem()
        {
            // Inicializa TargetType con el tipo de la clase
            TargetType = typeof(DeploymentPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}