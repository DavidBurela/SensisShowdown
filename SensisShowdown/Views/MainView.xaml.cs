using Bing.Maps;
using SensisShowdown.ViewModels;

namespace SensisShowdown.Views
{
    public sealed partial class MainView
    {
        public MainViewModel ViewModel { get; set; }

        public MainView()
        {
            this.InitializeComponent();

            var melbourneLocation = new Location(-37.815466076788368, 144.96403013003976);
            map.ZoomLevel = 14;
            map.Center = melbourneLocation;
            map.SetView(melbourneLocation);    // Workaround: There is an issue with the map. Images won't display until you move the map.

            ViewModel = this.DataContext as MainViewModel;
        }
        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.ShowDown();
        }
    }
}
