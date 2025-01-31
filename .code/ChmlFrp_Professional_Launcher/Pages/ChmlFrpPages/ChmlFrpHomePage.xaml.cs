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
        private Reminding Reminding = new();
        Downloadfiles Downloadfiles = new();

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
            rdLaunchPage_Click(null, null);
        }

        public void rdLaunchPage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new HomePage());
            Reminding.LogsOutputting("进入HomePage");
        }

        private void rdTMA_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new TMAPage());
            Reminding.LogsOutputting("进入TMAPage");
        }
    }
}
