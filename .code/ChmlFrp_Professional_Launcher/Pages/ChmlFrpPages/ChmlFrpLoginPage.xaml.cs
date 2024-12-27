using System.Windows;
using System.Windows.Controls;
using IniParser.Model;
using IniParser;


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

        public ChmlFrpLoginPage()
        {
            InitializeComponent();
            Reminding.LogsOutputting("进入ChmlFrpLoginPage");
            IniData data;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(SetPath.setupIniPath);
            TextBox_Username.Text = data["ChmlFrp_Professional_Launcher Setup"]["Username"];
            TextBox_password.Text = data["ChmlFrp_Professional_Launcher Setup"]["Password"];
        }

        private void TextBox_Username_ini(object sender, TextChangedEventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(SetPath.setupIniPath);
            data["ChmlFrp_Professional_Launcher Setup"]["Username"] = TextBox_Username.Text;
            parser.WriteFile(SetPath.setupIniPath, data);
        }

        private void TextBox_password_ini(object sender, TextChangedEventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(SetPath.setupIniPath);
            data["ChmlFrp_Professional_Launcher Setup"]["Password"] = TextBox_password.Text;
            parser.WriteFile(SetPath.setupIniPath, data);
        }

        private void logon(object sender, RoutedEventArgs e)
        {
            logonButton.Click -= logon;
            if (Downloadfiles.GitAPI_Login())
            {
                NavigationService.Navigate(new ChmlFrphomePage());
                return;
            }
            else
            {
                TextBox_Username.Text = null;
                TextBox_password.Text = null;
                logonButton.Click += logon;
            }
        }
    }
}
