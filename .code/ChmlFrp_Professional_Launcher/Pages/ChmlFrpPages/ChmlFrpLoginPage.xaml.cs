using IniParser;
using IniParser.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// ChmlFrpLoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChmlFrpLoginPage : Page
    {
        private Reminding Reminding = new();
        private SetPath SetPath = new();
        private Downloadfiles Downloadfiles = new();
        private IniData data;

        public ChmlFrpLoginPage()
        {
            InitializeComponent();
            Reminding.LogsOutputting("进入ChmlFrpLoginPage");
            var parser = new FileIniDataParser();
            data = parser.ReadFile(SetPath.setupIniPath);
            if (data["ChmlFrp_Professional_Launcher Setup"]["Username"] != "")
            {
                TextBox_Username.Text = data["ChmlFrp_Professional_Launcher Setup"]["Username"];
            }
            if (data["ChmlFrp_Professional_Launcher Setup"]["Password"] != "")
            {
                TextBox_password.Password = data["ChmlFrp_Professional_Launcher Setup"]["Password"];
            }
        }

        private void TextBox_Username_ini(object sender, TextChangedEventArgs e)
        {
            if (TextBox_Username.Text == "用户名或邮箱")
            {
                TextBox_Username.Foreground = new SolidColorBrush(Colors.Gray);
                return;
            }
            else if (TextBox_Username.Text == "")
            {
                TextBox_Username.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                TextBox_Username.Foreground = new SolidColorBrush(Colors.Black);
            }
            var parser = new FileIniDataParser();
            data["ChmlFrp_Professional_Launcher Setup"]["Username"] = TextBox_Username.Text;
            parser.WriteFile(SetPath.setupIniPath, data);
        }

        private void logon(object sender, RoutedEventArgs e)
        {
            logonButton.Click -= logon;
            data["ChmlFrp_Professional_Launcher Setup"]["Password"] = TextBox_password.Password;
            var parser = new FileIniDataParser();
            parser.WriteFile(SetPath.setupIniPath, data);
            if (Downloadfiles.GitAPI_Login(true))
            {
                NavigationService.Navigate(new ChmlFrphomePage());
                return;
            }
            else
            {
                logonButton.Click += logon;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Username.Text == "用户名或邮箱")
            {
                TextBox_Username.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Username.Text == "")
            {
                TextBox_Username.Text = "用户名或邮箱";
            }
        }
    }
}