using System.Windows;
using System.Windows.Controls;

namespace ChmlFrp_Professional_Launcher.Pages.ChmlFrpPages.Register
{
    /// <summary>
    /// RegisterPageOne.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterPageOne : Page
    {
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
        Reminding Reminding = new();

        public RegisterPageOne()
        {
            InitializeComponent();
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.PagesNavigation.Navigate(
                MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.LoginPage
            );
            return;
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            if (TextBox_Username.Text == "" || TextBox_password.Text == "" || TextBox_QQ.Text == "")
            {
                Reminding.RemindingShow("请填写完整信息", "red");
                return;
            }
            MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.PagesNavigation.Navigate(
                MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.LoginPage.RegisterPageTwo
            );
        }

        private void TextBox_Username_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Username.TextTwo = "";
        }

        private void TextBox_QQ_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_QQ.TextTwo = "";
        }

        private void TextBox_password_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_password.TextTwo = "";
        }

        private void TextBox_Username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Username.Text == "")
            {
                TextBox_Username.TextTwo = "用户名";
            }
        }

        private void TextBox_password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_password.Text == "")
            {
                TextBox_password.TextTwo = "密码";
            }
        }

        private void TextBox_QQ_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_QQ.Text == "")
            {
                TextBox_QQ.TextTwo = "QQ";
            }
        }
    }
}
