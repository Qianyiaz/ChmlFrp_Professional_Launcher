using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChmlFrp_Professional_Launcher.Pages.ChmlFrpPages.Register;
using IniParser;
using IniParser.Model;
using Newtonsoft.Json.Linq;

namespace ChmlFrp_Professional_Launcher.Pages.ChmlFrpPages.Login
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
        private SetPath SetPath = new();
        private Downloadfiles Downloadfiles = new();
        private IniData data;
        private FileIniDataParser parser = new();
        public RegisterPageOne RegisterPageOne = new();
        public RegisterPageTwo RegisterPageTwo = new();

        public LoginPage()
        {
            InitializeComponent();
            data = parser.ReadFile(SetPath.setupIniPath);
        }

        private async void logon(object sender, RoutedEventArgs e)
        {
            logonButton.Click -= logon;
            data["ChmlFrp_Professional_Launcher Setup"]["Password"] = TextBox_password.Text;
            parser.WriteFile(SetPath.setupIniPath, data);
            data["ChmlFrp_Professional_Launcher Setup"]["Username"] = TextBox_Username.Text;
            parser.WriteFile(SetPath.setupIniPath, data);
            if (Downloadfiles.GitAPI_Login(true))
            {
                string jsonContent = System.IO.File.ReadAllText(SetPath.temp_api_path);
                var jsonObject = JObject.Parse(jsonContent);
                data["ChmlFrp_Professional_Launcher Setup"]["Token"] = jsonObject["data"]
                    ["usertoken"]
                    ?.ToString();
                parser.WriteFile(SetPath.setupIniPath, data);
                await Task.Delay(1000);
                this.Visibility = Visibility.Collapsed;
                MainWindow.ChmlFrpHomePage = new();
                MainWindow.PagesNavigation.Navigate(MainWindow.ChmlFrpHomePage);
                return;
            }
            else
            {
                logonButton.Click += logon;
                return;
            }
        }

        private void TextBox_Username_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Username.TextTwo = "";
        }

        private void TextBox_password_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_password.TextTwo = "";
        }

        private void TextBox_password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_password.Text == "")
            {
                TextBox_password.TextTwo = "密码";
            }
        }

        private void TextBox_Username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Username.Text == "")
            {
                TextBox_Username.TextTwo = "用户名或邮箱";
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.Visibility = Visibility.Collapsed;
            MainWindow.LaunchPageButton.IsChecked = true;
            MainWindow.ChmlfrpPageButton.Click += MainWindow.rdChmlfrpPage_Click;
            MainWindow.PagesNavigation.Navigate(MainWindow.LaunchPage);
            return;
        }

        //private void Registerpage_Navigate(object sender, RoutedEventArgs e)
        //{
        //    MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.PagesNavigation.Navigate(RegisterPageOne);
        //}
    }
}
