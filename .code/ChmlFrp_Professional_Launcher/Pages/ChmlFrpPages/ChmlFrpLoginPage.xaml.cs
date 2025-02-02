using System.Windows;
using System.Windows.Controls;
using ChmlFrp_Professional_Launcher.Pages.ChmlFrpPages.Login;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// ChmlFrpLoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChmlFrpLoginPage : Page
    {
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
        public LoginPage LoginPage = new();

        public ChmlFrpLoginPage()
        {
            InitializeComponent();
            PagesNavigation.Navigate(LoginPage);
        }

        private void Border_MouseLeftButtonDown(
            object sender,
            System.Windows.Input.MouseButtonEventArgs e
        )
        {
            MainWindow.DragMove();
        }
    }
}
