using System.IO;
using System.Windows.Controls;
using Path = System.IO.Path;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Windows.Media.Imaging;
using System.Net;


namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// ChmlfrpPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChmlFrphomePage : Page
    {
        public ChmlFrphomePage()
        {
            InitializeComponent();
            Uri uri = new Uri("/Pages/ChmlFrpPages/ChmlFrpLoginedPages/HomePage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }

        private void rdLaunchPage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Uri uri = new Uri("/Pages/ChmlFrpPages/ChmlFrpLoginedPages/HomePage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }

        private void rdTMA_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Uri uri = new Uri("/Pages/ChmlFrpPages/ChmlFrpLoginedPages/TMAPage.xaml", UriKind.Relative);
            Pages1Navigation.Source = uri;
        }
    }
}
