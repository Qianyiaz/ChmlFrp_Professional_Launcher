using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;
using IniParser.Model;
using IniParser;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// ChmlFrpLoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChmlFrpLoginPage : Page
    {
        private ClienterClass clienterClass;

        public ChmlFrpLoginPage()
        {
            InitializeComponent();
            clienterClass = new ClienterClass();
            IniData data;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(clienterClass.setupIniPath);
            TextBox_Username.Text = data["ChmlFrp_Professional_Launcher Setup"]["Username"];
            TextBox_password.Text = data["ChmlFrp_Professional_Launcher Setup"]["Password"];
        }

        private void TextBox_Username_ini(object sender, TextChangedEventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(clienterClass.setupIniPath);
            data["ChmlFrp_Professional_Launcher Setup"]["Username"] = TextBox_Username.Text;
            parser.WriteFile(clienterClass.setupIniPath, data);
        }

        private void TextBox_password_ini(object sender, TextChangedEventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(clienterClass.setupIniPath);
            data["ChmlFrp_Professional_Launcher Setup"]["Password"] = TextBox_password.Text;
            parser.WriteFile(clienterClass.setupIniPath, data);
        }

        private void logon(object sender, RoutedEventArgs e)
        {
            logonButton.Click -= logon;
            string url = "https://cf-v2.uapis.cn/login?username=" + TextBox_Username.Text + "&password=" + TextBox_password.Text;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    File.WriteAllText(clienterClass.temp_api_path, webClient.DownloadString(url));
                }
                catch
                {
                    return;
                }
            }

            string jsonContent = File.ReadAllText(clienterClass.temp_api_path);
            var parsedJson = JToken.Parse(jsonContent);
            string formattedJson = parsedJson.ToString(Formatting.Indented);
            var jsonObject = JObject.Parse(jsonContent);
            string msg = jsonObject["msg"]?.ToString();
            if (msg == "登录成功")
            {
                NavigationService.Navigate(new ChmlFrphomePage());
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
