using System.Windows;
using System.Windows.Controls;
using ChmlFrp_Professional_Launcher.Pages.ChmlFrpLoginPages;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// ChmlfrpPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChmlFrphomePage : Page
    {
        Downloadfiles Downloadfiles = new();
        HomePage HomePage;
        TMAPage TMAPage;

        public ChmlFrphomePage()
        {
            InitializeComponent();
            if (!Downloadfiles.GitAPI_Login(false))
            {
                MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
                ChmlFrpLoginPage ChmlFrpLoginPage = new();
                MainWindow.PagesNavigationtwo.Navigate(ChmlFrpLoginPage);
                return;
            }
            HomePage = new();
            TMAPage = new();
            rdLaunchPage_Click(null, null);
        }

        public void rdLaunchPage_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(HomePage);
        }

        private void rdTMA_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(TMAPage);
        }
    }
}
