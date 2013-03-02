using Bing.Maps;

namespace SensisShowdown.Views
{
    public sealed partial class MainView
    {
        public MainView()
        {
            this.InitializeComponent();

            var melbourneLocation = new Location(-37.815466076788368, 144.96403013003976);
            map.ZoomLevel = 14;
            map.Center = melbourneLocation;
            map.SetView(melbourneLocation);    // Workaround: There is an issue with the map. Images won't display until you move the map.

        }
    }
}
