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
        public ChmlFrphomePage()
        {
            InitializeComponent();
            Reminding.LogsOutputting("进入ChmlFrphomePage");
            rdLaunchPage_Click(null, null);
        }

        private void rdLaunchPage_Click(object sender, System.Windows.RoutedEventArgs e)
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
